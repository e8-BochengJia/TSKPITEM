<%@ Control Language="C#" ClassName="topm" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>



<!--头部 开始-->
<%
    ITools tools = ToolsFactory.CreateTools();
     string Cur_Position = tools.NullStr(Session["Cur_Position"]);
     
     %>
<%
    if (DateTime.Today == tools.NullDate("2020-07-28"))
    {
        Response.Write("<style>");
        Response.Write("html{filter: grayscale(100%); -webkit-filter: grayscale(100%); -moz-filter: grayscale(100%); -ms-filter: grayscale(100%); -o-filter: grayscale(100%); filter:progid:DXImageTransform.Microsoft.BasicImage(grayscale=1);}");
        Response.Write("</style>");
    }
%>
<div class="mem_head_logo">
      <div class="head_logo_content">
          <div class="head_logo_left">
             <a href="/"> <img src="/images/logo4.png"></a>
           </div>
           <ul class="head_logo_list">
                <li class="<%=(Cur_Position=="index"?"active":"") %>"><a href="/member/index.aspx">会员中心首页</a></li>
                <li class="<%=(Cur_Position=="info"?"active":"") %>"><a href="/member/m_info.aspx">个人资料</a></li>
                <li class="<%=(Cur_Position=="password"?"active":"") %>"><a href="/member/m_password.aspx">修改密码</a></li>
           </ul>
          <div class="head_logo_right">
            <p><span></span><a href="/member/login_do.aspx?action=logout">退出登录</a></p>
          </div>
      </div>
  </div>
<!--头部 结束-->

<script>
    $(function () {
        $('.head_logo_list li').on('click', function () {
            $(this).addClass('active').siblings('li').removeClass('active');
        });
        $('.lsit2-tit ul li').on('click', function () {
            $(this).addClass('active').siblings('li').removeClass('active');
        });
    });
</script> 

