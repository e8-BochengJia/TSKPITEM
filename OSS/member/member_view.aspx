<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private Member myApp;
    private MemberGrade Mgrade;

 
    private string Member_Grade_Name;

    private string Member_Email, Member_LoginMobile, Member_NickName, Member_Password, Member_VerifyCode, Member_LastLogin_IP, Member_Site, Member_Source, U_Member_QQ, U_Member_MSN, U_Member_Question, U_Member_Answer, U_Member_Bloodtype, U_Member_Realname, U_Member_Country, U_Member_Province, U_Member_City, U_Member_Address, U_Member_Job, U_Member_Postcode, U_Member_Edu, U_Member_School, U_Member_IDCard, U_Member_OpenID;
private int Member_ID,Member_Emailverify,Member_LoginMobileverify,Member_LoginCount,Member_CoinCount,Member_CoinRemain,Member_Trash,Member_Grade,Member_AllowSysEmail,Member_AllowSysMobile,U_Member_Male,U_Member_Mark,U_Member_Article_Commend,U_Member_State;
private DateTime Member_LastLogin_Time,Member_Addtime,U_MeMber_Birth;
private double Member_Account,Member_Frozen;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("833b9bdd-a344-407b-b23a-671348d57f76");
        myApp = new Member();
        tools = ToolsFactory.CreateTools();
        Mgrade = new MemberGrade();


        Member_ID = tools.CheckInt(Request.QueryString["Member_ID"]);
        MemberInfo entity = myApp.GetMemberByID(Member_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Member_ID = entity.Member_ID;
            Member_Email = entity.Member_Email;
            Member_Emailverify = entity.Member_Emailverify;
            Member_LoginMobile = entity.Member_LoginMobile;
            Member_LoginMobileverify = entity.Member_LoginMobileverify;
            Member_NickName = entity.Member_NickName;
            Member_Password = entity.Member_Password;
            Member_VerifyCode = entity.Member_VerifyCode;
            Member_LoginCount = entity.Member_LoginCount;
            Member_LastLogin_IP = entity.Member_LastLogin_IP;
            Member_LastLogin_Time = entity.Member_LastLogin_Time;
            Member_CoinCount = entity.Member_CoinCount;
            Member_CoinRemain = entity.Member_CoinRemain;
            Member_Addtime = entity.Member_Addtime;
            Member_Trash = entity.Member_Trash;
            Member_Grade = entity.Member_Grade;
            Member_Account = entity.Member_Account;
            Member_Frozen = entity.Member_Frozen;
            Member_AllowSysEmail = entity.Member_AllowSysEmail;
            Member_AllowSysMobile = entity.Member_AllowSysMobile;
            Member_Site = entity.Member_Site;
            Member_Source = entity.Member_Source;
            U_Member_QQ = entity.U_Member_QQ;
            U_Member_MSN = entity.U_Member_MSN;
            U_Member_Question = entity.U_Member_Question;
            U_Member_Answer = entity.U_Member_Answer;
            U_Member_Male = entity.U_Member_Male;
            U_MeMber_Birth = entity.U_MeMber_Birth;
            U_Member_Bloodtype = entity.U_Member_Bloodtype;
            U_Member_Realname = entity.U_Member_Realname;
            U_Member_Country = entity.U_Member_Country;
            U_Member_Province = entity.U_Member_Province;
            U_Member_City = entity.U_Member_City;
            U_Member_Address = entity.U_Member_Address;
            U_Member_Job = entity.U_Member_Job;
            U_Member_Postcode = entity.U_Member_Postcode;
            U_Member_Edu = entity.U_Member_Edu;
            U_Member_School = entity.U_Member_School;
            U_Member_IDCard = entity.U_Member_IDCard;
            U_Member_Mark = entity.U_Member_Mark;
            U_Member_Article_Commend = entity.U_Member_Article_Commend;
            U_Member_State = entity.U_Member_State;
            U_Member_OpenID = entity.U_Member_OpenID;
           
             Glaer.Trade.B2C.BLL.MEM.IMemberGrade MyMGBLL  = Glaer.Trade.B2C.BLL.MEM.MemberGradeFactory.CreateMemberGrade();;

             int MGradeId = 0;
            
            MemberGradeInfo GradeInfo = Mgrade.GetMemberGradeByID(Member_Grade);
            if (GradeInfo != null) { Member_Grade_Name = GradeInfo.Member_Grade_Name; MGradeId = GradeInfo.Member_Grade_ID; }
            else { Member_Grade_Name = "--"; }
            
            
            
            
            
            
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
        Mgrade = null;
     
    }
    
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>

