<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="System.Collections.Generic" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%

    Question myApp;
    ITools tools;
    myApp = new Question();
    tools = ToolsFactory.CreateTools();
    string action = Request["action"];
    string keyword, Q_id, product_id;
    int Q_cate = 0;
    switch (action)
    {
        case "new":
            Public.CheckLogin("0f8290b2-c31e-4a76-8e1b-078cedbbabcb");

            myApp.AddQuestion();
            break;
        case "renew":
            Public.CheckLogin("d1243e82-cc4e-4b77-a3c9-10c0eb60f499");

            myApp.editQuestion();
            break;
        case "move":
            Public.CheckLogin("46d0f1d1-9bb3-4ffd-afe6-8e214de43db4");

            myApp.DelQuestion();
            break;
        case "list":
            Public.CheckLogin("318a6535-6af3-4839-9393-816cbc75616d");

            Response.Write(myApp.GetQuestions());
            Response.End();
            break;
        case "Qhnew":
             Public.CheckLogin("29799310-e6bc-491c-812e-0d87be7200e2");
             myApp.AddQuestionHistory();
            break;
        case "Qhrenew":
            Public.CheckLogin("7e0a9a43-af8f-44c9-b00e-aa8de567f9e7");
            myApp.EditQuestionHistory();
            break;
        case "movehistory":
            Public.CheckLogin("4a2a3deb-cc3b-42eb-898e-0de38315fef6");

            myApp.DelQuestionHistory();
                break;
        case "list_history":
            Public.CheckLogin("0727f3b4-4edc-4e49-94a0-d728fe7d35ef");
            Response.Write(myApp.GetQuestionsHistory());
            Response.End();
            break;
        case "questionlist":
            Response.Write(myApp.SelectQuestion());
            Response.End();
            break;
        case "QHCoin":
            //string actiosn = Request["ID"];
            Response.Write(myApp.Coin_QuestionH());
            Response.End();
            break;
        case "refresh_product":
            keyword = Request["keyword"];
            Q_cate = tools.NullInt(Request["Q_cate"]);
            Q_id = tools.NullStr(Session["selected_productid"]);
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.AppendLine("<input type=\"hidden\" id=\"all_flag\" value=\"0\" />");
            sbHtml.AppendLine("<input type=\"hidden\" id=\"allids\" value=\"" + myApp.Get_ProductList_IDs() + "\" />");
            sbHtml.AppendLine("<input type=\"hidden\" id=\"selarrow\" value=\"0," + Q_id + "\" />");
            sbHtml.AppendLine("<div class=\"list_tip_div\" id=\"list_seltip\" ></div>");
            sbHtml.AppendLine("<table id=\"list\"></table>");
            sbHtml.AppendLine("<div id=\"pager\"></div>");
            sbHtml.AppendLine("<script type=\"text/javascript\">");
            sbHtml.AppendLine("jQuery(\"#list\").jqGrid({");
            sbHtml.AppendLine("url: '/Questions/question_do.aspx?action=questionlist&Q_cate=" + Q_cate + "&keyword=" + Server.UrlEncode(keyword) + "',");
            sbHtml.AppendLine("    datatype: \"json\",");
            sbHtml.AppendLine("    colNames: ['ID', '所属类别', '题目标题'],");
            sbHtml.AppendLine("    colModel: [");
            sbHtml.AppendLine("        {width:30,align:'center', name: 'id', index: 'id',sortable:false},");
            sbHtml.AppendLine("        { align: 'left', name: 'QuestionInfo.cate', index: 'QuestionInfo.cate', sortable: false },");
            sbHtml.AppendLine("        { align: 'left', name: 'QuestionInfo.Q_Question', index: 'QuestionInfo.Q_Question', sortable: false }");
            sbHtml.AppendLine("    ],");
            sbHtml.AppendLine("    sortname: 'QuestionInfo.ID',");
            sbHtml.AppendLine("    sortorder: \"desc\",");
            sbHtml.AppendLine("    rowNum: 10,");
            //sbHtml.AppendLine("    rowList:[10,20,40], ");
            sbHtml.AppendLine("    pager: 'pager', ");
            sbHtml.AppendLine("    multiselect: true,");
            sbHtml.AppendLine("    viewrecords:true,");
            sbHtml.AppendLine("    viewsortcols: [false,'horizontal',true],");
            sbHtml.AppendLine("    width: 597,");
            sbHtml.AppendLine("    height: \"100%\",");
            sbHtml.AppendLine("    onSelectRow: function(id,status){  ");
            sbHtml.AppendLine("    jqgrid_rowclick(id,status);");
            sbHtml.AppendLine("    jqgrid_seltip_display();");
            sbHtml.AppendLine("    }, ");
            sbHtml.AppendLine("    loadComplete:function(){");
            sbHtml.AppendLine("        jqgrid_selarry();");
            sbHtml.AppendLine("        jqgrid_seltip_display();");
            sbHtml.AppendLine("    }");

            sbHtml.AppendLine("    });");
            sbHtml.AppendLine("    jqgrid_allclick();");
            sbHtml.AppendLine("</script>");
            Response.Write(sbHtml.ToString());
            Response.End();
            break;
        case "saveproductid":
            Session["selected_productid"] = tools.NullStr(Request["productid"]);
            if (tools.NullStr(Request["productid"]) == "0,")
            {
                Session["selected_productid"] = "";
            }
            break;
        case "showproduct":
            product_id = tools.NullStr(Session["selected_productid"]);
            int limit, group;
            limit = tools.CheckInt(Request["limit"]);
            group = tools.CheckInt(Request["group"]);
            if (group == 0)
            {
                if (limit == 0)
                {
                    if (product_id.Length > 0)
                    {
                        Response.Write(myApp.ShowProduct(product_id));
                    }
                    else
                    {
                        Response.Write("<span class=\"pickertip\">已选择题目</span>");
                    }
                }
               
            }
            else
            {
                Response.Write(myApp.ShowProduct(product_id));
            }
            break;

    }


    myApp = null;
    tools = null;

%>