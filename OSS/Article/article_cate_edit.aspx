<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ArticleCate myApp;
    private ITools tools;

    private string Article_Cate_Name, Article_Cate_Site, Article_Cate_Href, Article_Cate_SEO_Title, Article_Cate_SEO_Keyword, Article_Cate_SEO_Description;
    private int Article_Cate_ID, Article_Cate_Sort, Article_Cate_ParentID, Article_Cate_IsTop;
    private int Article_Cate_Type;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("8e2eb41c-060b-4a1c-9c7c-403d6f1072fa");

        myApp = new ArticleCate();
        tools = ToolsFactory.CreateTools();

        Article_Cate_ID = tools.CheckInt(Request.QueryString["Article_cate_id"]);
        ArticleCateInfo entity = myApp.GetArticleCateByID(Article_Cate_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Article_Cate_ID = entity.Article_Cate_ID;
            Article_Cate_Name = entity.Article_Cate_Name;
            Article_Cate_Sort = entity.Article_Cate_Sort;
            Article_Cate_Site = entity.Article_Cate_Site;
            Article_Cate_ParentID = entity.Article_Cate_ParentID;
            Article_Cate_Href = entity.Article_Cate_Href;
            Article_Cate_SEO_Title = entity.Article_Cate_SEO_Title;
            Article_Cate_SEO_Keyword = entity.Article_Cate_SEO_Keyword;
            Article_Cate_SEO_Description = entity.Article_Cate_SEO_Description;
            Article_Cate_IsTop = entity.Article_Cate_IsTop;
            Article_Cate_Type = entity.Article_Cate_Type;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script>
        function change_articlemaincate(target_div, obj) {
            $("#" + target_div).load("/article/article_cate_do.aspx?action=change_maincate&target=" + target_div + "&cate_id=" + $("#" + obj).val() + "&timer=" + Math.random());
        }
        $(function () {
            if ($("input[type='radio'][name='Article_Cate_Type']:checked").val() == 1) {
                $("#link_href").show("slow");
            }

            $("#Article_Cate_Type1").click(function () {
                $("#link_href").hide();

            })
            $("#Article_Cate_Type2").click(function () {

                $("#link_href").show("slow");
            })
        })
    </script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">

            <tr>
                <td class="content_title">修改文章类别</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="article_cate_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">文章类别类型</td>
                                <td class="cell_content">
                                    <input name="Article_Cate_Type" type="radio" id="Article_Cate_Type1" value="0" <% =Public.CheckedRadio(Article_Cate_Type.ToString(), "0")%> />
                                    普通
                                    <input name="Article_Cate_Type" type="radio" id="Article_Cate_Type2" value="1" <% =Public.CheckedRadio(Article_Cate_Type.ToString(), "1")%> />
                                链接
                                  
                            </tr>
                            <tr>
                                <td class="cell_title">类别名称</td>
                                <td class="cell_content">
                                    <input name="Article_Cate_Name" type="text" id="Article_Cate_Name" size="50" maxlength="50" value="<% =Article_Cate_Name%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">所属分类</td>
                                <td class="cell_content"><span id="main_cate">
                                    <%=myApp.Article_Category_Select(Article_Cate_ParentID, "main_cate")%></span></td>
                            </tr>
                            <tr id="link_href" style=" <% =Article_Cate_Type==0?"display: none;":""%>">
                                <td class="cell_title">外部链接
                                </td>
                                <td class="cell_content">
                                    <input name="Article_Cate_Href" type="text" id="Article_Cate_Href" size="50" value="<% =Article_Cate_Href%>" maxlength="50" /><span class="t12_red">*</span> <span class="tip">填写外部链接地址，如“http://www.baidu.com”</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">是否导航展示
                                </td>
                                <td class="cell_content">
                                    <input name="Article_Cate_IsTop" type="radio" id="Article_Cate_IsTop" value="1" <% =Public.CheckedRadio(Article_Cate_IsTop.ToString(), "1")%> />是
                                    <input type="radio" name="Article_Cate_IsTop" id="Article_Cate_IsTop1" value="0" <% =Public.CheckedRadio(Article_Cate_IsTop.ToString(), "0")%> />否 <span class="tip">&nbsp;&nbsp;选择“是”，前端首页顶部导航栏处将会展示</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">类别排序</td>
                                <td class="cell_content">
                                    <input name="Article_Cate_Sort" type="text" id="Article_Cate_Sort" size="10" maxlength="10" value="<% =Article_Cate_Sort%>" />
                                    <span class="tip">数字越小越靠前</span></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="renew" />
                                    <input type="hidden" id="Article_Cate_ID" name="Article_Cate_ID" value="<% =Article_Cate_ID%>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'Article_cate_list.aspx';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
