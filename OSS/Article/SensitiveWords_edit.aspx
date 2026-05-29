<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private SensitiveWords myApp;
    private ITools tools;

    private string Name;
    private int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("36d9082b-e75e-4079-818f-a7b4c9a7cc31");

        myApp = new SensitiveWords();
    tools = ToolsFactory.CreateTools();
    
    ID = tools.CheckInt(Request.QueryString["ID"]);
    SensitiveWordsInfo entity = myApp.GetSensitiveWordsByID(ID);
    if (entity == null) {
        Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    } else {
        ID = entity.ID;
Name = entity.Name;

    }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">敏感词添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="SensitiveWords_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">敏感词</td>
          <td class="cell_content"><input name="Name" type="text" id="Name" size="50" maxlength="100" value="<%=Name %>" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
                <input type="hidden" id="ID" name="ID" value="<% =ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'SensitiveWords_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>