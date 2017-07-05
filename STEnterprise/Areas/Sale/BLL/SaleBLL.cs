using STEnterprise.Areas.Sale.Models;
using STEnterprise.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using STEnterprise.Areas.Inventory.Models;

namespace STEnterprise.Areas.Sale.BLL
{
    public class SaleBLL
    {

        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;

        private void BuildModelForSaleDetail(DbDataReader objDataReader, SaleModel objSaleModel)
        {

            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "CustomerId":
                        if (!Convert.IsDBNull(objDataReader["CustomerId"]))
                        {
                            objSaleModel.CustomerId = Convert.ToInt16(objDataReader["CustomerId"]);
                        }
                        break;
                    case "CustomerName":
                        if (!Convert.IsDBNull(objDataReader["CustomerName"]))
                        {
                            objSaleModel.CustomerName = objDataReader["CustomerName"].ToString();
                        }
                        break;
                    //case "TruckDetailId":
                    //    if (!Convert.IsDBNull(objDataReader["TruckDetailId"]))
                    //    {
                    //        objSaleModel.TruckDetailId = Convert.ToInt32(objDataReader["TruckDetailId"].ToString());
                    //    }
                    //    break;
                    case "TruckNumber":
                        if (!Convert.IsDBNull(objDataReader["TruckNumber"]))
                        {
                            objSaleModel.TruckNumber = objDataReader["TruckNumber"].ToString();
                        }
                        break;
                    case "TotalAmount":
                        if (!Convert.IsDBNull(objDataReader["TotalAmount"]))
                        {
                            objSaleModel.TotalAmount = Convert.ToDecimal(objDataReader["TotalAmount"].ToString());
                        }
                        break;
                    case "ProductId":
                        if (!Convert.IsDBNull(objDataReader["ProductId"]))
                        {
                            objSaleModel.ProductId = Convert.ToInt16(objDataReader["ProductId"]);
                        }
                        break;
                    case "ProductName":
                        if (!Convert.IsDBNull(objDataReader["ProductName"]))
                        {
                            objSaleModel.ProductName = objDataReader["ProductName"].ToString();
                        }
                        break;
                    case "PaidAmount":
                        if (!Convert.IsDBNull(objDataReader["PaidAmount"]))
                        {
                            objSaleModel.PaidAmount = Convert.ToDecimal(objDataReader["PaidAmount"].ToString());
                        }
                        break;
                    case "AdjustmentAmount":
                        if (!Convert.IsDBNull(objDataReader["AdjustmentAmount"]))
                        {
                            objSaleModel.AdjustmentAmount = Convert.ToDecimal(objDataReader["AdjustmentAmount"]);
                        }
                        break;
                    case "PreviousDueAmount":
                        if (!Convert.IsDBNull(objDataReader["PreviousDueAmount"]))
                        {
                            objSaleModel.PreviousDueAmount = Convert.ToDecimal(objDataReader["PreviousDueAmount"].ToString());
                        }
                        break;
                    //case "CustomerContactNumber":
                    //    if (!Convert.IsDBNull(objDataReader["CustomerContactNumber"]))
                    //    {
                    //        objCustomerInfo.CustomerContactNumber =
                    //            objDataReader["CustomerContactNumber"].ToString();
                    //    }
                    //    break;
                    //case "CustomerEmail":
                    //    if (!Convert.IsDBNull(objDataReader["CustomerEmail"]))
                    //    {
                    //        objCustomerInfo.CustomerEmail = objDataReader["CustomerEmail"].ToString();
                    //    }
                    //    break;
                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objSaleModel.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objSaleModel.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objSaleModel.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objSaleModel.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objSaleModel.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objSaleModel.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objSaleModel.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objSaleModel.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public List<SaleModel>  GetAllConsignmentNumber()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<SaleModel> objSaleModelList = new List<SaleModel>();
            SaleModel objSaleModel;

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetAllConsignmentNumberForReturnSale",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSaleModel = new SaleModel();

                        objSaleModel.ConsignmentNumber = objDbDataReader["ConsignmentNumber"].ToString();
                        objSaleModelList.Add(objSaleModel);
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

            return objSaleModelList;
        }

