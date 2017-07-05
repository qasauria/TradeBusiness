using STEnterprise.Areas.Accounts.Models;
using STEnterprise.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Ledger.BLL
{
    public class LedgerBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForBuildModelForChequeDetail(DbDataReader objDataReader, DailyLedger objDailyLedger)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {

                    case "DailyLedgerId":
                        if (!Convert.IsDBNull(objDataReader["DailyLedgerId"]))
                        {
                            objDailyLedger.DailyLedgerId = Convert.ToInt32(objDataReader["DailyLedgerId"]);
                        }
                        break;
                    case "TransationDate":
                        if (!Convert.IsDBNull(objDataReader["TransationDate"]))
                        {
                            objDailyLedger.TransactionDate = Convert.ToDateTime(objDataReader["TransationDate"].ToString());
                        }
                        break;
                    case "AccountType":
                        if (!Convert.IsDBNull(objDataReader["AccountType"]))
                        {
                            objDailyLedger.AccountType = objDataReader["AccountType"].ToString();
                        }
                        break;

                    case "PartyId":
                        if (!Convert.IsDBNull(objDataReader["PartyId"]))
                        {
                            objDailyLedger.PartyId = Convert.ToByte(objDataReader["PartyId"]);
                        }
                        break;
                    case "PartyName":
                        if (!Convert.IsDBNull(objDataReader["PartyName"]))
                        {
                            objDailyLedger.PartyName = objDataReader["PartyName"].ToString();
                        }
                        break;
                    case "LedgerEntryType":
                        if (!Convert.IsDBNull(objDataReader["LedgerEntryType"]))
                        {
                            objDailyLedger.LedgerEntryType = objDataReader["LedgerEntryType"].ToString();
                        }
                        break;
                    case "DebitAmount":
                        if (!Convert.IsDBNull(objDataReader["DebitAmount"]))
                        {
                            objDailyLedger.DebitAmount = Convert.ToDecimal(objDataReader["DebitAmount"]);
                        }
                        break;
                    case "CreditAmount":
                        if (!Convert.IsDBNull(objDataReader["CreditAmount"]))
                        {
                            objDailyLedger.CreditAmount = Convert.ToDecimal(objDataReader["CreditAmount"].ToString());
                        }
                        break;                    
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objDailyLedger.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        public List<DailyLedger> GetPartyNameByAccountType(string AccountType)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<DailyLedger> objDailyLedgerList = new List<DailyLedger>();

            DailyLedger objDailyLedger = new DailyLedger();

            try
            {
                objDbCommand.AddInParameter("AccountType", AccountType);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetPartyNameByAccountType]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objDailyLedger = new DailyLedger();
                        this.BuildModelForBuildModelForChequeDetail(objDbDataReader, objDailyLedger);
                        objDailyLedgerList.Add(objDailyLedger);
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

            return objDailyLedgerList;
        }

        public string SaveLadger(List<DailyLedger> objLedger)
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

            objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetUniqueNumberExist]", CommandType.StoredProcedure);
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
                foreach (DailyLedger objDailyLedger in objLedger)
                {
                    objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
                    objDbCommand.AddInParameter("TransactionDate", objDailyLedger.TransactionDate);
                    objDbCommand.AddInParameter("AccountType", objDailyLedger.AccountType);
                    objDbCommand.AddInParameter("PartyId", objDailyLedger.PartyId);
                    objDbCommand.AddInParameter("LedgerEntryType", objDailyLedger.LedgerEntryType);
                    objDbCommand.AddInParameter("DebitAmount", objDailyLedger.DebitAmount);
                    objDbCommand.AddInParameter("CreditAmount", objDailyLedger.CreditAmount);
                    objDbCommand.AddInParameter("UniqNumber", UniqNumber);
                    objDbCommand.AddInParameter("Remarks", objDailyLedger.Remarks);
                    objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

                    noOfAffacted = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].[uspCreateLadger]", CommandType.StoredProcedure);
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

        public List<DailyLedger> GetPrevieByTransactionId(string TransactionId)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<DailyLedger> objDailyLedgerList = new List<DailyLedger>();

            DailyLedger objDailyLedger = new DailyLedger();

            try
            {
                objDbCommand.AddInParameter("TransactionId", TransactionId);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspPreviewByTransactionId]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objDailyLedger = new DailyLedger();
                        objDailyLedger.TransactionId = objDbDataReader["TransactionId"].ToString();
                        objDailyLedger.TransactionDateShow = string.Format("{0:dd/MMM/yyyy}", objDbDataReader["TransactionDate"].ToString());
                        objDailyLedger.CustomerName = objDbDataReader["CustomerName"].ToString();
                        objDailyLedger.SupplierName = objDbDataReader["SupplierName"].ToString();
                        objDailyLedger.ExpenseType = objDbDataReader["ExpenseType"].ToString();
                        objDailyLedger.DebitAmount = Convert.ToDecimal(objDbDataReader["DebitAmount"]);
                        objDailyLedger.CreditAmount = Convert.ToDecimal(objDbDataReader["CreditAmount"]);
                        objDailyLedgerList.Add(objDailyLedger);
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

            return objDailyLedgerList;
        }

    }
}
