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
    int cate_id = tools.CheckInt(Request["cate_id"]);
    Article_ID = tools.CheckInt(Request["id"]);

    int Article_cate_ID = 0;

    string Englishname = "";
    string chinaname = "";
    int Article_cate_parentID = 0;
    int secondid = 0;
    ArticleInfo article = cms.GetArticleByID(Article_ID);
    if (article == null)
    {
        Response.Redirect("/");
    }
    else
    {
        if (article.Article_IsAudit != 2 || article.Article_CateID != cate_id)
        {
            Response.Redirect("/");
        }
        Article_cate_ID = article.Article_CateID;
        ArticleCateInfo articlecate = cms.GetArticleCateByID(Article_cate_ID);
        if (articlecate != null)
        {
            if (articlecate.Article_Cate_ParentID != 0)
            {
                Article_cate_parentID = cms.GetParentCate_ID(Article_cate_ID);
                if (Article_cate_parentID != 0)
                {
                    articlecate = cms.GetArticleCateByID(Article_cate_parentID);
                }
                else
                {
                    Response.Redirect("/404.aspx");
                }
            }
            else
            {
                Article_cate_parentID = Article_cate_ID;
            }
            
            if (articlecate.Article_Cate_Name == "全景科协")
            {
                Englishname = "About Us";
                chinaname = articlecate.Article_Cate_Name;
            }
            if (articlecate.Article_Cate_Name == "科协动态")
            {
                Englishname = "Dynamic";
                chinaname = articlecate.Article_Cate_Name;
            }
            if (articlecate.Article_Cate_Name == "科技工作之家")
            {
                Englishname = "Workers'Home";
                chinaname = articlecate.Article_Cate_Name;
            }
            if (article.Article_SEO_Title.Length > 0)
            {
                SEO_Title = article.Article_SEO_Title;
            }
            if (article.Article_SEO_Keyword.Length > 0)
            {
                SEO_Keywords = article.Article_SEO_Keyword;
            }
            if (article.Article_SEO_Description.Length > 0)
            {
                SEO_Description = article.Article_SEO_Description;
            }
        }
        secondid = cms.GetParentSecondCate_ID(Article_cate_ID);
       

    }
    cms.UpdatePages(article.Article_ID);
    AD ad = new AD();
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=SEO_Title%></title>
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="Keywords" content="<% = SEO_Keywords%>" />
    <meta name="Description" content="<%=SEO_Description%>" />

    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index.css"))%>" rel="stylesheet">
    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>

    <script type="text/javascript">

        $(function () {

            $("#da").click(function () {
                $(".news-details-text").css("font-size", "20px");
                $(".news-details-text").css("line-height", "38px");


                $(".news-details-text p").css("font-size", "20px");
                $(".news-details-text p").css("line-height", "38px");

                $(this).addClass('active');
                $("#zhong").removeClass('active');
                $("#xiao").removeClass('active');

            });
            $("#zhong").click(function () {
                $(".news-details-text").css("font-size", "16px");
                $(".news-details-text").css("line-height", "32px");


                $(".news-details-text p").css("font-size", "16px");
                $(".news-details-text p").css("line-height", "32px");

                $(this).addClass('active');
                $("#da").removeClass('active');
                $("#xiao").removeClass('active');
            });
            $("#xiao").click(function () {
                $(".news-details-text").css("font-size", "12px");
                $(".news-details-text").css("line-height", "26px");


                $(".news-details-text p").css("font-size", "12px");
                $(".news-details-text p").css("line-height", "26px");
                $(this).addClass('active');
                $("#da").removeClass('active');
                $("#zhong").removeClass('active');

            });
        });
        function btnPrint_onclick() {

            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
        }
    </script>

