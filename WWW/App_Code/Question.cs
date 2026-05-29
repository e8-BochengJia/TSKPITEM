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
    private IVote Myvote;
    Public_Class pub = new Public_Class();
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
        Myvote = VoteFactory.CreateVote();

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


    public QuestionInfo GetQuestionByID(int ID)
    {

        return Myquestion.GetQuestionByID(ID, pub.CreateUserPrivilege("d1243e82-cc4e-4b77-a3c9-10c0eb60f499"));
    }


    //题目
    public string SelectQuestion()
    {
        return null;
        //string keyword = tools.CheckStr(Request["keyword"]);
        //string product_id = "0";
        //if (keyword != "输入题库名称搜索" && keyword != null)
        //{
        //    keyword = keyword;
        //}
        //else
        //{
        //    keyword = "";
        //}
        //QueryInfo Query = new QueryInfo();
        //Query.PageSize = tools.CheckInt(Request["rows"]);
        //if (tools.CheckInt(Request["page"]) == 0)
        //{
        //    Query.CurrentPage = 1;
        //}
        //else
        //{
        //    Query.CurrentPage = tools.CheckInt(Request["page"]);
        //}
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "QuestionInfo.ID", "<>", "0"));

        //int cate_id = tools.CheckInt(Request["Q_Cate"]);
        //if (cate_id > 0)
        //{

        //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "QuestionInfo.Q_Cate", "=", cate_id.ToString()));

        //}

        //if (keyword.Length > 0)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "QuestionInfo.Q_Question", "like", keyword));

        //}

        //Query.OrderInfos.Add(new OrderInfo("QuestionInfo.ID", "Desc"));
        //Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        //PageInfo pageinfo = Myquestion.GetPageInfo(Query, Public.GetUserPrivilege());
        //IList<QuestionInfo> entitys = Myquestion.GetQuestions(Query, Public.GetUserPrivilege());

        //if (entitys != null)
        //{

        //    StringBuilder jsonBuilder = new StringBuilder();
        //    jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
        //    jsonBuilder.Append(":[");
        //    foreach (QuestionInfo entity in entitys)
        //    {
        //        jsonBuilder.Append("{\"id\":" + entity.ID + ",\"cell\":[");

        //        jsonBuilder.Append("\"");
        //        jsonBuilder.Append(entity.ID);
        //        jsonBuilder.Append("\",");

        //        jsonBuilder.Append("\"");

        //        jsonBuilder.Append(GetQuestion_Cate_Name(entity.Q_Cate));
        //        jsonBuilder.Append("\",");

        //        jsonBuilder.Append("\"");
        //        jsonBuilder.Append(Public.JsonStr(entity.Q_Question));
        //        jsonBuilder.Append("\",");



        //        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        //        jsonBuilder.Append("]},");
        //    }
        //    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        //    jsonBuilder.Append("]");
        //    jsonBuilder.Append("}");

        //    entitys = null;
        //    return jsonBuilder.ToString();
        //}
        //else { return null; }
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
        IList<QuestionInfo> entitys = Myquestion.GetQuestions(Query, pub.CreateUserPrivilege("d1243e82-cc4e-4b77-a3c9-10c0eb60f499"));

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



    #region 套题管理
    public QuestionHistoryInfo GetQuestionsHistoryByID(int ID)
    {
        return MyquestionH.GetQuestionHistoryByID(ID, pub.CreateUserPrivilege("0727f3b4-4edc-4e49-94a0-d728fe7d35ef"));
    }

    public string GetQuestionsHistory()
    {

        return null;

    }

    public object GetQH_memberCount(int QHid)
    {
        string Sqlstr = "Select count(Consump_ID) from Member_Consumption where Consump_Qid=" + QHid;
        return DBHelper.ExecuteScalar(Sqlstr);
    }





    #endregion

    #region 投票管理
    public VoteInfo GetVoteByID(int ID)
    {
        return Myvote.GetVoteByID(ID);
    }


    public IList<VoteSelectInfo> GetVoteSelectByVoteID(int ID)
    {
        return Myvote.GetVoteSelectsByVoteID(ID);
    }

    public string AddVoteMember()
    {
        int Vote_ID = tools.CheckInt(Request["Vote_ID"]);
        int Vote_Select_VoteID = tools.CheckInt(Request["vote"]);
        if (Vote_Select_VoteID <= 0)
        {
            return pub.Msg_Json("请选择一个选项投票！", "");

        }
        if (Request.Cookies["VoteID" + Vote_ID] != null)
        {
            return pub.Msg_Json("您已投票不可再投！", "");
          
        }
        VoteInfo entity = GetVoteByID(Vote_ID);
        if (entity != null)
        {
            if (DateTime.Compare(entity.Vote_Start, DateTime.Today) <= 0 && DateTime.Compare(entity.Vote_End, DateTime.Today) >= 0)
            {


               

                    VoteMemberInfo one = new VoteMemberInfo();
                    one.Vote_Member_VoteID = Vote_ID;
                    one.Vote_Member_VoteSelectID = Vote_Select_VoteID;
                    one.Vote_Member_MemberID = tools.NullInt(Session["member_id"]);
                    one.Vote_Member_AddTime = DateTime.Now;
                    Myvote.AddVoteMember(one);
                    Myvote.UpdateVoteSelectNumber(Vote_Select_VoteID);
                    Myvote.UpdateVoteNumber(Vote_ID);


                    Response.Cookies["VoteID" + Vote_ID].Value = Vote_ID.ToString();
                    Response.Cookies["VoteID" + Vote_ID].Expires = DateTime.Now.AddDays(1);


                    //if (tools.NullInt(Session["member_id"]) > 0)
                    //{
                    //    if (GetVoteByMemberID(tools.NullInt(Session["member_id"]), Vote_ID) == 1)
                    //    {
                    //        //todo 投票成功增加积分？
                    //    }
                    //}
                    return pub.Msg_Json("", "/member/voteshow.aspx?vote_id="+Vote_ID);
                
               
            }
            else
            {
                return pub.Msg_Json("投票已到期，不可投票！", "");
            }
        }
        else
        {
            return pub.Msg_Json("投票不可用，请返回重新进入！", "");
        }
    }

    public int GetVoteByMemberID(int MemberID, int VoteID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "VoteMemberInfo.Vote_Member_ID", ">", "0"));

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "VoteMemberInfo.Vote_Member_MemberID", "=", MemberID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "VoteMemberInfo.Vote_Member_VoteID", "=", VoteID.ToString()));

        Query.OrderInfos.Add(new OrderInfo("VoteMemberInfo.Vote_Member_ID", "Desc"));

        IList<VoteMemberInfo> entitys = Myvote.GetVoteMembers(Query);
        if (entitys != null)
        {
            return entitys.Count;
        }
        else
        {
            return 0;
        }
    }
    #endregion

    #region 答题提交

    public string GetQuestionsHtml(QuestionHistoryInfo qhinfo)
    {
        StringBuilder sHtml = new StringBuilder();
        sHtml.Append("");
        sHtml.Append("");
        sHtml.Append("");
        sHtml.Append("");

        if (qhinfo != null)
        {
            string[] questionids = qhinfo.Q.Split('+');
            for (int i = 0; i < questionids.Length; i++)
            {
                QuestionInfo qinfo = Myquestion.GetQuestionByID(tools.CheckInt(questionids[i]), pub.CreateUserPrivilege("318a6535-6af3-4839-9393-816cbc75616d"));
                if (qhinfo != null)
                { 
                  //<dl>
                  //              <dt>1.《三十六计》是体现我国古代卓越军事思想的一部兵书，下列不属于《三十六计》的是：</dt>
                  //              <dd><i></i>A、浑水摸鱼</dd>
                  //              <dd><i></i>B、反戈一击</dd>
                  //              <dd><i></i>C、笑里藏刀</dd>
                  //              <dd><i></i>D、反客为主</dd>
                  //          </dl>

                    sHtml.Append("<dl><dt>"+(i+1)+"."+qinfo.Q_Question+"：</dt>");
                    sHtml.Append("<dd type='" + qinfo.ID + "' bind='A'><i></i>A、" + qinfo.Q_Option_A + "</dd><dd  type='" + qinfo.ID + "'  bind='B'><i></i>B、" + qinfo.Q_Option_B + "</dd><dd  type='" + qinfo.ID + "'  bind='C'><i></i>C、" + qinfo.Q_Option_C + "</dd><dd  type='" + qinfo.ID + "'  bind='D'><i></i>D、" + qinfo.Q_Option_D + "</dd>");
                    sHtml.Append("<input type=\"hidden\" value=\"\" id=\"q_" + qinfo.ID + "\" name=\"q_" + qinfo.ID + "\">");
                    sHtml.Append("</dl>");
               
                }

            }
            
        }
        return sHtml.ToString();
    }
    public string Question_Save()
    {
        string result = "本套题您共答对了 {0} 道，得分 {1}<br/>答对的题目：";
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        if (member_id == 0)
        {
            return pub.Msg_Json("登录已超时，请重新登录！", "");
        }
        int qhID=tools.CheckInt(Request["qhID"]);
        QuestionHistoryInfo qhinfo = GetQuestionsHistoryByID(qhID);
        if (qhinfo != null)
        {
            string[] questionids = qhinfo.Q.Split('+');
            for (int i = 0; i < questionids.Length; i++)
            {
                if (tools.CheckStr(Request["q_" + questionids[i] + ""]) == "")
                {
                    return pub.Msg_Json("请将竞赛试题答完！", "");
                }
            }
            int Adui=0;
            int souct=0;
            for (int i = 0; i < questionids.Length; i++)
            { 
             QuestionInfo qinfo = Myquestion.GetQuestionByID(tools.CheckInt(questionids[i]), pub.CreateUserPrivilege("318a6535-6af3-4839-9393-816cbc75616d"));
             if (qhinfo != null)
             {
                 if (qinfo.Q_Answer == tools.CheckStr(Request["q_" + questionids[i] + ""]))
                 {
                     Adui++;
                     souct = 10 + souct;
                     result += Adui + "、[" + qinfo.Q_Answer + "]";
                 }
             }

            }
            result=string.Format(result,Adui,souct);
            Member mem = new Member();
            mem.Member_Coin_AddConsume(souct, "科普竞答获取积分", member_id, false, qhID);
            mem = null;

            return pub.Msg_Json("", result);
        }
        else
        {
            return pub.Msg_Json("答题失败，请重新页面重试", "");
        }
    }
    #endregion

}