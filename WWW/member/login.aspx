
<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>


<%
    Public_Class pub = new Public_Class();
    AD ad = new AD();
    Session["CurrentNav"] = "0";
    Session["mifno"] = "";
    CMS cms = new CMS();
    ITools tools = ToolsFactory.CreateTools();
  
%>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <%= pub.SEO_TITLE()%></title>
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="keywords" content="<%=Application["site_keyword"].ToString()%>" />
    <meta name="description" content="<%=Application["site_description"].ToString()%>" />

    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index.css"))%>" rel="stylesheet">

    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>
    <script src="/layjs/layui.js" type="text/javascript"></script>
    <%
    if(DateTime.Today==tools.NullDate("2020-07-28"))
    {
        Response.Write("<style>");
        Response.Write("html{filter: grayscale(100%); -webkit-filter: grayscale(100%); -moz-filter: grayscale(100%); -ms-filter: grayscale(100%); -o-filter: grayscale(100%); filter:progid:DXImageTransform.Microsoft.BasicImage(grayscale=1);}");
        Response.Write("</style>");
    }
%>
    <style type="text/css">
        .but
        {
            display: inline-block;
            width: 400px;
            height: 46px;
            border-radius: 2px;
            background: #0062dd;
            text-align: center;
            line-height: 46px;
            color: #fff;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            border: 0px;
        }
        .cb
        {
                background-color: #1E9FFF;
        }
    </style>
    <script type="text/javascript">

        layui.use('layer', function () {
            var layer = layui.layer;
        });
        $(function () {

            $("#login").click(function () {
                $.ajax({
                    type: "GET",
                    url: "/member/login_do.aspx?action=login",
                    data: $("#logmember").serialize(),
                    dataType: "json",
                    success: function (data) {
                        if (data.err != "") {
                           
                       
                            layer.alert(data.err, { icon: 2});
                
                     
                        } else {
                            location.href = data.url;
                        }
                    }
                });
            });

        });
    </script>


</head>

<body style="background: #fff;">
    <div class="login_top">
        <div class="login_top_content">
            <div class="login_top_left">
                <img src="/images/logo1.png">
                <img src="/images/logo2.png">
                <span>欢迎登录</span>
            </div>
            <div class="login_top_right">
                <p>免费咨询电话 </p>
                <span><%=Application["Site_Tel"] %> </span>
            </div>
        </div>
    </div>
    <div class="login-content2">
        <div class="login-content2-box">

            <div class="login-box">
                <form id="logmember" method="post">
                    <h3>会员登录 <span>我不是会员，<a href="/member/register.aspx">立即注册</a></span></h3>
                    <ul>
                        <li>
                            <input type="text" name="member_name" id="member_name" placeholder="请输入昵称/手机号">
                        </li>
                        <li class="password-login">
                            <input type="password" name="member_password" id="member_password" placeholder="请输入密码">
                        </li>
                        <li class="yzm-login">
                            <input name="Trade_Verify" id="verifiyCode" type="text" placeholder="请输入验证码">
                            <img src="/public/verifycode.aspx" id="CodeImg" onclick="$('#CodeImg').attr('src','/Public/verifycode.aspx?timer='+Math.random());">
                        </li>


                    </ul>
                    <div class="forget-password clearfix">

                        <input name="chk_UserName" type="checkbox" id="chk_UserName" class="chk_1" checked="checked" value="1" />

                        记住密码
                <a href="/member/getpassword.aspx">忘记密码？</a>
                    </div>
                    <div class="login-btn" style="margin: 10px 0 0 0;">

                        <input id="login" type="button" class="but" value="立即登录" />

                    </div>
                    
                </form>
            </div>
        </div>
    </div>


    <!-- slogen -->

    <!-- 底部 -->
    <div class="bottom-box2">
        <div class="bottom-center2 clearfix">
            <div class="bottom-left">
                <p><a href="/">首页</a> <%=cms.Bottom_About() %></p>
                <p>唐山市科学技术协会主办 Copyright © 2008-2020 唐山科普在线 版权所有 </p>
                <p>未经授权禁止复制或建立镜像 | <a href="http://beian.miit.gov.cn" target="_blank" style="font-weight: 100">冀ICP备05016301号-1 </a>| 技术支持：Glaer</p>
            <p><a href="http://www.12321.cn" target="_blank">
                <img alt="网络不良与垃圾信息举报中心" src="/images/12321.jpg"></a> | <a href="http://www.cyberpolice.cn" target="_blank" style="font-weight: 100">
                    <img alt="公安机关备案号" src="/images/cyberPoliceGif_02.gif">公安机关备案号: 13020002152308</a> | <a href="http://bszs.conac.cn/sitename?method=show&id=54A5B4CF50F423EBE053022819ACB7AB" target="_blank">
                        <img alt="" src="/images/red.png"></a></p>
            </div>
            <div class="bottom-link">
                <p>友情链接</p>
                <%=cms.Home_FriendlyLink(1) %>
            </div>

        </div>
    </div>
</body>
</html>
