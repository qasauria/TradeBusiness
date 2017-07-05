using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using STEnterprise.Areas.Sale.Models;
using STEnterprise.DAL;

namespace STEnterprise.Areas.Sale.BLL
{
    // created by shovon
    public class CustomerDetailBLL
    {


        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;

        private void BuildModelForCustomerDetail(DbDataReader objDataReader, CustomerDetail objCustomerInfo)
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
                            objCustomerInfo.CustomerId = Convert.ToInt16(objDataReader["CustomerId"]);
                        }
                        break;
                    case "CustomerName":
                        if (!Convert.IsDBNull(objDataReader["CustomerName"]))
                        {
                            objCustomerInfo.CustomerName = objDataReader["CustomerName"].ToString();
                        }
                        break;
                    case "CustomerContactPerson":
                        if (!Convert.IsDBNull(objDataReader["CustomerContactPerson"]))
                        {
                            objCustomerInfo.CustomerContactPerson = objDataReader["CustomerContactPerson"].ToString();
                        }
                        break;
                    case "CustomerAddress":
                        if (!Convert.IsDBNull(objDataReader["CustomerAddress"]))
                        {
                            objCustomerInfo.CustomerAddress = objDataReader["CustomerAddress"].ToString();
                        }
                        break;
                    case "CountryId":
                        if (!Convert.IsDBNull(objDataReader["CountryId"]))
                        {
                            objCustomerInfo.CountryId = Convert.ToInt16(objDataReader["CountryId"]);
                        }
                        break;
                    case "CountryName":
                        if (!Convert.IsDBNull(objDataReader["CountryName"]))
                        {
                            objCustomerInfo.CountryName = objDataReader["CountryName"].ToString();
                        }
                        break;
                    case "CustomerContactNumber":
                        if (!Convert.IsDBNull(objDataReader["CustomerContactNumber"]))
                        {
                            objCustomerInfo.CustomerContactNumber =
                                objDataReader["CustomerContactNumber"].ToString();
                        }
                        break;
                    case "CustomerEmail":
                        if (!Convert.IsDBNull(objDataReader["CustomerEmail"]))
                        {
                            objCustomerInfo.CustomerEmail = objDataReader["CustomerEmail"].ToString();
                        }
                        break;
                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objCustomerInfo.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objCustomerInfo.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objCustomerInfo.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objCustomerInfo.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objCustomerInfo.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objCustomerInfo.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objCustomerInfo.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objCustomerInfo.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }



        // Country Name List for DropDown
        public List<Country> GetCountryName()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<Country> objCountryNameList = new List<Country>();
            Country objCountryName;

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetCountryNameList",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objCountryName = new Country();

                        objCountryName.CountryId = Convert.ToByte(objDbDataReader["CountryId"].ToString());
                        objCountryName.CountryName = objDbDataReader["CountryName"].ToString();
                        objCountryNameList.Add(objCountryName);
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

            return objCountryNameList;
        }


        // Get All Customer
        public List<CustomerDetail> GetAllCustomerInfo()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<CustomerDetail> objCustomerInfoList = new List<CustomerDetail>();
            CustomerDetail objCustomerInfo;

            try
            {
                //objDbCommand.AddInParameter("CreatedBy", SessionUtility.TBSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetCustomerInfoList",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objCustomerInfo = new CustomerDetail();
                        this.BuildModelForCustomerDetail(objDbDataReader, objCustomerInfo);
                        objCustomerInfoList.Add(objCustomerInfo);
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

            return objCustomerInfoList;
        }


        // save customer info
        public string SaveCustomerInfo(CustomerDetail objCustomerInfo)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("CustomerName", objCustomerInfo.CustomerName);
            objDbCommand.AddInParameter("CustomerContactPerson", objCustomerInfo.CustomerContactPerson);
            objDbCommand.AddInParameter("CustomerAddress", objCustomerInfo.CustomerAddress);
            objDbCommand.AddInParameter("CountryId", objCustomerInfo.CountryId);
            objDbCommand.AddInParameter("CustomerContactNumber", objCustomerInfo.CustomerContactNumber);
            objDbCommand.AddInParameter("CustomerEmail", objCustomerInfo.CustomerEmail);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateCustomerDetail", CommandType.StoredProcedure);

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

        public CustomerDetail GetCustomerInfo(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<CustomerDetail> objCustomerDetailList = new List<CustomerDetail>();

            CustomerDetail objCustomerDetail = new CustomerDetail();

            try
            {
                objDbCommand.AddInParameter("CustomerId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetCustomerInfo]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objCustomerDetail = new CustomerDetail();
                        this.BuildModelForCustomerDetail(objDbDataReader, objCustomerDetail);

                        objCustomerDetailList.Add(objCustomerDetail);


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

            return objCustomerDetail;
        }

        public string UpdateCustomerInfo(CustomerDetail objCustomerInfo)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("CustomerId", objCustomerInfo.CustomerId);
            objDbCommand.AddInParameter("CustomerName", objCustomerInfo.CustomerName);
            objDbCommand.AddInParameter("CustomerContactPerson", objCustomerInfo.CustomerContactPerson);
            objDbCommand.AddInParameter("CustomerAddress", objCustomerInfo.CustomerAddress);
            objDbCommand.AddInParameter("CountryId", objCustomerInfo.CountryId);
            objDbCommand.AddInParameter("CustomerContactNumber", objCustomerInfo.CustomerContactNumber);
            objDbCommand.AddInParameter("CustomerEmail", objCustomerInfo.CustomerEmail);
            //objDbCommand.AddInParameter("TradeLicense", objCustomerInfo.TradeLicense);
            objDbCommand.AddInParameter("IsActive", objCustomerInfo.IsActive);
            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateCustomerDetail", CommandType.StoredProcedure);

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

        public string DeleteCustomerInfo(int id)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("CustomerId", id);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteCustomerDetail", CommandType.StoredProcedure);

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