using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;
using STEnterprise.DAL;
using STEnterprise.Models;

namespace STEnterprise.BLL
{
    //ataur
    public class UserDetailBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForUserDetail(DbDataReader objDataReader, UserDetail objUserDetail)
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
                            objUserDetail.UserDetailId = Convert.ToByte(objDataReader["UserDetailId"]);
                        }
                        break;
                    case "Username":
                        if (!Convert.IsDBNull(objDataReader["Username"]))
                        {
                            objUserDetail.Username = objDataReader["Username"].ToString();
                        }
                        break;
                    case "Password":
                        if (!Convert.IsDBNull(objDataReader["Password"]))
                        {
                            objUserDetail.Password = objDataReader["Password"].ToString();
                        }
                        break;
                    //case "BranchId":
                    //    if (!Convert.IsDBNull(objDataReader["BranchId"]))
                    //    {
                    //        objUserInfo.BranchId = Convert.ToByte(objDataReader["BranchId"].ToString());
                    //    }
                    //    break;
                    //case "BranchName":
                    //    if (!Convert.IsDBNull(objDataReader["BranchName"]))
                    //    {
                    //        objUserInfo.BranchName = objDataReader["BranchName"].ToString();
                    //    }
                    //    break;

                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objUserDetail.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objUserDetail.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objUserDetail.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objUserDetail.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objUserDetail.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objUserDetail.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objUserDetail.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objUserDetail.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        // Get All User Information from Database 
        public List<UserDetail> GetAllUserInfo()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<UserDetail> objUserInfoList = new List<UserDetail>();
            UserDetail objUserDetail;

            try
            {
                objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetAllUserInfoList", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objUserDetail=new UserDetail();
                        this.BuildModelForUserDetail(objDbDataReader, objUserDetail);
                        objUserInfoList.Add(objUserDetail);
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

            return objUserInfoList;
        }

        // Create User Information
        public string CreateUserInfo(UserDetail objUserDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("Username", objUserDetail.Username);
            objDbCommand.AddInParameter("Password", SHA512PasswordGenerator(objUserDetail.Password));

            //objDbCommand.AddInParameter("IsActive", objUserInfo.IsActive);
            objDbCommand.AddInParameter("AdminUserId", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateUserInfo", CommandType.StoredProcedure);

                if (noRowCount > 0)
                {
                    objDbCommand.Transaction.Commit();
                    return "Save Successfully";
                }
                else
                {
                    objDbCommand.Transaction.Rollback();
                    return "Save Failed";
                }
            }
            catch (Exception ex)
            {
                objDbCommand.Transaction.Rollback();
                throw new Exception("Database Error Occured", ex);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }
        }


        //Encrypt Password using SHA512 Algorithm
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

        //  Existing User Name Check
        // created by shovon
        public bool GetUserNameIsExist(string userName)
        {
            bool userNameIsUse = false;
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            try
            {
                objDbCommand.AddInParameter("Username", userName);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetUserNameIsExist", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    userNameIsUse = true;
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
            return userNameIsUse;
        }
    }
}