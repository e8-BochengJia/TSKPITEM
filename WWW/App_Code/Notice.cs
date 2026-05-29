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
public class Notice
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private INotice MyBLL;
    private INoticeCate MyCateBLL;
    private Public_Class pub = new Public_Class();
    private IArticleCate MyArticleCate;
   
    public Notice()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = NoticeFactory.CreateNotice();
        MyCateBLL = NoticeFactory.CreateNoticeCate();
        
        MyArticleCate = ArticleFactory.CreateArticleCate();
    }

    public  NoticeCateInfo GetNoticeCateByID(int cate_id)
    {
        return MyCateBLL.GetNoticeCateByID(cate_id, pub.CreateUserPrivilege("fb3e87ba-3d4d-480d-934e-80048bcc0100"));
    }
    public  NoticeInfo GetNoticeByID(int cate_id)
    {
        return MyBLL.GetNoticeByID(cate_id, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
    }
    /// <summary>
    /// 公告分类导航
    /// </summary>
    /// <param name="Cate_ID"></param>
    /// <param name="gap_char"></param>
    /// <returns></returns>
    public string GetNotice_Cate_Nav(int Cate_ID, string gap_char)
    {

     
        //string cate_nav = "";
        //ArticleCateInfo category = MyArticleCate.GetArticleCateByID(Cate_ID, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        //if (category != null)
        //{

        //    cate_nav = gap_char+ "<a href=\"/notice/" + category.Article_Cate_ID + "/ \">" + category.Article_Cate_Name + "</a>";

            
        //}
        //return cate_nav;

        string cate_nav = "";
        NoticeCateInfo category = GetNoticeCateByID(Cate_ID);
        if (category != null)
        {

            cate_nav = gap_char + cate_nav + "<a href=\"/notice/" + category.Notice_Cate_ID + "/ \">" + category.Notice_Cate_Name + "</a>";


        }
        return cate_nav;
    }
    /// <summary>
    /// 左侧导航公告
    /// </summary>
    /// <param name="cate_id"></param>
    /// <returns></returns>
    public string GetNotice_CateLeft(int cate_id)
    {

        StringBuilder sHtml = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeCateInfo.Notice_Cate_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeCateInfo.Notice_Cate_Site", "=", "CN"));

        Query.OrderInfos.Add(new OrderInfo("NoticeCateInfo.Notice_Cate_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("NoticeCateInfo.Notice_Cate_ID", "ASC"));
        IList<NoticeCateInfo> Cates = MyCateBLL.GetNoticeCates(Query, pub.CreateUserPrivilege("fb3e87ba-3d4d-480d-934e-80048bcc0100"));
        if (Cates != null)
        {

            sHtml.AppendLine("<ul class='ky-tit-list'>");
            int i = 1;
            foreach (NoticeCateInfo entity in Cates)
            {
                string Css = "";

                if (cate_id == entity.Notice_Cate_ID)
                {
                    Css = "class='active'";
                }



                sHtml.AppendLine("  <li " + Css + "><a href=\"/notice/" + entity.Notice_Cate_ID + "/\">" + entity.Notice_Cate_Name + "</a></li>");

                i++;
            }
            sHtml.Append("</ul>");
        }
        return sHtml.ToString();
    }

    /// <summary>
    /// 获取某一分类下的公告。按照时间倒叙。
    /// </summary>
    /// <param name="cate_id"></param>
    /// <returns></returns>
    public void GetNotice_ListRight(int cate_id)
    {


        int curr_page = tools.CheckInt(Request["page"]);
           int CurrentPotion = tools.NullInt(Session["CurrentPotion"]);
        string page_url = "";

        if (curr_page < 1)
        {
            curr_page = 1;
        }
        page_url = page_url + "?";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = curr_page;
        if (cate_id != CurrentPotion)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_Cate", "=", cate_id.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Today + "',{NoticeInfo.Notice_ShowTime})", ">=", "0"));
        Query.OrderInfos.Add(new OrderInfo("NoticeInfo.Notice_ID", "DESC"));
        IList<NoticeInfo> entitys = MyBLL.GetNotices(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
        if (entitys != null)
        {
            if (entitys.Count == 1)
            {
                Response.Write(" <div class='workers-right'>");

                foreach (NoticeInfo entity in entitys)
                {
                    Response.Write("<div class=\"news-details-tit\" style=\"width: auto;\">");
                    Response.Write(" <h2>" + entity.Notice_Title + "</h2> <div class=\"news-details-date clearfix\">");
                    Response.Write("<span>发布时间：" + entity.Notice_Addtime.ToString("yyyy-MM-dd")+"</span>");
                    Response.Write("<em><img src=\"/images/icon-30.jpg\">收藏<img src=\"/images/icon-31.jpg\">打印 <img src=\"/images/icon-32.jpg\">字体： <i id='da'>大</i>   <i id='zhong' class=\"active\">中</i>   <i id='xiao'>小</i></em></div></div>");
                    Response.Write("<div id='div_show' class=\"text\">" + entity.Notice_Content + "</div>");
                
                    break;
                }

                Response.Write(" </div>");
            }
            else
            {
                Response.Write(" <div class='on-line-left on-line-left2'><ul class='on-line-news on-line-news2'>");

                foreach (NoticeInfo entity in entitys)
                {

                    Response.Write("<li class=\"clearfix\">");
                    Response.Write("<a href=\"/notice/" + entity.Notice_Cate + "/" + entity.Notice_ID + "\" title=\"" + entity.Notice_Title + "\">");
                    //int year = entity.Article_Addtime.Year;
                    //string month = entity.Article_Addtime.Month.ToString().PadLeft(2, '0');
                    //string day = entity.Article_Addtime.Day.ToString().PadLeft(2, '0');
                    string catename = "";
                    NoticeCateInfo acateInfo = GetNoticeCateByID(entity.Notice_Cate);
                    if (acateInfo != null)
                    {
                        catename = "<i>" + acateInfo.Notice_Cate_Name + "</i>";
                    }
                    //Response.Write("  <div class=\"date\"><h5>" + month + "/" + day + "</h5><p>" + year + "</p></div>");
                    Response.Write(" <div class=\"news-text\"> <h3>" + catename + entity.Notice_Title + "</h3>");
                    //Response.Write("<p>" + entity.Article_Intro + "</p>");
                    Response.Write("<h6><span><img src=\"/images/icon-rq.png\">发布:" + entity.Notice_Addtime.ToString("yyyy/MM/dd") + "</span></h6>");
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

}

