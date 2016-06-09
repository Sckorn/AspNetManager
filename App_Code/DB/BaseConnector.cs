using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Diagnostics;
using System.Data.Common;
using FootballManager;
using Npgsql;

/// <summary>
/// Summary description for PGConnector
/// </summary>
/// 
namespace FootballManager.Database
{
    public class BaseConnector
    {
        protected string _connectionString = String.Empty;

        private Exception _lastException = null;
        private Stopwatch sw = new Stopwatch();

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
            }
        }

        public string LastExceptionMessage
        {
            get
            {
                if (_lastException != null)
                    return _lastException.Message;
                else return string.Empty;
            }
        }

        public string LastExceptionStackTrace
        {
            get
            {
                if (_lastException != null)
                    return _lastException.StackTrace;
                else return string.Empty;
            }
        }

        public double TotalQueryTime
        {
            get
            {
                return sw.Elapsed.TotalMilliseconds;
            }
        }

        public BaseConnector()
        {
            //_connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MembershipConnection"].ToString();
        }

        public BaseConnector(string connString)
        {
            _connectionString = connString;
        }

        public DataSet ExecuteQuery(string procName, string[] parameters)
        {
            sw.Reset();
            sw.Start();

            DbProviderFactory factory = DbProviderFactories.GetFactory(Global.DefaultFactory);
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = _connectionString;
            DbCommand cmd = factory.CreateCommand();


            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            try
            {
                connection.Open();
                cmd.CommandText = ConstructQueryString(procName, parameters);
                cmd.Prepare();

                DbDataAdapter adapter = factory.CreateDataAdapter();

                DataSet ds = new DataSet();
                adapter.SelectCommand = cmd;

                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                _lastException = ex;
                Logger.WriteToLog(String.Format("Ошибка выполнения запроса: {0}", ex.Message));
                return null;
            }
            finally
            {
                connection.Close();
                sw.Stop();
            }            
        }

        private string ConstructQueryString(string name, string[] parameters)
        {
            try
            {
                string param = String.Empty;

                foreach (string str in parameters)
                {
                    if (!param.Equals(string.Empty))
                    {
                        param += ", ";
                    }
                    param += "'" + str + "'";
                }
                string query = String.Format("select * from {0}({1});", name, param);
                Logger.WriteToLog(String.Format("Запрос: {0}", query));
                return query;
            }
            catch (Exception ex)
            {
                _lastException = ex;
                Logger.WriteToLog(String.Format("Ошибка формирования SQL: {0}", ex.Message));
                return null;
            }
        }
    }
}