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
    int Article_ID=0;
    Article_ID = tools.CheckInt(Request["cate_id"]);

    ArticleCateInfo articlecate = cms.GetArticleCateByID(Article_ID);
    if(articlecate!=null)
    {
        SEO_Title = articlecate.Article_Cate_Name + " - "+SEO_Title;
        if(articlecate.Article_Cate_ID!=12)
        {
            Response.Redirect("/"+articlecate.Article_Cate_ID+"/");
        }
    }
    else
    {
        Response.Redirect("/404.aspx");
    }
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

    <script>
function checkInt(n,max){
    var regex = /^\d+$/;
    if(regex.test(n)){
        if (n > max || n <= 0) {
            $("#listpagenum").val(max);
       }
    } else {
        $("#listpagenum").val(1);
    }
}
</script>
    <style>
        .news-list-l h2 {
            width: 350px;
    height: 50px;
    line-height: 50px;
    font-size: 16px;
        }
        .news-list-l h2>i{
                display: inline-block;
    width: 4px;
    height: 18px;
    background: #ff0000;
    margin: 0 10px;
        }
        .news-list-l ul li{
            padding:10px 0px;
        }
    </style>
</head>
<body style="background: #f5f5fa;">

    <uc1:top ID="top" runat="server" />

<div class="w-1200 clearfix" style="margin-bottom: 40px;">
   <div class="banner-small"><%=ad.AD_Show("List_Banner", "", "cycle",0)%></div>

    <div class="news-list-l" >
        <%cms.Personnel(Article_ID,articlecate.Article_Cate_Name); %>
   </div>


   <!-- 左侧结束 -->
   <div class="news-list-r">
        <div class="essence-list">
            <h2><i></i>新闻阅读排行</h2>
            <%=cms.NewsRankingList() %>
        </div>
        <!-- 小banner -->
        <div class="banner-rigt"><%=ad.AD_Show("List_Right_1", "", "cycle",0)%></div>
        <!-- 排行 -->
       
        
       <div class="essence-list">
            <h2><i></i>精华推荐</h2>
            <ul>
                <%=cms.NewsRecommend() %>
            </ul>
        </div>
        <!-- 小banner -->
        <div class="banner-rigt"><%=ad.AD_Show("List_Right_2", "", "cycle",0)%></div>
        <!-- 二维码 -->
<%--        <div class="ew-code">
            <img src="/images/pic-ewm.jpg">
            <p>人民建党网微信</p>
            <p>扫一扫二维码   随时随地看新闻</p>
        </div>--%>
   </div>
   <!-- 右侧结束 -->
</div>
    <uc1:bottom ID="bottom" runat="server" />
</body>
</html>
