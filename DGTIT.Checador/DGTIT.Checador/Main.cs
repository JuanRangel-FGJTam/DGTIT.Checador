using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows.Forms;

namespace DGTIT.Checador
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            this.btnRegistrar.Click += new EventHandler(BtnRegistrar_Click);
            this.btnVerificar.Click += new EventHandler(BtnVerificar_Click);
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            var empleados = new FrmEmpleados();
            empleados.ShowDialog(this);
            this.Visible = true;

            //FrmRegistrar registrar = new FrmRegistrar();
            //registrar.ShowDialog();
        }

        private void BtnVerificar_Click(object sender, EventArgs e)
        {
            frmVerificar verificar = new frmVerificar();
            verificar.ShowDialog(this);
        }

    }
}
