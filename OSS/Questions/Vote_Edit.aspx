<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private ITools tools;
    private Vote myApp;
    private string Vote_Name, Vote_Remarks, Vote_SN;
    private int Vote_ID, Vote_Source, Vote_IsActive, Vote_Number, Vote_Type;
    private DateTime Vote_Start, Vote_End, Vote_AddTime;
    private System.Collections.Generic.IList<VoteSelectInfo> voteselects = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Vote();
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("0b4dd57c-9f47-4d2a-a48e-32ab060ca268");
        Vote_ID = tools.CheckInt(Request.QueryString["Vote_ID"]);
        VoteInfo entity = myApp.GetVoteByID(Vote_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Vote_ID = entity.Vote_ID;
            Vote_Name = entity.Vote_Name;
            Vote_Source = entity.Vote_Source;
            Vote_Start = entity.Vote_Start;
            Vote_End = entity.Vote_End;
            Vote_IsActive = entity.Vote_IsActive;
            Vote_Number = entity.Vote_Number;
            Vote_AddTime = entity.Vote_AddTime;
            Vote_Remarks = entity.Vote_Remarks;
            Vote_SN = entity.Vote_SN;
            Vote_Type = entity.Vote_Type;
            voteselects = myApp.GetVoteSelectByVoteID(Vote_ID);
        }
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
    <link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="/Scripts/promotion.js" type="text/javascript"></script>
    <script src="/layer/layui.js" type="text/javascript"></script>
    <link href="/layer/css/layui.css" rel="stylesheet" />
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">添加投票</td>
            </tr>
            <tr>
                <td class="content_content">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top" height="31" class="opt_foot">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="opt_gap">&nbsp;</td>
                                        <td class="opt_cur" id="frm_opt_1">
                                            <%=Public.Page_ScriptOption("choose_opt(1,2);", "基本信息")%></td>
                                        <td class="opt_gap">&nbsp;</td>
                                        <td class="opt_uncur" id="frm_opt_2">
                                            <%=Public.Page_ScriptOption("choose_opt(2,2);", "投票选项")%></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="opt_content">
                                <form id="formadd" name="formadd" method="post" action="/questions/vote_do.aspx">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
                                        <tr>
                                            <td class="cell_title">投票名称</td>
                                            <td class="cell_content">
                                                <input name="Vote_Name" type="text" id="Vote_Name" size="50" maxlength="50" value="<%=Vote_Name %>" /></td>
                                        </tr>
                                        <tr>
                                            <td class="cell_title">投票类型</td>
                                            <td class="cell_content">
                                                <input name="Vote_Type" type="radio" id="Vote_Type1" value="0" <% =Public.CheckedRadio(Vote_Type.ToString(), "0")%> />单选
                                    <input type="radio" name="Vote_Type" id="Vote_Type" value="1" <% =Public.CheckedRadio(Vote_Type.ToString(), "1")%> />多选</td>
                                        </tr>

                                        <tr>
                                            <td class="cell_title">投票开始时间</td>
                                            <td class="cell_content">
                                                <input type="text" name="Vote_Start" id="Vote_Start" size="50" maxlength="50" readonly="readonly"  value="<%=Vote_Start.ToString("yyyy-MM-dd") %>"/></td>
                                        </tr>
                                        <tr>
                                            <td class="cell_title">投票结束时间</td>
                                            <td class="cell_content">
                                                <input type="text" name="Vote_End" id="Vote_End" size="50" maxlength="50" readonly="readonly"  value="<%=Vote_End.ToString("yyyy-MM-dd") %>"/></td>
                                        </tr>
                                        <script>
                                            layui.use('laydate', function () {
                                                var laydate = layui.laydate;

                                                //执行一个laydate实例
                                                laydate.render({
                                                    elem: '#Vote_Start' //指定元素
                                                    , type: 'date'

                                                });
                                                laydate.render({
                                                    elem: '#Vote_End' //指定元素
                                                   , type: 'date'

                                                });
                                            });
                                        </script>
                                        <tr>
                                            <td class="cell_title">是否启用</td>
                                            <td class="cell_content">
                                                <input name="Vote_IsActive" type="radio" id="Vote_IsActive1" value="1"  <% =Public.CheckedRadio(Vote_IsActive.ToString(), "1")%> />是
                                    <input type="radio" name="Vote_IsActive" id="Vote_IsActive2" value="0" <% =Public.CheckedRadio(Vote_IsActive.ToString(), "0")%>/>否</td>
                                        </tr>
                                        <tr>
                                            <td class="cell_title">备注</td>
                                            <td class="cell_content">
                                                <textarea name="Vote_Remarks" cols="50" rows="5" id="Vote_Remarks"><%=Vote_Remarks %></textarea>
                                            </td>
                                        </tr>

                                    </table>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2" style="display: none;">
                                        <%
                                            int i = 0;
                                            if(voteselects!=null)
                                            {
                                                foreach(VoteSelectInfo one in voteselects)
                                                {
                                                    i++;
                                                    Response.Write("<tr>");
                                                    Response.Write("<td class=\"cell_title\">选项名称"+i+"</td>");
                                                    Response.Write("<td class=\"cell_content\">");
                                                    Response.Write(" <input name=\"Vote_Select_Name_"+i+"\" type=\"text\" id=\"Vote_Select_Name_"+i+"\" size=\"50\" maxlength=\"50\" value=\""+one.Vote_Select_Name+"\" />");
                                                    Response.Write(" <input name=\"Vote_Select_ID_"+i+"\" type=\"hidden\" id=\"Vote_Select_ID_"+i+"\" size=\"50\" maxlength=\"50\" value=\""+one.Vote_Select_ID+"\" />");
                                                    Response.Write("</td>");
                                                    Response.Write("</tr>");
                                                }
                                            }
                                            while(i<6)
                                            {
                                                i++;
                                                    Response.Write("<tr>");
                                                    Response.Write("<td class=\"cell_title\">选项名称"+i+"</td>");
                                                    Response.Write("<td class=\"cell_content\">");
                                                    Response.Write(" <input name=\"Vote_Select_Name_"+i+"\" type=\"text\" id=\"Vote_Select_Name_"+i+"\" size=\"50\" maxlength=\"50\" value=\"\" />");
                                                    Response.Write(" <input name=\"Vote_Select_ID_"+i+"\" type=\"hidden\" id=\"Vote_Select_ID_"+i+"\" size=\"50\" maxlength=\"50\" value=\"0\" />");
                                                    Response.Write("</td>");
                                                    Response.Write("</tr>");
                                            }
                                         %>
                                        
                                    </table>
                                    <div class="foot_gapdiv"></div>
                                    <div class="float_option_div" id="float_option_div">
                                        <input type="hidden" id="action" name="action" value="renew" />
                                        <input type="hidden" id="Vote_ID" name="Vote_ID" value="<%=Vote_ID %>" />
                                        <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                        <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'vote_list.aspx';" />
                                    </div>

                                </form>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>
    </div>
</body>
</html>
