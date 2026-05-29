<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    string keyword = "";
    int member_grade = 0, member_state = 0;
    string defaultkey = "";
    string member_source = "";
    Member myApp;
    string date_start, date_end;
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("3a9a9cdf-ef00-407d-98ef-44e23be397e8");
        tools = ToolsFactory.CreateTools();
        myApp = new Member();
        keyword = Request["keyword"];
        member_grade = tools.CheckInt(Request["member_grade"]);
        member_source = tools.CheckStr(Request["member_source"]);
        member_state = tools.CheckInt(Request["member_state"]);
        if (keyword != "输入会员名、邮箱、姓名进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入会员名、邮箱、姓名进行搜索";
        }
        if (keyword == "输入会员名、邮箱、姓名进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

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
                <td class="content_title">会员管理
                </td>
            </tr>
            <tr>
                <td height="5"></td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <form action="member_list.aspx" method="post" name="frm_sch" id="frm_sch">
                            <tr bgcolor="#F5F9FC">
                                <td align="right">
                                    <span class="left_nav">搜索</span> 注册日期：
                                <input type="text" class="input_calendar" name="date_start" id="date_start" maxlength="10"
                                    value="<%=date_start %>" />
                                    -
                                <input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10"
                                    value="<%=date_end%>" />
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $("#date_start").datepicker({ numberOfMonths: 1 });
                                            $("#date_end").datepicker({ numberOfMonths: 1 });
                                        })
                                    </script>
                                    <%--来源：<%=myApp.GetMemberSourceHTML(member_source, "member_source")%>--%>
                                状态：
                                    <select name="member_state">
                                        <option value="0" <%=member_state==0?"selected":"" %>>全部</option>
                                        <option value="1" <%=member_state==1?"selected":"" %>>正常</option>
                                        <option value="2" <%=member_state==2?"selected":"" %>>冻结</option>

                                    </select>
                                    <input type="hidden" name="listtype" value="<% =Request["listtype"]%>" /><input type="text"
                                        name="keyword" size="50" onfocus="if(this.value=='输入会员名、邮箱、姓名进行搜索'){this.value='';}"
                                        id="keyword" value="<% =keyword %>">
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
                            url: '/member/member_do.aspx?action=list&listtype=<% =Request["listtype"]%>&member_source=<%=member_source %>&date_start=<% =date_start%>&member_state=<%=member_state%>&date_end=<% =date_end%>&member_grade=<%=Request["member_grade"] %>&keyword=<%=Server.UrlEncode(defaultkey) %>',
                            datatype: "json",
                            colNames: ['ID', '会员名', '邮箱', '姓名', '性别', '有效积分', '总积分', '文章数', '登录数', '城市', '注册时间', "状态", "操作"],
                            colModel: [
				{ width: 50, name: 'MemberInfo.Member_ID', index: 'MemberInfo.Member_ID', align: 'center' },
                { width: 60, name: 'MemberInfo.Member_NickName', index: 'MemberInfo.Member_NickName', align: 'center' },
				{ name: 'MemberInfo.Member_Email', index: 'MemberInfo.Member_Email', align: 'center' },
				{ width: 60, name: 'MemberInfo.U_Member_Realname', index: 'MemberInfo.U_Member_Realname', align: 'center' },
				{ width: 60, name: 'MemberInfo.U_Member_Male', index: 'MemberInfo.U_Member_Male', align: 'center', sortable: false },

				{ width: 80, name: 'MemberInfo.Member_CoinRemain', index: 'MemberInfo.Member_CoinRemain', align: 'center' },
                { width: 80, name: 'MemberInfo.Member_CoinCount', index: 'MemberInfo.Member_CoinCount', align: 'center' },
				{ width: 80, name: 'MemberInfo.U_Member_Article_Commend', index: 'MemberInfo.U_Member_Article_Commend', align: 'center' },
                { width: 80, name: 'MemberInfo.Member_LoginCount', index: 'MemberInfo.Member_LoginCount', align: 'center' },
                 { width: 80, name: 'MemberInfo.U_Member_City', index: 'MemberInfo.U_Member_City', align: 'center' },
				{ width: 70, name: 'MemberInfo.Member_Addtime', index: 'MemberInfo.Member_Addtime', align: 'center' },

                	{ width: 70, name: 'MemberInfo.U_Member_State', index: 'MemberInfo.U_Member_State', align: 'center' },

				{ width: 80, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
                            ],
                            sortname: 'MemberInfo.Member_ID',
                            sortorder: "desc",
                            rowNum: 10,
                            rowList: [10, 20, 40],

                            pager: 'pager',
                            multiselect: true,
                            viewsortcols: [false, 'horizontal', true],
                            width: getTotalWidth() - 35,
                            height: "100%",

                            loadComplete: function (data) {
                                $("#memberAmount").text(jQuery("#list").jqGrid("getGridParam", "records"));

                                var entity = jQuery("#list").jqGrid("getGridParam", "userData");
                                $("#memberCoin").text(entity["MemberAllCoin"]);
                            }

                        });

                    </script>
                    <div class="divtotal">
                        会员数量：<span id="memberAmount">0</span> 会员积分：<span id="memberCoin">0</span>
                    </div>
                    <form action="/member/member_do.aspx" method="post">
                        <div style="margin-top: 5px;">
                            <% if (Public.CheckPrivilege("29c1d7e3-ef38-4f80-80c8-b376efafe11d"))
                               { %>
                            <input type="button" id="export" class="bt_orange" value="导出勾选会员信息" onclick="location.href = 'member_do.aspx?action=memberexport&member_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                            <input type="button" id="export_all" class="bt_orange" value="导出全部会员信息" onclick="location.href = $('#list').jqGrid('getGridParam', 'url').replace('action=list&', 'action=memberexport_all&') + '&sidx=' + $('#list').jqGrid('getGridParam', 'sortname') + '&sord=' + $('#list').jqGrid('getGridParam', 'sortorder');" />
                            <%} %>
                            <% if (Public.CheckPrivilege("2a50f81a-fd42-41e4-b13b-9c52ae7c8e09"))
                               { %>
                            <input type="button" id="Button1" class="bt_orange" value="正常" onclick="location.href='member_do.aspx?action=normal&member_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                            <input type="button" id="Button2" class="bt_orange" value="冻结" onclick="location.href='member_do.aspx?action=frozen&member_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                            <%} %>
                        </div>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
