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
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtComida = new System.Windows.Forms.MaskedTextBox();
            this.txtSalida = new System.Windows.Forms.MaskedTextBox();
            this.txtRegreso = new System.Windows.Forms.MaskedTextBox();
            this.txtEntrada = new System.Windows.Forms.MaskedTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tbFingerPrint = new System.Windows.Forms.TextBox();
            this.img_photo = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.label111 = new System.Windows.Forms.Label();
            this.lblUpdatedAt = new System.Windows.Forms.Label();
            this.panelHours = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_check = new System.Windows.Forms.CheckBox();
            this.groupBox_generales.SuspendLayout();
            this.groupBox_horarios.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.panelHours.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAgregar.Location = new System.Drawing.Point(284, 3);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(137, 35);
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
            this.btnRegistrarHuella.Location = new System.Drawing.Point(32, 171);
            this.btnRegistrarHuella.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.btnRegistrarHuella.Name = "btnRegistrarHuella";
            this.btnRegistrarHuella.Size = new System.Drawing.Size(114, 23);
            this.btnRegistrarHuella.TabIndex = 5;
            this.btnRegistrarHuella.Text = "Actualizar Huella";
            this.btnRegistrarHuella.UseVisualStyleBackColor = true;
            this.btnRegistrarHuella.Click += new System.EventHandler(this.BtnRegistrarHuellaClick);
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
            this.groupBox_generales.Controls.Add(this.lblUpdatedAt);
            this.groupBox_generales.Controls.Add(this.label111);
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
            this.groupBox_generales.Size = new System.Drawing.Size(495, 230);
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
            this.groupBox_horarios.Controls.Add(this.label10);
            this.groupBox_horarios.Controls.Add(this.label9);
            this.groupBox_horarios.Controls.Add(this.label8);
            this.groupBox_horarios.Controls.Add(this.label4);
            this.groupBox_horarios.Controls.Add(this.txtComida);
            this.groupBox_horarios.Controls.Add(this.txtSalida);
            this.groupBox_horarios.Controls.Add(this.txtRegreso);
            this.groupBox_horarios.Controls.Add(this.txtEntrada);
            this.groupBox_horarios.Location = new System.Drawing.Point(3, 3);
            this.groupBox_horarios.Name = "groupBox_horarios";
            this.groupBox_horarios.Size = new System.Drawing.Size(262, 204);
            this.groupBox_horarios.TabIndex = 10;
            this.groupBox_horarios.TabStop = false;
            this.groupBox_horarios.Text = "Horario";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(55, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Salida";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Regreso";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(49, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Comida";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Entrada";
            // 
            // txtComida
            // 
            this.txtComida.Location = new System.Drawing.Point(101, 56);
            this.txtComida.Mask = "00:00";
            this.txtComida.Name = "txtComida";
            this.txtComida.Size = new System.Drawing.Size(100, 20);
            this.txtComida.TabIndex = 7;
            this.txtComida.ValidatingType = typeof(System.DateTime);
            // 
            // txtSalida
            // 
            this.txtSalida.Location = new System.Drawing.Point(101, 116);
            this.txtSalida.Mask = "00:00";
            this.txtSalida.Name = "txtSalida";
            this.txtSalida.Size = new System.Drawing.Size(100, 20);
            this.txtSalida.TabIndex = 6;
            this.txtSalida.ValidatingType = typeof(System.DateTime);
            // 
            // txtRegreso
            // 
            this.txtRegreso.Location = new System.Drawing.Point(101, 89);
            this.txtRegreso.Mask = "00:00";
            this.txtRegreso.Name = "txtRegreso";
            this.txtRegreso.Size = new System.Drawing.Size(100, 20);
            this.txtRegreso.TabIndex = 5;
            this.txtRegreso.ValidatingType = typeof(System.DateTime);
            // 
            // txtEntrada
            // 
            this.txtEntrada.Location = new System.Drawing.Point(101, 25);
            this.txtEntrada.Mask = "00:00";
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(100, 20);
            this.txtEntrada.TabIndex = 4;
            this.txtEntrada.ValidatingType = typeof(System.DateTime);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.img_photo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox_generales, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelHours, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelActions, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(681, 521);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tbFingerPrint);
            this.flowLayoutPanel1.Controls.Add(this.btnRegistrarHuella);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 239);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(174, 216);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // tbFingerPrint
            // 
            this.tbFingerPrint.Font = new System.Drawing.Font("Consolas", 8F);
            this.tbFingerPrint.Location = new System.Drawing.Point(3, 3);
            this.tbFingerPrint.Multiline = true;
            this.tbFingerPrint.Name = "tbFingerPrint";
            this.tbFingerPrint.ReadOnly = true;
            this.tbFingerPrint.Size = new System.Drawing.Size(171, 162);
            this.tbFingerPrint.TabIndex = 14;
            // 
            // img_photo
            // 
            this.img_photo.AutoSize = true;
            this.img_photo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.img_photo.Location = new System.Drawing.Point(3, 0);
            this.img_photo.Name = "img_photo";
            this.img_photo.Padding = new System.Windows.Forms.Padding(4);
            this.img_photo.Size = new System.Drawing.Size(174, 236);
            this.img_photo.TabIndex = 12;
            // 
            // panelActions
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panelActions, 2);
            this.panelActions.Controls.Add(this.btnAgregar);
            this.panelActions.Location = new System.Drawing.Point(3, 474);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(675, 44);
            this.panelActions.TabIndex = 13;
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(6, 203);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(104, 13);
            this.label111.TabIndex = 17;
            this.label111.Text = "Ultima actualizacion:";
            // 
            // lblUpdatedAt
            // 
            this.lblUpdatedAt.AutoSize = true;
            this.lblUpdatedAt.Location = new System.Drawing.Point(112, 203);
            this.lblUpdatedAt.Name = "lblUpdatedAt";
            this.lblUpdatedAt.Size = new System.Drawing.Size(0, 13);
            this.lblUpdatedAt.TabIndex = 18;
            // 
            // panelHours
            // 
            this.panelHours.Controls.Add(this.groupBox1);
            this.panelHours.Controls.Add(this.groupBox_horarios);
            this.panelHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHours.Location = new System.Drawing.Point(183, 239);
            this.panelHours.Name = "panelHours";
            this.panelHours.Size = new System.Drawing.Size(495, 229);
            this.panelHours.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_check);
            this.groupBox1.Location = new System.Drawing.Point(271, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 204);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones";
            // 
            // cb_check
            // 
            this.cb_check.AutoSize = true;
            this.cb_check.Location = new System.Drawing.Point(6, 28);
            this.cb_check.Name = "cb_check";
            this.cb_check.Size = new System.Drawing.Size(115, 17);
            this.cb_check.TabIndex = 1;
            this.cb_check.Text = "Registra asistencia";
            this.cb_check.UseVisualStyleBackColor = true;
            // 
            // FrmRegistrar
            // 
            this.AcceptButton = this.btnAgregar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 521);
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
            this.panelHours.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox tbFingerPrint;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label lblUpdatedAt;
        private System.Windows.Forms.Panel panelHours;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_check;
    }
}