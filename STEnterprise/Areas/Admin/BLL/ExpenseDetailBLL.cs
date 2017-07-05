using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using STEnterprise.Areas.Admin.Models;
using STEnterprise.DAL;

namespace STEnterprise.Areas.Admin.BLL
{
    public class ExpenseDetailBLL
    {

        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;

        private void BuildModelForExpenseDetail(DbDataReader objDataReader, ExpenseDetail objExpenseDetailInfo)
        {

            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "ExpenseDetailId":
                        if (!Convert.IsDBNull(objDataReader["ExpenseDetailId"]))
                        {
                            objExpenseDetailInfo.ExpenseDetailId = Convert.ToInt16(objDataReader["ExpenseDetailId"]);
                        }
                        break;
                    case "ExpenseType":
                        if (!Convert.IsDBNull(objDataReader["ExpenseType"]))
                        {
                            objExpenseDetailInfo.ExpenseType = objDataReader["ExpenseType"].ToString();
                        }
                        break;
                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objExpenseDetailInfo.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objExpenseDetailInfo.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objExpenseDetailInfo.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objExpenseDetailInfo.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objExpenseDetailInfo.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objExpenseDetailInfo.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objExpenseDetailInfo.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objExpenseDetailInfo.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        public List<ExpenseDetail> GetAllExpenseTypeInfo()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<ExpenseDetail> objExpenseDetailInfoList = new List<ExpenseDetail>();
            ExpenseDetail objExpenseDetailInfo;

            try
            {
                //objDbCommand.AddInParameter("CreatedBy", SessionUtility.TBSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetExpenseTypeList",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objExpenseDetailInfo = new ExpenseDetail();
                        this.BuildModelForExpenseDetail(objDbDataReader, objExpenseDetailInfo);
                        objExpenseDetailInfoList.Add(objExpenseDetailInfo);
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

            return objExpenseDetailInfoList;
        }

        public string SaveExpenseDetailInfo(ExpenseDetail objExpenseDetailInfo)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ExpenseType", objExpenseDetailInfo.ExpenseType);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateExpenseDetail", CommandType.StoredProcedure);

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

        public ExpenseDetail GetExpenseDetailInfo(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<ExpenseDetail> objExpenseDetailList = new List<ExpenseDetail>();

            ExpenseDetail objExpenseDetail = new ExpenseDetail();

            try
            {
                objDbCommand.AddInParameter("ExpenseDetailId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetExpenseDetailInfo]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objExpenseDetail = new ExpenseDetail();
                        this.BuildModelForExpenseDetail(objDbDataReader, objExpenseDetail);

                        objExpenseDetailList.Add(objExpenseDetail);


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

            return objExpenseDetail;
        }

        public string UpdateExpenseDetailInfo(ExpenseDetail objExpenseDetailInfo)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ExpenseDetailId", objExpenseDetailInfo.ExpenseDetailId);
            objDbCommand.AddInParameter("ExpenseType", objExpenseDetailInfo.ExpenseType);
           // objDbCommand.AddInParameter("IsActive", objCustomerInfo.IsActive);
            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateExpenseDetail", CommandType.StoredProcedure);

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

        public string DeleteExpenseDetailInfo(int id)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ExpenseDetailId", id);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteExpenseDetail", CommandType.StoredProcedure);

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

        public bool GetExpenseNameIsExist(string ExpenseName)
        {
            bool DesignationNameIsUse = false;
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            try
            {
                objDbCommand.AddInParameter("ExpenseType", ExpenseName);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetExpenseTypeIsExist", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    DesignationNameIsUse = true;
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
            return DesignationNameIsUse;
        }
    }
}