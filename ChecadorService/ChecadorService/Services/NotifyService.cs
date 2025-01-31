using ChecadorService.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChecadorService.Services
{
    internal class NotifyService
    {
        private readonly ILog log;

        public NotifyService()
        {
            log = LogManager.GetLogger(typeof(NotifyService));
        }

        public async Task Notify()
        {
            try
            {
                var name = CustomApplicationSettings.GetAppName();
                var ipAddress = GetIpAddress();
                var query = $"INSERT INTO [dbo].[clientsStatusLog]([name],[address],[updated_at]) VALUES ('{name}','{ipAddress}', getdate())";
                var connectionString = ConfigurationManager.ConnectionStrings["UsuariosDBEntities"].ConnectionString ?? throw new ArgumentNullException("UsuariosDBEntities", "The connection string is not registered.");
                using (var sqlConnection = new SqlConnection(connectionString))
                {   
                    await sqlConnection.OpenAsync();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    await sqlCommand.ExecuteNonQueryAsync();
                    sqlConnection.Close();
                }
            }catch(Exception err)
            {
                log.Error("Fail at attempt to send the notification to the remote server", err);
            }
        }

        private string GetIpAddress()
        {
            try
            {
                // Get the hostname of the machine
                string hostName = Dns.GetHostName();

                // Get the IP addresses associated with the hostname
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);

                // Filter out IPv6 addresses and display the first available IPv4 address
                string ipAddress = addresses.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();

                // Display the IP address in the label
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    return ipAddress;
                }
                else
                {
                    return "0.0.0.0";
                }
            }
            catch (Exception err)
            {
                log.Error(err.Message, err);
                return "0.0.0.1";
            }
        }

    }
}
