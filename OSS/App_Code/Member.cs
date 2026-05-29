using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Sys;
using Glaer.Trade.Util.SQLHelper;

/// <summary>
///Member 的摘要说明
/// </summary>
public class Member
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IMember MyBLL;
    private IMail mail;
    private MemberGrade MyGrade;
    private IMemberGrade MyMGBLL;
    private IMemberConsumption MyCoinlog;
     

    public Member()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = MemberFactory.CreateMember();
 
        mail = MailFactory.CreateMail();
    
        MyGrade = new MemberGrade();
        MyMGBLL = MemberGradeFactory.CreateMemberGrade();
        MyCoinlog = MemberConsumptionFactory.CreateMemberConsumption();
       
        //myerp = new ERPProcess();
    }

    /// <summary>
    /// 检查注册邮箱是否使用
    /// </summary>
    /// <param name="Member_Email"></param>
    /// <returns></returns>
    public bool Check_Member_Email(string Member_Email)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Email", "=", Member_Email));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        PageInfo page = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        if (page != null)
        {
            if (page.RecordCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 检查注册手机号是否使用
    /// </summary>
    /// <param name="Member_LoginMobile"></param>
    /// <returns></returns>
    public bool Check_Member_LoginMobile(string Member_LoginMobile)
    {
        Glaer.Trade.Util.SQLHelper.ISQLHelper DBHelper = Glaer.Trade.Util.SQLHelper.SQLHelperFactory.CreateSQLHelper();
        try
        {
            int count = tools.NullInt(DBHelper.ExecuteScalar("SELECT Member_ID FROM Member WHERE Member_LoginMobile = '" + Member_LoginMobile + "' AND Member_Trash = 0 AND Member_Site='CN'"));
            if (count > 0) { return true; } else { return false; }
        }
        catch (Exception)
        {
            return false;
        }
    }
    //检查手机号
    public bool Checkmobile(string check_str)
    {
        bool result = true;
        if (check_str.Length != 11)
        {
            result = false;
        }
        if (result)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("1[0-9]{10}");
            result = regex.IsMatch(check_str);
        }


        return result;
    }


    /// <summary>
    /// 检查昵称是否使用 存在返回ID
    /// </summary>
    /// <param name="nick_name"></param>
    /// <returns></returns>
    public int Check_Member_NicknameGETid(string nick_name)
    {

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;



        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));

        Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_NickName", "=", nick_name));
        Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "=", nick_name));
        Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberInfo.Member_LoginMobile", "=", nick_name));

        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> infos = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
        //IList<MemberInfo> infos = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        if (infos != null)
        {
            if (infos.Count > 0)
            {
                return infos[0].Member_ID;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }


    public void EditMember_Recommend_ByID()
    {

        if (Public.CheckPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"))
        {
            int member_id = tools.CheckInt(Request["member_id"]);
            string memberRecommend = tools.CheckStr(Request["memberRecommend"]);

            MemberInfo entity = GetMemberByID(member_id);
            if (entity != null)
            {
                string U_Member_Recommend = "";
                int RecID = Check_Member_NicknameGETid(memberRecommend);
                if (RecID > 0)
                {
                    U_Member_Recommend = RecID.ToString();
                }
                else
                {
                    Public.Msg("error", "错误提示", "推荐人不存在,请输入正确的推荐人电话或邮箱！", false, "{back}");
                    Response.End();
                }


                if (MyBLL.EditMember(entity, Public.GetUserPrivilege()))
                {
                    Public.Msg("info", "提示", "修改成功", false, "/member/member_view.aspx?member_id=" + member_id);
                    Response.End();
                }
                else
                {
                    Public.Msg("error", "错误提示", "操作失败", false, "{back}");
                    Response.End();
                }
            }
            else
            {
                Public.Msg("error", "错误提示", "操作失败", false, "{back}");
                Response.End();
            }

        }
        else
        {
            Public.Msg("error", "错误提示", "您没有权限", false, "{back}");
            Response.End();
        }
    }

    public void EditMember_Grade_ByID()
    {

        if (Public.CheckPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"))
        {
            int member_id = tools.CheckInt(Request["member_id"]);
            int Grad_id = tools.CheckInt(Request["memberGrade"]);

            MemberInfo entity = GetMemberByID(member_id);
            if (entity != null)
            {
                entity.Member_Grade = (Grad_id == 0 ? entity.Member_Grade : Grad_id);
                if (MyBLL.EditMember(entity, Public.GetUserPrivilege()))
                {
                    Public.Msg("info", "提示", "修改成功", false, "/member/member_view.aspx?member_id=" + member_id);
                    Response.End();
                }
                else
                {
                    Public.Msg("error", "错误提示", "操作失败", false, "{back}");
                    Response.End();
                }
            }
            else
            {
                Public.Msg("error", "错误提示", "操作失败", false, "{back}");
                Response.End();
            }

        }
        else
        {
            Public.Msg("error", "错误提示", "您没有权限", false, "{back}");
            Response.End();
        }
    }



    public void EditMember_Email_ByID()
    {

        if (Public.CheckPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"))
        {
            int member_id = tools.CheckInt(Request["member_id"]);
            string email = tools.CheckStr(Request["memberEmail"]);


            if (email == "")
            {
                Public.Msg("error", "错误提示", "请输入E-mail！", false, "{back}");
                Response.End();
            }
            else
            {
                if (tools.CheckEmail(email))
                {
                    if (Check_Member_Email(email))
                    {
                        Public.Msg("error", "错误提示", "该邮件地址已被使用！", false, "{back}");
                        Response.End();
                    }
                    else
                    {
                        MemberInfo entity = GetMemberByID(member_id);
                        if (entity != null)
                        {
                            entity.Member_Email = email;
                            entity.Member_Emailverify = 1;
                            if (MyBLL.EditMember(entity, Public.GetUserPrivilege()))
                            {
                                Public.Msg("info", "提示", "修改成功", false, "/member/member_view.aspx?member_id=" + member_id);
                                Response.End();
                            }
                            else
                            {
                                Public.Msg("error", "错误提示", "操作失败", false, "{back}");
                                Response.End();
                            }
                        }
                        else
                        {
                            Public.Msg("error", "错误提示", "操作失败", false, "{back}");
                            Response.End();
                        }
                    }
                }
                else
                {
                    Public.Msg("error", "错误提示", "无效的E-mail！", false, "{back}");
                    Response.End();

                }
            }



        }
        else
        {
            Public.Msg("error", "错误提示", "您没有权限", false, "{back}");
            Response.End();
        }
    }

    public void EditMember_Mobile_ByID()
    {

        if (Public.CheckPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"))
        {
            int member_id = tools.CheckInt(Request["member_id"]);
            string memberMobile = tools.CheckStr(Request["memberMobile"]);


            if (memberMobile == "")
            {
                Public.Msg("error", "错误提示", "请输入手机号码！", false, "{back}");
                Response.End();
            }
            else
            {
                if (Checkmobile(memberMobile))
                {
                    if (Check_Member_LoginMobile(memberMobile))
                    {
                        Public.Msg("error", "错误提示", "该手机号码已被使用！", false, "{back}");
                        Response.End();
                    }
                    else
                    {
                        MemberInfo entity = GetMemberByID(member_id);
                        if (entity != null)
                        {
                            entity.Member_LoginMobile = memberMobile;
                            entity.Member_LoginMobileverify = 1;
                            if (MyBLL.EditMember(entity, Public.GetUserPrivilege()))
                            {
                                Public.Msg("info", "提示", "修改成功", false, "/member/member_view.aspx?member_id=" + member_id);
                                Response.End();
                            }
                            else
                            {
                                Public.Msg("error", "错误提示", "操作失败", false, "{back}");
                                Response.End();
                            }
                        }
                        else
                        {
                            Public.Msg("error", "错误提示", "操作失败", false, "{back}");
                            Response.End();
                        }
                    }
                }
                else
                {
                    Public.Msg("error", "错误提示", "无效的手机号码！", false, "{back}");
                    Response.End();

                }
            }



        }
        else
        {
            Public.Msg("error", "错误提示", "您没有权限", false, "{back}");
            Response.End();
        }
    }

    public MemberInfo GetMemberByID(int ID)
    {
        return MyBLL.GetMemberByID(ID, Public.GetUserPrivilege());
    }

    public MemberInfo GetMemberByNickName(string nickname)
    {
        return MyBLL.GetMemberByNickName(nickname, Public.GetUserPrivilege());
    }
    public string GetMemberNameByID(int ID)
    {
        string name = "";
        MemberInfo minfo = MyBLL.GetMemberByID(ID, Public.GetUserPrivilege());
        if (minfo != null)
        {
            name = minfo.Member_NickName;
        }
        return name;
    }

    //public string GetGradeName(int gradeid)
    //{
    //    MemberGradeInfo membergrade = MyGrade.GetMemberGradeByID(gradeid);
    //    if (membergrade != null)
    //    {
    //        return membergrade.Member_Grade_Name;
    //    }
    //    else
    //    {
    //        return "--";
    //    }
    //}

    public string GetMembers()
    {
        int member_grade = tools.CheckInt(Request["member_grade"]);
        string member_source = tools.CheckStr(Request["member_source"]);
        string date_start = Public.CheckDateTime(Request.QueryString["date_start"]);
        string date_end = Public.CheckDateTime(Request.QueryString["date_end"]);
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
        int member_state = tools.CheckInt(Request["member_state"]);

        //string listtype = tools.CheckStr(Request.QueryString["listtype"]);
        //switch (listtype)
        //{
        //    case "activate":
        //        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Emailverify", "=", "1"));
        //        break;
        //    case "inactive":
        //        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Emailverify", "=", "0"));
        //        break;
        //    case "emailsubscribe":
        //        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_AllowSysEmail", "=", "1"));
        //        break;
        //}
        if (member_state != 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.U_Member_State", "=", (member_state - 1).ToString()));
        }
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_NickName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_LoginMobile", "=", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberInfo.U_Member_Realname", "like", keyword));
          
        }
       
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{MemberInfo.Member_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{MemberInfo.Member_Addtime})", "<=", "0"));
        }
      
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());




        #region 积分统计
        ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();
        double MemberAllCoin = 0;

        string SqlList = "SELECT SUM(Member_CoinRemain) AS MemberAllCoin";

        SqlList += " FROM Member " + DBHelper.GetSqlParam(Query.ParamInfos);

        DataTable OPTable = DBHelper.Query(SqlList);
        if (OPTable != null && OPTable.Rows.Count > 0)
        {
            MemberAllCoin = tools.NullDbl(OPTable.Rows[0]["MemberAllCoin"]);

        }

        #endregion



        if (entitys != null)
        {
           
        

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberInfo entity in entitys)
            {
              

                jsonBuilder.Append("{\"id\":" + entity.Member_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href=\\\"member_view.aspx?member_id=" + entity.Member_ID + "\\\" title=\\\"查看\\\">"+Public.JsonStr(entity.Member_NickName)+"</a");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Member_Email));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.U_Member_Realname);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplaySex(entity.U_Member_Male));
                jsonBuilder.Append("\",");

            

          

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a style='color:#EB5E00' href=\\\"coin_detail.aspx?keyword=" + entity.Member_NickName + "\\\" title=\\\"查看明细\\\">" + entity.Member_CoinRemain + "</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a style='color:#EB5E00' href=\\\"coin_detail.aspx?keyword=" + entity.Member_NickName + "\\\" title=\\\"查看明细\\\">" + entity.Member_CoinCount + "</a>");
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.U_Member_Article_Commend);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_LoginCount);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.U_Member_City);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Addtime.ToString("yy-MM-dd"));
                jsonBuilder.Append("\",");

                if (entity.U_Member_State == 0)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("正常");
                    jsonBuilder.Append("\",");
                }
                else {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("冻结");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"member_view.aspx?member_id=" + entity.Member_ID + "\\\" title=\\\"查看\\\">查看</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

             
            }

            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");



            jsonBuilder.Append(", \"userdata\":{\"MemberAllCoin\":\"" + MemberAllCoin + "\"}");








            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    ///// <summary>
    ///// 获得会员可用积分
    ///// </summary>
    ///// <param name="Member_ID"></param>
    ///// <returns></returns>
    //public double GetMemberCoinRemain(MemberInfo memEntity)
    //{
    //    if (memEntity == null) return 0;

    //    if (memEntity.U_Member_CardVerify == 1)
    //    {
    //        return 0;
    //        //return myerp.GetRemainCoin(memEntity.U_Member_CardNo);
    //    }

    //    try { return memEntity.Member_CoinRemain; }
    //    catch (Exception ex) { return 0; }
    //}

    ///// <summary>
    ///// 修改会员实体积分字段
    ///// </summary>
    ///// <param name="memEntity"></param>
    ///// <returns></returns>
    //public MemberInfo GetMemberCoin(MemberInfo memEntity)
    //{
    //    if (memEntity != null && memEntity.U_Member_CardVerify == 1)
    //    {
    //        Dictionary<string, string> result = myerp.GetPoints(memEntity.U_Member_CardNo);
    //        try
    //        {
    //            memEntity.Member_CoinCount = tools.NullDbl(result["total"]);
    //            memEntity.Member_CoinRemain = tools.NullDbl(result["available"]);
    //        }
    //        catch { }
    //    }

    //    return memEntity;
    //}

    //根据昵称关键词获取指定条件会员编号
    public string GetMemberIDByKeyword(string keyword)
    {
        string MemberID = "";
        if (keyword.Length == 0)
        {
            return "0";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_NickName", "like", keyword));

        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (MemberInfo entity in entitys)
            {
                if (MemberID == "")
                {
                    MemberID = entity.Member_ID.ToString();
                }
                else
                {
                    MemberID = MemberID + "," + entity.Member_ID.ToString();
                }
            }
        }
        if (MemberID == "")
        {
            MemberID = "0";
        }
        return MemberID;

    }



    ////会员虚拟账号消费
    //public void Member_Account_Log(int Member_ID, double Amount, string Log_note)
    //{
    //    double Member_AccountRemain = 0;
    //    MemberInfo member = MyBLL.GetMemberByID(Member_ID, Public.GetUserPrivilege());
    //    if (member != null)
    //    {
    //        Member_AccountRemain = member.Member_Account;
    //        MemberAccountLogInfo accountLog = new MemberAccountLogInfo();
    //        accountLog.Account_Log_ID = 0;
    //        accountLog.Account_Log_MemberID = Member_ID;
    //        accountLog.Account_Log_Amount = Amount;
    //        accountLog.Account_Log_Remain = Member_AccountRemain + Amount;
    //        accountLog.Account_Log_Note = Log_note;
    //        accountLog.Account_Log_Addtime = DateTime.Now;
    //        accountLog.Account_Log_Site = Public.GetCurrentSite();

    //        MyAccountLog.AddMemberAccountLog(accountLog);

    //        if (Amount > 0)
    //        {
    //            member.Member_Account = Member_AccountRemain + Amount;
    //        }

    //        MyBLL.EditMember(member, Public.GetUserPrivilege());
    //    }
    //}



  
    //会员导出
    public void Member_Export()
    {
        string MembersArry = tools.CheckStr(Request["member_id"]);
        if (MembersArry == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }

        if (tools.Left(MembersArry, 1) == ",") { MembersArry = MembersArry.Remove(0, 1); }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;
   

        string[] dtcol = { "序号","ID", "会员名", "邮箱", "姓名", "性别", "电话", "生日", "QQ", "MSN", "城市", "联系地址","邮编","职业","学历","身份证号码","有效积分","登录数","文章数", "注册时间" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        int Orders_ID = 0;
        QueryInfo Query = new QueryInfo();
        MemberInfo memberinfo = null;
       
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_ID", "in", MembersArry));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "DESC"));

        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
         
            Glaer.Trade.Util.SQLHelper.ISQLHelper DBHelper = Glaer.Trade.Util.SQLHelper.SQLHelperFactory.CreateSQLHelper();
            int icount = 1;
            foreach (MemberInfo entity in entitys)
            {
               
                //membergrade = MyGrade.GetMemberGradeByID(entity.Member_Grade);


                dr = dt.NewRow();
                dr[0] = icount;
                dr[1] = entity.Member_ID;
                dr[2] = entity.Member_NickName;
                dr[3] = entity.Member_Email;
                dr[4] = entity.U_Member_Realname;
                dr[5] = Public.DisplaySex(entity.U_Member_Male);
                dr[6] = entity.Member_LoginMobile;

                dr[7] = entity.U_MeMber_Birth;
               
                dr[8] = entity.U_Member_QQ;
                dr[9] = entity.U_Member_MSN;
                dr[10] = entity.U_Member_City;
                dr[11] = entity.U_Member_Country + " " + entity.U_Member_Province + " " + entity.U_Member_City + " " + entity.U_Member_Address;
                dr[12] = entity.U_Member_Postcode;
                dr[13] = entity.U_Member_Job;
                dr[14] = entity.U_Member_Edu;
                dr[15] = entity.U_Member_IDCard;
            
                dr[16] = entity.Member_CoinRemain;
                dr[17] = entity.Member_LoginCount;
                dr[18] = entity.U_Member_Article_Commend;
                dr["注册时间"] = entity.Member_Addtime;
         

              
                dt.Rows.Add(dr);
                icount++;
            }
        }




        Public.toExcel(dt);
    }


    /// <summary>
    /// 导出全部会员
    /// </summary>
    /// <returns></returns>
    public void Member_Export_All()
    {
        int member_grade = tools.CheckInt(Request["member_grade"]);
        string member_source = tools.CheckStr(Request["member_source"]);
        string date_start = tools.CheckStr(Request.QueryString["date_start"]);
        string date_end = tools.CheckStr(Request.QueryString["date_end"]);
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));

       
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_NickName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "like", keyword));

            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberInfo.U_Member_Realname", "like", keyword));
        }
        if (member_grade > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Grade", "=", member_grade.ToString()));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{MemberInfo.Member_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{MemberInfo.Member_Addtime})", "<=", "0"));
        }
       
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;
        string[] dtcol = { "序号", "ID", "会员名", "邮箱", "姓名", "性别", "电话", "生日", "QQ", "MSN", "城市", "联系地址", "邮编", "职业", "学历", "身份证号码", "有效积分", "登录数", "文章数", "注册时间" };
       
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        if (entitys != null)
        {
            int icount = 1;
            foreach (MemberInfo entity in entitys)
            {
           
                dr = dt.NewRow();

                dr[0] = icount;
                dr[1] = entity.Member_ID;
                dr[2] = entity.Member_NickName;
                dr[3] = entity.Member_Email;
                dr[4] = entity.U_Member_Realname;
                dr[5] = Public.DisplaySex(entity.U_Member_Male);
                dr[6] = entity.Member_LoginMobile;

                dr[7] = entity.U_MeMber_Birth;

                dr[8] = entity.U_Member_QQ;
                dr[9] = entity.U_Member_MSN;
                dr[10] = entity.U_Member_City;
                dr[11] = entity.U_Member_Country + " " + entity.U_Member_Province + " " + entity.U_Member_City + " " + entity.U_Member_Address;
                dr[12] = entity.U_Member_Postcode;
                dr[13] = entity.U_Member_Job;
                dr[14] = entity.U_Member_Edu;
                dr[15] = entity.U_Member_IDCard;

                dr[16] = entity.Member_CoinRemain;
                dr[17] = entity.Member_LoginCount;
                dr[18] = entity.U_Member_Article_Commend;
                dr["注册时间"] = entity.Member_Addtime;

                dt.Rows.Add(dr);
                icount++;
            }
        }

        Public.toExcel(dt);
    }

    //用户积分处理
    public void Member_Coin_Process()
    {
     

        string Member_ID = tools.NullStr(Request.Form["favor_memberid"]);
        int member_all = tools.CheckInt(Request.Form["favor_memberall"]);
        if (member_all == 1)
        {
            Member_ID = "";
        }
    
        int coin_amount = tools.CheckInt(Request["coin_amount"]);
        string coin_reason = tools.CheckStr(Request["coin_reason"]);
   
        int coin_remain = 0;

        if (coin_amount == 0 || coin_reason == "")
        {
            Public.Msg("error", "错误提示", "请将输入要操作用户名\\积分\\备注", false, "{back}");
            Response.End();
        }
        if (member_all == 0 && Member_ID == "")
        {
            Public.Msg("error", "错误提示", "请选择操作的会员", false, "{back}");
            Response.End();
        }
        if (Member_ID != "")
        {
            foreach (string Promotion_Coupon_MemberID in Member_ID.Split(','))
            {
                MemberInfo memberinfo = MyBLL.GetMemberByID(tools.CheckInt(Promotion_Coupon_MemberID), Public.GetUserPrivilege());
                int member_id = 0;
                if (memberinfo != null)
                {

                    member_id = memberinfo.Member_ID;
                    coin_remain = memberinfo.Member_CoinRemain;
                }
                memberinfo = null;
                if (member_id == 0)
                {
                    //Public.Msg("error", "错误提示", "用户不存在", false, "{back}");
                    //Response.End();
                    continue;
                }
                if (coin_amount < (0 - coin_remain))
                {
                    //Public.Msg("error", "错误提示", "扣除积分超过会员可用积分", false, "{back}");
                    //Response.End();
                    continue;
                }
                Member_Coin_AddConsume(coin_amount, coin_reason, member_id, false);

            }
            Public.Msg("positive", "操作成功", "操作成功", true, "coin_detail.aspx");
        }
        else
        {
            QueryInfo Query = new QueryInfo();

            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
            Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "desc"));

            //PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
            IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                foreach (MemberInfo memberinfo in entitys)
                {

                    int member_id = 0;

                    member_id = memberinfo.Member_ID;
                    coin_remain = memberinfo.Member_CoinRemain;

                    if (coin_amount < (0 - coin_remain))
                    {
                        //Public.Msg("error", "错误提示", "扣除积分超过会员可用积分", false, "{back}");
                        //Response.End();
                        continue;
                    }
                    Member_Coin_AddConsume(coin_amount, coin_reason, member_id, false);
                }
                Public.Msg("positive", "操作成功", "操作成功", true, "coin_detail.aspx");
            }
            else
            {
                Public.Msg("error", "错误提示", "操作失败", false, "{back}");
            }
        }




    }

    //会员积分消费
    public void Member_Coin_AddConsume(int coin_amount, string coin_reason, int member_id, bool is_return)
    {
        int Member_CoinRemain = 0;
        MemberInfo member = MyBLL.GetMemberByID(member_id, Public.GetUserPrivilege());
        if (member != null)
        {
            Member_CoinRemain = member.Member_CoinRemain;
            MemberConsumptionInfo consumption = new MemberConsumptionInfo();
            consumption.Consump_ID = 0;
            consumption.Consump_MemberID = member_id;
            consumption.Consump_Coin = coin_amount;
            consumption.Consump_CoinRemain = Member_CoinRemain + coin_amount;
            consumption.Consump_Reason = coin_reason;
            consumption.Consump_Addtime = DateTime.Now;

            MyCoinlog.AddMemberConsumption(consumption);

            if (coin_amount > 0)
            {
                if (is_return)
                {
                
                    member.Member_CoinRemain = Member_CoinRemain + coin_amount;
                    member.Member_CoinCount = member.Member_CoinCount + coin_amount;
                    member.U_Member_Article_Commend = member.U_Member_Article_Commend + 1;
                }
                else
                {
                    member.Member_CoinRemain = Member_CoinRemain + coin_amount;
                    member.Member_CoinCount = member.Member_CoinCount + coin_amount;
                }
            }
            else
            {
                member.Member_CoinRemain = Member_CoinRemain + coin_amount;
            }

            MyBLL.EditMember(member, Public.GetUserPrivilege());
        }
    }

    //用户虚拟账户处理
    public void Member_Account_Process()
    {
        string member_nickname = tools.CheckStr(Request["member_name"]);
        double account_amount = tools.CheckInt(Request["account_amount"]);
        string account_reason = tools.CheckStr(Request["account_reason"]);
        int member_id = 0;
        double account_remain = 0;

        if (member_nickname == "" || account_amount == 0 || account_reason == "")
        {
            Public.Msg("error", "错误提示", "请将输入要操作用户名\\金额\\备注", false, "{back}");
            Response.End();
        }
        MemberInfo memberinfo = MyBLL.GetMemberByNickName(member_nickname, Public.GetUserPrivilege());
        if (memberinfo != null)
        {
            member_id = memberinfo.Member_ID;
            account_remain = memberinfo.Member_Account;
        }
        memberinfo = null;
        if (member_id == 0)
        {
            Public.Msg("error", "错误提示", "用户不存在", false, "{back}");
            Response.End();
        }
        if (account_amount < (0 - account_remain))
        {
            Public.Msg("error", "错误提示", "扣除金额超过会员虚拟账户余额", false, "{back}");
            Response.End();
        }
        //Member_Account_Log(member_id, account_amount, account_reason);
        Public.Msg("positive", "操作成功", "操作成功", true, "Account_detail.aspx");
    }

    public string Get_MemberNickname(int member_id)
    {
        string member_nickname = "";
        MemberInfo entity = GetMemberByID(member_id);
        if (entity != null)
        {
            member_nickname = entity.Member_NickName;
        }
        return member_nickname;
    }

  

    //发送邮件处理
    public void Send_Sysemail()
    {

        string member_id = "";
        member_id = tools.CheckStr(Request["member_id"]);
        if (member_id == "")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));


            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_AllowSysEmail", "=", "1"));

            Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Asc"));

            IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                foreach (MemberInfo entity in entitys)
                {
                    if (member_id == "")
                    {
                        member_id = entity.Member_ID.ToString();
                    }
                    else
                    {
                        member_id = member_id + "," + entity.Member_ID.ToString();
                    }
                }
            }
        }

        string sysmail_title = tools.CheckStr(Request.Form["sysmail_title"]);
        string sysmail_content = tools.CheckHTML(Request.Form["sysmail_content"]);

        //FORM重复提交
        string tmp_str = "";
        tmp_str = tmp_str + "<html>";
        tmp_str = tmp_str + "<head>";
        tmp_str = tmp_str + "<title>管理平台</title>";
        tmp_str = tmp_str + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">";
        tmp_str = tmp_str + "<link rel=\"stylesheet\" href=\"/public/style.css\" type=\"text/css\">";
        tmp_str = tmp_str + "</head>";
        tmp_str = tmp_str + "<body bgcolor=\"#FFFFFF\" text=\"#000000\" onload=\"document.form1.submit();\">";
        tmp_str = tmp_str + "<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">";
        tmp_str = tmp_str + "  <form name=\"form1\" method=\"post\" action=\"sysemail_do.aspx\" >";
        tmp_str = tmp_str + "\t<tr>";
        tmp_str = tmp_str + "\t  <td>";
        tmp_str = tmp_str + "\t <textarea name=\"sysmail_content\" id=\"sysmail_content\" style=\"display:none;\">" + sysmail_content + "</textarea>";
        tmp_str = tmp_str + "\t <input name=\"sysmail_title\" type=\"hidden\" id=\"sysmail_title\" value=\"" + sysmail_title + "\" >";
        tmp_str = tmp_str + "\t <input name=\"member_id\" type=\"hidden\" id=\"member_id\" value=\"" + member_id + "\" >";
        tmp_str = tmp_str + "\t <input name=\"page\" type=\"hidden\" id=\"page\" value=\"1\" >";
        tmp_str = tmp_str + "\t  </td>";
        tmp_str = tmp_str + "\t</tr>";
        tmp_str = tmp_str + "  </form>";
        tmp_str = tmp_str + "</table>";
        tmp_str = tmp_str + "</body>";
        tmp_str = tmp_str + "</html>";
        Response.Write(tmp_str);
        Response.End();

    }

    //发送订阅邮件
    public void Member_Sysemail_Send()
    {

        //取得上一页参数
        string sysmail_title, sysmail_content, member_id, member_arry, member_email;

        sysmail_title = Request.Form["sysmail_title"];
        sysmail_content = Request.Form["sysmail_content"];
        member_id = Request.Form["member_id"];
        member_email = "";
        member_arry = "";

        //处理参数
        int page = 0;

        int ii = 0;
        page = tools.CheckInt(Request["page"]);
        MemberInfo entity;


        //发送Email
        if (member_id.Length > 0)
        {
            foreach (string subid in member_id.Split(','))
            {
                if (tools.CheckInt(subid) > 0)
                {
                    entity = MyBLL.GetMemberByID(tools.CheckInt(subid), Public.GetUserPrivilege());
                    if (entity != null)
                    {
                        if (member_email != "")
                        {
                            if (member_arry == "")
                            {
                                member_arry = subid;
                            }
                            else
                            {
                                member_arry = member_arry + "," + subid;
                            }
                        }
                        if (member_arry == "")
                        {
                            member_email = entity.Member_Email;
                        }

                    }

                }
            }
        }

        if (member_email.Length > 0)
        {
            Sendmail(member_email, sysmail_title, sysmail_title, sysmail_content);
        }
        //FORM重复提交
        string tmp_str = "";
        tmp_str = tmp_str + "<html>";
        tmp_str = tmp_str + "<head>";
        tmp_str = tmp_str + "<title>管理平台</title>";
        tmp_str = tmp_str + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">";
        tmp_str = tmp_str + "<link rel=\"stylesheet\" href=\"/css/style.css\" type=\"text/css\">";
        tmp_str = tmp_str + "</head>";

        if (member_id != "")
        {
            member_id = member_arry;

            tmp_str = tmp_str + "<body style=\"margin:10px;\" onload=\"document.form1.submit();\">";
            tmp_str = tmp_str + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\" class=\"content_table\">";
            tmp_str = tmp_str + "  <tr> ";
            tmp_str = tmp_str + "    <td height=\"25\" class=\"content_title\">邮件发送中……</td>";
            tmp_str = tmp_str + "  </tr>";
            tmp_str = tmp_str + "  <tr> ";
            tmp_str = tmp_str + "    <td height=\"30\" class=\"t14red\">";
            tmp_str = tmp_str + "\t<table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">";
            tmp_str = tmp_str + "\t  <tr> ";
            tmp_str = tmp_str + "        <td width=\"60\" height=\"60\"></td>";
            tmp_str = tmp_str + "        <td width=\"60\"><img src=\"/images/loading.gif\"></td>";
            tmp_str = tmp_str + "\t\t<td align=\"left\" class=\"t14_red\">邮件发送中，请不要关闭窗口……" + member_email + "</td>";
            tmp_str = tmp_str + "\t  </tr>";
            tmp_str = tmp_str + "\t</table>";
            tmp_str = tmp_str + "\t</td>";
            tmp_str = tmp_str + "  </tr>";
            tmp_str = tmp_str + "</table>";
            tmp_str = tmp_str + "<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">";
            tmp_str = tmp_str + "  <form name=\"form1\" method=\"post\" action=\"?\">";
            tmp_str = tmp_str + "\t<tr>";
            tmp_str = tmp_str + "\t  <td>";
            tmp_str = tmp_str + "\t <textarea name=\"sysmail_content\" id=\"sysmail_content\" style=\"display:none;\">" + sysmail_content + "</textarea>";
            tmp_str = tmp_str + "\t <input name=\"sysmail_title\" type=\"hidden\" id=\"sysmail_title\" value=\"" + sysmail_title + "\" >";
            tmp_str = tmp_str + "\t <input name=\"member_id\" type=\"hidden\" id=\"member_id\" value=\"" + member_id + "\" >";
            tmp_str = tmp_str + "\t <input name=\"page\" type=\"hidden\" id=\"page\" value=\"1\" >";
            tmp_str = tmp_str + "\t  </td>";
            tmp_str = tmp_str + "\t</tr>";
            tmp_str = tmp_str + "  </form>";
            tmp_str = tmp_str + "</table>";
        }
        else
        {
            tmp_str = tmp_str + "<body style=\"margin:10px;\">";
            tmp_str = tmp_str + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\" class=\"content_table\">";
            tmp_str = tmp_str + "  <tr> ";
            tmp_str = tmp_str + "    <td height=\"25\" class=\"content_title\">管理平台</td>";
            tmp_str = tmp_str + "  </tr>";
            tmp_str = tmp_str + "  <tr> ";
            tmp_str = tmp_str + "    <td height=\"30\" class=\"t14red\">";
            tmp_str = tmp_str + "\t<table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">";
            tmp_str = tmp_str + "\t  <tr> ";
            tmp_str = tmp_str + "        <td width=\"60\" height=\"60\"></td>";
            tmp_str = tmp_str + "        <td width=\"60\"><img src=\"/images/icon_alert_b.gif\" width=\"50\" height=\"50\"></td>";
            tmp_str = tmp_str + "\t\t<td align=\"left\" class=\"t14_red\">邮件发送成功！</td>";
            tmp_str = tmp_str + "\t  </tr>";
            tmp_str = tmp_str + "\t</table>";
            tmp_str = tmp_str + "\t</td>";
            tmp_str = tmp_str + "  </tr>";
            tmp_str = tmp_str + "</table>";

        }
        tmp_str = tmp_str + "</body>";
        tmp_str = tmp_str + "</html>";
        Response.Write(tmp_str);

    }

    //会员选择
    public string SelectMember()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入昵称、邮箱、姓名、手机进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }

        //IList<MemberInfo> entityList = (IList<MemberInfo>)Session["EmailMemberInfo"];
        //string memberSelected = "0";

        //foreach (MemberInfo mminfo in entityList)
        //{
        //    memberSelected += "," + mminfo.Member_ID.ToString();
        //}

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
     

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
       
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_NickName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_LoginMobile", "=", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberInfo.U_Member_Realname", "like", keyword));
        }


        //if (memberSelected.Length > 0)
        //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_ID", "not in", memberSelected));

        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "DESC"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberInfo entity in entitys)
            {
               
                jsonBuilder.Append("{\"id\":" + entity.Member_ID + ",\"cell\":[");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Member_NickName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Member_Email));
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

    //展示选择会员
    public string ShowMember()
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"60\"><input type=\"button\" value=\"添加\" onclick=\"SelectMember()\" class=\"bt_orange\"></td>");
        jsonBuilder.Append("        <td>ID</td>");
        jsonBuilder.Append("        <td>昵称</td>");
        jsonBuilder.Append("        <td>注册邮箱</td>");

        jsonBuilder.Append("    </tr>");

        IList<MemberInfo> entityList = (IList<MemberInfo>)Session["EmailMemberInfo"];

        MemberInfo memberEntity = null;

        foreach (MemberInfo entity in entityList)
        {
            memberEntity = MyBLL.GetMemberByID(entity.Member_ID, Public.GetUserPrivilege());
            if (memberEntity != null)
            {
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                jsonBuilder.Append("        <td><input type=\"hidden\" name=\"member_id\" value=\"" + entity.Member_ID + "\"><a href=\"javascript:member_del(" + entity.Member_ID + ");\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");

                jsonBuilder.Append("        <td align=\"left\">" + memberEntity.Member_ID + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + Public.JsonStr(memberEntity.Member_NickName) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + Public.JsonStr(memberEntity.Member_Email) + "</td>");
                jsonBuilder.Append("    </tr>");
            }
        }
        jsonBuilder.Append("</table>");
        entityList = null;

        return jsonBuilder.ToString();
    }

    //会员等级选择
    public string GetMemberGradeHTML(int GradeId, string selectname)
    {
        string select_str = "";
        select_str += "<select name=\"" + selectname + "\">";
        select_str += "<option value=\"-1\">不限</option>";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));
        IList<MemberGradeInfo> entitys = MyMGBLL.GetMemberGrades(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (MemberGradeInfo entity in entitys)
            {
                if (entity.Member_Grade_ID == GradeId)
                {
                    select_str += "<option value=\"" + entity.Member_Grade_ID + "\" selected=\"selected\">" + entity.Member_Grade_Name + "</option>";
                }
                else
                {
                    select_str += "<option value=\"" + entity.Member_Grade_ID + "\">" + entity.Member_Grade_Name + "</option>";
                }
            }
        }
        select_str += "</select>";
        return select_str;
    }

   

    #region "邮件处理"


    //邮件发送处理过程
    public int Sendmail(string mailto, string mailsubject, string mailbodytitle, string mailbody)
    {

        //-------------------------------------定义邮件设置---------------------------------
        int mformat = 0;

        //-------------------------------------定义邮件模版---------------------------------
        string MailBody_Temp = null;
        MailBody_Temp = "";
        MailBody_Temp = MailBody_Temp + "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=GB2312\" /></head>";
        MailBody_Temp = MailBody_Temp + "<body>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailHeader><SPAN class=MailBody_title>{MailBody_title}</SPAN></DIV>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailContent>";
        MailBody_Temp = MailBody_Temp + "{MailBody_content}";
        MailBody_Temp = MailBody_Temp + "<p><br><B>{sys_config_site_name}</B><br>欲了解更多信息，请访问<a href='{sys_config_site_url}'>{sys_config_site_url}</a> 或致电{sys_config_site_tel}</P></DIV>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailFooter><P class=comments>&copy; {sys_config_site_name}</P></DIV>";
        MailBody_Temp = MailBody_Temp + "<style type=text/css>";
        MailBody_Temp = MailBody_Temp + "P {FONT-SIZE: 14px; MARGIN: 10px 0px 5px; LINE-HEIGHT: 130%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + "td {FONT-SIZE: 12px; LINE-HEIGHT: 150%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + "BODY {BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: 0px; PADDING-TOP: 0px; BORDER-BOTTOM: 0px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif }";
        MailBody_Temp = MailBody_Temp + "UL {MARGIN-TOP: 0px; FONT-SIZE: 14px; LINE-HEIGHT: 130%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + ".comments {FONT-SIZE: 12px; MARGIN: 0px; COLOR: gray; LINE-HEIGHT: 130%}";
        MailBody_Temp = MailBody_Temp + ".mailHeader {PADDING-RIGHT: 23px; PADDING-LEFT: 23px; PADDING-BOTTOM: 10px; COLOR: #003366; PADDING-TOP: 10px; BORDER-BOTTOM: #7a8995 1px solid; BACKGROUND-COLOR: #ebebeb}";
        MailBody_Temp = MailBody_Temp + ".mailContent {PADDING-RIGHT: 23px; PADDING-LEFT: 23px; PADDING-BOTTOM: 23px; PADDING-TOP: 11px}";
        MailBody_Temp = MailBody_Temp + ".mailFooter {PADDING-RIGHT: 23px; BORDER-TOP: #bbbbbb 1px solid; PADDING-LEFT: 23px; PADDING-BOTTOM: 11px; PADDING-TOP: 11px}";
        MailBody_Temp = MailBody_Temp + ".MailBody_title {  font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 20px; font-weight: bold; color: #009900}";
        MailBody_Temp = MailBody_Temp + "A:visited { COLOR: #105bac} A:hover { COLOR: orange} .img_border { border: 1px solid #E5E5E5}";
        MailBody_Temp = MailBody_Temp + ".highLight { BACKGROUND-COLOR: #FFFFCC; PADDING: 15px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif}</style>";
        MailBody_Temp = MailBody_Temp + "</body><html>";

        //------------------------------------开始发送过程------------------------------------
        string body = "";
        switch (mformat)
        {
            case 0:
                //HTML格式
                body = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=GB2312\" />" + MailBody_Temp;
                body = body.Replace("{MailBody_title}", mailbodytitle);
                body = body.Replace("{MailBody_content}", mailbody);
                break;
            case 1:
                //纯文本格式
                body = mailbody;
                break;
        }

        body = replace_sys_config(body);

        // ERROR: Not supported in C#: OnErrorStatement
        try
        {
            mail.From = Application["Mail_From"].ToString();
            mail.Replyto = Application["Mail_Replyto"].ToString();
            mail.FromName = Application["Mail_FromName"].ToString();
            mail.Server = Application["Mail_Server"].ToString();
            //邮件格式 0=支持HTML,1=纯文本
            mail.ServerUsername = Application["Mail_ServerUserName"].ToString(); ;
            mail.ServerPassword = Application["Mail_ServerPassWord"].ToString();
            mail.ServerPort = tools.CheckInt(Application["Mail_ServerPort"].ToString());
            if (tools.CheckInt(Application["Mail_EnableSsl"].ToString()) == 0)
            {
                mail.EnableSsl = false;
            }
            else
            {
                mail.EnableSsl = true;
            }
            mail.Encode = Application["Mail_Encode"].ToString();

            if (mail.SendEmail(mailto, mailsubject, body))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        catch (Exception ex)
        {
            return 0;
        }



    }

    //替换系统变量
    public string replace_sys_config(string replacestr)
    {
        string functionReturnValue;
        functionReturnValue = replacestr;
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_name}", Application["site_name"].ToString());
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_url}", Application["site_url"].ToString());
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_tel}", Application["site_tel"].ToString());
        return functionReturnValue;
    }

    #endregion
    //会员冻结
    public void Member_Audit(int Status)
    {
        string MembersArry = tools.CheckStr(Request["member_id"]);
        if (MembersArry == "")
        {
            Public.Msg("error", "错误信息", "请选择会员", false, "{back}");
            return;
        }

        if (tools.Left(MembersArry, 1) == ",") { MembersArry = MembersArry.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();


        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_ID", "in", MembersArry));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "DESC"));

        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (MemberInfo entity in entitys)
            {

                entity.U_Member_State = Status;
                MyBLL.EditMember(entity, Public.GetUserPrivilege());
            }
        }
        Response.Redirect("/member/member_list.aspx");
    }
    public string GetMemberConsumptions()
    {

        string Member_IDstr = "";

        string keyword, date_start, date_end;
        //关键词
        keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "")
        {
            Member_IDstr = "0";
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_NickName", "like", keyword));
            IList<MemberInfo> members = MyBLL.GetMembers(Query1, Public.GetUserPrivilege());
            if (members != null)
            {
                foreach (MemberInfo ent in members)
                {
                    Member_IDstr = Member_IDstr + "," + ent.Member_ID;
                }
            }
            Query1 = null;
        }


        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (tools.CheckInt(Request["rows"]) < 1)
        {
            Query.PageSize = 1;
        }
        if (Member_IDstr != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberConsumptionInfo.Consump_MemberID", "in", Member_IDstr));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{MemberConsumptionInfo.Consump_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{MemberConsumptionInfo.Consump_Addtime})", "<=", "0"));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyCoinlog.GetPageInfo(Query);
        IList<MemberConsumptionInfo> entitys = MyCoinlog.GetMemberConsumptions(Query);

        if (entitys != null)
        {
            MemberInfo memberinfo = null;

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberConsumptionInfo entity in entitys)
            {



                jsonBuilder.Append("{\"MemberConsumptionInfo.Consump_ID\":" + entity.Consump_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Consump_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                memberinfo = MyBLL.GetMemberByID(entity.Consump_MemberID, Public.GetUserPrivilege());
                if (memberinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(memberinfo.Member_NickName));
                }
                else
                {
                    jsonBuilder.Append("未知");
                }
                memberinfo = null;
                jsonBuilder.Append("\",");

                if (entity.Consump_Coin > 0)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Consump_Coin);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("");
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Consump_Coin);
                    jsonBuilder.Append("\",");
                }


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Consump_CoinRemain);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Consump_Reason));
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
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }
    //展示选择会员
    public string ShowMember(string Member_ID)
    {
        int del_member = tools.CheckInt(Request["mid"]);
        StringBuilder jsonBuilder = new StringBuilder();
        string member_id = "";
        MemberGradeInfo membergrade = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        //member_ID = tools.NullStr(Request["bid"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_ID", "in", Member_ID));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"600\" bgcolor=\"#B0CADA\">");
            jsonBuilder.Append("    <tr class=\"list_head_bg\">");
            jsonBuilder.Append("        <td colspan=\"4\" align=\"left\">已选择会员 <span id=\"member_unfold\">[<a href=\"javascript:void(0);\" onclick=\"$('#member_unfold').hide();$('#member_fold').show();$('#member_picker').attr('class','div_picker_unfold');\">展开</a>]</span><span id=\"member_fold\" style=\"display:none;\">[<a href=\"javascript:void(0);\" onclick=\"$('#member_unfold').show();$('#member_fold').hide();$('#member_picker').attr('class','div_picker');\">还原</a>]</span></td>");
            jsonBuilder.Append("    </tr>");
            foreach (MemberInfo entity in entitys)
            {
                if (entity != null)
                {

                    if (del_member != entity.Member_ID)
                    {

                        if (member_id == "")
                        {
                            member_id = entity.Member_ID.ToString();
                        }
                        else
                        {
                            member_id += "," + entity.Member_ID.ToString();
                        }
                        membergrade = MyMGBLL.GetMemberGradeByID(entity.Member_Grade, Public.GetUserPrivilege());
                        jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                        jsonBuilder.Append("        <td align=\"left\">" + entity.Member_NickName + "</td>");
                        jsonBuilder.Append("        <td align=\"center\">" + entity.Member_Email + "</td>");
            
                        jsonBuilder.Append("        <td><a href=\"javascript:picker_member_del('" + entity.Member_ID + "');\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");
                        jsonBuilder.Append("    </tr>");
                    }
                }

            }
            jsonBuilder.Append("</table>");
            if (member_id == "")
            {
                jsonBuilder = null;
                jsonBuilder = new StringBuilder();
                jsonBuilder.Append("<span class=\"pickertip\">已选择会员</span>");
            }
            else
            {
                jsonBuilder.Append("<script>if($('#member_picker').attr('class')=='div_picker_unfold'){$('#member_unfold').hide();$('#member_fold').show();}else{$('#member_unfold').show();$('#member_fold').hide();}</script>");
            }
        }
        else
        {
            jsonBuilder.Append("<span class=\"pickertip\">已选择会员</span>");
        }
        Session["selected_memberid"] = member_id;
        jsonBuilder.Append("<script>$('#favor_memberid').val('" + member_id + "');</script>");
        entitys = null;

        return jsonBuilder.ToString();
    }

    //获取全部会员编号
    public string Get_MemberList_IDs()
    {
        string member_arry = "";
        string keyword = tools.CheckStr(Request["keyword"]);
        string member_id = "0";
        if (keyword != "输入昵称、邮箱、姓名、手机进行搜索" && keyword != null)
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
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_NickName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_LoginMobile", "=", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberInfo.U_Member_Realname", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (MemberInfo entity in entitys)
            {
                if (member_arry.Length > 0)
                {
                    member_arry = member_arry + "," + entity.Member_ID.ToString();
                }
                else
                {
                    member_arry = entity.Member_ID.ToString();
                }
            }
        }
        return member_arry;
    }

}
