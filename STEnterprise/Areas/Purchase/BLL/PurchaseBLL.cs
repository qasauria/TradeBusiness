using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using STEnterprise.Areas.Inventory.Models;
using STEnterprise.Areas.Purchase.Models;
using STEnterprise.DAL;

namespace STEnterprise.Areas.Purchase.BLL
{
    //ataur
    public class PurchaseBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForPurchase(DbDataReader objDataReader, PurchaseModel objPurchaseModel)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "PurchaseId":
                        if (!Convert.IsDBNull(objDataReader["PurchaseId"]))
                        {
                            objPurchaseModel.PurchaseId = Convert.ToInt32(objDataReader["PurchaseId"]);
                        }
                        break;
                    case "SupplierId":
                        if (!Convert.IsDBNull(objDataReader["SupplierId"]))
                        {
                            objPurchaseModel.SupplierId= Convert.ToInt32(objDataReader["SupplierId"].ToString());
                        }
                        break;
                    case "SupplierName":
                        if (!Convert.IsDBNull(objDataReader["SupplierName"]))
                        {
                            objPurchaseModel.SupplierName = objDataReader["SupplierName"].ToString();
                        }
                        break;
                    case "TruckDetailId":
                        if (!Convert.IsDBNull(objDataReader["TruckDetailId"]))
                        {
                            objPurchaseModel.TruckDetailId = Convert.ToInt32(objDataReader["TruckDetailId"].ToString());
                        }
                        break;
                    case "TruckNumber":
                        if (!Convert.IsDBNull(objDataReader["TruckNumber"]))
                        {
                            objPurchaseModel.TruckNumber = objDataReader["TruckNumber"].ToString();
                        }
                        break;
                    case "ProductId":
                        if (!Convert.IsDBNull(objDataReader["ProductId"]))
                        {
                            objPurchaseModel.ProductId = Convert.ToInt32(objDataReader["ProductId"].ToString());
                        }
                        break;
                    case "ProductName":
                        if (!Convert.IsDBNull(objDataReader["ProductName"]))
                        {
                            objPurchaseModel.ProductName = objDataReader["ProductName"].ToString();
                        }
                        break;
                    case "ConsignmentNumber":
                        if (!Convert.IsDBNull(objDataReader["ConsignmentNumber"]))
                        {
                            objPurchaseModel.ConsignmentNumber = objDataReader["ConsignmentNumber"].ToString();
                        }
                        break;
                    case "PurchaseDate":
                        if (!Convert.IsDBNull(objDataReader["PurchaseDate"]))
                        {
                            objPurchaseModel.PurchaseDate = Convert.ToDateTime(objDataReader["PurchaseDate"].ToString());
                        }
                        break;
                    case "TotalAmount":
                        if (!Convert.IsDBNull(objDataReader["TotalAmount"]))
                        {
                            objPurchaseModel.TotalAmount = Convert.ToDecimal(objDataReader["TotalAmount"].ToString());
                        }
                        break;
                    case "AdjustmentAmount":
                        if (!Convert.IsDBNull(objDataReader["AdjustmentAmount"]))
                        {
                            objPurchaseModel.AdjustmentAmount = Convert.ToDecimal(objDataReader["AdjustmentAmount"].ToString());
                        }
                        break;
                    case "PaidAmount":
                        if (!Convert.IsDBNull(objDataReader["PaidAmount"]))
                        {
                            objPurchaseModel.PaidAmount = Convert.ToDecimal(objDataReader["PaidAmount"].ToString());
                        }
                        break;
                    case "PreviousDueAmount":
                        if (!Convert.IsDBNull(objDataReader["PreviousDueAmount"]))
                        {
                            objPurchaseModel.PreviousDueAmount = Convert.ToDecimal(objDataReader["PreviousDueAmount"].ToString());
                        }
                        break;
                    case "ReturnAmount":
                        if (!Convert.IsDBNull(objDataReader["ReturnAmount"]))
                        {
                            objPurchaseModel.ReturnAmount = Convert.ToDecimal(objDataReader["ReturnAmount"].ToString());
                        }
                        break;
                    case "PaymentMethod":
                        if (!Convert.IsDBNull(objDataReader["PaymentMethod"]))
                        {
                            objPurchaseModel.PaymentMethod = Convert.ToByte(objDataReader["PaymentMethod"].ToString());
                        }
                        break;

                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objPurchaseModel.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objPurchaseModel.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objPurchaseModel.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objPurchaseModel.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objPurchaseModel.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objPurchaseModel.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objPurchaseModel.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objPurchaseModel.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        public string SavePurchase(PurchaseDetail objPurchaseDetail)
        {
            int noOfAffacted = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);

