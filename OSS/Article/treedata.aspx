<%@ Page Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        //Public.CheckLogin("1a3208d0-70a4-49dd-8010-400f1254535a");
        ArticleCate cate = new ArticleCate();
        string Article_CateIDs = Request.QueryString["Article_CateIDs"];
        
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.ContentType = "text/xml";
        Response.Write("<?xml version='1.0' encoding='utf-8'?>");
        Response.Write("<tree id=\"0\">");
        Response.Write(cate.ArticleCateTree(0, Article_CateIDs));
        Response.Write("</tree>");
    }
</script>
