using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using STEnterprise.DAL;
using STEnterprise.Models;
using ReportObject = STEnterprise.CommonObject.ReportObject;

namespace STEnterprise.BLL
{
    public class ReportBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
       
         public DataTable GetDailyBuyReport()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt=new DataTable();
            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", ReportObject.ConsignmentNumber);
                objDbCommand.AddInParameter("SupplierId", ReportObject.SupplierId);
                objDbCommand.AddInParameter("ProductId", ReportObject.ProductId);
                objDbCommand.AddInParameter("TruckDetailId", ReportObject.TruckDetailId);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetDailyBuyReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public DataTable GetDailyLocalSaleReport()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", ReportObject.ConsignmentNumber);
                objDbCommand.AddInParameter("ProductId", ReportObject.ProductId);
                objDbCommand.AddInParameter("TruckDetailId", ReportObject.TruckDetailId);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                objDbCommand.AddInParameter("CustomerId", ReportObject.CustomerId);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetDailyLocalSaleReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public DataTable GetConsignmentWiseSaleReport()
        {

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", ReportObject.ConsignmentNumber);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetDailyForeignSaleReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public DataTable GetDailyStockReport()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                
                objDbCommand.AddInParameter("ProductId", ReportObject.ProductId);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetDailyStockReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public DataTable GetDailyDueStatementReport()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", ReportObject.ConsignmentNumber);
                //objDbCommand.AddInParameter("SupplierId", ReportObject.SupplierId);
                objDbCommand.AddInParameter("CustomerId", ReportObject.CustomerId);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetDailyDueStatementReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public DataTable GetDailyPayStatementReport()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", ReportObject.ConsignmentNumber);
                objDbCommand.AddInParameter("SupplierId", ReportObject.SupplierId);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetDailyPayStatementReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public DataTable GetDailyForeignSaleReport()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", ReportObject.ConsignmentNumber);
                objDbCommand.AddInParameter("CustomerId", ReportObject.CustomerId);
                objDbCommand.AddInParameter("CountryId", ReportObject.CountryId);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetDailyForeignSaleReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public DataTable GetDailyPartlyLedgerReport()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetDailyPartyLedgerReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt; 
        }

        public DataTable GetDailyTotalProductionCostReport()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspDailyTotalProductionCostReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }
        public DataTable GetIncomeStatementReport()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                //objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspIncomeStatementReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public DataTable GetSalesLedgerAccount()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspSalesLedgerAccountReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }
        public DataTable GetPurchaseLedgerAccount()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspPurchaseLedgerAccountReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }


        //ataur
        public DataTable GetChequePaymentForPurchase()
        {

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", ReportObject.ConsignmentNumber);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetChequePaymentForPurchaseReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        //ataur
        public DataTable GetChequeReceivedForSale()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", ReportObject.ConsignmentNumber);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetChequeReceivedForSaleReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        //ataur
        public DataTable GetConsignmentWiseTruckFare()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", ReportObject.ConsignmentNumber);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetConsignmentWiseTruckFairReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public DataTable GetDailyTotalCostEverySubject()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetDailyTotalCostEverySubjectReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

        public DataTable GetBuyerDetail()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            var dt = new DataTable();
            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", ReportObject.ConsignmentNumber);
                objDbCommand.AddInParameter("FromDate", ReportObject.FromDate);
                objDbCommand.AddInParameter("ToDate", ReportObject.ToDate);
                objDbCommand.AddInParameter("CustomerId", ReportObject.CustomerId);
                objDbCommand.AddInParameter("CountryId", ReportObject.CountryId);
                dt = objDataAccess.ExecuteTable(objDbCommand, "[dbo].uspGetBuyerDetailReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                objDataAccess.Dispose(objDbCommand);
            }

            return dt;
        }

    }
}