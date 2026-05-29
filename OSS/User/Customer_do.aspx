<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<script runat="server">
    
    private BigCustomer myApp;
    private Feedback feedback;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        myApp = new BigCustomer();
        feedback = new Feedback();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "check_member":
                string strPID;
                strPID = tools.CheckStr(Request.QueryString["member_id"]);
                if (strPID.Length > 0)
                {
                    IList<BigCustomerInfo> entityList = (IList<BigCustomerInfo>)Session["BigCustomerInfo"];
                    BigCustomerInfo entity = null;
                    string[] PIDARR = strPID.Split(',');
                    foreach (string addPID in PIDARR)
                    {
                        if (tools.CheckInt(addPID) < 1) { continue; }

                        entity = new BigCustomerInfo();
                        entity.Big_Customer_ID = int.Parse(addPID);
                        entityList.Add(entity);
                    }
                    Session["BigCustomerInfo"] = null;
                    Session["BigCustomerInfo"] = entityList;
                    entityList = null;
                }
                Response.Write(myApp.ShowCustomer());
                break;
            case "member_del":
                int member_id;
                member_id = tools.CheckInt(Request.QueryString["member_id"]);
                if (member_id > 0)
                {
                    IList<BigCustomerInfo> entityList = (IList<BigCustomerInfo>)Session["BigCustomerInfo"];
                    foreach (BigCustomerInfo entity in entityList)
                    {
                        if (entity.Big_Customer_ID == member_id) { entityList.Remove(entity); break; }
                    }
                    Session["BigCustomerInfo"] = null;
                    Session["BigCustomerInfo"] = entityList;
                    entityList = null;
                }

                Response.Write(myApp.ShowCustomer());
                break;  
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
