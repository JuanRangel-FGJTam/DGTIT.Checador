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
            this.label2 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtHuella = new System.Windows.Forms.TextBox();
            this.btnRegistrarHuella = new System.Windows.Forms.Button();
            this.dgvListar = new System.Windows.Forms.DataGridView();
            this.cboDirGral = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboSubdireccion = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboDepartamento = new System.Windows.Forms.ComboBox();
            this.cboDireccion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtComida = new System.Windows.Forms.MaskedTextBox();
            this.txtSalida = new System.Windows.Forms.MaskedTextBox();
            this.txtRegreso = new System.Windows.Forms.MaskedTextBox();
            this.txtEntrada = new System.Windows.Forms.MaskedTextBox();
            this.chkComida = new System.Windows.Forms.CheckBox();
            this.chkRegreso = new System.Windows.Forms.CheckBox();
            this.chkSalida = new System.Windows.Forms.CheckBox();
            this.chkEntrada = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Huella:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAgregar.Enabled = false;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAgregar.Location = new System.Drawing.Point(415, 231);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(99, 23);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(84, 29);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(321, 20);
            this.txtNombre.TabIndex = 3;
            // 
            // txtHuella
            // 
            this.txtHuella.Location = new System.Drawing.Point(84, 59);
            this.txtHuella.Name = "txtHuella";
            this.txtHuella.Size = new System.Drawing.Size(321, 20);
            this.txtHuella.TabIndex = 4;
            // 
            // btnRegistrarHuella
            // 
            this.btnRegistrarHuella.Location = new System.Drawing.Point(407, 56);
            this.btnRegistrarHuella.Name = "btnRegistrarHuella";
            this.btnRegistrarHuella.Size = new System.Drawing.Size(88, 23);
            this.btnRegistrarHuella.TabIndex = 5;
            this.btnRegistrarHuella.Text = "Capturar Huella";
            this.btnRegistrarHuella.UseVisualStyleBackColor = true;
            this.btnRegistrarHuella.Click += new System.EventHandler(this.btnRegistrarHuella_Click);
            // 
            // dgvListar
            // 
            this.dgvListar.AllowUserToAddRows = false;
            this.dgvListar.AllowUserToDeleteRows = false;
            this.dgvListar.AllowUserToResizeColumns = false;
            this.dgvListar.AllowUserToResizeRows = false;
            this.dgvListar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListar.Location = new System.Drawing.Point(19, 255);
            this.dgvListar.MultiSelect = false;
            this.dgvListar.Name = "dgvListar";
            this.dgvListar.ReadOnly = true;
            this.dgvListar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListar.Size = new System.Drawing.Size(495, 209);
            this.dgvListar.TabIndex = 6;
            this.dgvListar.SelectionChanged += new System.EventHandler(this.dgvListar_SelectionChanged);
            // 
            // cboDirGral
            // 
            this.cboDirGral.FormattingEnabled = true;
            this.cboDirGral.Location = new System.Drawing.Point(84, 94);
            this.cboDirGral.Name = "cboDirGral";
            this.cboDirGral.Size = new System.Drawing.Size(321, 21);
            this.cboDirGral.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboSubdireccion);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboDepartamento);
            this.groupBox1.Controls.Add(this.cboDireccion);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.cboDirGral);
            this.groupBox1.Controls.Add(this.btnRegistrarHuella);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtHuella);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 215);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales";
            // 
            // cboSubdireccion
            // 
            this.cboSubdireccion.FormattingEnabled = true;
            this.cboSubdireccion.Location = new System.Drawing.Point(84, 161);
            this.cboSubdireccion.Name = "cboSubdireccion";
            this.cboSubdireccion.Size = new System.Drawing.Size(321, 21);
            this.cboSubdireccion.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Subdirección:";
            // 
            // cboDepartamento
            // 
            this.cboDepartamento.FormattingEnabled = true;
            this.cboDepartamento.Location = new System.Drawing.Point(84, 188);
            this.cboDepartamento.Name = "cboDepartamento";
            this.cboDepartamento.Size = new System.Drawing.Size(321, 21);
            this.cboDepartamento.TabIndex = 12;
            // 
            // cboDireccion
            // 
            this.cboDireccion.FormattingEnabled = true;
            this.cboDireccion.Location = new System.Drawing.Point(84, 125);
            this.cboDireccion.Name = "cboDireccion";
            this.cboDireccion.Size = new System.Drawing.Size(321, 21);
            this.cboDireccion.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Dirección:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Departamento:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Dir. Gral.:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(74, 233);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(281, 20);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Buscar...";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtComida);
            this.groupBox2.Controls.Add(this.txtSalida);
            this.groupBox2.Controls.Add(this.txtRegreso);
            this.groupBox2.Controls.Add(this.txtEntrada);
            this.groupBox2.Controls.Add(this.chkComida);
            this.groupBox2.Controls.Add(this.chkRegreso);
            this.groupBox2.Controls.Add(this.chkSalida);
            this.groupBox2.Controls.Add(this.chkEntrada);
            this.groupBox2.Location = new System.Drawing.Point(513, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 215);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Horario";
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
            // frmRegistrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 483);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvListar);
            this.Controls.Add(this.btnAgregar);
            this.Name = "frmRegistrar";
            this.Text = "frmRegistrar";
            this.Load += new System.EventHandler(this.frmRegistrar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtHuella;
        private System.Windows.Forms.Button btnRegistrarHuella;
        private System.Windows.Forms.DataGridView dgvListar;
        private System.Windows.Forms.ComboBox cboDirGral;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
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
    }
}