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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using DGTIT.Checador.Services;

namespace DGTIT.Checador.Views
{
    public class Checador : CaptureForm
    {
        private readonly UsuariosDBEntities contexto;
        private readonly procuraduriaEntities1 procu;
        private readonly ChecadorService checadorService;
        private readonly FiscaliaService fiscaliaService;
        private readonly List<long> areasAvailables = new List<long>();
        private System.Windows.Forms.Timer timerDateTime;
        private System.Windows.Forms.Timer timerLogStatus;

        private DPFP.Verification.Verification Verificator;
        private Task taskAfterCheck;
        private CancellationTokenSource cancelationSource;
        private bool errorConexion = false;

        public Checador() : base() {
            contexto = new UsuariosDBEntities();
            procu = new procuraduriaEntities1();
            checadorService = new ChecadorService(contexto, procu);
            fiscaliaService = new FiscaliaService(procu, contexto);
            
            // * read the area id
            this.areasAvailables = Properties.Settings.Default["generalDirectionId"].ToString().Split(',').Select(i => Convert.ToInt64(i)).ToList();   
        }

        protected override void Init()
        {
            base.Init();
            base.Text = "Verificación de Huella Digital";
            Verificator = new DPFP.Verification.Verification();

            // * initialize backgrounds
            timerDateTime = new System.Windows.Forms.Timer();
            timerDateTime.Interval = (int) TimeSpan.FromSeconds(1).TotalMilliseconds;
            timerDateTime.Tick += new EventHandler(OnTimerTick);
            timerDateTime.Start();

            timerLogStatus = new System.Windows.Forms.Timer();
            timerLogStatus.Interval = (int) TimeSpan.FromMinutes(1).TotalMilliseconds;
            timerLogStatus.Tick += new EventHandler(OnTimerLogTick);
            timerLogStatus.Start();
        }

        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);

            SetLoading(true);

            cancelationSource = new CancellationTokenSource();
            CancellationToken ct = cancelationSource.Token;

            Task.Run(() => {
                ValidateFingerPrint(Sample, ct);
            }, ct);

        }

        public static void DelayAction(int millisecond, Action action)
        {
            var timer = new DispatcherTimer();
            timer.Tick += delegate
            {
                action.Invoke();
                timer.Stop();
            };

            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (cancelationSource != null) {
                cancelationSource.Cancel();
            }

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
        
        private void ValidateFingerPrint(DPFP.Sample Sample, CancellationToken ct) {
            
            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            if (features != null) {
                // * stop the service of the capturing the fingerprints
                StopCapturing();

                // prepare for validate each employees fingerprints
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                DPFP.Template template = new DPFP.Template();


                // loop for each employee and validate the finger print
                IEnumerable<employee> employees = Array.Empty<employee>();
                try {
                    employees = checadorService.GetEmployees().ToArray();
                }
                catch (Exception err) {
                    MakeReport("No se pudo obtener el listado de empleados: " + err.Message, err);
                    SetLoading(false);
                    SetNoRegistrada("Error de conexión");
                    SetAreaNoEncontrada();
                    return;
                }


                // * compare each employee
                foreach (var emp in employees) {
                    if (emp.fingerprint == null) {
                        continue;
                    }

                    // * validate the fingerprint of each employee
                    using (Stream ms = new MemoryStream(emp.fingerprint)) {
                        template = new DPFP.Template(ms);
                    }
                    if(template.Bytes == null) {
                        continue;
                    }

                    Verificator.Verify(features, template, ref result);

                    if (!result.Verified) {
                        continue;
                    }

                    MakeReport($"Empledo No {emp.employee_number} encontrado", EventLevel.Informational);

                    // * validete if the employee is active
                    if (!emp.active) {
                        SetLoading(false);
                        SetNoRegistrada("Empleado en baja");
                        SetEmpledoBaja();
                        MakeReport($"Empledo No {emp.employee_number} en baja", EventLevel.Informational);
                        break;
                    }


                    // * validete if the employee direction has assigned the area
                    if (emp.general_direction_id <= 0) {
                        SetLoading(false);
                        SetNoRegistrada("No cuenta con area registrada");
                        SetAreaNoEncontrada();
                        MakeReport($"Empledo No {emp.employee_number} no cuenta con area registrada", EventLevel.Informational);
                        break;
                    }

                    // * validate if the employee area its the same area of the checador
                    if (!this.areasAvailables.Contains(emp.general_direction_id)) {
                        SetLoading(false);
                        SetNoRegistrada("No pertenece a esta Area");
                        SetAreaNoEncontrada();
                        MakeReport($"Empledo No {emp.employee_number} no pertenece a esta Area", EventLevel.Informational);
                        break;
                    }

                    // * get the employee number
                    var employeeNumber = Convert.ToInt32(emp.plantilla_id) - 100000;

                    // * make the checkin record
                    DateTime cheeckTime = default;
                    try {
                        cheeckTime = this.checadorService.CheckInEmployee(employeeNumber);
                    }
                    catch (Exception err) {
                        SetLoading(false);
                        SetNoRegistrada("Error de conexión");
                        SetAreaNoEncontrada();
                        MakeReport(err.Message, err);
                        return;
                    }

                    // * display the photo and name of the employee
                    SetFotoEmpleado(fiscaliaService.GetEmployeePhoto(employeeNumber));
                    SetNombre(emp.name.ToString());

                    // * display the employee is checked on the UI
                    SetLoading(false);
                    SetChecada(cheeckTime);

                    break;
                }

                // no match found
                if (result.Verified == false) {
                    SetLoading(false);
                    SetNoRegistrada("No se reconoce la huella");
                    MakeReport("No se encotnro coincidencia de la huella", EventLevel.Informational);
                    SetAreaNoEncontrada();
                }

                // * task for clear the UI and unlock the fingerPrint device after some delay
                taskAfterCheck = Task.Run(() => {
                    try {
                        // sleep 3 seconds
                        System.Threading.Thread.Sleep(2500);

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
            }
        }

        #region background workers
        private void OnTimerTick(object sender, EventArgs e) {
            try {

                DateTime? serverDate = null;

                // get the date
                var task1 = Task.Run(() => {
                    serverDate = contexto.Database.SqlQuery<DateTime>("SELECT getdate()").First();
                });

                var task2 = Task.Run(() => {
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                });

                var taskCompleteIndex = Task.WaitAny(task1, task2);

                if (taskCompleteIndex == 1) {
                    throw new TimeoutException();
                }

                // * update the date and hour on the UI
                SetDateTimeServer(serverDate);

                // * clear data if before was without connection
                if (errorConexion) {
                    LimpiarCampos();
                    StartCapturing();
                    errorConexion = false;
                    MakeReport("Se recupero la conexion al tratar de obtener la fecha del servidor", EventLevel.Informational);
                }
            }
            catch (Exception err) {
                if (!errorConexion){
                    StopCapturing();
                    SetLostConnection();
                    MakeReport("Se perdio la conexion al tratar de obtener la fecha del servidor", err);
                }
                errorConexion = true;
            }
        }

        private void OnTimerLogTick(object sender, EventArgs e) {

            if(errorConexion) {
                return;
            }

            try {
                var _ipAddress = GetIpAddress();
                var _name = Properties.Settings.Default["name"].ToString();
                contexto.Database.ExecuteSqlCommand($"INSERT INTO [dbo].[clientsStatusLog]([name],[address],[updated_at]) VALUES ('{_name}','{_ipAddress}', getdate())");
            }
            catch (Exception err) {
                MakeReport(err.Message, err);
            }
        }
        #endregion

    }
}
    