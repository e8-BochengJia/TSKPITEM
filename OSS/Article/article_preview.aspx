<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Article myApp;
    private ITools tools;

    private ArticleCate myAppC;
    private SensitiveWords words;
    private string Article_Title, Article_Content, Article_Img, Article_Site, Article_Intro, Article_Keyword, Article_Source, Article_Author, Article_Hyperlink, Artide_ShoulderTitle;
    private int Article_ID, Article_Cate, Article_IsRecommend, Article_IsAudit, Article_Sort, Article_ContentID, Article_PageViews, Artide_ShoulderTitleSize, Article_HyperlinkSize, Artide_IsTop;
    private DateTime Article_Addtime;
    private string Article_SEO_Title, Article_SEO_Keyword, Article_SEO_Description;
    private string Article_Cate_Name;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276");

        myApp = new Article();
        myAppC = new ArticleCate();
        tools = ToolsFactory.CreateTools();
        words = new SensitiveWords();
        Article_ID = tools.CheckInt(Request.QueryString["Article_id"]);
        ArticleInfo entity = myApp.GetArticleByID(Article_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Article_ID = entity.Article_ID;
            Article_Cate = entity.Article_CateID;
            Article_IsRecommend = entity.Article_IsRecommend;
            Article_IsAudit = entity.Article_IsAudit;
            Article_Img = entity.Article_Img;
            Article_Source = entity.Article_Source;
            Article_Author = entity.Article_Author;
            Article_Keyword = entity.Article_Keyword;
            Article_Intro = entity.Article_Intro;
            Article_Sort = entity.Article_Sort;
            Article_Title = entity.Article_Title;
            Article_Content = entity.Article_Content;
            Article_Addtime = entity.Article_Addtime;
            Article_Site = entity.Article_Site;
            Article_Hyperlink = entity.Article_Hyperlink;
            Article_ContentID = entity.Article_ContentID;
            Article_SEO_Title = entity.Article_SEO_Title;
            Article_SEO_Keyword = entity.Article_SEO_Keyword;
            Article_SEO_Description = entity.Article_SEO_Description;
            Article_PageViews = entity.Article_PageViews;
            Artide_ShoulderTitle = entity.Artide_ShoulderTitle;
            Artide_ShoulderTitleSize = entity.Artide_ShoulderTitleSize;
            Article_HyperlinkSize = entity.Article_HyperlinkSize;
            Artide_IsTop = entity.Artide_IsTop;
            ArticleCateInfo cateinfo = myAppC.GetArticleCateByID(Article_Cate);
            if (cateinfo != null)
            {
                Article_Cate_Name = cateinfo.Article_Cate_Name;
            }
            else
            {
                Article_Cate_Name = "--";
            }
            Article_Content = words.FilterSensitiveWords(Article_Content);
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <link href="/CSS/index.css" rel="stylesheet" />
    <script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <link href="/Scripts/KindEditor/themes/default/default.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/KindEditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/KindEditor/kindeditor.js" type="text/javascript"></script>
    <script src="/Scripts/KindEditor/lang/zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/KindEditor/plugins/code/prettify.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
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
    </script>

    <script>
        function AuditProject(i, obj) {
            $("#action").val(obj);
            $('#button' + i).attr('disabled', ture);
            $("#formadd").submit();
        }
    </script>
    <style>
        #article_content img
        {
            display: block;
            margin: 0 auto;
        }
    </style>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">预览文章</td>
            </tr>
            <tr>
                <td class="content_content">
                    <%--<div class="news-details">
                        <div class="news-details-tit"><a>首页</a><%=myAppC.GetArticleInfo_Cate_Nav(Article_Cate, " &gt; ")%> > 正文</div>
                        <%if (Artide_ShoulderTitle.Length > 0) { Response.Write("<div style=\"font-size:" + Artide_ShoulderTitleSize + "px;float:right;line-height:20px;height:20px;text-align:center;width:720px;margin-bottom: 5px;\">" + Artide_ShoulderTitle + "</div>"); } %>
                        <h2><%=Article_Title %></h2>
                        <%if (Article_Hyperlink.Length > 0) { Response.Write("<div style=\"font-size:" + Article_HyperlinkSize + "px;line-height:20px;height:20px;text-align:right;width:720px;display:block;margin-top:10px;\">" + Article_Hyperlink + "</div>"); } %>

                        <%if (Article_Author.Length > 0) { Response.Write("<div class=\"time-tit\" style=\"border-bottom:none;height:20px;line-height:20px;margin-bottom:0px;margin-top:15px;\"><span style=\"text-align: center;display:block;font-size:16px;\">" + Article_Author + "</span></div>"); }%>
                        <div class="time-tit">
                           <h2><%=Article_Title %></h2>
                            <span><%=Article_Addtime.ToString("yyyy-MM-dd HH:mm:ss") %></span>

                            <img src="/images/6.png" style="width: 15px;">字号: <font class="button01">大</font> <font class="button02">中</font> <font class="button03">小</font>
                        </div>
                        <div id="article_content">
                            <%=Article_Content %>
                        </div>
                     
                        <div class="news-details-bottom clearfix" style="height: 80px; line-height: 80px;">
                            <%if (Article_Source.Length > 0 || Article_Keyword.Length > 0)
                              {%>
                            <div class="time-tit">
                                <%if (Article_Source.Length > 0) { Response.Write("来源：" + Article_Source + ""); }%>
                                <%if (Article_Keyword.Length > 0) { Response.Write("<span style=\"font-size:14px;\">关键词：" + Article_Keyword + "</span>"); }%>
                            </div>
                            <%} %>
                        </div>
                    </div>--%>
                      <p class="crumb-nav">
        当前位置 > <a href="/">首页 </a><%=myAppC.GetArticleInfo_Cate_Nav(Article_Cate, " &gt; ")%></p>
                     <div class="workers-right">
                         <div class="w-1200 clearfix" style="margin-bottom: 30px;">
        <div class="news-details-tit" style="width: auto;">
            <h2><%=Article_Title %></h2>
            <div class="news-details-date clearfix">
                  <span>添加时间：<%=Article_Addtime.ToString("yyyy-MM-dd") %>  作者：<%=Article_Author %>  来源：<%=Article_Source %>  点击：<%=Article_PageViews %></span>
                  <em>
                      <img src="/images/icon-30.jpg">收藏
                      <img src="/images/icon-31.jpg">打印        
                      <img src="/images/icon-32.jpg">字体： 
                      <i id="da">大</i>   <i id="zhong" class="active">中</i>   <i id="xiao">小</i>
                   </em>
            </div>
        </div>
             <div class="text">
                 <%=Article_Content %>
                 </div>
           </div></div>
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <div class="foot_gapdiv">
                    </div>
                    <form id="formadd" name="formadd" method="post" action="Article_do.aspx">

                        <div class="float_option_div" id="float_option_div">
                            <input type="hidden" id="action" name="action" value="" />
                            <input type="hidden" id="Article_ID" name="Article_ID" value="<% =Article_ID%>" />
                            <%--  <%if (Public.CheckPrivilege("807ea41c-545d-46f9-a24b-9f4b125a444a")&&Article_IsAudit==0)
                                {%>
                        <input name="button" type="submit" class="bt_orange"  id="button1" value="初审通过" onclick="AuditProject('1','oneAudit');" />
                        
                            <%} %>--%>

                            <%if (Public.CheckPrivilege("59ff13a1-2da6-4ece-b156-62d915ae996a"))
                              {%>
                            <input name="button" type="submit" class="bt_orange" id="button3" value="审核通过" onclick="AuditProject('3', 'twoAudit');" />
                            <input name="button" type="submit" class="bt_orange" id="button2" value="审核不通过" onclick="AuditProject('2', 'NoAudit');" />

                            <%} %>
                        </div>


                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
