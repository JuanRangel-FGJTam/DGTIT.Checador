using DGTIT.Checador.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace DGTIT.Checador.Views
{
    public class Checador : CaptureForm
    {
        private readonly UsuariosDBEntities contexto;
        private readonly procuraduriaEntities1 procu;
        private readonly ChecadorService checadorService;
        private readonly FiscaliaService fiscaliaService;
        private readonly List<long> areasAvailables = new List<long>();
        private System.Windows.Forms.Timer timer1;
        
        private DPFP.Verification.Verification Verificator;

        private Task taskAfterCheck;
        private CancellationTokenSource cancelationSource;


        public Checador() : base()
        {
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
            
            
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(OnTimerTick);
            timer1.Start();
        }

        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            if (features != null)
            {
                // * stop the service of the capturing the fingerprints
                StopCapturing();

                // prepare for validate each employees fingerprints
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                DPFP.Template template = new DPFP.Template();
                
                // loop for each employee and validate the finger print
                foreach (var emp in checadorService.GetEmployees())
                {
                    if (emp.fingerprint == null)
                    {
                        continue;
                    }

                    // * validate the fingerprint of each employee
                    using( Stream ms = new MemoryStream(emp.fingerprint) ){ 
                        template = new DPFP.Template(ms);
                    }
                    try
                    {
                        Verificator.Verify(features, template, ref result);
                    }
                    catch(Exception){
                        continue;
                    }

                    if (!result.Verified)
                    {
                        continue;
                    }

                    // * validete if the employee is active
                    if (!emp.active)
                    {
                        SetNoRegistrada("Empleado en baja");
                        SetAreaNoEncontrada();
                        break;
                    }


                    // * validete if the employee direction has assigned the area
                    if (emp.general_direction_id <= 0)
                    {
                        SetNoRegistrada("No cuenta con area registrada");
                        SetAreaNoEncontrada();
                        break;
                    }

                    // * validate if the employee area its the same area of the checador
                    if (!this.areasAvailables.Contains(emp.general_direction_id))
                    {
                        SetNoRegistrada("No pertenece a esta Area");
                        SetAreaNoEncontrada();
                        break;
                    }

                    // * get the employee number
                    var employeeNumber = Convert.ToInt32(emp.plantilla_id) - 100000;

                    // * display the photo and name of the employee
                    SetFotoEmpleado(fiscaliaService.GetEmployeePhoto(employeeNumber));
                    SetNombre(emp.name.ToString());

                    // * make the checkin record
                    var cheeckTime = this.checadorService.CheckInEmployee(employeeNumber);

                    // * display the employee is checked on the UI
                    SetChecada(cheeckTime);
                    PlayBell();

                    break;
                }

                if (result.Verified == false) //error en huella
                {
                    SetNoRegistrada("No se reconoce la huella");
                }


                // * task for clear the UI and unlock the fingerPrint device after some delay
                cancelationSource = new CancellationTokenSource();
                CancellationToken ct = cancelationSource.Token;
                taskAfterCheck = Task.Run(() => {
                    try
                    {
                        // sleep 3 seconds
                        System.Threading.Thread.Sleep(3000);
                        
                        // check if the task was cancelled
                        if (ct.IsCancellationRequested)
                        {
                            return;
                        }

                        // clear UI and restart eh capturing service
                        LimpiarCampos();
                        StartCapturing();
                    }
                    catch (OperationCanceledException) {}
                }, ct);
            }
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

        private void OnTimerTick(object sender, EventArgs e)
        {
            var dateQuery = contexto.Database.SqlQuery<DateTime>("SELECT getdate()");
            DateTime serverDate = dateQuery.AsEnumerable().First();
            this.lblFecha.Text = serverDate.ToString("dd/MM/yyyy");
            this.lblHora.Text = serverDate.ToString("hh:mm:ss");
        }

        protected override void CaptureFormClosing(object sender, FormClosingEventArgs e)
        {
            base.CaptureFormClosing(sender, e);
            if( cancelationSource != null)
            {
                cancelationSource.Cancel();
            }
        }

    }
}
