<%@ Page Language="C#" %>
<% 

    Config config = new Config();
    config.Sys_UpdateApplication();
    %>
<script type="text/javascript">
	alert('系统缓存已更新！');
	window.close();
</script>