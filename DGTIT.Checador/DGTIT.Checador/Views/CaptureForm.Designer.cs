namespace DGTIT.Checador
{
    partial class CaptureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptureForm));
            this.fingerPrintImg = new System.Windows.Forms.PictureBox();
            this.fotoEmpleado = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.picX = new System.Windows.Forms.PictureBox();
            this.picOK = new System.Windows.Forms.PictureBox();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.paneluser = new System.Windows.Forms.Panel();
            this.fingerPrint = new System.Windows.Forms.Panel();
            this.picLock = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fingerPrintImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoEmpleado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOK)).BeginInit();
            this.paneluser.SuspendLayout();
            this.fingerPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLock)).BeginInit();
            this.SuspendLayout();
            // 
            // fingerPrintImg
            // 
            this.fingerPrintImg.BackColor = System.Drawing.Color.Transparent;
            this.fingerPrintImg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fingerPrintImg.BackgroundImage")));
            this.fingerPrintImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.fingerPrintImg.Location = new System.Drawing.Point(505, 116);
            this.fingerPrintImg.Name = "fingerPrintImg";
            this.fingerPrintImg.Size = new System.Drawing.Size(184, 208);
            this.fingerPrintImg.TabIndex = 0;
            this.fingerPrintImg.TabStop = false;
            // 
            // fotoEmpleado
            // 
            this.fotoEmpleado.BackColor = System.Drawing.Color.White;
            this.fotoEmpleado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.fotoEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fotoEmpleado.Location = new System.Drawing.Point(75, 106);
            this.fotoEmpleado.Name = "fotoEmpleado";
            this.fotoEmpleado.Size = new System.Drawing.Size(365, 365);
            this.fotoEmpleado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fotoEmpleado.TabIndex = 2;
            this.fotoEmpleado.TabStop = false;
            // 
            // lblNombre
            // 
            this.lblNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNombre.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(42, 517);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(463, 67);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.Text = "Juan Salvador Rangel Almaguer";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessage.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblMessage.Location = new System.Drawing.Point(729, 515);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(517, 70);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.Text = " Hola mundo";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.Location = new System.Drawing.Point(385, 674);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(530, 30);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Direcci�n General de Recursos Humanos ";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // picX
            // 
            this.picX.Image = global::DGTIT.Checador.Properties.Resources.fail;
            this.picX.Location = new System.Drawing.Point(0, 0);
            this.picX.Name = "picX";
            this.picX.Size = new System.Drawing.Size(81, 80);
            this.picX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picX.TabIndex = 10;
            this.picX.TabStop = false;
            this.picX.Visible = false;
            // 
            // picOK
            // 
            this.picOK.Image = global::DGTIT.Checador.Properties.Resources.check;
            this.picOK.Location = new System.Drawing.Point(0, 0);
            this.picOK.Name = "picOK";
            this.picOK.Size = new System.Drawing.Size(81, 80);
            this.picOK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOK.TabIndex = 8;
            this.picOK.TabStop = false;
            this.picOK.Visible = false;
            // 
            // lblHora
            // 
            this.lblHora.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHora.BackColor = System.Drawing.Color.Transparent;
            this.lblHora.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHora.Font = new System.Drawing.Font("Century Gothic", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblHora.Location = new System.Drawing.Point(730, 313);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(517, 163);
            this.lblHora.TabIndex = 6;
            this.lblHora.Text = "12:00";
            this.lblHora.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFecha
            // 
            this.lblFecha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFecha.BackColor = System.Drawing.Color.Transparent;
            this.lblFecha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFecha.Font = new System.Drawing.Font("Century Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblFecha.Location = new System.Drawing.Point(730, 118);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(517, 157);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "2024-01-01";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::DGTIT.Checador.Properties.Resources.cross;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(1222, 30);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 7;
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // paneluser
            // 
            this.paneluser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("paneluser.BackgroundImage")));
            this.paneluser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.paneluser.Controls.Add(this.picOK);
            this.paneluser.Controls.Add(this.picX);
            this.paneluser.Location = new System.Drawing.Point(566, 510);
            this.paneluser.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.paneluser.Name = "paneluser";
            this.paneluser.Size = new System.Drawing.Size(80, 80);
            this.paneluser.TabIndex = 11;
            // 
            // fingerPrint
            // 
            this.fingerPrint.BackColor = System.Drawing.Color.Transparent;
            this.fingerPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fingerPrint.Controls.Add(this.picLock);
            this.fingerPrint.Location = new System.Drawing.Point(566, 352);
            this.fingerPrint.Margin = new System.Windows.Forms.Padding(0);
            this.fingerPrint.Name = "fingerPrint";
            this.fingerPrint.Size = new System.Drawing.Size(80, 80);
            this.fingerPrint.TabIndex = 14;
            // 
            // picLock
            // 
            this.picLock.Image = global::DGTIT.Checador.Properties.Resources.fingerprint_lock;
            this.picLock.InitialImage = null;
            this.picLock.Location = new System.Drawing.Point(0, 0);
            this.picLock.Name = "picLock";
            this.picLock.Size = new System.Drawing.Size(80, 80);
            this.picLock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLock.TabIndex = 0;
            this.picLock.TabStop = false;
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.DimGray;
            this.lblVersion.Location = new System.Drawing.Point(968, 674);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.lblVersion.Size = new System.Drawing.Size(297, 30);
            this.lblVersion.TabIndex = 13;
            this.lblVersion.Text = "Creado por DGTIT v3";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // CaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DGTIT.Checador.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.fingerPrint);
            this.Controls.Add(this.paneluser);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.fotoEmpleado);
            this.Controls.Add(this.fingerPrintImg);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "CaptureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CaptureFormClosing);
            this.Load += new System.EventHandler(this.CaptureForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fingerPrintImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoEmpleado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOK)).EndInit();
            this.paneluser.ResumeLayout(false);
            this.fingerPrint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.PictureBox fotoEmpleado;
        public System.Windows.Forms.Label lblNombre;
        public System.Windows.Forms.PictureBox fingerPrintImg;
        public System.Windows.Forms.Label lblFecha;
        public System.Windows.Forms.Label lblHora;
        public System.Windows.Forms.Label lblMessage;
        public System.Windows.Forms.PictureBox picOK;
        private System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.PictureBox picX;
        private System.Windows.Forms.Panel paneluser;
        private System.Windows.Forms.Panel fingerPrint;
        private System.Windows.Forms.PictureBox picLock;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblVersion;
    }
}