using System.Configuration;
using System.Data;
using System.Data.Common;

namespace STEnterprise.DAL
{
    public static class DbCommandExtension
    {
        private static string spPrefix = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static DbParameter GetParameter()
        {
            spPrefix = ConfigurationManager.AppSettings["SPPrefix"];
            DbProviderFactory objDbProviderFactory;
            string providerInvariantName = ConfigurationManager.AppSettings["ProviderType"];
            if (string.IsNullOrEmpty(providerInvariantName))
            {
                providerInvariantName = "System.Data.SqlClient";
            }
            // create the specific invariant provider
            objDbProviderFactory = DbProviderFactories.GetFactory(providerInvariantName);
            return objDbProviderFactory.CreateParameter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDbCommand"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        public static void AddInParameter(this DbCommand objDbCommand, string parameterName, object parameterValue)
        {
            DbParameter objDbParameter = GetParameter();
            objDbParameter.ParameterName = spPrefix + parameterName;
            objDbParameter.Value = parameterValue;
            objDbParameter.Direction = ParameterDirection.Input;
            objDbCommand.Parameters.Add(objDbParameter);
            //return objDbCommand;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDbCommand"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="parameterSize"></param>
        public static void AddInParameter(this DbCommand objDbCommand, string parameterName, object parameterValue, int parameterSize)
        {
            DbParameter objDbParameter = GetParameter();
            objDbParameter.ParameterName = spPrefix + parameterName;
            objDbParameter.Value = parameterValue;
            objDbParameter.Direction = ParameterDirection.Input;
            objDbParameter.DbType = DbType.String;
            objDbParameter.Size = parameterSize;
            objDbCommand.Parameters.Add(objDbParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDbCommand"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="dbType"></param>
        public static void AddInParameter(this DbCommand objDbCommand, string parameterName, object parameterValue, DbType dbType)
        {
            DbParameter objDbParameter = GetParameter();
            objDbParameter.ParameterName = spPrefix + parameterName;
            objDbParameter.Value = parameterValue;
            objDbParameter.Direction = ParameterDirection.Input;
            objDbParameter.DbType = dbType;
            objDbCommand.Parameters.Add(objDbParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDbCommand"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="dbType"></param>
        /// <param name="parameterSize"></param>
        public static void AddInParameter(this DbCommand objDbCommand, string parameterName, object parameterValue, DbType dbType, int parameterSize)
        {
            DbParameter objDbParameter = GetParameter();
            objDbParameter.ParameterName = spPrefix + parameterName;
            objDbParameter.Value = parameterValue;
            objDbParameter.Direction = ParameterDirection.Input;
            objDbParameter.DbType = dbType;
            objDbParameter.Size = parameterSize;
            objDbCommand.Parameters.Add(objDbParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDbCommand"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="parameterSize"></param>
        public static void AddOutParameter(this DbCommand objDbCommand, string parameterName, object parameterValue, int parameterSize)
        {
            DbParameter objDbParameter = GetParameter();
            objDbParameter.ParameterName = spPrefix + parameterName;
            objDbParameter.Value = parameterValue;
            objDbParameter.Direction = ParameterDirection.Output;
            objDbParameter.DbType = DbType.String;
            objDbParameter.Size = parameterSize;
            objDbCommand.Parameters.Add(objDbParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDbCommand"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="dbType"></param>
        /// <param name="parameterSize"></param>
        public static void AddOutParameter(this DbCommand objDbCommand, string parameterName, object parameterValue, DbType dbType, int parameterSize)
        {
            DbParameter objDbParameter = GetParameter();
            objDbParameter.ParameterName = spPrefix + parameterName;
            objDbParameter.Value = parameterValue;
            objDbParameter.Direction = ParameterDirection.Output;
            objDbParameter.DbType = dbType;
            objDbParameter.Size = parameterSize;
            objDbCommand.Parameters.Add(objDbParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDbCommand"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="parameterDirection"></param>
        public static void AddInOutParameter(this DbCommand objDbCommand, string parameterName, object parameterValue, ParameterDirection parameterDirection)
        {
            DbParameter objDbParameter = GetParameter();
            objDbParameter.ParameterName = spPrefix + parameterName;
            objDbParameter.Value = parameterValue;
            objDbParameter.Direction = parameterDirection;
            objDbParameter.DbType = DbType.String;
            objDbParameter.Size = 256;
            objDbCommand.Parameters.Add(objDbParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDbCommand"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="parameterDirection"></param>
        /// <param name="parameterSize"></param>
        public static void AddInOutParameter(this DbCommand objDbCommand, string parameterName, object parameterValue, ParameterDirection parameterDirection, int parameterSize)
        {
            DbParameter objDbParameter = GetParameter();
            objDbParameter.ParameterName = spPrefix + parameterName;
            objDbParameter.Value = parameterValue;
            objDbParameter.Direction = parameterDirection;
            objDbParameter.DbType = DbType.String;
            objDbParameter.Size = parameterSize;
            objDbCommand.Parameters.Add(objDbParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDbCommand"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="parameterDirection"></param>
        /// <param name="dbType"></param>
        /// <param name="parameterSize"></param>
        public static void AddInOutParameter(this DbCommand objDbCommand, string parameterName, object parameterValue, ParameterDirection parameterDirection, DbType dbType, int parameterSize)
        {
            DbParameter objDbParameter = GetParameter();
            objDbParameter.ParameterName = spPrefix + parameterName;
            objDbParameter.Value = parameterValue;
            objDbParameter.Direction = parameterDirection;
            objDbParameter.DbType = dbType;
            objDbParameter.Size = parameterSize;
            objDbCommand.Parameters.Add(objDbParameter);
        }
    }
}