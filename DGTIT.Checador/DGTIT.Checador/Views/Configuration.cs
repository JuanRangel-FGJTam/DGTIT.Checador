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
        private bool playSoundOnFail = false;
        private List<long> selectedIds = new List<long>();
        private List<general_directions> generalDirections;
        private int intervalSyncClock;
        private int employeesTimeout;

        public Configuration()
        {
            InitializeComponent();

            this.name = (string) Properties.Settings.Default["name"];
            this.selectedIds = Properties.Settings.Default["generalDirectionId"].ToString().Split(',').Select( d => Convert.ToInt64(d)).ToList();
            this.playSoundOnFail = Properties.Settings.Default["playSoundOnFail"].ToString() == "1";
            this.intervalSyncClock = Convert.ToInt32(Properties.Settings.Default["intervalSyncClock"]);
            this.employeesTimeout = Convert.ToInt32(Properties.Settings.Default["employeesTimeout"]);
            this.Load += new EventHandler(LoadedDone);
        }

        private void LoadedDone(object sender, EventArgs e)
        {

            // * load the general directions
            var entities = new UsuariosDBEntities();
            this.generalDirections = entities.general_directions.ToList();


            // * initialize the checklist
            this.checkListAreas.DisplayMember = "name";
            foreach (var item in this.generalDirections)
            {
                this.checkListAreas.Items.Add(item, this.selectedIds.Contains(item.id) );
            }
            this.checkListAreas.SelectedValue = selectedIds.ToArray();


            // * initialize the textbox
            this.txtName.Text = this.name;
            this.txtName.TextChanged += new EventHandler((object sender2, EventArgs e2) =>
            {
                name = txtName.Text;
            });
            this.button1.Click += new EventHandler(OnActulizarClick);

            this.chbPlayOnFail.Checked = this.playSoundOnFail;
            this.chbPlayOnFail.CheckedChanged += new EventHandler((object sender3, EventArgs e3) => {
                playSoundOnFail = chbPlayOnFail.Checked;
            });

            this.tb_intervalClock.Value = this.intervalSyncClock;

            this.tb_connectionTimeout.Value = this.employeesTimeout;
        }


        private void OnActulizarClick(object sender, EventArgs e)
        {
            // get selected items
            var listIds = new List<long>();
            foreach (var checkedItem in checkListAreas.CheckedItems)
            {
                // Cast the checked item back to MyItem to access its properties
                var item = (general_directions)checkedItem;
                listIds.Add(item.id);
            }
            // * set the properties
            Properties.Settings.Default["name"] = this.name;
            Properties.Settings.Default["generalDirectionId"] = string.Join(",", listIds.ToArray());
            Properties.Settings.Default["playSoundOnFail"] = this.playSoundOnFail?"1":"0";
            Properties.Settings.Default["intervalSyncClock"] = ((int)this.tb_intervalClock.Value).ToString();
            Properties.Settings.Default["employeesTimeout"] = ((int)this.tb_connectionTimeout.Value).ToString();

            Properties.Settings.Default.Save();
            MessageBox.Show("Configuracion actualizada", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

    }
}
