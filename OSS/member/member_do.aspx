<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<script runat="server">
    
    private Member myApp;
   
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        myApp = new Member();
      
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            
            case "list":
                Public.CheckLogin("3a9a9cdf-ef00-407d-98ef-44e23be397e8");
                
                Response.Write(myApp.GetMembers());
                Response.End();
                break;
            case "memberexport":
                Public.CheckLogin("29c1d7e3-ef38-4f80-80c8-b376efafe11d");

                myApp.Member_Export();
                Response.End();
                break;
            case "memberexport_all":
                Public.CheckLogin("29c1d7e3-ef38-4f80-80c8-b376efafe11d");

                myApp.Member_Export_All();
                Response.End();
                break;
            case "check_member":
                string strPID;
                strPID = tools.CheckStr(Request.QueryString["member_id"]);
                if (strPID.Length > 0)
                {
                    IList<MemberInfo> entityList = (IList<MemberInfo>)Session["EmailMemberInfo"];
                    MemberInfo entity = null;
                    string[] PIDARR = strPID.Split(',');
                    foreach (string addPID in PIDARR)
                    {
                        if (tools.CheckInt(addPID) < 1) { continue; }

                        entity = new MemberInfo();
                        entity.Member_ID = int.Parse(addPID);
                        entityList.Add(entity);
                    }
                    Session["EmailMemberInfo"] = null;
                    Session["EmailMemberInfo"] = entityList;
                    entityList = null;
                }
                Response.Write(myApp.ShowMember());
                break;
            case "member_del":
                int member_id;
                member_id = tools.CheckInt(Request.QueryString["member_id"]);
                if (member_id > 0)
                {
                    IList<MemberInfo> entityList = (IList<MemberInfo>)Session["EmailMemberInfo"];
                    foreach (MemberInfo entity in entityList)
                    {
                        if (entity.Member_ID == member_id) { entityList.Remove(entity); break; }
                    }
                    Session["EmailMemberInfo"] = null;
                    Session["EmailMemberInfo"] = entityList;
                    entityList = null;
                }

                Response.Write(myApp.ShowMember());
                break;
                
            case "edit_memberinfo_email":
                myApp.EditMember_Email_ByID();
                break;
            case "edit_memberinfo_mobile":
                myApp.EditMember_Mobile_ByID();
                break;
            case "edit_memberinfo_Grade":
                myApp.EditMember_Grade_ByID();
                break;
            case "edit_memberinfo_Recommend":
                myApp.EditMember_Recommend_ByID();
                break;
            case "normal":
                Public.CheckLogin("2a50f81a-fd42-41e4-b13b-9c52ae7c8e09");
                myApp.Member_Audit(0);
                break;
            case "frozen":
                Public.CheckLogin("2a50f81a-fd42-41e4-b13b-9c52ae7c8e09");
                myApp.Member_Audit(1);
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
