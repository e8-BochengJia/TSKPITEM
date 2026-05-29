<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Register Src="Public/topcate.ascx" TagName="topcate" TagPrefix="uc1" %>
<%@ Register Src="Public/bottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<%
    Public_Class pub = new Public_Class();
    AD ad = new AD();
    Session["CurrentNav"] = "0";
    CMS cms = new CMS();
    string sign = "";
    ITools tools = ToolsFactory.CreateTools();
    Session["CurrentNav"] = 0;
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <%= pub.SEO_TITLE()%></title>
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="keywords" content="<%=Application["site_keyword"].ToString()%>" />
    <meta name="description" content="<%=Application["site_description"].ToString()%>" />

    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index.css"))%>" rel="stylesheet">
   
    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>

</head>
<body style="background: #f5f5f9;">
    <uc1:topcate ID="top" runat="server" />

   <div class="w-1200 clearfix" style="margin-bottom: 30px;">
 
        <%cms.Search(); %>
 
</div>

<uc1:bottom ID="bottom" runat="server" />
</body>
</html>
