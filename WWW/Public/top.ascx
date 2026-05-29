<%@ Control Language="C#" ClassName="top" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>


<%
    ITools tools = ToolsFactory.CreateTools();
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    int CurrentNav = tools.NullInt(Session["CurrentNav"]);
    string topkeyword =pub.CheckXSS(tools.NullStr(Request["keyword"]));


%>
<script type="text/javascript">
    $(function(){
        $("#dav_ul li").click(function () {
            $(this).siblings('li').removeClass('active');  // 删除其兄弟元素的样式
            $(this).addClass('active');                    // 为点击元素添加类名
        });
    });
</script>
<!--头部 开始-->
<%
    if(DateTime.Today==tools.NullDate("2020-07-28"))
    {
        Response.Write("<style>");
        Response.Write("html{filter: grayscale(100%); -webkit-filter: grayscale(100%); -moz-filter: grayscale(100%); -ms-filter: grayscale(100%); -o-filter: grayscale(100%); filter:progid:DXImageTransform.Microsoft.BasicImage(grayscale=1);}");
        Response.Write("</style>");
    }
%>
<div class="logo-head">
    <div class="logo-head-center">
        <img src="/images/logo1.png">
        <img src="/images/logo2.png">
        <div class="search-right">
            <p>唐山科协欢迎您，<a href="http://old.tskp.org.cn">旧版入口</a><%if(Session["logintype"] == "True") {%><a href="/member/index.aspx"><%=Session["member_nickname"] %></a><a href="/member/login_do.aspx?action=logout" >退出</a><%}else{ %>
                <a href="/member/login.aspx">请登录</a> <a href="/member/register.aspx">免费注册</a>
                <%} %></p>
            <form name="frm_search" id="frm_top_search" action="/search.aspx" method="post" >
            <input name="keyword" id="keyword" type="text" placeholder="输入名称关键字进行搜索" value="<%=topkeyword %>">
            <a href="javascript:void(0);" onclick="$('#frm_top_search').submit();" class="search-btn"><img src="/images/icon-s.png"></a>
                 </form>
        </div>
    </div>
</div>
<!--导航 开始-->
<div class="nav-list">
   <ul id="dav_ul">
      
         <%=cms.Home_Navigation() %>
    
   </ul>
</div>
<!--导航 结束-->
<!--头部 结束-->