            // Insert purchase

            objDbCommand.AddInParameter("SupplierId", objPurchaseDetail.Purchase.SupplierId);
            objDbCommand.AddInParameter("ConsignmentNumber", objPurchaseDetail.Purchase.ConsignmentNumber);
            objDbCommand.AddInParameter("PurchaseDate", objPurchaseDetail.Purchase.PurchaseDate);
            objDbCommand.AddInParameter("PurchaseAmount", objPurchaseDetail.Purchase.PurchaseAmount);
            objDbCommand.AddInParameter("TotalAmount", objPurchaseDetail.Purchase.TotalAmount);
            objDbCommand.AddInParameter("PaidAmount", objPurchaseDetail.Purchase.PaidAmount);
            objDbCommand.AddInParameter("AdjustmentAmount", objPurchaseDetail.Purchase.AdjustmentAmount);
            objDbCommand.AddInParameter("OrderNumber", objPurchaseDetail.Purchase.OrderNumber);
            objDbCommand.AddInParameter("ChequeNumber", objPurchaseDetail.Purchase.ChequeNumber);
            objDbCommand.AddInParameter("TTNumber", objPurchaseDetail.Purchase.TTNumber);
            objDbCommand.AddInParameter("PaymentMethod", objPurchaseDetail.Purchase.PaymentMethod);
            objDbCommand.AddInParameter("Remarks", objPurchaseDetail.Purchase.Remarks);

            // Insert purchase details

