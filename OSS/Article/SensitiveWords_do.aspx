<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private SensitiveWords myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new SensitiveWords();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("ed203183-a081-4ec7-84af-8541a781e8bc");
                
                myApp.AddSensitiveWords();
                break;
            case "renew":
                Public.CheckLogin("36d9082b-e75e-4079-818f-a7b4c9a7cc31");
                
                myApp.EditSensitiveWords();
                break;
            case "move":
                Public.CheckLogin("c5da61a0-10a1-4d17-ab31-7bde1e7ddcf2");
                
                myApp.DelSensitiveWords();
                break;
            case "list":
                Public.CheckLogin("a8286f1e-b1cb-4523-821f-86a0d7c79793");
                
                Response.Write(myApp.GetSensitiveWordss());
                Response.End();
                break;

        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>