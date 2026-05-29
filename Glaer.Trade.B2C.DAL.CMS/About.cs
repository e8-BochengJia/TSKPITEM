using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.CMS
{
    public class About : IAbout
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public About()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddAbout(AboutInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM About";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["About_ID"] = entity.About_ID;
            DrAdd["About_IsActive"] = entity.About_IsActive;
            DrAdd["About_Title"] = entity.About_Title;
            DrAdd["About_Sign"] = entity.About_Sign;
            DrAdd["About_Content"] = entity.About_Content;
            DrAdd["About_Sort"] = entity.About_Sort;
            DrAdd["About_Site"] = entity.About_Site;
            DrAdd["About_IsTop"] = entity.About_IsTop;
            DrAdd["About_SEO_Title"] = entity.About_SEO_Title;
            DrAdd["About_SEO_Keyword"] = entity.About_SEO_Keyword;
            DrAdd["About_SEO_Description"] = entity.About_SEO_Description;

            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditAbout(AboutInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM About WHERE About_ID = " + entity.About_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["About_ID"] = entity.About_ID;
                    DrAdd["About_IsActive"] = entity.About_IsActive;
                    DrAdd["About_Title"] = entity.About_Title;
                    DrAdd["About_Sign"] = entity.About_Sign;
                    DrAdd["About_Content"] = entity.About_Content;
                    DrAdd["About_Sort"] = entity.About_Sort;
                    DrAdd["About_Site"] = entity.About_Site;
                    DrAdd["About_IsTop"] = entity.About_IsTop;
                    DrAdd["About_SEO_Title"] = entity.About_SEO_Title;
                    DrAdd["About_SEO_Keyword"] = entity.About_SEO_Keyword;
                    DrAdd["About_SEO_Description"] = entity.About_SEO_Description;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else {
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
            return true;

        }

        public virtual int DelAbout(int ID)
        {
            string SqlAdd = "DELETE FROM About WHERE About_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual AboutInfo GetAboutByID(int ID)
        {
            AboutInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM About WHERE About_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new AboutInfo();
                    entity.About_ID = Tools.NullInt(RdrList["About_ID"]);
                    entity.About_IsActive = Tools.NullInt(RdrList["About_IsActive"]);
                    entity.About_Title = Tools.NullStr(RdrList["About_Title"]);
                    entity.About_Sign = Tools.NullStr(RdrList["About_Sign"]);
                    entity.About_Content = Tools.NullStr(RdrList["About_Content"]);
                    entity.About_Sort = Tools.NullInt(RdrList["About_Sort"]);
                    entity.About_Site = Tools.NullStr(RdrList["About_Site"]);
                    entity.About_IsTop = Tools.NullInt(RdrList["About_IsTop"]);
                    entity.About_SEO_Title = Tools.NullStr(RdrList["About_SEO_Title"]);
                    entity.About_SEO_Keyword = Tools.NullStr(RdrList["About_SEO_Keyword"]);
                    entity.About_SEO_Description = Tools.NullStr(RdrList["About_SEO_Description"]);
                }
                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual AboutInfo GetAboutBySign(string Sign)
        {
            AboutInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM About WHERE About_Sign = '" + Sign + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new AboutInfo();
                    entity.About_ID = Tools.NullInt(RdrList["About_ID"]);
                    entity.About_IsActive = Tools.NullInt(RdrList["About_IsActive"]);
                    entity.About_Title = Tools.NullStr(RdrList["About_Title"]);
                    entity.About_Sign = Tools.NullStr(RdrList["About_Sign"]);
                    entity.About_Content = Tools.NullStr(RdrList["About_Content"]);
                    entity.About_Sort = Tools.NullInt(RdrList["About_Sort"]);
                    entity.About_Site = Tools.NullStr(RdrList["About_Site"]);
                    entity.About_IsTop = Tools.NullInt(RdrList["About_IsTop"]);
                    entity.About_SEO_Title = Tools.NullStr(RdrList["About_SEO_Title"]);
                    entity.About_SEO_Keyword = Tools.NullStr(RdrList["About_SEO_Keyword"]);
                    entity.About_SEO_Description = Tools.NullStr(RdrList["About_SEO_Description"]);
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

        public virtual IList<AboutInfo> GetAbouts(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<AboutInfo> entitys = null;
            AboutInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "About";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<AboutInfo>();
                    while (RdrList.Read())
                    {
                        entity = new AboutInfo();
                        entity.About_ID = Tools.NullInt(RdrList["About_ID"]);
                        entity.About_IsActive = Tools.NullInt(RdrList["About_IsActive"]);
                        entity.About_Title = Tools.NullStr(RdrList["About_Title"]);
                        entity.About_Sign = Tools.NullStr(RdrList["About_Sign"]);
                        entity.About_Content = Tools.NullStr(RdrList["About_Content"]);
                        entity.About_Sort = Tools.NullInt(RdrList["About_Sort"]);
                        entity.About_Site = Tools.NullStr(RdrList["About_Site"]);
                        entity.About_IsTop = Tools.NullInt(RdrList["About_IsTop"]);
                        entity.About_SEO_Title = Tools.NullStr(RdrList["About_SEO_Title"]);
                        entity.About_SEO_Keyword = Tools.NullStr(RdrList["About_SEO_Keyword"]);
                        entity.About_SEO_Description = Tools.NullStr(RdrList["About_SEO_Description"]);
                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
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

            try {
                Page = new PageInfo();
                SqlTable = "About";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(About_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }

}
