using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DGTIT.Checador.Utilities;
using DGTIT.Checador.Services;
using DPFP;
using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Data.Repositories;
using System.Web.UI.WebControls;

namespace DGTIT.Checador.Views
{
    public class Checador : CaptureForm
    {
        private readonly EmployeeService employeeService;
        private readonly ChecadorService checadorService;
        private readonly FiscaliaService fiscaliaService;
        private readonly List<long> areasAvailables = new List<long>();

        private EmployeeFingerprintMatcher employeeFingerprintM;
        private System.Windows.Forms.Timer timerLogStatus;
        private System.Windows.Forms.Timer timerSyncDateTime;
        private DPFP.Verification.Verification Verificator;
        private Task taskAfterCheck;
        private CancellationTokenSource cancelationSource;
        private InternalClock internalClock;

        
        public Checador() : base()
        {
            
            // * read configurations
            this.areasAvailables = CustomApplicationSettings.GetGeneralDirections().Split(';').Select( i => Convert.ToInt64(i)).ToList();

            // * initialized repos
            var employeeRepo = new SQLClientEmployeeRepository();
            var recordRepo = new SQLClientRecordRepository();
            var procuEmployeeRepo = new ProcuEmployeeRepository();

            // * initialized services
            checadorService = new ChecadorService(employeeRepo, recordRepo);
            fiscaliaService = new FiscaliaService(employeeRepo, procuEmployeeRepo);
            employeeService = new EmployeeService(employeeRepo, procuEmployeeRepo);
            employeeFingerprintM = new EmployeeFingerprintMatcher(this.areasAvailables.Select(item => (int) item));
        }

        protected override void Init()
        {
            base.Init();
            base.Text = "Verificación de Huella Digital";
            Verificator = new DPFP.Verification.Verification();

            // * initialize internal clock
            internalClock = new InternalClock(DateTime.Now, CurrentEventLog);
            internalClock.OnTimeChange = (currentTime) => SetDateTimeServer(currentTime);
            internalClock.StartClock();

            // * initialize backgrounds
            timerLogStatus = new System.Windows.Forms.Timer();
            timerLogStatus.Interval = (int) TimeSpan.FromMinutes(1).TotalMilliseconds;
            timerLogStatus.Tick += new EventHandler(OnTimerLogTick);
            timerLogStatus.Start();

            // * sync the time
            timerSyncDateTime = new System.Windows.Forms.Timer();
            timerSyncDateTime.Interval = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
            timerSyncDateTime.Tick += new EventHandler(SyncClockTime);
            timerSyncDateTime.Start();
            SyncClockTime(null, null);
        }

        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);
            SetLoading(true);
            cancelationSource = new CancellationTokenSource();
            CancellationToken ct = cancelationSource.Token;
            Task.Run(() => {
                ValidateFingerPrint(Sample, ct);
                // * task for clear the UI and unlock the fingerPrint device after some delay
                taskAfterCheck = Task.Run(() => {
                    try {
                        // sleep 3 seconds
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

                        // check if the task was cancelled
                        if (ct.IsCancellationRequested) {
                            return;
                        }

                        // clear UI and restart eh capturing service
                        LimpiarCampos();
                        StartCapturing();
                    }
                    catch (OperationCanceledException) { }
                }, ct);
            }, ct);
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (cancelationSource != null) {
                cancelationSource.Cancel();
            }

            internalClock.StopClock();

