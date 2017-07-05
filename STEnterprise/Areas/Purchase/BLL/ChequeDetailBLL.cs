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
    public class ChequeDetailBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForBuildModelForChequeDetail(DbDataReader objDataReader, ChequeDetail objChequeDetail)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {

                    case "ChequeDetailId":
                        if (!Convert.IsDBNull(objDataReader["ChequeDetailId"]))
                        {
                            objChequeDetail.ChequeDetailId = Convert.ToInt32(objDataReader["ChequeDetailId"]);
                        }
                        break;
                    case "ChequeIssuedBy":
                        if (!Convert.IsDBNull(objDataReader["ChequeIssuedBy"]))
                        {
                            objChequeDetail.ChequeIssuedBy =Convert.ToByte(objDataReader["ChequeIssuedBy"].ToString());
                        }
                        break;
                    case "ChequeIssuedByShow":
                        if (!Convert.IsDBNull(objDataReader["ChequeIssuedByShow"]))
                        {
                            objChequeDetail.ChequeIssuedByShow = objDataReader["ChequeIssuedByShow"].ToString();
                        }
                        break;
                    
                    case "OwnerId":
                        if (!Convert.IsDBNull(objDataReader["OwnerId"]))
                        {
                            objChequeDetail.OwnerId = Convert.ToByte(objDataReader["OwnerId"]);
                        }
                        break;
                    case "OwnerName":
                        if (!Convert.IsDBNull(objDataReader["OwnerName"]))
                        {
                            objChequeDetail.OwnerName = objDataReader["OwnerName"].ToString();
                        }
                        break;
                    case "ChequeNumber":
                        if (!Convert.IsDBNull(objDataReader["ChequeNumber"]))
                        {
                            objChequeDetail.ChequeNumber = objDataReader["ChequeNumber"].ToString();
                        }
                        break;
                    case "BankDetailId":
                        if (!Convert.IsDBNull(objDataReader["BankDetailId"]))
                        {
                            objChequeDetail.BankDetailId = Convert.ToByte(objDataReader["BankDetailId"]);
                        }
                        break;
                    case "BankName":
                        if (!Convert.IsDBNull(objDataReader["BankName"]))
                        {
                            objChequeDetail.BankName = objDataReader["BankName"].ToString();
                        }
                        break;
                    case "ChequeAmount":
                        if (!Convert.IsDBNull(objDataReader["ChequeAmount"]))
                        {
                            objChequeDetail.ChequeAmount = Convert.ToDecimal(objDataReader["ChequeAmount"]);
                        }
                        break;

                    case "ChequeIssueDate":
                        if (!Convert.IsDBNull(objDataReader["ChequeIssueDate"]))
                        {
                            objChequeDetail.ChequeIssueDateShow = string.Format("{0:dd/MM/yyyy}", objDataReader["ChequeIssueDate"]);
                        }
                        break;
                    case "ChequeSubmitDate":
                        if (!Convert.IsDBNull(objDataReader["ChequeSubmitDate"]))
                        {
                            objChequeDetail.ChequeSubmitDateShow = string.Format("{0:dd/MM/yyyy}", objDataReader["ChequeSubmitDate"]);
                        }
                        break;
                    case "ChequeMaturedDate":
                        if (!Convert.IsDBNull(objDataReader["ChequeMaturedDate"]))
                        {
                            objChequeDetail.ChequeMaturedDateShow = string.Format("{0:dd/MM/yyyy}", objDataReader["ChequeMaturedDate"]);
                        }
                        break;
                    case "ChequeStatus":
                        if (!Convert.IsDBNull(objDataReader["ChequeStatus"]))
                        {
                            objChequeDetail.ChequeStatusShow =objDataReader["ChequeStatus"].ToString();
                        }
                        break;
                    //case "ChequeStatusShow":
                    //    if (!Convert.IsDBNull(objDataReader["ChequeStatus"]))
                    //    {
                    //        objChequeDetail.ChequeStatusShow = objDataReader["ChequeStatus"].ToString();
                    //    }
                    //    break;

                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objChequeDetail.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objChequeDetail.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objChequeDetail.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objChequeDetail.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objChequeDetail.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objChequeDetail.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objChequeDetail.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objChequeDetail.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public List<ChequeDetail> GetAllChequeDetail()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<ChequeDetail> objChequeDetailList = new List<ChequeDetail>();

            ChequeDetail objChequeDetail = new ChequeDetail();

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetAllChequeDetail]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objChequeDetail = new ChequeDetail();
                        this.BuildModelForBuildModelForChequeDetail(objDbDataReader, objChequeDetail);
                        objChequeDetailList.Add(objChequeDetail);
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

            return objChequeDetailList;
        }

        public List<ChequeDetail> GetOwnerNameByChequeId(int chequeIssuedBy)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<ChequeDetail> objChequeDetailList = new List<ChequeDetail>();

            ChequeDetail objChequeDetail = new ChequeDetail();

            try
            {
                objDbCommand.AddInParameter("ChequeIssuedBy", chequeIssuedBy);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetOwnerNameByChequeId]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objChequeDetail = new ChequeDetail();
                        this.BuildModelForBuildModelForChequeDetail(objDbDataReader, objChequeDetail);
                        objChequeDetailList.Add(objChequeDetail);
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

            return objChequeDetailList;
        }

        public List<BankDetail> GetBankName()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<BankDetail> objBankDetailList = new List<BankDetail>();

            BankDetail objBankDetail = new BankDetail();

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetBankName]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objBankDetail = new BankDetail();
                        objBankDetail.BankDetailId = Convert.ToByte(objDbDataReader["BankDetailId"].ToString());
                        objBankDetail.BankName = objDbDataReader["BankName"].ToString();
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

        public string SaveChequeDetailInfo(ChequeDetail objChequeDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ChequeIssuedBy", objChequeDetail.ChequeIssuedBy);
            objDbCommand.AddInParameter("OwnerId", objChequeDetail.OwnerId);
            objDbCommand.AddInParameter("ChequeNumber", objChequeDetail.ChequeNumber);
            objDbCommand.AddInParameter("BankDetailId", objChequeDetail.BankDetailId);
            objDbCommand.AddInParameter("ChequeAmount", objChequeDetail.ChequeAmount);
            objDbCommand.AddInParameter("ChequeIssueDate", objChequeDetail.ChequeIssueDate);
            objDbCommand.AddInParameter("ChequeSubmitDate", objChequeDetail.ChequeSubmitDate);
            objDbCommand.AddInParameter("ChequeMaturedDate", objChequeDetail.ChequeMaturedDate);
            objDbCommand.AddInParameter("ChequeStatus", objChequeDetail.ChequeStatus);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateChequeDetail", CommandType.StoredProcedure);

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

        public ChequeDetail GetChequeDetail(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<ChequeDetail> objChequeDetailList = new List<ChequeDetail>();

            ChequeDetail objChequeDetail = new ChequeDetail();

            try
            {
                objDbCommand.AddInParameter("ChequeDetailId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetChequeDetailById]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objChequeDetail = new ChequeDetail();
                        this.BuildModelForBuildModelForChequeDetail(objDbDataReader, objChequeDetail);
                        objChequeDetailList.Add(objChequeDetail);
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

            return objChequeDetail;
        }

        public string UpdateChequeDetail(ChequeDetail objChequeDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ChequeDetailId", objChequeDetail.ChequeDetailId);
            objDbCommand.AddInParameter("ChequeNumber", objChequeDetail.ChequeNumber);
            objDbCommand.AddInParameter("BankDetailId", objChequeDetail.BankName);
            objDbCommand.AddInParameter("ChequeAmount", objChequeDetail.ChequeAmount);
            objDbCommand.AddInParameter("ChequeIssueDate", objChequeDetail.ChequeIssueDateShow);
            objDbCommand.AddInParameter("ChequeSubmitDate", objChequeDetail.ChequeSubmitDateShow);
            objDbCommand.AddInParameter("ChequeMaturedDate", objChequeDetail.ChequeMaturedDateShow);
            objDbCommand.AddInParameter("ChequeStatus", objChequeDetail.ChequeStatusShow);
            objDbCommand.AddInParameter("IsActive", objChequeDetail.IsActive);
            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateChequeDetail", CommandType.StoredProcedure);

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

        public string DeleteChequeDetail(int id)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ChequeDetailId", id);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteChequeDetailById", CommandType.StoredProcedure);

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