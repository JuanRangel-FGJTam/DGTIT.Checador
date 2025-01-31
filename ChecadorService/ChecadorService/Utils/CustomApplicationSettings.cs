using Microsoft.Win32;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecadorService.Utils {
    internal class CustomApplicationSettings {

        public static string GetGeneralDirections()
        {
            string settingValue = String.Empty;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UsuariosDBLocal"].ConnectionString))
            {
                sqlConnection.Open();
                var command = new SqlCommand("SELECT [VALUE] FROM [system].[options] WHERE [name] = 'AREAS'", sqlConnection);
                settingValue = command.ExecuteScalar().ToString();
                sqlConnection.Close();
            }
            return settingValue;
        }

        public static string GetStoragePath()
        {
            string settingValue = String.Empty;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UsuariosDBLocal"].ConnectionString))
            {
                sqlConnection.Open();
                var command = new SqlCommand("SELECT [VALUE] FROM [system].[options] WHERE [name] = 'STORAGEPATH'", sqlConnection);
                settingValue = command.ExecuteScalar().ToString();
                sqlConnection.Close();
            }
            return settingValue;
        }

        public static string GetAppName()

        {
            string settingValue = String.Empty;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UsuariosDBLocal"].ConnectionString))
            {
                sqlConnection.Open();
                var command = new SqlCommand("SELECT [VALUE] FROM [system].[options] WHERE [name] = 'NAME'", sqlConnection);
                settingValue = command.ExecuteScalar().ToString();
                sqlConnection.Close();
            }
            return settingValue;
        }   

    }
}
