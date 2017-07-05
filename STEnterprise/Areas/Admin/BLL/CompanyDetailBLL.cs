using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using STEnterprise.Areas.Admin.Models;
using STEnterprise.DAL;

namespace STEnterprise.Areas.Admin.BLL
{
    //created by ataur
    public class CompanyDetailBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;

        private void BuildModelForCompanyDetail(DbDataReader objDataReader, CompanyDetail objCompanyDetail)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "CompanyId":
                        if (!Convert.IsDBNull(objDataReader["CompanyId"]))
                        {
                            objCompanyDetail.CompanyId = Convert.ToInt16(objDataReader["CompanyId"]);
                        }
                        break;
                    case "CompanyName":
                        if (!Convert.IsDBNull(objDataReader["CompanyName"]))
                        {
                            objCompanyDetail.CompanyName = objDataReader["CompanyName"].ToString();
                        }
                        break;
                    //case "CompanyNameIsUse":
                    //    if (!Convert.IsDBNull(objDataReader["CompanyNameIsUse"]))
                    //    {
                    //        objCompanyDetail.CompanyNameIsUse = Convert.ToBoolean(objDataReader["CompanyNameIsUse"]);
                    //    }
                    //    break;
                    case "Logo":
                        if (!Convert.IsDBNull(objDataReader["Logo"]))
                        {
                            objCompanyDetail.Logo = (byte[])objDataReader["Logo"];
                        }
                        break;
                    case "Address":
                        if (!Convert.IsDBNull(objDataReader["Address"]))
                        {
                            objCompanyDetail.Address = objDataReader["Address"].ToString();
                        }
                        break;
                    case "Phone":
                        if (!Convert.IsDBNull(objDataReader["Phone"]))
                        {
                            objCompanyDetail.Phone = objDataReader["Phone"].ToString();
                        }
                        break;
                    case "Fax":
                        if (!Convert.IsDBNull(objDataReader["Fax"]))
                        {
                            objCompanyDetail.Fax = objDataReader["Fax"].ToString();
                        }
                        break;
                    case "Email":
                        if (!Convert.IsDBNull(objDataReader["Email"]))
                        {
                            objCompanyDetail.Email = objDataReader["Email"].ToString();
                        }
                        break;
                    case "TinCertificate":
                        if (!Convert.IsDBNull(objDataReader["TinCertificate"]))
                        {
                            objCompanyDetail.TinCertificate = objDataReader["TinCertificate"].ToString();
                        }
                        break;
                    case "VatRegNumber":
                        if (!Convert.IsDBNull(objDataReader["VatRegNumber"]))
                        {
                            objCompanyDetail.VatRegNumber = objDataReader["VatRegNumber"].ToString();
                        }
                        break;
                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objCompanyDetail.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objCompanyDetail.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objCompanyDetail.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objCompanyDetail.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objCompanyDetail.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objCompanyDetail.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objCompanyDetail.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objCompanyDetail.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //get all company information for index datatable
        public List<CompanyDetail> GetCompanyInfo()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<CompanyDetail> objCompanyInfoList = new List<CompanyDetail>();
            CompanyDetail objCompanyDetail;
            try
            {
                objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetCompanyDetailList",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objCompanyDetail = new CompanyDetail();
                        this.BuildModelForCompanyDetail(objDbDataReader, objCompanyDetail);
                        objCompanyInfoList.Add(objCompanyDetail);
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
            return objCompanyInfoList;
        }

        //save company information
        public string SaveCompanyInfo(CompanyDetail objCompanyDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("CompanyName", objCompanyDetail.CompanyName);
            //objDbCommand.AddInParameter("Logo", objCompanyDetail.Logo);
            objDbCommand.AddInParameter("Address", objCompanyDetail.Address);
            objDbCommand.AddInParameter("Phone", objCompanyDetail.Phone);
            objDbCommand.AddInParameter("Fax", objCompanyDetail.Fax);
            objDbCommand.AddInParameter("Email", objCompanyDetail.Email);
            objDbCommand.AddInParameter("TinCertificate", objCompanyDetail.TinCertificate);
            objDbCommand.AddInParameter("VatRegNumber", objCompanyDetail.VatRegNumber);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateCompanyDetailInfo",
                    CommandType.StoredProcedure);

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

        //get company detail by id for Edit and Delete action
        public CompanyDetail GetCompanyInfo(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<CompanyDetail> objCompanyDetails = new List<CompanyDetail>();

            CompanyDetail objCompanyDetail = new CompanyDetail();

            try
            {
                objDbCommand.AddInParameter("CompanyId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetCompanyById]",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objCompanyDetail = new CompanyDetail();
                        this.BuildModelForCompanyDetail(objDbDataReader, objCompanyDetail);
                        objCompanyDetails.Add(objCompanyDetail);
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

            return objCompanyDetail;
        }

        //update company detail 
        public string UpdateCompanyDetail(CompanyDetail objCompanyDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("CompanyId", objCompanyDetail.CompanyId);
            objDbCommand.AddInParameter("CompanyName", objCompanyDetail.CompanyName);
            objDbCommand.AddInParameter("Logo", objCompanyDetail.Logo);
            objDbCommand.AddInParameter("Address", objCompanyDetail.Address);
            objDbCommand.AddInParameter("Phone", objCompanyDetail.Phone);
            objDbCommand.AddInParameter("Fax", objCompanyDetail.Fax);
            objDbCommand.AddInParameter("Email", objCompanyDetail.Email);
            objDbCommand.AddInParameter("TinCertificate", objCompanyDetail.TinCertificate);
            objDbCommand.AddInParameter("VatRegNumber", objCompanyDetail.VatRegNumber);
            objDbCommand.AddInParameter("IsActive", objCompanyDetail.IsActive);
            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateCompanyDetailInfo",
                    CommandType.StoredProcedure);

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

        //Delete Company information
        public string DeleteCompanyDetail(int id)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("CompanyId", id);
            
            //objDbCommand.AddInParameter("CreatedBy", SessionUtility.TBSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteCompanyDetailInfo",
                    CommandType.StoredProcedure);

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