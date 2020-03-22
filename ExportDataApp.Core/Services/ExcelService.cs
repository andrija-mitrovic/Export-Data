using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExportDataApp.Core.Services
{
    public class ExcelService
    {
        private string _excelPath = ConfigurationManager.AppSettings["ExcelPath"];
        private string _dbfPath = ConfigurationManager.AppSettings["DbfPath"];
        private string _dbfExt = ".dbf";

        public void ExportDataToExcel(string fileName, string tempTable)
        {
            if (File.Exists(_dbfPath + tempTable + _dbfExt))
            {
                if (File.Exists(_excelPath + fileName))
                    File.Delete(_excelPath + fileName);

                try
                {
                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Open(_dbfPath + tempTable + _dbfExt);

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    xlWorkBook.SaveAs(_excelPath + fileName);
                    xlApp.Quit();

                    var excelApp = new Excel.Application();
                    excelApp.Visible = true;
                    excelApp.Workbooks.Open(_excelPath + fileName);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
