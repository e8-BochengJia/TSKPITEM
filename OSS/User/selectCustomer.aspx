<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

 
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script type="text/javascript">
    function member_add(obj) {
        $.ajax({
            url: encodeURI("Customer_do.aspx?action=check_member&member_id=" + SelectedValue(MM_findObj(obj)) + "&timer=" + Math.random()),
            type: "get",
            global: false,
            async: false,
            dataType: "html",
            success: function (data) {
                window.opener.$("#yhnr").html(data);
                window.close();
            },
            error: function () {
                alert("Error Script");
            }
        });
    }

    function SelectedValue(obj) {
        var _channel = "";
        for (var i = 0; i < obj.length; i++) {
            if (obj[i].checked) {
                if (_channel.length == 0) {
                    _channel = obj[i].value;
                }
                else {
                    _channel = _channel + "," + obj[i].value;
                }
            }
        }
        if (obj.length == null) {
            _channel = obj.value;
        }
        return _channel
    }
</script>
</head>
<body style="margin:10px;">

</body>
</html>
