<%@ Page Language="C#" %>

<%@ Register Src="~/Public/topcate.ascx" TagName="topcate" TagPrefix="uc1" %>
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
    int Article_ID = 0;
    Article_ID = tools.CheckInt(Request["cate_id"]);
    string Englishname = "";
    int parentID = 0;
    int secondid = 0;
    ArticleCateInfo articlecate = cms.GetArticleCateByID(Article_ID);
    if (articlecate != null)
    {
        if (articlecate.Article_Cate_ParentID != 0)
        {
            parentID = cms.GetParentCate_ID(Article_ID);
            if (parentID != 0)
            {
                articlecate = cms.GetArticleCateByID(parentID);
            }
            else
            {
                Response.Redirect("/404.aspx");
            }
        }
        else
        {
            parentID = Article_ID;
        }
        secondid = cms.GetParentSecondCate_ID(Article_ID);
        SEO_Title = articlecate.Article_Cate_Name + " - " + SEO_Title;
        if (articlecate.Article_Cate_ID != 3)
        {
            Response.Redirect("/" + articlecate.Article_Cate_ID + "/");
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

    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index1.css"))%>" rel="stylesheet" />
    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>

    <script type="text/javascript">
        function checkInt(n, max) {
            var regex = /^\d+$/;
            if (regex.test(n)) {
                if (n > max || n <= 0) {
                    $("#listpagenum").val(max);
                }
            } else {
                $("#listpagenum").val(1);
            }
        }
    </script>

</head>
<body style="background: #f5f5f9;">

    <uc1:topcate ID="top" runat="server" />
    <p class="crumb-nav">
        <img src="/images/icon-nav.png">当前位置 > <a href="/">首页 </a><%=cms.GetArticleInfo_Cate_Nav(parentID, " &gt; ")%>
    </p>
    <div class="w-1200 clearfix" style="margin-bottom: 30px; background: #fff;">
        <div class="on-line-left">
            <%=cms.GetAd_Show() %>
            <%cms.GetArticle_List_Kp(Article_ID); %>
        </div>
        <div class="on-line-right">
            <%=cms.GetArticle_CateList(parentID, secondid) %>
            <%=cms.HotArticleSort(Article_ID) %>
            <%=cms.ImgArticleShow(Article_ID) %>
            <%=cms.RecommendArticleShow(Article_ID) %>
        </div>
    </div>

    <uc1:bottom ID="bottom" runat="server" />
</body>
</html>
