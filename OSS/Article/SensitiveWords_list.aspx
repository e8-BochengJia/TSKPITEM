<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    string keyword, defaultkey, ReqURL;
    int CateID;
    private ArticleCate myAppC;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("a8286f1e-b1cb-4523-821f-86a0d7c79793");
        
        tools = ToolsFactory.CreateTools();
        myAppC = new ArticleCate();
        defaultkey = "";
        keyword = Request["keyword"];

        
        if (keyword != "输入敏感词搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入敏感词搜索";
        }
        if (keyword == "输入敏感词搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey);
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
      <td class="content_title">敏感词管理<a href="SensitiveWords_add.aspx">新增</a></td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <form action="Article_cate_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">

					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入敏感词搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				  
				</table>
    </form>
    </td></tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
            var lastsel;
        jQuery("#list").jqGrid({
            url: 'SensitiveWords_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
			colNames: ['ID', '敏感词', "操作"],
            colModel: [
				{ width: 40, name: 'SensitiveWordsInfo.ID', index: 'SensitiveWordsInfo.ID', align: 'center' },
				{ align: 'center', name: 'SensitiveWordsInfo.Name', index: 'SensitiveWordsInfo.Name', align: 'center' },
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'SensitiveWordsInfo.ID',
			sortorder: "desc",
			rowNum: 10,
			rowList:[10,20,40],						
			pager: 'pager',
			multiselect: false,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			onSelectRow: function (id) {
			    if (id && id !== lastsel) {
			        jQuery('#list').jqGrid('restoreRow', lastsel);
			        jQuery('#list').jqGrid('editRow', id, true);
			        lastsel = id;
			    }
			},
			height: "100%"
        });
        </script>
      </td>
    </tr>
  </table>
</div>
</body>
</html>