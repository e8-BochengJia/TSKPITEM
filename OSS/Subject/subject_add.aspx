<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("2b40c0e9-1543-48e5-8836-d7addfee4236");
        tools = ToolsFactory.CreateTools();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">
                    专题报道添加
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
                                <input name="Subject_Name" type="text" id="Subject_Name" size="50" maxlength="50" />
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
                                <input type="radio" name="Subject_IsActive" id="Subject_IsActive1" value="1" checked="checked" />是
                                <input type="radio" name="Subject_IsActive" id="Subject_IsActive2" value="0" />否
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                专题排序
                            </td>
                            <td class="cell_content">
                                <input name="Subject_Sort" type="text" id="Subject_Sort" value="1" size="10" maxlength="10" />
                                <span class="tip">数字越小越靠前</span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <tr>
                            <td align="right">
                                <input type="hidden" id="action" name="action" value="new" />
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
