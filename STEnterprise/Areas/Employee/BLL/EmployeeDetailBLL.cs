using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using STEnterprise.Areas.Employee.Models;
using STEnterprise.DAL;

namespace STEnterprise.Areas.Employee.BLL
{
    public class EmployeeDetailBLL
    {

        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;

        private void BuildModelForEmployeeDetail(DbDataReader objDataReader, EmployeeDetail objEmployeeInfo)
        {

            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "EmployeeId":
                        if (!Convert.IsDBNull(objDataReader["EmployeeId"]))
                        {
                            objEmployeeInfo.EmployeeId = Convert.ToInt16(objDataReader["EmployeeId"]);
                        }
                        break;
                    case "EmployeeName":
                        if (!Convert.IsDBNull(objDataReader["EmployeeName"]))
                        {
                            objEmployeeInfo.EmployeeName = objDataReader["EmployeeName"].ToString();
                        }
                        break;
                    case "PresentAddress":
                        if (!Convert.IsDBNull(objDataReader["PresentAddress"]))
                        {
                            objEmployeeInfo.PresentAddress = objDataReader["PresentAddress"].ToString();
                        }
                        break;
                    case "PermanentAddress":
                        if (!Convert.IsDBNull(objDataReader["PermanentAddress"]))
                        {
                            objEmployeeInfo.PermanentAddress = objDataReader["PermanentAddress"].ToString();
                        }
                        break;
                    case "DesignationId":
                        if (!Convert.IsDBNull(objDataReader["DesignationId"]))
                        {
                            objEmployeeInfo.DesignationId = Convert.ToByte(objDataReader["DesignationId"]);
                        }
                        break;
                    case "DesignationName":
                        if (!Convert.IsDBNull(objDataReader["DesignationName"]))
                        {
                            objEmployeeInfo.DesignationName = objDataReader["DesignationName"].ToString();
                        }
                        break;
                    case "Phone":
                        if (!Convert.IsDBNull(objDataReader["Phone"]))
                        {
                            objEmployeeInfo.Phone =
                                objDataReader["Phone"].ToString();
                        }
                        break;
                    case "Email":
                        if (!Convert.IsDBNull(objDataReader["Email"]))
                        {
                            objEmployeeInfo.Email = objDataReader["Email"].ToString();
                        }
                        break;
                    case "Age":
                        if (!Convert.IsDBNull(objDataReader["Age"]))
                        {
                            objEmployeeInfo.Age =Convert.ToByte(objDataReader["Age"]) ;
                        }
                        break;
                    case "HireDate":
                        if (!Convert.IsDBNull(objDataReader["HireDate"]))
                        {
                            objEmployeeInfo.HireDate = Convert.ToDateTime(objDataReader["HireDate"].ToString());
                        }
                        break;
                    case "HireDateShow":
                        if (!Convert.IsDBNull(objDataReader["HireDateShow"]))
                        {
                            objEmployeeInfo.HireDateShow = objDataReader["HireDateShow"].ToString();
                        }
                        break;
                    case "NationalId":
                        if (!Convert.IsDBNull(objDataReader["NationalId"]))
                        {
                            objEmployeeInfo.NationalId = objDataReader["NationalId"].ToString();
                        }
                        break;
                    case "MonthlySalary":
                        if (!Convert.IsDBNull(objDataReader["MonthlySalary"]))
                        {
                            objEmployeeInfo.MonthlySalary = Convert.ToDecimal(objDataReader["MonthlySalary"].ToString());
                        }
                        break;
                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objEmployeeInfo.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objEmployeeInfo.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objEmployeeInfo.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objEmployeeInfo.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objEmployeeInfo.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objEmployeeInfo.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objEmployeeInfo.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objEmployeeInfo.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }


        public List<EmployeeDetail> GetAllEmployeeInfo()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<EmployeeDetail> objEmployeeInfoList = new List<EmployeeDetail>();
            EmployeeDetail objEmployeeInfo;

