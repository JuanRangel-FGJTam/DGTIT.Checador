using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using DGTIT.Checador.Services;


namespace DGTIT.Checador
{
    public partial class Checador : CaptureForm
    {
        private readonly UsuariosDBEntities contexto;
        private readonly procuraduriaEntities1 procu;
        private readonly ChecadorService checadorService;
        private readonly FiscaliaService fiscaliaService;
        private readonly int currentAreaId = 0;

        private DPFP.Verification.Verification Verificator;

        public Checador()
        {
            InitializeComponent();

            contexto = new UsuariosDBEntities();
            procu = new procuraduriaEntities1();
            checadorService = new ChecadorService(contexto, procu);
            fiscaliaService = new FiscaliaService(procu, contexto);

            // * read the area id
            this.currentAreaId = Convert.ToInt32( File.ReadAllText(@"C:\area.ini") );
        }

        protected override void Init()
        {
            base.Init();
            base.Text = "Verificación de Huella Digital";
            Verificator = new DPFP.Verification.Verification();
        }

        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            if (features != null)
            {
                // Compare the feature set with our template
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();

                DPFP.Template template = new DPFP.Template();
                Stream stream;

                foreach (var emp in checadorService.GetEmployees() )
                {
                    if( emp.fingerprint == null )
                    {
                        continue;
                    }
                    
                    // * validate the fingerprint of each employee
                    stream = new MemoryStream(emp.fingerprint);
                    template = new DPFP.Template(stream);
                    Verificator.Verify(features, template, ref result);
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
                    if (emp.general_direction_id <=  0 )
                    {
                        SetNoRegistrada("No cuenta con area registrada");
                        SetAreaNoEncontrada();
                        break;
                    }

                    // * validate if the employee area its the same area of the checador
                    if (emp.general_direction_id != currentAreaId)
                    {
                        SetNoRegistrada("No pertenece a esta Area");
                        SetAreaNoEncontrada();
                        break;
                    }

                    // * get the employee number
                    var employeeNumber = Convert.ToInt32(emp.plantilla_id) - 100000;

                    // * display the photo and name of the employee
                    SetFotoEmpleado( fiscaliaService.GetEmployeePhoto(employeeNumber) );
                    SetNombre( emp.name.ToString() );

                    // * make the checkin record
                    var cheeckTime = this.checadorService.CheckInEmployee(employeeNumber);

                    // * display the employee is checked on the UI
                    SetChecada(cheeckTime);
                    PlayBell();

                    break;
                }

                if(result.Verified == false ) //error en huella
                {
                    SetNoRegistrada("No se reconoce la huella, favor de reintentar");
                }
                   
                // * clear the UI and unlock the fingerPrint device
                DelayAction(4000, new Action(() => {
                    LimpiarCampos();
                }));
                
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

    }
}

