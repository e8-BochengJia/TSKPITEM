<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Article myApp;
    private ITools tools;

    private ArticleCate myAppC;

    private string Article_Title, Article_Content, Article_Img, Article_Site, Article_Intro, Article_Keyword, Article_Source, Article_Author, Article_Hyperlink;
    private int Article_ID, Article_Cate, Article_IsRecommend, Article_IsAudit, Article_Sort, Article_ContentID, Article_PageViews;
    private DateTime Article_Addtime;
    private string Article_SEO_Title, Article_SEO_Keyword, Article_SEO_Description;
    private string Article_Cate_Name;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276");

        myApp = new Article();
        myAppC = new ArticleCate();
        tools = ToolsFactory.CreateTools();

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

            ArticleCateInfo cateinfo = myAppC.GetArticleCateByID(Article_Cate);
            if (cateinfo != null)
            {
                Article_Cate_Name = cateinfo.Article_Cate_Name;
            }
            else
            {
                Article_Cate_Name = "--";
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Public/u/ueditor.config.js?v=1" type="text/javascript"></script>
    <script src="/Public/u/ueditor.all.min.js" type="text/javascript"></script>
    <script src="/Public/u/lang/zh-cn/zh-cn.js" type="text/javascript"></script>
        <script>
            function AuditProject(i, obj) {
                $("#action").val(obj);
                $('#button' + i).attr('disabled', ture);
                $("#formadd").submit();
            }
    </script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">查看文章</td>
            </tr>
            <tr>
                <td class="content_content">


                    <form id="formadd" name="formadd" method="post" action="Article_do.aspx">


                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
                            <tr>
                                <td class="cell_title">文章标题</td>
                                <td class="cell_content"><% =Article_Title%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">文章类别</td>
                                <td class="cell_content"><%=Article_Cate_Name %></td>
                            </tr>
                            <tr>
                                <td class="cell_title">文章来源</td>
                                <td class="cell_content"><%=Article_Source %></td>
                            </tr>
                            <%--        <tr>
          <td class="cell_title">文章作者</td>
          <td class="cell_content"><input name="Article_Author" type="text" id="Article_Author" value="<%=Article_Author %>" size="50" maxlength="50" /></td>
        </tr>--%>
                            <%--        <tr>
          <td class="cell_title">关键词</td>
          <td class="cell_content"><input name="Article_Keyword" type="text" id="Article_Keyword" value="<%=Article_Keyword %>" size="50" maxlength="50" /></td>
        </tr>--%>
                            <tr>
                                <td class="cell_title">文章摘要</td>
                                <td class="cell_content"><%=Article_Intro%></td>
                            </tr>
                            <tr <%if (Article_Img.Length == 0) { Response.Write("style='display:none'"); } %>>
                                <td class="cell_title">预览图片</td>
                                <td class="cell_content">
                                    <img src="<% =Application["upload_server_url"]+Article_Img%>" id="img_Article_Img" /><input name="Article_Img" value="<%=Article_Img %>" type="hidden" id="Article_Img" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title" valign="top">文章内容(不可编辑)
                                </td>
                                <td class="cell_content" id="Article00">
                                    <textarea cols="80" id="Article_Content" name="Article_Content" rows="16" style="width: 100%"><%=Article_Content %></textarea>
                                    <script type="text/javascript">
                                        var ue = UE.getEditor('Article_Content');
                                    </script>

                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">TITLE<br />
                                    (页面标题)
                                </td>
                                <td class="cell_content">
                                    <%=Article_SEO_Title %>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">META_KEYWORDS<br />
                                    (页面关键词)
                                </td>
                                <td class="cell_content">
                                    <%=Article_SEO_Keyword %>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">META_DESCRIPTION<br />
                                    (页面描述)
                                </td>
                                <td class="cell_content">
                                    <%=Article_SEO_Description%>
                                </td>
                            </tr>

                            <tr>
                                <td class="cell_title">排序</td>
                                <td class="cell_content"><%=Article_Sort %>
                                    <span class="tip">数字越小越靠前</span></td>
                            </tr>
                            <tr>
                                <td class="cell_title">浏览次数
                                </td>
                                <td class="cell_content">
                                    <%=Article_PageViews %>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">是否首页推荐</td>
                                <td class="cell_content"><%if (Article_IsRecommend == 1) { Response.Write("是"); } else { Response.Write("否"); } %></td>
                            </tr>
                            <tr>
                                <td class="cell_title">是否精华</td>
                                <td class="cell_content"><%if (Article_ContentID == 1) { Response.Write("是"); } else { Response.Write("否"); } %></td>
                            </tr>
                            <tr>
                                <td class="cell_title">审核状态
                                </td>
                                <td class="cell_content">
                                    <%=myApp.GetArticAudit(Article_IsAudit) %>
                                </td>
                            </tr>
                        </table>



                        <div class="foot_gapdiv">
                        </div>
                        <%if (Article_IsAudit == 0 || Article_IsAudit == 4)
                                { %>
                        <div class="float_option_div" id="float_option_div">
                            <input type="hidden" id="action" name="action" value="" />
                            <input type="hidden" id="Article_ID" name="Article_ID" value="<% =Article_ID%>" />
                          <%--  <%if (Public.CheckPrivilege("807ea41c-545d-46f9-a24b-9f4b125a444a")&&Article_IsAudit==0)
                                {%>
                        <input name="button" type="submit" class="bt_orange"  id="button1" value="初审通过" onclick="AuditProject('1','oneAudit');" />
                        
                            <%} %>--%>

                            <%if (Public.CheckPrivilege("59ff13a1-2da6-4ece-b156-62d915ae996a"))
                                {%>
                        <input name="button" type="submit" class="bt_orange"  id="button3" value="审核通过" onclick="AuditProject('3','twoAudit');" />
                        
                            <%} %>

                            <input name="button" type="submit" class="bt_orange"  id="button2" value="审核不通过" onclick="AuditProject('2','NoAudit');" />
                        </div>

                        <%} %>


                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
