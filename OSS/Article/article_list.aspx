<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    int CateID;
    private ArticleCate myAppC;
    string keyword, defaultkey, ReqURL;
    int IsAudit;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276");
        myAppC = new ArticleCate();
        tools = ToolsFactory.CreateTools();

        defaultkey = "";
        keyword = tools.CheckStr(Request["keyword"]);
        CateID = tools.CheckInt(Request["Article_Cate"]);
        IsAudit=tools.CheckInt(Request["IsAudit"]);
        if (CateID == 0) { CateID = tools.CheckInt(Request["Article_Cate_parent"]); }
        if (keyword != "输入文章标题题搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入文章标题题搜索";
        }
        if (keyword == "输入文章标题题搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&IsAudit="+IsAudit+"&CateID=" + CateID;
        
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
      <td class="content_title">管理文章</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <form action="Article_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">
					<span class="left_nav">类别</span> 
					 <span id="main_cate"><%=myAppC.Article_Category_Select(CateID, "main_cate")%></span>

                        <span class="left_nav">审核状态</span> 
                        <select id="IsAudit" name="IsAudit" >
                            <option value="0"  <%=Public.CheckedSelected("0",IsAudit.ToString()) %> >全部</option>
                            <option value="1"  <%=Public.CheckedSelected("1",IsAudit.ToString()) %> >待审核</option>
                            <option value="2"  <%=Public.CheckedSelected("2",IsAudit.ToString()) %> >已审核</option>
                            <option value="3"  <%=Public.CheckedSelected("3",IsAudit.ToString()) %> >审核不通过</option>
                        </select>
					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入文章标题题搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'Article_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '文章标题', "所属类别","来源","作者","审核状态","推荐阅读","排序","浏览量", "操作"],
            colModel: [
				{width:20,  name: 'ArticleInfo.Article_ID', index: 'ArticleInfo.Article_ID', align: 'center'},
				{ name: 'ArticleInfo.Article_Title', index: 'ArticleInfo.Article_Title', align: 'center' },
				{ width: 40, align: 'center', name: 'ArticleInfo.Article_CateID', index: 'ArticleInfo.Article_CateID' },
                { width: 40, align: 'center', name: 'ArticleInfo.Article_Source', index: 'ArticleInfo.Article_Source', align: 'center' },
                { width: 40, align: 'center', name: 'ArticleInfo.Article_Author', index: 'ArticleInfo.Article_Author', align: 'center' },
                { width: 40, align: 'center', name: 'ArticleInfo.Article_IsAudit', index: 'ArticleInfo.Article_IsAudit', align: 'center' },
                { width: 40, align: 'center', name: 'ArticleInfo.Article_IsRecommend', index: 'ArticleInfo.Article_IsRecommend', align: 'center' },
                { width: 40, align: 'center', name: 'ArticleInfo.Article_Sort', index: 'ArticleInfo.Article_Sort', align: 'center'},
                 { width: 40, align: 'center', name: 'ArticleInfo.Article_PageViews', index: 'ArticleInfo.Article_PageViews', align: 'center' },
				{width:120, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'ArticleInfo.Article_ID',
			sortorder: "desc",
			rowNum: 10,
			rowList:[10,20,40],						
			pager: 'pager',
			multiselect: true,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			onSelectRow: function (id) {
			    if (id && id !== lastsel) {
			        jQuery('#list').jqGrid('restoreRow', lastsel);
			        jQuery('#list').jqGrid('editRow', id, true);
			        lastsel = id;
			    }
			},
			editurl: "/Article/article_do.aspx?action=listedit2",
			height: "100%"
        });
        </script>
        <form action="/article/article_do.aspx" method="post">
                <% if (Public.CheckPrivilege("cc00c494-d211-438c-baef-ac20d419b066"))
           { %>
        <input type="button" id="Button3" class="bt_orange" value="删除选中文章" onclick="location.href='article_do.aspx?action=batchmove&article_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <%} %>

        </form>
      </td>
    </tr>
  </table>
</div>
</body>
</html>