using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportDataApp.Core.Services
{
    public class CsvService
    {
        private string _filePath = ConfigurationManager.AppSettings["CsvPath"];
        private string _serverName = ConfigurationManager.AppSettings["ServerName"];
        private string _database = ConfigurationManager.AppSettings["Database"];

        public void ExportDataToCsv(string fileName, string query)
        {
            try
            {
                _filePath += fileName;
                string command = $"/C sqlcmd -S {_serverName} -d {_database} -E -Q \"SET NOCOUNT ON; {query}\" -o \"{_filePath}\" -h-1 -s \";\" -W -w 1024";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = command;
                process.StartInfo = startInfo;
                process.Start();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
