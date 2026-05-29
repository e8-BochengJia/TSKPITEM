<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    int Q_Cate;
    private Question myAppC;
    string keyword, defaultkey, ReqURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("318a6535-6af3-4839-9393-816cbc75616d");
        myAppC = new Question();
        tools = ToolsFactory.CreateTools();

        defaultkey = "";
        keyword = Request["keyword"];
        Q_Cate = tools.CheckInt(Request["Q_Cate"]);
        if (keyword != "输入标题搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入标题搜索";
        }
        if (keyword == "输入标题搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&Q_Cate=" + Q_Cate;
        
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
      <td class="content_title">题库管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="question_list.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">
					<span class="left_nav">类别</span> 
					 <select name="Q_Cate">
					   <option value="0">全部类别</option>
                        <% =myAppC.GetQuestionCateOption(Q_Cate)%>
                      </select>
					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入标题搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				  </form>
				</table>
    </td></tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
            jQuery("#list").jqGrid({
                url: 'question_do.aspx?action=list&<%=ReqURL %>',
                datatype: "json",
                colNames: ['ID', '所属类别', "题目标题", "选项A", "选项B", "选项C", "选项D", "答案", "操作"],
                colModel: [
				{ width: 40, name: 'QuestionInfo.ID', index: 'QuestionInfo.ID', align: 'center' },

				{ width: 40, name: 'CateName', index: 'CateName', align: 'center' },
				 {  align: 'center', name: 'QuestionInfo.Q_Question', index: 'QuestionInfo.Q_Question' },
                  { width: 50, align: 'center', name: 'QuestionInfo.Q_Option_A', index: 'QuestionInfo.Q_Option_A' },
                    { width: 50, align: 'center', name: 'QuestionInfo.Q_Option_B', index: 'QuestionInfo.Q_Option_B' },
                      { width: 50, align: 'center', name: 'QuestionInfo.Q_Option_C', index: 'QuestionInfo.Q_Option_C' },
                        { width: 50, align: 'center', name: 'QuestionInfo.Q_Option_D', index: 'QuestionInfo.Q_Option_D' },
                          { width: 50, align: 'center', name: 'QuestionInfo.Q_Answer', index: 'QuestionInfo.Q_Answer' },
				{ width: 80, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
			],
                sortname: 'QuestionInfo.ID',
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
