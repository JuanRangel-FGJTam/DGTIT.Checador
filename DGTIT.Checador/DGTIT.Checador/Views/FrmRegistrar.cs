using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Core.Interfaces;
using DGTIT.Checador.Data.Repositories;
using DGTIT.Checador.Helpers;
using DGTIT.Checador.Services;

namespace DGTIT.Checador
{
    public partial class FrmRegistrar : Form
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IProcuEmployeeRepo procuEmployeeRepo;
        private readonly ICatalogsRepository catalogsRepository;
        private readonly EmployeeService employeeService;
        private readonly FiscaliaService fiscaliaService;

        private DPFP.Template Template;
        private readonly Employee currentEmployee;
        
        public FrmRegistrar(Employee employee)
        {
            InitializeComponent();
            
            // * initialize the services
            this.employeeRepository = new SQLClientEmployeeRepository();
            this.procuEmployeeRepo = new ProcuEmployeeRepository();
            this.catalogsRepository = new CatalogsRepository();

            this.employeeService = new EmployeeService();
            this.fiscaliaService = new FiscaliaService(employeeRepository, procuEmployeeRepo);

            // * initialize the models
            this.currentEmployee = employee;
            //if( currentEmployee.Fingerprint != null && currentEmployee.Fingerprint.Any()) {
            //    DisableEdition();
            //}
            DisableEdition();
        }
        
        private async void OnLoaded(object sender, EventArgs e)
        {
            await LoadCatalogs();

            // * display the employee info
            this.tb_employeeNumber.Text = this.currentEmployee.EmployeeNumber.ToString();
            this.txtNombre.Text = currentEmployee.Name;
            this.cboDirGral.SelectedValue = currentEmployee.GeneralDirectionId;
            this.cboDireccion.SelectedValue = currentEmployee.DirectionId;
            this.cboSubdireccion.SelectedValue = currentEmployee.SubdirectorateId;
            this.cboDepartamento.SelectedValue = currentEmployee.DepartmentId;
            this.cb_check.Checked = currentEmployee.StatusId == 1;
            //if ( this.workingHours != null)
            //{
            //    this.txtEntrada.Text = workingHours.checkin;
            //    this.txtComida.Text = workingHours.toeat;
            //    this.txtRegreso.Text = workingHours.toarrive;
            //    this.txtSalida.Text = workingHours.checkout;
            //}
            lblUpdatedAt.Text = this.currentEmployee.UpdatedAt != null ? this.currentEmployee.UpdatedAt.ToLongDateString() : "";

            // * get the image of the fingerprint
            if (this.currentEmployee.Fingerprint != null)
            {
                this.btnRegistrarHuella.Text = "Capturar Huella";
                //Bitmap bmp = null;
                //using (var ms = new MemoryStream(this.currentEmployee.Fingerprint))
                //{
                //    var sample = new DPFP.Sample(ms);
                //    bmp = FingerPrint.ConvertSampleToBitmap(sample);
                //}
                //this.fingerPrintPictureBox.Image = bmp;
                var x = Convert.ToBase64String(this.currentEmployee.Fingerprint);
                this.tbFingerPrint.Text = x;
            }
            else
            {
                this.btnRegistrarHuella.Text = "Capturar Huella";
            }


            // * set the employee foto
            var taskEmployeeFoto = Task.Run(async () => {
                this.img_photo.Image = await this.fiscaliaService.GetEmployeePhoto(this.currentEmployee.EmployeeNumber);
            });
            Task.WaitAll(taskEmployeeFoto);


            // * attatch events to the inputs
            this.txtNombre.TextChanged += new EventHandler(TextBoxNameChanged);
            this.cboDirGral.SelectedValueChanged += new EventHandler(ComboBoxDirectionChanged);
            this.cboDireccion.SelectedValueChanged += new EventHandler(ComboBoxDirectionChanged);
            this.cboSubdireccion.SelectedValueChanged += new EventHandler(ComboBoxDirectionChanged);
            this.cboDepartamento.SelectedValueChanged += new EventHandler(ComboBoxDirectionChanged);

            //this.cb_active.CheckedChanged += new EventHandler(CheckBoxOptionChanged);
            this.cb_check.CheckedChanged += new EventHandler(CheckBoxOptionChanged);
        }
        
        private async Task LoadCatalogs()
        {
            var dirgral = await this.catalogsRepository.FindAllGeneralDirections();
            cboDirGral.DataSource = dirgral.ToList();
            cboDirGral.ValueMember = "Id";
            cboDirGral.DisplayMember = "Name";

            var dir = await this.catalogsRepository.FindAllDirections();
            cboDireccion.DataSource = dir.ToList();
            cboDireccion.ValueMember = "Id";
            cboDireccion.DisplayMember = "Name";

            var subdir = await this.catalogsRepository.FindAllSubdirectorates();
            cboSubdireccion.DataSource = subdir.ToList();
            cboSubdireccion.ValueMember = "Id";
            cboSubdireccion.DisplayMember = "Name";

            var depa = await this.catalogsRepository.FindAllDepartment();
            cboDepartamento.DataSource = depa.ToList();
            cboDepartamento.ValueMember = "Id";
            cboDepartamento.DisplayMember = "Name";
        }

