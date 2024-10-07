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
using DGTIT.Checador.Services;

namespace DGTIT.Checador
{
    public partial class FrmEmpleados : Form
    {
        private readonly FiscaliaService fiscaliaService;

        private UsuariosDBEntities contexto;
        private procuraduriaEntities1 procu;
        private int numeroEmpleado = 0;
        private string nombreEmpleado = "";
        private List<AREA> areasList = new List<AREA>();
        

        public FrmEmpleados()
        {
            InitializeComponent();
            
            // * initialized the dbContext 
            contexto = new UsuariosDBEntities();
            procu = new procuraduriaEntities1();
            
            // * initialized services
            fiscaliaService = new FiscaliaService(procu, contexto);

            this.btnEdit.Click += new EventHandler(BtnEditarClick);
        }

        private void FormOnLoad(object sender, EventArgs e)
        {
            // * initialize the dataGrid
            this.dataGridEmpleados.AutoGenerateColumns = false;
            this.dataGridEmpleados.Columns.Clear();
            this.dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nu Empleado",
                DataPropertyName = "NumEmpleado",
                Width = 120
            });
            this.dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 180
            });
            this.dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Apellido Paterno",
                DataPropertyName = "Paterno",
                Width = 180
            });
            this.dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Apellido Materno",
                DataPropertyName = "Materno",
                Width = 180
            });
            this.dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Curp",
                DataPropertyName = "Curp",
                Width = 180
            });
            this.dataGridEmpleados.DataSource = null;
            

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

                var areaSelected = (AREA) this.areaComboBox.SelectedItem;
                var textSearch = this.txtSearch.Text;

                // * ensured that at least one filter is specified to prevent display all the employees
                if (areaSelected == null && (string.IsNullOrEmpty(textSearch) || textSearch.Length < 3))
                {
                    return;
                }

                // * retrive the employees
                var areaSelectedID = areaSelected == null ? 0 : areaSelected.IDAREA;
                var empleados = fiscaliaService.SearchEmployees(textSearch, areaSelectedID ).ToList();
                dataGridEmpleados.DataSource = empleados;

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
                    numeroEmpleado = Convert.ToInt32(row.Cells[0].Value.ToString());
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
           
        private void BtnEditarClick(object sender, EventArgs e)
        {
            var formRegistrar = new FrmRegistrar(this.numeroEmpleado);
            formRegistrar.ShowDialog(this);
        }
    }
}