        public List<SaleModel> GetProductByConsainmentNumberForSale(string consignmentNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<SaleModel> objSaleModelList = new List<SaleModel>();
            SaleModel objSaleModel;

            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetProductByConsainmentNumberForSale",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSaleModel = new SaleModel();

                        objSaleModel.ProductId =Convert.ToInt16(objDbDataReader["ProductId"].ToString());
                        objSaleModel.ProductName = objDbDataReader["ProductName"].ToString();
                        objSaleModelList.Add(objSaleModel);
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

            return objSaleModelList;
        }



        // Customer Name List for DropDown
        public List<CustomerDetail> GetAllCustomerName()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<CustomerDetail> objCustomerNameList = new List<CustomerDetail>();
            CustomerDetail objCustomerName;

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetCustomerNameList",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objCustomerName = new CustomerDetail();

                        objCustomerName.CustomerId = Convert.ToByte(objDbDataReader["CustomerId"].ToString());
                        objCustomerName.CustomerName = objDbDataReader["CustomerName"].ToString();
                        objCustomerNameList.Add(objCustomerName);
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

            return objCustomerNameList;
        }

        // Save Sale
        public string SaveSale(SaleDetail objSaleDetail)
        {
            int noOfAffacted = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);

            // Insert sale

            objDbCommand.AddInParameter("CustomerId", objSaleDetail.Sales.CustomerId);
            objDbCommand.AddInParameter("ConsignmentNumber", objSaleDetail.Sales.ConsignmentNumber);
            objDbCommand.AddInParameter("SellingDate", objSaleDetail.Sales.SellingDate);
            objDbCommand.AddInParameter("SellingAmount", objSaleDetail.Sales.SellingAmount);
            objDbCommand.AddInParameter("PaymentMethod", objSaleDetail.Sales.PaymentMethod);
            objDbCommand.AddInParameter("ChequeNumber", objSaleDetail.Sales.ChequeNumber);
            objDbCommand.AddInParameter("TotalAmount", objSaleDetail.Sales.TotalAmount);
            objDbCommand.AddInParameter("PaidAmount", objSaleDetail.Sales.PaidAmount);
            objDbCommand.AddInParameter("AdjustmentAmount", objSaleDetail.Sales.AdjustmentAmount);
            objDbCommand.AddInParameter("Remarks", objSaleDetail.Sales.Remarks);
            objDbCommand.AddInParameter("OrderNumber", objSaleDetail.Sales.OrderNumber);
            // Insert sale details

