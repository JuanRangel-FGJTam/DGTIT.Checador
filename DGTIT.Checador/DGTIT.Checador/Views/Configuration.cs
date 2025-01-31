using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Core.Interfaces;
using DGTIT.Checador.Data.Repositories;
using DGTIT.Checador.Properties;
using DGTIT.Checador.Utilities;

namespace DGTIT.Checador.Views
{
    public partial class Configuration : Form
    {
        private ICatalogsRepository catalogsRepository;
        private string name = string.Empty;
        private string storagePath = string.Empty;
        private List<long> selectedIds = new List<long>();
        private List<GeneralDirection> generalDirections;
        
        public Configuration()
        {
            InitializeComponent();
            catalogsRepository = new CatalogsRepository();
            this.name = CustomApplicationSettings.GetAppName();
            this.storagePath = CustomApplicationSettings.GetStoragePath();
            this.selectedIds = CustomApplicationSettings.GetGeneralDirections().ToString().Split(';').Select( d => Convert.ToInt64(d)).ToList();
            this.Load += new EventHandler(LoadedDone);
        }

        private async void LoadedDone(object sender, EventArgs e)
        {
            // * load the general directions
            this.generalDirections = (await catalogsRepository.FindAllGeneralDirections()).ToList();
            
            // * initialize the checklist
            this.checkListAreas.DisplayMember = "name";
            foreach (var item in this.generalDirections)
            {
                this.checkListAreas.Items.Add(item, this.selectedIds.Contains(item.Id) );
            }
            this.checkListAreas.SelectedValue = selectedIds.ToArray();

            // * initialize the textboxw
            this.tb_storagePath.Text = this.storagePath;
            this.txtName.Text = this.name;
            this.txtName.TextChanged += new EventHandler((object sender2, EventArgs e2) =>
            {
                name = txtName.Text;
            });
            this.button1.Click += new EventHandler(OnActulizarClick);
        }


        private void OnActulizarClick(object sender, EventArgs e)
        {
            // get selected items
            var listIds = new List<long>();
            foreach (var checkedItem in checkListAreas.CheckedItems)
            {
                // Cast the checked item back to MyItem to access its properties
                var item = (GeneralDirection) checkedItem;
                listIds.Add(item.Id);
            }
            // * set the properties
            CustomApplicationSettings.SetAppName(this.name.Trim());
            CustomApplicationSettings.SetGeneralDirection(string.Join(";", listIds.ToArray()));
            MessageBox.Show("Configuracion actualizada", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        
    }
}
