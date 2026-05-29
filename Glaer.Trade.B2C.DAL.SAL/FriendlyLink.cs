using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.SAL
{
    public class FriendlyLinkCate : IFriendlyLinkCate
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public FriendlyLinkCate()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddFriendlyLinkCate(FriendlyLinkCateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM FriendlyLink_Cate";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["FriendlyLink_Cate_ID"] = entity.FriendlyLink_Cate_ID;
            DrAdd["FriendlyLink_Cate_Name"] = entity.FriendlyLink_Cate_Name;
            DrAdd["FriendlyLink_Cate_Sort"] = entity.FriendlyLink_Cate_Sort;
            DrAdd["FriendlyLink_Cate_Site"] = entity.FriendlyLink_Cate_Site;
            DrAdd["FriendlyLink_Cate_SEO_Title"] = entity.FriendlyLink_Cate_SEO_Title;
            DrAdd["FriendlyLink_Cate_SEO_Keyword"] = entity.FriendlyLink_Cate_SEO_Keyword;
            DrAdd["FriendlyLink_Cate_SEO_Description"] = entity.FriendlyLink_Cate_SEO_Description;

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

        public virtual bool EditFriendlyLinkCate(FriendlyLinkCateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM FriendlyLink_Cate WHERE FriendlyLink_Cate_ID = " + entity.FriendlyLink_Cate_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["FriendlyLink_Cate_ID"] = entity.FriendlyLink_Cate_ID;
                    DrAdd["FriendlyLink_Cate_Name"] = entity.FriendlyLink_Cate_Name;
                    DrAdd["FriendlyLink_Cate_Sort"] = entity.FriendlyLink_Cate_Sort;
                    DrAdd["FriendlyLink_Cate_Site"] = entity.FriendlyLink_Cate_Site;
                    DrAdd["FriendlyLink_Cate_SEO_Title"] = entity.FriendlyLink_Cate_SEO_Title;
                    DrAdd["FriendlyLink_Cate_SEO_Keyword"] = entity.FriendlyLink_Cate_SEO_Keyword;
                    DrAdd["FriendlyLink_Cate_SEO_Description"] = entity.FriendlyLink_Cate_SEO_Description;
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

        public virtual int DelFriendlyLinkCate(int ID)
        {
            string SqlAdd = "DELETE FROM FriendlyLink_Cate WHERE FriendlyLink_Cate_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual FriendlyLinkCateInfo GetFriendlyLinkCateByID(int ID)
        {
            FriendlyLinkCateInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM FriendlyLink_Cate WHERE FriendlyLink_Cate_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new FriendlyLinkCateInfo();
                    entity.FriendlyLink_Cate_ID = Tools.NullInt(RdrList["FriendlyLink_Cate_ID"]);
                    entity.FriendlyLink_Cate_Name = Tools.NullStr(RdrList["FriendlyLink_Cate_Name"]);
                    entity.FriendlyLink_Cate_Sort = Tools.NullInt(RdrList["FriendlyLink_Cate_Sort"]);
                    entity.FriendlyLink_Cate_Site = Tools.NullStr(RdrList["FriendlyLink_Cate_Site"]);
                    entity.FriendlyLink_Cate_SEO_Title = Tools.NullStr(RdrList["FriendlyLink_Cate_SEO_Title"]);
                    entity.FriendlyLink_Cate_SEO_Keyword = Tools.NullStr(RdrList["FriendlyLink_Cate_SEO_Keyword"]);
                    entity.FriendlyLink_Cate_SEO_Description = Tools.NullStr(RdrList["FriendlyLink_Cate_SEO_Description"]);
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

        public virtual IList<FriendlyLinkCateInfo> GetFriendlyLinkCates(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<FriendlyLinkCateInfo> entitys = null;
            FriendlyLinkCateInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "FriendlyLink_Cate";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<FriendlyLinkCateInfo>();
                    while (RdrList.Read())
                    {
                        entity = new FriendlyLinkCateInfo();
                        entity.FriendlyLink_Cate_ID = Tools.NullInt(RdrList["FriendlyLink_Cate_ID"]);
                        entity.FriendlyLink_Cate_Name = Tools.NullStr(RdrList["FriendlyLink_Cate_Name"]);
                        entity.FriendlyLink_Cate_Sort = Tools.NullInt(RdrList["FriendlyLink_Cate_Sort"]);
                        entity.FriendlyLink_Cate_Site = Tools.NullStr(RdrList["FriendlyLink_Cate_Site"]);
                        entity.FriendlyLink_Cate_SEO_Title = Tools.NullStr(RdrList["FriendlyLink_Cate_SEO_Title"]);
                        entity.FriendlyLink_Cate_SEO_Keyword = Tools.NullStr(RdrList["FriendlyLink_Cate_SEO_Keyword"]);
                        entity.FriendlyLink_Cate_SEO_Description = Tools.NullStr(RdrList["FriendlyLink_Cate_SEO_Description"]);
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
                SqlTable = "FriendlyLink_Cate";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(FriendlyLink_Cate_ID) FROM " + SqlTable + SqlParam;

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

    public class FriendlyLink : IFriendlyLink
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public FriendlyLink()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddFriendlyLink(FriendlyLinkInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM FriendlyLink";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["FriendlyLink_ID"] = entity.FriendlyLink_ID;
            DrAdd["FriendlyLink_CateID"] = entity.FriendlyLink_CateID;
            DrAdd["FriendlyLink_Name"] = entity.FriendlyLink_Name;
            DrAdd["FriendlyLink_Img"] = entity.FriendlyLink_Img;
            DrAdd["FriendlyLink_URL"] = entity.FriendlyLink_URL;
            DrAdd["FriendlyLink_IsActive"] = entity.FriendlyLink_IsActive;
            DrAdd["FriendlyLink_IsImg"] = entity.FriendlyLink_IsImg;
            DrAdd["FriendlyLink_Site"] = entity.FriendlyLink_Site;
            DrAdd["FriendlyLink_Sort"] = entity.FriendlyLink_Sort;

            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditFriendlyLink(FriendlyLinkInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM FriendlyLink WHERE FriendlyLink_ID = " + entity.FriendlyLink_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)  {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["FriendlyLink_ID"] = entity.FriendlyLink_ID;
                    DrAdd["FriendlyLink_CateID"] = entity.FriendlyLink_CateID;
                    DrAdd["FriendlyLink_Name"] = entity.FriendlyLink_Name;
                    DrAdd["FriendlyLink_Img"] = entity.FriendlyLink_Img;
                    DrAdd["FriendlyLink_URL"] = entity.FriendlyLink_URL;
                    DrAdd["FriendlyLink_IsActive"] = entity.FriendlyLink_IsActive;
                    DrAdd["FriendlyLink_IsImg"] = entity.FriendlyLink_IsImg;
                    DrAdd["FriendlyLink_Site"] = entity.FriendlyLink_Site;
                    DrAdd["FriendlyLink_Sort"] = entity.FriendlyLink_Sort;
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

        public virtual int DelFriendlyLink(int ID)
        {
            string SqlAdd = "DELETE FROM FriendlyLink WHERE FriendlyLink_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual FriendlyLinkInfo GetFriendlyLinkByID(int ID)
        {
            FriendlyLinkInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM FriendlyLink WHERE FriendlyLink_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new FriendlyLinkInfo();
                    entity.FriendlyLink_ID = Tools.NullInt(RdrList["FriendlyLink_ID"]);
                    entity.FriendlyLink_CateID = Tools.NullInt(RdrList["FriendlyLink_CateID"]);
                    entity.FriendlyLink_Name = Tools.NullStr(RdrList["FriendlyLink_Name"]);
                    entity.FriendlyLink_Img = Tools.NullStr(RdrList["FriendlyLink_Img"]);
                    entity.FriendlyLink_URL = Tools.NullStr(RdrList["FriendlyLink_URL"]);
                    entity.FriendlyLink_IsActive = Tools.NullInt(RdrList["FriendlyLink_IsActive"]);
                    entity.FriendlyLink_IsImg = Tools.NullInt(RdrList["FriendlyLink_IsImg"]);
                    entity.FriendlyLink_Site = Tools.NullStr(RdrList["FriendlyLink_Site"]);
                    entity.FriendlyLink_Sort = Tools.NullInt(RdrList["FriendlyLink_Sort"]);
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

        public virtual IList<FriendlyLinkInfo> GetFriendlyLinks(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<FriendlyLinkInfo> entitys = null;
            FriendlyLinkInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "FriendlyLink";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<FriendlyLinkInfo>();
                    while (RdrList.Read())
                    {
                        entity = new FriendlyLinkInfo();
                        entity.FriendlyLink_ID = Tools.NullInt(RdrList["FriendlyLink_ID"]);
                        entity.FriendlyLink_CateID = Tools.NullInt(RdrList["FriendlyLink_CateID"]);
                        entity.FriendlyLink_Name = Tools.NullStr(RdrList["FriendlyLink_Name"]);
                        entity.FriendlyLink_Img = Tools.NullStr(RdrList["FriendlyLink_Img"]);
                        entity.FriendlyLink_URL = Tools.NullStr(RdrList["FriendlyLink_URL"]);
                        entity.FriendlyLink_IsActive = Tools.NullInt(RdrList["FriendlyLink_IsActive"]);
                        entity.FriendlyLink_IsImg = Tools.NullInt(RdrList["FriendlyLink_IsImg"]);
                        entity.FriendlyLink_Site = Tools.NullStr(RdrList["FriendlyLink_Site"]);
                        entity.FriendlyLink_Sort = Tools.NullInt(RdrList["FriendlyLink_Sort"]);

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
                SqlTable = "FriendlyLink";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(FriendlyLink_ID) FROM " + SqlTable + SqlParam;

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