</head>
<body style="background: #f5f5f9;">

    <uc1:topcate ID="top" runat="server" />

    <p class="crumb-nav">
        <img src="/images/icon-nav.png">当前位置 > <a href="/">首页 </a><%=cms.GetArticleInfo_Cate_Nav(Article_cate_ID, " &gt; ")%>
    </p>
    <div class="w-1200 clearfix" style="margin-bottom: 30px; background: #fff;">
           <!--startprint-->
        <div class="news-details-tit">
            <h2><%=article.Article_Title %></h2>
            <div class="news-details-date clearfix">
                <span>添加时间：<%=article.Article_Addtime.ToString("yyyy-MM-dd") %>  作者：<%=article.Article_Author %>  来源：<%=article.Article_Source %>  点击：<%=article.Article_PageViews %></span>
                <em style="cursor:pointer;">
                    <img src="/images/icon-30.jpg">收藏
                      <img src="/images/icon-31.jpg" onclick="btnPrint_onclick();"><c onclick="btnPrint_onclick();">打印</c>        
                      <img src="/images/icon-32.jpg">字体： 
                      <i id="da">大</i>   <i id="zhong" class="active">中</i>   <i id="xiao">小</i>
                </em>
                <div class="bdsharebuttonbox bdshare-button-style0-16" style="display:inline-block;float:right;" data-bd-bind="1571824675822">
                  <b>
                  分享到
                   <a data-cmd="weixin" style="float:none;padding-left:5px;"><img  class="bds_weixin" data-cmd="weixin" src="/images/icon-py.jpg"> </a>
                   <a data-cmd="tsina" style="float:none;padding-left:5px;"><img data-cmd="tsina" src="/images/icon-wb.jpg"> </a>
                
                   <a data-cmd="renren"  style="float:none;padding-left:5px;"><img data-cmd="renren" src="/images/icon-rr.jpg"> </a>



               </b>
                    </div>
     <%--           <div class="bdsharebuttonbox bdshare-button-style0-16" style="display:inline-block;float:left;" data-bd-bind="1571824675822"> <p style="display:inline;float:left;font-size: 14px;line-height:45px;margin-bottom:0px;">分享到</p>
    <a data-cmd="weixin" style="padding-left:0px;background-image:none;width:34px; height:34px; border-radius:1000px; display:inline-block; vertical-align:middle; margin-left:20px; background-color:#40b231;"><img class="bds_weixin" data-cmd="weixin" src="/images/ym_icon20_2.png" style="padding-left:0px;background-image:none;width:20px; height:20px; display:block; margin:0 auto; margin-top:9px;"></a>
    <a data-cmd="tsina" style="padding-left:0px;background-image:none;width:34px; height:34px; border-radius:1000px; display:inline-block; vertical-align:middle; margin-left:20px; background-color:#e04848;"><img src="/images/ym_icon20_3.png" data-cmd="tsina" style="width:20px; height:20px; display:block; margin:0 auto; margin-top:9px;"></a>
    <a style="padding-left:0px;background-image:none;width:34px; height:34px; border-radius:1000px; display:inline-block; vertical-align:middle; margin-left:20px; background-color:#0073ff;"><img class="bds_more" data-cmd="more" src="/images/ym_icon20_4.png" style="padding-left:7px;background-image:none;width:20px; height:20px; display:block; margin:0 auto; margin-top:9px;"></a>
</div>--%>
            </div>
        </div>
        <div class="news-details-text">
            <%=article.Article_Content %>
              <!--endprint-->
             <%=cms.Kp_Recommend(Article_cate_ID) %>
        </div>
         <div class="on-line-right">
            <%=cms.GetArticle_CateList(Article_cate_parentID, secondid) %>
            <%=cms.HotArticleSort(Article_ID) %>
            <%=cms.ImgArticleShow(Article_ID) %>
            <%=cms.RecommendArticleShow(Article_ID) %>
        </div>
    </div>

   
    <uc1:bottom ID="bottom" runat="server" />
</body>
<script>window._bd_share_config = { "common": { "bdSnsKey": {}, "bdText": "", "bdMini": "2", "bdMiniList": false, "bdPic": "", "bdStyle": "0", "bdSize": "16" }, "share": {} }; with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];</script>
</html>
