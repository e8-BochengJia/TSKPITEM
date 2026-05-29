<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    private ITools tools;
    string keyword, defaultkey, ReqURL;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("639ad269-9a65-421e-b70f-825df98c2437");

        tools = ToolsFactory.CreateTools();

        defaultkey = "";
        keyword = Request["keyword"];

        if (keyword != "输入专题名称搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入专题名称搜索";
        }
        if (keyword == "输入专题名称搜索")
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
    <!--css-->
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <!--js-->
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">
                    专题报道管理
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <form action="subject_list.aspx" method="post" name="frm_sch" id="frm_sch">
                        <tr bgcolor="#F5F9FC">
                            <td align="right">
                                <span class="left_nav">搜索</span>
                                <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入专题名称搜索'){this.value='';}"
                                    value="<% =keyword %>">
                                <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" />
                            </td>
                        </tr>
                        </form>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <table id="list">
                    </table>
                    <div id="pager">
                    </div>
                    <script type="text/javascript">
                        jQuery("#list").jqGrid({
                            url: 'subject_do.aspx?action=list&<%=ReqURL %>',
                            datatype: "json",
                            colNames: ["ID", "专题名称", "是否启用", "排序", "操作"],
                            colModel: [
				{ width: 40, name: 'ArticleSubjectInfo.Subject_ID', index: 'ArticleSubjectInfo.Subject_ID', align: 'center' },
				{ width: 80, name: 'ArticleSubjectInfo.Subject_Name', index: 'ArticleSubjectInfo.Subject_Name' },
				{ width: 40, name: 'ArticleSubjectInfo.Subject_IsActive', index: 'ArticleSubjectInfo.Subject_IsActive', align: 'center' },
                { width: 40, name: 'ArticleSubjectInfo.Subject_Sort', index: 'ArticleSubjectInfo.Subject_Sort', align: 'center' },
				{ width: 80, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
			],
                            sortname: 'ArticleSubjectInfo.Subject_ID',
                            sortorder: "asc",
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
