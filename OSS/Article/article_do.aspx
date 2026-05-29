<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">

    private Article myApp;
    private ITools tools;
    private ArticleCate myAppC;
    private string article_id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Article();
        tools = ToolsFactory.CreateTools();
        myAppC = new ArticleCate();
        article_id = tools.CheckStr(Request.QueryString["article_id"]);
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("870e6332-ab75-41cc-98c3-17e8af7827d3");

                myApp.AddArticle();
                break;
            case "renew":
                Public.CheckLogin("1daab676-20b6-4073-af76-132ee8874556");

                myApp.EditArticle();
                break;

            case "renew2":
                Public.CheckLogin("1daab676-20b6-4073-af76-132ee8874556");

                myApp.EditArticle2();
                break;

            case "editcate":
                Public.CheckLogin("1daab676-20b6-4073-af76-132ee8874556");
                myApp.EditArticle_batch();
                break;
            case "listedit":
                Public.CheckLogin("1daab676-20b6-4073-af76-132ee8874556");

                myApp.ListEditArticle();
                break;

            case "listedit2":
                Public.CheckLogin("1daab676-20b6-4073-af76-132ee8874556");

                myApp.ListEditArticle2();
                break;

            case "move":
                Public.CheckLogin("cc00c494-d211-438c-baef-ac20d419b066");

                myApp.DelArticle();
                break;

            case "batchmove":
                Public.CheckLogin("cc00c494-d211-438c-baef-ac20d419b066");
                myApp.DelArticle_batch();
                break;
            case "list":
                Public.CheckLogin("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276");

                Response.Write(myApp.GetArticles());
                Response.End();
                break;


            case "oneAudit":
                Public.CheckLogin("807ea41c-545d-46f9-a24b-9f4b125a444a");
                myApp.ArticleAuditOne(1);
                break;

            case "twoAudit":
                Public.CheckLogin("59ff13a1-2da6-4ece-b156-62d915ae996a");
                myApp.ArticleAuditTwo(2);
                break;
            case "NoAudit":
                Public.CheckLogin("807ea41c-545d-46f9-a24b-9f4b125a444a/59ff13a1-2da6-4ece-b156-62d915ae996a");
                myApp.ArticleAuditReturn(3);
                break;


            case "Toplist":
                Public.CheckLogin("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276");

                Response.Write(myApp.GetTopArticles());
                Response.End();
                break;

            case "check_name":
                Public.CheckLogin("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276");
                Response.Write(myApp.GetGetArticleByTitle());
                Response.End();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">文章转移</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/article/article_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">

                            <tr>
                                <td class="cell_title">转移到文章类别</td>
                                <td class="cell_content">
                                    <span id="main_cate"><%=myAppC.Article_Category_Select(0, "main_cate")%></span></td>
                            </tr>

                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="editcate" />
                                    <input type="hidden" id="article_id" name="article_id" value="<%=article_id %>" />
                                    <input name="Brand_Img" type="hidden" id="Brand_Img" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="确定" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'product.aspx';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>--%>
