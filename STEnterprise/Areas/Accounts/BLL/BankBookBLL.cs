using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using STEnterprise.DAL;
using STEnterprise.Areas.Accounts.Models;

namespace STEnterprise.Areas.Accounts.BLL
{
    public class BankBookBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForBuildModelForBankBook(DbDataReader objDataReader, BankBookModel objBankBookModel)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {

                    case "BankBookId":
                        if (!Convert.IsDBNull(objDataReader["BankBookId"]))
                        {
                            objBankBookModel.BankBookId = Convert.ToInt32(objDataReader["BankBookId"]);
                        }
                        break;
                    case "TransationDate":
                        if (!Convert.IsDBNull(objDataReader["TransationDate"]))
                        {
                            objBankBookModel.TransactionDate = Convert.ToDateTime(objDataReader["TransationDate"].ToString());
                        }
                        break;
                    case "BankName":
                        if (!Convert.IsDBNull(objDataReader["BankName"]))
                        {
                            objBankBookModel.BankName =objDataReader["BankName"].ToString();
                        }
                        break;
                    case "BankDetailId":
                        if (!Convert.IsDBNull(objDataReader["BankDetailId"]))
                        {
                            objBankBookModel.BankDetailId = Convert.ToInt16(objDataReader["BankDetailId"].ToString());
                        }
                        break;
                    case "AccountNumber":
                        if (!Convert.IsDBNull(objDataReader["AccountNumber"]))
                        {
                            objBankBookModel.AccountNumber = objDataReader["AccountNumber"].ToString();
                        }
                        break;
                    
                    case "BankEntryType":
                        if (!Convert.IsDBNull(objDataReader["BankEntryType"]))
                        {
                            objBankBookModel.BankEntryType = objDataReader["BankEntryType"].ToString();
                        }
                        break;
                    case "DebitAmount":
                        if (!Convert.IsDBNull(objDataReader["DebitAmount"]))
                        {
                            objBankBookModel.DebitAmount = Convert.ToDecimal(objDataReader["DebitAmount"]);
                        }
                        break;
                    case "CreditAmount":
                        if (!Convert.IsDBNull(objDataReader["CreditAmount"]))
                        {
                            objBankBookModel.CreditAmount = Convert.ToDecimal(objDataReader["CreditAmount"].ToString());
                        }
                        break;                   
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objBankBookModel.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        public List<BankBookModel> GetBankName()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<BankBookModel> objBankBookModelList = new List<BankBookModel>();

            BankBookModel objBankBookModel = new BankBookModel();

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetBankNameForBankBook]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objBankBookModel = new BankBookModel();
                        this.BuildModelForBuildModelForBankBook(objDbDataReader, objBankBookModel);
                        objBankBookModelList.Add(objBankBookModel);
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

            return objBankBookModelList;
        }

        public BankBookModel GetAccountNumberByBankName(int? BankName)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<BankBookModel> objBankBookModelList = new List<BankBookModel>();
            BankBookModel objBankBookModel = new BankBookModel();

            try
            {
                objDbCommand.AddInParameter("BankName", BankName);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetAccountNumberByBankName]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objBankBookModel = new BankBookModel();
                        this.BuildModelForBuildModelForBankBook(objDbDataReader, objBankBookModel);
                        objBankBookModelList.Add(objBankBookModel);
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

            return objBankBookModel;
        }

        public string SaveBankBook(List<BankBookModel> objbank)
        {
            int noOfAffacted = 0;
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            string UniqNumber = string.Empty;
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString().PadLeft(2, '0');
            string day = DateTime.Now.Day.ToString().PadLeft(2, '0');
            objDbCommand.AddInParameter("Year", year);
            objDbCommand.AddInParameter("Month", month);
            objDbCommand.AddInParameter("Day", day);

            objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetUniqueNumberExistForBankBook]", CommandType.StoredProcedure);
            string a = string.Empty;
            if (objDbDataReader.HasRows)
            {
                while (objDbDataReader.Read())
                {
                    a = Convert.ToString(objDbDataReader["uniqCode"]).PadLeft(3, '0');
                    if (a == "000")
                    {
                        a = string.Format("{0:000}", 1);
                        UniqNumber = string.Concat(year, month, day, a);
                    }
                    else
                    {
                        UniqNumber = string.Concat(year, month, day, Convert.ToString(objDbDataReader["uniqCode"]).PadLeft(3, '0'));
                    }
                }
            }

            try
            {
                objDataAccess = DataAccess.NewDataAccess();
                foreach (BankBookModel objBankBook in objbank)
                {
                    objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
                    objDbCommand.AddInParameter("TransactionDate", objBankBook.TransactionDate);
                    objDbCommand.AddInParameter("BankName", objBankBook.BankDetailId);
                    objDbCommand.AddInParameter("AccountNumber", objBankBook.AccountNumber);
                    objDbCommand.AddInParameter("BankEntryType", objBankBook.BankEntryType);
                    objDbCommand.AddInParameter("DebitAmount", objBankBook.DebitAmount);
                    objDbCommand.AddInParameter("CreditAmount", objBankBook.CreditAmount);
                    objDbCommand.AddInParameter("UniqNumber", UniqNumber);
                    objDbCommand.AddInParameter("Remarks", objBankBook.Remarks);
                    objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

                    noOfAffacted = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].[uspCreateBankBook]", CommandType.StoredProcedure);
                    if (noOfAffacted > 0)
                    {
                        objDbCommand.Transaction.Commit();
                    }
                    else
                    {
                        objDbCommand.Transaction.Rollback();
                    }
                }
                // return "saved";
                return SessionUtility.STSessionContainer.UniqNumber = UniqNumber;
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

        public List<BankBookModel> GetPrevieByTransactionId(string TransactionId)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<BankBookModel> objBankBookModelList = new List<BankBookModel>();

            BankBookModel objBankBookModel = new BankBookModel();

            try
            {
                objDbCommand.AddInParameter("TransactionId", TransactionId);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspPreviewByTransactionIdForBankBook]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objBankBookModel = new BankBookModel();
                        objBankBookModel.TransactionId = objDbDataReader["TransactionId"].ToString();
                        objBankBookModel.TransactionDateShow = string.Format("{0:dd/MMM/yyyy}", objDbDataReader["TransactionDate"]);
                        objBankBookModel.BankName = objDbDataReader["BankName"].ToString();
                        objBankBookModel.AccountNumber = objDbDataReader["AccountNumber"].ToString();
                        objBankBookModel.DebitAmount = Convert.ToDecimal(objDbDataReader["DebitAmount"]);
                        objBankBookModel.CreditAmount = Convert.ToDecimal(objDbDataReader["CreditAmount"]);
                        objBankBookModelList.Add(objBankBookModel);
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

            return objBankBookModelList;
        }

    }
}