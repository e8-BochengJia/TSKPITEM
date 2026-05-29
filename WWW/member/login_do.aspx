<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<script runat="server">

  
    protected void Page_Load(object sender, EventArgs e)
    {
        //weixin wx = new weixin();
        ITools tools = ToolsFactory.CreateTools();
        Member member = new Member();
        string openid = tools.CheckStr(Request["openid"]);
        string type = tools.CheckStr(Request["type"]);

  
        string action = Request["action"];
        switch (action)
        {
            case "login":
                Response.Write(member.Member_Login());
                break;      
           case "checklogin":
                MemberInfo memberinfo = member.GetMemberByID();
                  if (memberinfo == null)
                  {
                      Response.Write("fail");
                      Response.End();
                  }
                break;
            case "logout":
                member.Member_LogOut();
                break;
            case "register":
                Response.Write(member.Member_Register());
                break;
            case "getpass":
                member.member_getpass_sendmail();
                break;
            case "verify":
                member.member_getpass_verify();
                break;
            case "resetpass":
                member.member_getpass_resetpass();
                break;
            case "ajax_checklogin":
                member.Member_Login_Check_Ajax();
                break;

            case "member_getpass_validate":
                member.member_getpass_validate();
                break;
        }

    }
</script>