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


namespace DGTIT.Checador.Views {
    public partial class AuthForm : Form {

        private readonly UsuariosDBEntities contexto;

        private int count = 0;
        public AuthForm() {
            InitializeComponent();
            contexto = new UsuariosDBEntities();
        }

        private void BtnValidar_Click(object sender, EventArgs e) {
            Cursor = Cursors.WaitCursor;

            var _hashedPassword = HashPasswordSHA1(tbPassword.Text);

            var _client = contexto.clients.FirstOrDefault(item => item.user == "checador@fgjtam.gob.mx" && item.password.ToLower() == _hashedPassword);

            if(_client == null) {
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

        private static string HashPasswordSHA1(string password) {
            // Convert the input string to a byte array
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Initialize SHA1 and compute the hash
            using (SHA1 sha1 = SHA1.Create()) {
                byte[] hashBytes = sha1.ComputeHash(passwordBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes) {
                    sb.Append(b.ToString("x2"));  // Converts each byte to a 2-character hex string
                }
                return sb.ToString();
            }
        }
    }
}
