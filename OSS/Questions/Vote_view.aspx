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
        Public.CheckLogin("a4aada81-2e0b-460d-9fff-a69eb6d57e54");
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
    <script src="/layer/layui.js" type="text/javascript"></script>
    <link href="/layer/css/layui.css" rel="stylesheet" />
                        <script>
                        layui.use('element', function () {
                            var element = layui.element;
                        });
                    </script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">投票信息</td>
            </tr>
            <tr>
                <td class="content_content">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                        <tr>
                            <td class="cell_title">投票</td>
                            <td class="cell_content"><%=Vote_Name %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">投票类型</td>
                            <td class="cell_content"><%if (Vote_Type == 0) { Response.Write("单选"); } else { Response.Write("多选"); } %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">开始时间</td>
                            <td class="cell_content"><%=Vote_Start.ToString("yyyy-MM-dd") %></td>
                        </tr>

                        <tr>
                            <td class="cell_title">结束时间</td>
                            <td class="cell_content"><%=Vote_End.ToString("yyyy-MM-dd") %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">启用状态</td>
                            <td class="cell_content"><%=myApp.GetVoteIsActive(Vote_IsActive) %></td>
                        </tr>

                        <tr>
                            <td class="cell_title">投票总数</td>
                            <td class="cell_content"><%=Vote_Number %>票</td>
                        </tr>
                        <tr>
                            <td class="cell_title">创建时间</td>
                            <td class="cell_content"><%=Vote_AddTime %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">备注</td>
                            <td class="cell_content"><%=Vote_Remarks %></td>
                        </tr>

                        <tr>
                            <td class="cell_title">详情</td>
                            <td class="cell_content">
                                <table width="500px;">
                                    
                                        <%
                                            if (voteselects != null)
                                            {
                                                foreach (VoteSelectInfo one in voteselects)
                                                {
                                                    double mm;
                                                    if (Vote_Number > 0)
                                                    {
                                                        mm = Math.Round(tools.NullDbl(one.Vote_Select_Number) / tools.NullDbl(Vote_Number) * 100, 2);
                                                    }
                                                    else
                                                    {
                                                        mm = 0;
                                                    }

                                                    Response.Write("<tr>");
                                                    Response.Write("<td width=\"100px;\">" + one.Vote_Select_Name + "</td>");

                                                    Response.Write("<td>");
                                                    Response.Write("<p>" + one.Vote_Select_Number + "票</p>");

                                                    Response.Write("<div class=\"layui-progress layui-progress-big\"><div class=\"layui-progress-bar\" lay-percent=\""+mm+"%\"></div></div>");
                                                    Response.Write("</td>");
                                                    Response.Write("</tr>");
                                                }
                                            }
                                        %>

                                </table>
                            </td>
                        </tr>
                    </table>

                    <div style="text-align: right; margin: 10px 0px;">
                        <input name="button" type="submit" class="bt_orange" id="button" value="返回" onclick="history.go(-1);" />

                    </div>
                </td>
            </tr>

        </table>
    </div>
</body>
</html>
