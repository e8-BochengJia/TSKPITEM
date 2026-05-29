using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class ActivityUser : IActivityUser
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ActivityUser()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddActivityUser(UserInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM U_User";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["User_ID"] = entity.User_ID;
            DrAdd["User_Type"] = entity.User_Type;
            DrAdd["User_Province"] = entity.User_Province;
            DrAdd["User_Name"] = entity.User_Name;
            DrAdd["User_Password"] = entity.User_Password;
            DrAdd["User_AddPower"] = entity.User_AddPower;
            DrAdd["User_EditPower"] = entity.User_EditPower;
            DrAdd["User_DelPower"] = entity.User_DelPower;
            DrAdd["User_AuditPower"] = entity.User_AuditPower;
            DrAdd["User_AddTime"] = entity.User_AddTime;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditActivityUser(UserInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM U_User WHERE User_ID = " + entity.User_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["User_ID"] = entity.User_ID;
                    DrAdd["User_Type"] = entity.User_Type;
                    DrAdd["User_Province"] = entity.User_Province;
                    DrAdd["User_Name"] = entity.User_Name;
                    DrAdd["User_Password"] = entity.User_Password;
                    DrAdd["User_AddPower"] = entity.User_AddPower;
                    DrAdd["User_EditPower"] = entity.User_EditPower;
                    DrAdd["User_DelPower"] = entity.User_DelPower;
                    DrAdd["User_AuditPower"] = entity.User_AuditPower;
                    DrAdd["User_AddTime"] = entity.User_AddTime;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
            return true;

        }

        public virtual int DelActivityUser(int ID)
        {
            string SqlAdd = "DELETE FROM U_User WHERE User_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual UserInfo GetActivityUserByID(int ID)
        {
            UserInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM U_User WHERE User_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new UserInfo();

                    entity.User_ID = Tools.NullInt(RdrList["User_ID"]);
                    entity.User_Type = Tools.NullInt(RdrList["User_Type"]);
                    entity.User_Province = Tools.NullStr(RdrList["User_Province"]);
                    entity.User_Name = Tools.NullStr(RdrList["User_Name"]);
                    entity.User_Password = Tools.NullStr(RdrList["User_Password"]);
                    entity.User_AddPower = Tools.NullInt(RdrList["User_AddPower"]);
                    entity.User_EditPower = Tools.NullInt(RdrList["User_EditPower"]);
                    entity.User_DelPower = Tools.NullInt(RdrList["User_DelPower"]);
                    entity.User_AuditPower = Tools.NullInt(RdrList["User_AuditPower"]);
                    entity.User_AddTime = Tools.NullDate(RdrList["User_AddTime"]);

                }

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual IList<UserInfo> GetActivityUsers(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<UserInfo> entitys = null;
            UserInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "U_User";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<UserInfo>();
                    while (RdrList.Read())
                    {
                        entity = new UserInfo();
                        entity.User_ID = Tools.NullInt(RdrList["User_ID"]);
                        entity.User_Type = Tools.NullInt(RdrList["User_Type"]);
                        entity.User_Province = Tools.NullStr(RdrList["User_Province"]);
                        entity.User_Name = Tools.NullStr(RdrList["User_Name"]);
                        entity.User_Password = Tools.NullStr(RdrList["User_Password"]);
                        entity.User_AddPower = Tools.NullInt(RdrList["User_AddPower"]);
                        entity.User_EditPower = Tools.NullInt(RdrList["User_EditPower"]);
                        entity.User_DelPower = Tools.NullInt(RdrList["User_DelPower"]);
                        entity.User_AuditPower = Tools.NullInt(RdrList["User_AuditPower"]);
                        entity.User_AddTime = Tools.NullDate(RdrList["User_AddTime"]);

                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "U_User";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(User_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserInfo GetActivityUserByLogin(string name, string password)
        {
            UserInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM U_User WHERE User_Name = '" + name + "' and User_Password='" + password + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new UserInfo();

                    entity.User_ID = Tools.NullInt(RdrList["User_ID"]);
                    entity.User_Type = Tools.NullInt(RdrList["User_Type"]);
                    entity.User_Province = Tools.NullStr(RdrList["User_Province"]);
                    entity.User_Name = Tools.NullStr(RdrList["User_Name"]);
                    entity.User_Password = Tools.NullStr(RdrList["User_Password"]);
                    entity.User_AddPower = Tools.NullInt(RdrList["User_AddPower"]);
                    entity.User_EditPower = Tools.NullInt(RdrList["User_EditPower"]);
                    entity.User_DelPower = Tools.NullInt(RdrList["User_DelPower"]);
                    entity.User_AuditPower = Tools.NullInt(RdrList["User_AuditPower"]);
                    entity.User_AddTime = Tools.NullDate(RdrList["User_AddTime"]);

                }

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }


    }

}
