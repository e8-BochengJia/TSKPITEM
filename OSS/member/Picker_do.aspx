<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%
    ITools tools;
  
    string keyword;
    string member_id;
  
    tools = ToolsFactory.CreateTools();
    Member myApp = new Member();
    string action = Request["action"];
    switch (action)
    {
     
      
        case "memberlist":
            Response.Write(myApp.SelectMember());
            Response.End();
            break;
    
        case "refresh_member":
            keyword = Request["keyword"];
            member_id = tools.NullStr(Session["selected_memberid"]);
            Response.Write("<input type=\"hidden\" id=\"all_flag\" value=\"0\" />");
            Response.Write("<input type=\"hidden\" id=\"allids\" value=\"" + myApp.Get_MemberList_IDs() + "\" />");
            Response.Write("<input type=\"hidden\" id=\"selarrow\" value=\"0," + member_id + "\" />");
            Response.Write("<div class=\"list_tip_div\" id=\"list_seltip\"></div>");
            Response.Write("<table id=\"list\"></table>");
            Response.Write("<div id=\"pager\"></div>");
            Response.Write("<script type=\"text/javascript\">");
            Response.Write("jQuery(\"#list\").jqGrid({");
            Response.Write("url: 'picker_do.aspx?action=memberlist&keyword=" + Server.UrlEncode(keyword) + "',");
            Response.Write("    datatype: \"json\",");
            Response.Write("    colNames: ['ID','昵称','注册邮箱'],");
            Response.Write("    colModel: [");
            Response.Write("        {width:30,align:'center', name: 'id', index: 'id',sortable:false},");
            Response.Write("        {align:'left', name: 'MemberInfo.Member_NickName', index: 'MemberInfo.Member_NickName'},");
            Response.Write("        {align:'left', name: 'MemberInfo.Member_Email', index: 'MemberInfo.Member_Email'}");
       
            Response.Write("    ],");
            Response.Write("    sortname: 'MemberInfo.Member_ID',");
            Response.Write("    sortorder: \"desc\",");
            Response.Write("    rowNum: 10,");
            //Response.Write("    rowList:[10,20,40], ");
            Response.Write("    pager: 'pager', ");
            Response.Write("    multiselect: true,");
            Response.Write("    viewrecords:true,");
            Response.Write("    viewsortcols: [false,'horizontal',true],");
            Response.Write("    width: 597,");
            Response.Write("    height: \"100%\",");
            Response.Write("    onSelectRow: function(id,status){  ");
            Response.Write("    jqgrid_rowclick(id,status);");
            Response.Write("    jqgrid_seltip_display();");
            Response.Write("    }, ");
            Response.Write("    loadComplete:function(){");
            Response.Write("        jqgrid_selarry();");
            Response.Write("        jqgrid_seltip_display();");
            Response.Write("    }");

            Response.Write("    });");
            Response.Write("    jqgrid_allclick();");
            Response.Write("</script>");
            break;
        case "showmember":
            member_id = tools.NullStr(Session["selected_memberid"]);
            if (member_id.Length > 0)
            {
                Response.Write(myApp.ShowMember(member_id));
            }
            else
            {
                Response.Write("<span class=\"pickertip\">已选择会员</span>");
            }
            break;
       
        case "savememberid":
            Session["selected_memberid"] = tools.NullStr(Request["memberid"]);
            if (tools.NullStr(Request["memberid"]) == "0,")
            {
                Session["selected_memberid"] = "";
            }
            break;
    


     
    }
    myApp = null;
%>
