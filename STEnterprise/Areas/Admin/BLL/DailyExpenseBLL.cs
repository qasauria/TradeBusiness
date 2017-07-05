using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using STEnterprise.Areas.Admin.Models;
using STEnterprise.Areas.Sale.Models;
using STEnterprise.DAL;

namespace STEnterprise.Areas.Admin.BLL
{
    // created by shovon
    public class DailyExpenseBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;

        private void BuildModelForDailyExpense(DbDataReader objDataReader, DailyExpense objDailyExpense)
        {

            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "DailyExpenseId":
                        if (!Convert.IsDBNull(objDataReader["DailyExpenseId"]))
                        {
                            objDailyExpense.DailyExpenseId = Convert.ToInt32(objDataReader["DailyExpenseId"]);
                        }
                        break;
                    case "ExpenseDetailId":
                        if (!Convert.IsDBNull(objDataReader["ExpenseDetailId"]))
                        {
                            objDailyExpense.ExpenseDetailId = Convert.ToInt16(objDataReader["ExpenseDetailId"].ToString());
                        }
                        break;
                    case "ExpenseType":
                        if (!Convert.IsDBNull(objDataReader["ExpenseType"]))
                        {
                            objDailyExpense.ExpenseType = objDataReader["ExpenseType"].ToString();
                        }
                        break;
                    case "TotalAmount":
                        if (!Convert.IsDBNull(objDataReader["TotalAmount"]))
                        {
                            objDailyExpense.TotalAmount = Convert.ToDouble(objDataReader["TotalAmount"].ToString());
                        }
                        break;
                    case "PaidAmount":
                        if (!Convert.IsDBNull(objDataReader["PaidAmount"]))
                        {
                            objDailyExpense.PaidAmount = Convert.ToDouble(objDataReader["PaidAmount"].ToString());
                        }
                        break;
                    case "DueAmount":
                        if (!Convert.IsDBNull(objDataReader["DueAmount"]))
                        {
                            objDailyExpense.DueAmount = Convert.ToDouble(objDataReader["DueAmount"].ToString());
                        }
                        break;
                    case "Date":
                        if (!Convert.IsDBNull(objDataReader["Date"]))
                        {
                            objDailyExpense.Date = Convert.ToDateTime(objDataReader["Date"].ToString());
                        }
                        break;
                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objDailyExpense.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objDailyExpense.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objDailyExpense.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objDailyExpense.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objDailyExpense.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objDailyExpense.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objDailyExpense.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objDailyExpense.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public List<DailyExpense> GetAllDailyExpenseInfo()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<DailyExpense> objDailyExpenseList = new List<DailyExpense>();
            DailyExpense objDailyExpense;

            try
            {
                //objDbCommand.AddInParameter("CreatedBy", SessionUtility.TBSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetDailyExpenseList",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objDailyExpense = new DailyExpense();
                        this.BuildModelForDailyExpense(objDbDataReader, objDailyExpense);
                        objDailyExpenseList.Add(objDailyExpense);
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

            return objDailyExpenseList;
        }

        public string SaveDailyExpenseInfo(DailyExpense objDailyExpenseInfo)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ExpenseDetailId", objDailyExpenseInfo.ExpenseDetailId);
            objDbCommand.AddInParameter("TotalAmount", objDailyExpenseInfo.TotalAmount);
            objDbCommand.AddInParameter("PaidAmount", objDailyExpenseInfo.PaidAmount);
            objDbCommand.AddInParameter("DueAmount", objDailyExpenseInfo.DueAmount);
            objDbCommand.AddInParameter("Date", objDailyExpenseInfo.Date);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateDailyExpense", CommandType.StoredProcedure);

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

        public DailyExpense GetDailyExpenseInfo(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<DailyExpense> objDailyExpenseList = new List<DailyExpense>();

            DailyExpense objDailyExpense = new DailyExpense();

            try
            {
                objDbCommand.AddInParameter("DailyExpenseId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetDailyExpenseInfo]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objDailyExpense = new DailyExpense();
                        this.BuildModelForDailyExpense(objDbDataReader, objDailyExpense);

                        objDailyExpenseList.Add(objDailyExpense);


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

            return objDailyExpense;
        }

        public string UpdateDailyExpenseInfo(DailyExpense objDailyExpenseInfo)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("DailyExpenseId", objDailyExpenseInfo.DailyExpenseId);
            objDbCommand.AddInParameter("ExpenseDetailId", objDailyExpenseInfo.ExpenseDetailId);
            objDbCommand.AddInParameter("TotalAmount", objDailyExpenseInfo.TotalAmount);
            objDbCommand.AddInParameter("PaidAmount", objDailyExpenseInfo.PaidAmount);
            objDbCommand.AddInParameter("DueAmount", objDailyExpenseInfo.DueAmount);
            objDbCommand.AddInParameter("Date", objDailyExpenseInfo.Date);
            objDbCommand.AddInParameter("IsActive", objDailyExpenseInfo.IsActive);
            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateDailyExpense", CommandType.StoredProcedure);

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

        public string DeleteDailyExpenseInfo(int id)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("DailyExpenseId", id);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteDailyExpense", CommandType.StoredProcedure);

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