using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using STEnterprise.Areas.Purchase.Models;
using STEnterprise.DAL;

namespace STEnterprise.Areas.Purchase.BLL
{
    /// <summary>
    ///  created by shovon
    /// </summary>
    public class TruckDetailBLL
    {

        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForTruckDetail(DbDataReader objDataReader, TruckDetail objTruckDetail)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "TruckDetailId":
                        if (!Convert.IsDBNull(objDataReader["TruckDetailId"]))
                        {
                            objTruckDetail.TruckDetailId = Convert.ToInt32(objDataReader["TruckDetailId"]);
                        }
                        break;
                    case "ConsignmentNumber":
                        if (!Convert.IsDBNull(objDataReader["ConsignmentNumber"]))
                        {
                            objTruckDetail.ConsignmentNumber = objDataReader["ConsignmentNumber"].ToString();
                        }
                        break;
                    case "TruckNumber":
                        if (!Convert.IsDBNull(objDataReader["TruckNumber"]))
                        {
                            objTruckDetail.TruckNumber = objDataReader["TruckNumber"].ToString();
                        }
                        break;
                    case "TruckFare":
                        if (!Convert.IsDBNull(objDataReader["TruckFare"]))
                        {
                            objTruckDetail.TruckFare = Convert.ToDecimal(objDataReader["TruckFare"].ToString());
                        }
                        break;
                    case "AdvancePayment":
                        if (!Convert.IsDBNull(objDataReader["AdvancePayment"]))
                        {
                            objTruckDetail.AdvancePayment = Convert.ToDecimal(objDataReader["AdvancePayment"].ToString());
                        }
                        break;
                    case "LoadingCost":
                        if (!Convert.IsDBNull(objDataReader["LoadingCost"]))
                        {
                            objTruckDetail.LoadingCost = Convert.ToDecimal(objDataReader["LoadingCost"].ToString());
                        }
                        break;
                    case "UnloadingCost":
                        if (!Convert.IsDBNull(objDataReader["UnloadingCost"]))
                        {
                            objTruckDetail.UnloadingCost = Convert.ToDecimal(objDataReader["UnloadingCost"].ToString());
                        }
                        break;
                    case "OtherCost":
                        if (!Convert.IsDBNull(objDataReader["OtherCost"]))
                        {
                            objTruckDetail.OtherCost = Convert.ToDecimal(objDataReader["OtherCost"].ToString());
                        }
                        break;
                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objTruckDetail.IsActive = Convert.ToBoolean(objDataReader["IsActive"].ToString());
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objTruckDetail.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objTruckDetail.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objTruckDetail.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objTruckDetail.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objTruckDetail.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objTruckDetail.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objTruckDetail.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public List<TruckDetail> GetAllTruckDetail()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<TruckDetail> objTruckDetailList = new List<TruckDetail>();
            TruckDetail objTruckDetail;

            try
            {
                //objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetTruckDetailList", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objTruckDetail = new TruckDetail();
                        this.BuildModelForTruckDetail(objDbDataReader, objTruckDetail);
                        objTruckDetailList.Add(objTruckDetail);
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

            return objTruckDetailList;
        }

        public string SaveTruckDetail(TruckDetail objTruckDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("ConsignmentNumber", objTruckDetail.ConsignmentNumber);
            objDbCommand.AddInParameter("TruckNumber", objTruckDetail.TruckNumber);
            objDbCommand.AddInParameter("TruckFare", objTruckDetail.TruckFare);
            objDbCommand.AddInParameter("AdvancePayment", objTruckDetail.AdvancePayment);
            objDbCommand.AddInParameter("LoadingCost", objTruckDetail.LoadingCost);
            objDbCommand.AddInParameter("UnloadingCost", objTruckDetail.UnloadingCost);
            objDbCommand.AddInParameter("OtherCost", objTruckDetail.OtherCost);
            //objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspCreateTruckDetail", CommandType.StoredProcedure);

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

        public string UpdateTruckDetail(TruckDetail objTruckDetail)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("TruckDetailId", objTruckDetail.TruckDetailId);
            objDbCommand.AddInParameter("ConsignmentNumber", objTruckDetail.ConsignmentNumber);
            objDbCommand.AddInParameter("TruckNumber", objTruckDetail.TruckNumber);
            objDbCommand.AddInParameter("TruckFare", objTruckDetail.TruckFare);
            objDbCommand.AddInParameter("AdvancePayment", objTruckDetail.AdvancePayment);
            objDbCommand.AddInParameter("LoadingCost", objTruckDetail.LoadingCost);
            objDbCommand.AddInParameter("UnloadingCost", objTruckDetail.UnloadingCost);
            objDbCommand.AddInParameter("OtherCost", objTruckDetail.OtherCost);
            objDbCommand.AddInParameter("IsActive", objTruckDetail.IsActive);
            //objDbCommand.AddInParameter("UpdatedBy", SessionUtility.STSessionContainer.UserID);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspUpdateTruckDetail", CommandType.StoredProcedure);

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

        public string DeleteTruckDetail(int truckDetailId)
        {
            int noRowCount = 0;

            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.Serializable);
            objDbCommand.AddInParameter("TruckDetailId", truckDetailId);

            try
            {
                noRowCount = objDataAccess.ExecuteNonQuery(objDbCommand, "[dbo].uspDeleteTruckDetail", CommandType.StoredProcedure);

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