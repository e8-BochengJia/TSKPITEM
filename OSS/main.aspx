<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Article myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        myApp = new Article();

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<style type="text/css">
.list_head_bg {text-align:right; width:110px;}
.list_td_bg {text-align:left;}
.list_td_bg a{ font-size:16px; font-weight:bold; color:#f90;}
</style>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="5" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">我的桌面</td>
    </tr>
    <tr>
      <td class="content_content">
        <div style="margin:5px; font-weight:bold;">文章管理</div>
                <table width="100%" class="list_table_bg" cellspacing="1" cellpadding="5">
                    <tr>
                        <td class="list_head_bg">待审核</td>
                        <td class="list_td_bg"><a href="/Article/article_list.aspx?IsAudit=1"><%=myApp.GetArticleCount(1) %>个</a></td>
                        <td class="list_head_bg">审核不通过</td>
                         <td class="list_td_bg"><a href="/Article/article_list.aspx?IsAudit=4"><%=myApp.GetArticleCount(2) %>个</a></td>
                    </tr>
            
                </table>
      </td>
    </tr>
  </table>
</div>
</body>
</html>
