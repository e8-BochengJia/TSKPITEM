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
    Question question = new Question();
    int Vote_ID = tools.CheckInt(Request["Vote_ID"]);
    VoteInfo entity = question.GetVoteByID(Vote_ID);
    IList<VoteSelectInfo> voteselects = null;
    if (entity != null)
    {
        if (entity.Vote_IsActive == 1 && DateTime.Compare(entity.Vote_Start, DateTime.Today) <= 0 && DateTime.Compare(entity.Vote_End, DateTime.Today) >= 0)
        {
            voteselects = question.GetVoteSelectByVoteID(Vote_ID);
        }
        else
        {
            Response.Redirect("/index.aspx");
        }
    }
    else
    {
        Response.Redirect("/index.aspx");
    }
    
%>

<!DOCTYPE html>
<html>
<head style="background: #f5f5fa;">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Application["site_name"].ToString()%>-投票</title>
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="Keywords" content="<%=Application["site_name"].ToString()%>-投票" />
    <meta name="Description" content="<%=Application["site_name"].ToString()%>-投票" />

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
    
    .vote-center{
      width: 1200px;
      margin:20px auto;
    }
    .vote-center h2{
      font-size: 18px;
      line-height: 50px;
    }
    .vote-center dl{
      padding:20px 0;
      border-top: 1px solid #ddd;
      border-bottom: 1px solid #ddd;
    }
    .vote-center dl dt{
      font-size: 16px;
      line-height: 40px;
    }
    .vote-center dl dd{
      font-size: 14px;
      line-height: 40px;
    }
    .vote-center dl dd input{
      margin-right: 10px;
      vertical-align: middle;
    }
     .vote-btn{
      margin-top: 30px;
     }
    .vote-btn a{
      display: inline-block;
      width: 180px;
      height: 44px;
      text-align: center;
      line-height: 44px;
      font-size: 16px;
      border-radius: 3px;
       background: #0066e7;
    color: #fff; 
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
    <uc1:topcate ID="top" runat="server" />
    <!-- 列表 -->
    <p class="crumb-nav">
        <img src="/images/icon-nav.png">当前位置 > <a href="/">首页 </a>>投票结果</p>
    <div class="w-1200 clearfix" style="margin-bottom: 40px;">

        <div class="news-details">
            <form method="post" name="frm_memeber" id="frm_memeber">


                <div class="vote-center">
        <h2 style="text-align:center;">感谢您的参与！</h2>
<dl>
    <dt><%=entity.Vote_Name %></dt>
      <table  style="width:500px;margin-top:10px; ">
                                    
                                        <%
                                            if (voteselects != null)
                                            {
                                                foreach (VoteSelectInfo one in voteselects)
                                                {
                                                    double mm;
                                                    if (entity.Vote_Number > 0)
                                                    {
                                                        mm = Math.Round(tools.NullDbl(one.Vote_Select_Number) / tools.NullDbl(entity.Vote_Number) * 100, 2);
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
    </dl>
    </div>

            </form>

        </div>

    </div>
    <uc1:bottom ID="bottom" runat="server" />
</body>
</html>

