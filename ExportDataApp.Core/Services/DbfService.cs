using ExcelReportApp.Core.Data;
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
    public class DbfService
    {
        private const string _dbfExtension = ".dbf";
        private readonly string _tableName = "";
        private string _path = ConfigurationManager.AppSettings["ExcelPath"];
        private readonly IProductRepository _repo;

        public DbfService(IProductRepository repo, string tableName)
        {
            _repo = repo;
            _tableName = tableName;
        }

        public void ExportDataToDbf(DataTable dt)
        {
            if (_tableName != "")
            {
                if (File.Exists(_path + _tableName + _dbfExtension))
                    File.Delete(_path + _tableName + _dbfExtension);

                CreateDbfFile(dt, _path);
                InsertDataDbf(dt);
            }
            else
            {
                throw new Exception("You must insert table name!");
            }
        }

        private void InsertDataDbf(DataTable dt)
        {
            _repo.InsertProducts(_tableName, dt, new AdsDbConnection());
        }

        private void CreateDbfFile(DataTable dt, string path)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string createSql = $"CREATE TABLE {_tableName} (";

                    foreach (DataColumn dc in dt.Columns)
                    {
                        string fieldName = dc.ColumnName;
                        string type = dc.DataType.ToString();

                        switch (type)
                        {
                            case "System.String":
                                type = "char(50)";
                                break;
                            case "System.Boolean":
                                type = "char(10)";
                                break;
                            case "System.Int32":
                                type = "Numeric(10,0)";
                                break;
                            case "System.Double":
                                type = "Numeric(10,2)";
                                break;
                            case "System.Decimal":
                                type = "Numeric(10,2)";
                                break;
                            case "System.DateTime":
                                type = "Date";
                                break;
                        }

                        createSql += fieldName + " " + type + ",";
                    }

                    createSql = createSql.Substring(0, createSql.Length - 1) + ")";
                    _repo.CreateTable(createSql, new AdsDbConnection());
                }
            }
        }
    }
}
