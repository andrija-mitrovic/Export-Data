using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReportApp.Core.Data
{
    public class SqlDbConnection : IDbConnection
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataAdapter _dataAdapter;
        private SqlCommand _command;
        public SqlDbConnection()
        {
            _dataAdapter = new SqlDataAdapter();
            _connection = new SqlConnection(_connectionString);
        }
        private SqlConnection OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
            {
                _connection.Open();
            }
            return _connection;
        }

        public void ExecuteCreateQuery(string query)
        {
            ExecuteNonQuery(query);
        }

        public void ExecuteInsertQuery(string query)
        {
            ExecuteNonQuery(query);
        }

        public DataTable ExecuteSelectQuery(string query)
        {
            _command = new SqlCommand();
            DataTable dt = null;
            DataSet ds = new DataSet();

            try
            {
                using (_command = new SqlCommand(query, OpenConnection()))
                {
                    _command.CommandTimeout = 0;
                    _dataAdapter = new SqlDataAdapter(_command);
                    ds = new DataSet();
                    _dataAdapter.Fill(ds);
                    dt = ds.Tables[0];
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error - ExecuteSelectQuery - Query: " + query + "\n Exception: " + ex.Message);
            }

            return dt;
        }

        private void ExecuteNonQuery(string query)
        {
            _command = new SqlCommand();
            try
            {
                using (_command = new SqlCommand(query, OpenConnection()))
                {
                    _command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error - ExecuteInsertQuery - Query: " + query + "\n Exception: " + ex.Message);
            }
        }
    }
}
