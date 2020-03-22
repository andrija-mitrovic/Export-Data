using System.Data;

namespace ExcelReportApp.Core.Data
{
    public interface IDbConnection
    {
        void ExecuteInsertQuery(string query);
        DataTable ExecuteSelectQuery(string query);
        void ExecuteCreateQuery(string query);
    }
}