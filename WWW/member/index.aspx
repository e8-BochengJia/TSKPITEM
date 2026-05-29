<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/topm.ascx" TagPrefix="uctop1" TagName="top" %>
<%@ Register Src="~/Public/Bottom.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Session["Cur_Position"] = "index";
    Session["mifno"] = "";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();

    Member MEM = new Member();

    MEM.Member_Login_Check("/member/index.aspx");
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
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.9.1.js"></script>
    <link href="/css/member_center.css" rel="stylesheet" type="text/css" />

    <link href="/layjs/css/layui.css" rel="stylesheet" />
    <script type="text/javascript" src="/layjs/layui.js"></script>
    <script type="text/javascript">
        layui.use('layer', function () {
            var layer = layui.layer;
        });
        $(function () {
          
            ChangeShowAticle(1,5);
            ChangeShow(1, 3);
        });
        function ChangeShow(index, value) {

            $("#m_coin").load("/member/member_do.aspx?action=Coin_index&type=" + index + "&count=" + value + "&timer=" + Math.random());


        }
        function ChangeShowAticle(index, value) {

            $("#article_div").load("/member/member_do.aspx?action=article_index&type=" + index + "&count=" + value + "&timer=" + Math.random());


        }

    </script>



</head>
<body style="background: #f5f5f9;">

    <uctop1:top ID="top1" runat="server" />

    <div class="meber-content clearfix">

        <div class="mem-center clearfix">
            <!-- 左侧 -->
            <%MEM.Get_Member_Left_HTML(0, 0); %>
            <!-- 右侧 -->
            <div class="mem-center-right">
                <div class="center-right-name clearfix">
                    <div class="name-head">
                        <dl class="clearfix">
                            <%--<dt>
                              <img src="/images/name.png">
                              <a href="#">修改头像</a>
                            </dt>--%>
                            <dd>
                                <b>昵称：<%=Session["member_nickname"] %></b>
                                <p>会员类别：<%=gradename %></p>
                                <p>上次登录时间：<%=DateTime.Parse(Session["member_lastlogin_time"].ToString()).ToString("yyyy-MM-dd HH:mm") %></p>
                            </dd>
                        </dl>
                    </div>
                    <!-- center -->
                    <ul class="name-head-date">
                       
                        <%--  <li>
                            <span>27 <i>次</i></span>
                            <p>我的分享</p>
                        </li>--%>
                        <li>
                            <span><a href="m_coin.aspx?type=2" style="color: #338fff;"><%=MEM.GetQuestionH() %></a> <i>次</i></span>
                            <p>我的答题</p>
                        </li>
                        <li>
                            <span><a href="m_coin.aspx?type=3" style="color: #338fff;"><%=MEM.GetVoteCount() %></a> <i>次</i></span>
                            <p>我的投票</p>
                        </li>
                         <li>
                            <b><a href="m_coin.aspx?type=1" style="color: #de3838;"><%=memberinfo.Member_CoinRemain %> </a><i>分</i></b>
                            <p>我的积分</p>
                        </li>
                        <%--  <li>
                            <b>27 <i>篇</i></b>
                            <p>收藏文章</p>
                        </li>
                        <li>
                            <b>27 <i>篇</i></b>
                            <p>转载文章</p>
                        </li>
                              <li>
                            <b>27 <i>篇</i></b>
                            <p>科普作品</p>
                        </li>
                            --%>
                        <li>
                            <b><a href="m_article.aspx" style="color: #de3838;"><%=MEM.GetMArticle() %></a> <i>篇</i></b>
                            <p>原创文章</p>
                        </li>
                      
                    </ul>
                    <!-- 右侧 -->
                    <%if (entity.Member_Grade_ID == 1)
                      {%>
                    <div class="name-head-btn">
                        <p>您已是<%=gradename %>，您可以申请加入科普学会！</p>
                        <span>入会前请阅读<a href="/about/index.aspx?sign=m_notice" target="_blank">《入会须知》</a></span>
                        <a href="#" class="btn">申请入会</a>
                    </div>
                    <%} else{%>
                      <div class="name-head-btn">
                        <p>您已是网站会员，您可以申请加入科普学会！</p>
                        <span>入会前请阅读<a href="/about/index.aspx?sign=m_notice" target="_blank">《入会须知》</a></span>
                        <a href="#" class="btn">申请入会</a>
                    </div>
                    <%} %>
                </div>
                <!-- 2层 -->
                <div class="mem-center-list2">
                    <div class="lsit2-tit">
                        <span>我的投稿 </span>
                        <ul>
                          <%--  <li class="active" onclick="ChangeShowAticle(1,5)">佳作推荐</li>--%>
                            <li class="active" onclick="ChangeShowAticle(1,5)">科普原创</li>
                  <%--          <li onclick="ChangeShowAticle(1,5)">科普作品</li>--%>
                        </ul>
                        <a href="m_article.aspx">全部 ></a>
                    </div>
                    <div id="article_div">


                    </div>
                 
                </div>
                <!-- 3 -->

                <%--<div class="mem-center-list2">
                    <div class="lsit2-tit">
                        <span>收藏分享 </span>
                        <ul>
                            <li class="active">我的收藏</li>
                            <li>我的分享</li>
                        </ul>
                        <a href="#">全部转载 ></a>
                    </div>
                    <table width="960" cellspacing="0" style="width: 960px;">
                        <thead>
                            <tr>
                                <th width="90">序号</th>
                                <th width="400">文章标题</th>
                                <th width="130">转载时间 </th>
                                <th width="210">来源  </th>
                                <th width="130">操作</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>01</td>
                                <td>青少年科普与时俱进 从“小发明”到“科技大赛”</td>
                                <td>2019-08-01 </td>
                                <td>中国科普网</td>
                                <td class="more"><a href="#">查看</a> / <a href="#">撤销</a></td>
                            </tr>
                            <tr>
                                <td>02</td>
                                <td>青少年科普与时俱进 从“小发明”到“科技大赛”</td>
                                <td>2019-08-01 </td>
                                <td>中国科普网</td>
                                <td class="more"><a href="#">查看</a> / <a href="#">撤销</a></td>
                            </tr>


                        </tbody>
                    </table>
                </div>--%>
                <!-- 4 -->
                <div class="mem-center-list2">
                    <div class="lsit2-tit">
                        <span>会员专享</span>
                        <ul>
                            <li class="active" onclick="ChangeShow(1,3)">我的积分 </li>
                            <li onclick="ChangeShow(2,3)">我的答题</li>
                            <li onclick="ChangeShow(3,3)">我的投票</li>
                        </ul>
                        <a href="m_coin.aspx">全部 ></a>
                    </div>
                    <div id="m_coin">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <ucbottom:bottom ID="bottom1" runat="server" />

</body>
</html>
