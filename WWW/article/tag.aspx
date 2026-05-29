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

    //SpecialInfo entity = cms.GetSpecialByID(Article_ID);
    SpecialInfo entity = null;
    if(entity!=null)
    {
        SEO_Title = entity.Special_Title + "-" + SEO_Title;
        if(entity.Special_IsRecommend==1&&entity.Special_IsAudit==1)
        {

        }
        else
        {
            Response.Redirect("/");
        }
    }
    else
    {
        Response.Redirect("/");
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



</head>
<body>

    <uc1:top ID="top" runat="server" />

    <!-- 专题 -->
<div class="top-banner" style="background: url(<%if(entity.Special_BannerImg.Length>0){Response.Write(pub.FormatImgURL(entity.Special_BannerImg, "fullpath"));}%>) no-repeat top;"></div>  

   <%-- <%=cms.Special_Show(entity) %>--%>

    <uc1:bottom ID="bottom" runat="server" />
    <script type="text/javascript">
//banner图
        var _banner = $('.banner li');
        var _num = $('.new-number span');
        var timer = ' ';
        var index = 0;

        function changeBanner() {
            var _bannerIndex = _banner.eq(index);
            var _numIndex = _num.eq(index);
            _bannerIndex.fadeIn().siblings().fadeOut();
            _numIndex.addClass('cur').siblings().removeClass('cur');
            index = ++index % _num.size();
        }

        _num.on('mouseover', function () {
            clearInterval(timer);
            index = $(this).index();
            changeBanner();
            timer = setInterval(changeBanner, 8000);
        });

        timer = setInterval(changeBanner, 8000);
        changeBanner();

</script>
<!-- 访谈 -->
    <script type="text/javascript" src="/js/jquery.bxslider.js"></script>
</body>
</html>
