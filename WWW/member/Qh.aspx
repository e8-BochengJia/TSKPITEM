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
    int ID = tools.CheckInt(Request["ID"]);
    int type = 2;
    if (ID == 0)
    {
        Response.Redirect("/member/m_coin.aspx?type=2");
    }
    Member MEM = new Member();

    MEM.Member_Login_Check("/member/Qh.aspx");
    MemberInfo memberinfo = MEM.GetMemberByID();

    if (memberinfo == null)
    {
        Response.Redirect("/member/login.aspx?action=logout");
    }
    Question question = new Question();
    QuestionHistoryInfo qhistory = question.GetQuestionsHistoryByID(ID);
    if (qhistory == null)
    {
        Response.Redirect("/member/m_coin.aspx?type=2");
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
        /*a:hover
        {
            text-decoration: underline;
        }*/

        .add-product-centre ul li > span
        {
            width: 100px!important;
        }

        .mem-center-list2 .lsit2-tit ul li.active
        {
            color: black;
            border-bottom: 0px solid #338fff;
        }
        .mem-center-list2 .lsit2-tit ul li
        {
            line-height:25px;
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
                    url: "/member/member_do.aspx?action=Question_Save&qhID=<%=ID%>",
                    data: $("#frm_memeber").serialize(),
                    dataType: "json",
                    success: function (data) {
                        if (data.err != "") {

                            layer.alert(data.err, { icon: 2 });
                        } else {
                            layer.alert(data.url, function () { window.location = '/member/m_coin.aspx?type=2' });
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

    <uctop1:top ID="top1" runat="server" />

    <div class="meber-content clearfix">

        <div class="mem-center clearfix">
            <!-- 左侧 -->
            <%MEM.Get_Member_Left_HTML(0, type + 7); %>
            <!-- 右侧 -->
            <div class="mem-center-right">

                <div class="mem-center-list2">
                    <div class="lsit2-tit">
                        <span>会员专享</span>

                        <ul>
                            <li style="font-size: 14px; cursor: initial; color: red">竞答规则：竞赛试题为选择题，每周一套竞赛题，每套题10道，每答对一题可获得10分积分。准备好了吗？<br />
                                开始您的科普竞答之旅吧！</li>

                        </ul>

                    </div>
                    <div class="right-news-910" style="width: 960px;background: #fff;margin-top:10px;border-top:1px solid #ddd;">
                          <form  method="post" name="frm_memeber" id="frm_memeber">
                        <div class="answer-list">
                            <h3>第<%=ID %>套题</h3>
                            <%=question.GetQuestionsHtml(qhistory) %>
                           
                        </div>
                        <div class="add-product-btn"><a class="add-product-btn-left" href="javascript:void(0);" style="margin:0 30px;"  id="summit_date">保存</a></div>
                             
                                </form>
                    </div>
                

                </div>

            </div>
        </div>

    </div>
    <script>
        $(function () {
            $('.answer-list dl dd').on('click', function () {
                $(this).addClass('active').siblings('dd').removeClass('active');
                var v = $(this).attr("type");
                var bind = $(this).attr("bind");
                $("#q_" + v).val(bind);
            });
        });
</script>
    <ucbottom:bottom ID="bottom1" runat="server" />

</body>
</html>
