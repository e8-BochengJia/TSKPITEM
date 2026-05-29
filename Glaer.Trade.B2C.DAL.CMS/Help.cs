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
    public class HelpCate : IHelpCate
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public HelpCate()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddHelpCate(HelpCateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Help_Cate";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Help_Cate_ID"] = entity.Help_Cate_ID;
            DrAdd["Help_Cate_ParentID"] = entity.Help_Cate_ParentID;
            DrAdd["Help_Cate_Name"] = entity.Help_Cate_Name;
            DrAdd["Help_Cate_Sort"] = entity.Help_Cate_Sort;
            DrAdd["Help_Cate_Site"] = entity.Help_Cate_Site;
            DrAdd["Help_Cate_SEO_Title"] = entity.Help_Cate_SEO_Title;
            DrAdd["Help_Cate_SEO_Keyword"] = entity.Help_Cate_SEO_Keyword;
            DrAdd["Help_Cate_SEO_Description"] = entity.Help_Cate_SEO_Description;

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

        public virtual bool EditHelpCate(HelpCateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Help_Cate WHERE Help_Cate_ID = " + entity.Help_Cate_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Help_Cate_ID"] = entity.Help_Cate_ID;
                    DrAdd["Help_Cate_ParentID"] = entity.Help_Cate_ParentID;
                    DrAdd["Help_Cate_Name"] = entity.Help_Cate_Name;
                    DrAdd["Help_Cate_Sort"] = entity.Help_Cate_Sort;
                    DrAdd["Help_Cate_Site"] = entity.Help_Cate_Site;
                    DrAdd["Help_Cate_SEO_Title"] = entity.Help_Cate_SEO_Title;
                    DrAdd["Help_Cate_SEO_Keyword"] = entity.Help_Cate_SEO_Keyword;
                    DrAdd["Help_Cate_SEO_Description"] = entity.Help_Cate_SEO_Description;

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

        public virtual int DelHelpCate(int ID)
        {
            string SqlAdd = "DELETE FROM Help_Cate WHERE Help_Cate_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual HelpCateInfo GetHelpCateByID(int ID)
        {
            HelpCateInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Help_Cate WHERE Help_Cate_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new HelpCateInfo();
                    entity.Help_Cate_ID = Tools.NullInt(RdrList["Help_Cate_ID"]);
                    entity.Help_Cate_ParentID = Tools.NullInt(RdrList["Help_Cate_ParentID"]);
                    entity.Help_Cate_Name = Tools.NullStr(RdrList["Help_Cate_Name"]);
                    entity.Help_Cate_Sort = Tools.NullInt(RdrList["Help_Cate_Sort"]);
                    entity.Help_Cate_Site = Tools.NullStr(RdrList["Help_Cate_Site"]);
                    entity.Help_Cate_SEO_Title = Tools.NullStr(RdrList["Help_Cate_SEO_Title"]);
                    entity.Help_Cate_SEO_Keyword = Tools.NullStr(RdrList["Help_Cate_SEO_Keyword"]);
                    entity.Help_Cate_SEO_Description = Tools.NullStr(RdrList["Help_Cate_SEO_Description"]);

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

        public virtual IList<HelpCateInfo> GetHelpCates(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<HelpCateInfo> entitys = null;
            HelpCateInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Help_Cate";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<HelpCateInfo>();
                    while (RdrList.Read())
                    {
                        entity = new HelpCateInfo();
                        entity.Help_Cate_ID = Tools.NullInt(RdrList["Help_Cate_ID"]);
                        entity.Help_Cate_ParentID = Tools.NullInt(RdrList["Help_Cate_ParentID"]);
                        entity.Help_Cate_Name = Tools.NullStr(RdrList["Help_Cate_Name"]);
                        entity.Help_Cate_Sort = Tools.NullInt(RdrList["Help_Cate_Sort"]);
                        entity.Help_Cate_Site = Tools.NullStr(RdrList["Help_Cate_Site"]);
                        entity.Help_Cate_SEO_Title = Tools.NullStr(RdrList["Help_Cate_SEO_Title"]);
                        entity.Help_Cate_SEO_Keyword = Tools.NullStr(RdrList["Help_Cate_SEO_Keyword"]);
                        entity.Help_Cate_SEO_Description = Tools.NullStr(RdrList["Help_Cate_SEO_Description"]);

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

            try
            {
                Page = new PageInfo();
                SqlTable = "Help_Cate";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Help_Cate_ID) FROM " + SqlTable + SqlParam;

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

    }

    public class Help : IHelp
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Help()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddHelp(HelpInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Help";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Help_ID"] = entity.Help_ID;
            DrAdd["Help_CateID"] = entity.Help_CateID;
            DrAdd["Help_IsFAQ"] = entity.Help_IsFAQ;
            DrAdd["Help_IsActive"] = entity.Help_IsActive;
            DrAdd["Help_Title"] = entity.Help_Title;
            DrAdd["Help_Content"] = entity.Help_Content;
            DrAdd["Help_Sort"] = entity.Help_Sort;
            DrAdd["Help_Site"] = entity.Help_Site;
            DrAdd["Help_SEO_Title"] = entity.Help_SEO_Title;
            DrAdd["Help_SEO_Keyword"] = entity.Help_SEO_Keyword;
            DrAdd["Help_SEO_Description"] = entity.Help_SEO_Description;
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

        public virtual bool EditHelp(HelpInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Help WHERE Help_ID = " + entity.Help_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Help_ID"] = entity.Help_ID;
                    DrAdd["Help_CateID"] = entity.Help_CateID;
                    DrAdd["Help_IsFAQ"] = entity.Help_IsFAQ;
                    DrAdd["Help_IsActive"] = entity.Help_IsActive;
                    DrAdd["Help_Title"] = entity.Help_Title;
                    DrAdd["Help_Content"] = entity.Help_Content;
                    DrAdd["Help_Sort"] = entity.Help_Sort;
                    DrAdd["Help_Site"] = entity.Help_Site;
                    DrAdd["Help_SEO_Title"] = entity.Help_SEO_Title;
                    DrAdd["Help_SEO_Keyword"] = entity.Help_SEO_Keyword;
                    DrAdd["Help_SEO_Description"] = entity.Help_SEO_Description;

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

        public virtual int DelHelp(int ID)
        {
            string SqlAdd = "DELETE FROM Help WHERE Help_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual HelpInfo GetHelpByID(int ID)
        {
            HelpInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Help WHERE Help_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new HelpInfo();

                    entity.Help_ID = Tools.NullInt(RdrList["Help_ID"]);
                    entity.Help_CateID = Tools.NullInt(RdrList["Help_CateID"]);
                    entity.Help_IsFAQ = Tools.NullInt(RdrList["Help_IsFAQ"]);
                    entity.Help_IsActive = Tools.NullInt(RdrList["Help_IsActive"]);
                    entity.Help_Title = Tools.NullStr(RdrList["Help_Title"]);
                    entity.Help_Content = Tools.NullStr(RdrList["Help_Content"]);
                    entity.Help_Sort = Tools.NullInt(RdrList["Help_Sort"]);
                    entity.Help_Site = Tools.NullStr(RdrList["Help_Site"]);
                    entity.Help_SEO_Title = Tools.NullStr(RdrList["Help_SEO_Title"]);
                    entity.Help_SEO_Keyword = Tools.NullStr(RdrList["Help_SEO_Keyword"]);
                    entity.Help_SEO_Description = Tools.NullStr(RdrList["Help_SEO_Description"]);

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

        public virtual IList<HelpInfo> GetHelps(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<HelpInfo> entitys = null;
            HelpInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Help";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<HelpInfo>();
                    while (RdrList.Read())
                    {
                        entity = new HelpInfo();
                        entity.Help_ID = Tools.NullInt(RdrList["Help_ID"]);
                        entity.Help_CateID = Tools.NullInt(RdrList["Help_CateID"]);
                        entity.Help_IsFAQ = Tools.NullInt(RdrList["Help_IsFAQ"]);
                        entity.Help_IsActive = Tools.NullInt(RdrList["Help_IsActive"]);
                        entity.Help_Title = Tools.NullStr(RdrList["Help_Title"]);
                        entity.Help_Content = Tools.NullStr(RdrList["Help_Content"]);
                        entity.Help_Sort = Tools.NullInt(RdrList["Help_Sort"]);
                        entity.Help_Site = Tools.NullStr(RdrList["Help_Site"]);
                        entity.Help_SEO_Title = Tools.NullStr(RdrList["Help_SEO_Title"]);
                        entity.Help_SEO_Keyword = Tools.NullStr(RdrList["Help_SEO_Keyword"]);
                        entity.Help_SEO_Description = Tools.NullStr(RdrList["Help_SEO_Description"]);

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
                SqlTable = "Help";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Help_ID) FROM " + SqlTable + SqlParam;

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

    }


}
