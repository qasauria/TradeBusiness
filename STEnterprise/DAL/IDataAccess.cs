using System;
using System.Data;
using System.Data.Common;

namespace STEnterprise.DAL
{
    public interface IDataAccess : IDisposable
    {
        DbCommand GetCommand(bool isTransaction, IsolationLevel isolationLevel);
        DbCommand GetCommand(bool isTransaction, IsolationLevel isolationLevel, string textOrSpName, CommandType commandType);
        DbCommand GetCommand(string connectionString, bool isTransaction, IsolationLevel isolationLevel);

        int ExecuteNonQuery(DbCommand objDbCommand, string textOrSpName, CommandType commandType);
        int ExecuteNonQuery(DbCommand objDbCommand);
        DbDataReader ExecuteReader(DbCommand objDbCommand, string textOrSpName, CommandType commandType);
        DbDataReader ExecuteReader(DbCommand objDbCommand);
        DataTable ExecuteTable(DbCommand objDbCommand, string textOrSpName, CommandType commandType);
        DataTable ExecuteTable(DbCommand objDbCommand);
        DataSet ExecuteDataSet(DbCommand objDbCommand, string textOrSpName, CommandType commandType);
        object ExecuteScalar(DbCommand objDbCommand, string textOrSpName, CommandType commandType);
        object ExecuteScalar(DbCommand objDbCommand);

        void Dispose(DbCommand objDbCommand);
    }
}