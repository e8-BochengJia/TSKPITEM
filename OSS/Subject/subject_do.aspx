<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    private ArticleSubject myApp;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new ArticleSubject();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("2b40c0e9-1543-48e5-8836-d7addfee4236");

                myApp.AddArticleSubject();
                break;
            case "renew":
                Public.CheckLogin("ae5b5047-b85f-4934-84a0-e4f4f898dd78");

                myApp.EditArticleSubject();
                break;
            case "move":
                Public.CheckLogin("79d6139b-950d-4598-9a90-1cb67505205e");

                myApp.DelArticleSubject();
                break;
            case "list":
                Public.CheckLogin("639ad269-9a65-421e-b70f-825df98c2437");
                Response.Write(myApp.GetArticleSubjects());
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
