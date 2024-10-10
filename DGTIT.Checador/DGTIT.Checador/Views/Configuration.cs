using DGTIT.Checador.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DGTIT.Checador.Views
{
    public partial class Configuration : Form
    {

        private string name = string.Empty;
        private int selectedId = 0;
        private List<general_directions> generalDirections;

        public Configuration()
        {
            InitializeComponent();

            this.name = (string) Properties.Settings.Default["name"];
            this.selectedId = Convert.ToInt32(Properties.Settings.Default["generalDirectionId"]);
            this.Load += new EventHandler(LoadedDone);
        }

        private void LoadedDone(object sender, EventArgs e)
        {

            // * load the general directions
            var entities = new UsuariosDBEntities();
            this.generalDirections = entities.general_directions.ToList();


            // * initialize the inputs
            this.comboBox1.DataSource = this.generalDirections;
            this.comboBox1.ValueMember = "id";
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.SelectedItem = this.generalDirections.FirstOrDefault(item => item.id == selectedId);
            this.comboBox1.SelectedValueChanged += new EventHandler((object sender1, EventArgs e1) =>
            {
                selectedId = (int)( (general_directions) comboBox1.SelectedItem ).id;
            });

            this.txtName.Text = this.name;
            this.txtName.TextChanged += new EventHandler((object sender2, EventArgs e2) =>
            {
                name = txtName.Text;
            });

            this.button1.Click += new EventHandler(OnActulizarClick);

        }

        private void OnActulizarClick(object sender, EventArgs e)
        {
            Properties.Settings.Default["name"] = this.name;
            Properties.Settings.Default["generalDirectionId"] = this.selectedId;
            Properties.Settings.Default.Save();
            MessageBox.Show("Configuracion actualizada", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

    }
}
