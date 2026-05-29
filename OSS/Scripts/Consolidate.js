function Consolidatecheck_payline(obj) {

    $('#tip_' + obj).load('/ConsolidateOrder/Consolidate_Policy_do.aspx?action=checkpayline&val=' + $('#' + obj).val() + '&timer=' + Math.random())
        if ($("#tip_" + obj).html().indexOf("error") > 0) {
            return false;
        }
        else {
            return true;
        }
}

function check_membergrade(chk_name) {
    var chkflag = false;
    for (var i = 0; i < $('input').length; i++) {

        if ($('input:eq(' + i + ')').attr('name') == chk_name && $('input:eq(' + i + ')').attr('checked')) {
            chkflag = true;
            break;
        }
    }
    if (!chkflag) {
        $("#tip_membergrade").html("<span class=\"tip_bg_error\">请选择针对会员！</span>");
    }
    else {
        $("#tip_membergrade").html("");
    }
    if ($("#tip_membergrade").html().indexOf("error") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function Consolidatecheck_price(selobj, obj) {
    if ($("#" + selobj).attr("checked")) {
        $('#tip_' + obj).load('/ConsolidateOrder/Consolidate_Policy_do.aspx?action=checkprice&val=' + $('#' + obj).val() + '&timer=' + Math.random())
    }

    if ($("#tip_" + obj).html().indexOf("error") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function Consolidatecheck_percent(selobj, obj) {
    if ($("#" + selobj).attr("checked")) {
        $('#tip_' + obj).load('/ConsolidateOrder/Consolidate_Policy_do.aspx?action=checkpercent&val=' + $('#' + obj).val() + '&timer=' + Math.random())
    }

    if ($("#tip_" + obj).html().indexOf("error") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function check_blank(obj) {
    $('#tip_' + obj).load('/ConsolidateOrder/Consolidate_Policy_do.aspx?action=isblank&val=' + $('#' + obj).val() + '&timer=' + Math.random())
    if ($("#tip_" + obj).html().indexOf("error") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function Consolidatecheck_favor_policy() {
    $.ajaxSetup({ async: false });
    var ch1 = Consolidatecheck_payline('Consolidate_Policy_Payline');
    var ch3 = Consolidatecheck_price('Consolidate_Policy_Manner1', 'Consolidate_Policy_Price');
    var ch5 = Consolidatecheck_percent('Consolidate_Policy_Manner2', 'Consolidate_Policy_Percent');
    //var member = check_membergrade('Member_Grade');
    var member = true;
    var blank = check_blank('Consolidate_Policy_Name');
    if (ch1 && ch3 && ch5 && member && blank) {
        return true;
    }
    else {
        return false;
    }


}