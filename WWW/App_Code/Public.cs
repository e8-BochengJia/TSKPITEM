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
using System.Text.RegularExpressions;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using System.IO;
using Newtonsoft.Json.Linq;

/// <summary>
///Product 的摘要说明
/// </summary>
public class Public_Class
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IMail mail;

    public Public_Class()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
        tools = ToolsFactory.CreateTools();
        mail = MailFactory.CreateMail();


    }
    /// <summary>
    /// 获得当前登录用户ID
    /// </summary>
    /// <returns></returns>
    public int GetMemberIDBySession()
    {
        return tools.NullInt(Session["member_id"]);
    }
    public string Msg_Json(string err, string url)
    {
        JObject jo = new JObject();
        jo.Add(new JProperty("err", err));
        jo.Add(new JProperty("url", url));
        return jo.ToString();
    }
    /// <summary>
    /// 会员是否登录
    /// </summary>
    /// <returns></returns>
    public bool CheckMemberLogin()
    {
        if (Convert.ToString(Session["member_logined"]) == "True")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 信息提示窗口
    /// </summary>
    /// <param name="msgtype">信息类型</param>
    /// <param name="msgtitle">信息头</param>
    /// <param name="msgcontent">信息内容</param>
    /// <param name="autoredirect">自动转向</param>
    /// <param name="autoredirecttime">停留时间</param>
    /// <param name="redirecturl">转向URL</param>
    public void Msg(string msgtype, string msgtitle, string msgcontent, bool autoredirect, int autoredirecttime, string redirecturl)
    {
        string msgtype_img = "";
        switch (msgtype)
        {
            case "error":
                msgtype_img = "<img src=\"/images/msg-error.gif\">";
                break;
            case "info":
                msgtype_img = "<img src=\"/images/msg-info.jpg\">";
                break;
            case "positive":
                msgtype_img = "<img src=\"/images/msg-positive.gif\">";
                break;
        }

        string Mhtml;
        Mhtml = "<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />";
        Mhtml += "<link rel=\"stylesheet\" href=\"/css/msg.css\" type=\"text/css\">";
        Mhtml += "<title>" + msgtitle + "</title>";
        if (autoredirect)
        {
            Mhtml += "<meta http-equiv=\"refresh\" content=\"" + autoredirecttime + ";URL=" + redirecturl + "\">";
        }

        Mhtml += "</head><body>";
        Mhtml += "<table width=\"100%\" height=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        Mhtml += "  <tr>";
        Mhtml += "    <td align=\"center\" valign=\"middle\"><table width=\"500\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" bgcolor=\"#FFFFFF\">";
        Mhtml += "      <tr><td height=\"30\" class=\"msg_title\">" + msgtitle + "</td></tr>";
        Mhtml += "      <tr>";
        Mhtml += "        <td align=\"left\" class=\"msg_content_border\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        Mhtml += "          <tr><td height=\"20\" align=\"right\" valign=\"middle\" colspan=\"2\"></td></tr>";
        Mhtml += "          <tr>";
        Mhtml += "            <td width=\"60\" align=\"center\" valign=\"middle\">" + msgtype_img + "</td>";
        Mhtml += "            <td align=\"left\" valign=\"middle\" class=\"msg_content\">" + msgcontent + "</td>";
        Mhtml += "          </tr>";

        if (redirecturl == "{back}")
        {
            Mhtml += "          <tr>";
            Mhtml += "            <td height=\"30\" align=\"right\" valign=\"middle\" colspan=\"2\"><input type=\"button\" name=\"ok\" id=\"btn_ok\" value=\"确定\" class=\"msg_btn\" onclick=\"javascript:history.go(-1);\"></td>";
            Mhtml += "          </tr>";
        }
        else if (redirecturl == "{close}")
        {
            Mhtml += "          <tr>";
            Mhtml += "            <td height=\"30\" align=\"right\" valign=\"middle\" colspan=\"2\"><input type=\"button\" name=\"ok\" id=\"btn_ok\" value=\"确定\" class=\"msg_btn\" onclick=\"javascript:window.opener = null; window.open('', '_self', '');window.close();\"></td>";
            Mhtml += "          </tr>";
        }
        else
        {
            Mhtml += "          <tr>";
            Mhtml += "            <td height=\"30\" align=\"right\" valign=\"middle\" colspan=\"2\"><input type=\"button\" name=\"ok\" id=\"btn_ok\" value=\"确定\" class=\"msg_btn\" onclick=\"location.href='" + redirecturl + "';\"></td>";
            Mhtml += "          </tr>";
        }
        Mhtml += "<script type=\"text/javascript\"> ";
        Mhtml += "	function document.onkeydown() { if(event.keyCode==13){document.getElementById(\"btn_ok\").click(); return false;} } ";
        Mhtml += "</script>";
        Mhtml += "        </table></td>";
        Mhtml += "        </tr>";
        Mhtml += "    </table></td>";
        Mhtml += "  </tr>";
        Mhtml += "</table>";
        Mhtml += "</body>";
        Mhtml += "</html>";

        System.Web.HttpContext.Current.Response.Write(Mhtml);
        System.Web.HttpContext.Current.Response.End();
    }

    /// <summary>
    /// 信息提示窗口
    /// </summary>
    /// <param name="msgtype">信息类型</param>
    /// <param name="msgtitle">信息头</param>
    /// <param name="msgcontent">信息内容</param>
    /// <param name="autoredirect">自动转向</param>
    /// <param name="redirecturl">转向URL</param>
    public void Msg(string msgtype, string msgtitle, string msgcontent, bool autoredirect, string redirecturl)
    {
        Msg(msgtype, msgtitle, msgcontent, autoredirect, 3, redirecturl);
    }

    /// <summary>
    /// Ajax提示
    /// </summary>
    /// <param name="msgtype"></param>
    /// <param name="msg"></param>
    public void Tip(string msgtype, string msg)
    {


        string msgtype_img = null;
        string table_class = null;
        msgtype_img = "";
        table_class = "";
        switch (msgtype)
        {
            case "error":
                msgtype_img = "<img src=\"/images/tip-error.gif\" hspace=\"5\" align=\"absmiddle\">";
                table_class = "tip_bg_error";
                break;
            case "info":
                msgtype_img = "<img src=\"/images/tip-info.gif\" hspace=\"5\" align=\"absmiddle\">";
                table_class = "tip_bg_info";
                break;
            case "positive":
                msgtype_img = "<img src=\"/images/tip-positive.gif\" hspace=\"5\" align=\"absmiddle\">";
                table_class = "tip_bg_positive";
                break;
            case "right":
                msgtype_img = "<img src=\"/images/icon_success.gif\" hspace=\"5\" align=\"absmiddle\">";
                table_class = "tip_bg_positive";
                break;
        }

        string HtmlStr = null;
        HtmlStr = "<table border=\"0\" cellspacing=\"2\" cellpadding=\"0\"><tr><td class=\"" + table_class + "\">";
        HtmlStr += msgtype_img + msg;
        HtmlStr += "</td></tr></table>";
        Response.Write(HtmlStr);
    }

    /// <summary>
    /// SEO标题
    /// </summary>
    /// <returns></returns>
    public string SEO_TITLE()
    {
        return Application["Site_Name"] + " - " + Application["Site_Title"];
    }

    /// <summary>
    /// 格式化货币
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public string FormatCurrency(double InVal)
    {
        if (InVal == null)
            return "NULL";
        try { return "￥" + Math.Round(InVal, GetPriceAccuracy(), MidpointRounding.AwayFromZero).ToString("0.00"); }
        catch { return "￥0.00"; }
    }

    /// <summary>
    /// 格式化商品名称：商品名+副标题
    /// </summary>
    /// <param name="Product_name"></param>
    /// <param name="Product_subname"></param>
    /// <returns></returns>
    public string Format_Product_Name(string Product_name, string Product_subname)
    {
        if (Product_subname != "")
        {
            return Product_name + "<span class=\"t12_red\">" + Product_subname + "</span>";
        }
        else
        {
            return Product_name;
        }
    }

    /// <summary>
    /// 转换null为空
    /// </summary>
    /// <param name="Check_Str"></param>
    /// <returns></returns>
    public string FormatNullToStr(string Check_Str)
    {
        if (Check_Str == null)
        {
            Check_Str = "";
        }
        return Check_Str;
    }


    /// <summary>
    /// 获得标准的站点标识
    /// </summary>
    /// <returns></returns>
    public string GetStandardSite()
    {
        return "CN";
    }

    /// <summary>
    /// 过滤XSS攻击字符
    /// </summary>
    /// <param name="inVal">输入字符</param>
    /// <returns></returns>
    public string CheckXSS(string inVal)
    {
        if (inVal == null || inVal.Length == 0)
        {
            return "";
        }
        else
        {
            inVal = Regex.Replace(inVal, "alert\\([^\\)]*\\)", "", RegexOptions.IgnoreCase);
            return inVal.Replace("\"", "&quot;");
        }
    }

    ///// <summary>
    ///// 获得当前站点标识
    ///// </summary>
    ///// <returns></returns>
    //public string GetCurrentSite()
    //{
    //    try
    //    {
    //        if (tools.NullStr(HttpContext.Current.Session["CurrentSite"]).Length != 0)
    //        {
    //            return tools.NullStr(HttpContext.Current.Session["CurrentSite"]);
    //        }

    //        else
    //        {
    //            return "CN";
    //        }
    //    }
    //    catch
    //    {
    //        return "CN";
    //    }
    //}

    ///// <summary>
    ///// 读取当前配置返回配置实体
    ///// </summary>
    ///// <param name="Site">站点标识</param>
    ///// <returns></returns>
    //public Glaer.Trade.B2C.Model.ConfigInfo GetConfigInfoBySite()
    //{
    //    Glaer.Trade.B2C.Model.ConfigInfo entity = null;
    //    try
    //    {
    //        entity = (Glaer.Trade.B2C.Model.ConfigInfo)System.Web.HttpContext.Current.Application["sysconfig" + GetCurrentSite()];

    //    }
    //    catch (Exception ex) { throw ex; }
    //    if (entity == null)
    //    {
    //        entity = new Glaer.Trade.B2C.Model.ConfigInfo();
    //    }
    //    return entity;
    //}

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="pagecount"></param>
    /// <param name="currentpage"></param>
    /// <param name="pageurl"></param>
    /// <param name="pagesize"></param>
    /// <param name="recordcount"></param>
    public void Page(int pagecount, int currentpage, string pageurl, int pagesize, int recordcount)
    {
        int ipage = 0;

        if (currentpage <= 1)
        {
            Response.Write("<a href=\"javascript:void(0);\">上一页</a>");
        }
        else
        {
            Response.Write("<a href='" + pageurl + "&page=" + (currentpage - 1).ToString() + "'>上一页</a>");
        }
        if (pagecount <= 12)
        {
            for (ipage = 1; ipage <= pagecount; ipage++)
            {
                if (currentpage == ipage)
                {
                    Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                }
                else
                {
                    Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                }
            }
        }
        else if (pagecount > 12 & pagecount < 16)
        {
            if (currentpage < 9)
            {
                for (ipage = 1; ipage <= 10; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                Response.Write(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
            else
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                Response.Write(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
        }
        else if (pagecount >= 16)
        {
            if (currentpage < 9)
            {
                for (ipage = 1; ipage <= 10; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                Response.Write(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
            else if (currentpage + 7 > pagecount)
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                Response.Write(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
            else
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                Response.Write(" ... ");
                //Response.Write("</td>");
                for (ipage = currentpage - 5; ipage <= currentpage + 4; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                Response.Write(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Response.Write("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        Response.Write("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
        }
        //Response.Write("<td class=");
        if (currentpage == pagecount)
        {
            Response.Write("<a href=\"javascript:void(0);\">下一页</a>");
        }
        else
        {
            Response.Write("<a href='" + pageurl + "&page=" + (currentpage + 1).ToString() + "'>下一页</a>");
        }

        Response.Write(" 共<span>" + pagecount + "</span>页 ");
        Response.Write("到第 <input class='pagesinput' type=\"text\"  onkeyup=\"if (Math.round(value)!=value) execCommand('undo')\" onafterpaste=\"if (Math.round(value)!=value) execCommand('undo')\" onblur=\"checkInt(this.value," + pagecount + ");\" style=\"width:40px; text-align:center;\" value=\"" + currentpage + "\" id=\"listpagenum\"> 页 <input type=\"button\" class=\"pagesbutton\" value=\"确定\" onclick=\"location='" + pageurl + "&page='+$('#listpagenum').val()\">");
        //Response.Write("</div>");
        //Response.Write("到第<select id=\"listpagenum\">");
        //for (int i = 1; i <= pagecount; i++)
        //{
        //    if (currentpage == i)
        //    {
        //        Response.Write("<option value=\"" + i + "\" selected=\"selected\">" + i + "</option>");
        //    }
        //    else
        //    {
        //        Response.Write("<option value=\"" + i + "\">" + i + "</option>");
        //    }
        //}
        //Response.Write("</select>");
        //Response.Write("页<input type=\"button\" value=\"确 定\" onclick=\"location='" + pageurl + "&page='+$('#listpagenum').val()\">");
    }

    public string PageStr(int pagecount, int currentpage, string pageurl, int pagesize, int recordcount,string type)
    {
        StringBuilder sHtml = new StringBuilder();
        int ipage = 0;

        if (currentpage <= 1)
        {
            sHtml.Append("<a href=\"javascript:void(0);\">上一页</a>");
        }
        else
        {
            sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + (currentpage - 1).ToString() + "))'>上一页</a>");
        }
        if (pagecount <= 12)
        {
            for (ipage = 1; ipage <= pagecount; ipage++)
            {
                if (currentpage == ipage)
                {
                    sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                }
                else
                {
                    sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                }
            }
        }
        else if (pagecount > 12 & pagecount < 16)
        {
            if (currentpage < 9)
            {
                for (ipage = 1; ipage <= 10; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                sHtml.Append(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
            }
            else
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                sHtml.Append(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
            }
        }
        else if (pagecount >= 16)
        {
            if (currentpage < 9)
            {
                for (ipage = 1; ipage <= 10; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                Response.Write(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
            }
            else if (currentpage + 7 > pagecount)
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                Response.Write(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
            }
            else
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                sHtml.Append(" ... ");
                //Response.Write("</td>");
                for (ipage = currentpage - 5; ipage <= currentpage + 4; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                sHtml.Append(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + ipage + "))'>" + ipage + "</a>");
                    }
                }
            }
        }
        //Response.Write("<td class=");
        if (currentpage == pagecount)
        {
            sHtml.Append("<a href=\"javascript:void(0);\">下一页</a>");
        }
        else
        {
            sHtml.Append("<a href='javascript:void(ChangeShow(" + type + "," + (currentpage + 1) + "))'>下一页</a>");
        }

        sHtml.Append(" 共<span>" + pagecount + "</span>页 ");
        sHtml.Append("到第 <input class='pagesinput' type=\"text\"  onkeyup=\"if (Math.round(value)!=value) execCommand('undo')\" onafterpaste=\"if (Math.round(value)!=value) execCommand('undo')\" onblur=\"checkInt(this.value," + pagecount + ");\" style=\"width:40px; text-align:center;\" value=\"" + currentpage + "\" id=\"listpagenum\"> 页 <input type=\"button\" class=\"pagesbutton\" value=\"确定\" onclick=\"javascript:void(ChangeShow(" + type + ",$('#listpagenum').val()))\">");
        return sHtml.ToString();
    }


    public string PageStr1(int pagecount, int currentpage, string pageurl, int pagesize, int recordcount)
    {
        StringBuilder sHtml = new StringBuilder();
        int ipage = 0;

        if (currentpage <= 1)
        {
            sHtml.Append("<a href=\"javascript:void(0);\">上一页</a>");
        }
        else
        {
            sHtml.Append("<a href='" + pageurl + "&page=" + (currentpage - 1).ToString() + "'>上一页</a>");
        }
        if (pagecount <= 12)
        {
            for (ipage = 1; ipage <= pagecount; ipage++)
            {
                if (currentpage == ipage)
                {
                    sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                }
                else
                {
                    sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                }
            }
        }
        else if (pagecount > 12 & pagecount < 16)
        {
            if (currentpage < 9)
            {
                for (ipage = 1; ipage <= 10; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                sHtml.Append(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
            else
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                sHtml.Append(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
        }
        else if (pagecount >= 16)
        {
            if (currentpage < 9)
            {
                for (ipage = 1; ipage <= 10; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                Response.Write(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
            else if (currentpage + 7 > pagecount)
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                Response.Write(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
            else
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                sHtml.Append(" ... ");
                //Response.Write("</td>");
                for (ipage = currentpage - 5; ipage <= currentpage + 4; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
                //Response.Write("<td class=page_omit>");
                sHtml.Append(" ... ");
                //Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        sHtml.Append("<a href='javascript:void(0);' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        sHtml.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
        }
        //Response.Write("<td class=");
        if (currentpage == pagecount)
        {
            sHtml.Append("<a href=\"javascript:void(0);\">下一页</a>");
        }
        else
        {
            sHtml.Append("<a href='" + pageurl + "&page=" + (currentpage + 1).ToString() + "'>下一页</a>");
        }

        sHtml.Append(" 共<span>" + pagecount + "</span>页 ");
        sHtml.Append("到第 <input class='pagesinput' type=\"text\"  onkeyup=\"if (Math.round(value)!=value) execCommand('undo')\" onafterpaste=\"if (Math.round(value)!=value) execCommand('undo')\" onblur=\"checkInt(this.value," + pagecount + ");\" style=\"width:40px; text-align:center;\" value=\"" + currentpage + "\" id=\"listpagenum\"> 页 <input type=\"button\" class=\"pagesbutton\" value=\"确定\" onclick=\"location='" + pageurl + "&page='+$('#listpagenum').val()\">");
        return sHtml.ToString();
    }
    /// <summary>
    /// 转换图片地址
    /// </summary>
    /// <param name="imgpath"></param>
    /// <param name="returntype"></param>
    /// <returns></returns>
    public string FormatImgURL(string imgpath, string returntype)
    {
        string tmpimg = "";
        string tmpimg1;
        imgpath = FormatNullToStr(imgpath);

        if (imgpath == "/images/detail_no_pic.gif" || imgpath == "")
        {
            return "/images/detail_no_pic.gif";
        }

        switch (returntype)
        {
            case "original":
                if (imgpath != "/images/detail_no_pic.gif")
                {
                    tmpimg = "";
                }
                else
                {
                    tmpimg = Application["site_url"] + imgpath;
                }
                break;
            case "fullpath":
                if (imgpath != "/images/detail_no_pic.gif")
                {
                    tmpimg = Convert.ToString(Application["upload_server_url"]);
                    tmpimg = tmpimg.TrimEnd('/') + imgpath;
                }
                else
                {
                    tmpimg = "/images/detail_no_pic.gif";
                }
                break;
            case "thumbnail":
                if (imgpath != "/images/detail_no_pic.gif")
                {
                    tmpimg1 = imgpath;
                    foreach (string tmp in imgpath.Split('/'))
                    {
                        tmpimg1 = tmp;
                    }
                    tmpimg1 = imgpath.Replace(tmpimg1, "s_" + tmpimg1);
                    tmpimg = Convert.ToString(Application["upload_server_url"]);
                    tmpimg = tmpimg.TrimEnd('/') + tmpimg1;
                }
                else
                {
                    tmpimg = "/images/detail_no_pic.gif";
                }
                break;

            case "thumbnail2":
                if (imgpath != "/images/detail_no_pic.gif")
                {
                    tmpimg1 = imgpath;
                    foreach (string tmp in imgpath.Split('/'))
                    {
                        tmpimg1 = tmp;
                    }
                    tmpimg1 = imgpath.Replace(tmpimg1, "d_" + tmpimg1);
                    tmpimg = Convert.ToString(Application["upload_server_url"]);
                    tmpimg = tmpimg.TrimEnd('/') + tmpimg1;
                }
                else
                {
                    tmpimg = "/images/detail_no_pic.gif";
                }
                break;

        }
        return tmpimg;
    }

    public string FormatImgURL2(string urlPath, string urlType)
    {
        if (urlPath.Length == 0 || urlPath == "/images/detail_no_pic.gif") { return "/images/detail_no_pic.gif"; }

        string fileCompletePath = "";
        try
        {
            string fileServerURL = System.Web.HttpContext.Current.Application["Upload_Server_URL"].ToString();
            string filePath = urlPath.Substring(0, urlPath.LastIndexOf('/') + 1);
            string fileName = urlPath.Substring(urlPath.LastIndexOf('/') + 1);

            if (fileServerURL.Substring(fileServerURL.Length - 1) == "/") { fileServerURL = fileServerURL.Substring(0, fileServerURL.Length - 1); }

            switch (urlType)
            {
                case "original":
                    fileCompletePath = urlPath;
                    break;
                case "fullpath":
                    fileCompletePath = fileServerURL + urlPath;
                    break;
                case "thumbnail":
                    fileCompletePath = fileServerURL + filePath + "s_" + fileName;
                    break;
            }
        }
        catch (Exception ex)
        {
            fileCompletePath = urlPath;
        }
        return fileCompletePath;
    }




    /// <summary>
    /// 生成随机码
    /// </summary>
    /// <returns></returns>
    public string Createvkey()
    {
        string strSource = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string[] strArray = strSource.Split(',');

        string strKey = "";
        Random ran = new Random(Guid.NewGuid().GetHashCode());
        for (int i = 0; i < 64; i++) { strKey += strArray[ran.Next(62)]; }
        ran = null;

        return strKey;
    }

    public string CreatevkeyN(int Length)
    {
        string strSource = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string[] strArray = strSource.Split(',');

        string strKey = "";
        Random ran = new Random(Guid.NewGuid().GetHashCode());
        for (int i = 0; i < Length; i++) { strKey += strArray[ran.Next(62)]; }
        ran = null;

        return strKey;
    }

    public string CreatevkeyL(int Length)
    {
        string strSource = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string[] strArray = strSource.Split(',');

        string strKey = "";
        Random ran = new Random(Guid.NewGuid().GetHashCode());
        for (int i = 0; i < Length; i++) { strKey += strArray[ran.Next(36)]; }
        ran = null;

        return strKey;
    }
    public string Createvkey(int length)
    {
        string strSource = "0,1,2,3,4,5,6,7,8,9";
        string[] strArray = strSource.Split(',');

        string strKey = "";
        Random ran = new Random(Guid.NewGuid().GetHashCode());
        for (int i = 0; i < length; i++) { strKey += strArray[ran.Next(9)]; }
        ran = null;

        return strKey;
    }

    public string CheckRadio(string input_value, string default_value)
    {
        if (input_value == default_value)
        {
            return " checked";
        }
        else
        {
            return "";
        }
    }

    public string CheckBox(string input_value, string default_value)
    {
        foreach (string str in default_value.Split(','))
        {
            if (input_value == str)
            {
                return "checked";
            }
        }

        return "";
    }
    public string CheckSelect(string input_value, string default_value)
    {
        if (input_value == default_value)
        {
            return "  selected=\"selected\" ";
        }
        else
        {
            return "";
        }
    }

    public string InIMGCode(string imgPath)
    {
        return "<img src=\"" + imgPath + "\" align=\"absmiddle\" hspace=\"5\" />";
    }

    //检查手机号
    public bool CheckMobile(string strMobile)
    {
        if (strMobile.Length != 11)
        {
            return false;
        }
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("1[0-9]{10}");
        return regex.IsMatch(strMobile);
    }

    //检查手机号或固定电话
    public bool Checkmobile(string check_str)
    {
        bool result = true;
        if (check_str.Length < 11)
        {
            result = false;
        }
        if (result)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$)|(^((\(\d{3}\))|(\d{3}\-))?(1[023456789]\d{9})$)");
            result = regex.IsMatch(check_str);
        }
        return result;
    }




    /// <summary>
    /// 价格精度
    /// </summary>
    /// <returns></returns>
    public int GetPriceAccuracy()
    {
        return Convert.ToInt32(ConfigurationManager.AppSettings["price_accuracy"]);
    }

    /// <summary>
    /// 获得兑换比例（1RMB兑换多少积分）
    /// </summary>
    /// <returns></returns>
    public int GetExchangeRatio()
    {
        return Convert.ToInt32(ConfigurationManager.AppSettings["exchange_ratio"]);
    }







    #region "邮件处理"

    //邮件发送处理过程
    public int Sendmail(string mailto, string mailsubject, string mailbodytitle, string mailbody)
    {

        //-------------------------------------定义邮件设置---------------------------------
        int mformat = 0;

        //-------------------------------------定义邮件模版---------------------------------
        string MailBody_Temp = null;
        MailBody_Temp = "";
        MailBody_Temp = MailBody_Temp + "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=GB2312\" /></head>";
        MailBody_Temp = MailBody_Temp + "<body>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailHeader><SPAN class=MailBody_title>{MailBody_title}</SPAN></DIV>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailContent>";
        MailBody_Temp = MailBody_Temp + "{MailBody_content}";
        MailBody_Temp = MailBody_Temp + "<p><br><B>{sys_config_site_name}</B><br>欲了解更多信息，请访问<a href='{sys_config_site_url}'>{sys_config_site_url}</a> 或致电{sys_config_site_tel}</P></DIV>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailFooter><P class=comments>&copy; {sys_config_site_name}</P></DIV>";
        MailBody_Temp = MailBody_Temp + "<style type=text/css>";
        MailBody_Temp = MailBody_Temp + "P {FONT-SIZE: 14px; MARGIN: 10px 0px 5px; LINE-HEIGHT: 130%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + "td {FONT-SIZE: 12px; LINE-HEIGHT: 150%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + "BODY {BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: 0px; PADDING-TOP: 0px; BORDER-BOTTOM: 0px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif }";
        MailBody_Temp = MailBody_Temp + "UL {MARGIN-TOP: 0px; FONT-SIZE: 14px; LINE-HEIGHT: 130%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + ".comments {FONT-SIZE: 12px; MARGIN: 0px; COLOR: gray; LINE-HEIGHT: 130%}";
        MailBody_Temp = MailBody_Temp + ".mailHeader {PADDING-RIGHT: 23px; PADDING-LEFT: 23px; PADDING-BOTTOM: 10px; COLOR: #003366; PADDING-TOP: 10px; BORDER-BOTTOM: #7a8995 1px solid; BACKGROUND-COLOR: #ebebeb}";
        MailBody_Temp = MailBody_Temp + ".mailContent {PADDING-RIGHT: 23px; PADDING-LEFT: 23px; PADDING-BOTTOM: 23px; PADDING-TOP: 11px}";
        MailBody_Temp = MailBody_Temp + ".mailFooter {PADDING-RIGHT: 23px; BORDER-TOP: #bbbbbb 1px solid; PADDING-LEFT: 23px; PADDING-BOTTOM: 11px; PADDING-TOP: 11px}";
        MailBody_Temp = MailBody_Temp + ".MailBody_title {  font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 20px; font-weight: bold; color: #E7168E}";
        MailBody_Temp = MailBody_Temp + "A:visited { COLOR: #105bac} A:hover { COLOR: orange} .img_border { border: 1px solid #E5E5E5}";
        MailBody_Temp = MailBody_Temp + ".highLight { BACKGROUND-COLOR: #FFFFCC; PADDING: 15px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif}</style>";
        MailBody_Temp = MailBody_Temp + "</body><html>";

        //------------------------------------开始发送过程------------------------------------
        string body = "";
        switch (mformat)
        {
            case 0:
                //HTML格式
                body = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=GB2312\" />" + MailBody_Temp;
                body = body.Replace("{MailBody_title}", mailbodytitle);
                body = body.Replace("{MailBody_content}", mailbody);
                break;
            case 1:
                //纯文本格式
                body = mailbody;
                break;
        }

        body = replace_sys_config(body);

        // ERROR: Not supported in C#: OnErrorStatement
        try
        {
            mail.From = Application["Mail_From"].ToString();
            mail.Replyto = Application["Mail_Replyto"].ToString();
            mail.FromName = Application["Mail_FromName"].ToString();
            mail.Server = Application["Mail_Server"].ToString();
            //邮件格式 0=支持HTML,1=纯文本
            mail.ServerUsername = Application["Mail_ServerUserName"].ToString(); ;
            mail.ServerPassword = Application["Mail_ServerPassWord"].ToString();
            mail.ServerPort = tools.CheckInt(Application["Mail_ServerPort"].ToString());
            if (tools.CheckInt(Application["Mail_EnableSsl"].ToString()) == 0)
            {
                mail.EnableSsl = false;
            }
            else
            {
                mail.EnableSsl = true;
            }
            mail.Encode = Application["Mail_Encode"].ToString();

            if (mail.SendEmail(mailto, mailsubject, body))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        catch (Exception ex)
        {
            return 0;
        }



    }

    //替换系统变量
    public string replace_sys_config(string replacestr)
    {
        string functionReturnValue;
        functionReturnValue = replacestr;
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_name}", Application["site_name"].ToString());
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_url}", Convert.ToString(Application["site_url"]).TrimEnd('/'));
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_tel}", Application["site_tel"].ToString());
        return functionReturnValue;
    }

    #endregion

    /// <summary>
    /// 创建一个用户权限实例
    /// </summary>
    /// <param name="PrivilegeCode">权限代码</param>
    /// <returns></returns>
    public RBACUserInfo CreateUserPrivilege(string PrivilegeCode)
    {
        RBACUserInfo UserInfo = new RBACUserInfo();

        UserInfo.RBACRoleInfos = new List<RBACRoleInfo>();
        UserInfo.RBACRoleInfos.Add(new RBACRoleInfo());

        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos = new List<RBACPrivilegeInfo>();
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos.Add(new RBACPrivilegeInfo());

        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos[0].RBAC_Privilege_ID = PrivilegeCode;

        return UserInfo;
    }



    /// <summary>
    /// 验证IP地址格式
    /// </summary>
    /// <param name="IP"></param>
    /// <returns></returns>
    public bool CheckIP(string IP)
    {
        string num = @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))";
        return System.Text.RegularExpressions.Regex.IsMatch(IP, num);
    }


    /// <summary>
    /// 获得当前站点标识
    /// </summary>
    /// <returns></returns>
    public string GetCurrentSite()
    {
        try
        {
            if (tools.NullStr(HttpContext.Current.Session["CurrentSite"]).Length != 0)
            {
                return tools.NullStr(HttpContext.Current.Session["CurrentSite"]);
            }

            else
            {
                return "CN";
            }
        }
        catch
        {
            return "CN";
        }
    }



    #region 获取ip地址

    /// <summary>
    /// 获取ip地址
    /// </summary>
    /// <returns></returns>
    public string IPAddress()
    {


        try
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理
                if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。
                        result = result.Replace(" ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (IsIPAddress(temparyip[i])
                                && temparyip[i].Substring(0, 3) != "10."
                                && temparyip[i].Substring(0, 7) != "192.168"
                                && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i];    //找到不是内网的地址
                            }
                        }
                    }
                    else if (IsIPAddress(result)) //代理即是IP格式 ,IsIPAddress判断是否是IP的方法,
                        return result;
                    else
                        result = null;    //代理中的内容 非IP，取IP
                }

            }

            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (result == null || result == String.Empty)
                result = HttpContext.Current.Request.UserHostAddress;

            return result;
        }
        catch
        {
            return tools.NullStr(Request.ServerVariables["Remote_Addr"]);
        }

    }

    /// <summary>  
    /// 判断输入的ip地址是否正确，返回TRUE or FALSE  
    /// </summary>  
    /// <param name="strJudgeString">等待判断的字符串</param>  
    /// <returns>TRUE OR FALSE</returns>  
    public bool IsIPAddress(string strJudgeString)
    {
        bool blnTest = false;
        bool _Result = true;

        Regex regex = new Regex("^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$");
        blnTest = regex.IsMatch(strJudgeString);
        if (blnTest == true)
        {
            string[] strTemp = strJudgeString.Split(new char[] { '.' }); // textBox1.Text.Split(new char[] { ‘.’ });  
            int nDotCount = strTemp.Length - 1; //字符串中.的数量，若.的数量小于3，则是非法的ip地址  
            if (3 == nDotCount)//判断字符串中.的数量  
            {
                for (int i = 0; i < strTemp.Length; i++)
                {
                    if (Convert.ToInt32(strTemp[i]) > 255)
                    { //大于255则提示，不符合IP格式  

                        _Result = false;
                        //txtbox_ServerIP.Text = "";  
                    }
                }
            }
            else
            {

                _Result = false;
            }
        }
        else
        {
            //输入非数字则提示，不符合IP格式  

            _Result = false;
            // txtbox_ServerIP.Text = "";  
        }
        return _Result;
    }
    #endregion

    public string GetFileMD5(string FileName)
    {
        try
        {
            FileStream file = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch
        {
            return "";
        }
    }


    public void Index_Banner()
    {
        Response.Write("<div class=\"banner02\">");
        Response.Write("<img src=\"/images/div_bg2.jpg\">");
        Response.Write("<ul>");
        //Response.Write("<li>技术先进<span>Advanced Technology</span></li>");
        //Response.Write("<li>检测专业<span>Professional Testing</span></li>");
        //Response.Write("<li>评价权威<span>Authoritative Assessment</span></li>");
        Response.Write("</ul>");
        Response.Write("<div class=\"clear\"></div>");
        Response.Write("</div>");
    }
    /// <summary>    
    /// 验证身份证号码    
    /// </summary>    
    /// <param name="Id">身份证号码</param>    
    /// <returns>验证成功为True，否则为False</returns>    
    public bool CheckIDCard(string Id)
    {
        if (Id.Length == 18)
        {
            bool check = CheckIDCard18(Id);
            return check;
        }
        else if (Id.Length == 15)
        {
            bool check = CheckIDCard15(Id);
            return check;
        }
        else
        {
            return false;
        }
    }

    /// <summary> 
    /// 验证18位身份证号 
    /// </summary> 
    /// <param name="Id">身份证号</param> 
    /// <returns>验证成功为True，否则为False</returns> 
    private static bool CheckIDCard18(string Id)
    {
        long n = 0;
        if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
        {
            return false;//数字验证 
        }
        string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (address.IndexOf(Id.Remove(2)) == -1)
        {
            return false;//省份验证 
        }
        string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
        DateTime time = new DateTime();
        if (DateTime.TryParse(birth, out time) == false)
        {
            return false;//生日验证 
        }
        string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
        string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
        char[] Ai = Id.Remove(17).ToCharArray();
        int sum = 0;
        for (int i = 0; i < 17; i++)
        {
            sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
        }
        int y = -1;
        Math.DivRem(sum, 11, out y);
        if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
        {
            return false;//校验码验证 
        }
        return true;//符合GB11643-1999标准 
    }

    /// <summary> 
    /// 验证15位身份证号 
    /// </summary> 
    /// <param name="Id">身份证号</param> 
    /// <returns>验证成功为True，否则为False</returns> 
    private static bool CheckIDCard15(string Id)
    {
        long n = 0;
        if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
        {
            return false;//数字验证 
        }
        string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (address.IndexOf(Id.Remove(2)) == -1)
        {
            return false;//省份验证 
        }
        string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
        DateTime time = new DateTime();
        if (DateTime.TryParse(birth, out time) == false)
        {
            return false;//生日验证 
        }
        return true;//符合15位身份证标准 
    }

}
