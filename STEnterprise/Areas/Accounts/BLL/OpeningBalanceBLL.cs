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
    public class OpeningBalanceBLL
    {

        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForBuildModelForOpeningBalance(DbDataReader objDataReader, OpeningBalance objOpeningBalance)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {

                    case "OpeningBalanceId":
                        if (!Convert.IsDBNull(objDataReader["OpeningBalanceId"]))
                        {
                            objOpeningBalance.OpeningBalanceId = Convert.ToSByte(objDataReader["OpeningBalanceId"]);
                        }
                        break;
                    case "OpeningBalanceDate":
                        if (!Convert.IsDBNull(objDataReader["OpeningBalanceDate"]))
                        {
                            objOpeningBalance.OpeningBalanceDate = Convert.ToDateTime(objDataReader["OpeningBalanceDate"].ToString());
                        }
                        break;
                    case "AccountType":
                        if (!Convert.IsDBNull(objDataReader["AccountType"]))
                        {
                            objOpeningBalance.AccountType = objDataReader["AccountType"].ToString();
                        }
                        break;

                    case "PartyId":
                        if (!Convert.IsDBNull(objDataReader["PartyId"]))
                        {
                            objOpeningBalance.PartyId = Convert.ToByte(objDataReader["PartyId"]);
                        }
                        break;
                    case "PartyName":
                        if (!Convert.IsDBNull(objDataReader["PartyName"]))
                        {
                            objOpeningBalance.PartyName = objDataReader["PartyName"].ToString();
                        }
                        break;
                    case "Ammount":
                        if (!Convert.IsDBNull(objDataReader["Ammount"]))
                        {
                            objOpeningBalance.Amount = Convert.ToDecimal(objDataReader["Ammount"].ToString());
                        }
                        break;           
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objOpeningBalance.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        public string CreateOpeningBalnce(OpeningBalance objOpeningBalance)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("OpeningBalanceDate", objOpeningBalance.OpeningBalanceDate);
            objDbCommand.AddInParameter("AccountType", objOpeningBalance.AccountType);
            objDbCommand.AddInParameter("PartyId", objOpeningBalance.PartyId);
            objDbCommand.AddInParameter("Ammount", objOpeningBalance.Amount);

            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateOpeningBalnce", CommandType.StoredProcedure);

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
    }
}