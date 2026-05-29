<%@ Page Language="C#" %>

<%@ Register Src="~/Public/topcate.ascx" TagName="topcate" TagPrefix="uc1" %>
<%@ Register Src="~/Public/bottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();

    Session["CurrentNav"] = "-1";
    Session["mifno"] ="";
    
%>

<!DOCTYPE html>
<html>
<head style="background: #f5f5fa;">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Application["site_name"].ToString()%>-找回密码</title>
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="Keywords" content="<%=Application["site_name"].ToString()%>-找回密码" />
    <meta name="Description" content="<%=Application["site_name"].ToString()%>-找回密码" />

    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index.css"))%>" rel="stylesheet">

    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>
    <link href="/css/member_center.css" rel="stylesheet" type="text/css" />
    <link href="/layjs/css/layui.css" rel="stylesheet" />
      <script type="text/javascript" src="/layjs/layui.js"></script>
    <style type="text/css">
        .add-product-centre ul li > span
        {
            width: 105px;
        }

        .add-product-btn-left
        {
            margin: 0 30px 0 110px;
        }
    </style>
    <script type="text/javascript">
        layui.use('layer', function () {
            var layer = layui.layer;
        });
        $(function () {

            $("#summit_date").click(function () {

                $.ajax({
                    type: "GET",
                    url: "/member/member_do.aspx?action=findpassword",
                    data: $("#frm_memeber").serialize(),
                    dataType: "json",
                    success: function (data) {
                        if (data.err != "") {

                            layer.alert(data.err, { icon: 2 });
                        } else {
                            location.href = data.url; 
                        }
                    }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                        layer.alert(textStatus);

                    }
                });
            });
        });

    </script>
</head>

<body style="background: #f5f5f9;">
    <uc1:topcate ID="top" runat="server" />
    <!-- 列表 -->
    <p class="crumb-nav">
        <img src="/images/icon-nav.png">当前位置 > <a href="/">首页 </a>>找回密码</p>
    <div class="w-1200 clearfix" style="margin-bottom: 40px;">

        <div class="news-details">
            <form method="post" name="frm_memeber" id="frm_memeber">
                <div class="add-product-centre clearfix" style=" margin-top: 10px; width: 960px">

                    <ul style="margin-top: 20px;">
                      
                           <li>
                            <span>登录账号：</span>


                            <input type="text" name="member_name" placeholder="请填写昵称/手机号" value="" /><i style="color: red">*</i>
                        </li>
                        <li>
                            <span>找回密码问题：</span>


                            <input type="text" name="U_Member_Question" placeholder="请填写已设置的问题" value="" /><i style="color: red">*</i>
                        </li>
                        <li>
                            <span>找回密码答案：</span>
                            <input type="text" name="U_Member_Answer" placeholder="请填写已设置的答案" value="" /><i style="color: red">*</i>
                        </li>
                    </ul>

                </div>
                <div class="add-product-btn">
                    <a href="javascript:void(0);" id="summit_date" class="add-product-btn-left">下一步</a>

                </div>
            </form>

        </div>

    </div>
    <uc1:bottom ID="bottom" runat="server" />
</body>
</html>

