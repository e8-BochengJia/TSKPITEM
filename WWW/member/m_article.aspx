<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/topm.ascx" TagPrefix="uctop1" TagName="top" %>
<%@ Register Src="~/Public/Bottom.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Session["Cur_Position"] = "index";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    int type = 1;
    
    Member MEM = new Member();

    MEM.Member_Login_Check("/member/m_article.aspx");
    MemberInfo memberinfo = MEM.GetMemberByID();
    if (memberinfo == null)
    {
        Response.Redirect("/member/login.aspx?action=logout");
    }
   
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="会员中心 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index.css"))%>" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>
    <link href="/css/member_center.css" rel="stylesheet" type="text/css" />
    <link href="/layjs/css/layui.css" rel="stylesheet" />
    <script type="text/javascript" src="/layjs/layui.js"></script>
    <style type="text/css">
        #changetype a:hover
        {
            text-decoration: underline;
        }
        .add-product-centre ul li > span
        {
            width: 100px!important;
        }
        .add-product-btn>a {
    margin-top: 0px;
    display: inline-block;
    width: 100px;
    height: 40px;
    border-radius: 3px;
    font-size: 16px;
    text-align: center;
    line-height: 40px;
    color:#fff!important;
}
    </style>
    <script type="text/javascript">
        layui.use('layer', function () {
            var layer = layui.layer;
        });
        function move(obj) {
            layer.confirm("撤销后，将不可复原！是否继续？", {
                btn: ["继续", "取消"], icon: 0, shade: 0.1
            }, function () {
                $.ajax({
                    type: "get",
                    global: false,
                    async: false,
                    dataType: "html",
                    url: encodeURI("/member/member_do.aspx?action=move_article&id=" + obj + "&timer=" + Math.random()),
                    success: function (data) {
                        if (data == "success") {
                            layer.alert('操作成功', 
function () { window.location = '/member/m_article.aspx' }
);
                        }
                        else {
                            layer.alert(data, {
                                skin: 'layui-layer-molv'
, closeBtn: 0
                            });
                        }
                    },
                    error: function () {
                        layer.msg("请求错误，请稍后重试", {
                            icon: 2, shade: 0.1
                        });
                    }

                })
            }
            , function () { layer.close(); });
        }
        $(function () {
            ChangeShow(<%=type%>, 1);
           


         });
        function ChangeShow(index, value) {

            $("#changetype").load("/member/member_do.aspx?action=article_list&type=" + index + "&page=" + value + "&timer=" + Math.random());


        }
    </script>

</head>
<body style="background: #f5f5f9;">

    <uctop1:top ID="top1" runat="server" />

    <div class="meber-content clearfix">

        <div class="mem-center clearfix">
            <!-- 左侧 -->
            <%MEM.Get_Member_Left_HTML(0, 4); %>
            <!-- 右侧 -->
            <div class="mem-center-right">

                <div class="mem-center-list2">
                    <div class="lsit2-tit">
                        <span>我的投稿</span>
                        <ul>
                       
                            <li ></li>
                          
                        </ul>
                        <div class="add-product-btn" style="margin-top:18px; float:right;"><a href="add_article.aspx" style="text-decoration:none;" class="add-product-btn-left">发表/投稿</a></div>
                    </div>
                    <div id="changetype">
                   
                    </div>

                </div>
               
            </div>
        </div>

    </div>
    <ucbottom:bottom ID="bottom1" runat="server" />

</body>
</html>
