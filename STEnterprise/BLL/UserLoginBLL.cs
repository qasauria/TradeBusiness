using System;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;
using STEnterprise.DAL;
using STEnterprise.Models;

namespace STEnterprise.BLL
{
    public class UserLoginBLL
    {

        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;

        private void BuildModelForUserLogin(DbDataReader objDataReader, UserLogin objUserLogin)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "UserDetailId":
                        if (!Convert.IsDBNull(objDataReader["UserDetailId"]))
                        {
                            objUserLogin.UserDetailId = Convert.ToInt32(objDataReader["UserDetailId"].ToString());
                        }
                        break;
                    case "Username":
                        if (!Convert.IsDBNull(objDataReader["Username"]))
                        {
                            objUserLogin.Username = objDataReader["Username"].ToString();
                        }
                        break;
                    case "Password":
                        if (!Convert.IsDBNull(objDataReader["Password"]))
                        {
                            objUserLogin.Password = objDataReader["Password"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public UserLogin IsValid(UserLogin objUserLogin)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            UserLogin objLoginUser = null;
            objDbCommand.AddInParameter("Username", objUserLogin.Username);
            objDbCommand.AddInParameter("Password", SHA512PasswordGenerator(objUserLogin.Password));
            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspIsAuthenticated]", CommandType.StoredProcedure);
                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objLoginUser = new UserLogin();
                        this.BuildModelForUserLogin(objDbDataReader, objLoginUser);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                if (objDbDataReader != null)
                {
                    objDbDataReader.Close();
                }
                objDataAccess.Dispose(objDbCommand);
            }

            return objLoginUser;
        }

        private string SHA512PasswordGenerator(string strInput)
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();
            byte[] arrHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(strInput));
            StringBuilder sbHash = new StringBuilder();
            for (int i = 0; i < arrHash.Length; i++)
            {
                sbHash.Append(arrHash[i].ToString("x2"));
            }
            return sbHash.ToString();
        }
    }
}