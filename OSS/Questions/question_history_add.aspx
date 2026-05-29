<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">


    private ITools tools;

    private Question Myapp;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["selected_productid"] = "";
        }
        Public.CheckLogin("29799310-e6bc-491c-812e-0d87be7200e2");
        Myapp = new Question();
        tools = ToolsFactory.CreateTools();

    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>

</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">添加套题</td>
            </tr>

            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/Questions/question_do.aspx">

                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2">
                            <tr>
                                <td class="cell_title">应用题库</td>
                                <td class="cell_content"><a href="" id="btn_opt">
                                    <input type="button" value="选择题库" class="bt_orange" /></a><span class="tip">&nbsp;&nbsp;点击选择题库</span><input type="hidden" name="favor_productid" id="favor_productid" />
                                    <div class="div_picker" id="product_picker"></div>

                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="Q_ID" name="Q_ID" value="" />
                                    <input type="hidden" id="action" name="action" value="Qhnew" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                </td>
                            </tr>
                        </table>


                    </form>
                </td>
            </tr>
        </table>

    </div>
    <script type="text/javascript">

        $("#btn_opt").click(function () {
            $("#btn_opt").attr("href", "question_Check.aspx?tag=tag&timer=" + Math.random());
        });

        $("#btn_opt").zxxbox({ height: 'auto', width: 600, title: '', bar: false, btnclose: false });
    </script>
    <%--<script src="/Scripts/promotion.js" type="text/javascript"></script>--%>
</body>
</html>
