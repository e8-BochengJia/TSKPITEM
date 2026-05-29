<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    private Help myApp;
    private ITools tools;
    private HelpCate myAppC;

    private string Help_Title, Help_Content, Help_Site;
    private int Help_ID, Help_CateID, Help_IsFAQ, Help_IsActive, Help_Sort;
    private string Help_SEO_Title, Help_SEO_Keyword, Help_SEO_Description;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("14422eb0-8367-45e1-b955-c40aee162094");

        myApp = new Help();
        tools = ToolsFactory.CreateTools();
        myAppC = new HelpCate();

        Help_ID = tools.CheckInt(Request.QueryString["Help_ID"]);
        HelpInfo entity = myApp.GetHelpByID(Help_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Help_ID = entity.Help_ID;
            Help_CateID = entity.Help_CateID;
            Help_IsFAQ = entity.Help_IsFAQ;
            Help_IsActive = entity.Help_IsActive;
            Help_Title = entity.Help_Title;
            Help_Content = entity.Help_Content;
            Help_Sort = entity.Help_Sort;
            Help_Site = entity.Help_Site;
            Help_SEO_Title = entity.Help_SEO_Title;
            Help_SEO_Keyword = entity.Help_SEO_Keyword;
            Help_SEO_Description = entity.Help_SEO_Description;
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
    <script src="/Public/u/ueditor.config.js?v=2" type="text/javascript"></script>
    <script src="/Public/u/ueditor.all.min.js?v=2" type="text/javascript"></script>
    <script src="/Public/u/lang/zh-cn/zh-cn.js" type="text/javascript"></script>


    <script>

        function editoradd(value, id) {
            UE.getEditor('Help_Content').execCommand('insertHtml', value)

        }
    </script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">
                    添加帮助
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="help_do.aspx">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                        <tr>
                            <td class="cell_title">
                                帮助主题
                            </td>
                            <td class="cell_content">
                                <input name="Help_Title" type="text" id="Help_Title" size="50" maxlength="50" value="<% =Help_Title%>" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                帮助类别
                            </td>
                            <td class="cell_content">
                                <select name="Help_CateID" id="Help_CateID">
                                    <% =myAppC.HelpCateOption(Help_CateID)%>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                上传图片
                            </td>
                            <td class="cell_content">
                                <iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/FileUpload.aspx?App=content&formname=formadd&frmelement=Help_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>"
                                    width="100%" height="35" frameborder="0" scrolling="no"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title" valign="top">
                                帮助内容
                            </td>
                            <td class="cell_content">
                                <textarea cols="80" id="Help_Content" name="Help_Content" rows="16" style="width:100%"><% =Help_Content%></textarea>
                                    <script type="text/javascript">
                                        var ue = UE.getEditor('Help_Content', {
                                            allowDivTransToP: false
                                        });
                                    </script>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                TITLE<br />
                                (页面标题)
                            </td>
                            <td class="cell_content">
                                <input name="Help_SEO_Title" type="text" id="Help_SEO_Title" size="50" maxlength="200" value="<% =Help_SEO_Title%>" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                META_KEYWORDS<br />
                                (页面关键词)
                            </td>
                            <td class="cell_content">
                                <input name="Help_SEO_Keyword" type="text" id="Help_SEO_Keyword" size="50" maxlength="200" value="<% =Help_SEO_Keyword%>"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                META_DESCRIPTION<br />
                                (页面描述)
                            </td>
                            <td class="cell_content">
                                <textarea name="Help_SEO_Description" cols="50" rows="5" id="Help_SEO_Description"><% =Help_SEO_Description%></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                是否显示
                            </td>
                            <td class="cell_content">
                                <input name="Help_IsActive" type="radio" id="Help_IsActive1" value="1" <% =Public.CheckedRadio(Help_IsActive.ToString(), "1")%> />是
                                <input type="radio" name="Help_IsActive" id="Help_IsActive2" value="0" <% =Public.CheckedRadio(Help_IsActive.ToString(), "0")%> />否
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                是否常见
                            </td>
                            <td class="cell_content">
                                <input name="Help_IsFAQ" type="radio" id="Help_IsFAQ1" value="1" <% =Public.CheckedRadio(Help_IsFAQ.ToString(), "1")%> />是
                                <input type="radio" name="Help_IsFAQ" id="Help_IsFAQ2" value="0" <% =Public.CheckedRadio(Help_IsFAQ.ToString(), "0")%> />否
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                排序
                            </td>
                            <td class="cell_content">
                                <input name="Help_Sort" type="text" id="Help_Sort" onkeyup="if(isNaN(value))execCommand('undo')"
                                    onafterpaste="if(isNaN(value))execCommand('undo')" value="<%=Help_Sort %>" size="10"
                                    maxlength="10" />
                                <span class="tip">数字越小越靠前</span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <tr>
                            <td align="right">
                                <input type="hidden" id="action" name="action" value="renew" />
                                <input type="hidden" id="Help_ID" name="Help_ID" value="<% =Help_ID%>" />
                                <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';"
                                    onmouseout="this.className='bt_grey';" onclick="location='help_list.aspx';" />
                            </td>
                        </tr>
                    </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
