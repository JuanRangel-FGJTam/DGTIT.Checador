using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Core.Interfaces;
using DGTIT.Checador.Data.Repositories;
using DGTIT.Checador.Helpers;
using DGTIT.Checador.Services;
using DGTIT.Checador.ViewModels;

namespace DGTIT.Checador
{
    public partial class FrmEmpleados : Form
    {
        private readonly FiscaliaService fiscaliaService;

        private IEmployeeRepository employeeRepository;
        private IProcuEmployeeRepo procuEmployeeRepo;
        private EmployeeService employeeService;
        private int serachEmployeeNumber = 0;
        private int selectedEmployeeNumber = 0;
        private string nombreEmpleado = "";

        public FrmEmpleados()
        {
            InitializeComponent();

            // * initialized the dbContext 
            employeeRepository = new SQLClientEmployeeRepository();
            procuEmployeeRepo = new ProcuEmployeeRepository();

            // * initialized services
            fiscaliaService = new FiscaliaService(employeeRepository, procuEmployeeRepo);
            employeeService = new EmployeeService(employeeRepository, procuEmployeeRepo);

            // * hidden search box
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

        private async Task ListarEmpleados()
        {
            // * clear selection 
            DisabledEditButton();
            this.selectedEmployeeNumber = 0;
            this.nombreEmpleado = "";

            try
            {
                dataGridEmpleados.DataSource = null;

                // * ensured that at least one filter is specified to prevent display all the employees
                if (serachEmployeeNumber.ToString().Length <= 3) return;

                // * retrive the employees
                var empleados = await fiscaliaService.SearchEmployees(serachEmployeeNumber.ToString());
                var empleadosVM = empleados.Select(e => EmployeeViewModel.FromEntity(e)).ToList();
                dataGridEmpleados.DataSource = empleadosVM;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void TextBoxEmployeeNumberChanged(object sender, EventArgs e)
        {
            serachEmployeeNumber = Convert.ToInt32( txtEmployeeNumber.ValidateText() );
            await ListarEmpleados();
        }
        
        private async void TextBoxSearchTextChanged(object sender, EventArgs e)
        {
            await ListarEmpleados();
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
                    EnabledEditButton();
                }
            }
            else
            {
                DisabledEditButton();
            }
        }
           
        private void BtnEditarClick(object sender, EventArgs e)
        {
            // * load the employee
            Employee currentEmployee = null;
            var taskLoadEmployee = Task.Run(async () => {
                try
                {
                    currentEmployee = await employeeService.GetEmployee(this.selectedEmployeeNumber);
                }
                catch(Exception) { }
            });
            Task.WaitAll(taskLoadEmployee);

            if (currentEmployee == null)
            {
                this.Activate();
                MessageBox.Show(this, "El empleado no pertenece a este departamento.", "Empleado no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // * show the form
            var formRegistrar = new FrmRegistrar(currentEmployee);
            formRegistrar.ShowDialog(this);

            txtEmployeeNumber.Focus();
            txtEmployeeNumber.Select();
        }

        private void DataGridEmpleadosDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // * attempt to get the employee data from the table row
            DataGridViewRow row;
            EmployeeViewModel data;
            try
            {
                row = dataGridEmpleados.Rows[e.RowIndex];
                data = (EmployeeViewModel) row.DataBoundItem;
            }
            catch (Exception) { }

            // * load the employee
            Employee currentEmployee = null;
            var taskLoadEmployee = Task.Run(async () =>
            {
                try
                {
                    currentEmployee = await employeeService.GetEmployee(this.selectedEmployeeNumber);
                }
                catch (Exception) { }
            });
            Task.WaitAll(taskLoadEmployee);

            if (currentEmployee == null)
            {
                this.Activate();
                MessageBox.Show(this, "El empleado no pertenece a este departamento.", "Empleado no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // * show the form
            var formRegistrar = new FrmRegistrar(currentEmployee);
            formRegistrar.ShowDialog(this);

            txtEmployeeNumber.Focus();
            txtEmployeeNumber.Select();
        }
    
        private void DisabledEditButton()
        {
            this.btnEdit.Text = "EDITAR EMPLEADO";
            this.btnEdit.Enabled = false;
            this.btnEdit.Visible = false;
        }

        private void EnabledEditButton()
        {
            this.btnEdit.Text = $"EDITAR: {nombreEmpleado}";
            this.btnEdit.Enabled = true;
            this.btnEdit.Visible = true;
        }

    }
}
