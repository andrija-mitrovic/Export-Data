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
            string str = "";
            for (int i = 0; i < 100; i++)
            {
                foreach (DataRow row in dt.Rows)
                {
                    str = $"INSERT INTO {tableName} (sifra,barco,naziv,jedmj,pakov,zempo,roktr) VALUES (";
                    str += row["sifra"] + ",";
                    str += "'" + row["barco"].ToString().Trim(' ') + "',";
                    str += "'" + row["naziv"].ToString().Trim(' ') + "',";
                    str += "'" + row["jedmj"].ToString().Trim(' ') + "',";
                    str += row["pakov"].ToString().Replace(".", "").Replace(",", ".") + ",";
                    str += "'" + row["zempo"].ToString().Trim(' ') + "',";
                    str += row["roktr"] + ");";

                    connection.ExecuteInsertQuery(str);
                }
            }
        }
    }
}
