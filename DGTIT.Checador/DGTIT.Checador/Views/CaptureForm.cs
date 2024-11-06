using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private EventLog eventLog;
        private CancellationTokenSource cancellationTokenSource;
        public EventLog CurrentEventLog { get => this.eventLog; }

        public bool AllowCapture
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

            // * prepare the event log
            this.eventLog = new EventLog();
            this.eventLog.Source = "Checador3";    

            // * prepare the UI elements
            this.lblNombre.Text = "";
            this.lblMessage.Text = "";
            this.lblFecha.BorderStyle = BorderStyle.None;
            this.lblHora.BorderStyle = BorderStyle.None;
            this.lblMessage.BorderStyle = BorderStyle.None;
            this.lblNombre.BorderStyle = BorderStyle.None;
            this.btnClose.Enabled = true;
            this.lblStatus.ForeColor = Color.FromArgb(255,65, 94, 179);

            // * load settings
            this.playSoundOnFail = Properties.Settings.Default["playSoundOnFail"].ToString() == "1";
            
            // * attach event handlers
            btnClose.Click += new EventHandler((object s, EventArgs ee) => {
                this.Close();
            });

            // * initialize the  capturer object
            if (Capturer == null) {
                Capturer = new DPFP.Capture.Capture();
            }

        }

        private void CaptureForm_Load(object sender, EventArgs e) {
            ToogleFullScreen(true);
            LimpiarCampos();
            lblFecha.Text = "Cargando...";
            Init();

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            // * delay the start of capturing the fingerprints
            Task.Run(() => {
                try {
                    // * what 2 secconds or until cancellation
                    if(!token.WaitHandle.WaitOne(TimeSpan.FromSeconds(2))) {
                        Capturer.StartCapture();
                        SetLoading(false);
                        // * enable exit button
                        if (btnClose.IsHandleCreated) {
                            Invoke(new Action(() => {
                                this.btnClose.Enabled = true;
                            }));
                        }
                    }
                }
                catch(OperationCanceledException){
                    MakeReport("Starting capture process was stop by cancellation token");
                }
            }, token);
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {

            Console.WriteLine("Capture form closing");

            if (cancellationTokenSource != null) {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }

            Capturer.Dispose();
            Capturer = null;

            // Call the base method to proceed with closing
            base.OnFormClosing(e);
        }

        private void ToogleFullScreen(bool fullScreen) {
            if (fullScreen) {
                // Set the form to full screen
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
                ChangeScreenResolution(new Size(1280, 720));
            }
            else {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
        }

        #region Update UI methods
        protected virtual void Init()
		{
            // Create a capture operation
            if (Capturer == null) {
                Capturer = new DPFP.Capture.Capture();  
            }

            // Subscribe for capturing events
            Capturer.EventHandler = this;  
            
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
            AllowCapture = true;
            SetLoading(false);
            MakeReport("Iniciando captura de huella", EventLevel.Informational);
        }

		protected void StopCapturing()
		{
            this.AllowCapture = false;
            MakeReport("Deteniendo captura de huella", EventLevel.Informational );
        }
        
        protected void MakeReport(string message, EventLevel eventLevel = EventLevel.Informational)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {eventLevel}] " + message );

            var eventType = EventLogEntryType.Information;
            switch (eventLevel) {
                case EventLevel.Warning:
                    eventType = EventLogEntryType.Warning;
                    break;

                case EventLevel.Error:
                    eventType = EventLogEntryType.Error;
                    break;
            }

            try {
                eventLog.WriteEntry(message, eventType);
            }
            catch { }
        }

        protected void MakeReport(string message, Exception err) {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {EventLevel.Error}] " + message );
            try {
                eventLog.WriteEntry( $"{message}: {err.Message} [{err.StackTrace}]", EventLogEntryType.Error);
            }
            catch { }
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
                    picLostConnection.Visible = false;
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
        
        protected void SetDateTimeServer(DateTime? serverDate) {
            // * update the date and hour on the UI
            if (lblFecha.IsHandleCreated) {
                Invoke(new Action(() => { this.lblFecha.Text = serverDate.Value.ToString("dd/MM/yyyy"); }));
            }
            if (lblHora.IsHandleCreated) {
                Invoke(new Action(() => { this.lblHora.Text = serverDate.Value.ToString("hh:mm:ss"); }));
            }
        }

        protected void SetLostConnection() {
            if (picLostConnection.IsHandleCreated) {
                Invoke(new Action(() => {
                    this.picLostConnection.Visible = true;
                }));
            }
            
            if (lblMessage.IsHandleCreated) {
                Invoke(new Action(() => {
                    this.lblMessage.ForeColor = Color.PaleVioletRed;
                    this.lblMessage.Text = "Sin conexion";
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
            if(AllowCapture) {
                MakeReport("La muestra ha sido capturada");
                Process(Sample);
            }
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
            AllowCapture = true;
            try {
                MakeReport("El Lector de huellas ha sido conectado");
            }
            catch { }
            SetNoRegistrada("");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            AllowCapture = false;
            try {
                MakeReport("El Lector de huellas ha sido desconectado");
            }
            catch{ }
            SetNoRegistrada("Dispositivo desconectado");
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