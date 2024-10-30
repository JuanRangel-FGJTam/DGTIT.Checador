using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using DGTIT.Checador.Properties;
using static DGTIT.Checador.User32;


namespace DGTIT.Checador
{
    /* NOTE: This form is a base for the EnrollmentForm and the VerificationForm,
		All changes in the CaptureForm will be reflected in all its derived forms.
	*/
    delegate void Function();

    public partial class CaptureForm : Form, DPFP.Capture.EventHandler
	{
        private DPFP.Capture.Capture Capturer;
        private bool _allowCapture = false;
        private readonly bool playSoundOnFail = false;
        public bool allowCapture
        {
            get => _allowCapture;
            set
            {
                if (picLock.IsHandleCreated) {
                    Invoke(new Action(() =>
                    {
                        picLock.Visible = !value;
                    }));
                }
                _allowCapture = value;
            }
        }
        

        public CaptureForm()
		{
			InitializeComponent();

            lblNombre.Text = "";
            lblMessage.Text = "";
            lblFecha.BorderStyle = BorderStyle.None;
            lblHora.BorderStyle = BorderStyle.None;
            lblMessage.BorderStyle = BorderStyle.None;
            lblNombre.BorderStyle = BorderStyle.None;
            this.FormBorderStyle = FormBorderStyle.None;
            this.btnClose.Enabled = false;

            this.playSoundOnFail = Properties.Settings.Default["playSoundOnFail"].ToString() == "1";

            this.lblStatus.ForeColor = Color.FromArgb(255,65, 94, 179);
        }

        protected virtual void Init()
		{
            try {
                Capturer = new DPFP.Capture.Capture();  // Create a capture operation.
                Capturer.EventHandler = this;  // Subscribe for capturing events.
            }
            catch(Exception err)
            {
                MakeReport(err.Message, EventLevel.Error);
                MessageBox.Show("No se pudo iniciar la operacion de captura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // * make rounded the employee photo and the fingerprint image
            using (var gp = new GraphicsPath()) {
                gp.AddEllipse(new Rectangle(0, 0, fotoEmpleado.Width - 1, fotoEmpleado.Height - 1 ));
                fotoEmpleado.Region = new Region(gp);
            }
            using (var gp = new GraphicsPath()) {
                gp.AddEllipse(new Rectangle(0, 0, fingerPrintImg.Width - 1, fingerPrintImg.Height - 1));
                fingerPrintImg.Region = new Region(gp);
            }
        }

		protected virtual void Process(DPFP.Sample Sample)
		{
			// Draw fingerprint sample image.
			DrawPicture( DGTIT.Checador.Helpers.FingerPrint.ConvertSampleToBitmap(Sample));
		}

		protected void StartCapturing()
		{
            this.allowCapture = true;
            
            if (Capturer == null){
                return;
            }
            try {
                Capturer.StartCapture();
                SetLoading(false);
            }
            catch (Exception err) {
                MakeReport("No se puede iniciar la captura.", EventLevel.Warning);
                SetNoRegistrada("No se puede iniciar la captura");
            }
            
		}

		protected void StopCapturing()
		{
            this.allowCapture = false;
            if (Capturer == null) {
                return;
            }
            try {
                Capturer.StopCapture();
            }
            catch {
                MakeReport("No se puede terminar la captura.", EventLevel.Warning);
            }
		}
        
        protected void MakeReport(string message, EventLevel eventLevel = EventLevel.Informational)
        {
            // TODO: make some log
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {eventLevel.ToString()}] " + message );
        }

        protected virtual void PlayBell()
        {
            try
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.check_sound);
                player.Play();
            }
            catch (Exception) { }
        }
        
