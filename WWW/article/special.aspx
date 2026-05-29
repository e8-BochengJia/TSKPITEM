<%@ Page Language="C#" %>
<%@ Register Src="~/Public/top.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/Public/bottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();

    string SEO_Keywords = tools.NullStr(Application["Site_Keyword"]);
    string SEO_Description = tools.NullStr(Application["Site_Description"]);
    string SEO_Title = pub.SEO_TITLE();
    
    AD ad = new AD();

%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=SEO_Title%></title>
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="Keywords" content="<% = SEO_Keywords%>" />
    <meta name="Description" content="<%=SEO_Description%>" />

    <link href="/css/index1.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index1.css"))%>" rel="stylesheet">
    <link href="/css/index2.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index2.css"))%>" rel="stylesheet">
    <link href="/css/jquery.bxslider.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/jquery.bxslider.css"))%>" rel="stylesheet" type="text/css">
    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>



</head>
<body style="background: #f5f5fa;">

    <uc1:top ID="top" runat="server" />

<div class="w-1200 clearfix" style="margin-bottom: 40px;">
       <div class="banner-small"><%=ad.AD_Show("List_Banner", "", "cycle",0)%></div>

    <div class="zt-center-4" >
       <%-- <%=cms.GetSpecial() %>--%>
</div>

    </div> 
    <uc1:bottom ID="bottom" runat="server" />
</body>
</html>
