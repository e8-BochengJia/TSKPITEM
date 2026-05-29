<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    private NoticeCate myAppC;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("d2658816-1905-471f-935e-c60d4620f4d7");

        myAppC = new NoticeCate();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js?v=1" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
    <script src="/Public/u/ueditor.config.js?v=2" type="text/javascript"></script>
    <script src="/Public/u/ueditor.all.min.js?v=2" type="text/javascript"></script>
    <script src="/Public/u/lang/zh-cn/zh-cn.js" type="text/javascript"></script>

    <script>

        function editoradd(value, id) {
            UE.getEditor('Notice_Content').execCommand('insertHtml', value)

        }
    </script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">添加公告
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="notice_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">公告主题
                                </td>
                                <td class="cell_content">
                                    <input name="Notice_Title" type="text" id="Notice_Title" size="50" maxlength="50" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">公告类别
                                </td>
                                <td class="cell_content">
                                    <select name="Notice_Cate" id="Notice_Cate">
                                        <% =myAppC.NoticeCateOption(0)%>
                                    </select>
                                </td>
                            </tr>


                            <tr>
                                <td class="cell_title">上传图片
                                </td>
                                <td class="cell_content">
                                    <iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/FileUpload.aspx?App=content&formname=formadd&frmelement=Notice_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>"
                                        width="100%" height="35" frameborder="0" scrolling="no"></iframe>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">上传附件
                                </td>
                                <td class="cell_content">
                                    <iframe id="iframe3" src="<% =Application["upload_server_url"]%>/public/FileUpload.aspx?App=contentfile&formname=formadd&frmelement=Notice_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>"
                                        width="100%" height="35" frameborder="0" scrolling="no"></iframe>
                                    <span class="tip">支持格式： .jpg|.gif|.png|.swf|.rar|.zip|.pdf|.xls|.jpeg|.xlsx|.doc|.docx|.txt</span>

                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title" valign="top">公告内容
                                </td>
                                <td class="cell_content">
                                    <textarea cols="80" id="Notice_Content" name="Notice_Content" rows="16" style="width: 100%"></textarea>
                                    <script type="text/javascript">
                                        var ue = UE.getEditor('Notice_Content', {
                                            allowDivTransToP: false
                                        });
                                    </script>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">公示截至日期</td>
                                <td class="cell_content">
                                    <input type="text" class="input_calendar" name="Notice_ShowTime" id="Notice_ShowTime" maxlength="10" readonly="readonly" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" />
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $("#Notice_ShowTime").datepicker({ numberOfMonths: 1 });
                                        });
                                    </script>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">TITLE<br />
                                    (页面标题)
                                </td>
                                <td class="cell_content">
                                    <input name="Notice_SEO_Title" type="text" id="Notice_SEO_Title" size="50" maxlength="200" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">META_KEYWORDS<br />
                                    (页面关键词)
                                </td>
                                <td class="cell_content">
                                    <input name="Notice_SEO_Keyword" type="text" id="Notice_SEO_Keyword" size="50"
                                        maxlength="200" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">META_DESCRIPTION<br />
                                    (页面描述)
                                </td>
                                <td class="cell_content">
                                    <textarea name="Notice_SEO_Description" cols="50" rows="5" id="Notice_SEO_Description"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">热点
                                </td>
                                <td class="cell_content">
                                    <input name="Notice_IsHot" type="radio" id="Notice_IsHot1" value="1" />是
                                <input type="radio" name="Notice_IsHot" id="Notice_IsHot2" value="0" checked="checked" />否
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">是否显示
                                </td>
                                <td class="cell_content">
                                    <input name="Notice_IsAudit" type="radio" id="Notice_IsAudit1" value="1" checked="checked" />是
                                <input type="radio" name="Notice_IsAudit" id="Notice_IsAudit2" value="0" />否
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="new" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';"
                                        onmouseout="this.className='bt_grey';" onclick="location = 'notice_list.aspx';" />
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
