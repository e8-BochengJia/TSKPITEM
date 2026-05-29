<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Vote myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Vote();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("0b4dd57c-9f47-4d2a-a48e-32ab060ca268");
                
                myApp.AddVote();
                break;
            case "renew":
                Public.CheckLogin("2d0fab9b-e8f0-4c3f-9fe8-949f4416ef1f");
                
                myApp.EditVote();
                break;
            case "move":
                Public.CheckLogin("41e28e33-14e4-45d2-9e23-a63cd706f0e9");
                
                myApp.DelVote();
                break;
            case "list":
                Public.CheckLogin("a4aada81-2e0b-460d-9fff-a69eb6d57e54");
                
                Response.Write(myApp.GetVotes());
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