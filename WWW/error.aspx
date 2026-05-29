<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Register Src="~/Public/topcate.ascx" TagName="topcate" TagPrefix="uc1" %>
<%@ Register Src="Public/bottom.ascx" TagName="bottom" TagPrefix="uc1" %>

<%
    Session["CurrentNav"] = "0";
    Public_Class pub = new Public_Class();
%>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>您访问的页面出错了 | <%=Application["site_title"].ToString()%></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="keywords" content="<%=Application["site_keyword"].ToString()%>" />
    <meta name="description" content="<%=Application["site_description"].ToString()%>" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">


    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index.css"))%>" rel="stylesheet">
</head>

<body>
    <uc1:topcate ID="top" runat="server" />

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td height="80"></td>
        </tr>
        <tr>
            <td align="center">
                <img src="/images/error_icon.png"></td>
        </tr>
        <tr>
            <td align="center">
                <h1>哎呀…您访问的页面出错了</h1>
            </td>
        </tr>
        <tr>
            <td align="center">系统在处理您的页面请求时发生了错误，请稍后重试</td>
        </tr>
        <tr>
            <td align="center"><a href="/" style="font-size:18px;">返回网站首页</a></td>
        </tr>
        <tr>
            <td height="80"></td>
        </tr>
    </table>
    <uc1:bottom ID="bottom" runat="server" />
</body>
</html>
