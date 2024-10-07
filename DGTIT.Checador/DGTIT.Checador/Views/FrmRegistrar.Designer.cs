namespace DGTIT.Checador
{
    partial class FrmRegistrar
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnRegistrarHuella = new System.Windows.Forms.Button();
            this.cboDirGral = new System.Windows.Forms.ComboBox();
            this.groupBox_generales = new System.Windows.Forms.GroupBox();
            this.tb_employeeNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSubdireccion = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboDepartamento = new System.Windows.Forms.ComboBox();
            this.cboDireccion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox_horarios = new System.Windows.Forms.GroupBox();
            this.txtComida = new System.Windows.Forms.MaskedTextBox();
            this.txtSalida = new System.Windows.Forms.MaskedTextBox();
            this.txtRegreso = new System.Windows.Forms.MaskedTextBox();
            this.txtEntrada = new System.Windows.Forms.MaskedTextBox();
            this.chkComida = new System.Windows.Forms.CheckBox();
            this.chkRegreso = new System.Windows.Forms.CheckBox();
            this.chkSalida = new System.Windows.Forms.CheckBox();
            this.chkEntrada = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tbFingerPrint = new System.Windows.Forms.TextBox();
            this.img_fingerPrint = new System.Windows.Forms.Label();
            this.img_photo = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.groupBox_generales.SuspendLayout();
            this.groupBox_horarios.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAgregar.Enabled = false;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAgregar.Location = new System.Drawing.Point(277, 3);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(99, 35);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "Actualizar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.BtnUpdateClick);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(97, 55);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(374, 20);
            this.txtNombre.TabIndex = 3;
            // 
            // btnRegistrarHuella
            // 
            this.btnRegistrarHuella.Location = new System.Drawing.Point(32, 3);
            this.btnRegistrarHuella.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.btnRegistrarHuella.Name = "btnRegistrarHuella";
            this.btnRegistrarHuella.Size = new System.Drawing.Size(114, 23);
            this.btnRegistrarHuella.TabIndex = 5;
            this.btnRegistrarHuella.Text = "Capturar Huella";
            this.btnRegistrarHuella.UseVisualStyleBackColor = true;
            this.btnRegistrarHuella.Click += new System.EventHandler(this.btnRegistrarHuella_Click);
            // 
            // cboDirGral
            // 
            this.cboDirGral.FormattingEnabled = true;
            this.cboDirGral.Location = new System.Drawing.Point(97, 81);
            this.cboDirGral.Name = "cboDirGral";
            this.cboDirGral.Size = new System.Drawing.Size(374, 21);
            this.cboDirGral.TabIndex = 7;
            // 
            // groupBox_generales
            // 
            this.groupBox_generales.Controls.Add(this.tb_employeeNumber);
            this.groupBox_generales.Controls.Add(this.label2);
            this.groupBox_generales.Controls.Add(this.cboSubdireccion);
            this.groupBox_generales.Controls.Add(this.label7);
            this.groupBox_generales.Controls.Add(this.cboDepartamento);
            this.groupBox_generales.Controls.Add(this.cboDireccion);
            this.groupBox_generales.Controls.Add(this.label6);
            this.groupBox_generales.Controls.Add(this.label5);
            this.groupBox_generales.Controls.Add(this.label3);
            this.groupBox_generales.Controls.Add(this.txtNombre);
            this.groupBox_generales.Controls.Add(this.cboDirGral);
            this.groupBox_generales.Controls.Add(this.label1);
            this.groupBox_generales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_generales.Location = new System.Drawing.Point(183, 3);
            this.groupBox_generales.Name = "groupBox_generales";
            this.groupBox_generales.Size = new System.Drawing.Size(495, 215);
            this.groupBox_generales.TabIndex = 8;
            this.groupBox_generales.TabStop = false;
            this.groupBox_generales.Text = "Datos Generales";
            // 
            // tb_employeeNumber
            // 
            this.tb_employeeNumber.Location = new System.Drawing.Point(97, 29);
            this.tb_employeeNumber.Name = "tb_employeeNumber";
            this.tb_employeeNumber.ReadOnly = true;
            this.tb_employeeNumber.Size = new System.Drawing.Size(174, 20);
            this.tb_employeeNumber.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "N Empleado";
            // 
            // cboSubdireccion
            // 
            this.cboSubdireccion.FormattingEnabled = true;
            this.cboSubdireccion.Location = new System.Drawing.Point(97, 135);
            this.cboSubdireccion.Name = "cboSubdireccion";
            this.cboSubdireccion.Size = new System.Drawing.Size(374, 21);
            this.cboSubdireccion.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Subdirección:";
            // 
            // cboDepartamento
            // 
            this.cboDepartamento.FormattingEnabled = true;
            this.cboDepartamento.Location = new System.Drawing.Point(97, 162);
            this.cboDepartamento.Name = "cboDepartamento";
            this.cboDepartamento.Size = new System.Drawing.Size(374, 21);
            this.cboDepartamento.TabIndex = 12;
            // 
            // cboDireccion
            // 
            this.cboDireccion.FormattingEnabled = true;
            this.cboDireccion.Location = new System.Drawing.Point(97, 108);
            this.cboDireccion.Name = "cboDireccion";
            this.cboDireccion.Size = new System.Drawing.Size(374, 21);
            this.cboDireccion.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Dirección:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Departamento:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Dir. Gral.:";
            // 
            // groupBox_horarios
            // 
            this.groupBox_horarios.Controls.Add(this.txtComida);
            this.groupBox_horarios.Controls.Add(this.txtSalida);
            this.groupBox_horarios.Controls.Add(this.txtRegreso);
            this.groupBox_horarios.Controls.Add(this.txtEntrada);
            this.groupBox_horarios.Controls.Add(this.chkComida);
            this.groupBox_horarios.Controls.Add(this.chkRegreso);
            this.groupBox_horarios.Controls.Add(this.chkSalida);
            this.groupBox_horarios.Controls.Add(this.chkEntrada);
            this.groupBox_horarios.Location = new System.Drawing.Point(183, 224);
            this.groupBox_horarios.Name = "groupBox_horarios";
            this.groupBox_horarios.Size = new System.Drawing.Size(380, 216);
            this.groupBox_horarios.TabIndex = 10;
            this.groupBox_horarios.TabStop = false;
            this.groupBox_horarios.Text = "Horario";
            // 
            // txtComida
            // 
            this.txtComida.Location = new System.Drawing.Point(114, 56);
            this.txtComida.Mask = "00:00";
            this.txtComida.Name = "txtComida";
            this.txtComida.Size = new System.Drawing.Size(100, 20);
            this.txtComida.TabIndex = 7;
            this.txtComida.ValidatingType = typeof(System.DateTime);
            // 
            // txtSalida
            // 
            this.txtSalida.Location = new System.Drawing.Point(114, 116);
            this.txtSalida.Mask = "00:00";
            this.txtSalida.Name = "txtSalida";
            this.txtSalida.Size = new System.Drawing.Size(100, 20);
            this.txtSalida.TabIndex = 6;
            this.txtSalida.ValidatingType = typeof(System.DateTime);
            // 
            // txtRegreso
            // 
            this.txtRegreso.Location = new System.Drawing.Point(114, 89);
            this.txtRegreso.Mask = "00:00";
            this.txtRegreso.Name = "txtRegreso";
            this.txtRegreso.Size = new System.Drawing.Size(100, 20);
            this.txtRegreso.TabIndex = 5;
            this.txtRegreso.ValidatingType = typeof(System.DateTime);
            // 
            // txtEntrada
            // 
            this.txtEntrada.Location = new System.Drawing.Point(114, 25);
            this.txtEntrada.Mask = "00:00";
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(100, 20);
            this.txtEntrada.TabIndex = 4;
            this.txtEntrada.ValidatingType = typeof(System.DateTime);
            // 
            // chkComida
            // 
            this.chkComida.AutoSize = true;
            this.chkComida.Location = new System.Drawing.Point(36, 60);
            this.chkComida.Name = "chkComida";
            this.chkComida.Size = new System.Drawing.Size(61, 17);
            this.chkComida.TabIndex = 3;
            this.chkComida.Text = "Comida";
            this.chkComida.UseVisualStyleBackColor = true;
            // 
            // chkRegreso
            // 
            this.chkRegreso.AutoSize = true;
            this.chkRegreso.Location = new System.Drawing.Point(36, 92);
            this.chkRegreso.Name = "chkRegreso";
            this.chkRegreso.Size = new System.Drawing.Size(66, 17);
            this.chkRegreso.TabIndex = 2;
            this.chkRegreso.Text = "Regreso";
            this.chkRegreso.UseVisualStyleBackColor = true;
            // 
            // chkSalida
            // 
            this.chkSalida.AutoSize = true;
            this.chkSalida.Location = new System.Drawing.Point(36, 118);
            this.chkSalida.Name = "chkSalida";
            this.chkSalida.Size = new System.Drawing.Size(55, 17);
            this.chkSalida.TabIndex = 1;
            this.chkSalida.Text = "Salida";
            this.chkSalida.UseVisualStyleBackColor = true;
            // 
            // chkEntrada
            // 
            this.chkEntrada.AutoSize = true;
            this.chkEntrada.Location = new System.Drawing.Point(36, 29);
            this.chkEntrada.Name = "chkEntrada";
            this.chkEntrada.Size = new System.Drawing.Size(63, 17);
            this.chkEntrada.TabIndex = 0;
            this.chkEntrada.Text = "Entrada";
            this.chkEntrada.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox_generales, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox_horarios, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.img_photo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelActions, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(681, 504);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnRegistrarHuella);
            this.flowLayoutPanel1.Controls.Add(this.tbFingerPrint);
            this.flowLayoutPanel1.Controls.Add(this.img_fingerPrint);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 224);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(174, 216);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // tbFingerPrint
            // 
            this.tbFingerPrint.Location = new System.Drawing.Point(3, 32);
            this.tbFingerPrint.Multiline = true;
            this.tbFingerPrint.Name = "tbFingerPrint";
            this.tbFingerPrint.ReadOnly = true;
            this.tbFingerPrint.Size = new System.Drawing.Size(171, 156);
            this.tbFingerPrint.TabIndex = 14;
            // 
            // img_fingerPrint
            // 
            this.img_fingerPrint.AutoSize = true;
            this.img_fingerPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.img_fingerPrint.Location = new System.Drawing.Point(3, 191);
            this.img_fingerPrint.Name = "img_fingerPrint";
            this.img_fingerPrint.Padding = new System.Windows.Forms.Padding(4);
            this.img_fingerPrint.Size = new System.Drawing.Size(171, 21);
            this.img_fingerPrint.TabIndex = 13;
            // 
            // img_photo
            // 
            this.img_photo.AutoSize = true;
            this.img_photo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.img_photo.Location = new System.Drawing.Point(3, 0);
            this.img_photo.Name = "img_photo";
            this.img_photo.Padding = new System.Windows.Forms.Padding(4);
            this.img_photo.Size = new System.Drawing.Size(174, 221);
            this.img_photo.TabIndex = 12;
            // 
            // panelActions
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panelActions, 2);
            this.panelActions.Controls.Add(this.btnAgregar);
            this.panelActions.Location = new System.Drawing.Point(3, 457);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(675, 44);
            this.panelActions.TabIndex = 13;
            // 
            // FrmRegistrar
            // 
            this.AcceptButton = this.btnAgregar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 504);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmRegistrar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DGTIT - Actualizar Empleado";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnLoaded);
            this.groupBox_generales.ResumeLayout(false);
            this.groupBox_generales.PerformLayout();
            this.groupBox_horarios.ResumeLayout(false);
            this.groupBox_horarios.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnRegistrarHuella;
        private System.Windows.Forms.ComboBox cboDirGral;
        private System.Windows.Forms.GroupBox groupBox_generales;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox_horarios;
        private System.Windows.Forms.CheckBox chkComida;
        private System.Windows.Forms.CheckBox chkRegreso;
        private System.Windows.Forms.CheckBox chkSalida;
        private System.Windows.Forms.CheckBox chkEntrada;
        private System.Windows.Forms.MaskedTextBox txtComida;
        private System.Windows.Forms.MaskedTextBox txtSalida;
        private System.Windows.Forms.MaskedTextBox txtRegreso;
        private System.Windows.Forms.MaskedTextBox txtEntrada;
        private System.Windows.Forms.ComboBox cboDepartamento;
        private System.Windows.Forms.ComboBox cboDireccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSubdireccion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label img_photo;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.TextBox tb_employeeNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label img_fingerPrint;
        private System.Windows.Forms.TextBox tbFingerPrint;
    }
}