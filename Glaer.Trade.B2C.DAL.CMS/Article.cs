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
    public class Article : IArticle
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Article()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddArticle(ArticleInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Article";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Article_ID"] = entity.Article_ID;
            DrAdd["Article_CateID"] = entity.Article_CateID;
            DrAdd["Article_Title"] = entity.Article_Title;
            DrAdd["Article_Source"] = entity.Article_Source;
            DrAdd["Article_Author"] = entity.Article_Author;
            DrAdd["Article_Img"] = entity.Article_Img;
            DrAdd["Article_Keyword"] = entity.Article_Keyword;
            DrAdd["Article_Intro"] = entity.Article_Intro;
            DrAdd["Article_Content"] = entity.Article_Content;
            DrAdd["Article_Addtime"] = entity.Article_Addtime;
            DrAdd["Article_Hits"] = entity.Article_Hits;
            DrAdd["Article_IsRecommend"] = entity.Article_IsRecommend;
            DrAdd["Article_IsAudit"] = entity.Article_IsAudit;
            DrAdd["Article_Sort"] = entity.Article_Sort;
            DrAdd["Article_Site"] = entity.Article_Site;
            DrAdd["Article_Hyperlink"] = entity.Article_Hyperlink;
            DrAdd["Article_ContentID"] = entity.Article_ContentID;
            DrAdd["Article_SEO_Title"] = entity.Article_SEO_Title;
            DrAdd["Article_SEO_Keyword"] = entity.Article_SEO_Keyword;
            DrAdd["Article_SEO_Description"] = entity.Article_SEO_Description;
            DrAdd["Article_PageViews"] = entity.Article_PageViews;
            DrAdd["Artide_ShoulderTitle"] = entity.Artide_ShoulderTitle;
            DrAdd["Artide_ShoulderTitleSize"] = entity.Artide_ShoulderTitleSize;
            DrAdd["Article_HyperlinkSize"] = entity.Article_HyperlinkSize;
            DrAdd["Artide_IsTop"] = entity.Artide_IsTop;
            DrAdd["Subject_ID"] = entity.Subject_ID;
            DrAdd["Artide_SouceType"] = entity.Artide_SouceType;
            DrAdd["Article_memberID"] = entity.Article_memberID;
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

        public virtual bool EditArticle(ArticleInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Article WHERE Article_ID = " + entity.Article_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Article_ID"] = entity.Article_ID;
                    DrAdd["Article_CateID"] = entity.Article_CateID;
                    DrAdd["Article_Title"] = entity.Article_Title;
                    DrAdd["Article_Source"] = entity.Article_Source;
                    DrAdd["Article_Author"] = entity.Article_Author;
                    DrAdd["Article_Img"] = entity.Article_Img;
                    DrAdd["Article_Keyword"] = entity.Article_Keyword;
                    DrAdd["Article_Intro"] = entity.Article_Intro;
                    DrAdd["Article_Content"] = entity.Article_Content;
                    DrAdd["Article_Addtime"] = entity.Article_Addtime;
                    DrAdd["Article_Hits"] = entity.Article_Hits;
                    DrAdd["Article_IsRecommend"] = entity.Article_IsRecommend;
                    DrAdd["Article_IsAudit"] = entity.Article_IsAudit;
                    DrAdd["Article_Sort"] = entity.Article_Sort;
                    DrAdd["Article_Site"] = entity.Article_Site;
                    DrAdd["Article_Hyperlink"] = entity.Article_Hyperlink;
                    DrAdd["Article_ContentID"] = entity.Article_ContentID;
                    DrAdd["Article_SEO_Title"] = entity.Article_SEO_Title;
                    DrAdd["Article_SEO_Keyword"] = entity.Article_SEO_Keyword;
                    DrAdd["Article_SEO_Description"] = entity.Article_SEO_Description;
                    DrAdd["Article_PageViews"] = entity.Article_PageViews;
                    DrAdd["Artide_ShoulderTitle"] = entity.Artide_ShoulderTitle;
                    DrAdd["Artide_ShoulderTitleSize"] = entity.Artide_ShoulderTitleSize;
                    DrAdd["Article_HyperlinkSize"] = entity.Article_HyperlinkSize;
                    DrAdd["Artide_IsTop"] = entity.Artide_IsTop;
                    DrAdd["Subject_ID"] = entity.Subject_ID;
                    DrAdd["Artide_SouceType"] = entity.Artide_SouceType;
                    DrAdd["Article_memberID"] = entity.Article_memberID;
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

        public virtual int DelArticle(int ID)
        {
            string SqlAdd = "DELETE FROM Article WHERE Article_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ArticleInfo GetArticleByID(int ID)
        {
            ArticleInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Article WHERE Article_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ArticleInfo();

                    entity.Article_ID = Tools.NullInt(RdrList["Article_ID"]);
                    entity.Article_CateID = Tools.NullInt(RdrList["Article_CateID"]);
                    entity.Article_Title = Tools.NullStr(RdrList["Article_Title"]);
                    entity.Article_Source = Tools.NullStr(RdrList["Article_Source"]);
                    entity.Article_Author = Tools.NullStr(RdrList["Article_Author"]);
                    entity.Article_Img = Tools.NullStr(RdrList["Article_Img"]);
                    entity.Article_Keyword = Tools.NullStr(RdrList["Article_Keyword"]);
                    entity.Article_Intro = Tools.NullStr(RdrList["Article_Intro"]);
                    entity.Article_Content = Tools.NullStr(RdrList["Article_Content"]);
                    entity.Article_Addtime = Tools.NullDate(RdrList["Article_Addtime"]);
                    entity.Article_Hits = Tools.NullInt(RdrList["Article_Hits"]);
                    entity.Article_IsRecommend = Tools.NullInt(RdrList["Article_IsRecommend"]);
                    entity.Article_IsAudit = Tools.NullInt(RdrList["Article_IsAudit"]);
                    entity.Article_Sort = Tools.NullInt(RdrList["Article_Sort"]);
                    entity.Article_Site = Tools.NullStr(RdrList["Article_Site"]);
                    entity.Article_Hyperlink = Tools.NullStr(RdrList["Article_Hyperlink"]);
                    entity.Article_ContentID = Tools.NullInt(RdrList["Article_ContentID"]);
                    entity.Article_SEO_Title = Tools.NullStr(RdrList["Article_SEO_Title"]);
                    entity.Article_SEO_Keyword = Tools.NullStr(RdrList["Article_SEO_Keyword"]);
                    entity.Article_SEO_Description = Tools.NullStr(RdrList["Article_SEO_Description"]);
                    entity.Article_PageViews = Tools.NullInt(RdrList["Article_PageViews"]);
                    entity.Artide_ShoulderTitle = Tools.NullStr(RdrList["Artide_ShoulderTitle"]);
                    entity.Artide_ShoulderTitleSize = Tools.NullInt(RdrList["Artide_ShoulderTitleSize"]);
                    entity.Article_HyperlinkSize = Tools.NullInt(RdrList["Article_HyperlinkSize"]);
                    entity.Artide_IsTop = Tools.NullInt(RdrList["Artide_IsTop"]);
                    entity.Subject_ID = Tools.NullInt(RdrList["Subject_ID"]);
                    entity.Artide_SouceType = Tools.NullInt(RdrList["Artide_SouceType"]);
                    entity.Article_memberID = Tools.NullInt(RdrList["Article_memberID"]);
                   

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

        /// <summary>
        /// 获取最新添加的文章信息
        /// </summary>
        /// <returns>文章信息实体</returns>
        public virtual ArticleInfo GetGetArticleLastID()
        {

            ArticleInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT top 1 * FROM Article order by Article_ID desc";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ArticleInfo();

                    entity.Article_ID = Tools.NullInt(RdrList["Article_ID"]);
                    entity.Article_CateID = Tools.NullInt(RdrList["Article_CateID"]);
                    entity.Article_Title = Tools.NullStr(RdrList["Article_Title"]);
                    entity.Article_Source = Tools.NullStr(RdrList["Article_Source"]);
                    entity.Article_Author = Tools.NullStr(RdrList["Article_Author"]);
                    entity.Article_Img = Tools.NullStr(RdrList["Article_Img"]);
                    entity.Article_Keyword = Tools.NullStr(RdrList["Article_Keyword"]);
                    entity.Article_Intro = Tools.NullStr(RdrList["Article_Intro"]);
                    entity.Article_Content = Tools.NullStr(RdrList["Article_Content"]);
                    entity.Article_Addtime = Tools.NullDate(RdrList["Article_Addtime"]);
                    entity.Article_Hits = Tools.NullInt(RdrList["Article_Hits"]);
                    entity.Article_IsRecommend = Tools.NullInt(RdrList["Article_IsRecommend"]);
                    entity.Article_IsAudit = Tools.NullInt(RdrList["Article_IsAudit"]);
                    entity.Article_Sort = Tools.NullInt(RdrList["Article_Sort"]);
                    entity.Article_Site = Tools.NullStr(RdrList["Article_Site"]);
                    entity.Article_Hyperlink = Tools.NullStr(RdrList["Article_Hyperlink"]);
                    entity.Article_ContentID = Tools.NullInt(RdrList["Article_ContentID"]);
                    entity.Article_SEO_Title = Tools.NullStr(RdrList["Article_SEO_Title"]);
                    entity.Article_SEO_Keyword = Tools.NullStr(RdrList["Article_SEO_Keyword"]);
                    entity.Article_SEO_Description = Tools.NullStr(RdrList["Article_SEO_Description"]);
                    entity.Article_PageViews = Tools.NullInt(RdrList["Article_PageViews"]);
                    entity.Artide_ShoulderTitle = Tools.NullStr(RdrList["Artide_ShoulderTitle"]);
                    entity.Artide_ShoulderTitleSize = Tools.NullInt(RdrList["Artide_ShoulderTitleSize"]);
                    entity.Article_HyperlinkSize = Tools.NullInt(RdrList["Article_HyperlinkSize"]);
                    entity.Artide_IsTop = Tools.NullInt(RdrList["Artide_IsTop"]);
                    entity.Subject_ID = Tools.NullInt(RdrList["Subject_ID"]);
                    entity.Artide_SouceType = Tools.NullInt(RdrList["Artide_SouceType"]);
                    entity.Article_memberID = Tools.NullInt(RdrList["Article_memberID"]);
                   
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

        public virtual IList<ArticleInfo> GetArticles(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ArticleInfo> entitys = null;
            ArticleInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Article";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ArticleInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ArticleInfo();
                        entity.Article_ID = Tools.NullInt(RdrList["Article_ID"]);
                        entity.Article_CateID = Tools.NullInt(RdrList["Article_CateID"]);
                        entity.Article_Title = Tools.NullStr(RdrList["Article_Title"]);
                        entity.Article_Source = Tools.NullStr(RdrList["Article_Source"]);
                        entity.Article_Author = Tools.NullStr(RdrList["Article_Author"]);
                        entity.Article_Img = Tools.NullStr(RdrList["Article_Img"]);
                        entity.Article_Keyword = Tools.NullStr(RdrList["Article_Keyword"]);
                        entity.Article_Intro = Tools.NullStr(RdrList["Article_Intro"]);
                        entity.Article_Content = Tools.NullStr(RdrList["Article_Content"]);
                        entity.Article_Addtime = Tools.NullDate(RdrList["Article_Addtime"]);
                        entity.Article_Hits = Tools.NullInt(RdrList["Article_Hits"]);
                        entity.Article_IsRecommend = Tools.NullInt(RdrList["Article_IsRecommend"]);
                        entity.Article_IsAudit = Tools.NullInt(RdrList["Article_IsAudit"]);
                        entity.Article_Sort = Tools.NullInt(RdrList["Article_Sort"]);
                        entity.Article_Site = Tools.NullStr(RdrList["Article_Site"]);
                        entity.Article_Hyperlink = Tools.NullStr(RdrList["Article_Hyperlink"]);
                        entity.Article_ContentID = Tools.NullInt(RdrList["Article_ContentID"]);
                        entity.Article_SEO_Title = Tools.NullStr(RdrList["Article_SEO_Title"]);
                        entity.Article_SEO_Keyword = Tools.NullStr(RdrList["Article_SEO_Keyword"]);
                        entity.Article_SEO_Description = Tools.NullStr(RdrList["Article_SEO_Description"]);
                        entity.Article_PageViews = Tools.NullInt(RdrList["Article_PageViews"]);
                        entity.Artide_ShoulderTitle = Tools.NullStr(RdrList["Artide_ShoulderTitle"]);
                        entity.Artide_ShoulderTitleSize = Tools.NullInt(RdrList["Artide_ShoulderTitleSize"]);
                        entity.Article_HyperlinkSize = Tools.NullInt(RdrList["Article_HyperlinkSize"]);
                        entity.Artide_IsTop = Tools.NullInt(RdrList["Artide_IsTop"]);
                        entity.Subject_ID = Tools.NullInt(RdrList["Subject_ID"]);
                        entity.Artide_SouceType = Tools.NullInt(RdrList["Artide_SouceType"]);
                        entity.Article_memberID = Tools.NullInt(RdrList["Article_memberID"]);
                   
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
                SqlTable = "Article";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Article_ID) FROM " + SqlTable + SqlParam;

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

        public virtual void UpdateArticleViews(int ID)
        {
            DBHelper.ExecuteNonQuery("Update Article set Article_PageViews = Article_PageViews + 1 where Article_ID = " + ID);
        }


        #region ArticleCategory

   
        /// <summary>
        /// 添加文章附加分类
        /// </summary>
        /// <param name="entity">文章附加分类实体</param>
        /// <returns>操作结果：true(成功)、flash(失败)</returns>
        public virtual bool AddArticleCategory(ArticleCategoryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Article_Category";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Article_Category_ID"] = entity.Article_Category_ID;
            DrAdd["Article_Category_ArticleID"] = entity.Article_Category_ArticleID;
            DrAdd["Article_Category_CategoryID"] = entity.Article_Category_CategoryID;

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

        /// <summary>
        /// 删除指定文章信息的附加分类信息
        /// </summary>
        /// <param name="ID">文章信息ID</param>
        /// <returns>影响记录数量</returns>
        public virtual int DelArticleCategory(int ID)
        {
            string SqlAdd = "DELETE FROM Article_Category WHERE Article_Category_ArticleID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据查询条件获取文章附加分类
        /// </summary>
        /// <param name="Query">查询条件集合实体</param>
        /// <returns>文章附加分类实体集合</returns>
        public virtual IList<ArticleCategoryInfo> GetArticleCategorys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ArticleCategoryInfo> entitys = null;
            ArticleCategoryInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Article_Category";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ArticleCategoryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ArticleCategoryInfo();
                        entity.Article_Category_ID = Tools.NullInt(RdrList["Article_Category_ID"]);
                        entity.Article_Category_ArticleID = Tools.NullInt(RdrList["Article_Category_ArticleID"]);
                        entity.Article_Category_CategoryID = Tools.NullInt(RdrList["Article_Category_CategoryID"]);

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

        /// <summary>
        /// 获取指定分类下所有文章附加分类信息
        /// </summary>
        /// <param name="CateID">文章分类ID</param>
        /// <param name="CateIDs">文章分类ID范围（二选一）</param>
        /// <returns>文章附加分类实体集合</returns>
        public virtual IList<ArticleCategoryInfo> GetArticleCategorys(int CateID, params string[] CateIDs)
        {
            IList<ArticleCategoryInfo> entitys = null;
            ArticleCategoryInfo entity = null;
            string SqlList, strWhere;
            SqlDataReader RdrList = null;
            try
            {
                if (CateIDs!=null && CateIDs.Length > 0)
                {
                    string strCateId = "-1";
                    foreach (string str in CateIDs)
                    {
                        strCateId = strCateId + "," + str;
                    }
                    strWhere = " and c.Article_Category_CategoryID in (" + strCateId + ")";
                }
                else
                {
                    strWhere = " and c.Article_Category_CategoryID =" + CateID + "";
                }
                //（CN站点下、通过审核、）
                SqlList = "SELECT DISTINCT c.Article_Category_ArticleID FROM  Article_Category AS c INNER JOIN  Article AS a ON c.Article_Category_ArticleID = a.Article_id WHERE (a.Article_Site = 'CN') AND (a.Article_IsAudit = '2') " + strWhere;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ArticleCategoryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ArticleCategoryInfo();
                        entity.Article_Category_ArticleID = Tools.NullInt(RdrList["Article_Category_ArticleID"]);
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

        /// <summary>
        /// 根据文章是否存在指定附加分类
        /// </summary>
        /// <param name="CateID">文章分类ID</param>
        /// <param name="ArticleID">文章信息ID</param>
        /// <returns>文章附加分类实体</returns>
        public virtual ArticleCategoryInfo IsExistByCateIDAndArticleID(int CateID, int ArticleID)
        {
            ArticleCategoryInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT c.Article_Category_ID,c.Article_Category_CategoryID,c.Article_Category_ArticleID FROM  Article_Category AS c INNER JOIN Article AS a ON c.Article_Category_ArticleID = a.Article_id WHERE (a.Article_Site = 'CN') AND (a.Article_IsAudit = '2') and c.Article_Category_CategoryID=" + CateID + " and c.Article_Category_ArticleID=" + ArticleID + "";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ArticleCategoryInfo();
                    entity.Article_Category_ID = Tools.NullInt(RdrList["Article_Category_ID"]);
                    entity.Article_Category_ArticleID = Tools.NullInt(RdrList["Article_Category_ArticleID"]);
                    entity.Article_Category_CategoryID = Tools.NullInt(RdrList["Article_Category_CategoryID"]);
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

        /// <summary>
        /// 获取指定分类范围下包含指定文章的附加分类
        /// </summary>
        /// <param name="Aid">文章信息ID</param>
        /// <param name="CateIds">文章分类范围</param>
        /// <returns></returns>
        public virtual ArticleCategoryInfo GetArticleCategoryByAIDAndCateID(int Aid, string CateIds)
        {

            ArticleCategoryInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT top 1 Article_Category_ID, Article_Category_ArticleID, Article_Category_CategoryID FROM  Article_Category WHERE (Article_Category_ArticleID = " + Aid + ") AND (Article_Category_CategoryID IN (" + CateIds + "))";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ArticleCategoryInfo();
                    entity.Article_Category_ID = Tools.NullInt(RdrList["Article_Category_ID"]);
                    entity.Article_Category_ArticleID = Tools.NullInt(RdrList["Article_Category_ArticleID"]);
                    entity.Article_Category_CategoryID = Tools.NullInt(RdrList["Article_Category_CategoryID"]);
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

        #endregion

    

    }

    public class ArticleCate : IArticleCate
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ArticleCate()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddArticleCate(ArticleCateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Article_Cate";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Article_Cate_ID"] = entity.Article_Cate_ID;
            DrAdd["Article_Cate_ParentID"] = entity.Article_Cate_ParentID;
            DrAdd["Article_Cate_Name"] = entity.Article_Cate_Name;
            DrAdd["Article_Cate_Sort"] = entity.Article_Cate_Sort;
            DrAdd["Article_Cate_Site"] = entity.Article_Cate_Site;
            DrAdd["Article_Cate_Href"] = entity.Article_Cate_Href;
            DrAdd["Article_Cate_SEO_Title"] = entity.Article_Cate_SEO_Title;
            DrAdd["Article_Cate_SEO_Keyword"] = entity.Article_Cate_SEO_Keyword;
            DrAdd["Article_Cate_SEO_Description"] = entity.Article_Cate_SEO_Description;
            DrAdd["Article_Cate_IsTop"] = entity.Article_Cate_IsTop;
            DrAdd["Article_Cate_Type"] = entity.Article_Cate_Type;
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

        public virtual bool EditArticleCate(ArticleCateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Article_Cate WHERE Article_Cate_ID = " + entity.Article_Cate_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Article_Cate_ID"] = entity.Article_Cate_ID;
                    DrAdd["Article_Cate_ParentID"] = entity.Article_Cate_ParentID;
                    DrAdd["Article_Cate_Name"] = entity.Article_Cate_Name;
                    DrAdd["Article_Cate_Sort"] = entity.Article_Cate_Sort;
                    DrAdd["Article_Cate_Site"] = entity.Article_Cate_Site;
                    DrAdd["Article_Cate_Href"] = entity.Article_Cate_Href;
                    DrAdd["Article_Cate_SEO_Title"] = entity.Article_Cate_SEO_Title;
                    DrAdd["Article_Cate_SEO_Keyword"] = entity.Article_Cate_SEO_Keyword;
                    DrAdd["Article_Cate_SEO_Description"] = entity.Article_Cate_SEO_Description;
                    DrAdd["Article_Cate_IsTop"] = entity.Article_Cate_IsTop;
                    DrAdd["Article_Cate_Type"] = entity.Article_Cate_Type;
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

        public virtual int DelArticleCate(int ID)
        {
            string SqlAdd = "DELETE FROM Article_Cate WHERE Article_Cate_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ArticleCateInfo GetArticleCateByID(int ID)
        {
            ArticleCateInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Article_Cate WHERE Article_Cate_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ArticleCateInfo();

                    entity.Article_Cate_ID = Tools.NullInt(RdrList["Article_Cate_ID"]);
                    entity.Article_Cate_ParentID = Tools.NullInt(RdrList["Article_Cate_ParentID"]);
                    entity.Article_Cate_Name = Tools.NullStr(RdrList["Article_Cate_Name"]);
                    entity.Article_Cate_Sort = Tools.NullInt(RdrList["Article_Cate_Sort"]);
                    entity.Article_Cate_Site = Tools.NullStr(RdrList["Article_Cate_Site"]);
                    entity.Article_Cate_Href = Tools.NullStr(RdrList["Article_Cate_Href"]);
                    entity.Article_Cate_SEO_Title = Tools.NullStr(RdrList["Article_Cate_SEO_Title"]);
                    entity.Article_Cate_SEO_Keyword = Tools.NullStr(RdrList["Article_Cate_SEO_Keyword"]);
                    entity.Article_Cate_SEO_Description = Tools.NullStr(RdrList["Article_Cate_SEO_Description"]);
                    entity.Article_Cate_IsTop = Tools.NullInt(RdrList["Article_Cate_IsTop"]);
                    entity.Article_Cate_Type = Tools.NullInt(RdrList["Article_Cate_Type"]);
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

        public virtual IList<ArticleCateInfo> GetArticleCates(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ArticleCateInfo> entitys = null;
            ArticleCateInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Article_Cate";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ArticleCateInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ArticleCateInfo();
                        entity.Article_Cate_ID = Tools.NullInt(RdrList["Article_Cate_ID"]);
                        entity.Article_Cate_ParentID = Tools.NullInt(RdrList["Article_Cate_ParentID"]);
                        entity.Article_Cate_Name = Tools.NullStr(RdrList["Article_Cate_Name"]);
                        entity.Article_Cate_Sort = Tools.NullInt(RdrList["Article_Cate_Sort"]);
                        entity.Article_Cate_Site = Tools.NullStr(RdrList["Article_Cate_Site"]);
                        entity.Article_Cate_Href = Tools.NullStr(RdrList["Article_Cate_Href"]);
                        entity.Article_Cate_SEO_Title = Tools.NullStr(RdrList["Article_Cate_SEO_Title"]);
                        entity.Article_Cate_SEO_Keyword = Tools.NullStr(RdrList["Article_Cate_SEO_Keyword"]);
                        entity.Article_Cate_SEO_Description = Tools.NullStr(RdrList["Article_Cate_SEO_Description"]);
                        entity.Article_Cate_IsTop = Tools.NullInt(RdrList["Article_Cate_IsTop"]);
                        entity.Article_Cate_Type = Tools.NullInt(RdrList["Article_Cate_Type"]);

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
                SqlTable = "Article_Cate";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Article_Cate_ID) FROM " + SqlTable + SqlParam;

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

        public virtual string Get_All_SubCateID(int Cate_ID)
        {
            string SqlList, Cate_Arry;
            Cate_Arry = Cate_ID.ToString();
            if (Cate_ID == 0)
            {
                return Cate_Arry;
            }
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "with a as (select Article_Cate_ID from Article_Cate where Article_Cate_ID=" + Cate_ID + " union all select Article_Cate.Article_Cate_ID from Article_Cate,a where Article_Cate.Article_Cate_ParentID=a.Article_Cate_ID) select * from a";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    while (RdrList.Read())
                    {
                        if (Cate_ID != Tools.NullInt(RdrList["Article_Cate_ID"]))
                        {
                            Cate_Arry += "," + Tools.NullInt(RdrList["Article_Cate_ID"]);
                        }
                    }
                }
                return Cate_Arry;
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

    public class SensitiveWords : ISensitiveWords
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SensitiveWords()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSensitiveWords(SensitiveWordsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM SensitiveWords";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ID"] = entity.ID;
            DrAdd["Name"] = entity.Name;

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

        public virtual bool EditSensitiveWords(SensitiveWordsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM SensitiveWords WHERE ID = " + entity.ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ID"] = entity.ID;
                    DrAdd["Name"] = entity.Name;

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

        public virtual int DelSensitiveWords(int ID)
        {
            string SqlAdd = "DELETE FROM SensitiveWords WHERE ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SensitiveWordsInfo GetSensitiveWordsByID(int ID)
        {
            SensitiveWordsInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM SensitiveWords WHERE ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SensitiveWordsInfo();

                    entity.ID = Tools.NullInt(RdrList["ID"]);
                    entity.Name = Tools.NullStr(RdrList["Name"]);

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

        public virtual IList<SensitiveWordsInfo> GetSensitiveWordss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SensitiveWordsInfo> entitys = null;
            SensitiveWordsInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "SensitiveWords";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SensitiveWordsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SensitiveWordsInfo();
                        entity.ID = Tools.NullInt(RdrList["ID"]);
                        entity.Name = Tools.NullStr(RdrList["Name"]);

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
                SqlTable = "SensitiveWords";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(ID) FROM " + SqlTable + SqlParam;

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

    public class Special : ISpecial
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Special()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSpecial(SpecialInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Special";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Special_ID"] = entity.Special_ID;
            DrAdd["Special_Title"] = entity.Special_Title;
            DrAdd["Special_Intro"] = entity.Special_Intro;
            DrAdd["Special_Img"] = entity.Special_Img;
            DrAdd["Special_BannerImg"] = entity.Special_BannerImg;
            DrAdd["Special_Sort"] = entity.Special_Sort;
            DrAdd["Special_IsRecommend"] = entity.Special_IsRecommend;
            DrAdd["Special_IsAudit"] = entity.Special_IsAudit;
            DrAdd["Special_Site"] = entity.Special_Site;
            DrAdd["Special_Addtime"] = entity.Special_Addtime;
            DrAdd["Special_CateID"] = entity.Special_CateID;

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

        public virtual bool EditSpecial(SpecialInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Special WHERE Special_ID = " + entity.Special_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Special_ID"] = entity.Special_ID;
                    DrAdd["Special_Title"] = entity.Special_Title;
                    DrAdd["Special_Intro"] = entity.Special_Intro;
                    DrAdd["Special_Img"] = entity.Special_Img;
                    DrAdd["Special_BannerImg"] = entity.Special_BannerImg;
                    DrAdd["Special_Sort"] = entity.Special_Sort;
                    DrAdd["Special_IsRecommend"] = entity.Special_IsRecommend;
                    DrAdd["Special_IsAudit"] = entity.Special_IsAudit;
                    DrAdd["Special_Site"] = entity.Special_Site;
                    DrAdd["Special_Addtime"] = entity.Special_Addtime;
                    DrAdd["Special_CateID"] = entity.Special_CateID;

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

        public virtual int DelSpecial(int ID)
        {
            string SqlAdd = "DELETE FROM Special WHERE Special_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SpecialInfo GetSpecialByID(int ID)
        {
            SpecialInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Special WHERE Special_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SpecialInfo();

                    entity.Special_ID = Tools.NullInt(RdrList["Special_ID"]);
                    entity.Special_Title = Tools.NullStr(RdrList["Special_Title"]);
                    entity.Special_Intro = Tools.NullStr(RdrList["Special_Intro"]);
                    entity.Special_Img = Tools.NullStr(RdrList["Special_Img"]);
                    entity.Special_BannerImg = Tools.NullStr(RdrList["Special_BannerImg"]);
                    entity.Special_Sort = Tools.NullInt(RdrList["Special_Sort"]);
                    entity.Special_IsRecommend = Tools.NullInt(RdrList["Special_IsRecommend"]);
                    entity.Special_IsAudit = Tools.NullInt(RdrList["Special_IsAudit"]);
                    entity.Special_Site = Tools.NullStr(RdrList["Special_Site"]);
                    entity.Special_Addtime = Tools.NullDate(RdrList["Special_Addtime"]);
                    entity.Special_CateID = Tools.NullInt(RdrList["Special_CateID"]);

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

        public virtual IList<SpecialInfo> GetSpecials(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SpecialInfo> entitys = null;
            SpecialInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Special";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SpecialInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SpecialInfo();
                        entity.Special_ID = Tools.NullInt(RdrList["Special_ID"]);
                        entity.Special_Title = Tools.NullStr(RdrList["Special_Title"]);
                        entity.Special_Intro = Tools.NullStr(RdrList["Special_Intro"]);
                        entity.Special_Img = Tools.NullStr(RdrList["Special_Img"]);
                        entity.Special_BannerImg = Tools.NullStr(RdrList["Special_BannerImg"]);
                        entity.Special_Sort = Tools.NullInt(RdrList["Special_Sort"]);
                        entity.Special_IsRecommend = Tools.NullInt(RdrList["Special_IsRecommend"]);
                        entity.Special_IsAudit = Tools.NullInt(RdrList["Special_IsAudit"]);
                        entity.Special_Site = Tools.NullStr(RdrList["Special_Site"]);
                        entity.Special_Addtime = Tools.NullDate(RdrList["Special_Addtime"]);
                        entity.Special_CateID = Tools.NullInt(RdrList["Special_CateID"]);

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
                SqlTable = "Special";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Special_ID) FROM " + SqlTable + SqlParam;

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

    /// <summary>
    /// 专题报道
    /// </summary>
    public class ArticleSubject : IArticleSubject
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ArticleSubject()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }


        public virtual ArticleSubjectInfo GetArticleSubjectByName(string SubjectName, int SubjetcID)
        {
            ArticleSubjectInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Article_Subject WHERE Subject_Name ='" + SubjectName + "'";
                if (SubjetcID != 0)
                {
                    SqlList += " and Subject_ID<>" + SubjetcID;
                }
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ArticleSubjectInfo();

                    entity.Subject_ID = Tools.NullInt(RdrList["Subject_ID"]);
                    entity.Subject_Name = Tools.NullStr(RdrList["Subject_Name"]);
                    entity.Subject_Img = Tools.NullStr(RdrList["Subject_Img"]);
                    entity.Subject_IsActive = Tools.NullInt(RdrList["Subject_IsActive"]);
                    entity.Subject_Sort = Tools.NullInt(RdrList["Subject_Sort"]);
                    entity.Subject_Site = Tools.NullStr(RdrList["Subject_Site"]);

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



        public virtual bool AddArticleSubject(ArticleSubjectInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Article_Subject";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Subject_ID"] = entity.Subject_ID;
            DrAdd["Subject_Name"] = entity.Subject_Name;
            DrAdd["Subject_Img"] = entity.Subject_Img;
            DrAdd["Subject_IsActive"] = entity.Subject_IsActive;
            DrAdd["Subject_Sort"] = entity.Subject_Sort;
            DrAdd["Subject_Site"] = entity.Subject_Site;

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

        public virtual bool EditArticleSubject(ArticleSubjectInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Article_Subject WHERE Subject_ID = " + entity.Subject_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Subject_ID"] = entity.Subject_ID;
                    DrAdd["Subject_Name"] = entity.Subject_Name;
                    DrAdd["Subject_Img"] = entity.Subject_Img;
                    DrAdd["Subject_IsActive"] = entity.Subject_IsActive;
                    DrAdd["Subject_Sort"] = entity.Subject_Sort;
                    DrAdd["Subject_Site"] = entity.Subject_Site;

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

        public virtual int DelArticleSubject(int ID)
        {
            string SqlAdd = "DELETE FROM Article_Subject WHERE Subject_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ArticleSubjectInfo GetArticleSubjectByID(int ID)
        {
            ArticleSubjectInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Article_Subject WHERE Subject_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ArticleSubjectInfo();

                    entity.Subject_ID = Tools.NullInt(RdrList["Subject_ID"]);
                    entity.Subject_Name = Tools.NullStr(RdrList["Subject_Name"]);
                    entity.Subject_Img = Tools.NullStr(RdrList["Subject_Img"]);
                    entity.Subject_IsActive = Tools.NullInt(RdrList["Subject_IsActive"]);
                    entity.Subject_Sort = Tools.NullInt(RdrList["Subject_Sort"]);
                    entity.Subject_Site = Tools.NullStr(RdrList["Subject_Site"]);

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

        public virtual IList<ArticleSubjectInfo> GetArticleSubjects(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ArticleSubjectInfo> entitys = null;
            ArticleSubjectInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Article_Subject";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ArticleSubjectInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ArticleSubjectInfo();
                        entity.Subject_ID = Tools.NullInt(RdrList["Subject_ID"]);
                        entity.Subject_Name = Tools.NullStr(RdrList["Subject_Name"]);
                        entity.Subject_Img = Tools.NullStr(RdrList["Subject_Img"]);
                        entity.Subject_IsActive = Tools.NullInt(RdrList["Subject_IsActive"]);
                        entity.Subject_Sort = Tools.NullInt(RdrList["Subject_Sort"]);
                        entity.Subject_Site = Tools.NullStr(RdrList["Subject_Site"]);

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
                SqlTable = "Article_Subject";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Subject_ID) FROM " + SqlTable + SqlParam;

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
