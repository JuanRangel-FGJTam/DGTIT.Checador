using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup.Localizer;
using System.Windows.Media.Animation;
using DGTIT.Checador.Services;

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
        private working_hours workingHours;
        
        public FrmRegistrar(int employeeNumber)
        {
            InitializeComponent();
            this.employeeNumber = employeeNumber;


            // * initialize the services
            contexto = new UsuariosDBEntities();
            procu = new procuraduriaEntities1();
            this.checadorService = new ChecadorService(contexto, procu);
            this.fiscaliaService = new FiscaliaService(procu, contexto);


            // * initialize the models
            this.currentEmployee = checadorService.GetEmployee(employeeNumber);
            this.workingHours = checadorService.GetWorkinHours(employeeNumber);
            
        }
        
        private void OnLoaded(object sender, EventArgs e)
        {
            LoadCatalogs();

            // * display the employee info
            this.tb_employeeNumber.Text = employeeNumber.ToString();
            this.txtNombre.Text = currentEmployee.name;
            this.cboDirGral.SelectedValue = currentEmployee.general_direction_id;
            this.cboDireccion.SelectedValue = currentEmployee.direction_id;
            this.cboSubdireccion.SelectedValue = currentEmployee.subdirectorate_id;
            this.cboDepartamento.SelectedValue = currentEmployee.department_id;
            this.cb_check.Checked = currentEmployee.status_id == 1;
            if ( this.workingHours != null)
            {
                this.txtEntrada.Text = workingHours.checkin;
                this.txtComida.Text = workingHours.toeat;
                this.txtRegreso.Text = workingHours.toarrive;
                this.txtSalida.Text = workingHours.checkout;
            }

            lblUpdatedAt.Text = this.currentEmployee.updated_at == null ? "" : this.currentEmployee.updated_at.Value.ToLongDateString();

            // * get the image of the fingerprint
            if (this.currentEmployee.fingerprint != null)
            {
                this.btnRegistrarHuella.Text = "Capturar Huella";
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
                this.btnRegistrarHuella.Text = "Capturar Huella";
            }


            // * set the employee foto
            this.img_photo.Image = this.fiscaliaService.GetEmployeePhoto(employeeNumber);


            // * attatch events to the inputs
            this.txtNombre.TextChanged += new EventHandler(TextBoxNameChanged);
            this.cboDirGral.SelectedValueChanged += new EventHandler(ComboBoxDirectionChanged);
            this.cboDireccion.SelectedValueChanged += new EventHandler(ComboBoxDirectionChanged);
            this.cboSubdireccion.SelectedValueChanged += new EventHandler(ComboBoxDirectionChanged);
            this.cboDepartamento.SelectedValueChanged += new EventHandler(ComboBoxDirectionChanged);

            this.txtEntrada.TextChanged += new EventHandler(TextBoxHoursChanged);
            this.txtComida.TextChanged += new EventHandler(TextBoxHoursChanged);
            this.txtRegreso.TextChanged += new EventHandler(TextBoxHoursChanged);
            this.txtSalida.TextChanged += new EventHandler(TextBoxHoursChanged);

            //this.cb_active.CheckedChanged += new EventHandler(CheckBoxOptionChanged);
            this.cb_check.CheckedChanged += new EventHandler(CheckBoxOptionChanged);
        }
        
        private void LoadCatalogs()
        {
            var dirgral = contexto.general_directions.OrderBy(item => item.name).ToList();
            cboDirGral.DataSource = dirgral;
            cboDirGral.ValueMember = "id";
            cboDirGral.DisplayMember = "name";

            var dir = contexto.directions.OrderBy(item => item.name).Where(item => item.general_direction_id == currentEmployee.general_direction_id || item.id == 1).ToList();
            cboDireccion.DataSource = dir;
            cboDireccion.ValueMember = "id";
            cboDireccion.DisplayMember = "name";

            var subdir = contexto.subdirectorates.OrderBy(item => item.name).Where(item => item.direction_id == currentEmployee.direction_id || item.id == 1).ToList();
            cboSubdireccion.DataSource = subdir;
            cboSubdireccion.ValueMember = "id";
            cboSubdireccion.DisplayMember = "name";

            var depa = contexto.departments.OrderBy(item => item.name).Where(item => item.subdirectorate_id == currentEmployee.subdirectorate_id|| item.id == 1).ToList();
            cboDepartamento.DataSource = depa;
            cboDepartamento.ValueMember = "id";
            cboDepartamento.DisplayMember = "name";
        }

        private bool ValidateBeforeUdpate()
        {
            if (string.IsNullOrEmpty(currentEmployee.name))
            {
                MessageBox.Show("El nombre del empleado es requerido.", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtNombre.Focus();
                return false;
            }

            if (currentEmployee.fingerprint == null)
            {
                MessageBox.Show("La huella digital is requerida.", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.btnRegistrarHuella.Focus();
                return false;
            }


            if ( string.IsNullOrEmpty(workingHours.checkin))
            {
                txtEntrada.Text = "";
            }

            if (string.IsNullOrEmpty(workingHours.toeat))
            {
                txtComida.Text = "";
            }

            if (string.IsNullOrEmpty(workingHours.toarrive))
            {
                txtRegreso.Text = "";
            }

            if (string.IsNullOrEmpty(workingHours.checkout))
            {
                txtSalida.Text = "";
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
            var dialogTask = Task.Run(() =>
            {
                Invoke(new Action(() => busyDialog.ShowDialog()));
            });

            // * update the employee in a task
            var updateEmployeeTask = Task.Run<int>(() =>
            {
                try
                {
                    checadorService.UpdateEmployee(currentEmployee);
                    checadorService.UpdateWorkingHours(workingHours);
                    Thread.Sleep(1000);
                    return 1;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return 0;
                }
            });

            // * wait for the task is done and 
            var resultsCode = await updateEmployeeTask;

            // * close the busy dialog
            Invoke(new Action(() => busyDialog.Close()));


            if( resultsCode == 1)
            {
                MessageBox.Show("Los datos del empleado se actualizaron exitosamente.", "Empleado actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
            else
            {
                MessageBox.Show("Ocurrió un problema al actualizar los datos del empleado. Por favor, inténtalo de nuevo.", "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TextBoxNameChanged(object sender, EventArgs e) {
            this.currentEmployee.name = ( (TextBox) sender).Text;
        }

        private void ComboBoxDirectionChanged(object sender, EventArgs e)
        {
            var senderComboBox = ((ComboBox) sender);
            var selectedId = Convert.ToInt32(senderComboBox.SelectedValue);
            switch (senderComboBox.Name)
            {
                case "cboDirGral":
                    currentEmployee.general_direction_id = selectedId;
                    cboDireccion.DataSource = contexto.directions
                        .OrderBy(item => item.name)
                        .Where(item => item.general_direction_id == selectedId || item.id == 1)
                        .ToList();
                    break;

                case "cboDireccion":
                    currentEmployee.direction_id = selectedId;
                    cboSubdireccion.DataSource = contexto.subdirectorates
                        .OrderBy(item => item.name)
                        .Where(item => item.direction_id == selectedId || item.id == 1)
                        .ToList();
                    break;

                case "cboSubdireccion":
                    currentEmployee.subdirectorate_id = selectedId;
                    cboDepartamento.DataSource = contexto.departments
                        .OrderBy(item => item.name)
                        .Where(item => item.subdirectorate_id == selectedId || item.id == 1)
                        .ToList();
                    break;

                case "cboDepartamento":
                    currentEmployee.department_id = selectedId;
                    break;
            }
        }

        private void TextBoxHoursChanged(object sender, EventArgs e)
        {
            var senderTextBox = ((MaskedTextBox)sender);
            DateTime? dateSchedule;

            switch (senderTextBox.Name)
            {
                case "txtEntrada":
                    dateSchedule = (DateTime?) txtEntrada.ValidateText();
                    workingHours.checkin = (dateSchedule != null) ?dateSchedule.Value.ToString("HH:mm") : null;
                    break;

                case "txtComida":
                    dateSchedule = (DateTime?) txtComida.ValidateText();
                    workingHours.toeat = (dateSchedule != null) ? dateSchedule.Value.ToString("HH:mm") : null;
                    break;

                case "txtRegreso":
                    dateSchedule = (DateTime?) txtRegreso.ValidateText();
                    workingHours.toarrive = (dateSchedule != null) ? dateSchedule.Value.ToString("HH:mm") : null;

                    break;

                case "txtSalida":
                    dateSchedule = (DateTime?) txtSalida.ValidateText();
                    workingHours.checkout = (dateSchedule != null) ? dateSchedule.Value.ToString("HH:mm") : null;
                    break;
            }

        }

        private void CheckBoxOptionChanged(object sender, EventArgs e)
        {
            var senderCheckbox = (CheckBox) sender;

            switch (senderCheckbox.Name)
            {
                case "cb_check":
                    currentEmployee.status_id = cb_check.Checked ? 1 : 0;
                    break;
            }

        }

        #region fingerprint events
        private void OnTemplate(DPFP.Template template)
        {
            Invoke( new Action (()=>{
                Template = template;
                if (Template != null) {
                    currentEmployee.fingerprint = template.Bytes;
                    tbFingerPrint.Text = Convert.ToBase64String(currentEmployee.fingerprint);
                    MessageBox.Show("La huella dactilar se ha capturado con éxito.", "Fingerprint Enrollment");
                }
                else {
                    MessageBox.Show("La captura de huella dactilar no es válida. Intente de nuevo.", "Fingerprint Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }));
        }
        #endregion
    }
}
