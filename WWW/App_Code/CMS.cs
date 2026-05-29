using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.CMS;
using Glaer.Trade.B2C.BLL.Sys;
using System.Linq;
using Glaer.Trade.B2C.BLL.SAL;

/// <summary>
/// CMS 的摘要说明
/// </summary>
public class CMS
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private INotice Webnotice;
    private INoticeCate MyNoticeCate;
    private ITools tools;
    private IHelpCate MyHelpCate;
    private IHelp MyHelp;
    private IAbout MyAbout;
    private Public_Class pub;
    private IArticle MyArticle;

    private IArticleCate MyArticleCate;
    private IFriendlyLink mylink;
    private ISpecial MySpe;
    private AD ad;
    public CMS()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
        Webnotice = NoticeFactory.CreateNotice();
        tools = ToolsFactory.CreateTools();
        MyHelpCate = HelpFactory.CreateHelpCate();
        MyHelp = HelpFactory.CreateHelp();
        MyAbout = AboutFactory.CreateAbout();
        pub = new Public_Class();
        MyNoticeCate = NoticeFactory.CreateNoticeCate();
        MyArticle = ArticleFactory.CreateArticle();
        MyArticleCate = ArticleFactory.CreateArticleCate();
        mylink = FriendlyLinkFactory.CreateFriendlyLink();
        MySpe = SpecialFactory.CreateSpecial();
        ad = new AD();
    }

    public void UpdatePages(int ID)
    {
        Glaer.Trade.Util.SQLHelper.ISQLHelper DBHelper = Glaer.Trade.Util.SQLHelper.SQLHelperFactory.CreateSQLHelper();

        DBHelper.ExecuteNonQuery("update Article set Article_PageViews = Article_PageViews+1 where Article_ID = " + ID);
    }

    //获取文章所有子分类
    public string Get_All_SubCate(int Cate_id)
    {
        string Cate_Arry = MyArticleCate.Get_All_SubCateID(Cate_id);
        return Cate_Arry;
    }

    public virtual ArticleCateInfo GetArticleCateByID(int cate_id)
    {
        return MyArticleCate.GetArticleCateByID(cate_id, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
    }
    public int GetParentCate_ID(int SubCate_id)
    {

        ArticleCateInfo acinfo = GetArticleCateByID(SubCate_id);
        if (acinfo != null)
        {
            if (acinfo.Article_Cate_ParentID == 0)
            {
                return acinfo.Article_Cate_ID;
            }
            else
            {
                return GetParentCate_ID(acinfo.Article_Cate_ParentID);
            }
        }
        else
        {
            return 0;
        }
    }
    public int GetParentSecondCate_ID(int SubCate_id)
    {

        ArticleCateInfo acinfo = GetArticleCateByID(SubCate_id);


        if (acinfo != null)
        {
            ArticleCateInfo acinfoP = GetArticleCateByID(acinfo.Article_Cate_ParentID);
            if (acinfoP != null)
            {

                if (acinfoP.Article_Cate_ParentID == 0)
                {
                    return acinfo.Article_Cate_ID;
                }
                else
                {
                    return GetParentSecondCate_ID(acinfo.Article_Cate_ParentID);
                }
            }
            else
            {
                return acinfo.Article_Cate_ID;
            }
        }
        else
        {
            return 0;
        }
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
        ArticleCateInfo category = MyArticleCate.GetArticleCateByID(Cate_ID, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (category != null)
        {

            cate_nav = cate_nav + "<a href=\"/" + category.Article_Cate_ID + "/ \">" + category.Article_Cate_Name + "</a>";

            cate_nav = GetArticleInfo_Cate_Nav(category.Article_Cate_ParentID, gap_char) + gap_char + cate_nav;
        }
        return cate_nav;
    }

    #region 首页
    /// <summary>
    /// 首页导航
    /// </summary>
    /// <param name="Select"></param>
    /// <returns></returns>
    public string Home_Navigation()
    {
        StringBuilder StrHTML = new StringBuilder("");


        int index = tools.CheckInt(Request["cate_id"]);
        int noticecate_id = tools.CheckInt(Request["noticecate_id"]);
        int CurrentPotion = tools.NullInt(Session["CurrentPotion"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 12;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_IsTop", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "ASC"));
        IList<ArticleCateInfo> ArticleCates = MyArticleCate.GetArticleCates(Query, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (ArticleCates != null)
        {
            int i = 1;
            foreach (ArticleCateInfo entity in ArticleCates)
            {
                string Css = "";
                if (i == 1)
                {
                    if (index == 0 && CurrentPotion == 0)
                    {
                        Css = "class='active'";
                    }
                    StrHTML.Append(" <li " + Css + "><a href=\"/\">首页</a></li>");
                    Css = "";
                }
                if (index != 0)
                {
                    if (GetParentCate_ID(index) == entity.Article_Cate_ID)
                    {
                        Css = "class='active'";
                    }
                }
                else
                {
                    if (noticecate_id != 0)
                    {
                        if (GetParentCate_ID(CurrentPotion) == entity.Article_Cate_ID)
                        {
                            Css = "class='active'";
                        }
                    }
                }
                StrHTML.Append("<li " + Css + ">");
                if (entity.Article_Cate_Type == 1)
                {
                    StrHTML.Append("<a href=\"" + entity.Article_Cate_Href + "\">" + entity.Article_Cate_Name + "</a>");
                }
                else
                {
                    StrHTML.Append("<a href=\"/" + entity.Article_Cate_ID + "/\">" + entity.Article_Cate_Name + "</a>");
                }

                StrHTML.Append("</li>");
                i++;
            }
        }

        return StrHTML.ToString();
    }


    public string Home_TopOne(int PageSize)
    {
        StringBuilder StrHTML = new StringBuilder("");

        string Top = "";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Artide_IsTop", "=", "1"));

        //Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Hits", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {

            Top = "  <ul class='new-news'>";
            foreach (ArticleInfo entity in entitys)
            {
                Top = Top + "<li><a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">" + entity.Article_Title + "</a></li>";

            }
            Top += "</ul>";
        }


        return Top;
    }

    public string Home_ArticleList()
    {
        StringBuilder StrHTML = new StringBuilder("");



        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_IsTop", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "ASC"));
        IList<ArticleCateInfo> ArticleCates = MyArticleCate.GetArticleCates(Query, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (ArticleCates != null)
        {
            int i = 0;
            foreach (ArticleCateInfo entity in ArticleCates)
            {
                i++;
                if (i % 2 == 1)
                {
                    StrHTML.Append(GetHome_ArticleList(entity, true));
                }
                else
                {
                    StrHTML.Append(GetHome_ArticleList(entity, false));
                }

            }
        }

        return StrHTML.ToString();
    }


    public void Home_ArticleRightList(ref string TopRight, ref string RightList)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_IsTop", "=", "2"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "ASC"));
        IList<ArticleCateInfo> ArticleCates = MyArticleCate.GetArticleCates(Query, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (ArticleCates != null)
        {
            int i = 0;
            foreach (ArticleCateInfo entity in ArticleCates)
            {
                i++;
                if (i == 1)
                {
                    TopRight = GetRightTop_ArticleList(entity);
                }
                else
                {
                    RightList = RightList + GetRight_ArticleList(entity);
                }
            }
        }
    }
    /// <summary>
    /// 首页分类列表及文章展示
    /// </summary>
    /// <param name="cate"></param>
    /// <returns></returns>
    public string GetHome_ArticleList(ArticleCateInfo cate, bool IsRight)
    {
        StringBuilder StrHTML = new StringBuilder("");

        if (cate != null)
        {
            if (IsRight)
            {
                StrHTML.Append("<div class=\"more-list-l clearfix\">");

            }
            else
            {
                StrHTML.Append("<div class=\"more-list-l clearfix\" style=\"margin-right:0px;\">");
            }
            StrHTML.Append("<div class=\"more-list-tit\">");
            StrHTML.Append("<span><a  href=\"/" + cate.Article_Cate_ID + "/\" style=\"color:#c50c11;\">" + cate.Article_Cate_Name + "</a></span>");
            StrHTML.Append(" <em><a href=\"/" + cate.Article_Cate_ID + "/\">更多></a></em>");
            StrHTML.Append("</div>");
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 6;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate.Article_Cate_ID)));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
            if (entitys != null)
            {
                int i = 0;
                foreach (ArticleInfo entity in entitys)
                {
                    i++;
                    if (i == 1)
                    {
                        StrHTML.Append("<div class=\"more-list-pic clearfix\">");
                        StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                        StrHTML.Append("<div class=\"more-list-pic-1\">");
                        StrHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Article_Img, "thumbnail") + "\">");
                        //StrHTML.Append("<img class=\"lazy\" data-original=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\">");

                        StrHTML.Append("</div>");
                        StrHTML.Append("<div class=\"more-list-pic-text\">");
                        StrHTML.Append("<h4>" + entity.Article_Title + "</h4>");
                        StrHTML.Append("<p>" + entity.Article_Intro + "</p>");
                        StrHTML.Append("</div>");
                        StrHTML.Append("</a>");
                        StrHTML.Append("</div>");
                    }
                    else
                    {
                        StrHTML.Append("<p><a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\"><i></i>" + entity.Article_Title + "</a></p>");
                    }
                }
            }

            StrHTML.Append("</div>");
        }
        return StrHTML.ToString();
    }

    /// <summary>
    /// 首页分类列表及文章展示
    /// </summary>
    /// <param name="cate"></param>
    /// <returns></returns>
    public string GetTop_ArticleList(ArticleCateInfo cate)
    {
        StringBuilder StrHTML = new StringBuilder("");

        if (cate != null)
        {

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 12;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate.Article_Cate_ID)));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
            if (entitys != null)
            {
                int i = 0;
                foreach (ArticleInfo entity in entitys)
                {
                    i++;
                    if (i % 6 == 1)
                    {
                        StrHTML.Append("<h3><a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\"><i></i>" + entity.Article_Title + "</a></h3>");
                    }
                    else
                    {
                        StrHTML.Append("<p><a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\"><i></i>" + entity.Article_Title + "</a></p>");
                    }
                }
            }

            StrHTML.Append("</div>");
        }
        return StrHTML.ToString();
    }

    /// <summary>
    /// 首页分类列表及文章展示
    /// </summary>
    /// <param name="cate"></param>
    /// <returns></returns>
    public string GetRight_ArticleList(ArticleCateInfo cate)
    {
        StringBuilder StrHTML = new StringBuilder("");

        if (cate != null)
        {
            StrHTML.Append("<div class=\"more-list-r-center\">");
            StrHTML.Append("<div class=\"more-list-r-tit\">");
            StrHTML.Append("<span>" + cate.Article_Cate_Name + "</span>");
            StrHTML.Append(" <em><a href=\"/" + cate.Article_Cate_ID + "/\">更多></a></em>");
            StrHTML.Append("</div>");
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 12;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate.Article_Cate_ID)));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
            if (entitys != null)
            {
                int i = 0;
                foreach (ArticleInfo entity in entitys)
                {
                    i++;
                    if (i == 1)
                    {
                        StrHTML.Append("<div class=\"more-list-r-pic clearfix\">");
                        StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                        StrHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Article_Img, "thumbnail") + "\">");
                        StrHTML.Append("<span>" + entity.Article_Title + "</span>");

                        StrHTML.Append("</a>");
                        StrHTML.Append("</div>");
                    }
                    else
                    {
                        StrHTML.Append("<p><a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\"><i></i>" + entity.Article_Title + "</a></p>");
                    }
                }
            }

            StrHTML.Append("</div>");
        }
        return StrHTML.ToString();
    }

    /// <summary>
    /// 首页分类列表及文章展示
    /// </summary>
    /// <param name="cate"></param>
    /// <returns></returns>
    public string GetRightTop_ArticleList(ArticleCateInfo cate)
    {
        StringBuilder StrHTML = new StringBuilder("");

        if (cate != null)
        {
            StrHTML.Append("<div class=\"center-right-tit\">");
            StrHTML.Append("<span>" + cate.Article_Cate_Name + "</span>");
            StrHTML.Append(" <em><a href=\"/" + cate.Article_Cate_ID + "/\">更多></a></em>");
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 3;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", cate.Article_Cate_ID.ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
            if (entitys != null)
            {
                int i = 0;
                StrHTML.Append("<ul class=\"center-right-list\">");
                foreach (ArticleInfo entity in entitys)
                {
                    i++;
                    StrHTML.Append("<li>");
                    StrHTML.Append("<a href=\"/special/" + entity.Article_CateID + "/\" title=\"" + entity.Article_Title + "\">");
                    StrHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\">");
                    StrHTML.Append("<h3>" + entity.Article_Title + "</h3>");
                    StrHTML.Append("<p>" + entity.Article_Intro + "</p>");
                    StrHTML.Append("</a>");
                    StrHTML.Append("</li>");
                }
                StrHTML.Append("</ul>");
            }

            StrHTML.Append("</div>");
        }
        return StrHTML.ToString();
    }


    /// <summary>
    /// 首页图片新闻
    /// </summary>
    /// <param name="CateID"></param>
    /// <returns></returns>
    public string Home_ImgNews(int CateID)
    {
        StringBuilder StrHTML = new StringBuilder("");

        StrHTML.Append("<div class=\"lead-list2-tit clearfix\">");
        StrHTML.Append("<img src=\"/images/tit-3.png\">");
        StrHTML.Append(" <span><a href=\"/" + CateID + "/\">更多></a></span>");
        StrHTML.Append("</div>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(CateID)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            StrHTML.Append("<ul class=\"slideshow-pic\">");
            foreach (ArticleInfo entity in entitys)
            {
                StrHTML.Append("<li>");
                StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                StrHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\">");
                StrHTML.Append("<p>" + entity.Article_Title + "</p>");
                StrHTML.Append("</a>");
                StrHTML.Append("</li>");
            }
            StrHTML.Append("</ul>");
        }
        return StrHTML.ToString();
    }

    /// <summary>
    /// 首页图片新闻
    /// </summary>
    /// <param name="CateID"></param>
    /// <returns></returns>
    public string Home_InterviewNews(int CateID)
    {
        StringBuilder StrHTML = new StringBuilder("");

        StrHTML.Append("<div class=\"lead-list3-tit clearfix\">");
        StrHTML.Append("<img src=\"/images/tit-gd.png\">");
        StrHTML.Append(" <span><a href=\"/" + CateID + "/ \">更多></a></span>");
        StrHTML.Append("</div>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(CateID)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            StrHTML.Append("<ul class=\"interview-pic\">");
            foreach (ArticleInfo entity in entitys)
            {
                StrHTML.Append("<li>");
                StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                StrHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Article_Img, "thumbnail2") + "\">");
                //StrHTML.Append("<img class=\"lazy2\" data-original=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\"  src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" >");
                StrHTML.Append("<h3>" + entity.Article_Title + "</h3>");
                StrHTML.Append("<p>" + entity.Article_Intro + "</p>");
                StrHTML.Append("</a>");
                StrHTML.Append("</li>");
            }
            StrHTML.Append("</ul>");
        }
        return StrHTML.ToString();
    }


    /// <summary>
    /// 声音
    /// </summary>
    /// <param name="CateID"></param>
    /// <returns></returns>
    public string Home_Voice(int CateID)
    {
        StringBuilder StrHTML = new StringBuilder("");
        ArticleCateInfo cate = GetArticleCateByID(CateID);
        if (cate != null)
        {
            StrHTML.Append("<div class=\"more-list-r-center\">");
            StrHTML.Append("<div class=\"more-list-r-tit\">");
            StrHTML.Append("<span>" + cate.Article_Cate_Name + "</span>");
            StrHTML.Append(" <em><a href=\"/Voice/" + cate.Article_Cate_ID + "/\">更多></a></em>");
            StrHTML.Append("</div>");

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 4;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate.Article_Cate_ID)));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
            if (entitys != null)
            {
                int i = 0;
                foreach (ArticleInfo entity in entitys)
                {
                    i++;
                    if (i == 1)
                    {
                        StrHTML.Append("<div class=\"more-list-r-pic clearfix\">");
                        StrHTML.Append("<a href=\"/Voice/" + cate.Article_Cate_ID + "/\" title=\"" + entity.Article_Title + "\" >");
                        StrHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" class=\"name-pic\">");
                        StrHTML.Append("<b>" + entity.Article_Title + "</b>");
                        StrHTML.Append("<span>" + entity.Article_Intro + "</span>");
                        StrHTML.Append("</a>");
                        StrHTML.Append("</div>");
                    }
                    else
                    {
                        StrHTML.Append("<p><a href=\"/Voice/" + cate.Article_Cate_ID + "/\" title=\"" + entity.Article_Title + "\" class=\"a_voice\" ><b style=\"font-size:16px;\">" + entity.Article_Title + "</b> | " + entity.Article_Intro + "</a></p>");
                    }
                }
            }
            StrHTML.Append("</div>");
        }

        return StrHTML.ToString();
    }

    /// <summary>
    /// 人事任免
    /// </summary>
    /// <param name="CateID"></param>
    /// <returns></returns>
    public string Home_Personnel(int CateID)
    {
        StringBuilder StrHTML = new StringBuilder("");
        ArticleCateInfo cate = GetArticleCateByID(CateID);

        if (cate != null)
        {
            StrHTML.Append("<div class=\"more-list-r-center\">");
            StrHTML.Append("<div class=\"more-list-r-tit\">");
            StrHTML.Append("<span>" + cate.Article_Cate_Name + "</span>");
            StrHTML.Append(" <em><a href=\"/personnel/" + cate.Article_Cate_ID + "/\">更多></a></em>");
            StrHTML.Append("</div>");

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 8;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate.Article_Cate_ID)));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
            if (entitys != null)
            {
                foreach (ArticleInfo entity in entitys)
                {
                    StrHTML.Append("<p><a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\"><i></i>" + entity.Article_Title + "</a></p>");
                }
            }
            StrHTML.Append("</div>");
        }

        return StrHTML.ToString();
    }

    public string Home_Articles(int CateID)
    {
        StringBuilder StrHTML = new StringBuilder("");
        ArticleCateInfo cate = GetArticleCateByID(CateID);

        if (cate != null)
        {
            StrHTML.Append("<div class=\"more-list-r-center\" style=\"text-align: center;\">");
            StrHTML.Append("<div class=\"more-list-r-tit\" style=\"text-align: left;\">");
            StrHTML.Append("<span>" + cate.Article_Cate_Name + "</span>");
            //StrHTML.Append(" <em><a href=\"/" + cate.Article_Cate_ID + "/\">更多></a></em>");
            StrHTML.Append("</div>");

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 1;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate.Article_Cate_ID)));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
            if (entitys != null)
            {
                foreach (ArticleInfo entity in entitys)
                {
                    //StrHTML.Append("<div class=\"more-list-r-pic clearfix\">");
                    StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                    //StrHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\"  style=\"padding: 10px;\">");
                    StrHTML.Append("<img class=\"lazy\" data-original=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\"  style=\"padding: 10px;\">");

                    //StrHTML.Append("<p>" + entity.Article_Title + "</p>");

                    StrHTML.Append("</a>");
                    //StrHTML.Append("</div>");

                }
            }
            StrHTML.Append("</div>");
        }

        return StrHTML.ToString();
    }
    #endregion

    /// <summary>
    /// 获取第一级分类
    /// </summary>
    /// <param name="CateID"></param>
    /// <returns></returns>
    public int GetTopArticleCate(int CateID)
    {
        ArticleCateInfo entity = MyArticleCate.GetArticleCateByID(CateID, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));

        if (entity != null)
        {
            if (entity.Article_Cate_ParentID == 0)
            {
                return entity.Article_Cate_ID;
            }
            else
            {
                return GetTopArticleCate(entity.Article_Cate_ParentID);
            }

            //return GetTopArticleCate(entity.Article_Cate_ParentID);
        }
        else
        {
            return CateID;
        }
    }

    #region 关于我们

    public AboutInfo GetAboutBySign(string sign)
    {
        return MyAbout.GetAboutBySign(sign, pub.CreateUserPrivilege("db8de73b-9ac0-476e-866e-892dd35589c5"));
    }


    public string Bottom_About()
    {
        string help_html = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "AboutInfo.About_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "AboutInfo.About_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "AboutInfo.About_Sign", "<>", "register"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "AboutInfo.About_Sign", "<>", "m_notice"));
        Query.OrderInfos.Add(new OrderInfo("AboutInfo.About_Sort", "ASC"));
        IList<AboutInfo> abouts = MyAbout.GetAbouts(Query, pub.CreateUserPrivilege("db8de73b-9ac0-476e-866e-892dd35589c5"));
        Query = null;

        if (abouts != null)
        {
            foreach (AboutInfo entity in abouts)
            {
                help_html = help_html + " | <a href=\"/about/index.aspx?sign=" + entity.About_Sign + "\" target=\"_blank\">" + entity.About_Title + "</a> ";
            }
        }

        return help_html;
    }
    #endregion


    #region 友情链接

    /// <summary>
    /// 文字友情链接
    /// </summary>
    /// <param name="cateID"></param>
    /// <returns></returns>
    public string Home_FriendlyLink(int cateID)
    {
        string strHtml = "";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FriendlyLinkInfo.FriendlyLink_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_CateID", "=", cateID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("FriendlyLinkInfo.FriendlyLink_Sort", "ASC"));
        IList<FriendlyLinkInfo> entitys = mylink.GetFriendlyLinks(Query, pub.CreateUserPrivilege("2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5"));
        if (entitys != null)
        {
            strHtml += "<select id='select_name' onchange='openlink()'>";
            foreach (FriendlyLinkInfo entity in entitys)
            {


                strHtml = strHtml + "<option value='" + entity.FriendlyLink_URL + "'>" + entity.FriendlyLink_Name + "</option>";
            }
            strHtml += "</select>";
        }
        return strHtml;
    }

    /// <summary>
    /// 图片友情链接
    /// </summary>
    /// <param name="cateID"></param>
    /// <returns></returns>
    public string Home_FriendlyLinkImg(int cateID)
    {
        string strHtml = "";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FriendlyLinkInfo.FriendlyLink_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_CateID", "=", cateID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("FriendlyLinkInfo.FriendlyLink_Sort", "ASC"));
        IList<FriendlyLinkInfo> entitys = mylink.GetFriendlyLinks(Query, pub.CreateUserPrivilege("2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5"));
        if (entitys != null)
        {
            foreach (FriendlyLinkInfo entity in entitys)
            {
                strHtml = strHtml + "<li><a href=\"" + entity.FriendlyLink_URL + "\" target=\"_blank\" alt=\"" + entity.FriendlyLink_Name + "\"><img src=\"" + pub.FormatImgURL(entity.FriendlyLink_Img, "fullpath") + "\" /></a></li>";
            }
        }
        return strHtml;
    }
    #endregion


    public virtual ArticleInfo GetArticleByID(int article_id)
    {
        return MyArticle.GetArticleByID(article_id, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
    }

    public string GetArticleKeyword(int article_id, string keyword)
    {
        StringBuilder content = new StringBuilder("");

        if (keyword.Length > 0)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 4;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ID", "!=", article_id.ToString()));

            int ikeyword = 0;
            foreach (string keywordsub in keyword.Split(' '))
            {
                if (keywordsub != "")
                {
                    ikeyword++;
                }
            }

            if (ikeyword == 1)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Keyword", "like", keyword));
            }
            else
            {
                int j = 0;
                foreach (string keywordsub in keyword.Split(' '))
                {

                    if (keywordsub == "")
                    {
                        continue;
                    }
                    j++;
                    if (j == 1)
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ArticleInfo.Article_Keyword", "like", keywordsub));
                    }
                    else if (j == ikeyword)
                    {
                        Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ArticleInfo.Article_Keyword", "like", keywordsub));
                    }
                    else
                    {
                        Query.ParamInfos.Add(new ParamInfo("OR", "str", "ArticleInfo.Article_Keyword", "like", keywordsub));
                    }
                }
            }
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

            if (entitys != null)
            {
                content.Append("<p><b>相关文章</b></p>");
                foreach (ArticleInfo entity in entitys)
                {
                    content.Append("<p><a  href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" style=\"width:720px; height:20px; line-height:20px;overflow:hidden;\" title=\"" + entity.Article_Title + "\">" + entity.Article_Title + "</a></p>");
                }
            }
        }


        return content.ToString();
    }

    #region 列表页

    public void Search()
    {
        StringBuilder content = new StringBuilder("");

        string keyword = pub.CheckXSS(tools.CheckStr(Request["keyword"]));
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "?list=list" + "&keyword=" + keyword;

        if (curr_page < 1)
        {
            curr_page = 1;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ArticleInfo.Article_Title", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ArticleInfo.Article_Intro", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ArticleInfo.Article_Source", "like", keyword));
            //Query.ParamInfos.Add(new ParamInfo("OR", "str", "ArticleInfo.Article_Content", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ArticleInfo.Article_Keyword", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));

        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (entitys != null)
        {
            Response.Write(" <div class='on-line-left on-line-left2' style='margin-top:20px;width:1153px'><ul class='on-line-news on-line-news2' style='width:1153px'>");

            foreach (ArticleInfo entity in entitys)
            {

                Response.Write("<li class=\"clearfix\">");
                ArticleCateInfo acateinfo = GetArticleCateByID(GetParentCate_ID(entity.Article_CateID));
                string href = "/" + entity.Article_CateID + "/" + entity.Article_ID + "";
                if (acateinfo != null)
                {
                    if (acateinfo.Article_Cate_Name == "科普在线")
                    {
                        href = "/Voice/" + entity.Article_CateID + "/" + entity.Article_ID;
                    }
                }

                Response.Write("<a href=\"" + href + "\" title=\"" + entity.Article_Title + "\">");
                int year = entity.Article_Addtime.Year;
                string month = entity.Article_Addtime.Month.ToString().PadLeft(2, '0');
                string day = entity.Article_Addtime.Day.ToString().PadLeft(2, '0');
                string catename = "";
                ArticleCateInfo acateInfo = GetArticleCateByID(entity.Article_CateID);
                if (acateInfo != null)
                {
                    catename = "<i>" + acateInfo.Article_Cate_Name + "</i>";
                }
                Response.Write("  <div class=\"date\"><h5>" + month + "/" + day + "</h5><p>" + year + "</p></div>");
                Response.Write(" <div class=\"news-text\" style='width:900px'> <h3>" + catename + entity.Article_Title + "</h3>");
                Response.Write("<p style='width:900px'>" + entity.Article_Intro + "</p>");
                Response.Write("<h6><span><img src=\"/images/icon-zz.png\">作者:" + entity.Article_Author + " </span>| <span><img src=\"/images/icon-rq.png\">发布:" + entity.Article_Addtime.ToString("yyyy/MM/dd") + "</span>|<span><img src=\"/images/icon-ck.png\">浏览:" + entity.Article_PageViews + " </span></h6>");
                Response.Write(" </div></a></li>");
            }
            Response.Write("</ul>");
            Response.Write("<div class=\"list-page\">");
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
            Response.Write("</div></div>");

        }
        else
        {
            Response.Write(" <div class='on-line-left on-line-left2' style='margin-top:20px;width:1200px'><ul class='on-line-news on-line-news2'>");

            Response.Write("<ul>");
            Response.Write("<li><p style=\"text-align:center;\">未检索到相关文章</p></li>");
            Response.Write("</ul></div>");
        }

    }

    /// <summary>
    /// 新闻阅读排行榜
    /// </summary>
    /// <returns></returns>
    public string NewsRankingList()
    {
        StringBuilder StrHTML = new StringBuilder("");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_PageViews", "Desc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            int i = 0;
            foreach (ArticleInfo entity in entitys)
            {
                StrHTML.Append("<p>");
                i++;
                switch (i)
                {
                    case 1:
                        StrHTML.Append("<i style=\"background: #c92f32\">1</i>");
                        break;
                    case 2:
                        StrHTML.Append("<i style=\"background: #fdc52a\">2</i>");
                        break;
                    case 3:
                        StrHTML.Append("<i style=\"background: #828282\">3</i>");
                        break;
                    default:
                        StrHTML.Append("<i>" + i + "</i>");
                        break;
                }

                StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                StrHTML.Append(entity.Article_Title);
                StrHTML.Append("</a>");
                StrHTML.Append("</p>");
            }
        }
        return StrHTML.ToString();
    }

    /// <summary>
    /// 新闻精华推荐
    /// </summary>
    /// <returns></returns>
    public string NewsRecommend()
    {
        StringBuilder StrHTML = new StringBuilder("");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 4;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ContentID", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (entitys != null)
        {
            foreach (ArticleInfo entity in entitys)
            {
                StrHTML.Append("<li>");
                StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                StrHTML.Append("<h3>" + entity.Article_Title + "</h3>");
                StrHTML.Append("<p>" + entity.Article_Intro + "</p>");
                StrHTML.Append("</a>");

                StrHTML.Append("</li>");
            }
        }
        return StrHTML.ToString();
    }

    public void ArticleList(int Cate_ID)
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
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(Cate_ID)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            Response.Write("<ul>");

            int i = 0;
            foreach (ArticleInfo entity in entitys)
            {
                i++;

                if (i <= 2)
                {
                    Response.Write("<li class=\"clearfix\">");
                    Response.Write("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                    Response.Write("<div class=\"news-list-pic\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\"></div>");
                    Response.Write("<div class=\"news-list-text\">");
                    Response.Write("<h3>" + entity.Article_Title + "</h3>");
                    Response.Write("<p>" + entity.Article_Intro + "</p>");
                    Response.Write("<span>" + entity.Article_Addtime.ToString("yyyy-MM-dd HH:mm") + "</span>");
                    Response.Write("</div>");
                    Response.Write("</a>");
                    Response.Write("</li>");
                }
                else
                {
                    Response.Write("<li class=\"text-list\">");
                    Response.Write("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                    Response.Write("<h3>" + entity.Article_Title + "</h3>");
                    Response.Write("<p>" + entity.Article_Intro + "</p>");
                    Response.Write("<span>" + entity.Article_Addtime.ToString("yyyy-MM-dd HH:mm") + "</span>");
                    Response.Write("</a>");
                    Response.Write("</li>");
                }


            }
            Response.Write("</ul>");
            Response.Write("<div class=\"list-page\">");
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Redirect("/");
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
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(Cate_ID)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
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
                    list1 = list1 + "<li class=\"clearfix\">";
                    list1 = list1 + "<div class=\"voice-top clearfix\">";
                    list1 = list1 + "<img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" />";
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
                    list2 = list2 + "<li class=\"clearfix\">";
                    list2 = list2 + "<div class=\"voice-top clearfix\">";
                    list2 = list2 + "<img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" />";
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
            Response.Write("<div class=\"clear\"></div>");
            Response.Write("<div class=\"list-page\">");
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Redirect("/");
        }
    }

    public void Personnel(int Cate_ID, string Name)
    {
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "";

        if (curr_page < 1)
        {
            curr_page = 1;
        }
        page_url = page_url + "?";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(Cate_ID)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            Response.Write("<h2><i></i>" + Name + "</h2>");
            Response.Write("<ul>");

            int i = 0;
            foreach (ArticleInfo entity in entitys)
            {
                i++;

                Response.Write("<li class=\"text-list\">");
                Response.Write("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\">");
                Response.Write("<h3>" + entity.Article_Title + "</h3>");
                Response.Write("</a>");
                Response.Write("</li>");


            }
            Response.Write("</ul>");
            if (pageinfo.PageCount > 1)
            {
                Response.Write("<div class=\"list-page\">");
                pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
                Response.Write("</div>");
            }
            else
            {
                Response.Write("<div class=\"list-page\">");
                Response.Write("</div>");
            }

        }
        else
        {
            Response.Redirect("/");
        }
    }
    /// <summary>
    /// 首页推荐阅读
    /// </summary>
    public void Home_Recommend(int PageSize)
    {
        StringBuilder StrHTML = new StringBuilder("");
        int curr_page = tools.CheckInt(Request["page"]);
        if (curr_page < 1)
        {
            curr_page = 1;
        }
        if (PageSize <= 0)
        {
            PageSize = 4;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsRecommend", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (entitys != null)
        {
            foreach (ArticleInfo entity in entitys)
            {
                Response.Write("<li>");
                Response.Write("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + ".aspx\" title=\"" + entity.Article_Title + "\" class=\"clearfix\">");
                Response.Write("<div class=\"center-list-pic\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "thumbnail") + "\"></div>");
                //Response.Write("<div class=\"center-list-pic\"><img class=\"lazy\" data-original=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\"></div>");
                Response.Write("<div class=\"center-list-r\">");
                Response.Write("<h3>" + entity.Article_Title + "</h3>");
                Response.Write("<p>" + entity.Article_Intro + "</p>");
                Response.Write(" <span>" + entity.Article_Addtime.ToString("yyyy-MM-dd") + "</span>");
                Response.Write("</div>");
                Response.Write("</a>");
                Response.Write("</li>");
            }
        }
        else
        {
            Response.Write("Error");
        }
        Response.Write(StrHTML.ToString());
    }
    #endregion


    #region 文章资讯
    /// <summary>
    /// 左侧导航二级文章分类菜单
    /// </summary>
    /// <param name="cate_id"></param>
    /// <returns></returns>
    public string GetArticle_CateLeft(int parentcate_id, int cate_id)
    {

        StringBuilder sHtml = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", parentcate_id.ToString()));

        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "ASC"));
        IList<ArticleCateInfo> ArticleCates = MyArticleCate.GetArticleCates(Query, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (ArticleCates != null)
        {


            sHtml.AppendLine("<ul class='ky-tit-list'>");
            int i = 1;
            foreach (ArticleCateInfo entity in ArticleCates)
            {
                string Css = "";
                if (cate_id == parentcate_id)
                {
                    //if (i == 1)
                    //{
                    //    Css = "class='active'";
                    //}
                }
                else
                {
                    if (cate_id == entity.Article_Cate_ID)
                    {
                        Css = "class='active'";
                    }
                }

                if (entity.Article_Cate_Type == 1)
                {
                    sHtml.AppendLine("  <li " + Css + "><a href=\"" + entity.Article_Cate_Href + "\" target='_blank'>" + entity.Article_Cate_Name + "</a></li>");
                }
                else
                {
                    sHtml.AppendLine("  <li " + Css + "><a href=\"/" + entity.Article_Cate_ID + "/\">" + entity.Article_Cate_Name + "</a></li>");
                }

                i++;
            }
            sHtml.Append("</ul>");
        }
        return sHtml.ToString();
    }
    public string GetArticleIDFromArticleCategory(int cate_id)
    {
        string res = "";
        IList<ArticleCategoryInfo> entitys = MyArticle.GetArticleCategorys(cate_id, null);
        if (entitys != null && entitys.Count > 0)
        {
            foreach (ArticleCategoryInfo item in entitys)
            {
                res += item.Article_Category_ArticleID + ",";
            }
        }
        return res.TrimEnd(',');

    }

    /// <summary>
    /// 获取某一分类下的文章。按照时间倒叙。
    /// </summary>
    /// <param name="cate_id"></param>
    /// <returns></returns>
    public void GetArticle_ListRight(int cate_id)
    {


        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "";

        if (curr_page < 1)
        {
            curr_page = 1;
        }
        page_url = page_url + "?";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 12;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate_id)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        string aIDs = GetArticleIDFromArticleCategory(cate_id);
        if (aIDs != "")
        {

            Query.ParamInfos.Add(new ParamInfo("or", "int", "ArticleInfo.Article_ID", "in", aIDs));  //检查指定文章

        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));


        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            if (entitys.Count == 1)
            {
                Response.Write(" <div class='workers-right'>");

                foreach (ArticleInfo entity in entitys)
                {
                    Response.Write("<div class=\"news-details-tit\" style=\"width: auto;\">");
                    Response.Write(" <h2>" + entity.Article_Title + "</h2> <div class=\"news-details-date clearfix\">");
                    Response.Write("<span>添加时间：" + entity.Article_Addtime.ToString("yyyy-MM-dd") + "  作者：" + entity.Article_Author + "  来源：" + entity.Article_Source + "  点击：" + entity.Article_PageViews + "</span>");
                    Response.Write("<em><img src=\"/images/icon-30.jpg\">收藏<img src=\"/images/icon-31.jpg\">打印 <img src=\"/images/icon-32.jpg\">字体： <i id='da'>大</i>   <i id='zhong' class=\"active\">中</i>   <i id='xiao'>小</i></em></div></div>");
                    Response.Write("<div id='div_show' class=\"text\">" + entity.Article_Content + "</div>");
                    UpdatePages(entity.Article_ID);
                    break;
                }

                Response.Write(" </div>");
            }
            else
            {
                //Response.Write(" <div class='on-line-left on-line-left2'><ul class='on-line-news on-line-news2'>");

                //foreach (ArticleInfo entity in entitys)
                //{

                //    Response.Write("<li class=\"clearfix\">");
                //    Response.Write("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\">");
                //    int year = entity.Article_Addtime.Year;
                //    string month = entity.Article_Addtime.Month.ToString().PadLeft(2, '0');
                //    string day = entity.Article_Addtime.Day.ToString().PadLeft(2, '0');
                //    string catename = "";
                //    ArticleCateInfo acateInfo = GetArticleCateByID(entity.Article_CateID);
                //    if (acateInfo != null)
                //    {
                //        catename = "<i>" + acateInfo.Article_Cate_Name + "</i>";
                //    }
                //    Response.Write("  <div class=\"date\"><h5>" + month + "/" + day + "</h5><p>" + year + "</p></div>");
                //    Response.Write(" <div class=\"news-text\"> <h3>" + catename + entity.Article_Title + "</h3>");
                //    Response.Write("<p>" + entity.Article_Intro + "</p>");
                //    Response.Write("<h6><span><img src=\"/images/icon-zz.png\">作者:" + entity.Article_Author + " </span>| <span><img src=\"/images/icon-rq.png\">发布:" + entity.Article_Addtime.ToString("yyyy/MM/dd") + "</span>|<span><img src=\"/images/icon-ck.png\">浏览:" + entity.Article_PageViews + " </span></h6>");
                //    Response.Write(" </div></a></li>");
                //}
                //Response.Write("</ul>");
                //Response.Write("<div class=\"list-page\">");
                //pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
                //Response.Write("</div></div>");

                Response.Write("<div class='on-line-left on-line-left2'>");
                //查询已置顶的文章
                IList<ArticleInfo> entitysTop = entitys.Where(a => a.Artide_IsTop == 1).OrderBy(a => a.Article_Sort).Take(7).ToList();
                if (entitysTop != null && entitysTop.Count > 0)
                {
                    Response.Write(" <div class=\"clearfix\">  ");
                    if (entitysTop.Count > 4)
                    {
                        var entitysTop4 = entitysTop.Take(4).ToList();

                        Response.Write("<div class=\"news-banner2\" > <div class=\"banner-wrap2\"><ul class=\"banner2 clearfix\">");

                        foreach (ArticleInfo entity in entitysTop4)
                        {
                            if (entity.Article_Img.Contains(".mp4") || entity.Article_Img.Contains(".flv") || entity.Article_Img.Contains(".wmv"))
                            {
                                Response.Write("  <li> <a target='_blank' href='/" + entity.Article_CateID + "/" + entity.Article_ID + "' ><video poster ='/images/detail_no_pic.gif' width='380px' height='280px' src='" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "' controls >您的浏览器不支持 video 标签。</video><p>" + entity.Article_Title + "</p></a></li>");

                            }
                            else {
                                Response.Write("  <li> <a target='_blank' href='/" + entity.Article_CateID + "/" + entity.Article_ID + "' ><img src='" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "'><p>" + entity.Article_Title + "</p></a></li>");

                            }

                           


                        }
                        Response.Write("</ul>  <div class=\"new-number\">");

                        foreach (ArticleInfo entity in entitysTop4)
                        {
                            Response.Write("  <span></span>");
                        }
                        Response.Write("</div></div></div>");

                        var entitysTop3 = entitysTop.Where(a => !entitysTop4.Select(b => b.Article_ID).ToArray().Contains(a.Article_ID)).ToList();
                        if (entitysTop3 != null)
                        {
                            Response.Write(" <ul class=\"important-news-right\">");
                            foreach (ArticleInfo entity in entitysTop3)
                            {
                                Response.Write("   <li class=\"clearfix\"> <a href='/" + entity.Article_CateID + "/" + entity.Article_ID + "'><h4>" + entity.Article_Title + "</h4><p>" + entity.Article_Intro + "</p></a></li>");
                            }
                            Response.Write("</ul>");
                        }
                    }
                    else
                    {


                        Response.Write("<div class=\"news-banner2\" > <div class=\"banner-wrap2\"><ul class=\"banner2 clearfix\">");

                        foreach (ArticleInfo entity in entitysTop)
                        {
                            if (entity.Article_Img.Contains(".mp4") || entity.Article_Img.Contains(".flv") || entity.Article_Img.Contains(".wmv"))
                            {
                                Response.Write("  <li> <a target='_blank'  href='/" + entity.Article_CateID + "/" + entity.Article_ID + "' ><video poster ='/images/detail_no_pic.gif' width='380px' height='280px' src='" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "' controls >您的浏览器不支持 video 标签。</video><p>" + entity.Article_Title + "</p></a></li>");

                            }
                            else {
                                Response.Write("  <li> <a target='_blank' href='/" + entity.Article_CateID + "/" + entity.Article_ID + "' ><img src='" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "'><p>" + entity.Article_Title + "</p></a></li>");


                            }

                          

                        }
                        Response.Write("</ul>  <div class=\"new-number\">");

                        foreach (ArticleInfo entity in entitysTop)
                        {
                            Response.Write("  <span></span>");
                        }
                        Response.Write("</div></div></div>");
                    }

                    Response.Write("</div>");
                }
                //排除已置顶的文章
                IList<ArticleInfo> notinEntitys = entitys.Where(a => !entitysTop.Select(b => b.Article_ID).ToArray().Contains(a.Article_ID)).ToList();
                Response.Write("<ul class='on-line-news'>");
                foreach (ArticleInfo entity in notinEntitys)
                {

                    Response.Write("<li class=\"clearfix\">");
                    Response.Write("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\">");

                    string catename = "";
                    ArticleCateInfo acateInfo = GetArticleCateByID(entity.Article_CateID);
                    if (acateInfo != null)
                    {
                        catename = "<i>" + acateInfo.Article_Cate_Name + "</i>";
                    }
                    if (entity.Article_Img.Contains(".mp4") || entity.Article_Img.Contains(".flv") || entity.Article_Img.Contains(".wmv"))
                    {
                        Response.Write("  <div class='pic'><video poster ='/images/detail_no_pic.gif' width='180px' height='116px' src='" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "' controls >您的浏览器不支持 video 标签。</video></div>");

                    }
                    else
                    {
                        Response.Write(" <div class='pic'><img src='" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "'></div>");
                    }

                    Response.Write(" <div class=\"news-text\"> <h3>" + catename + entity.Article_Title + "</h3>");
                    Response.Write("<p>" + entity.Article_Intro + "</p>");
                    Response.Write("<h6><span><img src=\"/images/icon-zz.png\">作者:" + entity.Article_Author + " </span>| <span><img src=\"/images/icon-rq.png\">发布:" + entity.Article_Addtime.ToString("yyyy/MM/dd") + "</span>|<span><img src=\"/images/icon-ck.png\">浏览:" + entity.Article_PageViews + " </span></h6>");
                    Response.Write(" </div></a></li>");
                }
                Response.Write("</ul>");
                Response.Write("<div class=\"list-page\">");
                pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
                Response.Write("</div></div>");
            }
        }
        else
        {
            //Response.Redirect("/");
            Response.Write("");
        }

    }

    /// <summary>
    /// 某一分类下的推荐数据
    /// </summary>
    /// <param name="cateid"></param>
    /// <returns></returns>
    public string LikeRecommend(int cateid)
    {
        StringBuilder StrHTML = new StringBuilder("");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", cateid.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsRecommend", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (entitys != null)
        {
            foreach (ArticleInfo entity in entitys)
            {
                StrHTML.Append("<li>");

                StrHTML.Append("<a href=\"/" + entity.Article_CateID + "/" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\">");
                StrHTML.Append("<i></i>" + entity.Article_Title);

                StrHTML.Append("</a>");

                StrHTML.Append("</li>");
            }
        }
        return StrHTML.ToString();
    }

    public string Kp_Recommend(int cateid)
    {
        StringBuilder StrHTML = new StringBuilder("");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", cateid.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsRecommend", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (entitys != null)
        {
            StrHTML.Append("<div class=\"text-tj\"> <h2>推荐阅读</h2><ul class=\"clearfix\">");

            foreach (ArticleInfo entity in entitys)
            {
                StrHTML.Append("<li>");

                StrHTML.Append("<a href=\"/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\">");
                StrHTML.Append("<span>" + entity.Article_Title + "<span><em>" + entity.Article_Addtime.ToString("yyyy/MM/dd") + "</em>");

                StrHTML.Append("</a>");

                StrHTML.Append("</li>");
            }
            StrHTML.Append("</ul></div>");
        }
        return StrHTML.ToString();
    }
    #endregion

    #region 科普在线列表
    /// <summary>
    ///科普广告
    /// </summary>
    /// <returns></returns>
    public string GetAd_Show()
    {
        StringBuilder sHtml = new StringBuilder();
        sHtml.AppendLine("<ul class=\"list-top-pic clearfix\">");
        sHtml.AppendLine("<li style=\"width: 833px;height: 250px;\">");
        sHtml.AppendLine(ad.AD_Show("Home_kp", "", "cycle", 0));
        sHtml.AppendLine("</li>");
        sHtml.AppendLine(ad.AD_Show("Home_kp_bottom", "", "cycle_li", 0));
        sHtml.AppendLine("</ul>");
        return sHtml.ToString();
    }

    /// <summary>
    /// 科普列表
    /// </summary>
    /// <param name="cate_id"></param>
    public void GetArticle_List_Kp(int cate_id)
    {


        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "";

        if (curr_page < 1)
        {
            curr_page = 1;
        }
        page_url = page_url + "?";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 7;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate_id)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        //Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {

            Response.Write(" <ul class='on-line-news'>");

            foreach (ArticleInfo entity in entitys)
            {

                Response.Write("<li class=\"clearfix\">");
                Response.Write("<a href=\"/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\">");
                Response.Write(" <div class=\"pic\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\"></div>");
                string catename = "";
                ArticleCateInfo acateInfo = GetArticleCateByID(entity.Article_CateID);
                if (acateInfo != null)
                {
                    catename = "<i>" + acateInfo.Article_Cate_Name + "</i>";
                }

                Response.Write(" <div class=\"news-text\"> <h3>" + catename + entity.Article_Title + "</h3>");
                Response.Write("<p>" + entity.Article_Intro + "</p>");
                Response.Write("<h6><span><img src=\"/images/icon-zz.png\">作者:" + entity.Article_Author + " </span>| <span><img src=\"/images/icon-rq.png\">发布:" + entity.Article_Addtime.ToString("yyyy/MM/dd") + "</span>|<span><img src=\"/images/icon-ck.png\">浏览:" + entity.Article_PageViews + " </span></h6>");
                Response.Write(" </div></a></li>");
            }
            Response.Write("</ul>");
            Response.Write("<div class=\"list-page\">");
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
            Response.Write("</div>");

        }
        else
        {
            //Response.Redirect("/");
            Response.Write("");
        }

    }
    /// <summary>
    /// 科普在线列表右侧分类展示
    /// </summary>
    /// <param name="parentid"></param>
    /// <param name="cate_id"></param>
    /// <returns></returns>
    public string GetArticle_CateList(int parentid, int cate_id)
    {

        StringBuilder sHtml = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", parentid.ToString()));

        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "ASC"));
        IList<ArticleCateInfo> ArticleCates = MyArticleCate.GetArticleCates(Query, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (ArticleCates != null)
        {


            sHtml.AppendLine("<div class=\"right-list-1\"><h3 class=\"right-tit\"><i></i>热门分类</h3>");
            sHtml.AppendLine(" <ul class=\"clearfix\">");
            int i = 1;
            foreach (ArticleCateInfo entity in ArticleCates)
            {


                string Css = "";
                if (cate_id == parentid)
                {
                    if (i == 1)
                    {

                        sHtml.AppendLine("  <li class='active'><a href='/Voice/" + parentid + "/'>全部分类</a></li>");
                    }

                }
                else
                {
                    if (cate_id == entity.Article_Cate_ID)
                    {
                        Css = "class='active'";
                    }
                    if (i == 1)
                    {
                        sHtml.AppendLine("  <li ><a href='/Voice/" + parentid + "/'>全部分类</a></li>");
                    }
                }


                if (entity.Article_Cate_Type == 1)
                {
                    sHtml.AppendLine("  <li " + Css + "><a href=\"" + entity.Article_Cate_Href + "\" target='_blank'>" + entity.Article_Cate_Name + "</a></li>");
                }
                else
                {
                    sHtml.AppendLine("  <li " + Css + "><a href=\"/Voice/" + entity.Article_Cate_ID + "/\">" + entity.Article_Cate_Name + "</a></li>");

                }


                i++;
            }
            sHtml.Append("<ul></div>");
        }
        return sHtml.ToString();
    }

    /// <summary>
    /// 科普在线列表右侧热点排行咨询
    /// </summary>
    /// <param name="cate_id"></param>
    /// <returns></returns>
    public string HotArticleSort(int cate_id)
    {
        StringBuilder sHtml = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate_id)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_PageViews", "DESC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            sHtml.AppendLine(" <div class=\"right-list-2\"><h3 class=\"right-tit\"><i></i>热点排行</h3><ul>");
            int i = 1;
            foreach (ArticleInfo entity in entitys)
            {

                if (i > 3)
                {
                    sHtml.AppendLine("<li><a href=\"/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "\"><em>" + i + "</em>" + entity.Article_Title + "</a></li>");
                }
                else
                {
                    sHtml.AppendLine("<li><a href=\"/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "\"><em class=\"red\">" + i + "</em>" + entity.Article_Title + "</a></li>");
                }



                i++;
            }
            sHtml.AppendLine("</ul></div>");



        }
        return sHtml.ToString();

    }

    /// <summary>
    /// 科普在线右侧图文资讯
    /// </summary>
    /// <param name="cate_id"></param>
    /// <returns></returns>
    public string ImgArticleShow(int cate_id)
    {
        StringBuilder sHtml = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 3;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate_id)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Img", "<>", ""));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            sHtml.AppendLine("  <div class=\"right-list-3\"> <h3 class=\"right-tit\"><i></i>图文资讯</h3><ul>");

            foreach (ArticleInfo entity in entitys)
            {
                sHtml.AppendLine("<li class=\"clearfix\"><a href=\"/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "\"> <img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\">");
                sHtml.AppendLine("<div class=\"text-3\"><h3>" + entity.Article_Title + "</p></div></a></li>");

            }
            sHtml.AppendLine("</ul></div>");



        }
        return sHtml.ToString();
    }
    /// <summary>
    /// 科普在线 热点推荐
    /// </summary>
    /// <param name="cate_id"></param>
    /// <returns></returns>
    public string RecommendArticleShow(int cate_id)
    {
        StringBuilder sHtml = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", Get_All_SubCate(cate_id)));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsRecommend", "=", "1"));
        //Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            sHtml.AppendLine("  <div class=\"right-list-2\"> <h3 class=\"right-tit\"><i></i>热点推荐</h3><ul>");

            foreach (ArticleInfo entity in entitys)
            {
                sHtml.AppendLine("<li><a href=\"/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "\">" + entity.Article_Title + "</a></li>");


            }
            sHtml.AppendLine("</ul>");
            sHtml.AppendLine(ad.AD_Show("Home_kp_RightBottom", "", "cycle", 0));
            sHtml.AppendLine("</div>");



        }
        return sHtml.ToString();
    }
    #endregion

    #region 我的投稿
    public string Add_Aritlce()
    {
        int Article_ID = tools.CheckInt(Request["Article_ID"]);
        int Article_CateID = tools.CheckInt(Request["article_cateid"]);
        string Article_Title = tools.CheckStr(Request["title"]);
        string Article_Source = "唐山科普在线会员原创作品";
        string Article_Author = tools.CheckStr(Request["name"]);
        string Article_Img = tools.CheckStr(Request["img_Article_Content"]);
        string Article_Keyword = tools.CheckStr(Request.Form["Article_Keyword"]);
        string Article_Intro = tools.CheckStr(Request["Article_Intro"]);
        string Article_Content = Request["Article_Content"].ToString().Replace("'", "''");
        DateTime Article_Addtime = DateTime.Now;
        int Article_Hits = 0;
        int Article_IsRecommend = tools.CheckInt(Request.Form["Article_IsRecommend"]);
        int Article_IsAudit = 0;
        int Article_Sort = tools.CheckInt(Request.Form["Article_Sort"]);
        string Article_Hyperlink = tools.CheckStr(Request.Form["Article_Hyperlink"]);
        int Article_ContentID = tools.CheckInt(Request.Form["Article_ContentID"]);
        string Article_Site = pub.GetCurrentSite();
        string Article_SEO_Title = tools.CheckStr(Request.Form["Article_SEO_Title"]);
        string Article_SEO_Keyword = tools.CheckStr(Request.Form["Article_SEO_Keyword"]);
        string Article_SEO_Description = tools.CheckStr(Request.Form["Article_SEO_Description"]);
        int Article_PageViews = 0;

        int SubjectID = tools.CheckInt(Request.Form["Subject_ID"]);

        string Artide_ShoulderTitle = tools.CheckStr(Request.Form["Artide_ShoulderTitle"]);
        int Artide_ShoulderTitleSize = tools.CheckInt(Request.Form["Artide_ShoulderTitleSize"]);
        int Article_HyperlinkSize = tools.CheckInt(Request.Form["Article_HyperlinkSize"]);
        int Artide_IsTop = tools.CheckInt(Request.Form["Artide_IsTop"]);
        int member_id = tools.CheckInt(Session["member_id"].ToString());

        if (member_id == 0) { return pub.Msg_Json("登录超时，请刷新页面重试！", ""); }
        if (Article_Title == "") { return pub.Msg_Json("请填写标题！", ""); }
        if (Article_Author == "") { return pub.Msg_Json("请填写作者！", ""); }
        if (Article_Intro == "") { return pub.Msg_Json("请填写摘要！", ""); }
        if (Article_CateID == 0) { return pub.Msg_Json("请选择原创类型！", ""); }
        if (Article_Content == "") { return pub.Msg_Json("请填写内容！", ""); }




        //Article_Content = words.FilterSensitiveWords(Article_Content);
        ArticleInfo entity = new ArticleInfo();
        entity.Article_ID = Article_ID;
        entity.Article_CateID = Article_CateID;
        entity.Article_Title = Article_Title;
        entity.Article_Source = Article_Source;
        entity.Article_Author = Article_Author;
        if (Article_CateID == 55 || Article_CateID == 54)
        {
            entity.Article_Img = Article_Img;
        }
        else
        {
            entity.Article_Img = Article_Img;
        }
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
        entity.Artide_SouceType = 1;
        entity.Artide_IsTop = Artide_IsTop;
        entity.Article_memberID = member_id;
        if (MyArticle.AddArticle(entity, pub.CreateUserPrivilege("870e6332-ab75-41cc-98c3-17e8af7827d3")))
        {
            return pub.Msg_Json("", "add_article.aspx");
        }
        else
        {
            return pub.Msg_Json("操作失败，请重试！", "");
        }

    }

    public void Del_Aritlce()
    {
        int ID = tools.CheckInt(Request["id"]);
        ArticleInfo ainfo = MyArticle.GetArticleByID(ID, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (ainfo != null)
        {
            if (ainfo.Article_IsAudit != 2)
            {
                MyArticle.DelArticle(ID, pub.CreateUserPrivilege("cc00c494-d211-438c-baef-ac20d419b066"));
                Response.Write("success");
            }
            else
            {
                Response.Write("当前状态不可撤销！");
            }
        }
        else
        {
            Response.Write("操作失败，请重试！");
        }

    }
    #endregion

    #region 科普参与-原创文章
    /// <summary>
    /// 科普参与 主页面
    /// </summary>
    /// <param name="cateid">分类ID</param>
    /// <returns></returns>
    public string GetOnline_Html(int cateid, int psize, int type)
    {
        StringBuilder sHtml = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = psize;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", cateid.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (entitys != null)
        {
            ArticleCateInfo acate = GetArticleCateByID(cateid);
            string catename = "--";
            if (acate != null)
            {
                catename = acate.Article_Cate_Name;
            }
            if (type == 0)//普通绘画/摄影
            {
                sHtml.AppendLine("  <div class='list-kp-tit'><span><i></i>" + catename + "</span> <a href='/" + cateid + "/'>更多></a> </div>");
                sHtml.Append(" <div class='kp-list-2'> <ul class='clearfix'>");
                foreach (ArticleInfo entity in entitys)
                {

                    sHtml.AppendLine("<li><a href=\"/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "\">");
                    sHtml.Append(" <div class='pic'><img src='" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "'/></div><div class='right-text'><h4>" + entity.Article_Title + "</h4>");
                    if (entity.Article_Intro != null)
                    {
                        sHtml.Append(" <p>" + entity.Article_Intro + "</p>");
                    }

                    sHtml.AppendLine("</div></a></li>");
                }
                sHtml.Append(" </ul></div>");
            }
            else
            {
                //video 视频
                sHtml.AppendLine("  <div class='list-kp-tit'><span><i></i>" + catename + "</span> <a href='/" + cateid + "/'>更多></a> </div>");
                sHtml.Append(" <div class='kp-list-2'> <ul class='clearfix'>");
                foreach (ArticleInfo entity in entitys)
                {
                 
                   
                    sHtml.AppendLine("<li><a href=\"/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "\">");
                    sHtml.Append(" <div class='pic'><video width='280px' height='160px' src='" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "' controls poster='/images/pay.jpg'>您的浏览器不支持 video 标签。</video></div>");
                    sHtml.Append("<div class='right-text'><h4>" + entity.Article_Title + "</h4>");
                    if (entity.Article_Intro != null)
                    {
                        sHtml.Append(" <p>" + entity.Article_Intro + "</p>");
                    }

                    sHtml.AppendLine("</div></a></li>");
                }
                sHtml.Append(" </ul></div>");

            }


        }
        return sHtml.ToString();

    }

    public string GetOnline_Top(int cateid, int psize)
    {
        StringBuilder sHtml = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = psize;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", cateid.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (entitys != null)
        {
            ArticleCateInfo acate = GetArticleCateByID(cateid);
            string catename = "--";
            if (acate != null)
            {
                catename = acate.Article_Cate_Name;
            }

            sHtml.AppendLine("  <div  class='list-kp-tit'><span><i></i>" + catename + "</span> <a href='/" + cateid + "/'>更多></a> </div>");
            sHtml.Append(" <div class='kp-list-1 clearfix'> ");
 
            ArticleInfo singinfo=entitys.First();
            sHtml.Append("<div class='list-1-left'> <a href='/Voice/" + singinfo.Article_CateID + "/" + singinfo.Article_ID + "'><div class='pic-big'><img src='" + pub.FormatImgURL(singinfo.Article_Img, "fullpath") + "'></div>");
            sHtml.Append(" <h3>" + singinfo.Article_Title + "</h3>");
            if (singinfo.Article_Intro != null)
            {
                sHtml.Append(" <p>" + singinfo.Article_Intro + "</p>");
            }
            sHtml.Append("</a></div>");

            sHtml.Append("<ul class='list-1-right'>");
            foreach (ArticleInfo entity in entitys.Skip(1))
            {

                sHtml.AppendLine("<li class='clearfix'><a href=\"/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "\">");
                sHtml.Append(" <div class='pic'><img width='160px' height='100px' src='" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "'></div><div class='right-text'><h4>" + entity.Article_Title + "</h4>");
                if (entity.Article_Intro != null)
                {
                    sHtml.Append(" <p>" + entity.Article_Intro + "</p>");
                }

                sHtml.AppendLine("</div></a></li>");
            }
            sHtml.Append(" </ul></div>");



        }
        return sHtml.ToString();
    }

    public string GetOnline_Right(int cateid, int psize)
    {
        StringBuilder sHtml = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = psize;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", cateid.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (entitys != null)
        {
            ArticleCateInfo acate = GetArticleCateByID(cateid);
            string catename = "--";
            if (acate != null)
            {
                catename = acate.Article_Cate_Name;
            }

            sHtml.AppendLine(" <div class='list-kp-tit'><span><i></i>" + catename + "</span> <a href='/" + cateid + "/'>更多></a> </div>");
           

            sHtml.Append("<ul>");
            foreach (ArticleInfo entity in entitys)
            {

                sHtml.AppendLine("<li><a href=\"/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "\">");
                sHtml.Append(" <h3>" + entity.Article_Title + "</h3>");
                if (entity.Article_Intro != null)
                {
                    sHtml.Append(" <p>" + entity.Article_Intro + "</p>");
                }

                sHtml.AppendLine("</a></li>");
            }
            sHtml.Append(" </ul></div>");



        }
        return sHtml.ToString();
    }

    #endregion
}