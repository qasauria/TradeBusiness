using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using STEnterprise.Areas.Inventory.Models;
using STEnterprise.DAL;

namespace STEnterprise.Areas.Inventory.BLL
{
    //created by ataur
    public class ProductBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForBuildModelForProduct(DbDataReader objDataReader, Product objProduct)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "ProductId":
                        if (!Convert.IsDBNull(objDataReader["ProductId"]))
                        {
                            objProduct.ProductId = Convert.ToInt16(objDataReader["ProductId"]);
                        }
                        break;
                    case "ProductName":
                        if (!Convert.IsDBNull(objDataReader["ProductName"]))
                        {
                            objProduct.ProductName = objDataReader["ProductName"].ToString();
                        }
                        break;
                    case "ProductDescription":
                        if (!Convert.IsDBNull(objDataReader["ProductDescription"]))
                        {
                            objProduct.ProductDescription = objDataReader["ProductDescription"].ToString();
                        }
                        break;
                    case "TotalBag":
                        if (!Convert.IsDBNull(objDataReader["TotalBag"]))
                        {
                            objProduct.TotalBag = Convert.ToInt16(objDataReader["TotalBag"].ToString());
                        }
                        break;
                    case "Stock":
                        if (!Convert.IsDBNull(objDataReader["Stock"]))
                        {
                            objProduct.Stock = Convert.ToDecimal(objDataReader["Stock"].ToString());
                        }
                        break;

                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objProduct.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objProduct.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objProduct.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objProduct.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objProduct.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objProduct.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objProduct.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objProduct.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        //get all products for index dataTable
        public List<Product> GetAllProduct()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<Product> objProductList = new List<Product>();
            Product objProduct;

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetProductList", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objProduct = new Product();
                        this.BuildModelForBuildModelForProduct(objDbDataReader, objProduct);
                        objProductList.Add(objProduct);
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

            return objProductList;
        }

        //create products
        public string CreateProduct(Product objProduct)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ProductName", objProduct.ProductName);
            objDbCommand.AddInParameter("ProductDescription", objProduct.ProductDescription);
            objDbCommand.AddInParameter("TotalBag", objProduct.TotalBag);
            objDbCommand.AddInParameter("Stock", objProduct.Stock);

            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateProduct", CommandType.StoredProcedure);

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

        // get specific product by id for Edit Action and get Delete action
        public Product GetProduct(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<Product> objProducts = new List<Product>();

            Product objProduct = new Product();

            try
            {
                objDbCommand.AddInParameter("ProductId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetProductById]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objProduct = new Product();
                        this.BuildModelForBuildModelForProduct(objDbDataReader, objProduct);
                        objProducts.Add(objProduct);
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

            return objProduct;
        }

        //Update Product
        public string UpdateProduct(Product objProduct)
        {

            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ProductId", objProduct.ProductId);
            objDbCommand.AddInParameter("ProductName", objProduct.ProductName);
            objDbCommand.AddInParameter("ProductDescription", objProduct.ProductDescription);
            objDbCommand.AddInParameter("TotalBag", objProduct.TotalBag);
            objDbCommand.AddInParameter("Stock", objProduct.Stock);
            objDbCommand.AddInParameter("IsActive", objProduct.IsActive);
            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateProduct", CommandType.StoredProcedure);

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

        //Delete Specific Product 
        public string DeleteProduct(int id)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ProductId", id);

            //objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteProduct", CommandType.StoredProcedure);

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

        public bool GetProductNameIsExist(string productName)
        {
            bool MeasurementNameIsUse = false;
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            try
            {
                objDbCommand.AddInParameter("ProductName", productName);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetProductNameIsExist", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    MeasurementNameIsUse = true;
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
            return MeasurementNameIsUse;
        }
    }
}