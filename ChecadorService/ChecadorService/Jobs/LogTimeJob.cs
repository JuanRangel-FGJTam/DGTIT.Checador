using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Quartz;
using log4net;
using System.Threading.Tasks;
using System.Configuration;

namespace ChecadorService.Jobs {
    internal class LogTimeJob : IJob {

        //public LogInfoJob(ILog log) 
        //{
        //    Log = log ?? throw new ArgumentNullException(nameof(log));
        //}

        public async Task Execute(IJobExecutionContext context)
        {
            var connectionString  = ConfigurationManager.ConnectionStrings["UsuariosDBEntities"].ConnectionString;
            //var connectionString = context.Get("serverConnectionString") ?? throw new ArgumentException("The server connection string is not present.");
            using(var sqlConnection = new SqlConnection(connectionString.ToString())) 
            {
                sqlConnection.Open();
                var command = new SqlCommand("SELECT GETDATE() as date", sqlConnection);
                var responseText = await command.ExecuteScalarAsync();
                //Log.Info(responseText.ToString());
                Console.WriteLine(responseText.ToString());
                sqlConnection.Close();
            }
        }
    }
}
