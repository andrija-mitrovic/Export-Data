using Advantage.Data.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReportApp.Core.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void CreateTable(string query, IDbConnection connection)
        {
            connection.ExecuteCreateQuery(query);
        }

        public DataTable GetAllProducts(IDbConnection connection)
        {
            string query = "SELECT sifra, barco, naziv, jedmj, pakov, zempo, roktr FROM R_ROBA";

            return connection.ExecuteSelectQuery(query);
        }

        public void InsertProducts(string tableName, DataTable dt, IDbConnection connection)
        {
            foreach (DataRow row in dt.Rows)
            {
                string insertSql = $"INSERT INTO {tableName} (";
                foreach (DataColumn column in dt.Columns)
                {
                    insertSql += column.ColumnName + ",";
                }

                insertSql = insertSql.Substring(0, insertSql.Length - 1) + ") VALUES (";

                string insertRow = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (row[i].GetType() == typeof(string) || row[i].GetType() == typeof(DateTime) || row[i] == DBNull.Value)
                        insertRow += "'" + row[i].ToString().Trim(' ') + "',";
                    else
                        insertRow += row[i] + ",";
                }

                insertSql += insertRow.Substring(0, insertRow.Length - 1) + ")";

                connection.ExecuteInsertQuery(insertSql);
            }
        }
    }
}
