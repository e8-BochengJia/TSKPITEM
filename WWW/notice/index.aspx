<%@ Page Language="C#" %>

<%@ Register Src="~/Public/topcate.ascx" TagName="topcate" TagPrefix="uc1" %>
<%@ Register Src="~/Public/bottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%
    Notice notice = new Notice();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();

    string SEO_Keywords = tools.NullStr(Application["Site_Keyword"]);
    string SEO_Description = tools.NullStr(Application["Site_Description"]);
    string SEO_Title = pub.SEO_TITLE();
    int cate_ID = 0;
    cate_ID = tools.CheckInt(Request["noticecate_id"]);
   
    Session["CurrentPotion"] = "31";
    AD ad = new AD();

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=SEO_Title%></title>
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="Keywords" content="<% = SEO_Keywords%>" />
    <meta name="Description" content="<%=SEO_Description%>" />

    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index.css"))%>" rel="stylesheet" />

    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>

    <script type="text/javascript">

        $(function () {
            //$("#em_li i").click(function () {
            //    $(this).siblings('li').removeClass('active');  // 删除其兄弟元素的样式
            //    $(this).addClass('active');                    // 为点击元素添加类名
            //});
            $("#da").click(function () {
                $("#div_show").css("font-size", "20px");
                $("#div_show").css("line-height", "38px");

               
                $("#div_show p").css("font-size", "20px");
                $("#div_show p").css("line-height", "38px");

                $(this).addClass('active');
                $("#zhong").removeClass('active');
                $("#xiao").removeClass('active');

            });
            $("#zhong").click(function () {
                $("#div_show").css("font-size", "16px");
                $("#div_show").css("line-height", "32px");


                $("#div_show p").css("font-size", "16px");
                $("#div_show p").css("line-height", "32px");

                $(this).addClass('active');
                $("#da").removeClass('active');
                $("#xiao").removeClass('active');
            });
            $("#xiao").click(function () {
                $("#div_show").css("font-size", "12px");
                $("#div_show").css("line-height", "26px");


                $("#div_show p").css("font-size", "12px");
                $("#div_show p").css("line-height", "26px");
                $(this).addClass('active');
                $("#da").removeClass('active');
                $("#zhong").removeClass('active');

            });
        });
        function checkInt(n, max) {
            var regex = /^\d+$/;
            if (regex.test(n)) {
                if (n > max || n <= 0) {
                    $("#listpagenum").val(max);
                }
            } else {
                $("#listpagenum").val(1);
            }
        }
    </script>

</head>
<body style="background: #f5f5f9;">

    <uc1:topcate ID="top" runat="server" />
    <p class="crumb-nav">
        <img src="/images/icon-nav.png">当前位置 > <a href="/">首页 </a><%=notice.GetNotice_Cate_Nav(cate_ID, " &gt; ")%></p>
    <div class="w-1200 clearfix" style="margin-bottom: 30px;">
        <!-- 左侧 -->
        <div class="kx-state-left">
            <div class="ky-tit">
                <span>Notice</span>
                <p>通知公告</p>

            </div>
            <%=notice.GetNotice_CateLeft(cate_ID) %>
        </div>

       
          <%notice.GetNotice_ListRight(cate_ID); %>
         
        

    </div>
 
    <uc1:bottom ID="bottom" runat="server" />
</body>
</html>
