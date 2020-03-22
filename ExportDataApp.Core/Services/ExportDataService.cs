using ExcelReportApp.Core.Data.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportDataApp.Core.Services
{
    public class ExportDataService
    {
        public void ExportToCsv(string query, string fileName)
        {
            CsvService service = new CsvService();
            service.ExportDataToCsv(fileName, query);
        }

        public void ExportToExcel(string fileName, DataTable dt)
        {
            string tempTable = "Temp";
            ExportToDbf(tempTable, dt);
            ExcelService service = new ExcelService();
            service.ExportDataToExcel(fileName, tempTable);
        }

        public void ExportToDbf(string tableName, DataTable dt)
        {
            DbfService service = new DbfService(new ProductRepository(), tableName);
            service.ExportDataToDbf(dt);
        }
    }
}
