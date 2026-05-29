<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    private ITools tools;
    private Question MyApp;

    private SysMenu sysmenu = new SysMenu();

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("0f8290b2-c31e-4a76-8e1b-078cedbbabcb");

        MyApp = new Question();

        tools = ToolsFactory.CreateTools();

    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加题库</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <style type="text/css">
        #Q_Question
        {
            width: 267px;
        }
    </style>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">题库添加
                   
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="question_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">题目类别
                                </td>
                                <td class="cell_content">

                                    <select id="Q_Cate" name="Q_Cate">

                                        <%=MyApp.GetQuestionCateOption(0) %>
                                    </select>
                                    <%-- <span class="tip">&nbsp;&nbsp;选择上级分类，不选则默认为根类别</span>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">标题
                                </td>
                                <td class="cell_content">
                                    <input name="Q_Question" type="text" id="Q_Question" />
                                    <%--     <span class="t12_red">*</span> <span class="tip">&nbsp;&nbsp;填写类别名称，建议在4-8个字左右为佳</span>--%>
                                </td>
                            </tr>



                            <tr>
                                <td class="cell_title">选项
                                </td>
                                <td class="cell_content">选项A
                                    <input name="Q_Option_A" type="text" id="Q_Option_A" />
                                    选项B
                                    <input name="Q_Option_B" type="text" id="Q_Option_B" /><br />
                                    选项C
                                    <input name="Q_Option_C" type="text" id="Q_Option_C" />
                                    选项D
                                    <input name="Q_Option_D" type="text" id="Q_Option_D" />

                                </td>
                            </tr>

                            <tr>
                                <td class="cell_title">答案
                                </td>
                                <td class="cell_content">
                                    <input type="radio" name="Q_Answer" checked="checked" value="A" />A
                                 <input type="radio" name="Q_Answer" value="B" />B
 <input type="radio" name="Q_Answer" value="C" />C
                                 <input type="radio" name="Q_Answer" value="D" />D
                                </td>
                            </tr>

                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="new" />


                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';"
                                        onmouseout="this.className='bt_grey';" onclick="location = 'question_list.aspx';" />
                                </td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

