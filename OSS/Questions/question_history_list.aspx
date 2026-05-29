<%@ Page Language="C#"  ContentType="text/html" ResponseEncoding="utf-8"  %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<script runat="server">
    private ITools tools;
  
    private Question myAppC;
    //string keyword, defaultkey, ReqURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("0727f3b4-4edc-4e49-94a0-d728fe7d35ef");
        myAppC = new Question();
        tools = ToolsFactory.CreateTools();

        //defaultkey = "";
        //keyword = Request["keyword"];
      
        //if (keyword != "输入标题搜索" && keyword != null)
        //{
        //    keyword = keyword;
        //}
        //else
        //{
        //    keyword = "输入标题搜索";
        //}
        //if (keyword == "输入标题搜索")
        //{
        //    defaultkey = "";
        //}
        //else
        //{
        //    defaultkey = keyword;
        //}

        //ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&Q_Cate=" + Q_Cate;
        
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
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">套题管理</td>
    </tr>
    <tr><td height="5"></td></tr>
  
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
            jQuery("#list").jqGrid({
                url: 'question_do.aspx?action=list_history',
                datatype: "json",
                colNames: ['ID', '题目设置', "用户答题数", "添加时间", "操作"],
                colModel: [
				{ width: 40, name: 'QuestionHistoryInfo.ID', index: 'QuestionHistoryInfo.ID', align: 'center' },

				{ width: 120, name: 'QuestionHistoryInfo.Q', index: 'QuestionHistoryInfo.Q', align: 'center' },
				
                  { width: 50, align: 'center', name: 'num', index: 'num' },
                    { width: 50, align: 'center', name: 'QuestionHistoryInfo.Q_AddDate', index: 'QuestionHistoryInfo.Q_AddDate' },
          
				{ width: 80, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
                ],
                sortname: 'QuestionHistoryInfo.ID',
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