            // Call the base method to proceed with closing
            base.OnFormClosing(e);
        }

        private string GetIpAddress() {
            try {

                // Get the hostname of the machine
                string hostName = Dns.GetHostName();

                // Get the IP addresses associated with the hostname
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);

                // Filter out IPv6 addresses and display the first available IPv4 address
                string ipAddress = addresses.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();

                // Display the IP address in the label
                if (!string.IsNullOrEmpty(ipAddress)) {
                    return ipAddress;
                }
                else {
                    return "0.0.0.0";
                }
            }
            catch (Exception err) {
                MakeReport(err.Message, err);
                return "0.0.0.1";
            }
        }
        
        private async void ValidateFingerPrint(DPFP.Sample Sample, CancellationToken ct) {
            
            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);
            if (features == null)
            {
                return;
            }

            // get the employees
            IEnumerable<Employee> employees = Array.Empty<Employee>();
            try
            {
                employees = await this.employeeService.GetEmployees();
            }
            catch (Exception err)
            {
                MakeReport("No se pudo obtener el listado de empleados: " + err.Message, err);
                SetLoading(false);
                SetNoRegistrada("Error de conexión");
                SetAreaNoEncontrada();
                return;
            }

            // * stop the service of the capturing the fingerprints
            StopCapturing();

            var matchingResults = this.employeeFingerprintM.GetMarchingEmployee(employees, features);

            if(matchingResults.IsSuccess)
            {
                // * make the checkin record
                DateTime cheeckTime = DateTime.Now;
                try
                {
                    cheeckTime = await this.checadorService.CheckInEmployee(matchingResults.Data.EmployeeNumber);
                }
                catch (Exception err)
                {
                    SetLoading(false);
                    SetNoRegistrada("Error de conexión");
                    SetAreaNoEncontrada();
                    MakeReport(err.Message, err);
                    return;
                }

                // * display the photo and name of the employee
                var foto = await fiscaliaService.GetEmployeePhoto(matchingResults.Data.EmployeeNumber);
                SetFotoEmpleado(foto);
                SetNombre(matchingResults.Data.Name);
                SetChecada(cheeckTime);
                SetLoading(false);
            }
            else
            {
                switch (matchingResults.Status)
                {
                    case Models.MatchingStatus.INACTIVE:
                        SetNoRegistrada("Empleado en baja");
                        SetEmpledoBaja();
                        MakeReport($"Empledo No {matchingResults.Data.EmployeeNumber} en baja", EventLevel.Informational);
                        break;

                    case Models.MatchingStatus.BAD_AREA:
                        SetNoRegistrada("No pertenece a esta Area");
                        SetAreaNoEncontrada();
                        MakeReport($"Empledo No {matchingResults.Data.EmployeeNumber} no pertenece a esta Area", EventLevel.Informational);
                        break;

                    default:
                        SetNoRegistrada("No se reconoce la huella");
                        SetAreaNoEncontrada();
                        MakeReport("No se encotnro coincidencia de la huella", EventLevel.Informational);
                        break;
                }
                SetLoading(false);
            }
        }

        #region background workers
        private void OnTimerLogTick(object sender, EventArgs e)
        {   
            // TODO: Moved this logic to a service

            //try {
            //    var _ipAddress = GetIpAddress();
            //    var _name = Properties.Settings.Default["name"].ToString();
            //    contexto.Database.ExecuteSqlCommand($"INSERT INTO [dbo].[clientsStatusLog]([name],[address],[updated_at]) VALUES ('{_name}','{_ipAddress}', getdate())");
            //}
            //catch (Exception err) {
            //    MakeReport(err.Message, err);
            //}
        }
        private void SyncClockTime(object sender, EventArgs e)
        {
            try
            {
                MakeReport("Obteniendo hora del servidor.", EventLevel.Informational);

                // get the date from the server
                var task1 = Task.Run<DateTime?>(async () => await this.checadorService.GetServerTime());
                Task.WaitAll(task1);
                DateTime? serverDate = task1.Result;

                if (serverDate == null) throw new Exception("La fecha no se pudo recuperar del servidor.");
                if (internalClock == null) throw new Exception("El error interno no esta inicializado.");
                internalClock.SyncClock(serverDate.Value);
                MakeReport("Reloj interno sincronizado.", EventLevel.Informational);
            }
            catch (Exception err)
            {
                MakeReport("Error al obtener la fecha del servidor", err);
            }
        }
        #endregion
    }
}