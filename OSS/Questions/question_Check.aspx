<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

   
    Question myApp;
    ITools tools;
    string  searchtype, keyword, ReqURL;
    int Q_Cate;
    int limit, group;
    string defaultkey = "";
    string target = "";
    string productid;
    string tag = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        tools = ToolsFactory.CreateTools();
        myApp = new Question();
       
        searchtype = tools.CheckStr(Request.QueryString["searchtype"]);
        productid = tools.NullStr(Session["selected_productid"]);
        keyword = Request["keyword"];
        target = tools.NullStr(Request["target"]);
        limit = tools.CheckInt(Request["limit"]);
        group = tools.CheckInt(Request["group"]);
        Q_Cate = tools.CheckInt(Request["Q_Cate"]);
        tag = tools.CheckStr(Request["tag"]);
      
        if (keyword != "输入题库名称搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入题库名称搜索";
        }
        if (keyword == "输入题库名称搜索")
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
    <script src="/Scripts/promotion.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
<script type="text/javascript">
	function MM_ReturnID(){ //v3.0
	    $('#favor_productid').val($("#selarrow").val());
	    $.ajax({
	    async: false,
        type: "POST",
        url: "/Questions/question_do.aspx?action=saveproductid&timer=" + Math.random(),
        data: "productid=" +  $("#selarrow").val()
        });
	    $('#product_picker').load("/Questions/question_do.aspx?action=showproduct&group=<%=group %>&limit=<%=limit %>&timer=" + Math.random());
	    if($('#favor_productid').val()!="0,")
	    {
	        $('#favor_productall0').attr("checked",true);
	        $('#favor_productall1').attr("checked",false);
	    }
	    close_picker();
    }
    	
	
</script>
</head>
<body>
  <table width="600" border="0" cellpadding="0" cellspacing="0">
    <tr>
      <td>
      <table width="100%" cellpadding="0" cellspacing="0" border="0" class="picker_tittab">
      <tr><td class="picker_tit">题库选择</td><td width="30" align="center"><a href="javascript:void(0);" onclick="close_picker();"><img src="/images/close.gif" border="0"/></a></td></tr>
      </table>
      </td>
    </tr>
    <tr><td>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">

		  <tr bgcolor="#F5F9FC" >
			<td align="right"><span class="left_nav">搜索</span> 
			<span id="main_cate">
                 <select name="Q_Cate" id="Q_cate">
					   <option value="0">选择类别</option>
                <%=myApp.GetQuestionCateOption(Q_Cate)%>
                     </select>
			</span>
			 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入题库名称搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索"  onclick="refresh_productlist();"/>
			 <input type="hidden" name="tag" id="tag" value="<%=tag %>" />
			 </td>
		  </tr>
		</table>
    </td></tr>
    <tr>
      <td id="productlist" height="360" valign="top">
        <input type="hidden" id="all_flag" value="0" />
        <input type="hidden" id="allids" value="<%=myApp.Get_ProductList_IDs() %>" />
        <input type="hidden" id="selarrow" value="0,<%=productid %>" />
        <div class="list_tip_div" id="list_seltip"></div>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: '/Questions/question_do.aspx?action=questionlist&<%=ReqURL %>',
			datatype: "json",
			colNames: ['ID', '所属类别', '题目标题'],
            colModel: [
                {width:30,align:'center', name: 'id', index: 'id',sortable:false},
                { align: 'left', name: 'QuestionInfo.cate', index: 'QuestionInfo.cate', sortable: false },
				{ align: 'left', name: 'QuestionInfo.Q_Question', index: 'QuestionInfo.Q_Question', sortable: false }
				
			],
            sortname: 'QuestionInfo.ID',
			sortorder: "desc",
			rowNum: 10,
			//rowList:[10,20,40], 
			pager: 'pager', 
			multiselect: true,
			viewrecords:true,
			viewsortcols: [false,'horizontal',true],
			width: 597,
			height: "100%",
			onSelectRow: function(id,status){  
			 jqgrid_rowclick(id,status);
			 jqgrid_seltip_display();
			 
           }, 
			loadComplete:function(){
                jqgrid_selarry();
			    //列表选择记录提示
			    jqgrid_seltip_display();
            }
            
        });
        
        //设置全选标记 
        jqgrid_allclick();
        </script>
      </td>
    </tr>
    <tr><td height="30" align="center" bgcolor="#EAF4FD"><input type="button" name="btn_sch" class="btn_01" value="确定" onclick="MM_ReturnID();"/></td></tr>
  </table>
</body>
</html>
