<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["selected_memberid"] = "";
        Public.CheckLogin("833b9bdd-a344-407b-b23a-671348d57f76");
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">会员积分处理</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="coin_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
         <tr>
          <td class="cell_title">用户</td>
          
          <td class="cell_content"><%--<input name="Member_Name" type="text" id="Member_Name" style="width:200px;" maxlength="30" />--%>
            <input type="radio" name="favor_memberall" id="favor_memberall1" value="1" onclick="inimember(1);" checked/>所有用户 
          <input type="radio" name="favor_memberall" id="favor_memberall0" value="0" onclick="iniproduct(0);"/>指定用户  <a href="" id="btn_member"><input type="button" value="选择" class="bt_orange"/></a><input type="hidden" name="favor_memberid" id="favor_memberid" /> <span id="tip_favor_memberid"></span>
                  <div class="div_picker" id="member_picker"><span class="pickertip">全部会员</span></div>
          
          </td>
        </tr>
        <tr>
          <td class="cell_title">积分</td>
          <td class="cell_content"><input name="coin_amount" type="text" id="coin_amount" style="width:60px;" value="0" /> <span class="tip"></span></td>
        </tr>
        <tr>
          <td class="cell_title">备注</td>
          <td class="cell_content"><input name="coin_reason" type="text" id="coin_reason" style="width:400px;" maxlength="30" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="coin_process" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'coin_detail.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
<script type="text/javascript">
    $("#btn_member").click(function () {
        $("#btn_member").attr("href", "member_check.aspx?memberid=" + $("#favor_memberid").val() + "&timer=" + Math.random());
    });
    $("#btn_member").zxxbox({ height: 600, width: 600, title: '', bar: false, btnclose: false });
</script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
</body>
</html>