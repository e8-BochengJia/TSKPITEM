<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
   
private Question myApp;
private ITools tools;

private string Q_Question, Q_Option_A, Q_Option_B, Q_Option_C, Q_Option_D, Q_Answer;
private int ID,Q_Cate;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("d1243e82-cc4e-4b77-a3c9-10c0eb60f499");

        myApp = new Question();
        tools = ToolsFactory.CreateTools();

        ID = tools.CheckInt(Request.QueryString["ID"]);
        QuestionInfo entity = myApp.GetQuestionByID(ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            ID = entity.ID;
            Q_Cate = entity.Q_Cate;
            Q_Question = entity.Q_Question;
            Q_Option_A = entity.Q_Option_A;
            Q_Option_B = entity.Q_Option_B;
            Q_Option_C = entity.Q_Option_C;
            Q_Option_D = entity.Q_Option_D;
            Q_Answer = entity.Q_Answer;

        }
      
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
                <td class="content_title">
                题库修改
                   
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="question_do.aspx">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                        <tr>
                            <td class="cell_title">
                                题目类别
                            </td>
                            <td class="cell_content">
                              
                                <select id="Q_Cate" name="Q_Cate">
                                    
                                    <%=myApp.GetQuestionCateOption(Q_Cate)%>
                                </select>
                               <%-- <span class="tip">&nbsp;&nbsp;选择上级分类，不选则默认为根类别</span>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                标题
                            </td>
                            <td class="cell_content">
                                <input name="Q_Question" type="text" id="Q_Question" value="<%=Q_Question %>" />
                           <%--     <span class="t12_red">*</span> <span class="tip">&nbsp;&nbsp;填写类别名称，建议在4-8个字左右为佳</span>--%>
                            </td>
                        </tr>
                    
                        
                        
                        <tr>
                            <td class="cell_title">
                               选项
                            </td>
                            <td class="cell_content">
                            选项A <input name="Q_Option_A" type="text" id="Q_Option_A" value="<%=Q_Option_A %>" />
                            选项B <input name="Q_Option_B" type="text" id="Q_Option_B" value="<%=Q_Option_B %>" /><br />
                            选项C <input name="Q_Option_C" type="text" id="Q_Option_C" value="<%=Q_Option_C %>" />
                            选项D <input name="Q_Option_D" type="text" id="Q_Option_D" value="<%=Q_Option_D %>" />
                             
                            </td>
                        </tr>
                      
                        <tr>
                            <td class="cell_title">
                                答案
                            </td>
                            <td class="cell_content">
                                <input type="radio" name="Q_Answer" <% =Public.CheckedRadio(Q_Answer.ToString(), "A")%> value="A" />A
                                 <input type="radio" name="Q_Answer" <% =Public.CheckedRadio(Q_Answer.ToString(), "B")%> value="B" />B
 <input type="radio" name="Q_Answer" <% =Public.CheckedRadio(Q_Answer.ToString(), "C")%>  value="C" />C
                                 <input type="radio" name="Q_Answer" <% =Public.CheckedRadio(Q_Answer.ToString(), "D")%>  value="D" />D
                            </td>
                        </tr>
                        
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <tr>
                            <td align="right">
                                <input type="hidden" id="action" name="action" value="renew" />
                            
                                
            <input type="hidden" id="ID" name="ID" value="<% =ID%>" />
                                <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';"
                                    onmouseout="this.className='bt_grey';" onclick="location='question_list.aspx';" />
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

