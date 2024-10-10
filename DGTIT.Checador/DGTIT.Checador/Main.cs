using DGTIT.Checador.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows.Forms;
using static DGTIT.Checador.User32;

namespace DGTIT.Checador
{
    public partial class Main : Form
    {

        private Configuration ca;
        private Views.Checador checadorForm;
        
        public Main()
        {
            InitializeComponent();

            // * prepare the events
            this.btnRegistrar.Click += new EventHandler(BtnRegistrar_Click);
            this.btnVerificar.Click += new EventHandler(BtnVerificar_Click);
            this.btnSettings.Click += new EventHandler(ShowConfiguracion);
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
            checadorForm = new Views.Checador();
            checadorForm.ShowDialog(this);
        }

        private void ShowConfiguracion(object sender, EventArgs e)
        {
            if(ca == null)
            {
                var ca = new Configuration();
                ca.ShowDialog(this);
                ca = null;
            }
        }

    }
}
