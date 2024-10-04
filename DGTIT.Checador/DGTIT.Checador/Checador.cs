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
using System.Net;
using System.Net.NetworkInformation;

namespace DGTIT.Checador
{
    public partial class Checador : CaptureForm
    {
        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;
        private UsuariosDBEntities contexto;
        private procuraduriaEntities1 procu;

        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();
        }

        protected override void Init()
        {
            base.Init();
            base.Text = "Verificación de Huella Digital";
            Verificator = new DPFP.Verification.Verification();     // Create a fingerprint template verificator
            UpdateStatus(0);
        }

        private void UpdateStatus(int FAR)
        {
            // Show "False accept rate" value
            SetStatus(String.Format("False Accept Rate (FAR) = {0}", FAR));
        }

        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            // TODO: move to a separate task
            if (features != null)
            {
                // Compare the feature set with our template
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();

                DPFP.Template template = new DPFP.Template();
                Stream stream;

                foreach (var emp in contexto.employees)
                {
                    stream = new MemoryStream(emp.fingerprint);
                    template = new DPFP.Template(stream);

                    Verificator.Verify(features, template, ref result);
                    UpdateStatus(result.FARAchieved);
                    if (result.Verified)
                    {
                       // MakeReport("huella encontrada de: " + emp.name + " pertenece al departamento: " + emp.general_direction_id);
                     //   MakeReport(  " pertenece al departamento: " + emp.general_direction_id);
                      //  setNoRegistrada(" pertenece al departamento: " + emp.general_direction_id);
                        if (emp.general_direction_id !=  0 )
                        {
                            string text = System.IO.File.ReadAllText(@"C:\area.ini"); 
                            //setNoRegistrada(" Contenido del archivo = " + text);
                            if (Convert.ToString( emp.general_direction_id) == text.Trim())
                            {
                               // EmpleadoChecar(Convert.ToInt32(emp.plantilla_id) - 100000, emp.name.ToString(), emp.id);
                                int numeroEmpleado = Convert.ToInt32(emp.plantilla_id) - 100000;
                                var foto = (procu
                                              .EMPLEADO
                                              .Where(u => u.NUMEMP == numeroEmpleado)
                                              .Select(u => u.FOTO)
                                              .SingleOrDefault());
                                Bitmap bmp;
                                using (var ms = new MemoryStream(foto))
                                {
                                    bmp = new Bitmap(ms);
                                }
                                setNombre(emp.name.ToString());

                                setFotoEmpleado(bmp);
                                var dateQuery = contexto.Database.SqlQuery<DateTime>("SELECT getdate()");
                                DateTime serverDate = dateQuery.AsEnumerable().First();
                                var checadaRegistro = contexto.Set<record>();
                                checadaRegistro.Add(new record
                                {
                                    employee_id = emp.id,
                                    check = serverDate,
                                    created_at = serverDate,
                                    updated_at = serverDate
                                });

                                setChecada(serverDate);
                                //sonido
                                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\Timbre.wav");
                                player.Play();
                            }
                            else
                            {
                                setNoRegistrada("No pertenece a esta Area");
                                setAreaNoEncontrada();
                            }

                            //foreach(var dir in contexto.general_directions)
                            //{
                            //    if(emp.general_direction_id == dir.id )
                            //    {
                            //        IPHostEntry host;
                            //        string localIP = "";
                            //        host = Dns.GetHostEntry(Dns.GetHostName());
                            //        foreach (IPAddress ip in host.AddressList)
                            //        {
                            //            if (ip.AddressFamily.ToString() == "InterNetwork")
                            //            {
                            //                localIP = ip.ToString();
                            //            }
                            //        }
                            //        // MessageBox.Show("Tú IP Local Es: " + localIP);

                            //        if("192.168.124.196" == localIP)  //IP DGTIT 
                            //        {
                            //            //setNoRegistrada(" REGISTRAR CHECADA");
                            //            EmpleadoChecar(Convert.ToInt32(emp.plantilla_id) - 100000 , emp.name.ToString(), emp.id);
                            //        }
                            //        else
                            //        {
                            //            setNoRegistrada(" no  REGISTRAR CHECADA");
                            //        }

                            //        // setNoRegistrada( dir.ip_maquina);
                            //        break;
                            //    }                                
                            //}                           
                            //  setNoRegistrada("departamento ok" );l
                        }
                        else
                        {
                            setNoRegistrada("No cuenta con area registrada");
                            setAreaNoEncontrada();

                        }                        
                        DelayAction(4000, new Action(() => { this.Limpiar(); }));
                        break;
                    }
                    else
                    {
                       // DelayAction(4000, new Action(() => { this.Limpiar(); }));
                    }
                }
                if(result.Verified == false ) //error en huella
                {
                    setNoRegistrada("No se reconoce la huella, favor de reintentar");
                    DelayAction(4000, new Action(() => { this.Limpiar(); }));
                }
                contexto.SaveChanges();
            }
        }
        //public void EmpleadoChecar(int plantilla_id, string name, int id )
        //{
        //  //  int numeroEmpleado = Convert.ToInt32(plantilla_id) - 100000;
        //    var foto = (procu
        //                  .EMPLEADO
        //                  .Where(u => u.NUMEMP == plantilla_id)
        //                  .Select(u => u.FOTO)
        //                  .SingleOrDefault());
        //    Bitmap bmp;
        //    using (var ms = new MemoryStream(foto))
        //    {
        //        bmp = new Bitmap(ms);
        //    }
        //    setNombre(name);

        //    setFotoEmpleado(bmp);
        //    var dateQuery = contexto.Database.SqlQuery<DateTime>("SELECT getdate()");
        //    DateTime serverDate = dateQuery.AsEnumerable().First();
        //    var checadaRegistro = contexto.Set<record>();
        //    checadaRegistro.Add(new record
        //    {
        //        employee_id = id,
        //        check = serverDate,
        //        created_at = serverDate,
        //        updated_at = serverDate
        //    });

        //    setChecada(serverDate);
        //    //sonido
        //    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\Timbre.wav");
        //    player.Play();
        //}

        public void Limpiar()
        {
            LimpiarCampos();
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
        public Checador()
        {
            contexto = new UsuariosDBEntities();
            procu = new procuraduriaEntities1();
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var dateQuery = contexto.Database.SqlQuery<DateTime>("SELECT getdate()");
            DateTime serverDate = dateQuery.AsEnumerable().First();

            this.lblFecha.Text = serverDate.ToString("dd/MM/yyyy");
            this.lblHora.Text = serverDate.ToString("hh:mm:ss");

        }
    }
}

