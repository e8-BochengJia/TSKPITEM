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
    public class NoticeCate : INoticeCate
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public NoticeCate()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddNoticeCate(NoticeCateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Notice_Cate";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Notice_Cate_ID"] = entity.Notice_Cate_ID;
            DrAdd["Notice_Cate_Name"] = entity.Notice_Cate_Name;
            DrAdd["Notice_Cate_Sort"] = entity.Notice_Cate_Sort;
            DrAdd["Notice_Cate_Site"] = entity.Notice_Cate_Site;
            DrAdd["Notice_Cate_SEO_Title"] = entity.Notice_Cate_SEO_Title;
            DrAdd["Notice_Cate_SEO_Keyword"] = entity.Notice_Cate_SEO_Keyword;
            DrAdd["Notice_Cate_SEO_Description"] = entity.Notice_Cate_SEO_Description;

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

        public virtual bool EditNoticeCate(NoticeCateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Notice_Cate WHERE Notice_Cate_ID = " + entity.Notice_Cate_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Notice_Cate_ID"] = entity.Notice_Cate_ID;
                    DrAdd["Notice_Cate_Name"] = entity.Notice_Cate_Name;
                    DrAdd["Notice_Cate_Sort"] = entity.Notice_Cate_Sort;
                    DrAdd["Notice_Cate_Site"] = entity.Notice_Cate_Site;
                    DrAdd["Notice_Cate_SEO_Title"] = entity.Notice_Cate_SEO_Title;
                    DrAdd["Notice_Cate_SEO_Keyword"] = entity.Notice_Cate_SEO_Keyword;
                    DrAdd["Notice_Cate_SEO_Description"] = entity.Notice_Cate_SEO_Description;

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

        public virtual int DelNoticeCate(int ID)
        {
            string SqlAdd = "DELETE FROM Notice_Cate WHERE Notice_Cate_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual NoticeCateInfo GetNoticeCateByID(int ID)
        {
            NoticeCateInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Notice_Cate WHERE Notice_Cate_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new NoticeCateInfo();

                    entity.Notice_Cate_ID = Tools.NullInt(RdrList["Notice_Cate_ID"]);
                    entity.Notice_Cate_Name = Tools.NullStr(RdrList["Notice_Cate_Name"]);
                    entity.Notice_Cate_Sort = Tools.NullInt(RdrList["Notice_Cate_Sort"]);
                    entity.Notice_Cate_Site = Tools.NullStr(RdrList["Notice_Cate_Site"]);
                    entity.Notice_Cate_SEO_Title = Tools.NullStr(RdrList["Notice_Cate_SEO_Title"]);
                    entity.Notice_Cate_SEO_Keyword = Tools.NullStr(RdrList["Notice_Cate_SEO_Keyword"]);
                    entity.Notice_Cate_SEO_Description = Tools.NullStr(RdrList["Notice_Cate_SEO_Description"]);
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

        public virtual IList<NoticeCateInfo> GetNoticeCates(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<NoticeCateInfo> entitys = null;
            NoticeCateInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Notice_Cate";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<NoticeCateInfo>();
                    while (RdrList.Read())
                    {
                        entity = new NoticeCateInfo();
                        entity.Notice_Cate_ID = Tools.NullInt(RdrList["Notice_Cate_ID"]);
                        entity.Notice_Cate_Name = Tools.NullStr(RdrList["Notice_Cate_Name"]);
                        entity.Notice_Cate_Sort = Tools.NullInt(RdrList["Notice_Cate_Sort"]);
                        entity.Notice_Cate_Site = Tools.NullStr(RdrList["Notice_Cate_Site"]);
                        entity.Notice_Cate_SEO_Title = Tools.NullStr(RdrList["Notice_Cate_SEO_Title"]);
                        entity.Notice_Cate_SEO_Keyword = Tools.NullStr(RdrList["Notice_Cate_SEO_Keyword"]);
                        entity.Notice_Cate_SEO_Description = Tools.NullStr(RdrList["Notice_Cate_SEO_Description"]);

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
                SqlTable = "Notice_Cate";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Notice_Cate_ID) FROM " + SqlTable + SqlParam;

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

    public class Notice : INotice
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Notice()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddNotice(NoticeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Notice";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Notice_ID"] = entity.Notice_ID;
            DrAdd["Notice_Cate"] = entity.Notice_Cate;
            DrAdd["Notice_Ishot"] = entity.Notice_IsHot;
            DrAdd["Notice_IsAudit"] = entity.Notice_IsAudit;
            DrAdd["Notice_SysUserID"] = entity.Notice_SysUserID;
            DrAdd["Notice_SellerID"] = entity.Notice_SellerID;
            DrAdd["Notice_Title"] = entity.Notice_Title;
            DrAdd["Notice_Content"] = entity.Notice_Content;
            DrAdd["Notice_Addtime"] = entity.Notice_Addtime;
            DrAdd["Notice_Site"] = entity.Notice_Site;
            DrAdd["Notice_SEO_Title"] = entity.Notice_SEO_Title;
            DrAdd["Notice_SEO_Keyword"] = entity.Notice_SEO_Keyword;
            DrAdd["Notice_SEO_Description"] = entity.Notice_SEO_Description;
            DrAdd["Notice_ShowTime"] = entity.Notice_ShowTime;

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

        public virtual bool EditNotice(NoticeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Notice WHERE Notice_ID = " + entity.Notice_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Notice_ID"] = entity.Notice_ID;
                    DrAdd["Notice_Cate"] = entity.Notice_Cate;
                    DrAdd["Notice_Ishot"] = entity.Notice_IsHot;
                    DrAdd["Notice_IsAudit"] = entity.Notice_IsAudit;
                    DrAdd["Notice_SysUserID"] = entity.Notice_SysUserID;
                    DrAdd["Notice_SellerID"] = entity.Notice_SellerID;
                    DrAdd["Notice_Title"] = entity.Notice_Title;
                    DrAdd["Notice_Content"] = entity.Notice_Content;
                    DrAdd["Notice_Addtime"] = entity.Notice_Addtime;
                    DrAdd["Notice_Site"] = entity.Notice_Site;
                    DrAdd["Notice_SEO_Title"] = entity.Notice_SEO_Title;
                    DrAdd["Notice_SEO_Keyword"] = entity.Notice_SEO_Keyword;
                    DrAdd["Notice_SEO_Description"] = entity.Notice_SEO_Description;
                    DrAdd["Notice_ShowTime"] = entity.Notice_ShowTime;

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

        public virtual int DelNotice(int ID)
        {
            string SqlAdd = "DELETE FROM Notice WHERE Notice_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual NoticeInfo GetNoticeByID(int ID)
        {
            NoticeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Notice WHERE Notice_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new NoticeInfo();

                    entity.Notice_ID = Tools.NullInt(RdrList["Notice_ID"]);
                    entity.Notice_Cate = Tools.NullInt(RdrList["Notice_Cate"]);
                    entity.Notice_IsHot = Tools.NullInt(RdrList["Notice_Ishot"]);
                    entity.Notice_IsAudit = Tools.NullInt(RdrList["Notice_IsAudit"]);
                    entity.Notice_SysUserID = Tools.NullInt(RdrList["Notice_SysUserID"]);
                    entity.Notice_SellerID = Tools.NullInt(RdrList["Notice_SellerID"]);
                    entity.Notice_Title = Tools.NullStr(RdrList["Notice_Title"]);
                    entity.Notice_Content = Tools.NullStr(RdrList["Notice_Content"]);
                    entity.Notice_Addtime = Tools.NullDate(RdrList["Notice_Addtime"]);
                    entity.Notice_Site = Tools.NullStr(RdrList["Notice_Site"]);
                    entity.Notice_SEO_Title = Tools.NullStr(RdrList["Notice_SEO_Title"]);
                    entity.Notice_SEO_Keyword = Tools.NullStr(RdrList["Notice_SEO_Keyword"]);
                    entity.Notice_SEO_Description = Tools.NullStr(RdrList["Notice_SEO_Description"]);
                    entity.Notice_ShowTime = Tools.NullDate(RdrList["Notice_ShowTime"]);
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

        public virtual IList<NoticeInfo> GetNotices(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<NoticeInfo> entitys = null;
            NoticeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Notice";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<NoticeInfo>();
                    while (RdrList.Read())
                    {
                        entity = new NoticeInfo();
                        entity.Notice_ID = Tools.NullInt(RdrList["Notice_ID"]);
                        entity.Notice_Cate = Tools.NullInt(RdrList["Notice_Cate"]);
                        entity.Notice_IsHot = Tools.NullInt(RdrList["Notice_Ishot"]);
                        entity.Notice_IsAudit = Tools.NullInt(RdrList["Notice_IsAudit"]);
                        entity.Notice_SysUserID = Tools.NullInt(RdrList["Notice_SysUserID"]);
                        entity.Notice_SellerID = Tools.NullInt(RdrList["Notice_SellerID"]);
                        entity.Notice_Title = Tools.NullStr(RdrList["Notice_Title"]);
                        entity.Notice_Content = Tools.NullStr(RdrList["Notice_Content"]);
                        entity.Notice_Addtime = Tools.NullDate(RdrList["Notice_Addtime"]);
                        entity.Notice_Site = Tools.NullStr(RdrList["Notice_Site"]);
                        entity.Notice_SEO_Title = Tools.NullStr(RdrList["Notice_SEO_Title"]);
                        entity.Notice_SEO_Keyword = Tools.NullStr(RdrList["Notice_SEO_Keyword"]);
                        entity.Notice_SEO_Description = Tools.NullStr(RdrList["Notice_SEO_Description"]);
                        entity.Notice_ShowTime = Tools.NullDate(RdrList["Notice_ShowTime"]);

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

        public virtual IList<NoticeInfo> GetNoticeList(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<NoticeInfo> entitys = null;
            NoticeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Notice";
                SqlField = "Notice_ID,Notice_Cate,Notice_Ishot,Notice_IsAudit,Notice_SysUserID,Notice_SellerID,Notice_Title,Notice_Addtime,Notice_Site,Notice_SEO_Title,Notice_SEO_Keyword,Notice_SEO_Description,Notice_ShowTime";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<NoticeInfo>();
                    while (RdrList.Read())
                    {
                        entity = new NoticeInfo();
                        entity.Notice_ID = Tools.NullInt(RdrList["Notice_ID"]);
                        entity.Notice_Cate = Tools.NullInt(RdrList["Notice_Cate"]);
                        entity.Notice_IsHot = Tools.NullInt(RdrList["Notice_Ishot"]);
                        entity.Notice_IsAudit = Tools.NullInt(RdrList["Notice_IsAudit"]);
                        entity.Notice_SysUserID = Tools.NullInt(RdrList["Notice_SysUserID"]);
                        entity.Notice_SellerID = Tools.NullInt(RdrList["Notice_SellerID"]);
                        entity.Notice_Title = Tools.NullStr(RdrList["Notice_Title"]);
                        entity.Notice_Content = "";
                        entity.Notice_Addtime = Tools.NullDate(RdrList["Notice_Addtime"]);
                        entity.Notice_Site = Tools.NullStr(RdrList["Notice_Site"]);
                        entity.Notice_SEO_Title = Tools.NullStr(RdrList["Notice_SEO_Title"]);
                        entity.Notice_SEO_Keyword = Tools.NullStr(RdrList["Notice_SEO_Keyword"]);
                        entity.Notice_SEO_Description = Tools.NullStr(RdrList["Notice_SEO_Description"]);
                        entity.Notice_ShowTime = Tools.NullDate(RdrList["Notice_ShowTime"]);

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
                SqlTable = "Notice";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Notice_ID) FROM " + SqlTable + SqlParam;

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
