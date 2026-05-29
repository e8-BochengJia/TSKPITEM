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
        }
        secondid = cms.GetParentSecondCate_ID(Article_cate_ID);
        if (articlecate.Article_Cate_Name == "全景科协")
        {
            Englishname = "About Us";
            chinaname = articlecate.Article_Cate_Name;
        }
        if (articlecate.Article_Cate_Name == "科技工作之家")
        {
            Englishname = "Workers'Home";
            chinaname = articlecate.Article_Cate_Name;
        }
        if (articlecate.Article_Cate_Name == "科协动态")
        {
            Englishname = "Dynamic";
            chinaname = articlecate.Article_Cate_Name;
        }
        if (articlecate.Article_Cate_Name == "科普你我共参与" || articlecate.Article_Cate_ID==53)
        {
            Englishname = "Popular science participation";
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
                $("#div_show").css("font-size", "20px");
                $("#div_show").css("line-height", "38px");


                $("#div_show p").css("font-size", "20px");
                $("#div_show p").css("line-height", "38px");

                $(this).addClass('active');
                $("#zhong").removeClass('active');
                $("#xiao").removeClass('active');

            });
            $("#zhong").click(function () {
                $("#div_show").css("font-size", "16px");
                $("#div_show").css("line-height", "32px");


                $("#div_show p").css("font-size", "16px");
                $("#div_show p").css("line-height", "32px");

                $(this).addClass('active');
                $("#da").removeClass('active');
                $("#xiao").removeClass('active');
            });
            $("#xiao").click(function () {
                $("#div_show").css("font-size", "12px");
                $("#div_show").css("line-height", "26px");


                $("#div_show p").css("font-size", "12px");
                $("#div_show p").css("line-height", "26px");
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
    <div class="w-1200 clearfix" style="margin-bottom: 30px;">
        <!-- 左侧 -->
        <div class="kx-state-left">
            <div class="ky-tit">
                <span><%=Englishname %></span>
                <p><%=chinaname %></p>

            </div>
            <%=cms.GetArticle_CateLeft(Article_cate_parentID,secondid) %>
        </div>

        <div class="workers-right">
               <!--startprint-->
            <div class="news-details-tit" style="width: auto;">
                <h2><%=article.Article_Title %></h2>
                <div class="news-details-date clearfix">
                    <span>添加时间：<%=article.Article_Addtime.ToString("yyyy-MM-dd") %>  作者：<%=article.Article_Author %>  来源：<%=article.Article_Source %>  点击：<%=article.Article_PageViews %></span>
                    <em style="cursor:pointer;">
                        <img src="/images/icon-30.jpg">收藏
                      <img src="/images/icon-31.jpg" onclick="btnPrint_onclick();"><c onclick="btnPrint_onclick();">打印</c>        
                      <img src="/images/icon-32.jpg">字体： 
                      <i id="da">大</i>   <i id="zhong" class="active">中</i>   <i id="xiao">小</i>
                    </em>
                </div>
            </div>
            <div class="text" id="div_show">
                <%=article.Article_Content %>
            </div>
               <!--endprint-->
            <div class="text-xg">
            <h2>推荐阅读</h2>
            <ul class="clearfix">
              <%=cms.LikeRecommend(Article_cate_ID) %>
               
            </ul>
        </div>
        </div>

        

    </div>

    <%-- <div class="w-1200 clearfix" style="margin-bottom: 40px;">
        <div class="banner-small">
            <%=ad.AD_Show("List_Banner", "", "cycle",0)%>
        </div>
        <div class="news-details">
            <div class="news-details-tit"><a href="/">首页</a><%=cms.GetArticleInfo_Cate_Nav(article.Article_CateID, " &gt; ")%> > 正文</div>
            <%if (article.Artide_ShoulderTitle.Length > 0) { Response.Write("<div style=\"font-size:" + article.Artide_ShoulderTitleSize + "px;float:right;line-height:20px;height:20px;text-align:center;width:720px;margin-bottom: 5px;\">" + article.Artide_ShoulderTitle + "</div>"); } %>
            <h2><%=article.Article_Title %></h2>
            <%if (article.Article_Hyperlink.Length > 0) { Response.Write("<div style=\"font-size:" + article.Article_HyperlinkSize + "px;line-height:20px;height:20px;text-align:right;width:720px;display:block;margin-top:10px;\">" + article.Article_Hyperlink + "</div>"); } %>

            <%if (article.Article_Author.Length > 0) { Response.Write("<div class=\"time-tit\" style=\"border-bottom:none;height:20px;line-height:20px;margin-bottom:0px;margin-top:15px;\"><span style=\"text-align: center;display:block;font-size:16px;\">" + article.Article_Author + "</span></div>"); }%>

            <div class="time-tit">
                <span><%=article.Article_Addtime.ToString("yyyy-MM-dd HH:mm:ss") %></span>

                <img src="/images/6.png" style="width: 15px;">字号: <font class="button01">大</font> <font class="button02">中</font> <font class="button03">小</font>
            </div>
            <div id="article_content">
                <%=article.Article_Content %>
            </div>

            <div class="news-details-bottom clearfix">
                <%if (article.Article_Source.Length > 0 || article.Article_Keyword.Length > 0)
                    {%>
                <div class="time-tit">
                    <%if (article.Article_Source.Length > 0) { Response.Write("来源：" + article.Article_Source + ""); }%>
                    <%if (article.Article_Keyword.Length > 0) { Response.Write("<span style=\"font-size:14px;\">关键词：" + article.Article_Keyword + "</span>"); }%>
                </div>
                <%} %>
                <%=cms.GetArticleKeyword(article.Article_ID,article.Article_Keyword) %>
            </div>
            <div class="clear"></div>
            <div class="share-btn">
                <div class="share_class">
                    <div class="bdsharebuttonbox">
                        分享到：
                        <img src="/images/icon-wx.png" onclick="javascript:;" class="bds_weixin" data-cmd="weixin">
                        <img src="/images/icon-wb.png" onclick="javascript:;" class="bds_tsina" data-cmd="tsina">
                    </div>
                </div>
            </div>

        </div>
        <!-- 左侧结束 -->
        <div class="news-list-r">
            <div class="essence-list">
                <h2><i></i>精华推荐</h2>
                <ul>
                    <%=cms.NewsRecommend() %>
                </ul>
            </div>
            <!-- 小banner -->
            <div class="banner-rigt">
                <%=ad.AD_Show("List_Right_1", "", "cycle",0)%>
            </div>
            <!-- 排行 -->

            <div class="essence-list">
                <h2><i></i>新闻阅读排行</h2>
                <%=cms.NewsRankingList() %>
            </div>
            <!-- 小banner -->
            <div class="banner-rigt">
                <%=ad.AD_Show("List_Right_2", "", "cycle",0)%>
            </div>
            <!-- 二维码 -->
            <div class="ew-code">
                <img src="/images/pic-ewm.jpg">
                <p>人民建党网微信</p>
                <p>扫一扫二维码   随时随地看新闻</p>
            </div>
        </div>
        <!-- 右侧结束 -->
    </div>--%>
    <uc1:bottom ID="bottom" runat="server" />
</body>
<%--<script>window._bd_share_config = { "common": { "bdSnsKey": {}, "bdText": "", "bdMini": "2", "bdMiniList": false, "bdPic": "", "bdStyle": "0", "bdSize": "16" }, "share": {} }; with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];</script>--%>
</html>
