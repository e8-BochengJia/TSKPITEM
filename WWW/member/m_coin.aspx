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
    int type = tools.CheckInt(Request["type"]);
    if (type <= 0 || type > 3)
    {
        type = 1;
    }
    Member MEM = new Member();

    MEM.Member_Login_Check("/member/m_coin.aspx");
    MemberInfo memberinfo = MEM.GetMemberByID();
    if (memberinfo == null)
    {
        Response.Redirect("/member/login.aspx?action=logout");
    }
    string gradename = "网站会员";
    MemberGradeInfo entity = MEM.GetMemberGradeByMemberID();
    if (entity == null)
    {
        entity = new MemberGradeInfo();
        gradename = entity.Member_Grade_Name;
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
    </style>
    <script type="text/javascript">
        layui.use('layer', function () {
            var layer = layui.layer;
        });
        $(function () {
            ChangeShow(<%=type%>, 1);
           


        });
        function ChangeShow(index, value) {
          
            $("#changetype").load("/member/member_do.aspx?action=Coin&type=" + index + "&page=" + value + "&timer=" + Math.random());


        }

    </script>

</head>
<body style="background: #f5f5f9;">

    <uctop1:top ID="top1" runat="server" />

    <div class="meber-content clearfix">

        <div class="mem-center clearfix">
            <!-- 左侧 -->
            <%MEM.Get_Member_Left_HTML(0, type+7); %>
            <!-- 右侧 -->
            <div class="mem-center-right">

                <div class="mem-center-list2">
                    <div class="lsit2-tit">
                        <span>会员专享</span>
                        <ul>
                            <li onclick="ChangeShow(1,1)" <%=type==1?"class='active'":"" %>>我的积分 </li>
                            <li onclick="ChangeShow(2,1)" <%=type==2?"class='active'":"" %>>我的答题</li>
                            <li onclick="ChangeShow(3,1)" <%=type==3?"class='active'":"" %>>我的投票</li>
                        </ul>

                    </div>
                    <div id="changetype" >
                     
                    </div>

                </div>
               
            </div>
        </div>

    </div>
    <ucbottom:bottom ID="bottom1" runat="server" />

</body>
</html>
