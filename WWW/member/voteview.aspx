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
    IList<VoteSelectInfo> voteselects = null;
    int ID = tools.CheckInt(Request["ID"]);
    int type = 3;
    if (ID==0)
    {
        Response.Redirect("/member/m_coin.aspx?type=3");
    }
    Member MEM = new Member();

    MEM.Member_Login_Check("/member/voteview.aspx");
    MemberInfo memberinfo = MEM.GetMemberByID();
    
    if (memberinfo == null)
    {
        Response.Redirect("/member/login.aspx?action=logout");
    }
    Question ques = new Question();
    VoteInfo voteinfo = ques.GetVoteByID(ID);
    if (voteinfo == null)
    {
        Response.Redirect("/member/m_coin.aspx?type=3");
    }
    voteselects = ques.GetVoteSelectByVoteID(ID);
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
        /*a:hover
        {
            text-decoration: underline;
        }*/
        .add-product-centre ul li > span
        {
            width: 100px!important;
        }
        .mem-center-list2 tbody tr td
        {
              border-right: 0px solid #eee; 
                   border-bottom:0px solid #eee; 
        }
        .mem-center-list2 .lsit2-tit ul li.active
        {
            color: black;
            border-bottom: 0px solid #338fff;
        }
       
    </style>
    <script type="text/javascript">
        layui.use('layer', function () {
            var layer = layui.layer;
        });
        layui.use('element', function () {
            var element = layui.element;
        });

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
                            <li style="font-size:14px; cursor: initial;"> 共有【 <%=voteinfo.Vote_Number %> 】人参加调查</li>
                          
                        </ul>

                    </div>
              
                          <div class="add-product-centre clearfix" style="border-top: 1px solid #ddd;margin-top:10px;width:960px;">
                        <ul style="margin-top:20px;width:960px">
                            <li>
                                <span>投票：</span>
                                <%=voteinfo.Vote_Name %>
                            </li>
                               <li>
                                <span>开始时间：</span>
                                <%=voteinfo.Vote_Start %>
                            </li>
                               <li>
                                <span>结束时间：</span>
                                <%=voteinfo.Vote_End %>
                            </li>
                             <li>
                                <span>详情：</span>
                                 <table  style="width:500px;margin: -40px 105px;border: 0px solid #eee; ">
                                    
                                        <%
                                            if (voteselects != null)
                                            {
                                                foreach (VoteSelectInfo one in voteselects)
                                                {
                                                    double mm;
                                                    if (voteinfo.Vote_Number > 0)
                                                    {
                                                        mm = Math.Round(tools.NullDbl(one.Vote_Select_Number) / tools.NullDbl(voteinfo.Vote_Number) * 100, 2);
                                                    }
                                                    else
                                                    {
                                                        mm = 0;
                                                    }

                                                    Response.Write("<tr>");
                                                    Response.Write("<td width=\"100px;\">" + one.Vote_Select_Name + "</td>");

                                                    Response.Write("<td>");
                                                    Response.Write("<p>" + one.Vote_Select_Number + "票</p>");

                                                    Response.Write("<div class=\"layui-progress layui-progress-big\"><div class=\"layui-progress-bar\" lay-percent=\""+mm+"%\"></div></div>");
                                                    Response.Write("</td>");
                                                    Response.Write("</tr>");
                                                }
                                            }
                                        %>

                                </table>
                            </li>
                            </ul></div>
             

                </div>
               
            </div>
        </div>

    </div>
    <ucbottom:bottom ID="bottom1" runat="server" />

</body>
</html>
