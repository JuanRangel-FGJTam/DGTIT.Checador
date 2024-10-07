using DGTIT.Checador.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DGTIT.Checador
{
    public partial class FrmRegistrar : Form
    {
        private readonly ChecadorService checadorService;
        private readonly FiscaliaService fiscaliaService;

        private DPFP.Template Template;
        private UsuariosDBEntities contexto;
        private procuraduriaEntities1 procu;

        private readonly int employeeNumber;
        private employee currentEmployee;
        
        public FrmRegistrar(int employeeNumber)
        {
            InitializeComponent();
            this.employeeNumber = employeeNumber;

            contexto = new UsuariosDBEntities();
            procu = new procuraduriaEntities1();

            this.checadorService = new ChecadorService(contexto);
            this.fiscaliaService = new FiscaliaService(procu, contexto);
            
        }
        private void OnLoaded(object sender, EventArgs e)
        {
            LoadCatalogs();

            // * load the employee
            this.currentEmployee = checadorService.GetEmployee(employeeNumber);

            // * display the employee info
            this.tb_employeeNumber.Text = employeeNumber.ToString();
            this.txtNombre.Text = currentEmployee.name;
            this.cboDirGral.SelectedValue = currentEmployee.general_direction_id;
            this.cboDireccion.SelectedValue = currentEmployee.direction_id;
            this.cboSubdireccion.SelectedValue = currentEmployee.subdirectorate_id;
            this.cboDepartamento.SelectedValue = currentEmployee.department_id;

            // * get the working hours
            var workingHours = checadorService.GetWorkinHours(employeeNumber);
            if( workingHours != null)
            {
                this.txtEntrada.Text = workingHours.checkin;
                this.txtComida.Text = workingHours.toeat;
                this.txtRegreso.Text = workingHours.toarrive;
                this.txtSalida.Text = workingHours.checkout;
            }                                               

            // * get the image of the fingerprint
            if (this.currentEmployee.fingerprint != null)
            {
                this.btnRegistrarHuella.Text = "Actualizar Huella";
                //Bitmap bmp = null;
                //using (var ms = new MemoryStream(this.currentEmployee.fingerprint))
                //{
                //    var sample = new DPFP.Sample(ms);
                //    var sampleConversion = new DPFP.Capture.SampleConversion();
                //    sampleConversion.ConvertToPicture(sample, ref bmp);
                //}
                //this.img_fingerPrint.Image = bmp;

                var x = Convert.ToBase64String(this.currentEmployee.fingerprint);
                this.tbFingerPrint.Text = x;
            }
            else
            {
                this.btnRegistrarHuella.Text = "Registrar Huella";
            }


            // * set the employee foto
            this.img_photo.Image = this.fiscaliaService.GetEmployeePhoto(employeeNumber);
        }


        private void btnRegistrarHuella_Click(object sender, EventArgs e)
        {
            CapturarHuella capturar = new CapturarHuella();
            capturar.OnTemplate += this.OnTemplate;
            capturar.ShowDialog();
        }

        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke(new Function(delegate () {
                Template = template;
                btnAgregar.Enabled = (Template != null);
                if (Template != null)
                {
                    MessageBox.Show("The fingerprint template is ready for fingerprint verification.", "Fingerprint Enrollment");
                    //txtHuella.Text = "Huella capturada correctamente";
                }
                else
                {
                    MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
                }
            }));
        }

        
        private void LoadCatalogs()
        {
            var dirgral = contexto.general_directions.ToList();
            cboDirGral.DataSource = dirgral;
            cboDirGral.ValueMember = "id";
            cboDirGral.DisplayMember = "name";

            var dir = contexto.directions.ToList();
            cboDireccion.DataSource = dir;
            cboDireccion.ValueMember = "id";
            cboDireccion.DisplayMember = "name";

            var subdir = contexto.subdirectorates.ToList();
            cboSubdireccion.DataSource = subdir;
            cboSubdireccion.ValueMember = "id";
            cboSubdireccion.DisplayMember = "name";

            var depa = contexto.departments.ToList();
            cboDepartamento.DataSource = depa;
            cboDepartamento.ValueMember = "id";
            cboDepartamento.DisplayMember = "name";
        }
        
        private void BtnUpdateClick(object sender, EventArgs e)
        {
            //try
            //{
            //      byte[] streamHuella = Template.Bytes;

            //    using (SqlConnection connection = new SqlConnection("data source=192.168.123.245;initial catalog=UsuariosDB;persist security info=True;user id=usernsjp;password=NSJP010713;MultipleActiveResultSets=True"))
            //    {
                    
            //        connection.Open();
            //        string sql7 = " insert into employees (general_direction_id,direction_id,subdirectorate_id,department_id,plantilla_id,name,photo,fingerprint,created_at,updated_at)";
            //        sql7 = sql7 + " values ("+ cboDirGral.SelectedValue + ","+ cboDireccion.SelectedValue +","+ cboSubdireccion.SelectedValue +"," + cboDepartamento.SelectedValue + ",RIGHT('100000' + " +  numeroEmpleado  + ", 6),'" + txtNombre.Text + "','photos/',@param1,getdate(),getdate() )";
            //        sql7 = sql7 + " declare @employee_id Int select @employee_id=max(id) from employees "; 
            //        sql7 = sql7 + " insert into working_hours (employee_id,checkin,toeat,toarrive,checkout,created_at) ";
            //        sql7 = sql7 + " values (@employee_id,'"+ txtEntrada.Text + "','"+ txtComida.Text + "','" + txtRegreso.Text + "','"+ txtSalida.Text + "', getdate()) ";
            //       // sql7 = sql7 + " ";
            //        SqlCommand cmd = new SqlCommand(sql7, connection);
            //        cmd.Parameters.Add("@param1", SqlDbType.Image).Value = streamHuella;
            //        cmd.CommandType = CommandType.Text;
            //        cmd.ExecuteNonQuery();
            //        connection.Close(); 
            //    }
                


            //    MessageBox.Show("Registro agregado a la BD correctamente");
            //    Template = null;
            //    btnAgregar.Enabled = false;

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
