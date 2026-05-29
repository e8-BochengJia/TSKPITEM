using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Vote 的摘要说明
/// </summary>
public class Vote
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IVote MyBLL;
    public Vote()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = VoteFactory.CreateVote();
    }

    public string GetVoteSN()
    {
        bool ismatch = true;
        string SN = "";
        VoteInfo entity = null;

        SN = tools.FormatDate(DateTime.Now, "yyMMdd") + Public.Createvkey(6);

        while (ismatch == true)
        {
            entity = MyBLL.GetVoteBySN(SN);
            if (entity != null)
            {
                SN = tools.FormatDate(DateTime.Now, "yyMMdd") + Public.Createvkey(6);
            }
            else
            {
                ismatch = false;
            }
        }
        return SN;

    }

    public VoteInfo GetVoteByID(int ID)
    {
        return MyBLL.GetVoteByID(ID);
    }

    public VoteInfo GetVotyBySN(string SN)
    {
        return MyBLL.GetVoteBySN(SN);
    }

    public IList<VoteSelectInfo> GetVoteSelectByVoteID(int ID)
    {
        return MyBLL.GetVoteSelectsByVoteID(ID);
    }

    public virtual void AddVote()
    {
        int Vote_ID = tools.CheckInt(Request.Form["Vote_ID"]);
        string Vote_Name = tools.CheckStr(Request.Form["Vote_Name"]);
        if(Vote_Name.Length==0)
        {
            Public.Msg("error", "错误信息", "请填写投票名称", false, "{back}");
        }
        int Vote_Source = 0;
        string start = tools.NullStr(Request.Form["Vote_Start"]);
        if(start.Length==0)
        {
            Public.Msg("error", "错误信息", "请填写投票开始时间", false, "{back}");
        }
        string end= tools.NullStr(Request.Form["Vote_End"]);
        if (end.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写投票结束时间", false, "{back}");
        }
        DateTime Vote_Start = tools.NullDate(start);
        DateTime Vote_End = tools.NullDate(end);
        int Vote_IsActive = tools.CheckInt(Request.Form["Vote_IsActive"]);
        int Vote_Number = 0;
        DateTime Vote_AddTime =DateTime.Now;
        string Vote_Remarks = tools.CheckStr(Request.Form["Vote_Remarks"]);
        string Vote_SN = GetVoteSN();

        string Vote_Select_Name1 = tools.CheckStr(Request.Form["Vote_Select_Name_1"]);
        string Vote_Select_Name2 = tools.CheckStr(Request.Form["Vote_Select_Name_2"]);
        string Vote_Select_Name3 = tools.CheckStr(Request.Form["Vote_Select_Name_3"]);
        string Vote_Select_Name4 = tools.CheckStr(Request.Form["Vote_Select_Name_4"]);
        string Vote_Select_Name5 = tools.CheckStr(Request.Form["Vote_Select_Name_5"]);
        string Vote_Select_Name6 = tools.CheckStr(Request.Form["Vote_Select_Name_6"]);

        VoteInfo entity = new VoteInfo();
        entity.Vote_ID = Vote_ID;
        entity.Vote_Name = Vote_Name;
        entity.Vote_Source = Vote_Source;
        entity.Vote_Start = Vote_Start;
        entity.Vote_End = Vote_End;
        entity.Vote_IsActive = Vote_IsActive;
        entity.Vote_Number = Vote_Number;
        entity.Vote_AddTime = Vote_AddTime;
        entity.Vote_Remarks = Vote_Remarks;
        entity.Vote_SN = Vote_SN;

        if (MyBLL.AddVote(entity))
        {
            VoteInfo one = GetVotyBySN(Vote_SN);
            if(one!=null)
            {

                VoteSelectInfo selectinfo = new VoteSelectInfo();
                selectinfo.Vote_Select_ID = 0;
                
                selectinfo.Vote_Select_VoteID = one.Vote_ID;
                selectinfo.Vote_Select_Number = 0;

                if(Vote_Select_Name1.Length>0)
                {
                    selectinfo.Vote_Select_Name = Vote_Select_Name1;
                    MyBLL.AddVoteSelect(selectinfo);
                }
                if (Vote_Select_Name2.Length > 0)
                {
                    selectinfo.Vote_Select_Name = Vote_Select_Name2;
                    MyBLL.AddVoteSelect(selectinfo);
                }
                if (Vote_Select_Name3.Length > 0)
                {
                    selectinfo.Vote_Select_Name = Vote_Select_Name3;
                    MyBLL.AddVoteSelect(selectinfo);
                }
                if (Vote_Select_Name4.Length > 0)
                {
                    selectinfo.Vote_Select_Name = Vote_Select_Name4;
                    MyBLL.AddVoteSelect(selectinfo);
                }
                if (Vote_Select_Name5.Length > 0)
                {
                    selectinfo.Vote_Select_Name = Vote_Select_Name5;
                    MyBLL.AddVoteSelect(selectinfo);
                }
                if (Vote_Select_Name6.Length > 0)
                {
                    selectinfo.Vote_Select_Name = Vote_Select_Name6;
                    MyBLL.AddVoteSelect(selectinfo);
                }
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Vote_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditVote()
    {
        int Vote_ID = tools.CheckInt(Request.Form["Vote_ID"]);
        VoteInfo entity = GetVoteByID(Vote_ID);
        if(entity!=null)
        {
            string Vote_Name = tools.CheckStr(Request.Form["Vote_Name"]);
            if (Vote_Name.Length == 0)
            {
                Public.Msg("error", "错误信息", "请填写投票名称", false, "{back}");
            }
            string start = tools.NullStr(Request.Form["Vote_Start"]);
            if (start.Length == 0)
            {
                Public.Msg("error", "错误信息", "请填写投票开始时间", false, "{back}");
            }
            string end = tools.NullStr(Request.Form["Vote_End"]);
            if (end.Length == 0)
            {
                Public.Msg("error", "错误信息", "请填写投票结束时间", false, "{back}");
            }
            DateTime Vote_Start = tools.NullDate(start);
            DateTime Vote_End = tools.NullDate(end);
            int Vote_IsActive = tools.CheckInt(Request.Form["Vote_IsActive"]);
            string Vote_Remarks = tools.CheckStr(Request.Form["Vote_Remarks"]);

            entity.Vote_ID = Vote_ID;
            entity.Vote_Name = Vote_Name;
            entity.Vote_Start = Vote_Start;
            entity.Vote_End = Vote_End;
            entity.Vote_IsActive = Vote_IsActive;
            entity.Vote_Remarks = Vote_Remarks;

            if(MyBLL.EditVote(entity))
            {
                IList<VoteSelectInfo> voteselects = GetVoteSelectByVoteID(Vote_ID);
                int i = 0;
                string Vote_Select_Name = "";
                if (voteselects!=null)
                {
                    foreach(VoteSelectInfo one in voteselects)
                    {
                        i++;
                        Vote_Select_Name = tools.CheckStr(Request.Form["Vote_Select_Name_" + i]);
                        if(Vote_Select_Name.Length>0)
                        {
                            if (one.Vote_Select_Name != Vote_Select_Name)
                            {
                                one.Vote_Select_Name = Vote_Select_Name;
                                MyBLL.EditVoteSelect(one);
                            }
                        }
                        else
                        {
                            MyBLL.DelVoteSelect(one.Vote_Select_ID);
                        }
                        
                    }
                }
                while(i<6)
                {
                    i++;
                    Vote_Select_Name = tools.CheckStr(Request.Form["Vote_Select_Name_" + i]);
                    if(Vote_Select_Name.Length>0)
                    {
                        VoteSelectInfo selectinfo = new VoteSelectInfo();
                        selectinfo.Vote_Select_ID = 0;
                        selectinfo.Vote_Select_VoteID = Vote_ID;
                        selectinfo.Vote_Select_Number = 0;
                        selectinfo.Vote_Select_Name = Vote_Select_Name;
                        MyBLL.AddVoteSelect(selectinfo);
                        selectinfo = null;
                    }
                }
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Vote_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelVote()
    {
        int Vote_ID = tools.CheckInt(Request.QueryString["Vote_ID"]);
        if (MyBLL.DelVote(Vote_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Vote_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public string GetVoteIsActive(int Active)
    {
        string Name = "";
        switch(Active)
        {
            case 0:
                Name = "未启用";
                break;
            case 1:
                Name = "启用";
                break;
        }

        return Name;
    }

    public string GetVotes()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "VoteInfo.Vote_ID", ">", "0"));

        if(keyword.Length>0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "VoteInfo.Vote_Name", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetVotePageInfo(Query);
        IList<VoteInfo> entitys = MyBLL.GetVotes(Query);
        if(entitys!=null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");

            foreach (VoteInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Vote_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Vote_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Vote_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Vote_Start.ToString("yyyy-MM-dd"));
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Vote_End.ToString("yyyy-MM-dd"));
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetVoteIsActive(entity.Vote_IsActive));
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Vote_Number);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");


                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"vote_view.aspx?Vote_ID=" + entity.Vote_ID + "\\\" title=\\\"查看\\\" >查看</a>");
                if (Public.CheckPrivilege("2d0fab9b-e8f0-4c3f-9fe8-949f4416ef1f"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"vote_edit.aspx?Vote_ID=" + entity.Vote_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("41e28e33-14e4-45d2-9e23-a63cd706f0e9"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('vote_do.aspx?action=move&Vote_ID=" + entity.Vote_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
        else
        {
            return null;
        }
    }
}