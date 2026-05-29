<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        ITools tools;
        tools = ToolsFactory.CreateTools();
        CMS cms = new CMS();

        string action = Request["action"];
        switch (action)
        {
            case "RecommendList":
                cms.Home_Recommend(tools.NullInt(Request["PageSize"]));
                break;
        }
    }
</script>
