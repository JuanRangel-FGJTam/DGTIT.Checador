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
using System.Windows.Controls;
using System.Windows.Forms;
using DGTIT.Checador.Helpers;

namespace DGTIT.Checador
{
    public partial class FrmEmpleados : Form
    {
        private UsuariosDBEntities contexto;
        private procuraduriaEntities1 procu;
        private string numeroEmpleado = "";
        private string nombreEmpleado = "";
        private List<AREA> areasList = new List<AREA>();


        public FrmEmpleados()
        {
            InitializeComponent();
        }

        private void FormOnLoad(object sender, EventArgs e)
        {
            contexto = new UsuariosDBEntities();
            procu = new procuraduriaEntities1();
            
            LoadAreaComboBox();
        }

        private void LoadAreaComboBox()
        {
            this.areasList = procu.AREA.ToList();
            this.areaComboBox.DataSource = this.areasList;
            this.areaComboBox.ValueMember = "IDAREA";
            this.areaComboBox.DisplayMember = "AREA1";
            this.areaComboBox.SelectedItem = null;
            this.areaComboBox.SelectedValueChanged += new EventHandler(ComboBoxAreaValueChanged);
        }
        
        private void ListarEmpleados()
        {
            try
            {
                dataGridEmpleados.DataSource = null;

                var empleadosQuery = procu.EMPLEADO.Where(item => item.NUMEMP != 0);
                
                // * attempt to filter the employees by the area selecetd
                var areaSelected = (AREA)this.areaComboBox.SelectedItem;
                if ( areaSelected != null)
                {
                    empleadosQuery = empleadosQuery.Where(item => item.IDAREA == areaSelected.IDAREA);
                }

                // * attempt to filter the employees by the name
                var textSearch = this.txtSearch.Text;
                if(!string.IsNullOrEmpty(textSearch) && textSearch.Length >= 3)
                {
                    empleadosQuery = empleadosQuery.Where(item => 
                            item.NOMBRE.Contains(textSearch) ||
                            item.APELLIDOPATERNO.Contains(textSearch) ||
                            item.APELLIDOMATERNO.Contains(textSearch) 
                    );
                }

                // * ensured that at least one filter is specified to prevent display all the employees
                if ( empleadosQuery == null && ( string.IsNullOrEmpty(textSearch) || textSearch.Length < 3))
                {
                    empleadosQuery = empleadosQuery.Where(item => false);
                }

                // * map the employees for retrieving only the necessary columns
                var empleados = empleadosQuery.Select(emp => new {
                    NUM_EMPLEADO = emp.NUMEMP,
                    EMPLEADO = emp.NOMBRE,
                    PATERNO = emp.APELLIDOPATERNO,
                    MATERNO = emp.APELLIDOMATERNO,
                    CURP = emp.CURP
                }).ToList();

                if (empleados != null)
                {
                    dataGridEmpleados.DataSource = empleados;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBoxSearchTextChanged(object sender, EventArgs e)
        {
            ListarEmpleados();
        }
        private void ComboBoxAreaValueChanged(object sender, EventArgs e)
        {
            ListarEmpleados();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    byte[] streamHuella = Template.Bytes;

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
            //    Limpiar();
            //    Listar();
            //    Template = null;
            //    btnAgregar.Enabled = false;

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
        }

        private void DataGridSelectionChanged(object sender, EventArgs e)
        {
            if (dataGridEmpleados.CurrentRow.Index != -1)
            {
                foreach (DataGridViewRow row in dataGridEmpleados.SelectedRows)
                {
                    numeroEmpleado = row.Cells[0].Value.ToString();
                    string nombre = row.Cells[1].Value.ToString();
                    string paterno = row.Cells[2].Value.ToString();
                    string materno = row.Cells[3].Value.ToString();

                    nombreEmpleado = paterno + ' ' + materno + ' ' + nombre;
                    this.btnEdit.Text = $"Editar '{nombreEmpleado}'";
                    this.btnEdit.Enabled = true;
                }
            }
            else
            {
                this.btnEdit.Text = "Editar empleado";
                this.btnEdit.Enabled = false;
            }

        }
        
    }
}
