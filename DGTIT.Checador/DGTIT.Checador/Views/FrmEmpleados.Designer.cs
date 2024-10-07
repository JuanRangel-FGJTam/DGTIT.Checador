namespace DGTIT.Checador
{
    partial class FrmEmpleados
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
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dataGridEmpleados = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayout_actions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.flowLayout_filters = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.areaComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmpleados)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayout_actions.SuspendLayout();
            this.flowLayout_filters.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAgregar
            // 
            this.btnAgregar.AutoSize = true;
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAgregar.Location = new System.Drawing.Point(3, 3);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(99, 25);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // dataGridEmpleados
            // 
            this.dataGridEmpleados.AllowUserToAddRows = false;
            this.dataGridEmpleados.AllowUserToDeleteRows = false;
            this.dataGridEmpleados.AllowUserToResizeColumns = false;
            this.dataGridEmpleados.AllowUserToResizeRows = false;
            this.dataGridEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridEmpleados.Location = new System.Drawing.Point(3, 103);
            this.dataGridEmpleados.MultiSelect = false;
            this.dataGridEmpleados.Name = "dataGridEmpleados";
            this.dataGridEmpleados.ReadOnly = true;
            this.dataGridEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridEmpleados.Size = new System.Drawing.Size(876, 396);
            this.dataGridEmpleados.TabIndex = 6;
            this.dataGridEmpleados.SelectionChanged += new System.EventHandler(this.DataGridSelectionChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(3, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(280, 20);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.TextBoxSearchTextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayout_actions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayout_filters, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridEmpleados, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(882, 502);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // flowLayout_actions
            // 
            this.flowLayout_actions.Controls.Add(this.btnAgregar);
            this.flowLayout_actions.Controls.Add(this.btnEdit);
            this.flowLayout_actions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayout_actions.Location = new System.Drawing.Point(3, 3);
            this.flowLayout_actions.Name = "flowLayout_actions";
            this.flowLayout_actions.Size = new System.Drawing.Size(876, 34);
            this.flowLayout_actions.TabIndex = 7;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.Location = new System.Drawing.Point(108, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(116, 25);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Editar empleado";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // flowLayout_filters
            // 
            this.flowLayout_filters.Controls.Add(this.flowLayoutPanel4);
            this.flowLayout_filters.Controls.Add(this.flowLayoutPanel1);
            this.flowLayout_filters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayout_filters.Location = new System.Drawing.Point(3, 43);
            this.flowLayout_filters.Name = "flowLayout_filters";
            this.flowLayout_filters.Size = new System.Drawing.Size(876, 54);
            this.flowLayout_filters.TabIndex = 0;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanel4.Controls.Add(this.label4);
            this.flowLayoutPanel4.Controls.Add(this.txtSearch);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(304, 51);
            this.flowLayoutPanel4.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Buscar";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.areaComboBox);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(313, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(392, 40);
            this.flowLayoutPanel1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Area";
            // 
            // areaComboBox
            // 
            this.areaComboBox.FormattingEnabled = true;
            this.areaComboBox.Location = new System.Drawing.Point(3, 16);
            this.areaComboBox.Name = "areaComboBox";
            this.areaComboBox.Size = new System.Drawing.Size(386, 21);
            this.areaComboBox.TabIndex = 10;
            // 
            // FrmEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 502);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmEmpleados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DGTIT - Empleados";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormOnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmpleados)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayout_actions.ResumeLayout(false);
            this.flowLayout_actions.PerformLayout();
            this.flowLayout_filters.ResumeLayout(false);
            this.flowLayout_filters.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dataGridEmpleados;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayout_filters;
        private System.Windows.Forms.FlowLayoutPanel flowLayout_actions;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ComboBox areaComboBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label4;
    }
}