        private bool ValidateBeforeUdpate()
        {
            if (string.IsNullOrEmpty(currentEmployee.Name))
            {
                MessageBox.Show("El nombre del empleado es requerido.", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtNombre.Focus();
                return false;
            }

            if (currentEmployee.Fingerprint == null)
            {
                MessageBox.Show("La huella digital is requerida.", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.btnRegistrarHuella.Focus();
                return false;
            }
            return true;
        }

        private void BtnRegistrarHuellaClick(object sender, EventArgs e)
        {
            var capturar = new CapturarHuella();
            capturar.TopMost = true;
            capturar.OnTemplate += this.OnTemplate;
            capturar.ShowDialog(this);
        }
        
        private async void BtnUpdateClick(object sender, EventArgs e)
        {
            if (!ValidateBeforeUdpate())
            {
                return;
            }

            // * display a busy dialog
            var busyDialog = new BusyDialog();
            var dialogTask = Task.Run(() => Invoke(new Action(() => busyDialog.ShowDialog())));

            // * update the employee in a task
            try
            {
                await employeeService.UpdateEmployee(currentEmployee);
                Thread.Sleep(1000);
                MessageBox.Show("Los datos del empleado se actualizaron exitosamente.", "Empleado actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception)
            {
                MessageBox.Show("Ocurrió un problema al actualizar los datos del empleado. Por favor, inténtalo de nuevo.", "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // * close the busy dialog
            Invoke(new Action(() => busyDialog.Close()));
        }

        private void TextBoxNameChanged(object sender, EventArgs e) {
            this.currentEmployee.Name = ( (TextBox) sender).Text;
        }

        private async void ComboBoxDirectionChanged(object sender, EventArgs e)
        {
            var senderComboBox = ((ComboBox) sender);
            var selectedId = Convert.ToInt32(senderComboBox.SelectedValue);
            switch (senderComboBox.Name)
            {
                case "cboDirGral":
                    currentEmployee.GeneralDirectionId = selectedId;
                    cboDireccion.DataSource = (await this.catalogsRepository.FindAllDirections())
                        .OrderBy(item => item.Name)
                        .Where(item => item.GeneralDirectionId == selectedId || item.Id == 1)
                        .ToList();
                    break;

                case "cboDireccion":
                    currentEmployee.DirectionId = selectedId;
                    cboSubdireccion.DataSource = (await this.catalogsRepository.FindAllSubdirectorates())
                        .OrderBy(item => item.Name)
                        .Where(item => item.DirectionId == selectedId || item.Id == 1)
                        .ToList();
                    break;

                case "cboSubdireccion":
                    currentEmployee.SubdirectorateId = selectedId;
                    cboDepartamento.DataSource = (await this.catalogsRepository.FindAllDepartment())
                        .OrderBy(item => item.Name)
                        .Where(item => item.SubDirectorateId == selectedId || item.Id == 1)
                        .ToList();
                    break;

                case "cboDepartamento":
                    currentEmployee.DepartmentId = selectedId;
                    break;
            }
        }

        private void CheckBoxOptionChanged(object sender, EventArgs e)
        {
            var senderCheckbox = (CheckBox) sender;

            switch (senderCheckbox.Name)
            {
                case "cb_check":
                    currentEmployee.StatusId = cb_check.Checked ? 1 : 0;
                    break;
            }
        }

        #region fingerprint events
        private void OnTemplate(DPFP.Template template)
        {
            Invoke( new Action (()=>{
                Template = template;
                if (Template != null) {
                    currentEmployee.Fingerprint = template.Bytes;
                    tbFingerPrint.Text = Convert.ToBase64String(currentEmployee.Fingerprint);
                    MessageBox.Show("La huella dactilar se ha capturado con éxito.", "Fingerprint Enrollment");
                }
                else {
                    MessageBox.Show("La captura de huella dactilar no es válida. Intente de nuevo.", "Fingerprint Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }));
        }
        #endregion
    
        
        private void DisableEdition()
        {
            this.txtNombre.Enabled = false;
            this.txtEntrada.Enabled = false;
            this.txtComida.Enabled = false;
            this.txtRegreso.Enabled = false;
            this.txtSalida.Enabled = false;
            this.cb_check.Enabled = false;
            this.cboDirGral.Enabled = false;
            this.cboDireccion.Enabled = false;
            this.cboSubdireccion.Enabled = false;
            this.cboDepartamento.Enabled = false;
        }
    }
}
