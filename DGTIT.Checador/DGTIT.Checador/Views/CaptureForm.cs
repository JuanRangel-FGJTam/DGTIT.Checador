using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
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
        
        public CaptureForm()
		{
			InitializeComponent();

            lblFecha.BorderStyle = BorderStyle.None;
            lblHora.BorderStyle = BorderStyle.None;
            lblChecadaHora.BorderStyle = BorderStyle.None;
            lblChecadaFecha.BorderStyle = BorderStyle.None;
            lblNombre.BorderStyle = BorderStyle.None;
        }

        protected virtual void Init()
		{
            try
            {
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

                if ( null != Capturer )
                    Capturer.EventHandler = this;					// Subscribe for capturing events.
                else
                    SetPrompt("No se pudo iniciar la operación de captura");
            }
            catch
            {               
                MessageBox.Show("No se pudo iniciar la operación de captura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }
		}

		protected virtual void Process(DPFP.Sample Sample)
		{
			// Draw fingerprint sample image.
			DrawPicture( DGTIT.Checador.Helpers.FingerPrint.ConvertSampleToBitmap(Sample));
		}

		protected void StartCapturing()
		{
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    SetPrompt("Escanea tu huella usando el lector");
                }
                catch
                {
                    SetPrompt("No se puede iniciar la captura");
                }
            }
		}

		protected void StopCapturing()
		{
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    SetPrompt("No se puede terminar la captura");
                }
            }
		}

        protected void SetPrompt(string prompt)
        {
            this.Invoke(new Function(delegate () {
                // MessageBox.Show(prompt, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }));
        }
        
        protected void MakeReport(string message)
        {
            this.Invoke(new Function(delegate () {
                //StatusText.AppendText(message + "\r\n");
                //lblNombre.Text = message + "\r\n";
            }));
        }

        protected virtual void PlayBell()
        {
            try
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\Timbre.wav");
                player.Play();
            }
            catch (Exception)
            {
            }
        }

        #region Form Events Handler
        private void CaptureForm_Load(object sender, EventArgs e)
		{
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblHora.Text = DateTime.Now.ToString("hh:mm tt");

            Init();
			StartCapturing();


            // Set the form to full screen
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            ChangeScreenResolution(new Size(1600, 900));
            //ChangeScreenResolution(new Size(1280, 1024));
        }

		private void CaptureForm_FormClosed(object sender, FormClosedEventArgs e)
		{
            StopCapturing();
		}
		#endregion

        #region Update UI methods
        
        private void DrawPicture(Bitmap bitmap)
		{
			this.Invoke(new Function(delegate() {
				fingerPrintImg.Image = new Bitmap(bitmap, fingerPrintImg.Size);	// fit the image into the picture box
			}));
		}

		protected void SetNombre(string NombreEmpleado)
        {
			this.Invoke(new Function(delegate () {
				this.lblNombre.ForeColor = Color.Black;
				this.lblNombre.Text = NombreEmpleado;
			}));
        }

		protected void SetNoRegistrada(string NombreEmpleado)
		{
			this.Invoke(new Function(delegate () {
				this.lblNombre.ForeColor = Color.PaleVioletRed;
				this.lblNombre.Text = NombreEmpleado;
			}));
		}

		protected void SetFotoEmpleado(Bitmap bitmap)
		{
			this.Invoke(new Function(delegate () {
				fotoEmpleado.Image = new Bitmap(bitmap, fotoEmpleado.Size);   // fit the image into the picture box
			}));
		}

		protected void SetChecada(DateTime time)
		{
			this.Invoke(new Function(delegate () {


				lblChecadaFecha.Text = time.ToString("dd/MM/yyyy");
				lblChecadaHora.Text = time.ToString("hh:mm:ss");
				//picChecada.Visible = true; se comento 
				picOK.Visible = true;

			}));
		}
		
		protected void SetAreaNoEncontrada( )
		{
			this.Invoke(new Function(delegate () { 
				picX.Visible = true;
			}));
		}

		protected void SetHuellaNoEncontrada()
		{
			this.Invoke(new Function(delegate () {
				this.lblNombre.ForeColor = Color.DarkSalmon;
				this.lblNombre.Text = "No se reconoce la huella, favor de reintentar";
			}));
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
                    lblChecadaFecha.Text = "";
                    lblChecadaHora.Text = "";
                    picChecada.Visible = false;
                    picOK.Visible = false;
                    picX.Visible = false;
                }));
            }
            catch (Exception) { }

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
            SetPrompt("Escanea tu misma huella otra vez");
            Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MakeReport("La huella fue removida del lector");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("El lector fue tocado");
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
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MakeReport("La calidad de la muestra es BUENA");
            else
                MakeReport("La calidad de la muestra es MALA");
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