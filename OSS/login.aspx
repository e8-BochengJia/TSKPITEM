<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%
    String msg;
    switch (Request.QueryString["tip"])
    {
        case "ErrorVerifyCode":
            msg = "您输入的验证码不正确，请重新输入";
            break;
        case "ErrorInfo":
            msg = "用户名和密码不正确";
            break;
        case "nologin":
            msg = "登陆超时或未登录!";
            break;
        default:
            msg = "请保护好您的密码并定期更改密码";
            break;
    }


    //Glaer.Trade.Util.SQLHelper.ISQLHelper db = Glaer.Trade.Util.SQLHelper.SQLHelperFactory.CreateSQLHelper();
    ////Response.Write(db.ExecuteNonQuery("DELETE FROM Promotion_Favor_Coupon WHERE Promotion_Coupon_ID = 169"));
    //Response.Write(db.ExecuteNonQuery("insert into Promotion_Favor_Coupon ( Promotion_Coupon_Title) values ('ceshi')"));
    Session.Abandon();
    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
%>

<!DOCTYPE html>
<html style="height: 100%">
<head>
    <title>Trade OSS 电子商务平台运营支撑系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/css/animate.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js"></script>
    <script>
        function Check_login()
        {
            var r = false;
            $.ajax({
                type: "GET",
                url: "logindo.aspx?action=update",//url
                dataType: "html",
                success: function (data) {
                    if(data=="true")
                    {

                        r= true;
                    }
                    else
                    {

                        r= false;
                    }
                }
            });
            return r;
        }
    </script>
</head>
<body class="login">
    <table width="100%" border="0" style="height: 100%">
        <tr>
            <td style="height: 100%" align="center" valign="middle">
                <form method="post" name="frm_login" id="frm_login" action="/logindo.aspx">
                    <table width="400" border="0" cellspacing="0" cellpadding="0" class="table shadow">
                        <tbody>
                            <tr>
                                <td colspan="2" class="title">
                                    <h1>TSKP OSS 登录</h1>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <h2 class="animated shake" id="msg"><%=msg%></h2>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <input name="username" type="text" id="username" size="30" maxlength="20" placeholder="用户名" value="<%=Server.UrlDecode(Request.Cookies["username"].Value)%>" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <input name="password" type="password" id="password" size="30" maxlength="20" placeholder="密码" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <input type="text" name="verifycode" id="verifycode" maxlength="6" size="22" placeholder="验证码" />
                                    <img src="/public/verifycode.aspx" align="absmiddle" height="32" width="65">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <input type="hidden" name="action" value="login" />
                                    <input type="submit" value="登录" class="bt_login" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left:60px;"><input name="userremember" type="checkbox" id="userremember" value="1" checked="checked" class="magic-checkbox" /> <label for="userremember" style="font-size: 14px;line-height:20px;">记住用户名</label></td>
                            </tr>
                            <tr>
                                <td align="center" style="height:10px"></td>
                            </tr>
                        </tbody>
                    </table>
                </form>
            </td>
        </tr>
    </table>
</body>
</html>
