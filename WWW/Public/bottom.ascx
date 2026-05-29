<%@ Control Language="C#" ClassName="bottom" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%
    CMS cms = new CMS();
    ITools tools = ToolsFactory.CreateTools();
%>
<!--底部 开始-->
<!-- 底部 -->
<script type="text/javascript">
    function openlink() {

        if ($('#select_name option:selected').val() != null || $('#select_name option:selected').val() != "") {
            window.location.href = $('#select_name option:selected').val();
        }

    }
</script>
<div class="bottom-box">
    <div class="bottom-center clearfix">
        <div class="bottom-left">
            <p><a href="/">首页</a><%=cms.Bottom_About() %></p>
            <p>唐山市科学技术协会主办 Copyright © 2018-2020 唐山科普在线 版权所有 </p>
            <p>未经授权禁止复制或建立镜像 | <a href="http://beian.miit.gov.cn" target="_blank" style="font-weight: 100">冀ICP备05016301号-1 </a>| 技术支持：Glaer</p>
            <p><a href="http://www.12321.cn" target="_blank">
                <img alt="网络不良与垃圾信息举报中心" src="/images/12321.jpg"></a> | <a href="http://www.cyberpolice.cn" target="_blank" style="font-weight: 100">
                    <img alt="公安机关备案号" src="/images/cyberPoliceGif_02.gif">公安机关备案号: 13020002152308</a> | <a href="http://bszs.conac.cn/sitename?method=show&id=54A5B4CF50F423EBE053022819ACB7AB" target="_blank">
                        <img alt="" src="/images/red.png"></a></p>
        </div>
        <div class="bottom-link">
            <p>友情链接</p>
            <%=cms.Home_FriendlyLink(1) %>
        </div>
        <ul class="bottom-ewm">
            <li>
                <img src="/images/pic-ewm.jpg">
                <p>唐山微科普</p>
            </li>
            <li>
                <img src="/images/kpggh.jpg" width="92" height="92">
                <p>科普微信公共号</p>
            </li>
            <li>
                <img src="/images/kpapp.jpg" width="92" height="92">
                <p>科普中国APP</p>

            </li>
        </ul>
    </div>
</div>
<!--底部 结束-->