</head>
<body>
  <div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">会员信息</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">会员名</td>
          <td class="cell_content"><% =Member_NickName%></td>
        </tr>
        <tr>
          <td class="cell_title">邮箱</td>
          <td class="cell_content"><% =Member_Email%></td>
        </tr>
        <tr>
          <td class="cell_title">找回密码问题</td>
          <td class="cell_content"><% =U_Member_Question%></td>
        </tr>
          <tr>
          <td class="cell_title">找回密码答案</td>
          <td class="cell_content"><% =U_Member_Answer%></td>
        </tr>
        <tr>
          <td class="cell_title">积分总数</td>
          <td class="cell_content"><% =Member_CoinCount%></td>
        </tr>
        <tr>
          <td class="cell_title">可用积分</td>
          <td class="cell_content"><% =Member_CoinRemain%></td>
        </tr>
        <tr>
          <td class="cell_title">注册时间</td>
          <td class="cell_content"><% =Member_Addtime%></td>
        </tr>
          <tr>
          <td class="cell_title">QQ</td>
          <td class="cell_content"><% =U_Member_QQ%></td>
        </tr>
          <tr>
          <td class="cell_title">MSN</td>
          <td class="cell_content"><% =U_Member_MSN%></td>
        </tr>
          <tr>
          <td class="cell_title">性别</td>
          <td class="cell_content"><%=Public.DisplaySex(U_Member_Male)%></td>
        </tr>
          <tr>
          <td class="cell_title">生日</td>
          <td class="cell_content"><%=U_MeMber_Birth.ToString("yyyy-MM-dd")%></td>
        </tr>
          <tr>
          <td class="cell_title">血型</td>
          <td class="cell_content"><%=U_Member_Bloodtype%></td>
        </tr>
          <tr>
          <td class="cell_title">真实姓名</td>
          <td class="cell_content"><% =U_Member_Realname%></td>
        </tr>
          <tr>
          <td class="cell_title">国家</td>
          <td class="cell_content"><%=U_Member_Country%></td>
        </tr>
        <tr>
          <td class="cell_title">省/自治区</td>
          <td class="cell_content"><%=U_Member_Province%></td>
        </tr>
        <tr>
          <td class="cell_title">市</td>
          <td class="cell_content"><%=U_Member_City%></td>
        </tr>
        
      
        <tr>
          <td class="cell_title">手机</td>
          <td class="cell_content"><% =Member_LoginMobile%></td>
        </tr>
      
      
        <tr>
          <td class="cell_title">联系地址</td>
          <td class="cell_content"><% =U_Member_Country+" "+U_Member_Province+" "+U_Member_City +" "+ U_Member_Address %></td>
        </tr>
        <tr>
          <td class="cell_title">邮编</td>
          <td class="cell_content"><% =U_Member_Postcode%></td>
        </tr>
          <tr>
          <td class="cell_title">职业</td>
          <td class="cell_content"><% =U_Member_Job%></td>
        </tr>
           <tr>
          <td class="cell_title">学历</td>
          <td class="cell_content"><% =U_Member_Edu%></td>
        </tr>
        
           <tr>
          <td class="cell_title">身份证号</td>
          <td class="cell_content"><% =U_Member_IDCard%></td>
        </tr>
        
        <tr>
          <td class="cell_title">登陆次数</td>
          <td class="cell_content"><% =Member_LoginCount%></td>
        </tr>
        <tr>
          <td class="cell_title">最后登录时间</td>
          <td class="cell_content"><% =Member_LastLogin_Time%></td>
        </tr>
        <tr>
          <td class="cell_title">最后登录IP</td>
          <td class="cell_content"><% =Member_LastLogin_IP%></td>
        </tr>
        <tr>
          <td class="cell_title">会员等级</td>
          <td class="cell_content"><% =Member_Grade_Name%></td>
        </tr>
      </table>
      <div style="text-align:right; margin:10px 0px;"><input name="button" type="submit" class="bt_orange" id="button" value="返回" onclick="history.go(-1);" /></div>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
