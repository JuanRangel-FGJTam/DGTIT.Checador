
namespace DGTIT.Checador
{
    partial class Checador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.fotoEmpleado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChecada)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOK)).BeginInit(); 
            this.SuspendLayout();
            // 
            // fotoEmpleado
            // 
            this.fotoEmpleado.Location = new System.Drawing.Point(648, 204);
            this.fotoEmpleado.Size = new System.Drawing.Size(471, 495);
            // 
            // lblNombre
            // 
            this.lblNombre.Location = new System.Drawing.Point(1131, 494);
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Picture
            // 
            this.Picture.Location = new System.Drawing.Point(1136, 211);
            this.Picture.Size = new System.Drawing.Size(178, 238);
            // 
            // lblFecha
            // 
            this.lblFecha.Location = new System.Drawing.Point(158, 221);
            // 
            // lblChecadaFecha
            // 
           // this.lblChecadaFecha.Location = new System.Drawing.Point(1212, 600);
            this.lblChecadaFecha.Location = new System.Drawing.Point(1150, 600);
            // 
            // picChecada
            // 
            this.picChecada.Location = new System.Drawing.Point(1125, 612);
            // 
            // lblHora
            // 
           // this.lblHora.Location = new System.Drawing.Point(164, 284);
            this.lblHora.Location = new System.Drawing.Point(145, 284);
            // 
            // lblChecadaHora
            // 
           // this.lblChecadaHora.Location = new System.Drawing.Point(1212, 649);
            this.lblChecadaHora.Location = new System.Drawing.Point(1150, 649);
            // 
            // picOK
            // 
            this.picOK.Location = new System.Drawing.Point(1466, 440);
            this.picOK.Size = new System.Drawing.Size(84, 54); 
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Checador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.ControlBox = false;
            this.Name = "Checador";
            this.ShowIcon = false;
            this.Text = "Sistema de Registro de Asistencia";
            ((System.ComponentModel.ISupportInitialize)(this.fotoEmpleado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChecada)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picOK)).EndInit(); 
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
    }
}