            objDbCommand.AddInParameter("ProductId", objPurchaseDetail.ProductId);
            objDbCommand.AddInParameter("PurchasePrice", objPurchaseDetail.PurchasePrice);
            objDbCommand.AddInParameter("PurchaseUnitBag", objPurchaseDetail.PurchaseUnitBag);
            objDbCommand.AddInParameter("PurchaseUnitKg", objPurchaseDetail.PurchaseUnitKg);
            objDbCommand.AddInParameter("TruckNumber", objPurchaseDetail.TruckNumber);
            objDbCommand.AddInParameter("TruckFare", objPurchaseDetail.TruckFare);
            objDbCommand.AddInParameter("AdvancePayment", objPurchaseDetail.AdvancePayment);
            objDbCommand.AddInParameter("LoadingCost", objPurchaseDetail.LoadingCost);
            objDbCommand.AddInParameter("UnloadingCost", objPurchaseDetail.UnloadingCost);
            objDbCommand.AddInParameter("OtherCost", objPurchaseDetail.OtherCost);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);


            try
            {
                noOfAffacted = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].[uspCreatePurchase]", CommandType.StoredProcedure);
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

        public PurchaseModel GetSupplierDueAmount(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            PurchaseModel objPurchaseModel = new PurchaseModel();
            List<PurchaseModel> objPurchaseModelList = new List<PurchaseModel>();

            try
            {
                objDbCommand.AddInParameter("SupplierId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetDueBySupplier]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objPurchaseModel = new PurchaseModel();
                        this.BuildModelForPurchase(objDbDataReader, objPurchaseModel);

                        objPurchaseModelList.Add(objPurchaseModel);
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

            return objPurchaseModel;
        }

        public List<PurchaseModel> GetAllPurchaseInfo(string consignmentNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<PurchaseModel> objPurchaseModelList = new List<PurchaseModel>();
            PurchaseModel objPurchaseModel;
            PurchaseDetail objPurchaseDetail;

            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "uspGetPurchaseDetilByConsignmentNumber", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objPurchaseModel = new PurchaseModel();
                        objPurchaseDetail = new PurchaseDetail();
                        this.BuildModelForPurchase(objDbDataReader, objPurchaseModel);
                    //    this.BuildModelForPurchaseDetail(objDbDataReader, objPurchaseDetail);
                        objPurchaseModelList.Add(objPurchaseModel);
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

            return objPurchaseModelList;
        }
        public List<PurchaseModel> GetSuppliersByConsignment(string consignmentNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<PurchaseModel> objPurchaseModelList = new List<PurchaseModel>();
            PurchaseModel objPurchaseModel;
            PurchaseDetail objPurchaseDetail;

            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "uspGetSuppliersByConsignment", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objPurchaseModel = new PurchaseModel();
                        objPurchaseDetail = new PurchaseDetail();
                        this.BuildModelForPurchase(objDbDataReader, objPurchaseModel);                        
                        objPurchaseModelList.Add(objPurchaseModel);
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

            return objPurchaseModelList;
        }
        public PurchaseModel GetTotalAmountByConsignment( string consignmentNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            PurchaseModel objPurchaseModel = new PurchaseModel();

            try
            {
                objDbCommand.AddInParameter("consignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetTotalAmountByConsignment",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objPurchaseModel = new PurchaseModel();
                        objPurchaseModel.TotalAmount = Convert.ToDecimal(objDbDataReader["TotalAmount"].ToString());
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

            return objPurchaseModel;
        }

        public string SavePurchaseDemurrage(PurchaseDetail objPurchaseDetail)
        {
            int noOfAffacted = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);

            // Insert purchase

            objDbCommand.AddInParameter("ConsignmentNumber", objPurchaseDetail.Purchase.ConsignmentNumber);
            objDbCommand.AddInParameter("SupplierId", objPurchaseDetail.Purchase.SupplierId);
            objDbCommand.AddInParameter("PurchaseDate", objPurchaseDetail.Purchase.PurchaseDate);
            objDbCommand.AddInParameter("TotalAmount", objPurchaseDetail.Purchase.ReturnAmount);
            
            objDbCommand.AddInParameter("Remarks", objPurchaseDetail.Purchase.Remarks);

            // Insert sale details
            objDbCommand.AddInParameter("ProductId", objPurchaseDetail.ProductId);
            objDbCommand.AddInParameter("TruckNumber", objPurchaseDetail.TruckNumber);
            objDbCommand.AddInParameter("PurchaseUnitBag", objPurchaseDetail.ReturnPurchaseUnitBag);
            objDbCommand.AddInParameter("PurchaseUnitKG", objPurchaseDetail.ReturnPurchaseUnitKG);
            objDbCommand.AddInParameter("PurchasePrice", objPurchaseDetail.PurchasePrice);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);
            try
            {
                noOfAffacted = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].[uspSavePurchaseDemurrage]", CommandType.StoredProcedure);
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

        public PurchaseDetail GetUnitBagKgPriceByProdcutName(int productId, string consignmentNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            //List<SaleDetail> objSaleModelList = new List<SaleDetail>();
            PurchaseDetail objPurchaseModel = new PurchaseDetail();

            try
            {
                objDbCommand.AddInParameter("ProductId", productId);
                objDbCommand.AddInParameter("consignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetPurchaseUnitBagKgPriceByProdcutName",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objPurchaseModel = new PurchaseDetail();
                        objPurchaseModel.PurchaseUnitBag = Convert.ToDecimal(objDbDataReader["PurchaseUnitBag"].ToString());
                        objPurchaseModel.PurchaseUnitKg = Convert.ToDecimal(objDbDataReader["PurchaseUnitKG"].ToString());
                        objPurchaseModel.PurchasePrice = Convert.ToDecimal(objDbDataReader["PurchasePrice"].ToString());
                        //objSaleModelList.Add(objSaleModel);
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

            return objPurchaseModel;
        }

        public string SaveReturnPurchase(PurchaseDetail objPurchaseDetail)
        {
            int noOfAffacted = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);

            // Insert sale
            objDbCommand.AddInParameter("ConsignmentNumber", objPurchaseDetail.Purchase.ConsignmentNumber);
            objDbCommand.AddInParameter("SupplierId", objPurchaseDetail.Purchase.SupplierId);
            objDbCommand.AddInParameter("PurchaseDate", objPurchaseDetail.Purchase.PurchaseDate);
            objDbCommand.AddInParameter("TotalAmount", objPurchaseDetail.Purchase.ReturnAmount);
            objDbCommand.AddInParameter("Remarks", objPurchaseDetail.Purchase.Remarks);

            // Insert sale details
            objDbCommand.AddInParameter("ProductId", objPurchaseDetail.ProductId);
            objDbCommand.AddInParameter("TruckNumber", objPurchaseDetail.TruckNumber);
            //objDbCommand.AddInParameter("TotalAmount", objPurchaseDetail.ReturnPurchaseAmount);
            objDbCommand.AddInParameter("PurchaseUnitBag", objPurchaseDetail.ReturnPurchaseUnitBag);
            objDbCommand.AddInParameter("PurchaseUnitKG", objPurchaseDetail.ReturnPurchaseUnitKG);
            objDbCommand.AddInParameter("PurchasePrice", objPurchaseDetail.PurchasePrice);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);



            try
            {
                noOfAffacted = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].[uspCreateReturnPurchase]", CommandType.StoredProcedure);
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

        public List<PurchaseModel> GetPurchaseDetail(string consignmentNumber)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<PurchaseModel> objPurchaseModelList = new List<PurchaseModel>();
            PurchaseModel objPurchaseModel;

            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetPurchaseDetilByConsignmentNumber",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objPurchaseModel = new PurchaseModel();
                        this.BuildModelForPurchase(objDbDataReader, objPurchaseModel);
                        objPurchaseModelList.Add(objPurchaseModel);
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

            return objPurchaseModelList;
        }
        public List<PurchaseModel> GetProductNameByConsignment(string consignmentNumber, string truck)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<PurchaseModel> objPurchaseModelList = new List<PurchaseModel>();
            PurchaseModel objPurchaseModel;

            try
            {
                objDbCommand.AddInParameter("ConsignmentNumber", consignmentNumber);
                objDbCommand.AddInParameter("truck", truck);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetProductNameByConsignmentForReturn",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objPurchaseModel = new PurchaseModel();
                        this.BuildModelForPurchase(objDbDataReader, objPurchaseModel);
                        objPurchaseModelList.Add(objPurchaseModel);
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

            return objPurchaseModelList;
        }

        public List<PurchaseModel> GetAllPurchaseInfo()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<PurchaseModel> objPurchaseModelList = new List<PurchaseModel>();
            PurchaseModel objPurchaseModel;
            PurchaseDetail objPurchaseDetail;

            try
            {
              //  objDbCommand.AddInParameter("ConsignmentNumber", consignmentNumber);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[uspGetPurchaseInfoList]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objPurchaseModel = new PurchaseModel();
                        objPurchaseDetail = new PurchaseDetail();
                        this.BuildModelForPurchase(objDbDataReader, objPurchaseModel);
                        //    this.BuildModelForPurchaseDetail(objDbDataReader, objPurchaseDetail);
                        objPurchaseModelList.Add(objPurchaseModel);
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

            return objPurchaseModelList;
        }
    }
}