<%@ Page Language="C#" CodePage="65001"%>
<% 
   Public.CheckLogin("all");
 %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Top</title>
    <link href="/CSS/Style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("#switchleft").bind("click", function(){
                if($("#mainframeset", parent.document).attr("cols") == "210,*"){
                    $("#mainframeset", parent.document).attr("cols", "0,*");
                }else{
                    $("#mainframeset", parent.document).attr("cols", "210,*");
                }
            }); 
        });
    </script>
</head>
<body>
        <table id="layout-top" width="100%" border="0" cellspacing="0" cellpadding="0" class="top">
        <tr>
            <td id="layout-logo" class="logo withoutlogo">
                <ul>
                    <li><a href="javascript:parent.location.href='/';"><img src="/images/logo.png" border="0" class="iconlogo" /></a><span><a href="javascript:void(0);" onclick="javascript:switchMenu();"><img src="/images/layout/icon_menu.png" class="iconmenu" /></a></span></li>
                </ul>
            </td>
            <td id="layout-nav" class="nav">
                <ul>
                    <li class="on"><a href="javascript:void(0);" onclick="javascript:menuChange(this);" id="dashboard">控制台</a></li>

                    <li><a href="javascript:void(0);" onclick="javascript:menuChange(this);" id="marketing">广告</a></li>

                    <li><a href="javascript:void(0);" onclick="javascript:menuChange(this);" id="content">内容</a></li>

                    <li><a href="javascript:void(0);" onclick="javascript:menuChange(this);" id="member">会员</a></li>

                    <li><a href="javascript:void(0);" onclick="javascript:menuChange(this);" id="system">系统</a></li>
                </ul>
            </td>
            <td id="layout-info" class="info">
                <ul>
                    <li><img src="/images/layout/icon_user.png" /> <%=Session["User_Name"]%></li>
                    <li><img src="/images/layout/icon_logout.png" /> <a href="/logindo.aspx?action=loginout">退出</a></li>
                </ul>
            </td>
        </tr>
    </table>
<script type="text/javascript">
    function switchMenu(status) {
        switch (status) {
            case "on":
                $("#mainframeset", parent.document).attr("cols", "240,*");
                $("#layout-logo").removeClass("withoutlogo");
                break;
            case "off":
                $("#mainframeset", parent.document).attr("cols", "0,*");
                $("#layout-logo").addClass("withoutlogo");
                break;
            default:
                if ($("#mainframeset", parent.document).attr("cols") == "240,*") {
                    $("#mainframeset", parent.document).attr("cols", "0,*");
                    $("#layout-logo").addClass("withoutlogo");
                } else {
                    $("#mainframeset", parent.document).attr("cols", "240,*");
                    $("#layout-logo").removeClass("withoutlogo");
                }
                break;
        }
    }

    function menuChange(currobj) {

        $("#layout-nav").find("li").each(function () {
            $(this).removeAttr("class", "");
        });

        $(currobj).parent().attr("class", "on");

        var mainTarget = "/main.aspx", leftTarget = "/public/left.aspx";

        switch (currobj.id) {
            case "dashboard":
                mainTarget = "/main.aspx";
                leftTarget = "/public/left.aspx?channel=0";
                switchMenu("off");
                break;
            case "marketing":
                mainTarget = "/ad/ad.aspx";
                leftTarget = "/public/left.aspx?channel=1";
                switchMenu("on");
                break;

            case "content":
                mainTarget = "/article/article_list.aspx?menu_id=202";
                leftTarget = "/public/left.aspx?channel=2";
                switchMenu("on");
                break;
            case "system":
                mainTarget = "/system.aspx";
                leftTarget = "/public/left.aspx?channel=3";
                switchMenu("on");
                break;
            case "member":
                mainTarget = "/member/member_list.aspx";
                leftTarget = "/public/left.aspx?channel=4";
                switchMenu("on");
                break;


            default: break;
        }

        window.parent.main.location.href = mainTarget;
        window.parent.left.location.href = leftTarget;
    }
</script>
</body>
</html>
