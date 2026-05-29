<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    private ArticleSubject myApp;
    private ITools tools;

    private string Subject_Name, Subject_Img, Img_src;
    private int Subject_ID, Subject_IsActive, Subject_Sort;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("a0059a41-e628-4625-a67a-9da2f8b20fe1");

        myApp = new ArticleSubject();
        tools = ToolsFactory.CreateTools();

        Subject_ID = tools.CheckInt(Request.QueryString["subject_id"]);
        ArticleSubjectInfo entity = myApp.GetArticleSubjectByID(Subject_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Subject_ID = entity.Subject_ID;
            Subject_Name = entity.Subject_Name;
            Subject_Img = entity.Subject_Img;
            Img_src = Application["upload_server_url"] + Subject_Img;
            Subject_IsActive = entity.Subject_IsActive;
            Subject_Sort = entity.Subject_Sort;
        }
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            if ("<%=Subject_Img %>" != "") {
                $("#tr_Subject_Img").show();
                $("#img_Subject_Img").attr("src", "<%=Img_src%>")
            }
        });
    </script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">
                    专题报道编辑
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="subject_do.aspx">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                        <tr>
                            <td class="cell_title">
                                专题名称
                            </td>
                            <td class="cell_content">
                                <input name="Subject_Name" type="text" id="Subject_Name" size="50" maxlength="50"
                                    value="<%=Subject_Name %>" />
                                <span class="t12_red">*</span> <span class="tip">&nbsp;&nbsp;填写专题名称，最长不要超过200字</span>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="cell_title">
                                图片
                            </td>
                            <td class="cell_content">
                                <iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/FileUpload.aspx?App=subject&formname=formadd&frmelement=Subject_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>"
                                    width="100%" height="25" frameborder="0" scrolling="no"></iframe>
                            </td>
                        </tr>
                        <tr id="tr_Subject_Img" style="display: none;">
                            <td class="cell_title">
                            </td>
                            <td class="cell_content">
                                <img src="" id="img_Subject_Img" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                是否启用
                            </td>
                            <td class="cell_content">
                                <input type="radio" name="Subject_IsActive" id="Subject_IsActive1" value="1" <%=Public.CheckedRadio(Subject_IsActive.ToString(), "1")%> />是
                                <input type="radio" name="Subject_IsActive" id="Subject_IsActive2" value="0" <%=Public.CheckedRadio(Subject_IsActive.ToString(), "0")%> />否
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                专题排序
                            </td>
                            <td class="cell_content">
                                <input name="Subject_Sort" type="text" id="Subject_Sort" value="<%=Subject_Sort %>"
                                    size="10" maxlength="10" />
                                <span class="tip">数字越小越靠前</span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <tr>
                            <td align="right">
                                <input type="hidden" id="action" name="action" value="renew" />
                                <input type="hidden" id="Subject_ID" name="Subject_ID" value="<% =Subject_ID%>" />
                                <input type="hidden" id="Subject_Img" name="Subject_Img" />
                                <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';"
                                    onmouseout="this.className='bt_grey';" onclick="location='subject_list.aspx';" />
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
