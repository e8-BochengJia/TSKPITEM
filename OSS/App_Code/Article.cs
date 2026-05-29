using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.CMS;

/// <summary>
///Notice 的摘要说明
/// </summary>
public class Article
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IArticle MyBLL;
    ArticleCate articleCate;
    SensitiveWords words;
    ISpecial MySpe;
    IArticleCate MyCate;
    public Article()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ArticleFactory.CreateArticle();

        articleCate = new ArticleCate();
        words = new SensitiveWords();
        MySpe = SpecialFactory.CreateSpecial();
        MyCate= ArticleFactory.CreateArticleCate();
    }

    public virtual void AddArticle()
    {
        int Article_ID = tools.CheckInt(Request.Form["Article_ID"]);
        int Article_CateID = tools.CheckInt(Request.Form["Article_Cate"]);
        string Article_Title = tools.CheckStr(Request.Form["Article_Title"]);
        string Article_Source = tools.CheckStr(Request.Form["Article_Source"]);
        string Article_Author = tools.CheckStr(Request.Form["Article_Author"]);
        string Article_Img = tools.CheckStr(Request.Form["Article_Img"]);
        string Article_Keyword = tools.CheckStr(Request.Form["Article_Keyword"]);
        string Article_Intro = tools.CheckStr(Request.Form["Article_Intro"]);
        string Article_Content = Request.Form["Article_Content"];
        DateTime Article_Addtime = DateTime.Now;
        int Article_Hits = 0;
        int Article_IsRecommend = tools.CheckInt(Request.Form["Article_IsRecommend"]);
        int Article_IsAudit = 0;
        int Article_Sort = tools.CheckInt(Request.Form["Article_Sort"]);
        string Article_Hyperlink = tools.CheckStr(Request.Form["Article_Hyperlink"]);
        int Article_ContentID = tools.CheckInt(Request.Form["Article_ContentID"]);
        string Article_Site = Public.GetCurrentSite();
        string Article_SEO_Title = tools.CheckStr(Request.Form["Article_SEO_Title"]);
        string Article_SEO_Keyword = tools.CheckStr(Request.Form["Article_SEO_Keyword"]);
        string Article_SEO_Description = tools.CheckStr(Request.Form["Article_SEO_Description"]);
        int Article_PageViews = 0;

        int SubjectID = tools.CheckInt(Request.Form["Subject_ID"]);

        string Artide_ShoulderTitle = tools.CheckStr(Request.Form["Artide_ShoulderTitle"]);
        int Artide_ShoulderTitleSize = tools.CheckInt(Request.Form["Artide_ShoulderTitleSize"]);
        int Article_HyperlinkSize = tools.CheckInt(Request.Form["Article_HyperlinkSize"]);
        int Artide_IsTop = tools.CheckInt(Request.Form["Artide_IsTop"]);

        if (Article_CateID == 0)
        {
            Article_CateID = tools.CheckInt(Request.Form["Article_cate_parent"]);
        }

        if (Article_CateID == 0) { Public.Msg("error", "错误信息", "请选择类别！", false, "{back}"); return; }
        if (Article_Title == "") { Public.Msg("error", "错误信息", "请填写文章标题", false, "{back}"); return; }

        //Article_Content = words.FilterSensitiveWords(Article_Content);
        ArticleInfo entity = new ArticleInfo();
        entity.Article_ID = Article_ID;
        entity.Article_CateID = Article_CateID;
        entity.Article_Title = Article_Title;
        entity.Article_Source = Article_Source;
        entity.Article_Author = Article_Author;
        entity.Article_Img = Article_Img;
        entity.Article_Keyword = Article_Keyword;
        entity.Article_Intro = Article_Intro;
        entity.Article_Content = Article_Content;
        entity.Article_Addtime = Article_Addtime;
        entity.Article_Hits = Article_Hits;
        entity.Article_IsRecommend = Article_IsRecommend;
        entity.Article_IsAudit = Article_IsAudit;
        entity.Article_Sort = Article_Sort;
        entity.Article_Site = Article_Site;
        entity.Article_Hyperlink = Article_Hyperlink;
        entity.Article_ContentID = Article_ContentID;
        entity.Article_SEO_Title = Article_SEO_Title;
        entity.Article_SEO_Keyword = Article_SEO_Keyword;
        entity.Article_SEO_Description = Article_SEO_Description;
        entity.Article_PageViews = Article_PageViews;
        entity.Artide_ShoulderTitle = Artide_ShoulderTitle;
        entity.Artide_ShoulderTitleSize = Artide_ShoulderTitleSize;
        entity.Article_HyperlinkSize = Article_HyperlinkSize;
        entity.Subject_ID = SubjectID;
        entity.Artide_SouceType = 0;
        entity.Artide_IsTop = Artide_IsTop;
        entity.Article_memberID = 0;
        if (MyBLL.AddArticle(entity,Public.GetUserPrivilege()))
        {
            //AddArticleExtend(0);
            entity = MyBLL.GetGetArticleLastID(Public.GetUserPrivilege());
            if (entity != null)
            {
                
                if (entity.Article_Title == Article_Title)
                {
                    AddArticleCategory(entity.Article_ID);
                }
            }
          
            Public.AddRBACUserLog(59, "", "文章添加", Article_Title, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_add.aspx");
        }
        else
        {
            Public.AddRBACUserLog(59, "", "文章添加", Article_Title, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }


    private void AddArticleExtend(int Article_ID)
    {
        Glaer.Trade.Util.SQLHelper.ISQLHelper DBHelper = Glaer.Trade.Util.SQLHelper.SQLHelperFactory.CreateSQLHelper();

        if (Article_ID == 0)
            Article_ID = Convert.ToInt32(DBHelper.ExecuteScalar("SELECT MAX(Article_ID) FROM Article"));

        if (Article_ID == 0)
            return;

        string SqlAdd = "SELECT * FROM Article_Extend WHERE Article_Extend_ID = 0";
        DataTable DtAdd = null;
        DataRow DrAdd = null;
        try
        {
            DtAdd = DBHelper.Query(SqlAdd);
            for (int ii = 1; ii <= 5; ii++)
            {
                DrAdd = DtAdd.NewRow();
                DrAdd["Article_Extend_ArticleID"] = Article_ID;
                DrAdd["Article_Extend_Name"] = tools.CheckStr(Request.Form["Extend_Name" + ii]);
                DrAdd["Article_Extend_Content"] = tools.CheckStr(Request.Form["Extend_Content" + ii]);
                DtAdd.Rows.Add(DrAdd);
            }
            DBHelper.ExecuteNonQuery("DELETE FROM Article_Extend WHERE Article_Extend_ArticleID = " + Article_ID);
            DBHelper.SaveChanges(SqlAdd, DtAdd);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            DtAdd.Dispose();
            DtAdd = null;
            DrAdd = null;
        }

    }

    public DataTable GetArticleExtend(int Article_ID)
    {
        Glaer.Trade.Util.SQLHelper.ISQLHelper DBHelper = Glaer.Trade.Util.SQLHelper.SQLHelperFactory.CreateSQLHelper();

        return DBHelper.Query("SELECT * FROM Article_Extend WHERE Article_Extend_ArticleID = " + Article_ID);
    }


    public virtual void EditArticle()
    {

        int Article_ID = tools.CheckInt(Request.Form["Article_ID"]);
        int Article_CateID = tools.CheckInt(Request.Form["Article_Cate"]);
        string Article_Title = tools.CheckStr(Request.Form["Article_Title"]);
        string Article_Source = tools.CheckStr(Request.Form["Article_Source"]);
        string Article_Author = tools.CheckStr(Request.Form["Article_Author"]);
        string Article_Img = tools.CheckStr(Request.Form["Article_Img"]);
        string Article_Keyword = tools.CheckStr(Request.Form["Article_Keyword"]);
        string Article_Intro = tools.CheckStr(Request.Form["Article_Intro"]);
        string Article_Content = Request.Form["Article_Content"];
        int Article_Hits = tools.CheckInt(Request.Form["Article_Hits"]);
        int Article_IsRecommend = tools.CheckInt(Request.Form["Article_IsRecommend"]);
        int Article_IsAudit = 0;
        int Article_Sort = tools.CheckInt(Request.Form["Article_Sort"]);
        string Article_Hyperlink = tools.CheckStr(Request.Form["Article_Hyperlink"]);
        int Article_ContentID = tools.CheckInt(Request.Form["Article_ContentID"]);
        string Article_SEO_Title = tools.CheckStr(Request.Form["Article_SEO_Title"]);
        string Article_SEO_Keyword = tools.CheckStr(Request.Form["Article_SEO_Keyword"]);
        string Article_SEO_Description = tools.CheckStr(Request.Form["Article_SEO_Description"]);
        //int Article_PageViews = tools.CheckInt(Request.Form["Article_PageViews"]);
        int SubjectID = tools.CheckInt(Request.Form["Subject_ID"]);

        string Artide_ShoulderTitle = tools.CheckStr(Request.Form["Artide_ShoulderTitle"]);
        int Artide_ShoulderTitleSize = tools.CheckInt(Request.Form["Artide_ShoulderTitleSize"]);
        int Article_HyperlinkSize = tools.CheckInt(Request.Form["Article_HyperlinkSize"]);
        int Artide_IsTop = tools.CheckInt(Request.Form["Artide_IsTop"]);

        if (Article_CateID == 0)
        {
            Article_CateID = tools.CheckInt(Request.Form["Article_cate_parent"]);
        }

        if (Article_CateID == 0) { Public.Msg("error", "错误信息", "请选择类别！", false, "{back}"); return; }
        if (Article_Title == "") { Public.Msg("error", "错误信息", "请填写文章标题", false, "{back}"); return; }

        //Article_Content = words.FilterSensitiveWords(Article_Content);

        ArticleInfo entity = GetArticleByID(Article_ID);
        if (entity != null)
        {
            entity.Article_CateID = Article_CateID;
            entity.Article_Title = Article_Title;
            entity.Article_Source = Article_Source;
            entity.Article_Author = Article_Author;
            entity.Article_Img = Article_Img;
            entity.Article_Keyword = Article_Keyword;
            entity.Article_Intro = Article_Intro;
            entity.Article_Content = Article_Content;
            entity.Article_Hits = Article_Hits;
            entity.Article_IsRecommend = Article_IsRecommend;
            if(entity.Article_IsAudit==0)
            {
                entity.Article_IsAudit = 0;
            }
            else
            {
                entity.Article_IsAudit = Article_IsAudit;
            }
            entity.Article_Sort = Article_Sort;
            entity.Article_Hyperlink = Article_Hyperlink;
            entity.Article_ContentID = Article_ContentID;
            entity.Article_SEO_Title = Article_SEO_Title;
            entity.Article_SEO_Keyword = Article_SEO_Keyword;
            entity.Article_SEO_Description = Article_SEO_Description;
            //entity.Article_PageViews = Article_PageViews;
            entity.Artide_ShoulderTitle = Artide_ShoulderTitle;
            entity.Artide_ShoulderTitleSize = Artide_ShoulderTitleSize;
            entity.Article_HyperlinkSize = Article_HyperlinkSize;
            entity.Artide_IsTop = Artide_IsTop;
            entity.Subject_ID = SubjectID;
        }

        if (MyBLL.EditArticle(entity, Public.GetUserPrivilege()))
        {
            //AddArticleExtend(Article_ID);
            DelArticleCategory(Article_ID);
            AddArticleCategory(Article_ID);
            Public.AddRBACUserLog(59, Article_ID.ToString(), "文章修改", Article_Title, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "{close}");
        }
        else
        {
            Public.AddRBACUserLog(59, Article_ID.ToString(), "文章修改", Article_Title, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditArticle2()
    {

        int Article_ID = tools.CheckInt(Request.Form["Article_ID"]);
        int Article_CateID = tools.CheckInt(Request.Form["Article_Cate"]);
        string Article_Title = tools.CheckStr(Request.Form["Article_Title"]);
        string Article_Source = tools.CheckStr(Request.Form["Article_Source"]);
        string Article_Author = tools.CheckStr(Request.Form["Article_Author"]);
        string Article_Img = tools.CheckStr(Request.Form["Article_Img"]);
        string Article_Keyword = tools.CheckStr(Request.Form["Article_Keyword"]);
        string Article_Intro = tools.CheckStr(Request.Form["Article_Intro"]);
        string Article_Content = Request.Form["Article_Content"];
        int Article_Hits = tools.CheckInt(Request.Form["Article_Hits"]);
        int Article_IsRecommend = tools.CheckInt(Request.Form["Article_IsRecommend"]);
        int Article_IsAudit = 4;
        int Article_Sort = tools.CheckInt(Request.Form["Article_Sort"]);
        string Article_Hyperlink = tools.CheckStr(Request.Form["Article_Hyperlink"]);
        int Article_ContentID = tools.CheckInt(Request.Form["Article_ContentID"]);
        string Article_SEO_Title = tools.CheckStr(Request.Form["Article_SEO_Title"]);
        string Article_SEO_Keyword = tools.CheckStr(Request.Form["Article_SEO_Keyword"]);
        string Article_SEO_Description = tools.CheckStr(Request.Form["Article_SEO_Description"]);
        //int Article_PageViews = tools.CheckInt(Request.Form["Article_PageViews"]);

        string Artide_ShoulderTitle = tools.CheckStr(Request.Form["Artide_ShoulderTitle"]);
        int Artide_ShoulderTitleSize = tools.CheckInt(Request.Form["Artide_ShoulderTitleSize"]);
        int Article_HyperlinkSize = tools.CheckInt(Request.Form["Article_HyperlinkSize"]);
        int Artide_IsTop = tools.CheckInt(Request.Form["Artide_IsTop"]);

        if (Article_CateID == 0)
        {
            Article_CateID = tools.CheckInt(Request.Form["Article_cate_parent"]);
        }

        if (Article_CateID == 0) { Public.Msg("error", "错误信息", "请选择类别！", false, "{back}"); return; }
        if (Article_Title == "") { Public.Msg("error", "错误信息", "请填写文章标题", false, "{back}"); return; }

        //Article_Content = words.FilterSensitiveWords(Article_Content);

        ArticleInfo entity = GetArticleByID(Article_ID);
        if (entity != null)
        {
            //if(entity.Article_IsAudit!=1)
            //{
            //    { Public.Msg("error", "错误信息", "请等待初审通过！", false, "{back}"); return; }
            //}
            entity.Article_CateID = Article_CateID;
            entity.Article_Title = Article_Title;
            entity.Article_Source = Article_Source;
            entity.Article_Author = Article_Author;
            entity.Article_Img = Article_Img;
            entity.Article_Keyword = Article_Keyword;
            entity.Article_Intro = Article_Intro;
            entity.Article_Content = Article_Content;
            entity.Article_Hits = Article_Hits;
            entity.Article_IsRecommend = Article_IsRecommend;
            entity.Article_IsAudit = Article_IsAudit;
            entity.Article_Sort = Article_Sort;
            entity.Article_Hyperlink = Article_Hyperlink;
            entity.Article_ContentID = Article_ContentID;
            entity.Article_SEO_Title = Article_SEO_Title;
            entity.Article_SEO_Keyword = Article_SEO_Keyword;
            entity.Article_SEO_Description = Article_SEO_Description;
            //entity.Article_PageViews = Article_PageViews;
            entity.Artide_ShoulderTitle = Artide_ShoulderTitle;
            entity.Artide_ShoulderTitleSize = Artide_ShoulderTitleSize;
            entity.Article_HyperlinkSize = Article_HyperlinkSize;
            entity.Artide_IsTop = Artide_IsTop;
        }

        if (MyBLL.EditArticle(entity, Public.GetUserPrivilege()))
        {
            //AddArticleExtend(Article_ID);
            Public.AddRBACUserLog(59, Article_ID.ToString(), "文章修改", Article_Title, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "{close}");
        }
        else
        {
            Public.AddRBACUserLog(59, Article_ID.ToString(), "文章修改", Article_Title, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditArticle_batch()
    {
        string article_id = tools.CheckStr(Request["article_id"]);
        int Article_CateID = tools.CheckInt(Request.Form["Article_Cate"]);
        if (article_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要转移的文章", false, "Article_list.aspx");
            return;
        }
        if (Article_CateID == 0) { Public.Msg("error", "错误信息", "请选择类别！", false, "{back}"); return; }
        foreach (string str in article_id.Split(','))
        {
            ArticleInfo entity = GetArticleByID(tools.CheckInt(str));
            if (entity != null)
            {
                entity.Article_CateID = Article_CateID;

                MyBLL.EditArticle(entity, Public.GetUserPrivilege());
            }
        }
        Public.AddRBACUserLog(59, "", "文章批量修改类别", "文章ID：" + article_id, 1);
        Public.Msg("positive", "操作成功", "操作成功", true, "Article_list.aspx");
    }

    public virtual void ListEditArticle()
    {
        int Article_ID = tools.CheckInt(Request["id"]);

        int Article_Hits = tools.CheckInt(Request["ArticleInfo.Article_Hits"]);

        ArticleInfo entity = GetArticleByID(Article_ID);
        if (entity != null)
        {

            entity.Article_Hits = Article_Hits;

        }
        MyBLL.EditArticle(entity, Public.GetUserPrivilege());
        Public.AddRBACUserLog(59, "", "列表修改置顶排序", "文章ID：" + Article_ID, 1);
    }

    public virtual void ListEditArticle2()
    {
        int Article_ID = tools.CheckInt(Request["id"]);

        int Article_Sort = tools.CheckInt(Request["ArticleInfo.Article_Sort"]);

        ArticleInfo entity = GetArticleByID(Article_ID);
        if (entity != null)
        {

            entity.Article_Sort = Article_Sort;

        }
        MyBLL.EditArticle(entity, Public.GetUserPrivilege());
        Public.AddRBACUserLog(59, "", "列表修改文章排序", "文章ID：" + Article_ID, 1);
    }

    public virtual void DelArticle()
    {
        int Article_ID = tools.CheckInt(Request.QueryString["Article_ID"]);
        if (MyBLL.DelArticle(Article_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.AddRBACUserLog(59, Article_ID.ToString(), "文章删除", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_list.aspx");
        }
        else
        {
            Public.AddRBACUserLog(59, Article_ID.ToString(), "文章删除", "", 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelArticle_batch()
    {
        string article_id = tools.CheckStr(Request.QueryString["article_id"]);

        if (article_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要删除的文章", false, "{back}");
            return;
        }

        if (tools.Left(article_id, 1) == ",") { article_id = article_id.Remove(0, 1); }

        foreach (string str in article_id.Split(','))
        {
            if (MyBLL.DelArticle(tools.CheckInt(str), Public.GetUserPrivilege()) > 0)
            {
                
            }
        }
        Public.AddRBACUserLog(59, "", "文章批量删除", "文章ID：" + article_id, 1);
        Public.Msg("positive", "操作成功", "操作成功", true, "Article_list.aspx");
    }

    public virtual ArticleInfo GetArticleByID(int cate_id)
    {
        return MyBLL.GetArticleByID(cate_id, Public.GetUserPrivilege());
    }

    public int GetGetArticleByTitle()
    {
        string Article_Title = tools.CheckStr(Request["Article_Title"]);
        int Article_ID = tools.CheckInt(Request["Article_ID"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize =2;
        Query.CurrentPage =1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Title", "like", Article_Title));
        if(Article_ID>0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ID", "not in", Article_ID.ToString()));
        }
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID","DESC"));

        IList<ArticleInfo> entitys = MyBLL.GetArticles(Query, Public.GetUserPrivilege());
        if(entitys!=null)
        {
            return entitys.Count;
        }
        else
        {
            return 0;
        }
    }
    public string GetArticles()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int CateID = tools.CheckInt(Request["CateID"]);
        int IsAudit = tools.CheckInt(Request["IsAudit"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (CateID > 0)
        {
            string subcate = articleCate.Get_All_SubCateID(CateID);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", subcate.ToString()));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Title", "like", keyword));
        }
        if(IsAudit>0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", (IsAudit-1).ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        ArticleCateInfo CateInfo;

        IList<ArticleInfo> entitys = MyBLL.GetArticles(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ArticleInfo entity in entitys)
            {
                CateInfo = articleCate.GetArticleCateByID(entity.Article_CateID);
                
                jsonBuilder.Append("{\"id\":" + entity.Article_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Article_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (CateInfo != null) { jsonBuilder.Append(Public.JsonStr(CateInfo.Article_Cate_Name)); }
                else { jsonBuilder.Append(entity.Article_CateID); }
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Article_Source));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Article_Author));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetArticAudit(entity.Article_IsAudit).Replace("\"", "\\\""));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetIsActive(entity.Article_IsRecommend));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_PageViews);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");


                //if (Public.CheckPrivilege("807ea41c-545d-46f9-a24b-9f4b125a444a/59ff13a1-2da6-4ece-b156-62d915ae996a"))
                //{
                //    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\"> <a href=\\\"article_view.aspx?article_id=" + entity.Article_ID + "\\\" title=\\\"查看\\\" target=\\\"_blank\\\">查看</a>");
                //}

                if (Public.CheckPrivilege("1daab676-20b6-4073-af76-132ee8874556"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"article_edit.aspx?article_id=" + entity.Article_ID + "\\\" title=\\\"修改\\\" target=\\\"_blank\\\">修改</a>");
                }

                if (Public.CheckPrivilege("cc00c494-d211-438c-baef-ac20d419b066"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('article_do.aspx?action=move&article_id=" + entity.Article_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                if (Public.CheckPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"预览\\\"> <a href=\\\"article_preview.aspx?article_id=" + entity.Article_ID + "\\\" title=\\\"预览\\\" target=\\\"_blank\\\">预览</a>");

                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }

    }

    public string GetTopArticles()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int CateID = tools.CheckInt(Request["CateID"]);
        int IsAudit = tools.CheckInt(Request["IsAudit"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (CateID > 0)
        {
            string subcate = articleCate.Get_All_SubCateID(CateID);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", subcate.ToString()));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Title", "like", keyword));
        }
        if (IsAudit > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", (IsAudit - 1).ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Artide_IsTop", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        ArticleCateInfo CateInfo;

        IList<ArticleInfo> entitys = MyBLL.GetArticles(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ArticleInfo entity in entitys)
            {
                CateInfo = articleCate.GetArticleCateByID(entity.Article_CateID);

                jsonBuilder.Append("{\"id\":" + entity.Article_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Article_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (CateInfo != null) { jsonBuilder.Append(Public.JsonStr(CateInfo.Article_Cate_Name)); }
                else { jsonBuilder.Append(entity.Article_CateID); }
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Article_Source));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Article_Author));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetArticAudit(entity.Article_IsAudit).Replace("\"", "\\\""));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetIsActive(entity.Article_IsRecommend));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetIsActive(entity.Article_ContentID));
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_Hits);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_PageViews);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"预览\\\"> <a href=\\\"article_preview.aspx?article_id=" + entity.Article_ID + "\\\" title=\\\"预览\\\" target=\\\"_blank\\\">预览</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }
    }


    public string GetArticAudit(int Audit)
    {
        string Name = "";

        switch(Audit)
        {
            case 0:
                Name = "<span class=\"status_red\">待审核</span>";
                break;
            case 1:
                Name = "<span class=\"status_green\">初审通过</span>";
                break;
            case 2:
                Name = "<span class=\"status_red\">审核通过</span>";
                break;
            case 3:
                Name = "<span class=\"status_red\">审核不通过</span>";
                break;

        }

        return Name;
    }

    public string GetIsActive(int IsActive)
    {
        string Name = "";

        switch (IsActive)
        {
            case 0:
                Name = "否";
                break;
            case 1:
                Name = "是";
                break;

        }

        return Name;
    }

    /// <summary>
    /// 文章初审
    /// </summary>
    /// <param name="Audit"></param>
    public virtual void ArticleAuditOne(int Audit)
    {
        int Article_ID = tools.CheckInt(Request.Form["Article_ID"]);
        ArticleInfo entity = GetArticleByID(Article_ID);
        if(entity!=null)
        {
            //if(entity.Article_IsAudit==0)
            //{
                entity.Article_IsAudit = 1;

                if (MyBLL.EditArticle(entity, Public.GetUserPrivilege()))
                {
                    //AddArticleExtend(Article_ID);
                    Public.AddRBACUserLog(59, Article_ID.ToString(), "文章审核通过", entity.Article_Title, 1);
                    Public.Msg("positive", "操作成功", "操作成功", false, "{close}");
                }
                else
                {
                    Public.AddRBACUserLog(59, Article_ID.ToString(), "文章审核通过", entity.Article_Title, 0);
                    Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                }
            //}
            //else
            //{
            //    Public.Msg("error", "错误信息", "操作失败，不可执行此操作", false, "{back}");
            //}
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 文章终审
    /// </summary>
    /// <param name="Audit"></param>
    public virtual void ArticleAuditTwo(int Audit)
    {
        int Article_ID = tools.CheckInt(Request.Form["Article_ID"]);
        ArticleInfo entity = GetArticleByID(Article_ID);
        Member mem = new Member();
        if (entity != null)
        {
            //if (entity.Article_IsAudit == 4)
            //{
                entity.Article_IsAudit = 2;

                if (MyBLL.EditArticle(entity, Public.GetUserPrivilege()))
                {
                    //AddArticleExtend(Article_ID);
                    Public.AddRBACUserLog(59, Article_ID.ToString(), "文章审核通过", entity.Article_Title, 1);
                    if (entity.Artide_SouceType == 1)//赠送积分
                    {
                        mem.Member_Coin_AddConsume(200, "发表原创文章审核通过，获得积分。", entity.Article_memberID, true);
                    }
                    Public.Msg("positive", "操作成功", "操作成功", false, "{close}");
                }
                else
                {
                    Public.AddRBACUserLog(59, Article_ID.ToString(), "文章审核通过失败", entity.Article_Title, 0);
                    Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                }
            //}
            //else
            //{
            //    if(entity.Article_IsAudit==1)
            //    {
            //        Public.Msg("error", "错误信息", "文章未送审，不可执行此操作", false, "{back}");
            //    }
            //    else if(entity.Article_IsAudit == 2)
            //    {
            //        Public.Msg("error", "错误信息", "文章已终审，不可重复操作", false, "{back}");
            //    }
            //    else if(entity.Article_IsAudit == 3)
            //    {
            //        Public.Msg("error", "错误信息", "文章已退回，不可终审", false, "{back}");
            //    }
            //    else if(entity.Article_IsAudit ==0)
            //    {
            //        Public.Msg("error", "错误信息", "文章未初审，不可执行此操作", false, "{back}");
            //    }
                
            //}
                mem = null;
        }
        else
        {
            mem = null;
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 文章退回
    /// </summary>
    /// <param name="Audit"></param>
    public virtual void ArticleAuditReturn(int Audit)
    {
        int Article_ID = tools.CheckInt(Request.Form["Article_ID"]);
        ArticleInfo entity = GetArticleByID(Article_ID);
        if (entity != null)
        {
            //if (entity.Article_IsAudit != 3)
            //{
                entity.Article_IsAudit = 3;

                if (MyBLL.EditArticle(entity, Public.GetUserPrivilege()))
                {
                    //AddArticleExtend(Article_ID);
                    Public.AddRBACUserLog(59, Article_ID.ToString(), "文章审核不通过", entity.Article_Title, 1);
                    Public.Msg("positive", "操作成功", "操作成功", false, "{close}");
                }
                else
                {
                    Public.AddRBACUserLog(59, Article_ID.ToString(), "文章审核不通过", entity.Article_Title, 0);
                    Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                }
            //}
            //else
            //{
            //    Public.Msg("error", "错误信息", "文章已退回，不可重复操作", false, "{back}");
            //}
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual int GetArticleCount(int IsAudit)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage =1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ID", ">", "0"));

        if (IsAudit > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", (IsAudit - 1).ToString()));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", Public.GetCurrentSite()));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID","Desc"));


        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        if(pageinfo!=null)
        {
            return pageinfo.PageCount;
        }
        else
        {
            return 0;
        }
    }

    public virtual void AddSpecial()
    {
        int Special_ID = tools.CheckInt(Request.Form["Special_ID"]);
        string Special_Title = tools.CheckStr(Request.Form["Special_Title"]);
        string Special_Intro = tools.CheckStr(Request.Form["Special_Intro"]);
        string Special_Img = tools.CheckStr(Request.Form["Special_Img"]);
        string Special_BannerImg = tools.CheckStr(Request.Form["Special_BannerImg"]);
        int Special_Sort = tools.CheckInt(Request.Form["Special_Sort"]);
        int Special_IsRecommend = tools.CheckInt(Request.Form["Special_IsRecommend"]);
        int Special_IsAudit = 0;
        string Special_Site ="CN";
        DateTime Special_Addtime =DateTime.Now;
        int Article_CateID = tools.CheckInt(Request.Form["Article_Cate"]);

        if(Article_CateID==0)
        {
            Article_CateID= tools.CheckInt(Request.Form["Article_cate_parent"]);
        }

        if (Article_CateID == 0) { Public.Msg("error", "错误信息", "请选择类别！", false, "{back}"); return; }
        if (Special_Title == "") { Public.Msg("error", "错误信息", "请填写专题名称", false, "{back}"); return; }

        SpecialInfo entity = new SpecialInfo();
        entity.Special_ID = Special_ID;
        entity.Special_Title = Special_Title;
        entity.Special_Intro = Special_Intro;
        entity.Special_Img = Special_Img;
        entity.Special_BannerImg = Special_BannerImg;
        entity.Special_Sort = Special_Sort;
        entity.Special_IsRecommend = Special_IsRecommend;
        entity.Special_IsAudit = Special_IsAudit;
        entity.Special_Site = Special_Site;
        entity.Special_Addtime = Special_Addtime;
        entity.Special_CateID = Article_CateID;
        if (MySpe.AddSpecial(entity, Public.GetUserPrivilege()))
        {
            Public.AddRBACUserLog(59, "", "专题添加", Special_Title, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "Special_add.aspx");
        }
        else
        {
            Public.AddRBACUserLog(59, "", "专题添加", Special_Title, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditSpecial()
    {

        int Special_ID = tools.CheckInt(Request.Form["Special_ID"]);
        string Special_Title = tools.CheckStr(Request.Form["Special_Title"]);
        string Special_Intro = tools.CheckStr(Request.Form["Special_Intro"]);
        string Special_Img = tools.CheckStr(Request.Form["Special_Img"]);
        string Special_BannerImg = tools.CheckStr(Request.Form["Special_BannerImg"]);
        int Special_Sort = tools.CheckInt(Request.Form["Special_Sort"]);
        int Special_IsRecommend = tools.CheckInt(Request.Form["Special_IsRecommend"]);
        int Special_IsAudit =0;
        string Special_Site = "CN";
        int Article_CateID = tools.CheckInt(Request.Form["Article_Cate"]);
        if (Article_CateID == 0)
        {
            Article_CateID = tools.CheckInt(Request.Form["Article_cate_parent"]);
        }
        if (Article_CateID == 0) { Public.Msg("error", "错误信息", "请选择类别！", false, "{back}"); return; }
        if (Special_Title == "") { Public.Msg("error", "错误信息", "请填写专题名称", false, "{back}"); return; }

        SpecialInfo entity = GetSpecialByID(Special_ID);
        if(entity!=null)
        {
            entity.Special_ID = Special_ID;
            entity.Special_Title = Special_Title;
            entity.Special_Intro = Special_Intro;
            entity.Special_Img = Special_Img;
            entity.Special_BannerImg = Special_BannerImg;
            entity.Special_Sort = Special_Sort;
            entity.Special_IsRecommend = Special_IsRecommend;
            entity.Special_IsAudit = Special_IsAudit;
            entity.Special_Site = Special_Site;
            entity.Special_CateID = Article_CateID;


            if (MySpe.EditSpecial(entity, Public.GetUserPrivilege()))
            {
                Public.AddRBACUserLog(59, Special_ID.ToString(), "专题修改", Special_Title, 1);
                Public.Msg("positive", "操作成功", "操作成功", true, "{close}");
            }
            else
            {
                Public.AddRBACUserLog(59, Special_ID.ToString(), "专题修改", Special_Title, 0);
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.AddRBACUserLog(59, Special_ID.ToString(), "专题修改", Special_Title, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
        
    }

    public virtual void DelSpecial()
    {
        int Special_ID = tools.CheckInt(Request.QueryString["Special_ID"]);
        if (MySpe.DelSpecial(Special_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.AddRBACUserLog(59, Special_ID.ToString(), "专题删除", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "Special_list.aspx");
        }
        else
        {
            Public.AddRBACUserLog(59, Special_ID.ToString(), "专题删除", "", 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual SpecialInfo GetSpecialByID(int cate_id)
    {
        return MySpe.GetSpecialByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetSpecials()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int IsAudit = tools.CheckInt(Request["IsAudit"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);

        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SpecialInfo.Special_Title", "like", keyword));
        }
        if (IsAudit > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SpecialInfo.Special_IsAudit", "=", (IsAudit - 1).ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SpecialInfo.Special_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MySpe.GetPageInfo(Query, Public.GetUserPrivilege());

        ArticleCateInfo CateInfo;

        IList<SpecialInfo> entitys = MySpe.GetSpecials(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SpecialInfo entity in entitys)
            {
                CateInfo = articleCate.GetArticleCateByID(entity.Special_CateID);

                jsonBuilder.Append("{\"id\":" + entity.Special_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Special_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Special_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (CateInfo != null) { jsonBuilder.Append(Public.JsonStr(CateInfo.Article_Cate_Name)); }
                else { jsonBuilder.Append(entity.Special_CateID); }
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetSpecialAudit(entity.Special_IsAudit).Replace("\"", "\\\""));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Special_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetIsActive(entity.Special_IsRecommend));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");



                if (Public.CheckPrivilege("8e2eb41c-060b-4a1c-9c7c-403d6f1072fa"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"Special_edit.aspx?Special_ID=" + entity.Special_ID + "\\\" title=\\\"修改\\\" target=\\\"_blank\\\">修改</a>");
                }

                if (Public.CheckPrivilege("8152aeb2-3302-4ea9-bbc1-a3dcb300c4f8"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Special_do.aspx?action=move&Special_ID=" + entity.Special_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                if (Public.CheckPrivilege("86aa82ef-9cb2-49e4-a8c9-db708ab33f3a"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"预览\\\"> <a href=\\\"Special_preview.aspx?Special_ID=" + entity.Special_ID + "\\\" title=\\\"预览\\\" target=\\\"_blank\\\">预览</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }
    }

    public string GetSpecialAudit(int Audit)
    {
        string Name = "";

        switch (Audit)
        {
            case 0:
                Name = "<span class=\"status_red\">待审核</span>";
                break;
            case 1:
                Name = "<span class=\"status_green\">已审核</span>";
                break;
            case 2:
                Name = "<span class=\"status_red\">退回</span>";
                break;

        }

        return Name;
    }

    /// <summary>
    /// 专题页展示
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public string Special_Show(int ID)
    {
        StringBuilder StrHTML = new StringBuilder("");
        SpecialInfo entity = GetSpecialByID(ID);
        if (entity != null)
        {
            int CateID = entity.Special_CateID;

            StrHTML.Append(GetArticleByCateTopOne(CateID));


            StrHTML.Append("<div class=\"w-1200\">");
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", ">", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", CateID.ToString()));
            Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "ASC"));
            IList<ArticleCateInfo> ArticleCates = MyCate.GetArticleCates(Query, Public.GetUserPrivilege());
            if (ArticleCates != null)
            {
                int i = 0;
                string list = "";
                foreach (ArticleCateInfo one in ArticleCates)
                {

                    if (one.Article_Cate_Type == 1)
                    {
                        StrHTML.Append(GetSpecialArticleOne(one));
                    }
                    else if (one.Article_Cate_Type == 2)
                    {
                        StrHTML.Append(GetSpecialArticleTwo(one));
                    }
                    else if (one.Article_Cate_Type == 3)
                    {
                        i++;
                        if (i % 2 == 1)
                        {
                            list = "<div class=\"six-box clearfix\">";
                            list = list + "<div class=\"six-box-list\">";
                            list = list + GetSpecialArticleThird(one);
                            list = list + "</div>";
                        }
                        else
                        {
                            list = list + "<div class=\"six-box-list ml-20\">";
                            list = list + GetSpecialArticleThird(one);
                            list = list + "</div>";
                            list = list + "</div>";
                            
                            StrHTML.Append(list);
                            i = 0;
                            list = "";
                        }
                    }
                    else if (one.Article_Cate_Type == 4)
                    {
                        StrHTML.Append(GetSpecialArticleFourth(one));
                    }
                }

                if (i == 1)
                {
                    list = list + "<div class=\"six-box-list ml-20\">";
                    list = list + "</div>";
                    list = list + "</div>";
                    StrHTML.Append(list);
                }

            }
            StrHTML.Append("</div>");

        }

        return StrHTML.ToString();
    }

    /// <summary>
    /// 获取当前分类下第一个文章
    /// </summary>
    /// <param name="CateID"></param>
    /// <returns></returns>
    public string GetArticleByCateTopOne(int CateID)
    {
        StringBuilder StrHTML = new StringBuilder("");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 4;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", CateID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

        IList<ArticleInfo> entitys = MyBLL.GetArticles(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            int i = 0;
            StrHTML.Append("<div class=\"zt-top-news\">");
            foreach (ArticleInfo entity in entitys)
            {
                i++;
                if (i == 1)
                {
                    StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\"><h2>" + entity.Article_Title + "</h2></a>");
                    StrHTML.Append("<div class=\"fix-tit\">");
                    StrHTML.Append("<i>头条</i>");
                    StrHTML.Append("<img src=\"/images/icon-top.png\">");
                    StrHTML.Append("</div>");


                    StrHTML.Append("<div class=\"important-news clearfix\">");

                    //图
                    StrHTML.Append("<div class=\"news-banner\" style=\"width: 580px; height: 340px; margin-top: 0;\">");
                    StrHTML.Append("<div class=\"banner-wrap clearfix\" style=\"width: 580px; height: 340px;\">");
                    StrHTML.Append("<ul class=\"banner clearfix\">");
                    StrHTML.Append("<li>");
                    StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                    StrHTML.Append("<img src=\"" + Public.FormatImgURL(entity.Article_Img, "fullpath") + "\">");
                    StrHTML.Append("<h4>" + entity.Article_Title + "</h4>");
                    StrHTML.Append(" </a> ");
                    StrHTML.Append("</li>");
                    StrHTML.Append("</ul>");
                    StrHTML.Append("</div>");
                    StrHTML.Append("</div>");
                    StrHTML.Append("<ul class=\"important-news-right\">");
                }
                else
                {

                    StrHTML.Append("<li class=\"clearfix\">");
                    StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                    StrHTML.Append("<h4>" + entity.Article_Title + "</h4>");
                    StrHTML.Append("<p>" + entity.Article_Intro + "</p>");
                    StrHTML.Append("</a>");
                    StrHTML.Append("</li>");
                }
            }
            StrHTML.Append("</ul>");
            StrHTML.Append("</div>");
            StrHTML.Append("</div>");

        }

        return StrHTML.ToString();
    }

    /// <summary>
    /// 专题页轮播文章展示
    /// </summary>
    /// <param name="Cates"></param>
    /// <returns></returns>
    public string GetSpecialArticleOne(ArticleCateInfo Cates)
    {
        StringBuilder StrHTML = new StringBuilder("");
        if (Cates != null)
        {
            StrHTML.Append("<fieldset><legend>" + Cates.Article_Cate_Name + "</legend></fieldset>");

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 8;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", articleCate.Get_All_SubCateID(Cates.Article_Cate_ID)));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

            IList<ArticleInfo> entitys = MyBLL.GetArticles(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                StrHTML.Append("<div class=\"lead-list3 clearfix\">");
                StrHTML.Append("<ul class=\"interview-pic\" id=\"interview" + Cates.Article_Cate_ID + "\">");
                foreach (ArticleInfo entity in entitys)
                {
                    StrHTML.Append("<li>");
                    StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                    StrHTML.Append("<img src=\"" + Public.FormatImgURL(entity.Article_Img, "thumbnail2") + "\">");

                    StrHTML.Append("<h3>" + entity.Article_Title + "</h3>");
                    StrHTML.Append("<p>" + entity.Article_Intro + "</p>");
                    StrHTML.Append("</a>");
                    StrHTML.Append("</li>");
                }
                StrHTML.Append("</ul>");
                StrHTML.Append("<script type=\"text/javascript\">");
                StrHTML.Append(" $(document).ready(function(){");
                StrHTML.Append("$(\"#interview" + Cates.Article_Cate_ID + "\").bxSlider({");
                StrHTML.Append("slideWidth: 285,");
                StrHTML.Append("maxSlides: 5,");
                StrHTML.Append("moveSlides: 1,");
                StrHTML.Append("slideMargin: 20,");
                StrHTML.Append("auto: true");
                StrHTML.Append("});");
                StrHTML.Append(" });");
                StrHTML.Append("</script>");
                StrHTML.Append("</div>");
            }
        }
        return StrHTML.ToString();
    }


    /// <summary>
    /// 专题单个文章展示
    /// </summary>
    /// <param name="Cates"></param>
    /// <returns></returns>
    public string GetSpecialArticleTwo(ArticleCateInfo Cates)
    {
        StringBuilder StrHTML = new StringBuilder("");
        if (Cates != null)
        {
            StrHTML.Append("<fieldset><legend>" + Cates.Article_Cate_Name + "</legend></fieldset>");

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", ">", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", Cates.Article_Cate_ID.ToString()));
            Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "ASC"));
            IList<ArticleCateInfo> ArticleCates = MyCate.GetArticleCates(Query, Public.GetUserPrivilege());

            if (ArticleCates != null)
            {
                StrHTML.Append("<div class=\"party-class clearfix\">");
                int i = 0;
                foreach (ArticleCateInfo one in ArticleCates)
                {
                    i++;
                    if (i % 2 == 1)
                    {
                        StrHTML.Append("<div class=\"class-left-list\">");
                    }
                    else
                    {
                        StrHTML.Append("<div class=\"class-left-list ml-20\">");
                    }
                    StrHTML.Append("<h3><a href=\"/" + one.Article_Cate_ID + "/\" title=\"" + one.Article_Cate_Name + "\">" + one.Article_Cate_Name + "</a></h3>");

                    IList<ArticleInfo> entitys = GetArticleByCateID(one.Article_Cate_ID, 6);
                    if (entitys != null)
                    {
                        StrHTML.Append("<ul class=\"text-page-list\">");
                        foreach (ArticleInfo entity in entitys)
                        {
                            StrHTML.Append("<li>");
                            StrHTML.Append("<i>.</i>");
                            StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">" + entity.Article_Title + "</a>");
                            StrHTML.Append("</li>");
                        }
                        StrHTML.Append("</ul>");
                    }
                    StrHTML.Append("</div>");
                }
                StrHTML.Append("</div>");
            }
            else
            {
                StrHTML.Append("<div class=\"party-class clearfix\">");
                IList<ArticleInfo> entitys = GetArticleByCateID(Cates.Article_Cate_ID, 12);
                if (entitys != null)
                {
                    string list1 = "<div class=\"class-left-list\"><ul class=\"text-page-list\">";
                    string list2 = "<div class=\"class-left-list ml-20\"><ul class=\"text-page-list\">";
                    int i = 0;
                    foreach (ArticleInfo entity in entitys)
                    {
                        i++;
                        if (i % 2 == 1)
                        {
                            list1 = list1 + "<li><i>.</i><a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">" + entity.Article_Title + "</a></li>";
                        }
                        else
                        {
                            list2 = list2 + "<li><i>.</i><a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">" + entity.Article_Title + "</a></li>";
                        }
                    }
                    list1 = list1 + "</ul></div>";
                    list2 = list2 + "</ul></div>";

                    StrHTML.Append(list1 + list2);
                }

                StrHTML.Append("</div>");
            }
        }

        return StrHTML.ToString();
    }

    public IList<ArticleInfo> GetArticleByCateID(int ID, int PageSize)
    {
        StringBuilder StrHTML = new StringBuilder("");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", articleCate.Get_All_SubCateID(ID)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

        IList<ArticleInfo> entitys = MyBLL.GetArticles(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            return entitys;
        }
        else
        {
            return null;
        }
    }


    public string GetSpecialArticleThird(ArticleCateInfo Cates)
    {
        StringBuilder StrHTML = new StringBuilder("");
        if (Cates != null)
        {
            StrHTML.Append("<h3><span>" + Cates.Article_Cate_Name + "</span><a href=\"/" + Cates.Article_Cate_ID + "/\">更多></a></h3>");

            IList<ArticleInfo> entitys = GetArticleByCateID(Cates.Article_Cate_ID, 6);
            if (entitys != null)
            {
                StrHTML.Append("<ul class=\"text-page-list\">");
                foreach (ArticleInfo entity in entitys)
                {
                    StrHTML.Append("<li>");
                    StrHTML.Append("<i>.</i>");
                    StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">" + entity.Article_Title + "</a>");
                    StrHTML.Append("</li>");
                }
                StrHTML.Append("</ul>");
            }
        }
        return StrHTML.ToString();
    }
    /// <summary>
    /// 专题文章列表
    /// </summary>
    /// <param name="Cates"></param>
    /// <returns></returns>
    public string GetSpecialArticleFourth(ArticleCateInfo Cates)
    {
        StringBuilder StrHTML = new StringBuilder("");
        if (Cates != null)
        {
            StrHTML.Append("<fieldset><legend>" + Cates.Article_Cate_Name + "</legend></fieldset>");


            QueryInfo Query = new QueryInfo();
            Query.PageSize = 6;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", articleCate.Get_All_SubCateID(Cates.Article_Cate_ID)));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

            IList<ArticleInfo> entitys = MyBLL.GetArticles(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {

                StrHTML.Append("<ul class=\"bg-list clearfix\">");
                int i = 0;
                foreach (ArticleInfo entity in entitys)
                {
                    i++;
                    if (i % 3 == 0)
                    {
                        StrHTML.Append("<li style=\"margin-right:0px;\">");
                    }
                    else
                    {
                        StrHTML.Append("<li>");
                    }
                    //StrHTML.Append("<li>");
                    StrHTML.Append(" <h3>" + entity.Article_Title + "</h3>");
                    StrHTML.Append(" <p>" + entity.Article_Intro + "</p>");
                    StrHTML.Append(" <a  href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\" >更多></a>");
                    StrHTML.Append("</li>");
                }



                StrHTML.Append("</ul>");
            }
        }
        return StrHTML.ToString();
    }

    /// <summary>
    /// 专题审核
    /// </summary>
    /// <param name="Audit"></param>
    public virtual void SpecialAudit(int Audit)
    {
        int Special_ID = tools.CheckInt(Request.Form["Special_ID"]);
        SpecialInfo entity = GetSpecialByID(Special_ID);
        if(entity!=null)
        {
            string name = "专题审核通过";

            entity.Special_IsAudit = Audit;
            if(Audit==2)
            {
                name = "专题审核退回";
            }
            if (MySpe.EditSpecial(entity, Public.GetUserPrivilege()))
            {
                Public.AddRBACUserLog(59, Special_ID.ToString(), name, entity.Special_Title, 1);
                Public.Msg("positive", "操作成功", "操作成功", true, "{close}");
            }
            else
            {
                Public.AddRBACUserLog(59, Special_ID.ToString(), name, entity.Special_Title, 0);
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void VoiceList(int Cate_ID)
    {
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "";

        if (curr_page < 1)
        {
            curr_page = 1;
        }
        page_url = page_url + "?";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", articleCate.Get_All_SubCateID(Cate_ID)));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyBLL.GetArticles(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            Response.Write("<div class=\"voice-list clearfix\">");

            int i = 0;
            string list1 = "<ul class=\"voice-list-left\">";
            string list2 = "<ul class=\"voice-list-left voice-list-right\">";
            foreach (ArticleInfo entity in entitys)
            {
                i++;

                if (i % 2 == 1)
                {
                    if(entity.Article_IsAudit!=2)
                    {
                        list1 = list1 + "<li class=\"clearfix\" style=\"background: #dddddd;\">";
                    }
                    else
                    {
                        list1 = list1 + "<li class=\"clearfix\">";
                    }
                    
                    list1 = list1 + "<div class=\"voice-top clearfix\">";
                    list1 = list1 + "<img src=\"" + Public.FormatImgURL(entity.Article_Img, "thumbnail") + "\" />";
                    list1 = list1 + "<div class=\"name\">";
                    list1 = list1 + "<h4>" + entity.Article_Title + "</h4>";
                    list1 = list1 + "<span>" + entity.Article_Hyperlink + "</span>";
                    list1 = list1 + "</div>";
                    list1 = list1 + "</div>";
                    list1 = list1 + "<p>" + entity.Article_Intro + "</p>";
                    list1 = list1 + "</li>";
                }
                else
                {
                    if (entity.Article_IsAudit != 2)
                    {
                        list2 = list2 + "<li class=\"clearfix\" style=\"background: #dddddd;\">";
                    }
                    else
                    {
                        list2 = list2 + "<li class=\"clearfix\">";
                    }
                        
                    list2 = list2 + "<div class=\"voice-top clearfix\">";
                    list2 = list2 + "<img src=\"" + Public.FormatImgURL(entity.Article_Img, "thumbnail") + "\" />";
                    list2 = list2 + "<div class=\"name\">";
                    list2 = list2 + "<h4>" + entity.Article_Title + "</h4>";
                    list2 = list2 + "<span>" + entity.Article_Hyperlink + "</span>";
                    list2 = list2 + "</div>";
                    list2 = list2 + "</div>";
                    list2 = list2 + "<p>" + entity.Article_Intro + "</p>";
                    list2 = list2 + "</li>";
                }



            }
            list1 = list1 + "</ul>";
            list2 = list2 + "</ul>";
            Response.Write(list1 + list2);
            Response.Write("</div>");
        }
        else
        {
            Response.Redirect("/");
        }
    }

    #region 添加附加分类

    //添加多个文章分类
    public virtual void AddArticleCategory(int Article_Cate_ArticleID)
    {
        int Article_CateID = tools.CheckInt(Request.Form["Article_cate"]);
        string Article_Cate_CateIDs = tools.CheckStr(Request["Article_CateIDs"]);

        if (Article_Cate_CateIDs.IndexOf(Article_CateID.ToString()) < 0)
        {
            if (Article_Cate_CateIDs.Length == 0)
            {
                Article_Cate_CateIDs = Article_CateID.ToString();
            }
            else
            {
                Article_Cate_CateIDs = Article_Cate_CateIDs + "," + Article_CateID.ToString();
            }
        }

        if (Article_Cate_CateIDs.Length > 0)
        {
            ArticleCategoryInfo entity = new ArticleCategoryInfo();
            int Article_Cate_ID = tools.CheckInt(Request.Form["Article_Category_ID"]);
          
            string[] strCateIDs = Article_Cate_CateIDs.Split(',');
            entity.Article_Category_ArticleID = Article_Cate_ArticleID;
            entity.Article_Category_ID = Article_Cate_ID;
            foreach (string strID in strCateIDs)
            {
                entity.Article_Category_CategoryID = tools.CheckInt(strID);
                MyBLL.AddArticleCategory(entity);
            }
        }
    }

    //删除多个文章分类
    public virtual void DelArticleCategory(int Article_ID)
    {
        MyBLL.DelArticleCategory(Article_ID);
    }

    //获取文章所有附加分类
    public string GetArticleCategoryByArticleID(int Article_ID)
    {
        string ArticleIDs = string.Empty;
        IList<ArticleCategoryInfo> entitys = null;
        QueryInfo query = new QueryInfo();
        query.CurrentPage = 1;
        query.PageSize = 0;
        query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCategoryInfo.Article_Category_ArticleID", "=", Convert.ToString(Article_ID)));
        query.OrderInfos.Add(new OrderInfo("ArticleCategoryInfo.Article_Category_ID", "desc"));

        entitys = MyBLL.GetArticleCategorys(query);

        if (entitys != null)
        {
            foreach (ArticleCategoryInfo entity in entitys)
            {
                ArticleIDs += entity.Article_Category_CategoryID + ",";
            }
        }
        return ArticleIDs;
    }

    #endregion
}

public class ArticleCate
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IArticleCate MyBLL;

    public ArticleCate()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ArticleFactory.CreateArticleCate();
    }

    public virtual void AddArticleCate()
    {
        int Article_Cate_ID = tools.CheckInt(Request.Form["Article_Cate_ID"]);
        int Article_Cate_ParentID = tools.CheckInt(Request.Form["Article_cate"]);
        string Article_Cate_Name = tools.CheckStr(Request.Form["Article_Cate_Name"]);
        string Article_Cate_Href = tools.CheckStr(Request.Form["Article_Cate_Href"]);
        int Article_Cate_Sort = tools.CheckInt(Request.Form["Article_Cate_Sort"]);
        string Article_Cate_Site = Public.GetCurrentSite();

        string Article_Cate_SEO_Title = tools.CheckStr(Request.Form["Article_Cate_SEO_Title"]);
        string Article_Cate_SEO_Keyword = tools.CheckStr(Request.Form["Article_Cate_SEO_Keyword"]);
        string Article_Cate_SEO_Description = tools.CheckStr(Request.Form["Article_Cate_SEO_Description"]);
        int Article_Cate_IsTop= tools.CheckInt(Request.Form["Article_Cate_IsTop"]);
        int Article_Cate_Type= tools.CheckInt(Request.Form["Article_Cate_Type"]);
        if (Article_Cate_Name == "") { Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}"); return; }
        if (Article_Cate_ParentID == 0)
        {
            Article_Cate_ParentID = tools.CheckInt(Request.Form["Article_cate_parent"]);
        }
        if (Article_Cate_Type == 1 && Article_Cate_Href=="")
        {
            Public.Msg("error", "错误信息", "请填写外部链接", false, "{back}"); return;
        }
        ArticleCateInfo entity = new ArticleCateInfo();
        entity.Article_Cate_ID = Article_Cate_ID;
        entity.Article_Cate_ParentID = Article_Cate_ParentID;
        entity.Article_Cate_Name = Article_Cate_Name;
        entity.Article_Cate_Href = Article_Cate_Href;
        entity.Article_Cate_Sort = Article_Cate_Sort;
        entity.Article_Cate_Site = Article_Cate_Site;
        entity.Article_Cate_SEO_Title = Article_Cate_SEO_Title;
        entity.Article_Cate_SEO_Keyword = Article_Cate_SEO_Keyword;
        entity.Article_Cate_SEO_Description = Article_Cate_SEO_Description;
        entity.Article_Cate_IsTop = Article_Cate_IsTop;
        entity.Article_Cate_Type = Article_Cate_Type;
        if (MyBLL.AddArticleCate(entity, Public.GetUserPrivilege()))
        {
            Public.AddRBACUserLog(58, "", "文章类别添加", Article_Cate_Name, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_Cate_add.aspx");
        }
        else
        {
            Public.AddRBACUserLog(58, "", "文章类别添加", Article_Cate_Name, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditArticleCate()
    {

        int Article_Cate_ID = tools.CheckInt(Request.Form["Article_Cate_ID"]);
        int Article_Cate_ParentID = tools.CheckInt(Request.Form["Article_cate"]);
        string Article_Cate_Name = tools.CheckStr(Request.Form["Article_Cate_Name"]);
        string Article_Cate_Href = tools.CheckStr(Request.Form["Article_Cate_Href"]);
        int Article_Cate_Sort = tools.CheckInt(Request.Form["Article_Cate_Sort"]);
        string Article_Cate_Site = Public.GetCurrentSite();
        string Article_Cate_SEO_Title = tools.CheckStr(Request.Form["Article_Cate_SEO_Title"]);
        string Article_Cate_SEO_Keyword = tools.CheckStr(Request.Form["Article_Cate_SEO_Keyword"]);
        string Article_Cate_SEO_Description = tools.CheckStr(Request.Form["Article_Cate_SEO_Description"]);
        int Article_Cate_IsTop = tools.CheckInt(Request.Form["Article_Cate_IsTop"]);
        int Article_Cate_Type = tools.CheckInt(Request.Form["Article_Cate_Type"]);
        if (Article_Cate_Name == "") { Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}"); return; }
        if (Article_Cate_ParentID == 0)
        {
            Article_Cate_ParentID = tools.CheckInt(Request.Form["Article_cate_parent"]);
        }
        if (Article_Cate_ParentID == Article_Cate_ID)
        {
            Public.Msg("error", "错误信息", "分类选择有误", false, "{back}"); return;
        }
        if (Article_Cate_Type == 1 && Article_Cate_Href=="")
        {
            Public.Msg("error", "错误信息", "请填写外部链接", false, "{back}"); return;
        }
        ArticleCateInfo entity = new ArticleCateInfo();
        entity.Article_Cate_ID = Article_Cate_ID;
        entity.Article_Cate_ParentID = Article_Cate_ParentID;
        entity.Article_Cate_Name = Article_Cate_Name;
        entity.Article_Cate_Href = Article_Cate_Href;
        entity.Article_Cate_Sort = Article_Cate_Sort;
        entity.Article_Cate_Site = Article_Cate_Site;
        entity.Article_Cate_SEO_Title = Article_Cate_SEO_Title;
        entity.Article_Cate_SEO_Keyword = Article_Cate_SEO_Keyword;
        entity.Article_Cate_SEO_Description = Article_Cate_SEO_Description;
        entity.Article_Cate_IsTop = Article_Cate_IsTop;
        entity.Article_Cate_Type = Article_Cate_Type;
        if (MyBLL.EditArticleCate(entity, Public.GetUserPrivilege()))
        {
            Public.AddRBACUserLog(58, Article_Cate_ID.ToString(), "文章类别修改", Article_Cate_Name, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_Cate_list.aspx");
        }
        else
        {
            Public.AddRBACUserLog(58, Article_Cate_ID.ToString(), "文章类别修改", Article_Cate_Name, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void ListEditArticleCate()
    {

        int Article_Cate_ID = tools.CheckInt(Request["id"]);
        string Article_Cate_Name = tools.CheckStr(Request["ArticleCateInfo.Article_Cate_Name"]);
        int Article_Cate_Sort = tools.CheckInt(Request["ArticleCateInfo.Article_Cate_Sort"]);

        if (Article_Cate_Name == "") { Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}"); return; }

        ArticleCateInfo entity = GetArticleCateByID(Article_Cate_ID);
        if (entity != null)
        {
            entity.Article_Cate_ID = Article_Cate_ID;
            entity.Article_Cate_Name = Article_Cate_Name;
            entity.Article_Cate_Sort = Article_Cate_Sort;

            MyBLL.EditArticleCate(entity, Public.GetUserPrivilege());
        }
    }

    public virtual void DelArticleCate()
    {
        int Article_Cate_ID = tools.CheckInt(Request.QueryString["Article_Cate_ID"]);
        if (MyBLL.DelArticleCate(Article_Cate_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.AddRBACUserLog(58, Article_Cate_ID.ToString(), "文章类别删除", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_Cate_list.aspx");
        }
        else
        {
            Public.AddRBACUserLog(58, Article_Cate_ID.ToString(), "文章类别删除", "", 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual ArticleCateInfo GetArticleCateByID(int cate_id)
    {
        return MyBLL.GetArticleCateByID(cate_id,Public.GetUserPrivilege());
    }

    public string GetArticleCates()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int CateID = tools.CheckInt(Request["CateID"]);
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Name", "like", keyword));
        }
        if (CateID > 0)
        {
            string subcate = Get_All_SubCateID(CateID);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", "in", subcate.ToString()));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<ArticleCateInfo> entitys = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ArticleCateInfo entity in entitys)
            {
                ArticleCateInfo parentinfo = GetArticleCateByID(entity.Article_Cate_ParentID);

                jsonBuilder.Append("{\"id\":" + entity.Article_Cate_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_Cate_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Article_Cate_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (parentinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(parentinfo.Article_Cate_Name));
                }
                else
                {
                    jsonBuilder.Append("--");
                }
                parentinfo = null;
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_Cate_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("1daab676-20b6-4073-af76-132ee8874556"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"article_cate_edit.aspx?article_cate_id=" + entity.Article_Cate_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("cc00c494-d211-438c-baef-ac20d419b066"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('article_cate_do.aspx?action=move&article_cate_id=" + entity.Article_Cate_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }

    }

    public string ArticleCateOption(int selectValue, int Cate_ID)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", "<>", Cate_ID + ""));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "DESC"));
        IList<ArticleCateInfo> entitys = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ArticleCateInfo entity in entitys)
            {
                if (entity.Article_Cate_ID == selectValue)
                {
                    strHTML += "<option value=\"" + entity.Article_Cate_ID + "\" selected=\"selected\">" + entity.Article_Cate_Name + "</option>";
                }
                else
                {
                    strHTML += "<option value=\"" + entity.Article_Cate_ID + "\">" + entity.Article_Cate_Name + "</option>";
                }
                strHTML += ArticleSubCateOption(entity.Article_Cate_ID, selectValue, Cate_ID, "&nbsp;&nbsp;&nbsp;");
            }
        }
        return strHTML;
    }

    public string ArticleSubCateOption(int parent, int selectValue, int Cate_ID, string gapstr)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", parent.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", "<>", Cate_ID + ""));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "DESC"));
        IList<ArticleCateInfo> entitys = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ArticleCateInfo entity in entitys)
            {
                if (entity.Article_Cate_ID == selectValue)
                {
                    strHTML += "<option value=\"" + entity.Article_Cate_ID + "\" selected=\"selected\">" + gapstr + entity.Article_Cate_Name + "</option>";
                }
                else
                {
                    strHTML += "<option value=\"" + entity.Article_Cate_ID + "\">" + gapstr + entity.Article_Cate_Name + "</option>";
                }
                strHTML += ArticleSubCateOption(entity.Article_Cate_ID, selectValue, Cate_ID, gapstr + "&nbsp; ");
            }
        }
        return strHTML;
    }

    
    public string ArticleContentOption(int selectValue)
    {
        string strHTML = "";



        if (selectValue == 0)
        {
            strHTML += "<option value=\"" + "0" + "\" selected=\"selected\">" + "文章内容" + "</option>";
            strHTML += "<option value=\"" + "1" + "\">" + "超链接" + "</option>";
        }
        else
        {
            strHTML += "<option value=\"" + "0" + "\" >" + "文章内容" + "</option>";
            strHTML += "<option value=\"" + "1" + "\" selected=\"selected\">" + "超链接" + "</option>";
        }
                

        return strHTML;
    }

    public string Get_All_SubCateID(int ParentID)
    {
        return MyBLL.Get_All_SubCateID(ParentID);
    }

    public string Get_Category_Relate(int cate_id, string cate_str)
    {
        
        string cate_relate = cate_id.ToString();
        if (cate_id > 0)
        {
            ArticleCateInfo category = GetArticleCateByID(cate_id);
            if (category != null)
            {
                cate_relate = cate_relate + ",";
                cate_relate = cate_str + Get_Category_Relate(category.Article_Cate_ParentID, cate_relate);
            }
            else
            {
                cate_relate = "0";
            }
        }
        else
        {
            if (cate_str != "")
            {
                cate_relate = cate_str + cate_relate;
            }
        }
        return cate_relate;

    }

    public string Article_Category_Select(int cate_id, string div_name)
    {
        string select_list = "";
        string select_tmp = "";
        int grade = 0;
        int i;
        int parentid = 0;
        string select_name = "";
        string cate_relate = Get_Category_Relate(cate_id, "");
        cate_relate = cate_relate + ",";
        foreach (string cate in cate_relate.Split(','))
        {
            if (cate.Length > 0)
            {

                QueryInfo Query = new QueryInfo();
                Query.CurrentPage = 1;
                Query.PageSize = 0;
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", cate));
                Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "Desc"));
                IList<ArticleCateInfo> categorys = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());
                if (categorys != null)
                {

                    grade = grade + 1;
                    if (grade == 1)
                    {
                        select_tmp = "<select id=\"Article_cate\" name=\"Article_cate\" onchange=\"change_articlemaincate('" + div_name + "','Article_cate');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }
                    else
                    {
                        select_name = "Article_cate";
                        for (i = 1; i < grade; i++)
                        {
                            select_name = select_name + "_parent";
                        }
                        select_tmp = "<select id=\"" + select_name + "\" name=\"" + select_name + "\" onchange=\"change_articlemaincate('" + div_name + "','" + select_name + "');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }

                    foreach (ArticleCateInfo entity in categorys)
                    {
                        if (parentid == entity.Article_Cate_ID || cate_id == entity.Article_Cate_ID)
                        {
                            select_tmp = select_tmp + "<option value=\"" + entity.Article_Cate_ID + "\" selected>" + entity.Article_Cate_Name + "</option>";
                        }
                        else
                        {
                            select_tmp = select_tmp + "<option value=\"" + entity.Article_Cate_ID + "\">" + entity.Article_Cate_Name + "</option>";
                        }
                    }
                    select_tmp = select_tmp + "</select> ";
                    parentid = tools.CheckInt(cate);
                }

                Query = null;
                categorys = null;
                select_list = select_tmp + select_list;
            }
        }
        return select_list;
    }


    public string Article_LettList(string URL)
    {
        StringBuilder jsonBuilder = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize =0;
        Query.CurrentPage =1;

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "ASC"));

        IList<ArticleCateInfo> entitys = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());
        List<ArticleCateInfo> subCate = null;
        List<ArticleCateInfo> ThirdList = null;

        if (entitys!=null)
        {
            List<ArticleCateInfo> FirstList = entitys.Where(P => P.Article_Cate_ParentID== 0).ToList();

            if(FirstList!=null)
            {
                foreach(ArticleCateInfo One in FirstList)
                {
                    jsonBuilder.Append("<li onclick=\"menuOn(this);\"><a href=\""+ URL + "?Article_Cate="+ One.Article_Cate_ID + "\"  target=\"main\">&nbsp;&nbsp;&nbsp;&nbsp;" + One.Article_Cate_Name + "</a></li>");

                    //subCate = entitys.Where(P => P.Article_Cate_ParentID == One.Article_Cate_ID).ToList();

                    //if(subCate!=null)
                    //{
                    //    foreach (ArticleCateInfo Two in subCate)
                    //    {
                    //        jsonBuilder.Append("<li onclick=\"menuOn(this);\"><a href=\"" + URL + "?Article_Cate=" + Two.Article_Cate_ID + "\"  target=\"main\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Two.Article_Cate_Name + "</a></li>");

                    //        //ThirdList = entitys.Where(P => P.Article_Cate_ParentID == Two.Article_Cate_ID).ToList();

                    //        //if(ThirdList!=null)
                    //        //{
                    //        //    foreach (ArticleCateInfo Third in ThirdList)
                    //        //    {
                    //        //        jsonBuilder.Append("<li onclick=\"menuOn(this);\"><a href=\"" + URL + "?Article_Cate=" + Third.Article_Cate_ID + "\"  target=\"main\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Third.Article_Cate_Name + "</a></li>");
                    //        //    }
                    //        //}
                    //    }
                    //}
                }
            }
        }

        return jsonBuilder.ToString();
    }

    /// <summary>
    /// 文章页面导航
    /// </summary>
    /// <param name="Cate_ID"></param>
    /// <param name="gap_char"></param>
    /// <returns></returns>
    public string GetArticleInfo_Cate_Nav(int Cate_ID, string gap_char)
    {
        string cate_nav = "";
        ArticleCateInfo category = MyBLL.GetArticleCateByID(Cate_ID, Public.GetUserPrivilege());
        if (category != null)
        {
            cate_nav = cate_nav + "<a>" + category.Article_Cate_Name + "</a>";

            cate_nav = GetArticleInfo_Cate_Nav(category.Article_Cate_ParentID, gap_char) + gap_char + cate_nav;
        }
        return cate_nav;
    }

    public string ArticleCateTree(int parentid, string Article_CateIDs)
    {
        string cateidbak = "," + Article_CateIDs + ",";
        string strHTML = "";
        QueryInfo query = new QueryInfo();
        query.PageSize = 0;
        query.CurrentPage = 1;
        query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", Public.GetCurrentSite()));
        query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_ParentID", "=", parentid.ToString()));
        query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "Asc"));
        IList<ArticleCateInfo> entitys = MyBLL.GetArticleCates(query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ArticleCateInfo entity in entitys)
            {
                if (GetArticleCateCount(entity.Article_Cate_ID) > 0)
                {
                    if (cateidbak.IndexOf("," + entity.Article_Cate_ID + ",") >= 0)
                    {
                        strHTML += "<item text=\"" + entity.Article_Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Article_Cate_ID + "\" open=\"yes\" checked=\"yes\">\n";
                    }
                    else
                    {
                        strHTML += "<item text=\"" + entity.Article_Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Article_Cate_ID + "\">\n";
                    }
                    strHTML += ArticleCateTree(entity.Article_Cate_ID, Article_CateIDs);
                    strHTML += "</item>\n";
                }
                else
                {
                    if (cateidbak.IndexOf("," + entity.Article_Cate_ID + ",") >= 0)
                    {
                        strHTML += "<item text=\"" + entity.Article_Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Article_Cate_ID + "\" checked=\"yes\" />\n";
                    }
                    else
                    {
                        strHTML += "<item text=\"" + entity.Article_Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Article_Cate_ID + "\" />\n";
                    }
                }
            }
        }
        return strHTML;
    }
    public int GetArticleCateCount(int Article_CateID)
    {
        QueryInfo query = new QueryInfo();
        query.PageSize = 0;
        query.CurrentPage = 1;
        query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", Public.GetCurrentSite()));
        query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_ParentID", "=", Article_CateID.ToString()));
        IList<ArticleCateInfo> entitys = MyBLL.GetArticleCates(query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            return entitys.Count;
        }
        else
        {
            return 0;
        }
    }

    public int GetArticleCateAmount(int cate_id)
    {
        int amount = 0;
        QueryInfo Query = new QueryInfo();
        Query.CurrentPage = 1;
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_ParentID", "=", cate_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", Public.GetCurrentSite()));

        IList<ArticleCateInfo> entitys = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            amount = amount + entitys.Count;
            //foreach (ArticleCateInfo entity in entitys)
            //{
            //    amount = amount + GetArticleCateAmount(entity.Article_Cate_ID);
            //}
        }
        return amount;
    }

  
}

public class SensitiveWords
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISensitiveWords MyBLL;

    public SensitiveWords()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SensitiveWordsFactory.CreateSensitiveWords();
    }

    public virtual void AddSensitiveWords()
    {
        int ID = tools.CheckInt(Request.Form["ID"]);
        string Name = tools.CheckStr(Request.Form["Name"]);

        if (Name == "") { Public.Msg("error", "错误信息", "请填写敏感词", false, "{back}"); return; }

        SensitiveWordsInfo entity = new SensitiveWordsInfo();
        entity.ID = ID;
        entity.Name = Name;

        if (MyBLL.AddSensitiveWords(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "SensitiveWords_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditSensitiveWords()
    {

        int ID = tools.CheckInt(Request.Form["ID"]);
        string Name = tools.CheckStr(Request.Form["Name"]);

        if (Name == "") { Public.Msg("error", "错误信息", "请填写敏感词", false, "{back}"); return; }

        SensitiveWordsInfo entity = GetSensitiveWordsByID(ID);

        if(entity!=null)
        {
            entity.ID = ID;
            entity.Name = Name;


            if (MyBLL.EditSensitiveWords(entity))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "{close}");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
        
    }

    public virtual void DelSensitiveWords()
    {
        int ID = tools.CheckInt(Request.QueryString["ID"]);
        if (MyBLL.DelSensitiveWords(ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "SensitiveWords_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual SensitiveWordsInfo GetSensitiveWordsByID(int ID)
    {
        return MyBLL.GetSensitiveWordsByID(ID);
    }

    public string GetSensitiveWordss()
    {
        string keyword = tools.CheckStr(Request["keyword"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SensitiveWordsInfo.ID", ">","0"));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SensitiveWordsInfo.Name", "like", keyword));
        }
        
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query);

        IList<SensitiveWordsInfo> entitys = MyBLL.GetSensitiveWordss(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SensitiveWordsInfo entity in entitys)
            {


                jsonBuilder.Append("{\"id\":" + entity.ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Name));
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("36d9082b-e75e-4079-818f-a7b4c9a7cc31"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"SensitiveWords_edit.aspx?ID=" + entity.ID + "\\\" title=\\\"修改\\\" target=\\\"_blank\\\">修改</a>");
                }

                if (Public.CheckPrivilege("c5da61a0-10a1-4d17-ab31-7bde1e7ddcf2"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('SensitiveWords_do.aspx?action=move&ID=" + entity.ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }
    }

    public IList<SensitiveWordsInfo> GetSensitiveWordsByCache()
    {
        IList<SensitiveWordsInfo> entitys = null;

        IEncrypt encrypt = EncryptFactory.CreateEncrypt();

        string CacheKey = encrypt.SHA1("SensitiveWordsKey");

        if (Public.IsCacheExist(CacheKey))
        {
            entitys=(IList<SensitiveWordsInfo>)Public.GetCache(CacheKey);
        }
        else
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize =0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SensitiveWordsInfo.ID", ">", "0"));
            Query.OrderInfos.Add(new OrderInfo("SensitiveWordsInfo.ID", "ASC"));

            entitys = MyBLL.GetSensitiveWordss(Query);


            Public.AddCache(CacheKey, entitys, 20);
        }

        return entitys;
    }


    /// <summary>
    /// 过滤敏感词
    /// </summary>
    /// <param name="Words"></param>
    /// <returns></returns>
    public string FilterSensitiveWords(string Words)
    {
        if(Words.Length>0)
        {
            IList<SensitiveWordsInfo> entitys = GetSensitiveWordsByCache();
            if(entitys!=null)
            {
                foreach(SensitiveWordsInfo entity in entitys)
                {
                    Words=Words.Replace(entity.Name, "<span style=\"color:#E53333;\"><u>"+ entity.Name+"</u></span>");

                    if (Words.Length==0)
                    {
                        break;
                    }
                }
            }
        }

        return Words;
    }
  
}



/// <summary>
/// 专题报道
/// </summary>
public class ArticleSubject
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IArticleSubject MyBLL;

    public ArticleSubject()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ArticleFactory.CreateArticleSubject();
    }
    public virtual void AddArticleSubject()
    {
        int Subject_ID = tools.CheckInt(Request.Form["Subject_ID"]);
        string Subject_Name = tools.CheckStr(Request.Form["Subject_Name"]);
        string Subject_Img = tools.CheckStr(Request.Form["Subject_Img"]);
        int Subject_IsActive = tools.CheckInt(Request.Form["Subject_IsActive"]);
        int Subject_Sort = tools.CheckInt(Request.Form["Subject_Sort"]);
        string Subject_Site = Public.GetCurrentSite();


        if (Subject_Name == "")
        {
            { Public.Msg("error", "错误信息", "专题名称不可为空", false, "{back}"); return; }
        }

        ArticleSubjectInfo entity = new ArticleSubjectInfo();
        entity.Subject_ID = Subject_ID;
        entity.Subject_Name = Subject_Name;
        entity.Subject_Img = Subject_Img;
        entity.Subject_IsActive = Subject_IsActive;
        entity.Subject_Sort = Subject_Sort;
        entity.Subject_Site = Subject_Site;

        if (IsHaveSubject(Subject_Name, 0))
        {
            { Public.Msg("error", "错误信息", "专题名称已存在,请您重新填写", false, "{back}"); return; }
        }
        else
        {
            if (MyBLL.AddArticleSubject(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "subject_add.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }

    public virtual void EditArticleSubject()
    {

        int Subject_ID = tools.CheckInt(Request.Form["Subject_ID"]);
        string Subject_Name = tools.CheckStr(Request.Form["Subject_Name"]);
        string Subject_Img = tools.CheckStr(Request.Form["Subject_Img"]);
        int Subject_IsActive = tools.CheckInt(Request.Form["Subject_IsActive"]);
        int Subject_Sort = tools.CheckInt(Request.Form["Subject_Sort"]);
        string Subject_Site = Public.GetCurrentSite();

        if (Subject_Name == "")
        {
            { Public.Msg("error", "错误信息", "专题名称不可为空", false, "{back}"); return; }
        }

        ArticleSubjectInfo entity = new ArticleSubjectInfo();
        entity.Subject_ID = Subject_ID;
        entity.Subject_Name = Subject_Name;
        entity.Subject_Img = Subject_Img;
        entity.Subject_IsActive = Subject_IsActive;
        entity.Subject_Sort = Subject_Sort;
        entity.Subject_Site = Subject_Site;

        if (IsHaveSubject(Subject_Name, Subject_ID))
        {
            { Public.Msg("error", "错误信息", "专题名称已存在,请您重新填写", false, "{back}"); return; }
        }
        else
        {
            if (MyBLL.EditArticleSubject(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "subject_list.aspx");

            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }

    public virtual void DelArticleSubject()
    {
        int Subject_ID = tools.CheckInt(Request.QueryString["subject_id"]);
        if (MyBLL.DelArticleSubject(Subject_ID, Public.GetUserPrivilege()) > 0)
        {
            //Public.Msg("positive", "操作成功", "操作成功", true, "subject_list.aspx");
            Response.Redirect("subject_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual ArticleSubjectInfo GetArticleSubjectByID(int cate_id)
    {
        return MyBLL.GetArticleSubjectByID(cate_id, Public.GetUserPrivilege());
    }
    public string GetArticleSubjects()
    {

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);


        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleSubjectInfo.Subject_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleSubjectInfo.Subject_Name", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<ArticleSubjectInfo> entitys = MyBLL.GetArticleSubjects(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ArticleSubjectInfo entity in entitys)
            {
                jsonBuilder.Append("{\"AboutInfo.About_ID\":" + entity.Subject_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Subject_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Subject_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Subject_IsActive == 0 ? "否" : "是"));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Subject_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("ae5b5047-b85f-4934-84a0-e4f4f898dd78"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"subject_edit.aspx?subject_id=" + entity.Subject_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("79d6139b-950d-4598-9a90-1cb67505205e"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('subject_do.aspx?action=move&subject_id=" + entity.Subject_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 是否存在该专题,如果存在,不可添加
    /// </summary>
    /// <param name="SubjectName">专题名称</param>
    /// <returns></returns>
    public bool IsHaveSubject(string SubjectName, int SubjetcID)
    {
        bool b = false;
        ArticleSubjectInfo entity = MyBLL.GetArticleSubjectByName(SubjectName, SubjetcID);
        if (entity != null)
        {
            b = true;
        }
        return b;
    }

    /// <summary>
    /// 获取可用的专题,如果为资讯编辑
    /// </summary>
    /// <param name="SubjectID"></param>
    /// <returns></returns>
    public string GetArticleSubjectSelect(int SubjectID)
    {
        StringBuilder sbSubjectSelect = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleSubjectInfo.Subject_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleSubjectInfo.Subject_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ArticleSubjectInfo.Subject_ID", "asc"));
        IList<ArticleSubjectInfo> entitys = MyBLL.GetArticleSubjects(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            string isselected = "";
            sbSubjectSelect.Append("<select name=\"Subject_ID\">");
            if (SubjectID == 0)
            {
                sbSubjectSelect.Append("<option value='0' selected=\"selected\">请选择专题</option>");
            }
            else
            {
                sbSubjectSelect.Append("<option value='0' >请选择专题</option>");
            }
            foreach (ArticleSubjectInfo item in entitys)
            {
                if (SubjectID == item.Subject_ID)
                {
                    isselected = " selected=\"selected\"";
                }
                else
                {
                    isselected = "";
                }
                sbSubjectSelect.Append("<option value=" + item.Subject_ID + " " + isselected + ">" + item.Subject_Name + "</option>");
            }
            sbSubjectSelect.Append("</select>");
        }
        else
        {
            sbSubjectSelect.Append("暂无专题");
        }
        return sbSubjectSelect.ToString();
    }
}

