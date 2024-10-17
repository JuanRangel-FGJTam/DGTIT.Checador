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
        private int serachEmployeeNumber = 0;
        private int selectedEmployeeNumber = 0;
        private string nombreEmpleado = "";
        

        public FrmEmpleados()
        {
            InitializeComponent();
            
            // * initialized the dbContext 
            contexto = new UsuariosDBEntities();
            procu = new procuraduriaEntities1();
            
            // * initialized services
            fiscaliaService = new FiscaliaService(procu, contexto);
               
            // hidden search box
            txtSearch.Enabled = false;
            txtSearch.Visible = false;
            txtEmployeeNumber.Text = "";
            btnEdit.Visible = false;
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
                Width = 130
            });
            this.dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 220
            });
            this.dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Apellido Paterno",
                DataPropertyName = "Paterno",
                Width = 220
            });
            this.dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Apellido Materno",
                DataPropertyName = "Materno",
                Width = 220
            });
            this.dataGridEmpleados.DataSource = null;
            
            
            // * add the events 
            this.btnEdit.Click += new EventHandler(BtnEditarClick);
            this.txtEmployeeNumber.TextChanged += new EventHandler(TextBoxEmployeeNumberChanged);
            this.txtSearch.TextChanged += new EventHandler(TextBoxSearchTextChanged);

            txtEmployeeNumber.Focus();
            txtEmployeeNumber.Select();
        }

        private void ListarEmpleados()
        {

            // * clear selection 
            this.btnEdit.Text = "EDITAR EMPLEADO";
            this.btnEdit.Enabled = false;
            this.btnEdit.Visible = false;
            this.selectedEmployeeNumber = 0;
            this.nombreEmpleado = "";


            try
            {
                dataGridEmpleados.DataSource = null;

                // * ensured that at least one filter is specified to prevent display all the employees
                if (serachEmployeeNumber.ToString().Length <= 3)
                {
                    return;
                }

                // * retrive the employees
                var empleados = fiscaliaService.SearchEmployees(serachEmployeeNumber.ToString()).ToList();
                dataGridEmpleados.DataSource = empleados;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBoxEmployeeNumberChanged(object sender, EventArgs e)
        {
            serachEmployeeNumber = Convert.ToInt32( txtEmployeeNumber.ValidateText() );
            ListarEmpleados();
        }
        
        private void TextBoxSearchTextChanged(object sender, EventArgs e)
        {
            ListarEmpleados();
        }
        
        private void DataGridSelectionChanged(object sender, EventArgs e)
        {
            if (dataGridEmpleados.CurrentRow.Index != -1)
            {
                foreach (DataGridViewRow row in dataGridEmpleados.SelectedRows)
                {
                    selectedEmployeeNumber = Convert.ToInt32(row.Cells[0].Value.ToString());
                    string nombre = row.Cells[1].Value.ToString();
                    string paterno = row.Cells[2].Value.ToString();
                    string materno = row.Cells[3].Value.ToString();

                    nombreEmpleado = paterno + ' ' + materno + ' ' + nombre;
                    this.btnEdit.Text = $"EDITAR: {nombreEmpleado}";
                    this.btnEdit.Enabled = true;
                    this.btnEdit.Visible = true;
                }
            }
            else
            {
                this.btnEdit.Text = "EDITAR EMPLEADO";
                this.btnEdit.Enabled = false;
                this.btnEdit.Visible = false;
            }

        }
           
        private void BtnEditarClick(object sender, EventArgs e)
        {
            var formRegistrar = new FrmRegistrar(this.selectedEmployeeNumber);
            formRegistrar.ShowDialog(this);

            txtEmployeeNumber.Focus();
            txtEmployeeNumber.Select();
        }
    }
}
