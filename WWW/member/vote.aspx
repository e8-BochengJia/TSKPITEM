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
        $(function () {

            $("#summit_date").click(function () {

                $.ajax({
                    type: "GET",
                    url: "/member/member_do.aspx?action=addvote_m&Vote_ID=<%=Vote_ID%>",
                    data: $("#frm_memeber").serialize(),
                    dataType: "json",
                    success: function (data) {
                        if (data.err != "") {

                            layer.alert(data.err, { icon: 9 });
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
        <img src="/images/icon-nav.png">当前位置 > <a href="/">首页 </a>>投票</p>
    <div class="w-1200 clearfix" style="margin-bottom: 40px;">

        <div class="news-details">
            <form method="post" name="frm_memeber" id="frm_memeber">


                <div class="vote-center">
        <h2><%=entity.Vote_Name %></h2>
        <dl>
      <%if (voteselects != null)
                            {

                                if (entity.Vote_Remarks != "")
                                {
                                    Response.Write(" <dt>" + entity.Vote_Remarks + "</dt>");
                                }
                                    foreach (VoteSelectInfo one in voteselects)
                                    {
                                        Response.Write("<dd><input type='radio' name='vote' value='" + one.Vote_Select_ID + "'>" + one.Vote_Select_Name + "</dd>");
                                      
                                    }

                                    Response.Write("<input type=\"hidden\" name=\"Vote_Select_VoteID\" id=\"Vote_Select_VoteID\"  value=\"0\" />");
                             
                               
                            } %>
          
        </dl>
        <div class="vote-btn"><a id="summit_date" href="javascript:voide(0);">投票</a></div>
    </div>

            </form>

        </div>

    </div>
    <uc1:bottom ID="bottom" runat="server" />
</body>
</html>

