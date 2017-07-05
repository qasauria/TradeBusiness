using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using STEnterprise.Areas.Admin.Models;
using STEnterprise.DAL;
using STEnterprise.Models;

namespace STEnterprise.BLL
{
    public class UserDetailPartialBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForUserDetailPartial(DbDataReader objDataReader, UserDetailPartial objUserDetailPartial)
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
                            objUserDetailPartial.UserDetailId = Convert.ToByte(objDataReader["UserDetailId"]);
                        }
                        break;
                    case "Username":
                        if (!Convert.IsDBNull(objDataReader["Username"]))
                        {
                            objUserDetailPartial.Username = objDataReader["Username"].ToString();
                        }
                        break;
                    //case "BranchId":
                    //    if (!Convert.IsDBNull(objDataReader["BranchId"]))
                    //    {
                    //        objUserInfoPartial.BranchId = Convert.ToByte(objDataReader["BranchId"].ToString());
                    //    }
                    //    break;
                    //case "BranchName":
                    //    if (!Convert.IsDBNull(objDataReader["BranchName"]))
                    //    {
                    //        objUserInfoPartial.BranchName = objDataReader["BranchName"].ToString();
                    //    }
                    //    break;

                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objUserDetailPartial.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objUserDetailPartial.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objUserDetailPartial.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objUserDetailPartial.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objUserDetailPartial.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objUserDetailPartial.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objUserDetailPartial.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //Get UserInfo by UserId for Edit
        public UserDetailPartial GetUserInfo(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            UserDetailPartial objUserDetailPartial=new UserDetailPartial();
            List<UserDetailPartial> objUserInfoPartialList = new List<UserDetailPartial>();

            try
            {
                objDbCommand.AddInParameter("UserDetailId", id);
                //objDbCommand.AddInParameter("AdminUserId", SessionUtility.STSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetUserInfo]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objUserDetailPartial=new UserDetailPartial();
                        this.BuildModelForUserDetailPartial(objDbDataReader, objUserDetailPartial);

                        objUserInfoPartialList.Add(objUserDetailPartial);
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

            return objUserDetailPartial;
        }

        // Update Specific UserInfo
        public string UpdateUserInfo(UserDetailPartial objUserDetailPartial)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);

            objDbCommand.AddInParameter("UserDetailId", objUserDetailPartial.UserDetailId);
            objDbCommand.AddInParameter("Username", objUserDetailPartial.Username); 
            objDbCommand.AddInParameter("IsActive", objUserDetailPartial.IsActive);
            //updated by which Admin!
            objDbCommand.AddInParameter("UpdatedByAdminUserId", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateUserInfo", CommandType.StoredProcedure);

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

        //Delete Specific UserInfo
        public string DeleteUserInfo(int id)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("UserDetailId", id);
            //objDbCommand.AddInParameter("CreatedBy", objAdminUser.CreatedBy);         

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteUserInfo", CommandType.StoredProcedure);

                if (noRowCount > 0)
                {
                    objDbCommand.Transaction.Commit();
                    return "Delete Successfully";
                }
                else
                {
                    objDbCommand.Transaction.Rollback();
                    return "Delete Failed";
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
    }
}