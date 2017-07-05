using STEnterprise.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using STEnterprise.Areas.Purchase.Models;

namespace STEnterprise.Areas.Purchase.BLL
{
    public class SupplierBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForBuildModelForMeasurement(DbDataReader objDataReader, Supplier objSupplier)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "SupplierId":
                        if (!Convert.IsDBNull(objDataReader["SupplierId"]))
                        {
                            objSupplier.SupplierId = Convert.ToInt16(objDataReader["SupplierId"]);
                        }
                        break;
                    case "SupplierName":
                        if (!Convert.IsDBNull(objDataReader["SupplierName"]))
                        {
                            objSupplier.SupplierName = objDataReader["SupplierName"].ToString();
                        }
                        break;
                    case "SupplierAddress":
                        if (!Convert.IsDBNull(objDataReader["SupplierAddress"]))
                        {
                            objSupplier.SupplierAddress = objDataReader["SupplierAddress"].ToString();
                        }
                        break;
                    case "SupplierContactNo":
                        if (!Convert.IsDBNull(objDataReader["SupplierContactNo"]))
                        {
                            objSupplier.SupplierContactNo = objDataReader["SupplierContactNo"].ToString();
                        }
                        break;
                    case "SupplierEmail":
                        if (!Convert.IsDBNull(objDataReader["SupplierEmail"]))
                        {
                            objSupplier.SupplierEmail = objDataReader["SupplierEmail"].ToString();
                        }
                        break;

                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objSupplier.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objSupplier.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objSupplier.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objSupplier.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objSupplier.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objSupplier.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objSupplier.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objSupplier.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        // get all measurment data for index
        public List<Supplier> GetAllSupplier()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<Supplier> objSupplierList = new List<Supplier>();
            Supplier objSupplier;

            try
            {
                objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetSupplierList", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSupplier = new Supplier();
                        this.BuildModelForBuildModelForMeasurement(objDbDataReader, objSupplier);
                        objSupplierList.Add(objSupplier);
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

            return objSupplierList;
        }
        public string SaveSupplier(Supplier objSupplier)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("SupplierName", objSupplier.SupplierName);
            objDbCommand.AddInParameter("SupplierAddress", objSupplier.SupplierAddress);
            objDbCommand.AddInParameter("SupplierContactNo", objSupplier.SupplierContactNo);
            objDbCommand.AddInParameter("SupplierEmail", objSupplier.SupplierEmail);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateSupplier", CommandType.StoredProcedure);

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
        public bool GetSupplierNameIsExist(string supplierName)
        {
            bool SupplierNameIsUse = false;
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            try
            {
                objDbCommand.AddInParameter("SupplierName", supplierName);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetSupplierNameIsExist", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    SupplierNameIsUse = true;
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
            return SupplierNameIsUse;
        }

        public Supplier GetAllSupplierById(int supplierId)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<Supplier> objSupplierList = new List<Supplier>();

            Supplier objSupplier = new Supplier();

            try
            {
                objDbCommand.AddInParameter("SupplierId", supplierId);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetSupplierById]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSupplier = new Supplier();
                        this.BuildModelForBuildModelForMeasurement(objDbDataReader, objSupplier);
                        objSupplierList.Add(objSupplier);
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

            return objSupplier;
        }
        public string UpdateSupplier(Supplier objSupplier)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("SupplierId", objSupplier.SupplierId);
            objDbCommand.AddInParameter("SupplierName", objSupplier.SupplierName);
            objDbCommand.AddInParameter("SupplierAddress", objSupplier.SupplierAddress);
            objDbCommand.AddInParameter("SupplierContactNo", objSupplier.SupplierContactNo);
            objDbCommand.AddInParameter("SupplierEmail", objSupplier.SupplierEmail);
            objDbCommand.AddInParameter("IsActive", objSupplier.IsActive);
            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateSupplier", CommandType.StoredProcedure);

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

        //get all Supplier by id for delete 
        public Supplier GetSupplier(int supplierId)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<Supplier> objSupplierList = new List<Supplier>();

            Supplier objSupplier = new Supplier();

            try
            {
                objDbCommand.AddInParameter("SupplierId", supplierId);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetSupplierById]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSupplier = new Supplier();
                        this.BuildModelForBuildModelForMeasurement(objDbDataReader, objSupplier);
                        objSupplierList.Add(objSupplier);
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

            return objSupplier;
        }

        //delete all measurement
        public string DeleteSupplier(int supplierId)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("SupplierId", supplierId);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteSupplierById", CommandType.StoredProcedure);

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