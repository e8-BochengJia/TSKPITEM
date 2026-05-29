<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            font-size:12px;
        }
        .up_success
        {
            font-size: 12px;
            line-height: 150%;
            font-weight: bold;
            color: #009900;
            text-decoration: none;
        }
    </style>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
    <%
        ITools tools = ToolsFactory.CreateTools();
        string msgtype, msg, rtvalue, app, formname, frmelement, s_msg;

        msgtype = tools.CheckStr(Request.QueryString["msgtype"]);
        msg = tools.CheckStr(Request.QueryString["msg"]);
        rtvalue = tools.CheckStr(Request.QueryString["rtvalue"]);
        app = tools.CheckStr(Request.QueryString["app"]);
        formname = tools.CheckStr(Request.QueryString["formname"]);
        frmelement = tools.CheckStr(Request.QueryString["frmelement"]);
        s_msg = Application["upload_server_url"] + msg.Substring(0, msg.LastIndexOf("/") + 1) + "s_" + msg.Substring(msg.LastIndexOf("/") + 1);

        switch (msgtype)
        {
            case "error":
                if (msg == "error_filetype")
                {
                    Response.Write("无效的文件类型  ");
                }
                if (msg == "error_exceedlimit")
                {
                    Response.Write("文件大小超过限制  ");
                }
                Response.Write(" <a href=\"" + Application["upload_server_url"] + "Public/FileUpload.aspx?App=" + app + "&OrderName=" + Request["OrderName"] + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["upload_server_return_admin"] + "\" style=\"color:#5CBDED;\">重新上传</a>");
                break;
            case "success":
                if (rtvalue == "1")
                {
                    switch (app)
                    {
                        case "product":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                            Response.Write("parent.$('#td_upload').hide();");
                            Response.Write("</script>");
                            break;

                        case "Device":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                            Response.Write("parent.$('#td_upload').hide();");
                            Response.Write("</script>");
                            break;

                        case "friendlylink":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + Application["upload_server_url"] + msg + "';");
                            Response.Write("parent.$('#tr_" + frmelement + "').show();");
                            Response.Write("</script>");
                            break;
                        case "AD":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + Application["upload_server_url"] + msg + "';");
                            Response.Write("parent.$('#tr_" + frmelement + "').show();");
                            Response.Write("</script>");
                            break;
                        case "article":
                        case "Special":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + Application["upload_server_url"] + msg + "';");
                            Response.Write("parent.$('#tr_" + frmelement + "').show();");
                            Response.Write("</script>");
                            break;
                        case "brand":
                        case "jiangitem":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + Application["upload_server_url"] + msg + "';");
                            Response.Write("parent.$('#tr_" + frmelement + "').show();");
                            Response.Write("</script>");
                            break;
                        case "preview":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + Application["upload_server_url"] + msg + "';");
                            Response.Write("parent.$('#tr_" + frmelement + "').show();");
                            Response.Write("</script>");
                            break;
                        case "store":
                        case "category":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + Application["upload_server_url"] + msg + "';");
                            Response.Write("parent.$('#tr_" + frmelement + "').show();");
                            Response.Write("</script>");
                            break;
                        case "productintro":
                            Response.Write("<script type=\"text/javascript\">");
                            //Response.Write("parent.CKEDITOR.instances." + frmelement + ".insertHtml('<img src=\"" + Application["upload_server_url"] + msg + "\" />');");
                            Response.Write("parent.editoradd('<img src=\"" + Application["upload_server_url"] + msg + "\" />','" + frmelement + "');");
                            Response.Write("location.href=\"" + Application["upload_server_url"] + "Public/FileUpload.aspx?App=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["upload_server_return_admin"] + "\";</script>");
                            break;
                        case "content":
                            Response.Write("<script type=\"text/javascript\">");
                            //Response.Write("parent.CKEDITOR.instances." + frmelement + ".insertHtml('<img src=\"" + Application["upload_server_url"] + msg + "\" style=\"width:80%;\" />');");
                            //Response.Write("KindEditor.insertHtml('#" + frmelement + "','<img src=\"" + Application["upload_server_url"] + msg + "\" />');");
                            Response.Write("parent.editoradd('<img src=\"" + Application["upload_server_url"] + msg + "\"/>','" + frmelement + "');");
                            Response.Write("location.href=\"" + Application["upload_server_url"] + "Public/FileUpload.aspx?App=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["upload_server_return_admin"] + "\";</script>");
                            break;
                        case "promotion":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.editoradd('<img src=\"" + Application["upload_server_url"] + msg + "\" />','" + frmelement + "');");
                            //Response.Write("parent.CKEDITOR.instances." + frmelement + ".insertHtml('<img src=\"" + Application["upload_server_url"] + msg + "\" />');");
                            Response.Write("location.href=\"" + Application["upload_server_url"] + "Public/FileUpload.aspx?App=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["upload_server_return_admin"] + "\";</script>");
                            break;
                        case "pextend":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.InputParentImg('" + msg + "');");
                            Response.Write("</script>");
                            break;
                        case "software":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                            Response.Write("parent.$('#td_upload').hide();");
                            Response.Write("</script>");
                            break;
                        case "Coupon":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                            Response.Write("parent.$('#td_upload').hide();");
                            Response.Write("</script>");
                            break;

                        case "productPrint":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                            Response.Write("parent.$('#td_upload').hide();");
                            Response.Write("</script>");
                            break;

                        case "OrderInvoiceAnnex":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("</script>");
                            break;

                        case "PrintInvoiceAnnex":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("</script>");
                            break;

                        case "PrintServices":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                            Response.Write("</script>");
                            break;

                        case"BigCustomer":
                            Response.Write("<script type=\"text/javascript\">");
                            Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                            Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" +Application["upload_server_url"]+ msg + "';");
                            Response.Write("parent.$('#tr_" + frmelement + "').show();");
                            Response.Write("</script>");
                            break;
                        case "contentfile":
                            Response.Write("<script type=\"text/javascript\">");
                            //Response.Write("parent.editoradd('<img src=\"" + Application["upload_server_url"] + msg + " \"style=\"width:80%;\" />','" + frmelement + "');");
                            string name = msg.Substring(msg.LastIndexOf('/')+1);
                            Response.Write("parent.editoradd('<a class=\"bianse\" href=\"" + Application["upload_server_url"] + msg + "\" target=\"_blank\">点击下载浏览“" + name + "”</a>','" + frmelement + "');");
                           // Response.Write("parent.CKEDITOR.instances." + frmelement + ".insertHtml('<a href=\"" + Application["upload_server_url"] + msg + "\" target=\"_blank\">'+filename+'</a>');");
                            Response.Write("location.href=\"" + Application["upload_server_url"] + "Public/FileUpload.aspx?App=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["upload_server_return_admin"] + "\";</script>");
                            break;

                    }
                    Response.Write("<span class=\"up_success\">上传成功！</span>");
                }
                break;
        }
    %>
</body>
</html>
