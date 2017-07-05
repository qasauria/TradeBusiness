using STEnterprise.Areas.Employee.Models;
using STEnterprise.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Employee.BLL
{
    public class SalaryDetailBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;

        private void BuildModelForSalaryDetail(DbDataReader objDataReader, SalaryDetail objSalaryDetail)
        {

            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "SalaryDetailId":
                        if (!Convert.IsDBNull(objDataReader["SalaryDetailId"]))
                        {
                            objSalaryDetail.SalaryDetailId = Convert.ToInt16(objDataReader["SalaryDetailId"]);
                        }
                        break;
                    case "EmployeeId":
                        if (!Convert.IsDBNull(objDataReader["EmployeeId"]))
                        {
                            objSalaryDetail.EmployeeId = Convert.ToInt16(objDataReader["EmployeeId"]);
                        }
                        break;
                    case "EmployeeName":
                        if (!Convert.IsDBNull(objDataReader["EmployeeName"]))
                        {
                            objSalaryDetail.EmployeeName = objDataReader["EmployeeName"].ToString();
                        }
                        break;
                    case "DesignationName":
                        if (!Convert.IsDBNull(objDataReader["DesignationName"]))
                        {
                            objSalaryDetail.DesignationName = objDataReader["DesignationName"].ToString();
                        }
                        break;
                    case "MonthToBePaid":
                        if (!Convert.IsDBNull(objDataReader["MonthToBePaid"]))
                        {
                            objSalaryDetail.MonthToBePaid = objDataReader["MonthToBePaid"].ToString();
                        }
                        break;
                    case "MonthlySalary":
                        if (!Convert.IsDBNull(objDataReader["MonthlySalary"]))
                        {
                            objSalaryDetail.MonthlySalary = Convert.ToDecimal(objDataReader["MonthlySalary"].ToString());
                        }
                        break;

                    case "AdvancedPayment":
                        if (!Convert.IsDBNull(objDataReader["AdvancedPayment"]))
                        {
                            objSalaryDetail.AdvancedPayment = Convert.ToDecimal(objDataReader["AdvancedPayment"].ToString());
                        }
                        break;
                    case "WorkingDays":
                        if (!Convert.IsDBNull(objDataReader["WorkingDays"]))
                        {
                            objSalaryDetail.WorkingDays = Convert.ToByte(objDataReader["WorkingDays"].ToString());
                        }
                        break;
                    case "PerDaySalary":
                        if (!Convert.IsDBNull(objDataReader["PerDaySalary"]))
                        {
                            objSalaryDetail.PerDaySalary = Convert.ToDecimal(objDataReader["PerDaySalary"].ToString());
                        }
                        break;
                    case "AbsentDays":
                        if (!Convert.IsDBNull(objDataReader["AbsentDays"]))
                        {
                            objSalaryDetail.AbsentDays = Convert.ToByte(objDataReader["AbsentDays"].ToString());
                        }
                        break;
                    case "AbsentWisePerDayAmount":
                        if (!Convert.IsDBNull(objDataReader["AbsentWisePerDayAmount"]))
                        {
                            objSalaryDetail.AbsentWisePerDayAmount = Convert.ToDecimal(objDataReader["AbsentWisePerDayAmount"].ToString());
                        }
                        break;
                    case "PayableSalary":
                        if (!Convert.IsDBNull(objDataReader["PayableSalary"]))
                        {
                            objSalaryDetail.PayableSalary = Convert.ToDecimal(objDataReader["PayableSalary"].ToString());
                        }
                        break;
                    case "PaidSalary":
                        if (!Convert.IsDBNull(objDataReader["PaidSalary"]))
                        {
                            objSalaryDetail.PaidSalary = Convert.ToDecimal(objDataReader["PaidSalary"].ToString());
                        }
                        break;
                    case "DueSalary":
                        if (!Convert.IsDBNull(objDataReader["DueSalary"]))
                        {
                            objSalaryDetail.DueSalary = Convert.ToDecimal(objDataReader["DueSalary"].ToString());
                        }
                        break;
                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objSalaryDetail.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objSalaryDetail.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objSalaryDetail.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objSalaryDetail.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objSalaryDetail.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objSalaryDetail.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objSalaryDetail.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objSalaryDetail.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public string SaveSalaryDetail(SalaryDetail objSalaryDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("EmployeeId", objSalaryDetail.EmployeeId);
            objDbCommand.AddInParameter("MonthToBePaid", objSalaryDetail.MonthToBePaid);
            objDbCommand.AddInParameter("AdvancedPayment", objSalaryDetail.AdvancedPayment);
            objDbCommand.AddInParameter("WorkingDays", objSalaryDetail.WorkingDays);
            objDbCommand.AddInParameter("AbsentDays", objSalaryDetail.AbsentDays);
            objDbCommand.AddInParameter("PaidSalary", objSalaryDetail.PaidSalary);
            objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateSalaryDetail", CommandType.StoredProcedure);

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

        public List<SalaryDetail> GetSalaryDetails()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<SalaryDetail> objSalaryDetailList = new List<SalaryDetail>();
            SalaryDetail objSalaryDetail;

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetSalaryDetailsList",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSalaryDetail = new SalaryDetail();
                        this.BuildModelForSalaryDetail(objDbDataReader, objSalaryDetail);
                        objSalaryDetailList.Add(objSalaryDetail);
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

            return objSalaryDetailList;
        }

        public List<EmployeeDetail> GetEmployeeDetailName()
        {

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<EmployeeDetail> objEmployeeDetailNameList = new List<EmployeeDetail>();
            EmployeeDetail objEmployeeDetailName;

            try
            {
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetEmployeeDetailNameList",
                    CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objEmployeeDetailName = new EmployeeDetail();

                        objEmployeeDetailName.EmployeeId = Convert.ToByte(objDbDataReader["EmployeeId"].ToString());
                        objEmployeeDetailName.EmployeeName = objDbDataReader["EmployeeName"].ToString();
                        objEmployeeDetailNameList.Add(objEmployeeDetailName);
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

            return objEmployeeDetailNameList;
        }

        public SalaryDetail GetDesignationAndTotalSalaryById(int? id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<SalaryDetail> objSalaryDetailList = new List<SalaryDetail>();

            SalaryDetail objSalaryDetail = new SalaryDetail();

            try
            {
                objDbCommand.AddInParameter("EmployeeId",id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetDesignationAndTotalSalaryById]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSalaryDetail = new SalaryDetail();
                        //this.BuildModelForSalaryDetail(objDbDataReader, objSalaryDetail);
                        objSalaryDetail.DesignationName = objDbDataReader["DesignationName"].ToString();
                        objSalaryDetail.MonthlySalary = Convert.ToDecimal(objDbDataReader["MonthlySalary"]);
                        objSalaryDetailList.Add(objSalaryDetail);


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

            return objSalaryDetail;
        }


        public SalaryDetail GetSalaryDetailById(int id)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            List<SalaryDetail> objSalaryDetailList = new List<SalaryDetail>();

            SalaryDetail objSalaryDetail = new SalaryDetail();

            try
            {
                objDbCommand.AddInParameter("SalaryDetailId", id);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetSalaryDetailById]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objSalaryDetail = new SalaryDetail();
                        this.BuildModelForSalaryDetail(objDbDataReader, objSalaryDetail);
                        objSalaryDetailList.Add(objSalaryDetail);
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

            return objSalaryDetail;
        }

        public string UpdateSalaryDetail(SalaryDetail objSalaryDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("SalaryDetailId", objSalaryDetail.SalaryDetailId);
            objDbCommand.AddInParameter("AdvancedPayment", objSalaryDetail.AdvancedPayment);
            objDbCommand.AddInParameter("WorkingDays", objSalaryDetail.WorkingDays);
            objDbCommand.AddInParameter("AbsentDays", objSalaryDetail.AbsentDays);
            objDbCommand.AddInParameter("PaidSalary", objSalaryDetail.PaidSalary);
            objDbCommand.AddInParameter("IsActive", objSalaryDetail.IsActive);
            objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateSalaryDetail", CommandType.StoredProcedure);

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

        //public SalaryDetail GetAbsentWisePerdayAmountById(int? id, int Day,int AbsentDays)
        //{
        //    objDataAccess = DataAccess.NewDataAccess();
        //    objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
        //    DbDataReader objDbDataReader = null;

        //    List<SalaryDetail> objSalaryDetailList = new List<SalaryDetail>();

        //    SalaryDetail objSalaryDetail = new SalaryDetail();

        //    try
        //    {
        //        objDbCommand.AddInParameter("EmployeeId", id);
        //        objDbCommand.AddInParameter("PerDaySalary", Day);
        //        objDbCommand.AddInParameter("AbsentDays", AbsentDays);
        //        objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetDayWisePerdaySalaryById]", CommandType.StoredProcedure);

        //        if (objDbDataReader.HasRows)
        //        {
        //            while (objDbDataReader.Read())
        //            {
        //                objSalaryDetail = new SalaryDetail();
        //                objSalaryDetail.PerDaySalary = Convert.ToDecimal(objDbDataReader["PerDaySalary"]);
        //                objSalaryDetailList.Add(objSalaryDetail);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error : " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (objDbDataReader != null)
        //        {
        //            objDbDataReader.Close();
        //        }
        //        objDataAccess.Dispose(objDbCommand);
        //    }

        //    return objSalaryDetail;
        //}
    }
}