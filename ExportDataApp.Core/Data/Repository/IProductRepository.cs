using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace ExcelReportApp.Core.Data.Repository
{
    public interface IProductRepository
    {
        DataTable GetAllProducts(IDbConnection connection);
        void CreateTable(string query, IDbConnection connection);
        void InsertProducts(string tableName, DataTable dt, IDbConnection connection);
    }
}
