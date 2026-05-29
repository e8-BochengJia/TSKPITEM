<%@ Page Language="C#" %>
<%@ Register Src="~/Public/topcate.ascx" TagName="topcate" TagPrefix="uc1" %>
<%@ Register Src="~/Public/bottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();

    Session["CurrentNav"] = "-1";

    string about_title = "关于我们";
    string sign = "";
    sign = tools.CheckStr(Request["sign"]);
    int About_IsTop = 1;
    string SEO_Keywords = tools.NullStr(Application["Site_Keyword"]);
    string SEO_Description = tools.NullStr(Application["Site_Description"]);
    string SEO_Title = pub.SEO_TITLE();
    if (sign == "")
    {
        sign = "aboutus";
        about_title = "关于我们";
    }
    AboutInfo aboutinfo = cms.GetAboutBySign(sign);
    if (aboutinfo != null)
    {
        if(aboutinfo.About_IsActive==0)
        {
            Response.Redirect("/404.aspx");
            about_title = "关于我们";
        }
        about_title = aboutinfo.About_Title;
        About_IsTop = aboutinfo.About_IsTop;
        SEO_Title = about_title + " - " + SEO_Title;
        if (aboutinfo.About_SEO_Title.Length > 0)
        {
            SEO_Title =aboutinfo.About_SEO_Title;
        }
        if (aboutinfo.About_SEO_Keyword.Length > 0)
        {
            SEO_Keywords = aboutinfo.About_SEO_Keyword;
        }
        if (aboutinfo.About_SEO_Description.Length > 0)
        {
            SEO_Description = aboutinfo.About_SEO_Description;
        }
    }
    else
    {
        Response.Redirect("/404.aspx");
        about_title = "关于我们";
    }
        AD ad = new AD();
%>

<!DOCTYPE html>
<html>
<head style="background: #f5f5fa;">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=SEO_Title%></title>
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="Keywords" content="<% = SEO_Keywords%>" />
    <meta name="Description" content="<%=SEO_Description%>" />

    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index.css"))%>" rel="stylesheet">
    
    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>


</head>

<body style="background: #f5f5f9;">
    <uc1:topcate ID="top" runat="server" />
<!-- 列表 -->
    <p class="crumb-nav"><img src="/images/icon-nav.png">当前位置 > <a href="/">首页 </a> > <a href="javascript:;"><%=aboutinfo.About_Title %></a></p>
<div class="w-1200 clearfix" style="margin-bottom: 40px;">
  
   <div class="news-details">
        
        <h2 style="line-height:60px;"><%=aboutinfo.About_Title %></h2>
       <div id="article_content">
       <%=aboutinfo.About_Content %>
           </div>
               <div class="news-details-bottom clearfix" style=" height:80px; line-height:80px;">
        </div>

   </div>
   
</div>
    <uc1:bottom ID="bottom" runat="server" />
</body>
</html>

