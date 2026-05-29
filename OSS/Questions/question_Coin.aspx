<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

   
    Question myApp;
    ITools tools;

    int ID;


    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        tools = ToolsFactory.CreateTools();
        myApp = new Question();


        ID = tools.CheckInt(Request["ID"]);

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
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
    <table  width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="picker_tittab">
                    <tr>
                        <td class="picker_tit">答题记录</td>
                        <td width="30" align="center"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td class="content_content">


                <table id="list"></table>
                <div id="pager"></div>
               
                 <script type="text/javascript">
                     jQuery("#list").jqGrid({
                         url: 'question_do.aspx?action=QHCoin&ID=<%=ID%>',
                         datatype: "json",
                         colNames: ['用户名', '分数', "日期"],
                         colModel: [
                             { width: 30, align: 'center', name: 'name', index: 'name', sortable: false },
                { width: 30, align: 'center', name: 'MemberConsumptionInfo.Consump_Coin', index: 'MemberConsumptionInfo.Consump_Coin', sortable: false },
				{ width: 60, align: 'center', name: 'MemberConsumptionInfo.Consump_Addtime', index: 'MemberConsumptionInfo.Consump_Addtime', sortable: false }
                         ],
                         sortname: 'MemberConsumptionInfo.Consump_ID',
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
</body>
</html>
