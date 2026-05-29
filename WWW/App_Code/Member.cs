using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.MEM;

using Glaer.Trade.B2C.BLL.SAL;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.BLL.CMS;

/// <summary>
///Member 的摘要说明
/// </summary>
public class Member
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    ITools tools;
    IMember MyMember;
    IMemberGrade Mygrade;
    IMemberLog MyMemLog;
    Public_Class pub = new Public_Class();
    IEncrypt encrypt;

    IMemberFavorites MyFavor;

    IFeedBack MyFeedback;
    IMemberConsumption MyConsumption;
    private IQuestionHistory MyquestionH;
    private IVote Myvote;
    private ISQLHelper DBHelper;
    private IArticle MyArticle;

    private IArticleCate MyArticleCate;

    public Member()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyMember = MemberFactory.CreateMember();
        Mygrade = MemberGradeFactory.CreateMemberGrade();
        MyMemLog = MemberLogFactory.CreateMemberLog();
        encrypt = EncryptFactory.CreateEncrypt();

        MyFavor = MemberFavoritesFactory.CreateMemberFavorites();
        MyquestionH = QuestionHistoryFactory.CreateQuestionHistory();
        MyFeedback = FeedBackFactory.CreateFeedBack();
        MyConsumption = MemberConsumptionFactory.CreateMemberConsumption();
        Myvote = VoteFactory.CreateVote();
        MyArticle = ArticleFactory.CreateArticle();
        MyArticleCate = ArticleFactory.CreateArticleCate();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
    }

    #region"辅助函数"

    //检查昵称是否使用
    public bool Check_Member_Nickname(string nick_name)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_NickName", "=", nick_name));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        PageInfo page = MyMember.GetPageInfo(Query, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
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

    //检查注册邮箱是否使用
    public bool Check_Member_Email(string Member_Email, int member_id)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Email", "=", Member_Email));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_ID", "<>", member_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        PageInfo page = MyMember.GetPageInfo(Query, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
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


    //云马检查邮箱未验证视为未注册
    public bool Check_Member_Email_2(string Member_Email, int member_id)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        if (Check_Member_Nickname(Member_Email))
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Email", "=", Member_Email));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_ID", "<>", member_id.ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Emailverify", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));

            PageInfo page = MyMember.GetPageInfo(Query, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
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
        else
        {
            return Check_Member_Email(Member_Email, member_id);
        }


    }
    //检查密码
    public bool CheckSsn(string strSsn)
    {
        bool result = false;


        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9\u4e00-\u9fa5]*$");
        if (regex.IsMatch(strSsn))
        {
            result = true;
        }
        return result;
    }

    //检查密码
    public bool CheckPhone(string strSsn)
    {
        bool result = false;

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[0-9-]*$");
        result = regex.IsMatch(strSsn);

        return result;
    }

    //检查邀请码
    public bool CheckInvitedCode(string invitecode)
    {
        if (invitecode != "")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 1;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_InviteCode", "=", invitecode));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
            PageInfo page = MyMember.GetPageInfo(Query, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
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
        else
        {
            return false;
        }


    }

    //根据用户邮箱获取信息
    public MemberInfo GetMemberInfoByEmail(string Member_Email)
    {
        return MyMember.GetMemberByEmail(Member_Email, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
    }

    //获取默认会员等级
    public MemberGradeInfo GetMemberDefaultGrade()
    {
        return Mygrade.GetMemberDefaultGrade();
    }

    //会员日志
    public void Member_Log(int member_id, string member_name, int result, string description)
    {
        MemberLogInfo memberlog = new MemberLogInfo();
        memberlog.Log_ID = 0;
        memberlog.Log_Member_ID = member_id;
        memberlog.Log_Member_Name = member_name;
        memberlog.Log_Member_Result = result;
        memberlog.Log_Member_Action = description;
        memberlog.Log_Addtime = DateTime.Now;

        MyMemLog.AddMemberLog(memberlog);

    }

    //根据编号获取会员信息
    public MemberInfo GetMemberByID()
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        if (member_id > 0)
        {
            return MyMember.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        }
        else
        {
            return null;
        }
    }

    //根据编号获取会员信息
    public MemberInfo GetMemberByID(int m_id)
    {
        int member_id = tools.CheckInt(m_id.ToString());
        if (member_id > 0)
        {
            return MyMember.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        }
        else
        {
            return null;
        }
    }

    //根据会员编号获取会员等级信息
    public MemberGradeInfo GetMemberGradeByMemberID()
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        MemberInfo MEntity = MyMember.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (MEntity != null)
        {
            return Mygrade.GetMemberGradeByID(MEntity.Member_Grade, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
        }
        else
        {
            return null;
        }
    }

    //根据会员等级获取下一等级 文字提示
    public string GetLastMemberGrade()
    {
        MemberInfo memberinfo = GetMemberByID();
        string str = "";
        int requiredcoid = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_RequiredCoin", "ASC"));
        IList<MemberGradeInfo> entitys = Mygrade.GetMemberGrades(Query, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
        if (entitys != null)
        {
            bool bz = false;
            foreach (MemberGradeInfo entity in entitys)
            {
                if (bz)
                {
                    str = "（还差" + (entity.Member_Grade_RequiredCoin - memberinfo.Member_CoinCount) + "积分升级为" + entity.Member_Grade_Name + "）";
                    break;
                }
                if (entity.Member_Grade_ID == memberinfo.Member_Grade)
                {
                    bz = true;
                }
            }
        }
        return str;
    }

    //验证邮编
    public bool Check_Zip(string zip)
    {
        if (zip.Length != 6)
        {
            return false;
        }
        else
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[0-9]{6}");
            return regex.IsMatch(zip);
        }
    }

    //根据编号获取会员等级信息
    public MemberGradeInfo GetMemberGradeByID(int ID)
    {
        return Mygrade.GetMemberGradeByID(ID, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
    }

    public string GetMail_mSite(string site_url)
    {
        switch (site_url)
        {
            case "qq.com":
                site_url = "mail.qq.com";
                break;
            case "126.com":
                site_url = "mail.126.com";
                break;
            case "163.com":
                site_url = "mail.163.com";
                break;
            case "189.cn":
                site_url = "mail.189.cn";
                break;
            case "139.com":
                site_url = "mail.139.com";
                break;
            case "wo.com.cn":
                site_url = "mail.wo.com.cn";
                break;
            default:
                site_url = "mail." + site_url;
                break;
        }
        return site_url;
    }

    public string GetMemberNameByMobile(string Mobile)
    {
        string name = "用户";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_LoginMobile", "=", Mobile));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> entitys = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        if (entitys != null)
        {
            foreach (MemberInfo entity in entitys)
            {
                if (entity.Member_NickName.Length > 0)
                {
                    name = entity.Member_NickName;
                }
                break;
            }
        }

        return name;
    }

    /// <summary>
    /// 昵称格式验证
    /// </summary>
    /// <param name="member_nickname"></param>
    /// <returns></returns>
    public bool CheckNickNameFormat(string member_nickname)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(member_nickname, @"^[\u4e00-\u9fa5_a-zA-Z0-9]+$"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region "AJAx函数"

    public void Check_Nickname()
    {
        string member_nickname = tools.CheckStr(Request["val"]).Trim();
        if (member_nickname == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入用户名！</font>");
            return;
        }
        else
        {
            if (member_nickname.Length > 15)
            {
                Response.Write("<font color=\"#cc0000\">用户名不要超过15个字符！</font>");
                return;
            }

            if (!CheckNickNameFormat(member_nickname))
            {
                Response.Write("<font color=\"#cc0000\">用户名只能由中文、英文、数字及“_”组成</font>");
                return;
            }

            if (Check_Member_Nickname(member_nickname) == false)
            {
                Response.Write("<font color=\"#00a226\">用户名输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">该用户名已被使用，请使用其他用户名注册</font>");
                return;
            }
        }
    }

    //检查邮箱
    public void Check_MemberEmail()
    {
        string member_email = tools.CheckStr(Request["val"]);
        if (member_email == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入E-mail！</font>");
            return;
        }
        else
        {
            if (tools.CheckEmail(member_email))
            {
                int member_id = 0;
                //if (Convert.ToInt32(Session["Member_AllowSysEmail"]) != 1)
                //{
                //    member_id = pub.GetMemberIDBySession();
                //}
                member_id = pub.GetMemberIDBySession();

                if (Check_Member_Email(member_email, member_id) || Check_Member_Nickname(member_email))
                {
                    Response.Write("<font color=\"#cc0000\">该邮件地址已被使用。请更换邮件地址注册</font>");
                    return;
                }
                else
                {
                    Response.Write("<font color=\"#00a226\">E-mail输入正确！</font>");
                    return;
                }
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的E-mail！</font>");
                return;
            }
        }
    }
    //登录检查邮箱-未验证认为未注册
    public void Check_MemberEmail_Register()
    {
        string member_email = tools.CheckStr(Request["val"]);
        if (member_email == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入E-mail！</font>");
            return;
        }
        else
        {
            if (tools.CheckEmail(member_email))
            {
                int member_id = 0;
                if (Convert.ToInt32(Session["Member_AllowSysEmail"]) != 1)
                {
                    member_id = pub.GetMemberIDBySession();
                }

                if (Check_Member_Email_2(member_email, member_id))
                {
                    Response.Write("<font color=\"#cc0000\">该邮件地址已被使用。请更换邮件地址注册</font>");
                    return;
                }
                else
                {
                    Response.Write("<font color=\"#00a226\">E-mail输入正确！</font>");
                    return;
                }
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的E-mail！</font>");
                return;
            }
        }
    }
    /// <summary>
    /// 邮箱是否验证
    /// </summary>
    /// <param name="Email"></param>
    public void Check_YMMemberEmail(int status)
    {
        int member_status = status;
        if (member_status == 0)
        {
            Response.Write("<a href=\"/member/emailverify.aspx\" target=\"_blank\" style=\"\"><font color=\"#cc0000\">未验证邮箱</font></a>");
            return;
        }
        else
        {

            Response.Write("<a href=\"javascript:void(0);\" style=\"\"><font color=\"#00a226\">邮箱已验证！</font></a>");
            return;

        }
    }

    public void Check_MemberPasswprd()
    {
        string member_password = tools.CheckStr(Request["val"]);
        if (member_password.Length < 6 || member_password.Length > 20)
        {
            //Response.Write("<font color=\"#cc0000\">请输入6～20位密码（A-Z，a-z，0-9，不要输入空格）</font>");
            Response.Write("<font color=\"#cc0000\">请输入6～20位密码</font>");
            return;
        }
        else
        {
            if (CheckSsn(member_password))
            {
                Response.Write("<font color=\"#00a226\">密码输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">密码包含特殊字符！</font>");
                return;
            }
        }
    }

    public void Check_MemberrePasswprd()
    {
        string member_repassword = tools.CheckStr(Request["val"]);
        string member_password = tools.CheckStr(Request["val1"]);
        if (member_repassword.Length < 6 || member_repassword.Length > 20)
        {
            Response.Write("<font color=\"#cc0000\">请输入6～20位密码</font>");
            return;
        }
        if (member_repassword != member_password)
        {
            Response.Write("<font color=\"#cc0000\">两次密码不一致</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">确认密码输入正确！</font>");
            return;
        }
    }

    public void Check_MemberMobile()
    {
        string member_mobile = tools.CheckStr(Request["val"]);
        if (member_mobile == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入手机号码！</font>");
            return;
        }
        else
        {
            if (pub.Checkmobile(member_mobile))
            {
                Response.Write("<font color=\"#00a226\">手机号码输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的手机号码！</font>");
                return;
            }
        }
    }

    /// <summary>
    /// 检查是否手机验证
    /// </summary>
    /// <param name="Mobile"></param>

    public void Check_YMMemberMobile(int status)
    {
        int mobile_status = status;
        if (mobile_status == 0)
        {
            Response.Write("<a href=\"/member/account_mobilebinding.aspx\" target=\"_blank\" style=\"\"><font color=\"#cc0000\">未验证手机</font></a>");
            return;
        }
        else
        {
            Response.Write("<a href=\"javascript:void(0);\" style=\"\"><font color=\"#00a226\">手机已验证！</font></a>");
            return;
        }
    }

    public void Check_MemberPhone()
    {
        string member_mobile = tools.CheckStr(Request["val"]);
        if (member_mobile == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入联系人固定电话！</font>");
            return;
        }
        else
        {
            if (CheckPhone(member_mobile))
            {
                Response.Write("<font color=\"#00a226\"> </font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">电话格式错误，请重新输入！</font>");
                return;
            }
        }
    }

    public void Check_Verifycode()
    {
        string verifycode = tools.CheckStr(Request["val"]).ToLower();
        if (verifycode == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入验证码！</font>");
            return;
        }
        else
        {
            if (verifycode == Session["Trade_Verify"].ToString())
            {
                Response.Write("<font color=\"#00a226\">验证码输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的验证码！</font>");
                return;
            }
        }
    }

    public void Check_Invitedcode()
    {
        string member_invited = tools.CheckStr(Request["val"]);
        if (member_invited != "")
        {
            if (CheckInvitedCode(member_invited))
            {
                Response.Write("<font color=\"#00a226\">邀请码输入正确 </font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">邀请码错误，请重新输入！</font>");
                return;
            }
        }
        else
        {
            Response.Write("<font color=\"#cc0000\">邀请码错误，请重新输入！</font>");
        }
    }

    public void Check_Checkprotocal()
    {
        string protocal = tools.CheckStr(Request["val"]);
        if (protocal != "1")
        {
            Response.Write("<font color=\"#cc0000\">请阅读并接受网站使用协议！</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">您已接受网站使用协议！</font>");
            return;
        }
    }

    public void Check_IsBlank()
    {
        string content = tools.CheckStr(Request["val"]);
        string success = tools.CheckStr(Server.UrlDecode(Request["success"]));
        if (success == "") { success = "信息输入正确！"; }
        string error = tools.CheckStr(Server.UrlDecode(Request["error"]));
        if (error == "") { error = "信息不可为空！"; }
        if (content == "")
        {
            Response.Write("<font color=\"#cc0000\">" + error + "</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">" + success + "</font>");
            return;
        }
    }

    public void Check_LoginMobile()
    {
        string member_mobile = tools.CheckStr(Request["val"]);
        if (member_mobile == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入手机号码！</font>");
        }
        else
        {
            if (pub.Checkmobile(member_mobile))
            {
                if (Check_Member_LoginMobile(member_mobile))
                {
                    Response.Write("<font color=\"#cc0000\">该手机号码已被使用。请更换手机号码注册</font>");
                }
                else
                {
                    Response.Write("<font color=\"#00a226\">手机号码输入正确！</font>");
                }
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的手机号码！</font>");
            }
        }
    }

    /// <summary>
    /// 短信效验码验证
    /// </summary>
    public void Check_SMS_CheckCode()
    {
        string verifycode = tools.CheckStr(Request["val"]);
        string sign = tools.CheckStr(Request["sign"]);

        if (sign.Length == 0)
        {
            if (pub.CheckMemberLogin())
            {
                sign = Convert.ToString(Session["member_loginmobile"]);
            }
        }

        Dictionary<string, string> smscheckcode = Session["sms_check"] as Dictionary<string, string>;
        if (smscheckcode == null || smscheckcode["sign"] != sign)
        {
            Response.Write("<font color=\"#cc0000\">请输入短信效验码！</font>");
            return;
        }

        if (verifycode.Length == 0 || verifycode != smscheckcode["code"])
        {
            Response.Write("<font color=\"#cc0000\">请输入短信效验码！</font>");
            return;
        }

        if ((Convert.ToDateTime(smscheckcode["expiration"]) - DateTime.Now).TotalSeconds < 0)
        {
            Response.Write("<font color=\"#cc0000\">短信效验码过期！</font>");
            return;
        }

        Response.Write("<font color=\"#00a226\">短信效验码正确！</font>");
    }

    /// <summary>
    /// 检查手机号是否使用
    /// </summary>
    /// <param name="LoginMobile"></param>
    /// <returns></returns>
    public bool Check_Member_LoginMobile(string LoginMobile)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_LoginMobile", "=", LoginMobile));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        PageInfo page = MyMember.GetPageInfo(Query, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
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


    #endregion

    #region"注册登录"

    /// <summary>
    /// 创建昵称
    /// </summary>
    /// <returns></returns>
    public string CreateNickName()
    {
        return CreateNickName("tskp");
    }

    public string CreateNickName(string prefix)
    {
        string strNickName = string.Empty;
        bool valid = false;
        while (valid == false)
        {
            strNickName = prefix + pub.Createvkey(5);

            if (Check_Member_Nickname(strNickName) == false)
            {
                valid = true;
            }
        }

        return strNickName;
    }

    public string GetMemberInvite(string Invite)
    {
        //QueryInfo Query = new QueryInfo();
        //Query.PageSize = 0;
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_InviteCode", "=", Invite.ToString()));
        //Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "DESC"));
        //IList<MemberInfo> entitys = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        //if (entitys != null)
        //{
        //    GetMemberInvite(pub.CreatevkeyL(8));
        //}
        return "";
    }

    public virtual MemberInfo GetMemberByInvite(string Invited)
    {
        MemberInfo entity = null;
        if (Invited != "")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_InviteCode", "=", Invited.ToString()));
            Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "DESC"));
            IList<MemberInfo> entitys = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
            if (entitys != null)
            {
                entity = entitys[0];
            }
        }
        return entity;
    }

    //会员注册
    public string Member_Register()
    {
        string member_nickname = tools.CheckStr(pub.FormatNullToStr(Request["member_nickname"]).Trim());
        string member_email = tools.CheckStr(pub.FormatNullToStr(Request["member_email"]).Trim());
        string member_password = tools.CheckStr(pub.FormatNullToStr(Request["member_password"]).Trim());
        string member_password_confirm = tools.CheckStr(pub.FormatNullToStr(Request["member_password_confirm"]).Trim());
        string verifycode = tools.CheckStr(pub.FormatNullToStr(Request["verifycode"])).ToLower();
        string smscheckcode = tools.CheckStr(pub.FormatNullToStr(Request["smscheckcode"]));
        int Isagreement = tools.CheckInt(pub.FormatNullToStr(Request["checkbox_agreement"]));
        int Member_Type = tools.CheckInt(pub.FormatNullToStr(Request["Member_Type"]));
        string member_mobile = tools.CheckStr(pub.FormatNullToStr(Request["member_mobile"]));
        string U_Member_Realname = tools.CheckStr(pub.FormatNullToStr(Request["U_Member_Realname"]));
        string U_Member_IDCard = tools.CheckStr(pub.FormatNullToStr(Request["U_Member_IDCard"]));

        string Member_InvitedCode = tools.CheckStr(pub.FormatNullToStr(Request["Member_InvitedCode"]));
        string Member_InviteCode = GetMemberInvite(pub.CreatevkeyL(8));
        string Member_RegCity = tools.CheckStr(pub.FormatNullToStr(Request["Member_RegCity"]));
        string Member_RegState = tools.CheckStr(pub.FormatNullToStr(Request["Member_RegState"]));
        int DefaultGrade = 1;
        string register_type = tools.CheckStr(pub.FormatNullToStr(Request["register_type"]));

        string Member_ExclusiveCode = tools.CheckStr(Request["Member_ExclusiveCode"]);
        int Member_ExclusiveMemberID = 0;

        string Member_State = "", Member_City = "", Member_County = "", Member_Name = "", Member_StreetAddress = "", Member_Phone_Number = "", U_Member_CompanyName = "", U_Member_CompanyUrl = "";
        int Member_ID = 0;
        MemberGradeInfo member_grade = GetMemberDefaultGrade();
        if (member_grade != null)
        {
            DefaultGrade = member_grade.Member_Grade_ID;
        }

        MemberInfo member = new MemberInfo();

        if (member_nickname == "")
        {
            return pub.Msg_Json("请填写用户名！", "");
        }
        if (member_email == "")
        {
            return pub.Msg_Json("请填写邮箱！", "");
        }
        if (U_Member_Realname == "")
        {
            return pub.Msg_Json("请填写姓名！", "");
        }
        if (U_Member_IDCard == "")
        {
            return pub.Msg_Json("请填写身份证号！", "");
        }

        if (!pub.CheckIDCard(U_Member_IDCard))
        {

            return pub.Msg_Json("请输入正确的身份证号！", "");

        }
        if (member_mobile != "")
        {
            if (pub.CheckMobile(member_mobile))
            {
                if (Check_Member_LoginMobile(member_mobile) || Check_Member_Nickname(member_mobile))
                {

                    return pub.Msg_Json("该手机号码已被使用。请更换手机号码注册！", "");
                }
            }
            else
            {
                return pub.Msg_Json("请输入正确的手机号码！", "");

            }
        }
        if (tools.CheckEmail(member_email) == false)
        {

            return pub.Msg_Json("E-mail邮件格式无效！", "");
        }
        else
        {

            if (MyMember.GetMemberByEmail(member_email, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76")) != null)
            {
                return pub.Msg_Json("E-mail已存在,请重新填写！", "");
            }
        }

        if (MyMember.GetMemberByNickName(member_nickname, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76")) != null)
        {
            return pub.Msg_Json("昵称已存在,请重新填写！", "");
        }
       

        if (member_password == "" || member_password_confirm=="")
        {

            return pub.Msg_Json("请输入密码！", "");
        }
        else
        {
            if (member_password_confirm != member_password)
            {
                return pub.Msg_Json("两次密码输入不一致，请重新输入！", "");
                
            }
        }
        if (Isagreement != 1)
        {
            return pub.Msg_Json("要完成注册，您需要接受用户注册协议！", "");
                
        }


        #region 邀请码验证

        if (Member_InvitedCode != "")
        {

            //if (!CheckInvitedCode(Member_InvitedCode))
            //{
            //    pub.Msg("error", "错误信息", "无效的邀请码", false, "{back}");
            //}
        }

        #endregion

        member.Member_LoginMobile = member_mobile;
        member.Member_LoginMobileverify = 1;
        member.Member_LoginCount = 1;

        member.Member_ID = Member_ID;
        member.Member_Email = member_email;
        member.Member_Emailverify = 0;

        member.Member_NickName = member_nickname;
        member.Member_Password = encrypt.MD5(member_password);
        member.Member_VerifyCode = pub.Createvkey();
        //member.Member_LoginCount = 1;
        member.Member_LastLogin_IP = pub.IPAddress();
        member.Member_LastLogin_Time = DateTime.Now;
        member.Member_CoinCount = 0;
        member.Member_CoinRemain = 0;
        member.Member_Addtime = DateTime.Now;

        member.Member_Trash = 0;
        member.Member_Grade = DefaultGrade;
        member.Member_Account = 0;
        member.Member_Frozen = 0;

        member.Member_AllowSysEmail = 1;

        member.Member_Site = "CN";
        member.Member_Source = "";


        //添加会员基本信息
        member.U_Member_Realname = U_Member_Realname;

        member.U_Member_Male = 0;

        member.U_Member_QQ = "";
        member.U_MeMber_Birth = DateTime.Parse("2000-01-01");
        member.U_Member_Bloodtype = "A";
        member.U_Member_IDCard = U_Member_IDCard;
        member.U_Member_Job = "其他";
        member.U_Member_Edu = "大学";

        bool AddOrEdit = false;
        if (Member_ID == 0)
        {
            AddOrEdit = MyMember.AddMember(member, pub.CreateUserPrivilege("5d071ec6-31d8-4960-a77d-f8209bbab496"));
        }
        else
        {
            AddOrEdit = MyMember.EditMember(member, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
        }
        if (AddOrEdit)
        {

            double Sys_Invite_Reward = tools.NullDbl(Application["Sys_Invite_Reward"].ToString());
            double Sys_Invited_Reward = tools.NullDbl(Application["Sys_Invited_Reward"].ToString());


            MemberInfo memberinfo = MyMember.GetMemberByNickName(member.Member_NickName, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo != null)
            {
                //MemberInfo memberinvite = GetMemberByInvite(Member_InvitedCode);
                //if (memberinvite != null)
                //{

                //}

              
                    Member_Log(memberinfo.Member_ID, memberinfo.Member_NickName, 1, "会员注册");
              


                //member_register_sendemailverify(memberinfo.Member_Email, memberinfo.Member_VerifyCode);
                Session["member_email"] = memberinfo.Member_Email;
               
                    Session["member_id"] = memberinfo.Member_ID;
                    Session["member_email"] = memberinfo.Member_Email;
                    Session["member_nickname"] = memberinfo.Member_NickName;
                    Session["member_logined"] = "True";
                    Session["member_emailverify"] = memberinfo.Member_Emailverify;
                    Session["member_logincount"] = memberinfo.Member_LoginCount + 1;
                    Session["member_lastlogin_time"] = memberinfo.Member_LastLogin_Time;
                    Session["member_lastlogin_ip"] = memberinfo.Member_LastLogin_IP;
                    Session["member_coinremain"] = memberinfo.Member_CoinRemain;
                    Session["member_coincount"] = memberinfo.Member_CoinCount;
                    Session["member_grade"] = memberinfo.Member_Grade;
                    Session["Member_AllowSysEmail"] = memberinfo.Member_AllowSysEmail;
                    Response.Cookies["member_email"].Expires = DateTime.Now.AddDays(365);
                    Response.Cookies["member_email"].Value = memberinfo.Member_Email;
                    //string Content_add = "尊敬的用户您好，您的初始密码为：" + member_password + "，感谢您注册为唐山科普会员。";
                    //发送短信
                    //new SMS().Send(member_mobile, Content_add);

                    Member_Coin_AddConsume(500, "注册有礼！注册赠送积分。", memberinfo.Member_ID, false, 0);
                    //pub.Msg("positive", "操作成功", "恭喜您成为唐山科普用户，请在会员中心完善资料", true, "/member/index.aspx");
                    ////
                    //pub.Msg("positive", "操作成功", "已为您分配密码：" + member_password, false, "/member/member_coupon.aspx");
                    //Response.Redirect("/member/index.aspx");
               

                 return pub.Msg_Json("", "/member/index.aspx");
           
                }
            return pub.Msg_Json("", "/member/index.aspx");
            }
            else
            {
               
              return pub.Msg_Json("用户注册失败，请稍后再试！", "");
            }
       
    }

    public int GetExclusiveCode(string Member_ExclusiveCode)
    {
        DataTable DtList = null;

        string SqlList = "select Partner from LabelShop_LicenseCode where REGKEY='" + Member_ExclusiveCode + "'";

        DtList = DBHelper.Query(SqlList);

        if (DtList.Rows.Count > 0)
        {
            int Partner = tools.NullInt(DtList.Rows[0]["Partner"]);
            if (Partner > 0)
            {
                return Partner;
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






    //会员登录
    public string Member_Login()
    {
        int chk_UserName = tools.CheckInt(Request["chk_UserName"]);

        string Member_Username = tools.CheckStr(pub.FormatNullToStr(Request["member_name"]).Trim());
        string Member_Password = tools.CheckStr(Request["member_password"]);

        if (Member_Username == "")
        {
            return pub.Msg_Json("请输入登录账号！", "");
        }
        if (Member_Password == "")
        {
            return pub.Msg_Json("请输入密码！", "");

        }


        string Trade_Verify = tools.CheckStr(Request["Trade_Verify"]).ToLower();



        if (Trade_Verify != tools.NullStr(Session["Trade_Verify"]))
        {
            Session["logintype"] = "False";
            return pub.Msg_Json("验证码有误，请重新输入！", "");
        }

        Member_Password = encrypt.MD5(Member_Password);
        MemberInfo memberinfo = null;
        memberinfo = MyMember.GetMemberByLogin(Member_Username, Member_Password, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

        if (memberinfo != null)
        {


            if (memberinfo.U_Member_State == 1)
            {

                Session["logintype"] = "False";
                return pub.Msg_Json("账号已冻结，请联系管理员！", "");
            }


            Session["member_id"] = memberinfo.Member_ID;
            Session["member_email"] = memberinfo.Member_Email;
            Session["member_emailverify"] = memberinfo.Member_Emailverify;
            Session["member_loginmobile"] = memberinfo.Member_LoginMobile;
            Session["member_loginmobileverify"] = memberinfo.Member_LoginMobileverify;
            Session["member_nickname"] = memberinfo.Member_NickName;

            Session["logintype"] = "True";
            Session["member_logined"] = "True";
            Session["member_logincount"] = memberinfo.Member_LoginCount + 1;
            Session["member_lastlogin_time"] = memberinfo.Member_LastLogin_Time;
            Session["member_lastlogin_ip"] = memberinfo.Member_LastLogin_IP;
            Session["member_coinremain"] = memberinfo.Member_CoinRemain;
            Session["member_coincount"] = memberinfo.Member_CoinCount;
            Session["member_grade"] = memberinfo.Member_Grade;
            Session["Member_AllowSysEmail"] = memberinfo.Member_AllowSysEmail;
            Session["U_Member_Realname"] = memberinfo.U_Member_Realname;
            if (chk_UserName == 1)
            {

                Response.Cookies["member_UserName"].Value = Member_Username;
                Response.Cookies["member_UserName"].Expires = DateTime.Now.AddDays(7);
                Response.Cookies["member_UserPwd"].Value = Member_Password;
                Response.Cookies["member_UserPwd"].Expires = DateTime.Now.AddDays(7);
            }



            //更新用户登录信息
            MyMember.UpdateMemberLogin(memberinfo.Member_ID, memberinfo.Member_LoginCount + 1, pub.IPAddress(), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

            //更新会员等级
            //Update_MemberGrade();

            Member_Log(memberinfo.Member_ID, memberinfo.Member_NickName, 1, "会员登录");


            if (Session["url_after_login"] == null)
            {
                Session["url_after_login"] = "";
            }
            if (tools.NullStr(Session["url_after_login"]) == "")
            {
                return pub.Msg_Json("", "/member/index.aspx");

            }
            else
            {
                return pub.Msg_Json("", Session["url_after_login"].ToString());
            }


        }
        else
        {
            Session["logintype"] = "False";
            return pub.Msg_Json("账户名与密码不匹配，请重新输入！", "");
        }
    }


    //会员快速登录
    public void Member_AutoLogin()
    {
        if (Session["member_logined"].ToString() == "True")
        {
            return;
        }
        string Member_Username = string.Empty;
        Member_Username = "";
        string Member_Password = "";

        if (Request.Cookies["member_UserName"] != null)
        {
            Member_Username = tools.NullStr(Request.Cookies["member_UserName"].Value);
        }
        if (Request.Cookies["member_UserPwd"] != null)
        {
            Member_Password = tools.NullStr(Request.Cookies["member_UserPwd"].Value);
        }

        if (Member_Username.Length == 0 || Member_Password.Length == 0)
        {
            return;
        }



        if (Member_Username == "")
        {
            return;
        }
        MemberInfo memberinfo = MyMember.GetMemberByLogin(Member_Username, Member_Password, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null)
        {
            if (memberinfo.Member_Password != Member_Password)
            {
                //Session["logintype"] = "False";

                //Response.Redirect("/member/login.aspx?login=pmsg");
                //pub.Msg("error", "密码错误", "密码错误", false, "{back}");
                //Response.Write("密码错误");
                //Response.End();
                return;
            }
            //Response.Write("成功");
            Session["member_id"] = memberinfo.Member_ID;
            Session["member_email"] = memberinfo.Member_Email;
            Session["member_emailverify"] = memberinfo.Member_Emailverify;
            Session["member_loginmobile"] = memberinfo.Member_LoginMobile;
            Session["member_loginmobileverify"] = memberinfo.Member_LoginMobileverify;
            Session["member_nickname"] = memberinfo.Member_NickName;
            Session["member_logined"] = "True";
            Session["member_logincount"] = memberinfo.Member_LoginCount + 1;
            Session["member_lastlogin_time"] = memberinfo.Member_LastLogin_Time;
            Session["member_lastlogin_ip"] = memberinfo.Member_LastLogin_IP;
            Session["member_coinremain"] = memberinfo.Member_CoinRemain;
            Session["member_coincount"] = memberinfo.Member_CoinCount;
            Session["member_grade"] = memberinfo.Member_Grade;
            Session["Member_AllowSysEmail"] = memberinfo.Member_AllowSysEmail;
            Session["BidCustomer"] = "";

            Response.Cookies["member_UserName"].Value = Member_Username;
            Response.Cookies["member_UserName"].Expires = DateTime.Now.AddDays(7);
            Response.Cookies["member_UserPwd"].Value = Member_Password;
            Response.Cookies["member_UserPwd"].Expires = DateTime.Now.AddDays(7);


            //更新用户登录信息
            MyMember.UpdateMemberLogin(memberinfo.Member_ID, memberinfo.Member_LoginCount + 1, pub.IPAddress(), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

            ////更新会员等级
            //if (memberinfo.Member_Type == 0)
            //{
            //    Update_MemberGrade();
            //}

            Member_Log(memberinfo.Member_ID, memberinfo.Member_NickName, 1, "会员快速登录");

        }

    }

    public void Update_MemberGrade()
    {

        MemberInfo memberinfo = GetMemberByID();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", "CN"));

        Query.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_RequiredCoin", "desc"));
        IList<MemberGradeInfo> grades = Mygrade.GetMemberGrades(Query, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
        if (grades != null)
        {
            foreach (MemberGradeInfo grade in grades)
            {
                if (memberinfo.Member_CoinCount >= grade.Member_Grade_RequiredCoin)
                {
                    memberinfo.Member_Grade = grade.Member_Grade_ID;
                    Session["member_grade"] = memberinfo.Member_Grade;
                    MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
                    break;
                }
            }
        }
    }

    //会员错误登录检查
    public bool Check_Login_Err(string member_name)
    {
        bool result = false;
        int Log_ID = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 6;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberLogInfo.Log_Member_Name", "=", member_name));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberLogInfo.Log_Member_Result", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("MemberLogInfo.Log_ID", "Desc"));
        IList<MemberLogInfo> entitys = MyMemLog.GetMemberLogs(Query);
        if (entitys != null)
        {
            Log_ID = entitys[0].Log_ID;
        }
        Query = new QueryInfo();
        Query.PageSize = 6;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberLogInfo.Log_Member_Name", "=", member_name));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberLogInfo.Log_ID", ">", Log_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberLogInfo.Log_ID", "Desc"));
        entitys = MyMemLog.GetMemberLogs(Query);
        if (entitys != null)
        {
            if (entitys.Count > 4)
            {
                if (DateTime.Now.AddMinutes(-30) <= entitys[0].Log_Addtime)
                {
                    result = true;
                }
            }
        }

        return result;
    }

    //会员登录检查
    public void Member_Login_Check(string url_after_login)
    {
        if (!pub.CheckMemberLogin())
        {
            Session["url_after_login"] = url_after_login;

            Response.Redirect("/member/login.aspx");
        }
    }

    //Ajax会员登录检查
    public void Member_Login_Check_Ajax()
    {
        if (!pub.CheckMemberLogin())
        {
            Session["url_after_login"] = tools.NullStr(Request["url_login"]);
            Response.Write(tools.NullStr(Request["url_login"]));
        }
        else
        {
            Response.Write("True");
        }
    }

    //会员退出
    public void Member_LogOut()
    {
        Session.Abandon();
        Session["member_logined"] = "False";
        Response.Cookies["member_UserName"].Value = "";
        Session["logintype"] = "False";
        Response.Cookies["member_UserPwd"].Value = "";
        Response.Redirect("/member/login.aspx");
    }



    //发送验证邮件
    public int member_register_sendemailverify(string member_email, string member_verifycode)
    {
        //发送注册邮件
        string mailsubject, mailbodytitle, mailbody;
        mailsubject = "赶快验证，马上享受{sys_config_site_name}会员服务！";
        mailsubject = replace_sys_config(mailsubject);
        mailbodytitle = "赶快验证，马上享受{sys_config_site_name}会员服务！";
        mailbodytitle = replace_sys_config(mailbodytitle);
        mailbody = mail_template("emailverify", "", "", member_verifycode);
        return pub.Sendmail(member_email, mailsubject, mailbodytitle, mailbody);
    }

    //重新发送验证邮件
    public void member_register_resendemailverify()
    {
        if (Session["member_logined"].ToString() != "True")
        {
            Session["url_after_login"] = "/member/emailverify.aspx";
            Response.Redirect("/member/login.aspx");
        }
        else
        {
            MemberInfo memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo != null)
            {
                member_register_sendemailverify(memberinfo.Member_Email, memberinfo.Member_VerifyCode);
                Response.Redirect("/member/emailverify.aspx");
            }
            else
            {
                Session["url_after_login"] = "/member/emailverify.aspx";
                Response.Redirect("/member/login.aspx");
            }
        }
    }

    //更改注册Email
    public void member_register_modifyemail()
    {
        string member_email = "";
        string member_verifycode = "";
        member_email = tools.CheckStr(Request["member_email"]);
        if (tools.CheckEmail(member_email) == false)
        {
            pub.Msg("error", "邮件地址无效", "请输入有效的邮件地址", false, "{back}");
        }
        else
        {
            if (Check_Member_Email(member_email, pub.GetMemberIDBySession()))
            {
                pub.Msg("error", "该邮件地址已被使用", "该邮件地址已被使用。请使用另外一个邮件地址进行注册", false, "{back}");
            }
        }
        //更新用户邮件
        member_verifycode = pub.Createvkey();
        MemberInfo memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null)
        {
            memberinfo.Member_VerifyCode = member_verifycode;
            memberinfo.Member_Email = member_email;
            memberinfo.Member_Emailverify = 0;
            MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
        }


        //发送验证邮件
        member_register_sendemailverify(member_email, member_verifycode);

        //置Session和Cookies
        Session["member_email"] = member_email;
        Response.Cookies["member_email"].Expires = DateTime.Now.AddDays(365);
        Response.Cookies["member_email"].Value = member_email;

        //转到邮箱验证页面
        Response.Redirect("/member/emailverify.aspx");
    }

    //验证邮件
    public void member_register_emailverify()
    {
        string member_verifycode = "";
        string member_email = "";
        member_verifycode = tools.CheckStr(Request["VerifyCode"]);
        string emailverify_result = "false";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_VerifyCode", "=", member_verifycode));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> memberinfo = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        if (memberinfo != null)
        {
            foreach (MemberInfo entity in memberinfo)
            {
                member_email = entity.Member_Email;
                member_verifycode = pub.Createvkey();
                entity.Member_VerifyCode = member_verifycode;
                entity.Member_Emailverify = 1;
                if (MyMember.EditMember(entity, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
                {
                    emailverify_result = "true";
                    member_register_sendemailverifysuccess(member_email, member_verifycode);
                    Session["member_email"] = member_email;
                    Response.Cookies["member_email"].Expires = DateTime.Now.AddDays(365);
                    Response.Cookies["member_email"].Value = member_email;
                }

                member_register_login(entity);
            }
        }
        string Print = tools.NullStr(Session["Print_Result"]);
        if (Print == "Print")
        {
            Response.Redirect("/printservices/Print_Label_design.aspx");
        }
        if (Print == "Customization")
        {
            Response.Redirect("/printservices/Print_Customization.aspx");
        }
        Response.Redirect("/member/emailverify_result.aspx?result=" + emailverify_result);

    }


    //验证邮件成功后直接登录
    public void member_register_login(MemberInfo memberinfo)
    {
        Session["member_id"] = memberinfo.Member_ID;
        Session["member_email"] = memberinfo.Member_Email;
        Session["member_emailverify"] = memberinfo.Member_Emailverify;
        Session["member_loginmobile"] = memberinfo.Member_LoginMobile;
        Session["member_loginmobileverify"] = memberinfo.Member_LoginMobileverify;
        Session["member_nickname"] = memberinfo.Member_NickName;
        Session["member_logined"] = "True";
        Session["member_logincount"] = memberinfo.Member_LoginCount + 1;
        Session["member_lastlogin_time"] = memberinfo.Member_LastLogin_Time;
        Session["member_lastlogin_ip"] = memberinfo.Member_LastLogin_IP;
        Session["member_coinremain"] = memberinfo.Member_CoinRemain;
        Session["member_coincount"] = memberinfo.Member_CoinCount;
        Session["member_grade"] = memberinfo.Member_Grade;
        Session["Member_AllowSysEmail"] = memberinfo.Member_AllowSysEmail;
    }
    //发送注册成功邮件
    public int member_register_sendemailverifysuccess(string member_email, string member_verifycode)
    {
        //发送注册邮件
        string mailsubject = "";
        string mailbodytitle = "";
        string mailbody = "";
        mailsubject = "验证成功，欢迎使用{sys_config_site_name}！";
        mailsubject = replace_sys_config(mailsubject);
        mailbodytitle = "验证成功，欢迎使用{sys_config_site_name}！";
        mailbodytitle = replace_sys_config(mailbodytitle);
        mailbody = mail_template("emailverify_success", "", "", member_verifycode);
        return pub.Sendmail(member_email, mailsubject, mailbodytitle, mailbody);
    }

    //找回密码发送邮件
    public void member_getpass_sendmail()
    {
        Session["getpass_member_loginmobile"] = string.Empty;

        string member_email = "";
        string member_verifycode = "";
        member_email = tools.CheckStr(Request["member_email"]);
        //判断邮箱是否有效
        if (tools.CheckEmail(member_email))
        {
            MemberInfo memberinfo = MyMember.GetMemberByEmail(member_email, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo != null)
            {
                member_verifycode = pub.Createvkey();
                memberinfo.Member_VerifyCode = member_verifycode;
                MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));

                //发送找回密码邮件
                string mailsubject = "";
                string mailbodytitle = "";
                string mailbody = "";
                mailsubject = "重新设置密码";
                mailbodytitle = "重新设置密码";
                mailbody = mail_template("getpass", "", "", member_verifycode);
                pub.Sendmail(member_email, mailsubject, mailbodytitle, mailbody);
                Response.Redirect("/member/getpassword_mail_success.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "您输入的邮件地址不存在，请检查后重新输入", false, "{back}");
            }
        }
        else if (pub.CheckMobile(member_email))
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_LoginMobile", "=", member_email));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_LoginMobileverify", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
            IList<MemberInfo> entityList = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
            if (entityList != null)
            {
                Session["getpass_member_loginmobile"] = entityList[0].Member_LoginMobile;
                entityList = null;

                Response.Redirect("/member/getpassword_mobile.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "您输入的邮箱/手机号不存在，请检查后重新输入", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "请输入有效的邮箱/手机号", false, "{back}");
        }
    }

    //找回密码邮件验证
    public void member_getpass_verify()
    {
        string member_verifycode = "";
        member_verifycode = tools.CheckStr(Request["VerifyCode"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_VerifyCode", "=", member_verifycode));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> memberinfo = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        if (memberinfo != null)
        {
            foreach (MemberInfo entity in memberinfo)
            {
                Session["getpass_verify"] = "true";
                Session["getpass_member_id"] = entity.Member_ID;
                Session["getpass_member_mail"] = entity.Member_Email;
                Session["getpass_member_loginmobile"] = "";
            }
            Response.Redirect("/member/getpassword_reset.aspx");
        }
        else
        {
            Session["getpass_verify"] = "false";
            Session["getpass_member_id"] = 0;
            Session["getpass_member_mail"] = "";
            Session["getpass_member_loginmobile"] = "";
            Response.Redirect("/member/getpassword_verify_failed.aspx");
        }

    }

    //找回密码重新设置密码
    public void member_getpass_resetpass()
    {
        string member_id, member_email, member_password, member_password_confirm, verifycode, member_verifycode;
        if (tools.NullStr(Session["getpass_verify"]) == "true")
        {
            member_id = Session["getpass_member_id"].ToString();
            member_email = Session["getpass_member_mail"].ToString();
            member_password = tools.CheckStr(pub.FormatNullToStr(Request.Form["member_password"]).Trim());
            member_password_confirm = tools.CheckStr(pub.FormatNullToStr(Request.Form["member_password_confirm"]).Trim());
            verifycode = tools.CheckStr(Request["verifycode"]).ToLower();

            if (verifycode.ToLower() != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
            {
                pub.Msg("error", "验证码输入错误", "验证码输入错误", false, "{back}");
            }

            if (CheckSsn(member_password) == false)
            {
                pub.Msg("error", "密码包含特殊字符", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
            }
            else
            {
                if (member_password.Length < 6 || member_password.Length > 20)
                {
                    pub.Msg("error", "请输入6～20位密码", "请输入6～20位密码", false, "{back}");
                }
            }

            if (member_password_confirm != member_password)
            {
                pub.Msg("error", "两次密码输入不一致", "两次密码输入不一致，请重新输入", false, "{back}");
            }

            MemberInfo memberinfo = MyMember.GetMemberByID(tools.CheckInt(member_id), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo != null)
            {
                member_verifycode = pub.Createvkey();
                memberinfo.Member_VerifyCode = member_verifycode;
                memberinfo.Member_Password = encrypt.MD5(member_password);
                MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));

                if (Session["getpass_member_loginmobile"] != null && Convert.ToString(Session["getpass_member_loginmobile"]).Length > 0)
                {

                }
                else
                {
                    //发送验证邮件
                    string mailsubject, mailbodytitle, mailbody;
                    mailsubject = "密码已重新设置";
                    mailbodytitle = "密码已重新设置";
                    mailbody = mail_template("getpass_success", "", "", member_verifycode);
                    pub.Sendmail(member_email, mailsubject, mailbodytitle, mailbody);
                }
                Session["member_id"] = memberinfo.Member_ID;
                Session["member_email"] = memberinfo.Member_Email;
                Session["member_emailverify"] = memberinfo.Member_Emailverify;
                Session["member_loginmobile"] = memberinfo.Member_LoginMobile;
                Session["member_loginmobileverify"] = memberinfo.Member_LoginMobileverify;
                Session["member_nickname"] = memberinfo.Member_NickName;
                Session["member_logined"] = "True";
                Session["member_logincount"] = memberinfo.Member_LoginCount + 1;
                Session["member_lastlogin_time"] = memberinfo.Member_LastLogin_Time;
                Session["member_lastlogin_ip"] = memberinfo.Member_LastLogin_IP;
                Session["member_coinremain"] = memberinfo.Member_CoinRemain;
                Session["member_coincount"] = memberinfo.Member_CoinCount;
                Session["member_grade"] = memberinfo.Member_Grade;
                Session["Member_AllowSysEmail"] = memberinfo.Member_AllowSysEmail;




                //更新用户登录信息
                MyMember.UpdateMemberLogin(memberinfo.Member_ID, memberinfo.Member_LoginCount + 1, pub.IPAddress(), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

                //更新会员等级
                //Update_MemberGrade();

                Member_Log(memberinfo.Member_ID, memberinfo.Member_NickName, 1, "会员登录");


                Response.Redirect("/member/index.aspx");
            }
            else
            {
                //跳转
                Response.Redirect("/member/getpassword.aspx");
            }
        }
        else
        {
            //跳转
            Response.Redirect("/member/getpassword.aspx");
        }
    }

    /// <summary>
    /// 验证当前身份
    /// </summary>
    public void member_getpass_validate()
    {
        string member_mobile = Convert.ToString(Session["getpass_member_loginmobile"]);
        string smscheckcode = tools.CheckStr(pub.FormatNullToStr(Request.Form["smscheckcode"]));
        string verifycode = tools.CheckStr(pub.FormatNullToStr(Request.Form["verifycode"])).ToLower();

        if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            pub.Msg("error", "验证码输入错误", "验证码输入错误", false, "{back}");
        }

        #region 效验码验证

        Dictionary<string, string> sms_check = Session["sms_check"] as Dictionary<string, string>;
        if (sms_check == null || sms_check["sign"] != member_mobile)
        {
            pub.Msg("error", "错误信息", "短信效验码错误", false, "{back}");
        }

        if (smscheckcode.Length == 0 || smscheckcode != sms_check["code"])
        {
            pub.Msg("error", "错误信息", "短信效验码错误", false, "{back}");
        }

        if ((Convert.ToDateTime(sms_check["expiration"]) - DateTime.Now).TotalSeconds < 0)
        {
            pub.Msg("error", "错误信息", "短信效验码过期", false, "{back}");
        }
        sms_check = null;
        Session.Remove("sms_check");

        #endregion

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_LoginMobile", "=", member_mobile));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "DESC"));
        IList<MemberInfo> memberinfo = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        if (memberinfo != null)
        {
            foreach (MemberInfo entity in memberinfo)
            {
                Session["getpass_verify"] = "true";
                Session["getpass_member_id"] = entity.Member_ID;
                Session["getpass_member_mail"] = "";
                Session["getpass_member_loginmobile"] = entity.Member_LoginMobile;
            }
            Response.Redirect("/member/getpassword_reset.aspx");
        }
        else
        {
            Session["getpass_verify"] = "false";
            Session["getpass_member_id"] = 0;
            Session["getpass_member_mail"] = "";
            Session["getpass_member_loginmobile"] = "";

            Response.Redirect("/member/getpassword.aspx");
        }
    }



    #endregion

    #region "会员信息"

    /// <summary>
    /// 绑定手机号
    /// </summary>
    public void Member_BindingMobile()
    {
        string member_mobile = tools.CheckStr(pub.FormatNullToStr(Request.Form["member_mobile"]));
        string smscheckcode = tools.CheckStr(pub.FormatNullToStr(Request.Form["smscheckcode"]));
        string verifycode = tools.CheckStr(pub.FormatNullToStr(Request.Form["verifycode"])).ToLower();

        if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            pub.Msg("error", "验证码输入错误", "验证码输入错误", false, "{back}");
        }

        if (member_mobile == "")
        {
            pub.Msg("error", "错误信息", "请输入手机号码", false, "{back}");
        }
        else
        {
            if (pub.CheckMobile(member_mobile))
            {
                if (Check_Member_LoginMobile(member_mobile))
                {
                    pub.Msg("error", "错误信息", "该手机号码已被绑定。请使用另外一个手机号码进行绑定", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "无效的手机号码", false, "{back}");
            }
        }

        #region 效验码验证

        Dictionary<string, string> sms_check = Session["sms_check"] as Dictionary<string, string>;
        if (sms_check == null || sms_check["sign"] != member_mobile)
        {
            pub.Msg("error", "错误信息", "短信效验码错误", false, "{back}");
        }

        if (smscheckcode.Length == 0 || smscheckcode != sms_check["code"])
        {
            pub.Msg("error", "错误信息", "短信效验码错误", false, "{back}");
        }

        if ((Convert.ToDateTime(sms_check["expiration"]) - DateTime.Now).TotalSeconds < 0)
        {
            pub.Msg("error", "错误信息", "短信效验码过期", false, "{back}");
        }
        sms_check = null;
        Session.Remove("sms_check");

        #endregion

        MemberInfo memberinfo = GetMemberByID();
        if (memberinfo == null)
        {
            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        }

        memberinfo.Member_LoginMobile = member_mobile;
        memberinfo.Member_LoginMobileverify = 1;

        Session["member_loginmobile"] = memberinfo.Member_LoginMobile;
        Session["member_loginmobileverify"] = memberinfo.Member_LoginMobileverify;

        RBACUserInfo UserInfo = pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe");
        if (MyMember.EditMember(memberinfo, UserInfo))
        {


            pub.Msg("positive", "操作成功", "操作成功", true, "/member/index.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        }

    }

    /// <summary>
    /// 验证当前身份
    /// </summary>
    public void ValidateCurrentId()
    {
        MemberInfo memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        string member_mobile = tools.CheckStr(pub.FormatNullToStr(Request.Form["member_mobile"]));
        string smscheckcode = tools.CheckStr(pub.FormatNullToStr(Request.Form["smscheckcode"]));
        string verifycode = tools.CheckStr(pub.FormatNullToStr(Request.Form["verifycode"])).ToLower();

        //if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        //{
        //    pub.Msg("error", "验证码输入错误", "验证码输入错误", false, "{back}");
        //}

        #region 效验码验证

        Dictionary<string, string> sms_check = Session["sms_check"] as Dictionary<string, string>;
        if (sms_check == null || sms_check["sign"] != member_mobile)
        {
            pub.Msg("error", "错误信息", "短信效验码错误", false, "{back}");
        }

        if (smscheckcode.Length == 0 || smscheckcode != sms_check["code"])
        {
            pub.Msg("error", "错误信息", "短信效验码错误", false, "{back}");
        }

        if ((Convert.ToDateTime(sms_check["expiration"]) - DateTime.Now).TotalSeconds < 0)
        {
            pub.Msg("error", "错误信息", "短信效验码过期", false, "{back}");
        }
        sms_check = null;
        Session.Remove("sms_check");

        #endregion

        Session["sms_passvalidate"] = 1;

        if (memberinfo != null)
        {
            memberinfo.Member_LoginMobileverify = 1;
            memberinfo.Member_LoginMobile = member_mobile;
            MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
            Response.Redirect(Convert.ToString(Request.Form["gotourl"]));
        }
        else
        {
            pub.Msg("error", "验证失败", "请刷新页面后重试", false, "{back}");
        }


    }

    //会员中心左侧列表
    public void Get_Member_Left_HTML(int main, int sub)
    {
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append(" <div class=\"mem-center-left\">");

        strHTML.Append("	  <h4><i></i>会员信息</h4>");
        strHTML.Append("<ul class=\"center-left-left\">");
        strHTML.Append("			<li " + (sub == 1 ? "class=\"active\"" : "") + "><a href=\"/member/m_info.aspx\">个人资料</a></li>");
        strHTML.Append("			<li " + (sub == 2 ? "class=\"active\"" : "") + "><a href=\"/member/m_password.aspx\">修改密码</a></li>");

        strHTML.Append("</ul>");

        strHTML.Append("	  <h4><i></i>我的投稿</h4>");
        strHTML.Append("<ul class=\"center-left-left\">");
        //strHTML.Append("			<li " + (sub == 3 ? "class=\"active\"" : "") + "><a href=\"/member/m_info.aspx\">佳作推荐</a></li>");
        strHTML.Append("			<li " + (sub == 4 ? "class=\"active\"" : "") + "><a href=\"/member/m_article.aspx\">科普原创</a></li>");
        //strHTML.Append("			<li " + (sub == 5 ? "class=\"active\"" : "") + "><a href=\"/member/m_password.aspx\">科普作品</a></li>");
        strHTML.Append("</ul>");

        //strHTML.Append("	  <h4><i></i>收藏分享</h4>");
        //strHTML.Append("<ul class=\"center-left-left\">");
        //strHTML.Append("			<li " + (sub == 6 ? "class=\"active\"" : "") + "><a href=\"/member/m_info.aspx\">我的收藏</a></li>");
        //strHTML.Append("			<li " + (sub == 7 ? "class=\"active\"" : "") + "><a href=\"/member/m_password.aspx\">我的分享</a></li>");

        //strHTML.Append("</ul>");

        strHTML.Append("	  <h4><i></i>会员专享</h4>");
        strHTML.Append("<ul class=\"center-left-left\">");
        strHTML.Append("			<li " + (sub == 8 ? "class=\"active\"" : "") + "><a href=\"/member/m_coin.aspx\">我的积分</a></li>");
        strHTML.Append("			<li " + (sub == 9 ? "class=\"active\"" : "") + "><a href=\"/member/m_coin.aspx?type=2\">我的答题</a></li>");
        strHTML.Append("			<li " + (sub == 10 ? "class=\"active\"" : "") + "><a href=\"/member/m_coin.aspx?type=3\">我的投票</a></li>");
        strHTML.Append("</ul>");

        strHTML.Append("	  <h4><i></i>会员权限</h4>");
        strHTML.Append("<ul class=\"center-left-left\">");
        strHTML.Append("			<li " + (sub == 11 ? "class=\"active\"" : "") + "><a href=\"/member/login_do.aspx?action=logout\">退出登录</a></li>");
        //strHTML.Append("			<li " + (sub == 1 ? "class=\"active\"" : "") + "><a href=\"/member/m_password.aspx\">注销账户</a></li>");

        strHTML.Append("</ul>");

        strHTML.Append("</div>");
        Response.Write(strHTML.ToString());
    }




    //检查信息是否完整
    public bool Account_Iscompleteprofile()
    {
        bool Result = false;
        MemberInfo memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null)
        {

            Result = true;

        }
        return Result;
    }


    //会员密码修改
    public string UpdateMemberPassword()
    {
        string old_pwd = tools.CheckStr(pub.FormatNullToStr(Request["member_oldpassword"]));
        string member_password = tools.CheckStr(pub.FormatNullToStr(Request["member_password"]));
        string member_password_confirm = tools.CheckStr(pub.FormatNullToStr(Request["member_password_confirm"]));
        //string verifycode = tools.CheckStr(pub.FormatNullToStr(Request.Form["verifycode"])).ToLower();

        //if (verifycode.ToLower() != Session["Trade_Verify"].ToString())
        //{
        //    pub.Msg("info", "提示信息", "验证码输入错误", false, "{back}");
        //}

        if (old_pwd == "")
        {
            return pub.Msg_Json("请输入原密码", "");

        }

        if (CheckSsn(member_password) == false)
        {
            return pub.Msg_Json("密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", "");

        }
        else
        {
            if (member_password.Length < 6 || member_password.Length > 20)
            {

                return pub.Msg_Json("请输入6～20位新密码", "");
            }
        }

        if (member_password != member_password_confirm)
        {
            return pub.Msg_Json("两次密码输入不一致，请重新输入", "");

        }

        old_pwd = encrypt.MD5(old_pwd);
        member_password = encrypt.MD5(member_password);


        MemberInfo memberinfo = new MemberInfo();
        memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null)
        {

            string Member_Password = memberinfo.Member_Password;

            memberinfo.Member_Password = member_password;

            if (old_pwd != Member_Password)
            {
                return pub.Msg_Json("原密码输入错误，请重试！", "");

            }
            if (MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
            {
                return pub.Msg_Json("", "/member/m_password.aspx");

            }
            else
            {
                return pub.Msg_Json("密码修改失败，请稍后再试！", "");

            }
        }
        else
        {
            return pub.Msg_Json("密码修改失败，请稍后再试！", "");

        }
    }

    //会员找回密码
    public string FindMemberPassword()
    {
        string member_name = tools.CheckStr(pub.FormatNullToStr(Request["member_name"]));
        string U_Member_Question = tools.CheckStr(pub.FormatNullToStr(Request["U_Member_Question"]));
        string U_Member_Answer = tools.CheckStr(pub.FormatNullToStr(Request["U_Member_Answer"]));

        if (member_name == "")
        {
            return pub.Msg_Json("请填写登录账号", "");

        }
        if (U_Member_Question == "")
        {
            return pub.Msg_Json("请填写已设置的问题", "");

        }
        if (U_Member_Answer == "")
        {
            return pub.Msg_Json("请填写已设置的答案", "");

        }
        MemberInfo minfo = MyMember.Member_Login(member_name, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (minfo != null)
        {
            if (minfo.U_Member_Question == U_Member_Question)
            {
                if (minfo.U_Member_Answer == U_Member_Answer)
                {
                    Session["mifno"] = minfo.Member_ID;
                    return pub.Msg_Json("", "/member/Updatepassword.aspx");
                }
                else {
                    return pub.Msg_Json("您输入的答案不正确，请重新输入！", "");
                }
            }
            else {
                return pub.Msg_Json("您输入的问题不正确，请重新输入！", "");
            }
        }
        else {
            return pub.Msg_Json("您输入的账号不存在，请重新输入！", "");
        }


        
    }


    //会员密码找回重置
    public string UpdateFindPassword()
    {
       
        string member_password = tools.CheckStr(pub.FormatNullToStr(Request["member_password"]));
        string member_password_confirm = tools.CheckStr(pub.FormatNullToStr(Request["member_password_confirm"]));


       
        if (CheckSsn(member_password) == false)
        {
            return pub.Msg_Json("密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", "");

        }
        else
        {
            if (member_password.Length < 6 || member_password.Length > 20)
            {

                return pub.Msg_Json("重置密码过于简单，请重新输入", "");
            }
        }

        if (member_password != member_password_confirm)
        {
            return pub.Msg_Json("两次密码输入不一致，请重新输入", "");

        }

       
        member_password = encrypt.MD5(member_password);


        MemberInfo memberinfo = new MemberInfo();
        memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["mifno"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null)
        {

            string Member_Password1 = memberinfo.Member_Password;

            memberinfo.Member_Password = member_password;

            if (member_password == Member_Password1)
            {
                return pub.Msg_Json("新密码不能和原密码一致，请重新输入！", "");

            }
            if (MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
            {
                Session["mifno"] = "";
                return pub.Msg_Json("", "/member/login.aspx");

            }
            else
            {
                return pub.Msg_Json("重置密码失败，请稍后再试！", "");

            }
        }
        else
        {
            return pub.Msg_Json("重置密码失败，请稍后再试！", "");

        }
    }
    //修改邮件订阅状态
    public void UpdateMemberAllowSysEmail(int status)
    {
        MemberInfo memberinfo = new MemberInfo();
        memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null)
        {
            memberinfo.Member_AllowSysEmail = status;
            MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
            Session["Member_AllowSysEmail"] = status;
            Response.Redirect("/member/email_notify_set.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "邮件通知设置失败，请稍后再试！", false, "{back}");
        }
    }

    #endregion

    #region "我的账户"

    //会员积分消费
    public void Member_Coin_AddConsume(int coin_amount, string coin_reason, int member_id, bool is_return,int qID)
    {
        int Member_CoinRemain = 0;
        MemberInfo member = MyMember.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
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
            consumption.Consump_Qid = qID;
            MyConsumption.AddMemberConsumption(consumption);

            if (coin_amount > 0)
            {
                if (is_return)
                {
                    member.Member_CoinRemain = Member_CoinRemain + coin_amount;
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

            MyMember.EditMember(member, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
        }
    }


    public string Member_Coin_List(string type, string date_start, string date_end)
    {
        StringBuilder sHtml = new StringBuilder();
        int member_id = tools.CheckInt(Session["member_id"].ToString());

        string Pageurl = "";
        int curpage = tools.CheckInt(pub.FormatNullToStr(Request["page"]));

        //if (action == "history")
        //{
        //    Pageurl = "?action=" + action;
        //}
        //else
        //{
        //    Pageurl = "?action=" + action + "&date_start=" + date_start + "&date_end=" + date_end;
        //}

        if (curpage < 1)
        {
            curpage = 1;
        }


        if (type == "1")
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"90\">ID</td>");
            sHtml.Append("  <td width=\"290\" >获得积分</td>");
            sHtml.Append("  <td width=\"120\" >获得时间</td>");
            sHtml.Append("  <td width=\"450\" >获得理由</td>");
            sHtml.Append("</tr></thead>  <tbody>");

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 10;
            Query.CurrentPage = curpage;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberConsumptionInfo.Consump_MemberID", "=", member_id.ToString()));
            //if (tools.NullDate(date_start).Year >= 1900)
            //{
            //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{MemberConsumptionInfo.Consump_Addtime}, '" + tools.NullDate(date_start) + "')", "<=", "0"));
            //}
            //if (tools.NullDate(date_end).Year >= 1900)
            //{
            //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{MemberConsumptionInfo.Consump_Addtime}, '" + tools.NullDate(date_end) + "')", ">=", "0"));
            //}
            Query.OrderInfos.Add(new OrderInfo("MemberConsumptionInfo.Consump_ID", "Desc"));
            IList<MemberConsumptionInfo> consumptions = MyConsumption.GetMemberConsumptions(Query);
            PageInfo page = MyConsumption.GetPageInfo(Query);

            if (consumptions != null)
            {
                foreach (MemberConsumptionInfo entity in consumptions)
                {

                    sHtml.Append("<tr>");
                    sHtml.Append("  <td>" + entity.Consump_ID + "</td>");
                    sHtml.Append("  <td >" + entity.Consump_Coin + "</td>");
                    sHtml.Append("  <td>" + entity.Consump_Addtime.ToShortDateString() + " </td>");
                    sHtml.Append("  <td >" + entity.Consump_Reason + "</td>");
                    sHtml.Append("</tr>");

                }
                sHtml.Append("</tbody></table>");
                sHtml.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sHtml.Append("<tr><td align=\"right\"><div class=\"list-page\" style=\"float:right;padding-right:10px;\">");

                sHtml.Append(pub.PageStr(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount, type));
                Response.Write("</div></td></tr>");
                sHtml.Append("</table>");





            }
            else
            {
                sHtml.Append("<tr >");
                sHtml.Append("<td colspan=\"4\">暂无记录</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</tbody></table>");
            }
        }
        else if (type == "2")
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"90\">序号</td>");
            sHtml.Append("  <td width=\"450\" >套题</td>");
            sHtml.Append("  <td width=\"120\" >答题时间</td>");
            sHtml.Append("  <td width=\"120\" >获得积分</td>");
            sHtml.Append("  <td width=\"170\" >操作</td>");
            sHtml.Append("</tr></thead>  <tbody>");




            QueryInfo Query = new QueryInfo();
            Query.PageSize = 10;
            Query.CurrentPage = curpage;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "QuestionHistoryInfo.ID", ">", "0"));

            Query.OrderInfos.Add(new OrderInfo("QuestionHistoryInfo.ID", "Desc"));
            IList<QuestionHistoryInfo> consumptions = MyquestionH.GetQuestionHistorys(Query, pub.CreateUserPrivilege("0727f3b4-4edc-4e49-94a0-d728fe7d35ef"));
            PageInfo page = MyquestionH.GetPageInfo(Query, pub.CreateUserPrivilege("0727f3b4-4edc-4e49-94a0-d728fe7d35ef"));

            if (consumptions != null)
            {
                foreach (QuestionHistoryInfo entity in consumptions)
                {

                    sHtml.Append("<tr>");
                    sHtml.Append("  <td>" + entity.ID + "</td>");

                    MemberConsumptionInfo mcinfo = MyConsumption.GetMemberConsumptionByMemID(member_id, entity.ID);

                    if (mcinfo != null)
                    {
                        sHtml.Append("  <td  >第" + entity.ID + "套题</td>");
                        sHtml.Append("  <td>" + mcinfo.Consump_Addtime.ToShortDateString() + " </td>");
                        sHtml.Append("  <td>" + mcinfo.Consump_Coin + " </td>");
                        sHtml.Append("  <td >已答题</td>");
                    }
                    else
                    {
                        sHtml.Append("  <td ><a  href='/member/Qh.aspx?ID=" + entity.ID + "' target='_blank'>第" + entity.ID + "套题</a></td>");
                        sHtml.Append("  <td>--</td>");
                        sHtml.Append("  <td>--</td>");
                        sHtml.Append("  <td ><a style='color:#338fff' href='/member/Qh.aspx?ID=" + entity.ID + "' target='_blank'>去答题</a></td>");
                    }

                    sHtml.Append("</tr>");

                }
                sHtml.Append("</tbody></table>");
                sHtml.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sHtml.Append("<tr><td align=\"right\"><div class=\"list-page\" style=\"float:right;padding-right:10px;\">");

                sHtml.Append(pub.PageStr(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount, type));
                Response.Write("</div></td></tr>");
                sHtml.Append("</table>");





            }
            else
            {
                sHtml.Append("<tr >");
                sHtml.Append("<td colspan=\"4\">暂无记录</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</tbody></table>");
            }
        }
        else if (type == "3")
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"90\">序号</td>");
            sHtml.Append("  <td width=\"450\" >标题</td>");
            sHtml.Append("  <td width=\"120\" >选择项</td>");
            sHtml.Append("  <td width=\"120\" >时间</td>");
            sHtml.Append("  <td width=\"170\" >操作</td>");
            sHtml.Append("</tr></thead>  <tbody>");




            QueryInfo Query = new QueryInfo();
            Query.PageSize = 10;
            Query.CurrentPage = curpage;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "VoteMemberInfo.Vote_Member_MemberID", "=", member_id.ToString()));

            Query.OrderInfos.Add(new OrderInfo("VoteMemberInfo.Vote_Member_AddTime", "Desc"));
            IList<VoteMemberInfo> vmeminfo = Myvote.GetVoteMembers(Query);
            PageInfo page = Myvote.GetVoteMemberPageInfo(Query);

            if (vmeminfo != null)
            {
                foreach (VoteMemberInfo entity in vmeminfo)
                {

                    sHtml.Append("<tr>");
                    sHtml.Append("  <td>" + entity.Vote_Member_ID + "</td>");

                    VoteInfo voinfo = Myvote.GetVoteByID(entity.Vote_Member_VoteID);
                    VoteSelectInfo vseinfo = Myvote.GetVoteSelectByID(entity.Vote_Member_VoteSelectID);
                    if (voinfo != null)
                    {
                        sHtml.Append("  <td >" + voinfo.Vote_Name + "</td>");
                        if (vseinfo != null)
                        {
                            sHtml.Append("  <td>" + vseinfo.Vote_Select_Name + " </td>");
                        }
                        sHtml.Append("  <td>" + entity.Vote_Member_AddTime.ToShortDateString() + " </td>");
                        sHtml.Append("  <td ><a style='color:#338fff'  href='/member/voteview.aspx?ID=" + voinfo.Vote_ID + "' target='_blank'>查看结果</a></td>");
                    }
                    else
                    {
                        sHtml.Append("  <td  >--</td>");
                        sHtml.Append("  <td>--</td>");
                        sHtml.Append("  <td>--</td>");
                        sHtml.Append("  <td >--</td>");

                    }

                    sHtml.Append("</tr>");

                }
                sHtml.Append("</tbody></table>");
                sHtml.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sHtml.Append("<tr><td align=\"right\"><div class=\"list-page\" style=\"float:right;padding-right:10px;\">");

                sHtml.Append(pub.PageStr(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount, type));
                Response.Write("</div></td></tr>");
                sHtml.Append("</table>");





            }
            else
            {
                sHtml.Append("<tr >");
                sHtml.Append("<td colspan=\"5\">暂无记录</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</tbody></table>");
            }
        }
        else
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"90\">序号</td>");
            sHtml.Append("  <td width=\"290\" >获得积分</td>");
            sHtml.Append("  <td width=\"120\" >获得时间</td>");
            sHtml.Append("  <td width=\"450\" >获得理由</td>");
            sHtml.Append("</tr></thead>  <tbody>");
            sHtml.Append("<tr >");
            sHtml.Append("<td colspan=\"4\">暂无记录</td>");
            sHtml.Append("</tr>");
            sHtml.Append("</tbody></table>");

        }

        return sHtml.ToString();
    }


    public string Member_Coin_List(string type, int count)
    {
        StringBuilder sHtml = new StringBuilder();
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        if (count == 0)
        {
            count = 3;
        }
        string Pageurl = "";
        int curpage = tools.CheckInt(pub.FormatNullToStr(Request["page"]));


        if (curpage < 1)
        {
            curpage = 1;
        }


        if (type == "1")
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"90\">ID</td>");
            sHtml.Append("  <td width=\"400\" >获得理由</td>");
            sHtml.Append("  <td width=\"130\" >获得时间</td>");
            sHtml.Append("  <td width=\"340\" >获得积分</td>");
            sHtml.Append("</tr></thead>  <tbody>");

            QueryInfo Query = new QueryInfo();
            Query.PageSize = count;
            Query.CurrentPage = curpage;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberConsumptionInfo.Consump_MemberID", "=", member_id.ToString()));
            //if (tools.NullDate(date_start).Year >= 1900)
            //{
            //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{MemberConsumptionInfo.Consump_Addtime}, '" + tools.NullDate(date_start) + "')", "<=", "0"));
            //}
            //if (tools.NullDate(date_end).Year >= 1900)
            //{
            //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{MemberConsumptionInfo.Consump_Addtime}, '" + tools.NullDate(date_end) + "')", ">=", "0"));
            //}
            Query.OrderInfos.Add(new OrderInfo("MemberConsumptionInfo.Consump_ID", "Desc"));
            IList<MemberConsumptionInfo> consumptions = MyConsumption.GetMemberConsumptions(Query);
            PageInfo page = MyConsumption.GetPageInfo(Query);

            if (consumptions != null)
            {
                foreach (MemberConsumptionInfo entity in consumptions)
                {

                    sHtml.Append("<tr>");
                    sHtml.Append("  <td>" + entity.Consump_ID + "</td>");
                    sHtml.Append("  <td>" + entity.Consump_Coin + "</td>");
                    sHtml.Append("  <td>" + entity.Consump_Addtime.ToShortDateString() + " </td>");
                    sHtml.Append("  <td >" + entity.Consump_Reason + "</td>");
                    sHtml.Append("</tr>");

                }
                sHtml.Append("</tbody></table>");






            }
            else
            {
                sHtml.Append("<tr >");
                sHtml.Append("<td colspan=\"4\">暂无记录</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</tbody></table>");
            }
        }
        else if (type == "2")
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"90\">序号</td>");
            sHtml.Append("  <td width=\"400\" >套题</td>");
            sHtml.Append("  <td width=\"130\" >答题时间</td>");
            sHtml.Append("  <td width=\"210\" >获得积分</td>");
            sHtml.Append("  <td width=\"130\" >操作</td>");
            sHtml.Append("</tr></thead>  <tbody>");




            QueryInfo Query = new QueryInfo();
            Query.PageSize = count;
            Query.CurrentPage = curpage;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "QuestionHistoryInfo.ID", ">", "0"));

            Query.OrderInfos.Add(new OrderInfo("QuestionHistoryInfo.ID", "Desc"));
            IList<QuestionHistoryInfo> consumptions = MyquestionH.GetQuestionHistorys(Query, pub.CreateUserPrivilege("0727f3b4-4edc-4e49-94a0-d728fe7d35ef"));
            PageInfo page = MyquestionH.GetPageInfo(Query, pub.CreateUserPrivilege("0727f3b4-4edc-4e49-94a0-d728fe7d35ef"));

            if (consumptions != null)
            {
                foreach (QuestionHistoryInfo entity in consumptions)
                {

                    sHtml.Append("<tr>");
                    sHtml.Append("  <td>" + entity.ID + "</td>");

                    MemberConsumptionInfo mcinfo = MyConsumption.GetMemberConsumptionByMemID(member_id, entity.ID);

                    if (mcinfo != null)
                    {
                        sHtml.Append("  <td >第" + entity.ID + "套题</td>");
                        sHtml.Append("  <td>" + mcinfo.Consump_Addtime.ToShortDateString() + " </td>");
                        sHtml.Append("  <td>" + mcinfo.Consump_Coin + " </td>");
                        sHtml.Append("  <td >已答题</td>");
                    }
                    else
                    {
                        sHtml.Append("  <td ><a>第" + entity.ID + "套题</a></td>");
                        sHtml.Append("  <td>--</td>");
                        sHtml.Append("  <td>--</td>");
                        sHtml.Append("  <td class='more'><a href='/member/Qh.aspx?ID=" + entity.ID + "' target='_blank'>去答题</a></td>");
                    }

                    sHtml.Append("</tr>");

                }
                sHtml.Append("</tbody></table>");






            }
            else
            {
                sHtml.Append("<tr >");
                sHtml.Append("<td colspan=\"4\">暂无记录</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</tbody></table>");
            }
        }
        else if (type == "3")
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"90\">序号</td>");
            sHtml.Append("  <td width=\"400\" >标题</td>");
            sHtml.Append("  <td width=\"130\" >选择项</td>");
            sHtml.Append("  <td width=\"210\" >时间</td>");
            sHtml.Append("  <td width=\"130\" >操作</td>");
            sHtml.Append("</tr></thead>  <tbody>");




            QueryInfo Query = new QueryInfo();
            Query.PageSize = count;
            Query.CurrentPage = curpage;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "VoteMemberInfo.Vote_Member_MemberID", "=", member_id.ToString()));

            Query.OrderInfos.Add(new OrderInfo("VoteMemberInfo.Vote_Member_AddTime", "Desc"));
            IList<VoteMemberInfo> vmeminfo = Myvote.GetVoteMembers(Query);
            PageInfo page = Myvote.GetVoteMemberPageInfo(Query);

            if (vmeminfo != null)
            {
                foreach (VoteMemberInfo entity in vmeminfo)
                {

                    sHtml.Append("<tr>");
                    sHtml.Append("  <td>" + entity.Vote_Member_ID + "</td>");

                    VoteInfo voinfo = Myvote.GetVoteByID(entity.Vote_Member_VoteID);
                    VoteSelectInfo vseinfo = Myvote.GetVoteSelectByID(entity.Vote_Member_VoteSelectID);
                    if (voinfo != null)
                    {
                        sHtml.Append("  <td >" + voinfo.Vote_Name + "</td>");
                        if (vseinfo != null)
                        {
                            sHtml.Append("  <td>" + vseinfo.Vote_Select_Name + " </td>");
                        }
                        sHtml.Append("  <td>" + entity.Vote_Member_AddTime.ToShortDateString() + " </td>");
                        sHtml.Append("  <td class='more'><a href='/member/voteview.aspx?ID=" + voinfo.Vote_ID + "' target='_blank'>查看结果</a></td>");
                    }
                    else
                    {
                        sHtml.Append("  <td >--</td>");
                        sHtml.Append("  <td>--</td>");
                        sHtml.Append("  <td>--</td>");
                        sHtml.Append("  <td >--</td>");

                    }

                    sHtml.Append("</tr>");

                }
                sHtml.Append("</tbody></table>");






            }
            else
            {
                sHtml.Append("<tr >");
                sHtml.Append("<td colspan=\"5\">暂无记录</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</tbody></table>");
            }
        }
        else
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"90\">序号</td>");
            sHtml.Append("  <td width=\"290\" >获得积分</td>");
            sHtml.Append("  <td width=\"120\" >获得时间</td>");
            sHtml.Append("  <td width=\"450\" >获得理由</td>");
            sHtml.Append("</tr></thead>  <tbody>");
            sHtml.Append("<tr >");
            sHtml.Append("<td colspan=\"4\">暂无记录</td>");
            sHtml.Append("</tr>");
            sHtml.Append("</tbody></table>");

        }

        return sHtml.ToString();
    }







    //会员收藏夹
    public void Member_Favorates(string uses, int irowmax, int list)
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int icount = 0;
        int irow = 1;
        string productURL = string.Empty;
        string softwareURL = "/software/software_view.aspx?Software_ID=";

        QueryInfo Query = new QueryInfo();
        if (uses == "list")
        {
            Query.PageSize = 0;
        }
        else
        {
            Query.PageSize = irowmax;
        }
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_MemberID", "=", member_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberFavoritesInfo.Member_Favorites_ID", "Desc"));
        IList<MemberFavoritesInfo> favoriates = MyFavor.GetMemberFavoritess(Query);
        if (favoriates != null)
        {
            Response.Write(" <ul>");
            foreach (MemberFavoritesInfo entity in favoriates)
            {
                icount++;
                if (list == 0)
                {
                    if (entity.Member_Favorites_Type == 0)
                    {

                    }
                }
                else
                {
                    if (entity.Member_Favorites_Type == 1)
                    {

                    }
                }


            }
            Response.Write("	<div class=\"clear\"></div>");
            Response.Write("</ul>");
        }
    }

    //添加到收藏夹
    public void Member_Favorites_Add(string action, int targetid)
    {
        if (targetid == 0)
        {
            pub.Msg("info", "信息提示", "请选择要添加到收藏夹的内容", false, "{back}");
        }
        if (action == "goods")
        {
            //ProductInfo product = MyProduct.GetProductByID(targetid, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            //if (product != null)
            //{
            //    if (product.Product_IsInsale == 1 && product.Product_IsAudit == 1)
            //    {
            //        MemberFavoritesInfo favorcheck = MyFavor.GetMemberFavoritesByProductID(tools.CheckInt(Session["member_id"].ToString()), 0, targetid);
            //        if (favorcheck != null)
            //        {
            //            pub.Msg("info", "信息提示", "该商品已在您的收藏夹中！", true, "/member/member_favorites.aspx");
            //        }
            //        MemberFavoritesInfo favor = new MemberFavoritesInfo();
            //        favor.Member_Favorites_ID = 0;
            //        favor.Member_Favorites_MemberID = tools.CheckInt(Session["member_id"].ToString());
            //        favor.Member_Favorites_Type = 0;
            //        favor.Member_Favorites_TargetID = targetid;
            //        favor.Member_Favorites_Addtime = DateTime.Now;
            //        favor.Member_Favorites_Site = "CN";

            //        if (MyFavor.AddMemberFavorites(favor))
            //        {
            //            Response.Redirect("/member/member_favorites.aspx");
            //        }
            //        else
            //        {
            //            pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
            //        }
            //    }
            //    else
            //    {
            //        pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
            //    }
            //}
            //else
            //{
            //    pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
            //}
        }
        if (action == "softwares")
        {
            //SoftwareInfo software = MySoftware.GetSoftwareByID(targetid, pub.CreateUserPrivilege("3d9a4939-bcf8-4a2d-92ef-b63f54bc91d0"));
            //if (software != null)
            //{
            //    if (software.Software_IsActive == 1)
            //    {
            //        MemberFavoritesInfo favorcheck = MyFavor.GetMemberFavoritesByProductID(tools.CheckInt(Session["member_id"].ToString()), 1, targetid);
            //        if (favorcheck != null)
            //        {
            //            pub.Msg("info", "信息提示", "该商品已在您的收藏夹中！", true, "/member/member_favorites.aspx");
            //        }
            //        MemberFavoritesInfo favor = new MemberFavoritesInfo();
            //        favor.Member_Favorites_ID = 0;
            //        favor.Member_Favorites_MemberID = tools.CheckInt(Session["member_id"].ToString());
            //        favor.Member_Favorites_Type = 1;
            //        favor.Member_Favorites_TargetID = targetid;
            //        favor.Member_Favorites_Addtime = DateTime.Now;
            //        favor.Member_Favorites_Site = "CN";

            //        if (MyFavor.AddMemberFavorites(favor))
            //        {
            //            Response.Redirect("/member/member_favorites.aspx");
            //        }
            //        else
            //        {
            //            pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
            //        }
            //    }
            //    else
            //    {
            //        pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
            //    }
            //}
            //else
            //{
            //    pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
            //}
        }
    }

    //ajax 添加到收藏夹
    public void Ajax_Member_Favorites_Add(string action, int targetid)
    {
        if (targetid == 0)
        {
            Response.Write("False");
            Response.End();
        }
        if (action == "ajax_goods")
        {
            //ProductInfo product = MyProduct.GetProductByID(targetid, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            //if (product != null)
            //{
            //    if (product.Product_IsInsale == 1 && product.Product_IsAudit == 1)
            //    {
            //        MemberFavoritesInfo favorcheck = MyFavor.GetMemberFavoritesByProductID(tools.CheckInt(Session["member_id"].ToString()), 0, targetid);
            //        if (favorcheck != null)
            //        {
            //            Response.Write("Exist");
            //            Response.End();
            //        }
            //        MemberFavoritesInfo favor = new MemberFavoritesInfo();
            //        favor.Member_Favorites_ID = 0;
            //        favor.Member_Favorites_MemberID = tools.CheckInt(Session["member_id"].ToString());
            //        favor.Member_Favorites_Type = 0;
            //        favor.Member_Favorites_TargetID = targetid;
            //        favor.Member_Favorites_Addtime = DateTime.Now;
            //        favor.Member_Favorites_Site = "CN";

            //        if (MyFavor.AddMemberFavorites(favor))
            //        {
            //            Response.Write("True");
            //        }
            //        else
            //        {
            //            Response.Write("False");
            //        }
            //    }
            //    else
            //    {
            //        Response.Write("False");
            //    }
            //}
            //else
            //{
            //    Response.Write("False");
            //}
        }
    }

    //从收藏夹移除
    public void Member_Favorites_Del(int ID)
    {
        if (ID == 0)
        {
            pub.Msg("info", "信息提示", "请选择要删除的内容", false, "{back}");
        }
        MemberFavoritesInfo favor = MyFavor.GetMemberFavoritesByID(ID);
        if (favor != null)
        {
            if (favor.Member_Favorites_MemberID == tools.CheckInt(Session["member_id"].ToString()))
            {
                MyFavor.DelMemberFavorites(ID);
                Response.Redirect("/member/member_favorites.aspx");
            }
            else
            {
                pub.Msg("info", "信息提示", "收藏夹信息删除失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("info", "信息提示", "收藏夹信息删除失败，请稍后再试！", false, "{back}");
        }



    }




    /// <summary>
    /// 查询当前积分余额
    /// </summary>
    /// <returns></returns>
    public int Get_MemberCoin()
    {
        int account_value = 0;
        try
        {
            MemberInfo entityMEM = GetMemberByID();

            if (entityMEM != null)
                account_value = entityMEM.Member_CoinRemain;
        }
        catch (Exception ex) { throw ex; }
        return account_value;
    }

    #endregion

    #region 会员邮件验证/修改

    public void EmailValidate_Send()
    {
        string member_email = tools.CheckStr(Request.Form["member_email"]);
        string verifycode = tools.CheckStr(Request.Form["verifycode"]).ToLower();

        if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            pub.Msg("error", "验证码输入错误", "验证码输入错误", false, "{back}");
        }

        if (tools.CheckEmail(member_email) == false)
        {
            pub.Msg("error", "邮件地址无效", "请输入有效的邮件地址", false, "{back}");
        }
        else
        {
            if (Check_Member_Email(member_email, pub.GetMemberIDBySession()))
            {
                pub.Msg("error", "该邮件地址已被使用", "该邮件地址已被使用。请使用另外一个邮件地址进行注册", false, "{back}");
            }
        }
        string verify_code = pub.Createvkey();
        MemberInfo memberinfo = GetMemberByID();
        if (memberinfo != null)
        {
            memberinfo.Member_Email = member_email;
            memberinfo.Member_VerifyCode = verify_code;
            MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
            Session["member_email"] = member_email;
        }

        //发送注册邮件
        string mailsubject, mailbodytitle, mailbody;
        mailsubject = "{sys_config_site_name}邮箱验证提醒";
        mailsubject = replace_sys_config(mailsubject);
        mailbodytitle = "{sys_config_site_name}邮箱验证提醒";
        mailbodytitle = replace_sys_config(mailbodytitle);
        mailbody = mail_template("emailvalidate", "", "", verify_code);
        pub.Sendmail(member_email, mailsubject, mailbodytitle, mailbody);

        Response.Redirect("emailverify.aspx");
    }

    public void EmailValidate_Do()
    {
        string member_verifycode = "";
        string member_email = "";
        member_verifycode = tools.CheckStr(Request["VerifyCode"]);
        string emailverify_result = "false";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_VerifyCode", "=", member_verifycode));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", pub.GetStandardSite()));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> memberinfo = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        if (memberinfo != null)
        {
            foreach (MemberInfo entity in memberinfo)
            {
                member_email = entity.Member_Email;
                member_verifycode = pub.Createvkey();
                entity.Member_VerifyCode = member_verifycode;
                entity.Member_Emailverify = 1;
                if (MyMember.EditMember(entity, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
                {
                    Session["member_id"] = entity.Member_ID;
                    Session["member_email"] = entity.Member_Email;
                    Session["member_emailverify"] = entity.Member_Emailverify;
                    Session["member_loginmobile"] = entity.Member_LoginMobile;
                    Session["member_loginmobileverify"] = entity.Member_LoginMobileverify;
                    Session["member_nickname"] = entity.Member_NickName;
                    Session["member_logined"] = "True";
                    Session["member_logincount"] = entity.Member_LoginCount + 1;
                    Session["member_lastlogin_time"] = entity.Member_LastLogin_Time;
                    Session["member_lastlogin_ip"] = entity.Member_LastLogin_IP;
                    Session["member_coinremain"] = entity.Member_CoinRemain;
                    Session["member_coincount"] = entity.Member_CoinCount;
                    Session["member_grade"] = entity.Member_Grade;
                    Session["Member_AllowSysEmail"] = entity.Member_AllowSysEmail;

                    emailverify_result = "true";
                    member_register_sendemailverifysuccess(member_email, member_verifycode);
                    Session["member_email"] = member_email;
                    Response.Cookies["member_email"].Expires = DateTime.Now.AddDays(365);
                    Response.Cookies["member_email"].Value = member_email;
                }
            }
        }
        Response.Redirect("/member/emailverify_result.aspx?result=" + emailverify_result);


    }
    #endregion



    #region "服务中心"

    //未读已回复留言统计
    public int Feedback_UnreadReply()
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int unread_count = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FeedBackInfo.Feedback_MemberID", "=", member_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FeedBackInfo.Feedback_Reply_IsRead", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("FeedBackInfo.Feedback_ID", "Desc"));
        PageInfo page = MyFeedback.GetPageInfo(Query, pub.CreateUserPrivilege("9877a09e-5dda-4b1e-bf6f-042504449eeb"));
        if (page != null)
        {
            unread_count = page.RecordCount;
        }
        return unread_count;
    }

    //用户留言添加
    public void AddFeedBack(int Flag)
    {
        int Feedback_ID = 0;
        int Feedback_Type = tools.CheckInt(Request.Form["Feedback_Type"]);
        int Feedback_MemberID = tools.CheckInt(Session["member_id"].ToString());
        string Feedback_Name = tools.CheckStr(Request.Form["Feedback_Name"]);
        string Feedback_Tel = tools.CheckStr(Request.Form["Feedback_Tel"]);
        string Feedback_Email = tools.CheckStr(Request.Form["Feedback_Email"]);
        string Feedback_Content = tools.CheckStr(Request.Form["Feedback_Content"]);
        DateTime Feedback_Addtime = DateTime.Now;
        int Feedback_IsRead = 0;
        int Feedback_Reply_IsRead = 0;
        string Feedback_Reply_Content = "";
        string Feedback_Site = "CN";
        string verifycode = tools.CheckStr(Request.Form["verifycode"]).ToLower();
        if (!pub.CheckMemberLogin())
        {
            pub.Msg("error", "错误提示", "请先登录再提交留言", false, "{back}");
        }
        if (verifycode.ToLower() != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            pub.Msg("error", "错误提示", "验证码输入错误", false, "{back}");
        }

        if (Feedback_Name.Length < 1 || Feedback_Tel.Length < 1 || Feedback_Email.Length < 1)
        {
            pub.Msg("info", "信息提示", "请输入您的联系方式，以便于我们与您联系！", false, "{back}");
        }
        if (Feedback_Content.Length < 1)
        {
            pub.Msg("info", "信息提示", "请输入留言内容！", false, "{back}");
        }

        FeedBackInfo entity = new FeedBackInfo();
        entity.Feedback_ID = Feedback_ID;
        entity.Feedback_Type = Feedback_Type;
        entity.Feedback_MemberID = Feedback_MemberID;
        entity.Feedback_Name = Feedback_Name;
        entity.Feedback_Tel = Feedback_Tel;
        entity.Feedback_Email = Feedback_Email;
        entity.Feedback_Content = Feedback_Content;
        entity.Feedback_Addtime = Feedback_Addtime;
        entity.Feedback_IsRead = Feedback_IsRead;
        entity.Feedback_Reply_IsRead = Feedback_Reply_IsRead;
        entity.Feedback_Reply_Content = Feedback_Reply_Content;
        entity.Feedback_Site = Feedback_Site;

        if (MyFeedback.AddFeedBack(entity, pub.CreateUserPrivilege("8ccafb10-8a4a-425f-8111-a1e4eb46a0b4")))
        {
            if (Flag == 1)
            {
                Response.Redirect("/member/feedback.aspx?tip=success");
            }
            if (Flag == 0)
            {
                pub.Msg("positive", "操作成功", "您的咨询已成功提交，我们的客服人员会尽快回复，感谢您对" + Application["site_name"] + "的支持！祝您购物愉快！", false, "/service/guide.aspx");
                //pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                //Response.Redirect("/service/guide.aspx?tip=success");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    //用户留言列表
    public void Feedback_List()
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(pub.FormatNullToStr(Request["page"]));
        Pageurl = "?action=list";
        string icon_alt = "";
        string icon = "";
        if (curpage < 1)
        {
            curpage = 1;
        }

        //Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"3\">");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FeedBackInfo.Feedback_MemberID", "=", member_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("FeedBackInfo.Feedback_ID", "Desc"));
        IList<FeedBackInfo> entitys = MyFeedback.GetFeedBacks(Query, pub.CreateUserPrivilege("9877a09e-5dda-4b1e-bf6f-042504449eeb"));
        PageInfo page = MyFeedback.GetPageInfo(Query, pub.CreateUserPrivilege("9877a09e-5dda-4b1e-bf6f-042504449eeb"));
        if (entitys != null)
        {

            foreach (FeedBackInfo entity in entitys)
            {
                i = i + 1;
                //if (i > 1)
                //{
                //    Response.Write("<tr><td height=\"10\" colspan=\"3\" valign=\"top\" class=\"dotline_h\"></td></tr>");
                //}

                //icon = "/images/feedback_1.gif";

                switch (entity.Feedback_Type)
                {
                    case 1:
                        icon_alt = "简单的留言";
                        break;
                    case 2:
                        icon_alt = "对网站的意见";
                        break;
                    case 3:
                        icon_alt = "对公司的建议";
                        break;
                    case 4:
                        icon_alt = "具有合作意向";
                        break;
                    case 5:
                        icon_alt = "商品投诉";
                        break;
                    case 6:
                        icon_alt = "服务投诉";
                        break;

                }
                Response.Write("<dl>");
                Response.Write("     <dd style=\" width:534px; padding:0 30px 20px 0; \">");
                Response.Write("    <p style=\" font-size:14px;\">[" + icon_alt + "]内容:" + entity.Feedback_Content + "</p>");
                Response.Write("         <p style=\" font-size:14px;\">时间：" + entity.Feedback_Addtime + "</p>");
                Response.Write("         <p style=\" font-size:14px;\"> 客服回复：" + entity.Feedback_Reply_Content + " 感谢您对" + Application["Site_Name"].ToString() + "的支持！祝您购物愉快！</p>");
                //if (entity.Feedback_Reply_IsRead == 0)
                //{
                //    Response.Write("<img src=\"/images/icon_new.gif\">");<img src=\"/images/feedback_reply.gif\" alt=\"客服回复\" align=\"absmiddle\" />
                //    MyFeedback.EditFeedBackReadStatus(entity.Feedback_ID, entity.Feedback_IsRead, 1, pub.CreateUserPrivilege("02cc2c2c-9ecc-462a-86dc-406f792ac83a"));
                //}
                Response.Write(" <p style=\" font-size:14px;\">回复时间：" + entity.Feedback_Reply_Addtime + "</p>");
                Response.Write(" </dd>");
                Response.Write("</dl>");

                Response.Write("   <div class=\"clear\"></div>");


            }
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);

        }
        else
        {
            Response.Write("<dd align=\"center\" class=\"t12_grey\">没有记录</dd>");
        }


    }
    #endregion


    #region "邮件处理"
    //替换系统变量
    public string replace_sys_config(string replacestr)
    {
        string result_value = "";
        result_value = replacestr;
        result_value = result_value.Replace("{sys_config_site_name}", Application["site_name"].ToString());
        result_value = result_value.Replace("{sys_config_site_url}", Application["site_url"].ToString());
        result_value = result_value.Replace("{sys_config_site_tel}", Application["site_tel"].ToString());
        return result_value;
    }

    //邮件模版
    public string mail_template(string template_name, string member_email, string member_password, string member_verifycode)
    {
        string mailbody = "";
        switch (template_name)
        {
            case "emailverify":
                mailbody = "<p>欢迎您注册{sys_config_site_name}！请点击下面的链接进行验证。</p>";
                mailbody = mailbody + "<p><a href=\"{sys_config_site_url}/member/register_do.aspx?action=emailverify&VerifyCode={member_verifycode}\" target=\"_blank\">{sys_config_site_url}/member/register_do.aspx?action=emailverify&VerifyCode={member_verifycode}</a></p>";
                mailbody = mailbody + "<p>如果链接无法点击，请将以上链接复制到浏览器地址栏中打开，即可完成验证！</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";

                break;
            case "emailverify_success":
                mailbody = "<p>验证成功，欢迎使用{sys_config_site_name}！</p>";
                mailbody = mailbody + "<p><strong><a href=\"{sys_config_site_url}\" target=\"_blank\">现在就开始，体验物联网标识自助应用平台！</a></strong></p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";

                break;
            case "getpass":
                mailbody = "<p>收到此邮件是因为您申请了重新设置密码。如果您没有申请，请忽略这封邮件。</p>";
                mailbody = mailbody + "<p>请点击下面的链接来重新设置密码</p>";
                mailbody = mailbody + "<p><a href=\"{sys_config_site_url}/member/login_do.aspx?action=verify&VerifyCode={member_verifycode}\" target=\"_blank\">{sys_config_site_url}/member/login_do.aspx?action=verify&VerifyCode={member_verifycode}</a></p>";
                mailbody = mailbody + "<p>如果链接无法点击，请将以上链接复制到浏览器地址栏中打开，即可重新设置密码！</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";

                break;
            case "getpass_success":
                mailbody = "<p>您的密码已重新设置，请牢记新密码。</p>";
                mailbody = mailbody + "<p><strong><a href=\"{sys_config_site_url}/member/login.aspx\" target=\"_blank\">点击这里登录您的帐号</a></strong></p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;


            case "GroupAdd":
                //mailbody = "<p>感谢您加入{sys_config_site_name}的群组，</p>";
                mailbody = mailbody + "<p>您已经被邀请加入群组(" + member_verifycode + ")</p>";
                mailbody = mailbody + "<p>再次感谢您对{sys_config_site_name}的支持，并真诚欢迎您再次光临{sys_config_site_name}!</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;

            case "AddGroup":
                mailbody = mailbody + "<p>您已创建群组(" + member_verifycode + ")</p>";
                mailbody = mailbody + "<p>再次感谢您对{sys_config_site_name}的支持，并真诚欢迎您再次光临{sys_config_site_name}!</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;
            case "ApplyMember":
                mailbody = mailbody + "<p>您成为专属会员，请耐心等待审核</p>";
                mailbody = mailbody + "<p>再次感谢您对{sys_config_site_name}的支持，并真诚欢迎您再次光临{sys_config_site_name}!</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;
            case "emailvalidate":
                mailbody = "<p>欢迎您注册{sys_config_site_name}！请点击下面的链接进行验证。</p>";
                mailbody = mailbody + "<p><a href=\"{sys_config_site_url}/member/account_do.aspx?action=emailvalidate_do&VerifyCode={member_verifycode}\" target=\"_blank\">{sys_config_site_url}/member/account_do.aspx?action=emailvalidate_do&VerifyCode={member_verifycode}</a></p>";
                mailbody = mailbody + "<p>如果链接无法点击，请将以上链接复制到浏览器地址栏中打开，即可完成验证！</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;
            case "emailupdatevalidate":
                mailbody = "<p>欢迎您使用{sys_config_site_name}！请点击下面的链接进行验证。</p>";
                mailbody = mailbody + "<p><a href=\"{sys_config_site_url}/member/account_do.aspx?action=emailupdatevalidate_do&VerifyCode={member_verifycode}\" target=\"_blank\">{sys_config_site_url}/member/account_do.aspx?action=emailupdatevalidate_do&VerifyCode={member_verifycode}</a></p>";
                mailbody = mailbody + "<p>如果链接无法点击，请将以上链接复制到浏览器地址栏中打开，即可完成验证！</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;
            case "emailupdate":
                mailbody = "<p>欢迎您使用{sys_config_site_name}！请点击下面的链接进行验证。</p>";
                mailbody = mailbody + "<p><a href=\"{sys_config_site_url}/member/account_do.aspx?action=emailupdate_do&VerifyCode={member_verifycode}\" target=\"_blank\">{sys_config_site_url}/member/account_do.aspx?action=emailupdate_do&VerifyCode={member_verifycode}</a></p>";
                mailbody = mailbody + "<p>如果链接无法点击，请将以上链接复制到浏览器地址栏中打开，即可完成验证！</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;

            case "Coupon_Notice":
                //mailbody = "<p>感谢您加入{sys_config_site_name}的群组，</p>";
                mailbody = mailbody + "<p>您的优惠券(" + member_verifycode + ")即将过期，请尽快使用</p>";
                mailbody = mailbody + "<p>再次感谢您对{sys_config_site_name}的支持，并真诚欢迎您再次光临{sys_config_site_name}!</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;

        }
        mailbody = mailbody.Replace("{member_verifycode}", member_verifycode);
        mailbody = mailbody.Replace("{member_password}", member_password);
        mailbody = mailbody.Replace("{member_email}", member_email);
        return mailbody;
    }


    public string GetMail_Site(string site_url)
    {
        switch (site_url)
        {
            case "qq.com":
                site_url = "mail.qq.com";
                break;
            case "126.com":
                site_url = "mail.126.com";
                break;
            case "163.com":
                site_url = "mail.163.com";
                break;
            case "189.cn":
                site_url = "mail.189.cn";
                break;
            case "139.com":
                site_url = "mail.139.com";
                break;
            case "wo.com.cn":
                site_url = "mail.wo.com.cn";
                break;

            default:
                site_url = "mail." + site_url;
                break;
        }
        return site_url;
    }

    #endregion



    //会员退出
    public void Member_AutoLogOutBuyService()
    {
        Session.Abandon();
        Session["member_logined"] = false;
        Response.Cookies["member_UserName"].Value = "";
        Response.Cookies["member_UserPwd"].Value = "";
    }


    public string Update_Member()
    {
        string Member_NickName = tools.CheckStr(tools.NullStr(Request["Member_NickName"]).Trim());
        int U_Member_Male = tools.CheckInt(tools.NullStr(Request["U_Member_Male"]));
        string Member_Email = tools.CheckStr(tools.NullStr(Request["Member_Email"]));
        string U_Member_QQ = tools.CheckStr(tools.NullStr(Request["U_Member_QQ"]));
        DateTime U_MeMber_Birth = tools.NullDate(tools.NullStr(Request["U_MeMber_Birth"]));
        string U_Member_Bloodtype = tools.CheckStr(tools.NullStr(Request["U_Member_Bloodtype"]));
        string U_Member_Realname = tools.CheckStr(tools.NullStr(Request["U_Member_Realname"]));
        string U_Member_IDCard = tools.CheckStr(tools.NullStr(Request["U_Member_IDCard"]));
        string U_Member_Job = tools.CheckStr(tools.NullStr(Request["U_Member_Job"]));
        string U_Member_Edu = tools.CheckStr(tools.NullStr(Request["U_Member_Edu"]));


        string U_Member_Answer = tools.CheckStr(tools.NullStr(Request["U_Member_Answer"]));
        string U_Member_Question = tools.CheckStr(tools.NullStr(Request["U_Member_Question"]));


        MemberInfo memberinfo = GetMemberByID();
        if (memberinfo == null)
        {
            return pub.Msg_Json("", "/member/login.aspx");

        }
        if (Member_NickName == "")
        {
            return pub.Msg_Json("请填写用户名", "");
        }
        if (Member_Email == "")
        {
            return pub.Msg_Json("请填写邮箱", "");
        }
        if (U_Member_Question == "")
        {
            return pub.Msg_Json("请填写问题", "");
        }
        if (U_Member_Answer == "")
        {
            return pub.Msg_Json("请填写答案", "");
        }
        if (U_Member_Realname == "")
        {
            return pub.Msg_Json("请填写姓名", "");
        }
        if (U_Member_IDCard == "")
        {
            return pub.Msg_Json("请填写身份证号", "");
        }
      

        if (!pub.CheckIDCard(U_Member_IDCard))
        {

            return pub.Msg_Json("请输入正确的身份证号", "");

        }
        if (memberinfo.Member_NickName != Member_NickName)
        {
            if (MyMember.GetMemberByNickName(Member_NickName, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76")) != null)
            {
                return pub.Msg_Json("用户名已存在,请重新填写！", "");
            }
        }
        if (memberinfo.Member_Email != Member_Email)
        {
            if (MyMember.GetMemberByEmail(Member_Email, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76")) != null)
            {
                return pub.Msg_Json("E-mail已存在,请重新填写！", "");
            }
        }


        memberinfo.Member_Email = Member_Email;
        memberinfo.Member_NickName = Member_NickName;
        memberinfo.U_Member_Realname = U_Member_Realname;

        memberinfo.U_Member_Male = U_Member_Male;

        memberinfo.U_Member_QQ = U_Member_QQ;
        memberinfo.U_MeMber_Birth = U_MeMber_Birth;
        memberinfo.U_Member_Bloodtype = U_Member_Bloodtype;
        memberinfo.U_Member_IDCard = U_Member_IDCard;
        memberinfo.U_Member_Job = U_Member_Job;
        memberinfo.U_Member_Edu = U_Member_Edu;
        memberinfo.U_Member_Answer = U_Member_Answer;
        memberinfo.U_Member_Question = U_Member_Question;

        MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));

        return pub.Msg_Json("", "/member/m_info.aspx");

    }

    #region 会员中心，2020年1月19日17:56:50 wtp
    public object GetVoteCount()
    {
        string Strsql = "select count(Vote_Member_ID) from Vote_Member where Vote_Member_MemberID=" + Session["member_id"].ToString();
        return DBHelper.ExecuteScalar(Strsql);
    }
    public object GetQuestionH()
    {
        string Strsql = "select count(Consump_ID) from Member_Consumption where Consump_MemberID=" + Session["member_id"].ToString() + " and Consump_Qid<>0";
        return DBHelper.ExecuteScalar(Strsql);
    }
    public object GetMArticle()
    {
        string Strsql = "select count(Article_ID) from Article where Article_memberID=" + Session["member_id"].ToString() + " and Artide_SouceType=1";
        return DBHelper.ExecuteScalar(Strsql);
    }
    #endregion

    #region 文章投稿

    public string Member_article_List(string type,int cateid, int count)
    {
        StringBuilder sHtml = new StringBuilder();
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        if (count == 0)
        {
            count = 5;
        }
        string Pageurl = "";
        int curpage = tools.CheckInt(pub.FormatNullToStr(Request["page"]));


        if (curpage < 1)
        {
            curpage = 1;
        }


       
        if (type == "1")
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"490\">文章标题</td>");

            sHtml.Append("  <td width=\"130\" >时间</td>");
            sHtml.Append("  <td width=\"210\" >作者</td>");
            sHtml.Append("  <td width=\"130\" >操作</td>");
            sHtml.Append("</tr></thead>  <tbody>");




            QueryInfo Query = new QueryInfo();
            Query.PageSize = count;
            Query.CurrentPage = curpage;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_memberID", "=", member_id.ToString()));

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ID", ">", "0"));
      
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Artide_SouceType", "=", type));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "2"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));


            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

            PageInfo page = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

            if (entitys != null)
            {
                foreach (ArticleInfo entity in entitys)
                {

                    sHtml.Append("<tr>");
                    sHtml.Append("  <td>" + entity.Article_Title + "</td>");
                    sHtml.Append("  <td>" + entity.Article_Addtime.ToShortDateString() + "</td>");
                    sHtml.Append("  <td>" + entity.Article_Author + " </td>");
                 
                    sHtml.Append("  <td ><a href='/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "'>查看</a></td>");
                    sHtml.Append("</tr>");

                }
                sHtml.Append("</tbody></table>");
            }
            else
            {
                sHtml.Append("<tr >");
                sHtml.Append("<td colspan=\"4\">暂无记录</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</tbody></table>");
            }
        }
        else
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");



            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"490\">文章标题</td>");
            sHtml.Append("  <td width=\"130\" >时间</td>");
            sHtml.Append("  <td width=\"210\" >作者</td>");
            sHtml.Append("  <td width=\"130\" >操作</td>");
            sHtml.Append("</tr></thead>  <tbody>");
            sHtml.Append("<tr >");
            sHtml.Append("<td colspan=\"4\">暂无记录</td>");
            sHtml.Append("</tr>");
            sHtml.Append("</tbody></table>");

        }

        return sHtml.ToString();
    }

    public string Member_article_List(string type)
    {
        StringBuilder sHtml = new StringBuilder();
        int member_id = tools.CheckInt(Session["member_id"].ToString());

        string Pageurl = "";
        int curpage = tools.CheckInt(pub.FormatNullToStr(Request["page"]));

       
        if (curpage < 1)
        {
            curpage = 1;
        }


        if (type == "1")
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
       
            sHtml.Append("  <td width=\"440\">文章标题</td>");
            sHtml.Append("  <td width=\"130\" >所属栏目</td>");
            sHtml.Append("  <td width=\"130\" >时间</td>");
            sHtml.Append("  <td width=\"130\" >审核状态</td>");
            sHtml.Append("  <td width=\"130\" >操作</td>");
            sHtml.Append("</tr></thead>  <tbody>");

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 10;
            Query.CurrentPage = curpage;
 
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_memberID", "=", member_id.ToString()));

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_ID", ">", "0"));

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Artide_SouceType", "=", type));
          
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));


            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Addtime", "DESC"));
            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

            PageInfo page = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

            if (entitys != null)
            {
                ArticleCateInfo CateInfo;
                CMS cms=new CMS();
                foreach (ArticleInfo entity in entitys)
                {
                    CateInfo=cms.GetArticleCateByID(entity.Article_CateID);
                    sHtml.Append("<tr>");
                    sHtml.Append("  <td>" + entity.Article_Title + "</td>");
                    if(CateInfo!=null)
                    {
                      sHtml.Append("  <td >" + CateInfo.Article_Cate_Name + "</td>");
                    }
                    else{
                      sHtml.Append("  <td >--</td>");
                    }
                  
                    sHtml.Append("  <td>" + entity.Article_Addtime.ToShortDateString() + " </td>");
                    sHtml.Append("  <td >" + GetArticAudit(entity.Article_IsAudit) + "</td>");
                    if (entity.Article_IsAudit == 2)
                    {
                        sHtml.Append("<td><a style='color:#338fff' href='/Voice/" + entity.Article_CateID + "/" + entity.Article_ID + "'>查看</a></td>");
                    }
                    else {
                        sHtml.Append("<td><a style='color:#338fff' href='javascript:move(" + entity.Article_ID + ")' >撤销</a></td>");
                    }
                    sHtml.Append("</tr>");

                }
                CateInfo = null;
                cms = null;
                sHtml.Append("</tbody></table>");
                sHtml.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

                sHtml.Append("<tr><td align=\"right\"><div class=\"list-page\" style=\"float:right;padding-right:10px;\">");

                sHtml.Append(pub.PageStr(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount, type));
                Response.Write("</div></td></tr>");
                sHtml.Append("</table>");





            }
            else
            {
                sHtml.Append("<tr >");
                sHtml.Append("<td colspan=\"5\">暂无记录</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</tbody></table>");
            }
        }
      
        else
        {
            sHtml.Append("<table width=\"960\" cellspacing=\"0\" style=\"width: 960px;\">");


            sHtml.Append("<thead style='text-align:center'><tr>");
            sHtml.Append("  <td width=\"440\">文章标题</td>");
            sHtml.Append("  <td width=\"130\" >所属栏目</td>");
            sHtml.Append("  <td width=\"130\" >时间</td>");
            sHtml.Append("  <td width=\"130\" >审核状态</td>");
            sHtml.Append("  <td width=\"130\" >操作</td>");
            sHtml.Append("</tr></thead>  <tbody>");
            sHtml.Append("<tr >");
            sHtml.Append("<td colspan=\"5\">暂无记录</td>");
            sHtml.Append("</tr>");
            sHtml.Append("</tbody></table>");

        }

        return sHtml.ToString();
    }


    public string GetArticAudit(int Audit)
    {
        string Name = "";

        switch (Audit)
        {
            case 0:
                Name = "<span class=\"status_red\">待审核</span>";
                break;
            case 1:
                Name = "<span class=\"status_green\">初审通过</span>";
                break;
            case 2:
                Name = "<span class=\"status_red\">审核通过</span>";
                break;
            case 3:
                Name = "<span class=\"status_red\">审核不通过</span>";
                break;

        }

        return Name;
    }
    #endregion

}