            objDbCommand.AddInParameter("ProductId", objSaleDetail.ProductId);
            objDbCommand.AddInParameter("TruckNumber", objSaleDetail.TruckNumber);
            //objDbCommand.AddInParameter("TruckDetailId", objSaleDetail.TruckDetailId);
            objDbCommand.AddInParameter("SalePrice", objSaleDetail.SalePrice);
            objDbCommand.AddInParameter("SaleUnitBag", objSaleDetail.SaleUnitBag);
            objDbCommand.AddInParameter("SaleUnitKG", objSaleDetail.SaleUnitKG);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);



            try
            {
                noOfAffacted = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].[uspCreateSales]", CommandType.StoredProcedure);
                if (noOfAffacted > 0)
                {
                    objDbCommand.Transaction.Commit();
                    return "saved";
                }
                else
                {
                    objDbCommand.Transaction.Rollback();
                    return "failed";
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

        public SaleModel GetCustomerDueAmount(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            SaleModel objSaleModel = new SaleModel();
            List<SaleModel> objSaleModelList = new List<SaleModel>();

            try
            {
                objDbCommand.AddInParameter("CustomerId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetDueByCustomerName]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSaleModel = new SaleModel();
                        this.BuildModelForSaleDetail(objDbDataReader, objSaleModel);

                        objSaleModelList.Add(objSaleModel);
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

            return objSaleModel;
        }

        // Start retrun Sale
        public List<SaleModel> GetCutomerNameByConsignment(string consignmentNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<SaleModel> objSaleModelList = new List<SaleModel>();
            SaleModel objSaleModel;

            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetCstomerNameByConsignmentForReturnSale",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSaleModel = new SaleModel();
                        if (!string.IsNullOrEmpty(objDbDataReader["CustomerId"].ToString()))
                        {
                            objSaleModel.CustomerId = Convert.ToInt16(objDbDataReader["CustomerId"]);
                            objSaleModel.CustomerName = objDbDataReader["CustomerName"].ToString();
                            objSaleModelList.Add(objSaleModel);
                        }
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

            return objSaleModelList;
        }

        public List<SaleModel> GetProductNameByConsignment(string truck, string consignmentNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<SaleModel> objSaleModelList = new List<SaleModel>();
            SaleModel objSaleModel;

            try
            {
                objDbCommand.AddInParameter("Truck", truck);
                objDbCommand.AddInParameter("ConsignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetProductNameByConsignmentNumberForReturnSale",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSaleModel = new SaleModel();
                        this.BuildModelForSaleDetail(objDbDataReader, objSaleModel);
                        objSaleModelList.Add(objSaleModel);
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

            return objSaleModelList;
        }
        public SaleModel GetTotalAmountByConsignmentNumber(string CustomerId)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            //List<SaleDetail> objSaleModelList = new List<SaleDetail>();
            SaleModel objSaleModel = new SaleModel();

            try
            {
                objDbCommand.AddInParameter("CustomerId", CustomerId);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetTotalAmountByConsignmentNumberForReturnSale",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSaleModel = new SaleModel();
                        if (!string.IsNullOrEmpty(objDbDataReader["TotalAmount"].ToString()))
                        {
                            objSaleModel.TotalAmount = Convert.ToDecimal(objDbDataReader["TotalAmount"].ToString());
                        }
                                               
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

            return objSaleModel;
        }


        public SaleDetail GetUnitBagKgPriceByProdcutName(int productId, string consignmentNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            //List<SaleDetail> objSaleModelList = new List<SaleDetail>();
            SaleDetail objSaleModel =new SaleDetail();

            try
            {
                objDbCommand.AddInParameter("ProductId", productId);
                objDbCommand.AddInParameter("consignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetUnitBagKgPriceByProdcutNameForReturnSale",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSaleModel = new SaleDetail();
                        if (!string.IsNullOrEmpty(objDbDataReader["SaleUnitBag"].ToString())|| !string.IsNullOrEmpty(objDbDataReader["SaleUnitKG"].ToString()))
                        {
                            objSaleModel.SaleUnitBag = Convert.ToDecimal(objDbDataReader["SaleUnitBag"].ToString());
                            objSaleModel.SaleUnitKG = Convert.ToDecimal(objDbDataReader["SaleUnitKG"].ToString());
                            objSaleModel.SalePrice = Convert.ToDecimal(objDbDataReader["SalePrice"].ToString());
                        }
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

            return objSaleModel;
        }

        public string SaveReturnSale(SaleDetail objSaleDetail)
        {
            int noOfAffacted = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);

            // Insert sale
            objDbCommand.AddInParameter("ConsignmentNumber", objSaleDetail.Sales.ConsignmentNumber);
            objDbCommand.AddInParameter("CustomerId", objSaleDetail.Sales.CustomerId);
            objDbCommand.AddInParameter("SellingDate", objSaleDetail.Sales.SellingDate);
            objDbCommand.AddInParameter("ReturnAmount", objSaleDetail.Sales.TotalReturnAmount);
            objDbCommand.AddInParameter("TotalAmount", objSaleDetail.Sales.TotalAmount);
            objDbCommand.AddInParameter("Remarks", objSaleDetail.Sales.Remarks);

            // Insert sale details
            objDbCommand.AddInParameter("ProductId", objSaleDetail.ProductId);
            objDbCommand.AddInParameter("TruckNumber", objSaleDetail.TruckNumber);
            objDbCommand.AddInParameter("ReturnSaleUnitBag", objSaleDetail.ReturnSaleUnitBag);
            objDbCommand.AddInParameter("ReturnSaleUnitKG", objSaleDetail.ReturnSaleUnitKG);
            objDbCommand.AddInParameter("SalePrice", objSaleDetail.SalePrice);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);





            try
            {
                noOfAffacted = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].[uspReturnSales]", CommandType.StoredProcedure);
                if (noOfAffacted > 0)
                {
                    objDbCommand.Transaction.Commit();
                    return "saved";
                }
                else
                {
                    objDbCommand.Transaction.Rollback();
                    return "failed";
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
        // end of return sale        


         //Save Damurrage sale
        public string DemurrageSave(SaleDetail objSaleDetail)
        {
            int noOfAffacted = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);


            // Insert sale
            objDbCommand.AddInParameter("ConsignmentNumber", objSaleDetail.Sales.ConsignmentNumber);
            objDbCommand.AddInParameter("CustomerId", objSaleDetail.Sales.CustomerId);
            objDbCommand.AddInParameter("SellingDate", objSaleDetail.Sales.SellingDate);
            objDbCommand.AddInParameter("ReturnAmount", objSaleDetail.Sales.TotalReturnAmount);
            objDbCommand.AddInParameter("TotalAmount", objSaleDetail.Sales.TotalAmount);
            objDbCommand.AddInParameter("Remarks", objSaleDetail.Sales.Remarks);

            // Insert sale details
            //objDbCommand.AddInParameter("ReturnAmount", objSaleDetail.ReturnAmount);
            objDbCommand.AddInParameter("ProductId", objSaleDetail.ProductId);
            objDbCommand.AddInParameter("TruckNumber", objSaleDetail.TruckNumber);
            objDbCommand.AddInParameter("ReturnSaleUnitBag", objSaleDetail.ReturnSaleUnitBag);
            objDbCommand.AddInParameter("ReturnSaleUnitKG", objSaleDetail.ReturnSaleUnitKG);
            objDbCommand.AddInParameter("SalePrice", objSaleDetail.SalePrice);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);


            try
            {
                noOfAffacted = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].[uspSaveDemurrageSale]", CommandType.StoredProcedure);
                if (noOfAffacted > 0)
                {
                    objDbCommand.Transaction.Commit();
                    return "saved";
                }
                else
                {
                    objDbCommand.Transaction.Rollback();
                    return "failed";
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

       
        public SaleDetail GetTotalBag(int? saleUnitBag, int? saleUnitKg, string productName)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            SaleDetail objSaleDetail = new SaleDetail();

            try
            {
                objDbCommand.AddInParameter("ProductName", productName);
                objDbCommand.AddInParameter("SaleUnitBag", saleUnitBag);
                objDbCommand.AddInParameter("SaleUnitKg", saleUnitKg);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetTotalBag",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSaleDetail = new SaleDetail();
                        objSaleDetail.SaleUnitBag = Convert.ToDecimal(objDbDataReader["TotalBag"]);
                        objSaleDetail.SaleUnitKG = Convert.ToDecimal(objDbDataReader["Stock"].ToString());


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

            return objSaleDetail;
        }

        public List<SaleDetail> GetTruckNumberByConsainmentNumber(string consignmentNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<SaleDetail> objSaleDetailList = new List<SaleDetail>();
            SaleDetail objSaleDetail = new SaleDetail();
            try
            {
               
                objDbCommand.AddInParameter("ConsignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetTruckNumberByConsainmentNumberForSale",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        if (!string.IsNullOrEmpty(objDbDataReader["TruckNumber"].ToString()))
                        {
                            objSaleDetail = new SaleDetail();
                            objSaleDetail.TruckNumber = objDbDataReader["TruckNumber"].ToString();
                            objSaleDetail.SaleDetailId = Convert.ToInt32(objDbDataReader["SaleDetailId"].ToString());
                            objSaleDetailList.Add(objSaleDetail);
                        }
                        
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

            return objSaleDetailList;
        }
    }
}