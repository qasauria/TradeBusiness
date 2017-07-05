using STEnterprise.Areas.Accounts.Models;
using STEnterprise.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace STEnterprise.Areas.Accounts.Controllers
{
    public class BankBookOpeningBalanceBLL
    {

        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForBuildModelForBankBookOpeningBalance(DbDataReader objDataReader, BankBookOpeningBalance objBankBookOpeningBalance)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {

                    case "BankBookOpeningBalnceId":
                        if (!Convert.IsDBNull(objDataReader["BankBookOpeningBalnceId"]))
                        {
                            objBankBookOpeningBalance.BankBookOpeningBalnceId = Convert.ToSByte(objDataReader["BankBookOpeningBalnceId"]);
                        }
                        break;
                    case "BankBookOpeningBalnceDate":
                        if (!Convert.IsDBNull(objDataReader["BankBookOpeningBalnceDate"]))
                        {
                            objBankBookOpeningBalance.BankBookOpeningBalnceDate = Convert.ToDateTime(objDataReader["BankBookOpeningBalnceDate"].ToString());
                        }
                        break;
                    case "BankName":
                        if (!Convert.IsDBNull(objDataReader["BankName"]))
                        {
                            objBankBookOpeningBalance.BankName = objDataReader["BankName"].ToString();
                        }
                        break;
                    case "BankDetailId":
                        if (!Convert.IsDBNull(objDataReader["BankDetailId"]))
                        {
                            objBankBookOpeningBalance.BankDetailId = Convert.ToSByte(objDataReader["BankDetailId"].ToString());
                        }
                        break;

                    case "AccountNumber":
                        if (!Convert.IsDBNull(objDataReader["AccountNumber"]))
                        {
                            objBankBookOpeningBalance.AccountNumber = objDataReader["AccountNumber"].ToString();
                        }
                        break;
                    case "Amount":
                        if (!Convert.IsDBNull(objDataReader["Amount"]))
                        {
                            objBankBookOpeningBalance.Amount = Convert.ToDecimal(objDataReader["Amount"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objBankBookOpeningBalance.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        public string CreateBankBookOpeningBalance(BankBookOpeningBalance objBankBookOpeningBalance)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("BankBookOpeningBalnceDate", objBankBookOpeningBalance.BankBookOpeningBalnceDate);
            objDbCommand.AddInParameter("BankName", objBankBookOpeningBalance.BankName);
            objDbCommand.AddInParameter("AccountNumber", objBankBookOpeningBalance.AccountNumber);
            objDbCommand.AddInParameter("Amount", objBankBookOpeningBalance.Amount);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateBankBookOpeningBalance", CommandType.StoredProcedure);

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
        public List<BankBookOpeningBalance> GetBankNameforOpeningBalnce()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<BankBookOpeningBalance> objBankBookOpeningBalanceList = new List<BankBookOpeningBalance>();

            BankBookOpeningBalance objBankBookOpeningBalance = new BankBookOpeningBalance();

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetBankNameForBankBook]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objBankBookOpeningBalance = new BankBookOpeningBalance();
                        this.BuildModelForBuildModelForBankBookOpeningBalance(objDbDataReader, objBankBookOpeningBalance);
                        objBankBookOpeningBalanceList.Add(objBankBookOpeningBalance);
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

            return objBankBookOpeningBalanceList;
        }
    }
}