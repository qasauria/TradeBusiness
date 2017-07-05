using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using STEnterprise.Areas.Accounts.Models;
using STEnterprise.DAL;

namespace STEnterprise.Areas.Accounts.BLL
{
    //ataur
    public class AccountHeadBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForAccountHead(DbDataReader objDataReader, AccountHead objAccountHead)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "AccountHeadId":
                        if (!Convert.IsDBNull(objDataReader["AccountHeadId"]))
                        {
                            objAccountHead.AccountHeadId = Convert.ToInt32(objDataReader["AccountHeadId"]);
                        }
                        break;
                    case "AccountCode":
                        if (!Convert.IsDBNull(objDataReader["AccountCode"]))
                        {
                            objAccountHead.AccountCode = objDataReader["AccountCode"].ToString();
                        }
                        break;
                    case "AccountName":
                        if (!Convert.IsDBNull(objDataReader["AccountName"]))
                        {
                            objAccountHead.AccountName = objDataReader["AccountName"].ToString();
                        }
                        break;
                    case "Description":
                        if (!Convert.IsDBNull(objDataReader["Description"]))
                        {
                            objAccountHead.Description = objDataReader["Description"].ToString();
                        }
                        break;
                    case "AccountType":
                        if (!Convert.IsDBNull(objDataReader["AccountType"]))
                        {
                            objAccountHead.AccountType = objDataReader["AccountType"].ToString();
                        }
                        break;
                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objAccountHead.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objAccountHead.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objAccountHead.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objAccountHead.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objAccountHead.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objAccountHead.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objAccountHead.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objAccountHead.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //get all account head list for index
        public List<AccountHead> GetAccountHeadList()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<AccountHead> objAccountHeadList = new List<AccountHead>();
            AccountHead objAccountHead;
            try
            {
                //objDbCommand.AddInParameter("CreatedBy", SessionUtility.TBSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetAccountHeadList", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objAccountHead = new AccountHead();
                        this.BuildModelForAccountHead(objDbDataReader, objAccountHead);
                        objAccountHeadList.Add(objAccountHead);
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
            return objAccountHeadList;
        }

        //Save account head
        public string CreateAccountHead(AccountHead objAccountHead)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("AccountCode", objAccountHead.AccountCode);
            objDbCommand.AddInParameter("AccountName", objAccountHead.AccountName);
            objDbCommand.AddInParameter("AccountType", objAccountHead.AccountType);
            objDbCommand.AddInParameter("Description", objAccountHead.Description);


            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateAccountHead", CommandType.StoredProcedure);

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

        //get account head detail by Id for edit and delete
        public AccountHead GetAccountHeadById(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<AccountHead> objAccountHeadList = new List<AccountHead>();
            AccountHead objAccountHead=new AccountHead();
            try
            {
                objDbCommand.AddInParameter("AccountHeadId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetAccountHeadById", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objAccountHead = new AccountHead();
                        this.BuildModelForAccountHead(objDbDataReader, objAccountHead);
                        objAccountHeadList.Add(objAccountHead);
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
            return objAccountHead;
        }

        // Update Accound Head 
        public string UpdateAccountHead(AccountHead objAccountHead)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("AccountHeadId", objAccountHead.AccountHeadId);
            objDbCommand.AddInParameter("AccountCode", objAccountHead.AccountCode);
            objDbCommand.AddInParameter("AccountName", objAccountHead.AccountName);
            objDbCommand.AddInParameter("AccountType", objAccountHead.AccountType);
            objDbCommand.AddInParameter("Description", objAccountHead.Description);
            objDbCommand.AddInParameter("IsActive", objAccountHead.IsActive);


            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateAccountHead", CommandType.StoredProcedure);

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

        //delete account head
        public string DeleteAccountHead(int id)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("AccountHeadId", id);
           
            //objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteAccountHead", CommandType.StoredProcedure);

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