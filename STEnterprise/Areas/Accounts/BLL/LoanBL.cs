using STEnterprise.Areas.Accounts.Models;
using STEnterprise.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Accounts.BLL
{
    public class LoanBL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForBuildModelForBankBook(DbDataReader objDataReader, Loan objLoan)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {

                    case "LoanId":
                        if (!Convert.IsDBNull(objDataReader["LoanId"]))
                        {
                            objLoan.LoanId = Convert.ToInt32(objDataReader["LoanId"]);
                        }
                        break;
                    case "Date":
                        if (!Convert.IsDBNull(objDataReader["Date"]))
                        {
                            objLoan.Date = Convert.ToDateTime(objDataReader["Date"].ToString());
                        }
                        break;
                    case "BankDetailId":
                        if (!Convert.IsDBNull(objDataReader["BankDetailId"]))
                        {
                            objLoan.BankDetailId = Convert.ToInt16(objDataReader["BankDetailId"].ToString());
                        }
                        break;
                    case "BankName":
                        if (!Convert.IsDBNull(objDataReader["BankName"]))
                        {
                            objLoan.BankName = objDataReader["BankName"].ToString();
                        }
                        break;

                    case "AccountNumber":
                        if (!Convert.IsDBNull(objDataReader["AccountNumber"]))
                        {
                            objLoan.AccountNumber = objDataReader["AccountNumber"].ToString();
                        }
                        break;


                    case "Amount":
                        if (!Convert.IsDBNull(objDataReader["Amount"]))
                        {
                            objLoan.Amount = Convert.ToDecimal(objDataReader["Amount"]);
                        }
                        break;

                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objLoan.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        public int CreateLoan(Loan objLoan)
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

            objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetTrunsactionNumberExistForLoan]", CommandType.StoredProcedure);
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
                objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
                objDbCommand.AddInParameter("Date", objLoan.Date);
                objDbCommand.AddInParameter("BankName", objLoan.BankName);
                objDbCommand.AddInParameter("AccountNumber", objLoan.AccountNumber);
                objDbCommand.AddInParameter("Amount", objLoan.Amount);
                objDbCommand.AddInParameter("UniqNumber", UniqNumber);
                objDbCommand.AddInParameter("Remarks", objLoan.Remarks);
                objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

                noOfAffacted = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].[uspCreateLoan]", CommandType.StoredProcedure);
                if (noOfAffacted > 0)
                {
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    objDbCommand.Transaction.Rollback();
                }

                return noOfAffacted;
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

        public List<Loan> GetBankNameforLoan()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<Loan> objLoanList = new List<Loan>();

            Loan objLoan = new Loan();

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetBankNameforLoan]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objLoan = new Loan();
                        objLoan.BankDetailId = Convert.ToInt16(objDbDataReader["BankDetailId"]);
                        objLoan.BankName = objDbDataReader["BankName"].ToString();
                        objLoanList.Add(objLoan);
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

            return objLoanList;
        }

        public Loan GetBankNameAndTotalAmountByTruncationNumber(string TruncationNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            Loan objLoan = new Loan();
            try
            {
                objDbCommand.AddInParameter("TruncationNumber", TruncationNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetBankNameAndTotalAmountByTruncationNumber]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objLoan = new Loan();
                        objLoan.Amount = Convert.ToDecimal(objDbDataReader["Amount"]);
                        objLoan.AccountNumber = objDbDataReader["AccountNumber"].ToString();
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

            return objLoan;
        }
        public List<Loan> GetBankNameByTruncationNumber(string TruncationNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<Loan> objLoanList = new List<Loan>();
            Loan objLoan = new Loan();
            try
            {
                objDbCommand.AddInParameter("TruncationNumber", TruncationNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetBankNameByTruncationNumber]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objLoan = new Loan();
                        objLoan.BankName = objDbDataReader["BankName"].ToString();
                        objLoan.BankDetailId = Convert.ToInt16(objDbDataReader["BankDetailId"].ToString());
                        objLoanList.Add(objLoan);
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

            return objLoanList;
        }

        public int PaidLoan(Loan objLoan)
        {
            int noOfAffacted = 0;
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            try
            {
                objDataAccess = DataAccess.NewDataAccess();
                objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
                objDbCommand.AddInParameter("Date", objLoan.Date);
                objDbCommand.AddInParameter("BankName", objLoan.BankName);
                objDbCommand.AddInParameter("AccountNumber", objLoan.AccountNumber);
                objDbCommand.AddInParameter("TrunsactionNumber", objLoan.TransactionNumber);
                objDbCommand.AddInParameter("Amount", objLoan.LoanPaid);
                objDbCommand.AddInParameter("Remarks", objLoan.Remarks);

                noOfAffacted = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].[uspPaidLoan]", CommandType.StoredProcedure);
                if (noOfAffacted > 0)
                {
                    objDbCommand.Transaction.Commit();
                }
                else
                {
                    objDbCommand.Transaction.Rollback();
                }

                return noOfAffacted;
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

        public List<Loan> GetAllTruncationNumber()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<Loan> objLoanList = new List<Loan>();

            Loan objLoan = new Loan();

            try
            {
                //objDbCommand.AddInParameter("term", term);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetAllTruncationNumberForPaidLoan]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objLoan = new Loan();
                        objLoan.TransactionNumber = objDbDataReader["TrunsactionNumber"].ToString();

                        objLoanList.Add(objLoan);
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

            return objLoanList;
        }
    }
}