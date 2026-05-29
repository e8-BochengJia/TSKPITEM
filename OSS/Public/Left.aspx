<%@ Page Language="C#" CodePage="65001"%>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<% SysMenu Menu = new SysMenu();
   ITools tools;
   Public.CheckLogin("all");
   tools = ToolsFactory.CreateTools();
   int channel_id = tools.CheckInt(Request["channel"]); %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="height:100%;">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Menu</title>
<link href="/css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/scripts/jquery.js"></script>
<script>
    function menuOn(currobj)
    {
        $("#layout-menu").find("li").each(function () {
            if (!$(this).hasClass("group")) {
                $(this).removeAttr("class", "");
            }
        });
        $(currobj).attr("class", "on");
    }

    function menuFold(currobj) {
        $("#layout-menu").find("li").each(function () {
            if (!($(this)[0] === $(currobj)[0])) {
                if ($(this).hasClass("group")) {
                    if ($(this).hasClass("open")) {
                        $(this).siblings().hide();
                        $(this).removeClass("open");
                        $(this).addClass("fold");
                    }
                }
            }
        });

        if ($(currobj).hasClass("open")) {
            $(currobj).siblings().hide(100);
            $(currobj).removeClass("open");
            $(currobj).addClass("fold");
        } else {
            $(currobj).siblings().show(100);
            $(currobj).removeClass("fold");
            $(currobj).addClass("open");
        }
    }
</script>
<style type="text/css">
    img{vertical-align:middle; margin-right:3px;}
</style>
</head>
<body style="height:100%;">
    <% Menu.Sys_Menu_Display(channel_id);%>
</body>
</html>