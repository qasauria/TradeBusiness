using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using STEnterprise.DAL;
using STEnterprise.Models;

namespace STEnterprise.BLL
{
    public class NavigationBLL
    {
        private IDataAccess objDataAccess;
        private DbCommand objDbCommand;
        private void BuildModelForNavigation(DbDataReader objDataReader, Navigation objNavigation)
        {
            DataTable objDataTable = objDataReader.GetSchemaTable();
            foreach (DataRow dr in objDataTable.Rows)
            {
                String column = dr.ItemArray[0].ToString();
                switch (column)
                {
                    case "MenuId":
                        if (!Convert.IsDBNull(objDataReader["MenuId"]))
                        {
                            objNavigation.MenuId = Convert.ToByte(objDataReader["MenuId"]);
                        }
                        break;
                    case "AreaName":
                        if (!Convert.IsDBNull(objDataReader["AreaName"]))
                        {
                            objNavigation.AreaName = objDataReader["AreaName"].ToString();
                        }
                        break;
                    case "ControllerName":
                        if (!Convert.IsDBNull(objDataReader["ControllerName"]))
                        {
                            objNavigation.ControllerName = objDataReader["ControllerName"].ToString();
                        }
                        break;
                    case "ActionName":
                        if (!Convert.IsDBNull(objDataReader["ActionName"]))
                        {
                            objNavigation.ActionName = objDataReader["ActionName"].ToString();
                        }
                        break;
                    case "MenuName":
                        if (!Convert.IsDBNull(objDataReader["MenuName"]))
                        {
                            objNavigation.MenuName = objDataReader["MenuName"].ToString();
                        }
                        break;
                    case "MenuLevel":
                        if (!Convert.IsDBNull(objDataReader["MenuLevel"]))
                        {
                            objNavigation.MenuLevel = Convert.ToByte(objDataReader["MenuLevel"].ToString());
                        }
                        break;
                    case "ParentId":
                        if (!Convert.IsDBNull(objDataReader["ParentId"]))
                        {
                            objNavigation.ParentId = Convert.ToByte(objDataReader["ParentId"].ToString());
                        }
                        break;
                    case "HasSubMenu":
                        if (!Convert.IsDBNull(objDataReader["HasSubMenu"]))
                        {
                            objNavigation.HasSubMenu = Convert.ToBoolean(objDataReader["HasSubMenu"].ToString());
                        }
                        break;
                    case "IsDefault":
                        if (!Convert.IsDBNull(objDataReader["IsDefault"]))
                        {
                            objNavigation.IsDefault = Convert.ToBoolean(objDataReader["IsDefault"]);
                        }
                        break;
                    case "IsActive":
                        if (!Convert.IsDBNull(objDataReader["IsActive"]))
                        {
                            objNavigation.IsActive = Convert.ToBoolean(objDataReader["IsActive"]);
                        }
                        break;
                    case "UserStatus":
                        if (!Convert.IsDBNull(objDataReader["UserStatus"]))
                        {
                            objNavigation.UserStatus = objDataReader["UserStatus"].ToString();
                        }
                        break;
                    case "CreatedBy":
                        if (!Convert.IsDBNull(objDataReader["CreatedBy"]))
                        {
                            objNavigation.CreatedBy = Convert.ToInt16(objDataReader["CreatedBy"]);
                        }
                        break;
                    case "CreatedDate":
                        if (!Convert.IsDBNull(objDataReader["CreatedDate"]))
                        {
                            objNavigation.CreatedDate = Convert.ToDateTime(objDataReader["CreatedDate"].ToString());
                        }
                        break;
                    case "UpdatedBy":
                        if (!Convert.IsDBNull(objDataReader["UpdatedBy"]))
                        {
                            objNavigation.UpdatedBy = Convert.ToInt16(objDataReader["UpdatedBy"].ToString());
                        }
                        break;
                    case "UpdatedDate":
                        if (!Convert.IsDBNull(objDataReader["UpdatedDate"]))
                        {
                            objNavigation.UpdatedDate = Convert.ToDateTime(objDataReader["UpdatedDate"].ToString());
                        }
                        break;
                    case "SortedBy":
                        if (!Convert.IsDBNull(objDataReader["SortedBy"]))
                        {
                            objNavigation.SortedBy = Convert.ToByte(objDataReader["SortedBy"].ToString());
                        }
                        break;
                    case "Remarks":
                        if (!Convert.IsDBNull(objDataReader["Remarks"]))
                        {
                            objNavigation.Remarks = objDataReader["Remarks"].ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public List<Navigation> GetAllMenuForUserMenuMapping(int? userDetailId, int? roleId)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<Navigation> objMenuList = new List<Navigation>();
            Navigation objNavigation;

            try
            {
                objDbCommand.AddInParameter("LoginId", userDetailId);
                objDbCommand.AddInParameter("RoleId", roleId);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetMenuForUserMenuMapping]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objNavigation = new Navigation();
                        this.BuildModelForNavigation(objDbDataReader, objNavigation);
                        objMenuList.Add(objNavigation);
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

            return objMenuList;
        }

        public List<Navigation> GetAllMenuForRoleMenuMapping(int? role)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<Navigation> objMenuList = new List<Navigation>();
            Navigation objNavigation;

            try
            {
                objDbCommand.AddInParameter("RoleId", role);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].[uspGetMenuForRoleMenuMapping]", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objNavigation = new Navigation();
                        this.BuildModelForNavigation(objDbDataReader, objNavigation);
                        objMenuList.Add(objNavigation);
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

            return objMenuList;
        }

        public bool GetAuthenticMenuId(int id, int userId)
        {
            bool IsAuthenticated = false;
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;

            try
            {
                objDbCommand.AddInParameter("MenuId", id);
                objDbCommand.AddInParameter("UserId", userId);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetAuthenticatedMenu", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    IsAuthenticated = true;
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
            return IsAuthenticated;
        }

        public Navigation GetRequestedMenuId(string controllerName, string actionName)
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            Navigation objNavigationId = null;

            objDbCommand.AddInParameter("ControllerName", controllerName);
            objDbCommand.AddInParameter("ActionName", actionName);

            try
            {


                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetRequestedMenuId", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objNavigationId = new Navigation();
                        this.BuildModelForNavigation(objDbDataReader, objNavigationId);
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

            return objNavigationId;
        }

        public List<Navigation> GetAllMenu()
        {
            objDataAccess = DataAccess.NewDataAccess();
            objDbCommand = objDataAccess.GetCommand(true, IsolationLevel.ReadCommitted);
            DbDataReader objDbDataReader = null;
            List<Navigation> objMenuList = new List<Navigation>();
            Navigation objNavigation;

            try
            {
                objDbCommand.AddInParameter("CreatedBy", SessionUtility.STSessionContainer.UserID);
                objDbDataReader = objDataAccess.ExecuteReader(objDbCommand, "[dbo].uspGetAllMenuList", CommandType.StoredProcedure);

                if (objDbDataReader.HasRows)
                {
                    while (objDbDataReader.Read())
                    {
                        objNavigation = new Navigation();
                        this.BuildModelForNavigation(objDbDataReader, objNavigation);
                        objMenuList.Add(objNavigation);
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

            return objMenuList;
        }
    }
}