using STEnterprise.Areas.Purchase.Models;
using STEnterprise.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Purchase.BLL
{
    public class BankDetailBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;

        private void BuildModelForBuildModelForBankDetail(DbDataReader objDataReader, BankDetail objBankDetail)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "BankDetailId":
                        if (!Convert.IsDBNull(objDataReader["BankDetailId"]))
                        {
                            objBankDetail.BankDetailId = Convert.ToByte(objDataReader["BankDetailId"]);
                        }
                        break;
                    case "BankName":
                        if (!Convert.IsDBNull(objDataReader["BankName"]))
                        {
                            objBankDetail.BankName = objDataReader["BankName"].ToString();
                        }
                        break;
                    case "AccountNumber":
                        if (!Convert.IsDBNull(objDataReader["AccountNumber"]))
                        {
                            objBankDetail.AccountNumber = objDataReader["AccountNumber"].ToString();
                        }
                        break;
                    case "BranchName":
                        if (!Convert.IsDBNull(objDataReader["BranchName"]))
                        {
                            objBankDetail.BranchName = objDataReader["BranchName"].ToString();
                        }
                        break;
                    case "Address":
                        if (!Convert.IsDBNull(objDataReader["Address"]))
                        {
                            objBankDetail.Address = objDataReader["Address"].ToString();
                        }
                        break;
                    case "ContactPerson":
                        if (!Convert.IsDBNull(objDataReader["ContactPerson"]))
                        {
                            objBankDetail.ContactPerson = objDataReader["ContactPerson"].ToString();
                        }
                        break;

                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objBankDetail.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objBankDetail.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objBankDetail.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objBankDetail.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objBankDetail.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objBankDetail.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objBankDetail.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objBankDetail.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        public List<BankDetail> GetAllBankDetail()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<BankDetail> objBankDetailList = new List<BankDetail>();
            BankDetail objBankDetail;

            try
            {
                //objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetBankDetailList", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objBankDetail = new BankDetail();
                        this.BuildModelForBuildModelForBankDetail(objDbDataReader, objBankDetail);
                        objBankDetailList.Add(objBankDetail);
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

            return objBankDetailList;
        }
        public string SaveBankDetail(BankDetail objBankDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("BankName", objBankDetail.BankName);
            objDbCommand.AddInParameter("BranchName", objBankDetail.BranchName);
            objDbCommand.AddInParameter("AccountNumber", objBankDetail.AccountNumber);
            objDbCommand.AddInParameter("Address", objBankDetail.Address);
            objDbCommand.AddInParameter("ContactPerson", objBankDetail.ContactPerson);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateBankDetail", CommandType.StoredProcedure);

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

        public bool GetBankNameIsExist(string bankName)
        {
            bool SupplierNameIsUse = false;
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            try
            {
                objDbCommand.AddInParameter("BankName", bankName);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetBankNameIsExist", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    SupplierNameIsUse = true;
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
            return SupplierNameIsUse;
        }

        public BankDetail GetAllBankDetailById(int bankDetailId)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<BankDetail> objBankDetailList = new List<BankDetail>();

            BankDetail objBankDetail = new BankDetail();

            try
            {
                objDbCommand.AddInParameter("BankDetailId", bankDetailId);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetAllBankDetailById]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objBankDetail = new BankDetail();
                        this.BuildModelForBuildModelForBankDetail(objDbDataReader, objBankDetail);
                        objBankDetailList.Add(objBankDetail);
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

            return objBankDetail;
        }

        public string UpdateBankDetail(BankDetail objBankDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("BankDetailId", objBankDetail.BankDetailId);
            objDbCommand.AddInParameter("BankName", objBankDetail.BankName);
            objDbCommand.AddInParameter("BranchName", objBankDetail.BranchName);
            objDbCommand.AddInParameter("AccountNumber", objBankDetail.AccountNumber);
            objDbCommand.AddInParameter("Address", objBankDetail.Address);
            objDbCommand.AddInParameter("ContactPerson", objBankDetail.ContactPerson);
            objDbCommand.AddInParameter("IsActive", objBankDetail.IsActive);
            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateBankDetail", CommandType.StoredProcedure);

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

        public string DeleteBankDetail(int bankDetailId)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("BankDetailId", bankDetailId);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteBankDetailById", CommandType.StoredProcedure);

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