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

namespace DGTIT.Checador
{
    public partial class Main : Form
    {

        private Views.Checador checadorForm;

        bool ctrlKPressed = false; 

        public Main()
        {
            InitializeComponent();
            this.KeyPreview = true;

            // * prepare the events
            this.btnRegistrar.Click += new EventHandler(BtnRegistrar_Click);
            this.btnVerificar.Click += new EventHandler(BtnVerificar_Click);
            this.KeyDown += new KeyEventHandler(ShowConfiguracion);
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
            this.Visible = false;
            this.ShowInTaskbar = false;

            checadorForm = new Views.Checador();
            checadorForm.ShowDialog(this);

            this.Visible = true;
            this.ShowInTaskbar = true;
        }

        private void ShowConfiguracion(object sender, KeyEventArgs e)
        {

            // Check if Ctrl is pressed along with K
            if (e.Control && e.KeyCode == Keys.K)
            {
                ctrlKPressed = true; // Set flag that Ctrl + K was pressed
            }
            // Check if Ctrl is pressed along with O and Ctrl + K was already pressed
            else if (ctrlKPressed && e.Control && e.KeyCode == Keys.O)
            {
                var ca = new Configuration();
                ca.ShowDialog(this);
                ca = null;
                ctrlKPressed = false; // Reset flag
            }
            else
            {
                ctrlKPressed = false; // Reset if any other key is pressed
            }
        }

    }
}
