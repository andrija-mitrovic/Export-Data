using Advantage.Data.Provider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReportApp.Core.Data
{
    public class AdsDbConnection : IDbConnection
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["Dbf"].ConnectionString;
        private AdsConnection _connection;
        private AdsDataAdapter _dataAdapter;
        private AdsCommand _command;

        public AdsDbConnection()
        {
            _dataAdapter = new AdsDataAdapter();
            _connection = new AdsConnection(_connectionString);
        }

        private AdsConnection OpenConnection()
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
            _command = new AdsCommand();
            DataTable dt = null;
            DataSet ds = new DataSet();

            try
            {
                using (_command = new AdsCommand(query, OpenConnection()))
                {
                    _command.CommandTimeout = 0;
                    _dataAdapter = new AdsDataAdapter(_command);
                    ds = new DataSet();
                    _dataAdapter.Fill(ds);
                    dt = ds.Tables[0];
                }
            }
            catch (AdsException ex)
            {
                throw new Exception("Error - ExecuteSelectQuery - Query: " + query + "\n Exception: " + ex.Message);
            }

            return dt;
        }

        private void ExecuteNonQuery(string query)
        {
            _command = new AdsCommand();

            try
            {
                using (_command = new AdsCommand(query, OpenConnection()))
                {
                    _command.ExecuteNonQuery();
                }
            }
            catch (AdsException ex)
            {
                throw new Exception("Error - ExecuteInsertQuery - Query: " + query + "\n Exception: " + ex.Message);
            }
        }
    }
}
