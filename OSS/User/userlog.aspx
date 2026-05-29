<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%  Public.CheckLogin("6b6a4750-6130-4a7d-9076-c7c97f4f5398");
    Sys MyApp = new Sys();
     
    ITools tools;
    tools =ToolsFactory.CreateTools();
    string date_start, date_end, VisitURL, keyword, defaultkey;
    int log_channel = tools.CheckInt(Request["channel"]);
    VisitURL = "";
    keyword = tools.CheckStr(Request["keyword"]);
    if (keyword != "输入用户、操作说明进行搜索" && keyword != null)
    {
        defaultkey=keyword;
    }
    else
    {
        keyword = "";
    }
    if (keyword == "")
    {
        defaultkey = "输入用户、操作说明进行搜索";
    }
    else
    {
        defaultkey = keyword;
    }
    VisitURL += "&keyword=" + Server.UrlEncode(keyword);

    VisitURL += "&channel=" + log_channel;
    //开始时间
    date_start = tools.CheckStr(Request["date_start"]);
    VisitURL += "&date_start=" + date_start;

    //结束时间
    date_end = tools.CheckStr(Request["date_end"]);
    VisitURL += "&date_end=" + date_end;
    %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>系统日志管理</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">系统日志管理</td>
    </tr>
    <tr>
      <td class="content_content" align="right">
      <form id="frmsearch" method="get" action="?">
        <table>
            <tr>
                <td>
                <span class="left_nav"></span>
                分类：<select name="channel" id="channel">
                <option value="0">全部</option>
                <%=MyApp.GetUserLogChannelOption(log_channel)%>
                </select> 
                操作时间：
                                <input type="text" class="input_calendar" name="date_start" id="date_start" maxlength="10"
                                    readonly="readonly" value="<%=date_start %>" />
                                -
                                <input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10"
                                    readonly="readonly" value="<%=date_end%>" />
                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        $("#date_start").datepicker({ numberOfMonths: 1 });
                                        $("#date_end").datepicker({ numberOfMonths: 1 });
                                    });
                                </script>
                                <input type="text" name="keyword" id="keyword" size="70" onfocus="if(this.value=='输入用户、操作说明进行搜索'){this.value='';}"
                        value="<% =defaultkey %>">
                </td>
                <td><input name="save" type="submit" class="bt_orange" value="搜索" /></td>
            </tr>
        </table>
        </form>
        
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
            jQuery("#list").jqGrid({
                url: 'user_do.aspx?action=loglist2<%=VisitURL %>',
                datatype: "json",
                colNames: ['时间', "用户", "目标编号", "目标名称", "操作说明", "操作结果","IP"],
                colModel: [
				{ width: 100, name: 'RBACUserLogInfo.Log_ID', index: 'RBACUserLogInfo.Log_ID', align: 'center' },
				{ align: 'center', width: 100, name: 'RBACUserLogInfo.Log_UserID', index: 'RBACUserLogInfo.Log_UserID' },
				{ width: 60, align: 'center', name: 'RBACUserLogInfo.Log_User_ObjectID', index: 'RBACUserLogInfo.Log_User_ObjectID' },
				{ name: 'RBACUserLogInfo.Log_Description', index: 'RBACUserLogInfo.Log_Description' },
				{ width: 100, align: 'center', name: 'RBACUserLogInfo.Log_Action', index: 'RBACUserLogInfo.Log_Action' },
                { width: 100, align: 'center', name: 'RBACUserLogInfo.Log_Result', index: 'RBACUserLogInfo.Log_Result' },
				{ width: 100, align: 'center', name: 'RBACUserLogInfo.Log_IP', index: 'RBACUserLogInfo.Log_IP' },
			],
                sortname: 'RBACUserLogInfo.Log_ID',
                sortorder: "desc",
                rowNum: 10,
                rowList: [10, 20, 40],
                pager: 'pager',
                multiselect: false,
                viewsortcols: [false, 'horizontal', true],
                width: getTotalWidth() - 35,
                height: "100%"
            });
        </script>
      </td>
    </tr>
  </table>
</div>
</body>
</html>
