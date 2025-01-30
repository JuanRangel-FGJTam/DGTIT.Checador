using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DGTIT.Checador.Views
{
    public partial class AuthForm : Form {

        private int count = 0;
        public AuthForm() {
            InitializeComponent();
        }

        private void BtnValidar_Click(object sender, EventArgs e) {
            Cursor = Cursors.WaitCursor;

            var _hashedPassword = DGTIT.Checador.Helpers.HashData.HashSHA1(tbPassword.Text);
            var _client = _hashedPassword.Equals("c0b13654c22164e7b3af8c4e1ed0783ae918a905") ? "ok" : null;
            if (_client == null) {
                MessageBox.Show("Contraseña inválida, intente de nuevo.", "No autorizado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                count++;

                if(count >= 3) {
                    this.DialogResult = DialogResult.Cancel;
                    Cursor = Cursors.Default;
                    return;
                }
                return;
            }

            Cursor = Cursors.Default;
            this.DialogResult = DialogResult.Yes;
        }
    }
}
