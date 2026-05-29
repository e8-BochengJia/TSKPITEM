using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.BLL.CMS;
using System.Text;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.BLL.MEM;
/// <summary>
///Question 的摘要说明
/// </summary>
public class Question
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IQuestion Myquestion;
    private IQuestionCate Myquestioncate;
    private IQuestionHistory MyquestionH;
    private ISQLHelper DBHelper;
    private IMemberConsumption MyCoinlog;
     
    public Question()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //

        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        Myquestion = QuestionFactory.CreateQuestion();
        Myquestioncate = QuestionCateFactory.CreateQuestionCate();
        MyquestionH = QuestionHistoryFactory.CreateQuestionHistory();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
        MyCoinlog = MemberConsumptionFactory.CreateMemberConsumption();
        
    }
    public string GetQuestionCateOption(int id)
    {

        StringBuilder sHtml = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "QuestionCateInfo.Q_Cate_Valid", "=", "1"));

        Query.OrderInfos.Add(new OrderInfo("QuestionCateInfo.ID", "asc"));
        IList<QuestionCateInfo> entitys = Myquestioncate.GetQuestionCates(Query);
        if (entitys != null)
        {
            foreach (QuestionCateInfo entity in entitys)
            {
                if (entity.ID == id)
                {
                    sHtml.Append("<option value=\"" + entity.ID + "\" selected=\"selected\">" + entity.Q_Cate_Name + "</option>");
                }
                else
                {
                    sHtml.Append("<option value=\"" + entity.ID + "\">" + entity.Q_Cate_Name + "</option>");
                }
            }
        }

        return sHtml.ToString();
    }
    public string GetQuestion_Cate_Name(int ID)
    {
        string name = "未知类别";
        QuestionCateInfo qcate = Myquestioncate.GetQuestionCateByID(ID);
        if (qcate != null)
        {
            name = qcate.Q_Cate_Name;
        }
        return name;

    }
    public void AddQuestion()
    {
        int Sys_Menu_ID = 0;
        int Q_Cate = tools.CheckInt(Request["Q_Cate"]);
        string Q_Question = tools.CheckStr(Request["Q_Question"]);

        string Q_Option_A = tools.CheckStr(Request["Q_Option_A"]);
        string Q_Option_B = tools.CheckStr(Request["Q_Option_B"]);
        string Q_Option_C = tools.CheckStr(Request["Q_Option_C"]);
        string Q_Option_D = tools.CheckStr(Request["Q_Option_D"]);

        string Q_Answer = tools.CheckStr(Request["Q_Answer"]);


        if (Q_Question == "")
        {
            Public.Msg("error", "错误信息", "请填写标题", false, "{back}");
        }
        if (Q_Option_A == "" || Q_Option_B == "" || Q_Option_C == "" || Q_Option_D == "")
        {
            Public.Msg("error", "错误信息", "存在不完整项，请将题目各项填写完整。", false, "{back}");
        }
        QuestionInfo ques = new QuestionInfo();
        ques.Q_Cate = Q_Cate;
        ques.Q_Option_A = Q_Option_A;
        ques.Q_Option_B = Q_Option_B;
        ques.Q_Option_C = Q_Option_C;
        ques.Q_Option_D = Q_Option_D;
        ques.Q_Answer = Q_Answer;
        ques.Q_Question = Q_Question;
        if (Myquestion.AddQuestion(ques, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "question_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    public void editQuestion()
    {
        int ID = tools.CheckInt(Request["ID"]);
        int Q_Cate = tools.CheckInt(Request["Q_Cate"]);
        string Q_Question = tools.CheckStr(Request["Q_Question"]);

        string Q_Option_A = tools.CheckStr(Request["Q_Option_A"]);
        string Q_Option_B = tools.CheckStr(Request["Q_Option_B"]);
        string Q_Option_C = tools.CheckStr(Request["Q_Option_C"]);
        string Q_Option_D = tools.CheckStr(Request["Q_Option_D"]);

        string Q_Answer = tools.CheckStr(Request["Q_Answer"]);


        if (Q_Question == "")
        {
            Public.Msg("error", "错误信息", "请填写标题", false, "{back}");
        }
        if (Q_Option_A == "" || Q_Option_B == "" || Q_Option_C == "" || Q_Option_D == "")
        {
            Public.Msg("error", "错误信息", "存在不完整项，请将题目各项填写完整。", false, "{back}");
        }
        QuestionInfo ques = new QuestionInfo();
        ques.ID = ID;
        ques.Q_Cate = Q_Cate;
        ques.Q_Option_A = Q_Option_A;
        ques.Q_Option_B = Q_Option_B;
        ques.Q_Option_C = Q_Option_C;
        ques.Q_Option_D = Q_Option_D;
        ques.Q_Answer = Q_Answer;
        ques.Q_Question = Q_Question;
        if (Myquestion.EditQuestion(ques, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "question_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    public QuestionInfo GetQuestionByID(int ID)
    {

        return Myquestion.GetQuestionByID(ID, Public.GetUserPrivilege());
    }

    public string GetQuestions()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int Q_Cate = tools.CheckInt(Request["Q_Cate"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (Q_Cate > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "QuestionInfo.Q_Cate", "=", Q_Cate.ToString()));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "QuestionInfo.Q_Question", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = Myquestion.GetPageInfo(Query, Public.GetUserPrivilege());



        IList<QuestionInfo> entitys = Myquestion.GetQuestions(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (QuestionInfo entity in entitys)
            {

                jsonBuilder.Append("{\"QuestionInfo.ID\":" + entity.ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetQuestion_Cate_Name(entity.Q_Cate));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Q_Question);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Q_Option_A);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Q_Option_B);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Q_Option_C);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Q_Option_D);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Q_Answer);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("d1243e82-cc4e-4b77-a3c9-10c0eb60f499"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"question_edit.aspx?ID=" + entity.ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("2c551863-a2bd-44a8-aef9-512784f0f4a0"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('question_do.aspx?action=move&ID=" + entity.ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }
    }

    //题目
    public string SelectQuestion()
    {

        string keyword = tools.CheckStr(Request["keyword"]);
        string product_id = "0";
        if (keyword != "输入题库名称搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "QuestionInfo.ID", "<>", "0"));
    
        int cate_id = tools.CheckInt(Request["Q_Cate"]);
        if (cate_id > 0)
        {

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "QuestionInfo.Q_Cate", "=", cate_id.ToString()));
          
        }

        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "QuestionInfo.Q_Question", "like", keyword));
          
        }

        Query.OrderInfos.Add(new OrderInfo("QuestionInfo.ID", "Desc"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = Myquestion.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<QuestionInfo> entitys = Myquestion.GetQuestions(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (QuestionInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.ID + ",\"cell\":[");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                jsonBuilder.Append(GetQuestion_Cate_Name(entity.Q_Cate));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Q_Question));
                jsonBuilder.Append("\",");

              

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");

            entitys = null;
            return jsonBuilder.ToString();
        }
        else { return null; }
    }

    //获取全部题目
    public string Get_ProductList_IDs()
    {
        string product_arry = "";
        string keyword = tools.CheckStr(Request["keyword"]);
        string product_id = "0";
        if (keyword != "输入题库名称搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "QuestionInfo.ID", "<>", "0"));

        int cate_id = tools.CheckInt(Request["Q_Cate"]);
        if (cate_id > 0)
        {

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "QuestionInfo.Q_Cate", "=", cate_id.ToString()));

        }

        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "QuestionInfo.Q_Question", "like", keyword));

        }

        Query.OrderInfos.Add(new OrderInfo("QuestionInfo.ID", "Desc"));
        IList<QuestionInfo> entitys = Myquestion.GetQuestions(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (QuestionInfo entity in entitys)
            {
                if (product_arry.Length > 0)
                {
                    product_arry = product_arry + "," + entity.ID.ToString();
                }
                else
                {
                    product_arry = entity.ID.ToString();
                }
            }
        }
        return product_arry;
    }

    //展示已选择产品
    public string ShowProduct(string Q_ID)
    {
        int del_product = tools.CheckInt(Request["pid"]);
        string product_id = "";
        StringBuilder jsonBuilder = new StringBuilder();
        int i = 0;
        //QueryInfo Query = new QueryInfo();
        //Query.PageSize = 0;
        //Query.CurrentPage = 1;
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "QuestionInfo.ID", "<>", "0"));

        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "QuestionInfo.ID", "in", Q_ID));

        //Query.OrderInfos.Add(new OrderInfo("QuestionInfo.ID", "Desc"));
        //IList<QuestionInfo> entitys = Myquestion.GetQuestions(Query, Public.GetUserPrivilege());
        //if (entitys != null)
        if (Q_ID != ""&& Q_ID.Split(',').Count()>0) 
        {

            jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"100%\" bgcolor=\"#B0CADA\">");
            jsonBuilder.Append("    <tr class=\"list_head_bg\">");
            jsonBuilder.Append("        <td colspan=\"5\" align=\"left\">已选择题目 <span id=\"product_unfold\">[<a href=\"javascript:void(0);\" onclick=\"$('#product_unfold').hide();$('#product_fold').show();$('#product_picker').attr('class','div_picker_unfold');\">展开</a>]</span><span id=\"product_fold\" style=\"display:none;\">[<a href=\"javascript:void(0);\" onclick=\"$('#product_unfold').show();$('#product_fold').hide();$('#product_picker').attr('class','div_picker');\">还原</a>]</span></td>");
            jsonBuilder.Append("    </tr>");
            jsonBuilder.Append("    <tr class=\"list_td_bg\">");
            jsonBuilder.Append("        <td width=\"10%\" align=\"center\">ID</td>");
            jsonBuilder.Append("        <td width=\"25%\" align=\"center\">所属分类</td>");
            jsonBuilder.Append("        <td width=\"50%\" align=\"center\">题目标题</td>");


          
             jsonBuilder.Append("        <td width=\"10%\" align=\"center\">排序</td>");

             jsonBuilder.Append("        <td width=\"10%\" align=\"center\">操作</td>");
            jsonBuilder.Append("        </tr>");
            //foreach (QuestionInfo entity in entitys)
            string[] ListQ=Q_ID.Split(',');
            for (int j = 0; j < ListQ.Count(); j++)
            {
                QuestionInfo entity = GetQuestionByID(tools.CheckInt(ListQ[j]));
                if (entity != null)
                {
                    if (del_product != entity.ID)
                    {
                        i = i + 1;
                        if (product_id == "")
                        {
                            product_id = entity.ID.ToString();
                        }
                        else
                        {
                            product_id += "," + entity.ID.ToString();
                        }
                        jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                        jsonBuilder.Append("        <td width=\"10%\" align=\"left\">" + entity.ID + "</td>");
                        jsonBuilder.Append("        <td width=\"25%\" align=\"left\">" + GetQuestion_Cate_Name(entity.Q_Cate) + "</td>");
                        jsonBuilder.Append("        <td width=\"50%\" align=\"left\">" + entity.Q_Question + "</td>");
                        jsonBuilder.Append("        <td width=\"10%\" align=\"center\"><input type=\"text\" size=\"5\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\"  name=\"listsort_" + entity.ID + "\" value=\"" + (i - 1) + "\" ></td>");
                        jsonBuilder.Append("        <td  width=\"10%\"><a href=\"javascript:picker_product_del('" + entity.ID + "');\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");

                        jsonBuilder.Append("    </tr>");
                    }
                }
            }
            jsonBuilder.Append("</table>");
            if (product_id == "")
            {
                jsonBuilder = null;
                jsonBuilder = new StringBuilder();
                jsonBuilder.Append("<span class=\"pickertip\">已选择题目</span>");
            }
            else
            {
                jsonBuilder.Append("<script>if($('#product_picker').attr('class')=='div_picker_unfold'){$('#product_unfold').hide();$('#product_fold').show();}else{$('#product_unfold').show();$('#product_fold').hide();}</script>");
            }
        }
        else
        {
            jsonBuilder.Append("<span class=\"pickertip\">已选择题目</span>");
        }
        Session["selected_productid"] = product_id;
        jsonBuilder.Append("<script>$('#favor_productid').val('" + product_id + "');</script>");
        //entitys = null;

        return jsonBuilder.ToString();
    }

    public void DelQuestion()
    {
        int ID = tools.CheckInt(Request["ID"]);

        if (Myquestion.DelQuestion(ID, Public.GetUserPrivilege()) > 0)
        {
            Public.AddRBACUserLog(2, ID.ToString(), "题库删除成功", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "question_list.aspx");
        }
        else
        {
            Public.AddRBACUserLog(2, ID.ToString(), "题库删除失败", "", 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    #region 套题管理
    public QuestionHistoryInfo GetQuestionsHistoryByID(int ID)
    {
        return MyquestionH.GetQuestionHistoryByID(ID, Public.GetUserPrivilege());
    }

    public string GetQuestionsHistory()
    {
      
        QueryInfo Query = new QueryInfo();

        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyquestionH.GetPageInfo(Query, Public.GetUserPrivilege());



        IList<QuestionHistoryInfo> entitys = MyquestionH.GetQuestionHistorys(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (QuestionHistoryInfo entity in entitys)
            {

                jsonBuilder.Append("{\"QuestionHistoryInfo.ID\":" + entity.ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                jsonBuilder.Append(entity.Q);

                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href=\\\"javascript:void(null);\\\" onclick=\\\"javascript:window.open('question_Coin.aspx?ID=" + entity.ID + "','答题得分记录','height=450, width=300, top=350,left=800, toolbar=no,scrollbars=yes')\\\"  style=\\\"color:blue;\\\">" + GetQH_memberCount(entity.ID) + "</span>");
                jsonBuilder.Append("\",");

              

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Q_AddDate.ToShortDateString());
                jsonBuilder.Append("\",");

             

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("7e0a9a43-af8f-44c9-b00e-aa8de567f9e7"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"question_history_edit.aspx?ID=" + entity.ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("4a2a3deb-cc3b-42eb-898e-0de38315fef6"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('question_do.aspx?action=movehistory&ID=" + entity.ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }
    }

    public object GetQH_memberCount(int QHid)
    {
        string Sqlstr = "Select count(Consump_ID) from Member_Consumption where Consump_Qid=" + QHid;
        return DBHelper.ExecuteScalar(Sqlstr);
    }

  
    public void AddQuestionHistory()
    {

      
        string Q = "";
        string QIDS = tools.NullStr(Request["favor_productid"]);
        if (QIDS == "")
        {
            Public.Msg("error", "错误信息", "请选择题目", false, "{back}");
        }
        if (QIDS.Length > 0)
        {
            Dictionary<int, int> listQh = new Dictionary<int, int>();
            foreach (string subproductid in QIDS.Split(','))
            {
                if (tools.CheckInt(subproductid) > 0)
                {
                    listQh.Add(tools.CheckInt(subproductid), tools.CheckInt(Request["listsort_" + subproductid]));
                }
            }
            var result = from pair in listQh

                         orderby pair.Value ascending

                         select pair;

            foreach (KeyValuePair<int, int> kvp in result)
            {
                Q += kvp.Key+"+";
             
            }
        }


        QuestionHistoryInfo entity = new QuestionHistoryInfo();
        entity.ID = 0;
        entity.Q = Q.TrimEnd('+');
        entity.Q_Hit = 0;
        entity.Q_AddDate = DateTime.Now;

        if (MyquestionH.AddQuestionHistory(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "question_History_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditQuestionHistory()
    {

        int ID = tools.CheckInt(Request.Form["Q_ID"]);
        string Q = tools.CheckStr(Request.Form["Q"]);
       
        QuestionHistoryInfo entity = GetQuestionsHistoryByID(ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "数据有误！请重新操作", false, "{back}");
        }
        string QIDS = tools.NullStr(Request["favor_productid"]);
        if (QIDS == "")
        {
            Public.Msg("error", "错误信息", "请选择题目", false, "{back}");
        }
        if (QIDS.Length > 0)
        {
            Dictionary<int, int> listQh = new Dictionary<int, int>();
            foreach (string subproductid in QIDS.Split(','))
            {
                if (tools.CheckInt(subproductid) > 0)
                {
                    listQh.Add(tools.CheckInt(subproductid), tools.CheckInt(Request["listsort_" + subproductid]));
                }
            }
            var result = from pair in listQh

                         orderby pair.Value ascending

                         select pair;

            foreach (KeyValuePair<int, int> kvp in result)
            {
                Q += kvp.Key + "+";

            }
        }
        entity.Q = Q.TrimEnd('+');


        if (MyquestionH.EditQuestionHistory(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Question_History_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }


    }

    public void DelQuestionHistory()
    {
        int ID = tools.CheckInt(Request.QueryString["ID"]);
        if (MyquestionH.DelQuestionHistory(ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "question_history_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public string Coin_QuestionH()
    {

        string ID = tools.CheckStr(Request["ID"]);
       
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberConsumptionInfo.Consump_ID", "<>", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberConsumptionInfo.Consump_Qid", "=", ID));



        Query.OrderInfos.Add(new OrderInfo("MemberConsumptionInfo.Consump_Addtime", "Desc"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyCoinlog.GetPageInfo(Query);
        IList<MemberConsumptionInfo> entitys = MyCoinlog.GetMemberConsumptions(Query);
        Member mem = new Member();
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberConsumptionInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Consump_ID + ",\"cell\":[");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(mem.GetMemberNameByID(entity.Consump_MemberID));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                jsonBuilder.Append(entity.Consump_Coin);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Consump_Addtime);
                jsonBuilder.Append("\",");



                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");

            entitys = null;
            return jsonBuilder.ToString();
        }
        else { return null; }
    }
    #endregion

}