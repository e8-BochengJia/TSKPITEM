<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Member member = new Member();
        CMS cms = new CMS();
        Question question = new Question();
         ITools tools = ToolsFactory.CreateTools();
         int count = tools.CheckInt(Request["count"]);
        //member.Member_Login_Check("/member/index.aspx");
        string action = Request["action"];
        string type = Request["type"];
        string address ="";
        switch (action)
        {


            case "updatepassword":
               Response.Write(member.UpdateMemberPassword());
                break;
            case "findpassword":
                Response.Write(member.FindMemberPassword());
                break;
            case "findupdatepassword":
                Response.Write(member.UpdateFindPassword());
                   break;
            case "allowsysemail":
                member.UpdateMemberAllowSysEmail(1);
                break;
            case "cancelsysemail":
                member.UpdateMemberAllowSysEmail(0);
                break;
         
            //case "bindingmobile":
            //    member.Member_BindingMobile();
            //    break;
            case "updatemember":
                Response.Write(member.Update_Member());
                break;

            case "smscheckcode":
                string strmobile = Convert.ToString(Session["member_loginmobile"]);
                address = Request.ServerVariables["remote_addr"];
                Dictionary<string, string> smscheckcode = new Dictionary<string, string>();
                smscheckcode.Add("sign", strmobile);
                smscheckcode.Add("code", new Public_Class().Createvkey(6));
                smscheckcode.Add("expiration", DateTime.Now.AddSeconds(120).ToString());
                Session["sms_check"] = smscheckcode;

                //发送短信
                //new SMS().Send(strmobile, "短信效验码：" + Convert.ToString(smscheckcode["code"]), address);

                //System.IO.File.WriteAllText(@"e:\手机验证码.txt", smscheckcode["code"] + "\r\n");

                //Response.Write("{\"result\":\"true\", \"msg\":\"\"}");
                break;
            case "validateid":
                member.ValidateCurrentId();
                break;
            case "emailvalidate_send":
                member.EmailValidate_Send();
                break;
            case "emailvalidate_do":
                member.EmailValidate_Do();
                break;
            case "Coin":
                
                Response.Write(member.Member_Coin_List(type, "", ""));
                Response.End();
                break;
            case "Coin_index":
              
           
                Response.Write(member.Member_Coin_List(type, count));
                Response.End();
                break;
            case "article_index":
                int cateid = 0;
                //if (type == "1")
                //{
                //    cateid = 42;
                //}
                //else if (type =="2")
                //{
                //    cateid = 41;
                //}
                //else {
                //    cateid = 44;
                //}
                Response.Write(member.Member_article_List(type,0, count));
                Response.End();
                break;
            case "article_list":
                Response.Write(member.Member_article_List(type));
                Response.End();
                break;
            case "addarticle":
                Response.Write(cms.Add_Aritlce());
                Response.End();
                break;
            case "move_article":
                cms.Del_Aritlce();
                break;
            case "Question_Save":
                Response.Write(question.Question_Save());
                Response.End();
              
                break;
            case "addvote_m":
                Response.Write(question.AddVoteMember());
                Response.End();
                       
                break;
                
    

        }
        cms = null;
        member = null;
                question=null;

    }
</script>
