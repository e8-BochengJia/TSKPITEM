<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    private ArticleCate myAppC;
    private Article myApp;
    private ITools tools;

    private string Article_Cate_Name, Article_Cate_Site, Article_Cate_Href, Article_Cate_SEO_Title, Article_Cate_SEO_Keyword, Article_Cate_SEO_Description;
    private int Article_Cate_ID, Article_Cate_Sort,Article_Cate_ParentID,Article_Cate_IsTop;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("d3570eff-1fc9-48bd-a247-ba7db0bc18bd");

        myAppC = new ArticleCate();
        myApp = new Article();
        tools = ToolsFactory.CreateTools();

        Article_Cate_ID = tools.CheckInt(Request.QueryString["Article_cate_id"]);
        ArticleCateInfo entity = myAppC.GetArticleCateByID(Article_Cate_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Article_Cate_ID = entity.Article_Cate_ID;
            Article_Cate_Name = entity.Article_Cate_Name;
            Article_Cate_Sort = entity.Article_Cate_Sort;
            Article_Cate_Site = entity.Article_Cate_Site;
            Article_Cate_ParentID=entity.Article_Cate_ParentID;
            Article_Cate_Href = entity.Article_Cate_Href;
            Article_Cate_SEO_Title = entity.Article_Cate_SEO_Title;
            Article_Cate_SEO_Keyword = entity.Article_Cate_SEO_Keyword;
            Article_Cate_SEO_Description = entity.Article_Cate_SEO_Description;
            Article_Cate_IsTop = entity.Article_Cate_IsTop;
        }
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <link href="/CSS/index1.css" rel="stylesheet" />
    <script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
        <link href="/Scripts/KindEditor/themes/default/default.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/KindEditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/KindEditor/kindeditor.js" type="text/javascript"></script>
    <script src="/Scripts/KindEditor/lang/zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/KindEditor/plugins/code/prettify.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">
                    声音预览
                </td>
            </tr>
            <tr>
                <td class="content_content">
                     <div class="news-details">
                         <%myApp.VoiceList(Article_Cate_ID); %>
                            </div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