        protected virtual void PlayFailSound() {
            try {
                if (playSoundOnFail) {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.fail_sound);
                    player.Play();
                }
            }
            catch (Exception) {}
        }

        #region Form Events Handler
        private void CaptureForm_Load(object sender, EventArgs e)
		{
            // Set the form to full screen
            //this.WindowState = FormWindowState.Maximized;
            //ChangeScreenResolution(new Size(1280, 720));

            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblHora.Text = DateTime.Now.ToString("HH:mm");

            Init();

            // * delay the start of capturing the fingerprints
            Task.Run(() =>
            {
                Thread.Sleep(2000);

                StartCapturing();

                if (btnClose.IsHandleCreated) {
                    Invoke(new Action(() => {
                        this.btnClose.Enabled = true;
                    }));
                }

            });

            btnClose.Click += new EventHandler((object s, EventArgs ee) =>
            {
                this.Close();
            });
        }

		protected virtual void CaptureFormClosing(object sender, FormClosingEventArgs e)
		{
            Task.Run(() => {
                Capturer.Dispose();
            });
		}
		#endregion

        #region Update UI methods
        
        private void DrawPicture(Bitmap bitmap)
		{
            if (fingerPrintImg.IsHandleCreated) {
                Invoke(new Action(() => {
                    fingerPrintImg.Image = new Bitmap(bitmap, fingerPrintImg.Size);
			    }));
            }
		}

		protected void SetNombre(string NombreEmpleado)
        {
            if (lblNombre.IsHandleCreated) {
                Invoke(new Action(() => {
				    this.lblNombre.ForeColor = Color.Black;
				    this.lblNombre.Text = NombreEmpleado;
                }));
            }
        }

		protected void SetNoRegistrada(string NombreEmpleado)
		{
            if (lblMessage.IsHandleCreated) {
                Invoke(new Action(() => {
				    this.lblMessage.ForeColor= Color.PaleVioletRed;
				    this.lblMessage.Text = NombreEmpleado;
                }));
            }
		}

		protected void SetFotoEmpleado(Bitmap bitmap)
		{
            if (fotoEmpleado.IsHandleCreated) {
                Invoke(new Action(() => {
				    fotoEmpleado.Image = new Bitmap(bitmap, fotoEmpleado.Size);   // fit the image into the picture box
                }));
			}
		}

		protected void SetChecada(DateTime time)
		{
            if (lblMessage.IsHandleCreated) {
                Invoke(new Action(() => {
                    lblMessage.ForeColor = Color.RoyalBlue;
                    lblMessage.Text = "Entrada registrada " + time.ToString("hh:mm:ss");
                }));
            }

            if (picOK.IsHandleCreated) {
                Invoke(new Action(() => {
                    picOK.Visible = true;
                }));
            }

            PlayBell();
        }

        protected void SetEmpledoBaja() {
            if (picUserFail.IsHandleCreated) {
                Invoke(new Action(() => {
                    picUserFail.Visible = true;
                }));
            }
            PlayFailSound();
        }

        protected void SetAreaNoEncontrada( )
		{
            if(picX.IsHandleCreated) {
                Invoke(new Action(() =>
                {
                    picX.Visible = true;
                }));
            }
            PlayFailSound();
        }

		protected void SetHuellaNoEncontrada()
		{
            if (lblMessage.IsHandleCreated) {
			    Invoke(new Action( () => {
                    lblMessage.ForeColor = Color.DarkSalmon;
                    lblMessage.Text = "No se reconoce la huella.";
			    }));
            }
            PlayFailSound();
        }

		protected void LimpiarCampos()
		{
            try
            {
                Invoke(new Action(() =>
                {
                    fotoEmpleado.Image = null;
                    lblNombre.Text = "";
                    lblNombre.ForeColor = Color.Black;
                    fingerPrintImg.Image = null;
                    lblMessage.Text = "";
                    picOK.Visible = false;
                    picX.Visible = false;
                    picUserFail.Visible = false;
                }));
            }
            catch (Exception) { }

		}
        
        protected void SetLoading(bool visible) {
            if (picLoading.IsHandleCreated) {
                Invoke(new Action(() => {
                    picLoading.Visible = visible;
                }));
            }
        }
        
        #endregion

        #region FingerPrint EventsHandler
        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            MakeReport("La muestra ha sido capturada");
            Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            //
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            // 
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("El Lector de huellas ha sido conectado");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("El Lector de huellas ha sido desconectado");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            // 
        }
        #endregion


        #region change resolution settings
        private void ChangeScreenResolution(Size size)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmDeviceName = new String(new char[32]);
            dm.dmFormName = new String(new char[32]);
            dm.dmSize = (short)Marshal.SizeOf(dm);

            if (0 != User32.EnumDisplaySettings(null, User32.ENUM_CURRENT_SETTINGS, ref dm) ) {
                dm.dmPelsWidth = size.Width;
                dm.dmPelsHeight = size.Height;
                int iRet = User32.ChangeDisplaySettings( ref dm, User32.CDS_UPDATEREGISTRY);
            }
        }
        #endregion

    }
}