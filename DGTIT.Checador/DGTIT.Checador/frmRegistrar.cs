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
    public partial class frmRegistrar : Form
    {
        private DPFP.Template Template;
        private UsuariosDBEntities contexto;
        private procuraduriaEntities1 procu;
        private string numeroEmpleado = "";

        public frmRegistrar()
        {
            InitializeComponent();
        }

        private void btnRegistrarHuella_Click(object sender, EventArgs e)
        {
            CapturarHuella capturar = new CapturarHuella();
            capturar.OnTemplate += this.OnTemplate;
            capturar.ShowDialog();
        }

        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                Template = template;
                btnAgregar.Enabled = (Template != null);
                if (Template != null)
                {
                    MessageBox.Show("The fingerprint template is ready for fingerprint verification.", "Fingerprint Enrollment");
                    txtHuella.Text = "Huella capturada correctamente";
                }
                else
                {
                    MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
                }
            }));
        }

        private void frmRegistrar_Load(object sender, EventArgs e)
        {
            contexto = new UsuariosDBEntities();
            procu = new procuraduriaEntities1();
            Listar();

            LlenarDirGral();
        }

        private void LlenarDirGral()
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

        private void Limpiar()
        {
            txtNombre.Text = "";
        }

        private void Listar()
        {
            try
            {
                //var products = from p in ne.Products
                //               where (supplierIDs.Contains((int)p.SupplierID))
                //               select p;

                var empleados = from emp in procu.EMPLEADO
                                    where (emp.NUMEMP != 0)
                                select new
                                {
                                    NUM_EMPLEADO = emp.NUMEMP,
                                    EMPLEADO = emp.NOMBRE,
                                    PATERNO = emp.APELLIDOPATERNO,
                                    MATERNO = emp.APELLIDOMATERNO
                                };
                if (empleados != null)
                {
                    DataTable dt = ToDataTable(empleados.ToList());

                    dgvListar.DataSource = dt;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            (dgvListar.DataSource as DataTable).DefaultView.RowFilter = string.Format("NUM_EMPLEADO like '{0}%' OR EMPLEADO like '{0}%' OR PATERNO like '{0}%' OR MATERNO like '{0}%'", txtSearch.Text);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                  byte[] streamHuella = Template.Bytes;

                using (SqlConnection connection = new SqlConnection("data source=192.168.123.245;initial catalog=UsuariosDB;persist security info=True;user id=usernsjp;password=NSJP010713;MultipleActiveResultSets=True"))
                {
                    
                    connection.Open();
                    string sql7 = " insert into employees (general_direction_id,direction_id,subdirectorate_id,department_id,plantilla_id,name,photo,fingerprint,created_at,updated_at)";
                    sql7 = sql7 + " values ("+ cboDirGral.SelectedValue + ","+ cboDireccion.SelectedValue +","+ cboSubdireccion.SelectedValue +"," + cboDepartamento.SelectedValue + ",RIGHT('100000' + " +  numeroEmpleado  + ", 6),'" + txtNombre.Text + "','photos/',@param1,getdate(),getdate() )";
                    sql7 = sql7 + " declare @employee_id Int select @employee_id=max(id) from employees "; 
                    sql7 = sql7 + " insert into working_hours (employee_id,checkin,toeat,toarrive,checkout,created_at) ";
                    sql7 = sql7 + " values (@employee_id,'"+ txtEntrada.Text + "','"+ txtComida.Text + "','" + txtRegreso.Text + "','"+ txtSalida.Text + "', getdate()) ";
                   // sql7 = sql7 + " ";
                    SqlCommand cmd = new SqlCommand(sql7, connection);
                    cmd.Parameters.Add("@param1", SqlDbType.Image).Value = streamHuella;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    connection.Close(); 
                }
                /*  employee empleado = new employee()
                 *    employee_id_ = empleado.id,
                      checkin = txtEntrada.Text,
                      toeat = txtComida.Text,
                      toarrive = txtRegreso.Text,
                      checkout = txtSalida.Text,
                      created_at = DateTime.Now
                 {
                      //general_direction_id = (long)cboDirGral.SelectedValue,
                      //direction_id = (long)cboDireccion.SelectedValue,
                      //subdirectorate_id = (long)cboSubdireccion.SelectedValue,
                      //department_id = (long)cboDepartamento.SelectedValue,
                      //plantilla_id = int.Parse(numeroEmpleado) + 10000,
                      name = txtNombre.Text,
                      //photo="C:",
                      //fingerprint = streamHuella,
                      //created_at = DateTime.Now,
                      //updated_at = DateTime.Now,
                      //status_id=1
                  };


          public long general_direction_id { get; set; }
          public long direction_id { get; set; }
          public long subdirectorate_id { get; set; }
          public long department_id { get; set; }
          public Nullable<int> plantilla_id { get; set; }
          public string name { get; set; }
          public string photo { get; set; }
          public byte[] fingerprint { get; set; }
          public Nullable<System.DateTime> created_at { get; set; }
          public Nullable<System.DateTime> updated_at { get; set; }
          public Nullable<int> status_id { get; set; }


                  contexto.employees.Add(empleado);
                 contexto.SaveChanges();

                  working_hours horario = new working_hours()
                  {
                      employee_id_ = empleado.id,
                      checkin = txtEntrada.Text,
                      toeat = txtComida.Text,
                      toarrive = txtRegreso.Text,
                      checkout = txtSalida.Text,
                      created_at = DateTime.Now
                  };

                  contexto.working_hours.Add(horario);
                  contexto.SaveChanges();*/


                MessageBox.Show("Registro agregado a la BD correctamente");
                Limpiar();
                Listar();
                Template = null;
                btnAgregar.Enabled = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dgvListar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvListar.CurrentRow.Index != -1)
            {
                foreach (DataGridViewRow row in dgvListar.SelectedRows)
                {
                     numeroEmpleado = row.Cells[0].Value.ToString();
                    string nombre = row.Cells[1].Value.ToString();
                    string paterno = row.Cells[2].Value.ToString();
                    string materno = row.Cells[3].Value.ToString();

                    txtNombre.Text = paterno + ' ' + materno + ' ' + nombre;


                }
            }
        }
    }
}