            try
            {
                //objDbCommand.AddInParameter("CreatedBy", SessionUtility.TBSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetEmployeeInfoList",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objEmployeeInfo = new EmployeeDetail();
                        this.BuildModelForEmployeeDetail(objDbDataReader, objEmployeeInfo);
                        objEmployeeInfoList.Add(objEmployeeInfo);
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

            return objEmployeeInfoList;
        }

        public List<Designation> GetDesignationName()
        {

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<Designation> objDesignationNameList = new List<Designation>();
            Designation objDesignationName;

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetDesignationNameList",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objDesignationName = new Designation();

                        objDesignationName.DesignationId = Convert.ToByte(objDbDataReader["DesignationId"].ToString());
                        objDesignationName.DesignationName = objDbDataReader["DesignationName"].ToString();
                        objDesignationNameList.Add(objDesignationName);
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

            return objDesignationNameList;
        }

        public string SaveEmployeeInfo(EmployeeDetail objEmployeeInfo)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("EmployeeName", objEmployeeInfo.EmployeeName);
            objDbCommand.AddInParameter("PresentAddress", objEmployeeInfo.PresentAddress);
            objDbCommand.AddInParameter("PermanentAddress", objEmployeeInfo.PermanentAddress);
            objDbCommand.AddInParameter("DesignationId", objEmployeeInfo.DesignationId);
            objDbCommand.AddInParameter("Phone", objEmployeeInfo.Phone);
            objDbCommand.AddInParameter("Email", objEmployeeInfo.Email);
            objDbCommand.AddInParameter("Age", objEmployeeInfo.Age);
            //var sovon = String.Format("{0:d/M/yyyy}", objEmployeeInfo.HireDate);
            objDbCommand.AddInParameter("HireDate", objEmployeeInfo.HireDate);
            //objDbCommand.AddInParameter("HireDate ", Convert.ToDateTime(sovon));
            objDbCommand.AddInParameter("NationalId ", objEmployeeInfo.NationalId);
            objDbCommand.AddInParameter("MonthlySalary ", objEmployeeInfo.MonthlySalary);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateEmployeeDetail", CommandType.StoredProcedure);

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

        public EmployeeDetail GetEmployeeInfo(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<EmployeeDetail> objEmployeeDetailList = new List<EmployeeDetail>();

            EmployeeDetail objEmployeeDetail = new EmployeeDetail();

            try
            {
                objDbCommand.AddInParameter("EmployeeId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetEmployeeInfo]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objEmployeeDetail = new EmployeeDetail();
                        this.BuildModelForEmployeeDetail(objDbDataReader, objEmployeeDetail);

                        objEmployeeDetailList.Add(objEmployeeDetail);


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

            return objEmployeeDetail;
        }

        public string UpdateEmployeeInfo(EmployeeDetail objEmployeeInfo)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("EmployeeId", objEmployeeInfo.EmployeeId);
            objDbCommand.AddInParameter("EmployeeName", objEmployeeInfo.EmployeeName);
            objDbCommand.AddInParameter("PresentAddress", objEmployeeInfo.PresentAddress);
            objDbCommand.AddInParameter("PermanentAddress", objEmployeeInfo.PermanentAddress);
            objDbCommand.AddInParameter("DesignationId", objEmployeeInfo.DesignationId);
            objDbCommand.AddInParameter("Phone", objEmployeeInfo.Phone);
            objDbCommand.AddInParameter("Email", objEmployeeInfo.Email);
            objDbCommand.AddInParameter("Age", objEmployeeInfo.Age);
            var sovon = String.Format("{0:d/M/yyyy}", objEmployeeInfo.HireDate);
            objDbCommand.AddInParameter("HireDate",Convert.ToDateTime(sovon));
            objDbCommand.AddInParameter("NationalId", objEmployeeInfo.NationalId);
            objDbCommand.AddInParameter("MonthlySalary ", objEmployeeInfo.MonthlySalary);
            objDbCommand.AddInParameter("IsActive", objEmployeeInfo.IsActive);
            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateEmployeeDetail", CommandType.StoredProcedure);

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

        public string DeleteEmployeeInfo(int id)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("EmployeeId", id);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteEmployeeDetail", CommandType.StoredProcedure);

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