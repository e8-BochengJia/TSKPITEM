

function delProduct(goods_id) {
    $.ajaxSetup({ async: false });
    $('#cart_product').load('save_do.aspx?action=goodstmp_del&goods_id=' + goods_id + '&fresh=' + Math.random());
    location=$("#page_path").val();
}

function editBuyamount(goods_id, amount) {
    $.ajaxSetup({ async: false });
    $('#cart_product').load('save_do.aspx?action=goodstmp_edit&goods_id=' + goods_id + '&buyamout=' + amount + '&fresh=' + Math.random());
}

function sltcommunity(strnumber, strname, state, city, county) {
    $("#Orders_Address_StreetAddress").val(strname);
    $("#U_Orders_DrugDelivery").val(strnumber);
    $("#searchdiv").hide();
    
    setAddressee();

    if (state.length > 0) {
        RefillAddress('div_area', 'Orders_Address_State', 'Orders_Address_City', 'Orders_Address_County', state, city, county);
    }
}

function oncommunity(obj) { 
	$(obj).attr("class", "on"); 
}
function offcommunity(obj) { 
	$(obj).attr("class", "off");
}

var closeTimer = null;
function KeepDisplay(){ clearTimeout(closeTimer);}
function CloseDisplay(obj){ closeTimer = setTimeout(function () { $(obj).hide(); closeTimer = null; }, 500);}

/*
$(document).ready(function() {
    $("#Orders_Address_StreetAddress").after("<div id=\"searchdiv\" class=\"searchdiv\" style=\"margin-top:26px; height:200px; overflow-y:scroll; border-top:none;\"></div>");
    $("#searchdiv").focus(function () { KeepDisplay(); });
    $("#searchdiv").blur(function () { CloseDisplay(this); });
    $("#Orders_Address_StreetAddress").focus(function () { KeepDisplay(); });
    
    $("#Orders_Address_StreetAddress").keyup(function(evt) {
    
        var tagDiv = "#searchdiv";
        var key = window.event ? evt.keyCode : evt.which;
        var itemCount = $(tagDiv + ' li').length;
        if ($(tagDiv).css("display") != "none") {
            switch (key) {
                case 38:    //向上
                    $(tagDiv + ' li:nth-child(' + itemIndex + ')').attr("class", "off");
                    if (itemIndex > 1) itemIndex--;
                    $(tagDiv + ' li:nth-child(' + itemIndex + ')').attr("class", "on");
                    $(this).val($(tagDiv + ' li:nth-child(' + itemIndex + ')').text());
                    return;
                case 40:    //向下
                    $(tagDiv + ' li:nth-child(' + itemIndex + ')').attr("class", "off");
                    if (itemIndex < itemCount) itemIndex++;
                    $(tagDiv + ' li:nth-child(' + itemIndex + ')').attr("class", "on");
                    $(this).val($(tagDiv + ' li:nth-child(' + itemIndex + ')').text());
                    return;
                case 13:    //回车
                    $(tagDiv + ' li').each(function(Index, entity) {
                        if ((Index + 1) == itemIndex) {
                            $(entity).click();
                            return;
                        }
                    })
                    return;
                default:
                    break;
            }
        }
        itemIndex = 0;

        var keyword = $(this).val();
        if (keyword.length == 0) return;
        
        var offset = $(this).offset();
        var intwidth = $(this).width() + 4;
        $("#searchdiv").css({
            "left": offset.left + "px",
            "width": intwidth + "px"
        });

        var html = "";
        $.ajax({ type: "get", global: false, async: false, dataType: "json",
            url: encodeURI("save_do.aspx?action=searchcomplete&keyword=" + keyword + "&timer=" + Math.random()),
            success: function(data) {
                if (data != null) {
                    html += "<ul>";
                    $.each(data, function(entityIndex, entity) {
                        html += "   <li onclick=\"sltcommunity('" + entity["deliveryunit"] + "', '" + entity["name"] + "', '" + entity["state"] + "', '" + entity["city"] + "', '" + entity["county"] + "');\" onmouseover=\"oncommunity(this);\" onmouseout=\"offcommunity(this);\">" + entity["name"] + "(" + entity["note"] + ")</li>";
                    })
                    html += "</ul>";
                    $("#searchdiv").html(html);
                    $("#searchdiv").show();
                }
                else { $("#searchdiv").hide(); }
            }
        });
        offset = null;
    });
});
*/

function sltdeliveryunit(strnumber, strname) {
    $("#U_Orders_DrugDelivery").val(strnumber);
    $("#sltdelivery").hide();
    setAddressee();
}

//new 根据地区刷新门店
function findstore() {
    var keyword = "";
    $("#U_Orders_DrugDelivery").load(encodeURI("save_do.aspx?action=findstore&county="+ $("#store_county").val() +"&keyword="+ keyword +"&timer=" + Math.random()));
}

//选择配送大队开始
$(document).ready(function() {
    $("#U_Orders_DrugDelivery").before("<div id=\"sltdelivery\" class=\"searchdiv\" style=\"border-bottom:none;\"></div>");
    $("#sltdelivery").focus(function () { KeepDisplay(); });
    $("#sltdelivery").blur(function () { CloseDisplay(this); });
    $("#U_Orders_DrugDelivery").focus(function () { KeepDisplay(); });
    
    $("#U_Orders_DrugDelivery").keyup(function(evt) {

        var tagDiv = "#sltdelivery";
        var key = window.event ? evt.keyCode : evt.which;
        var itemCount = $(tagDiv + ' li').length;
        if ($(tagDiv).css("display") != "none") {
            switch (key) {
                case 38:    //向上
                    $(tagDiv + ' li:nth-child(' + itemIndex + ')').attr("class", "off");
                    if (itemIndex > 1) itemIndex--;
                    $(tagDiv + ' li:nth-child(' + itemIndex + ')').attr("class", "on");
                    $(this).val($(tagDiv + ' li:nth-child(' + itemIndex + ')').text());
                    return;
                case 40:    //向下
                    $(tagDiv + ' li:nth-child(' + itemIndex + ')').attr("class", "off");
                    if (itemIndex < itemCount) itemIndex++;
                    $(tagDiv + ' li:nth-child(' + itemIndex + ')').attr("class", "on");
                    $(this).val($(tagDiv + ' li:nth-child(' + itemIndex + ')').text());
                    return;
                case 13:    //回车
                    $(tagDiv + ' li').each(function(Index, entity) {
                        if ((Index + 1) == itemIndex) {
                            $(entity).click();
                            return;
                        }
                    })
                    return;
                default:
                    break;
            }
        }
        itemIndex = 0;

        var keyword = $(this).val();
        if (keyword.length == 0) return;

        var offset = $(this).offset();
        var intwidth = $(this).width() + 4;
        $("#sltdelivery").css({
            "left": offset.left + "px",
            "width": intwidth + "px",
            "bottom": ($(window).height() - offset.top) + "px"
        });

        $.ajax({ type: "get", global: false, async: false, dataType: "html",
            url: encodeURI("save_do.aspx?action=deliveryunit&keyword=" + keyword + "&timer=" + Math.random()),
            success: function(data) {
                if (data.length > 0) { 
                    $("#sltdelivery").html(data); $("#sltdelivery").show(); 
                    //itemIndex = ($(tagDiv + ' li').length + 1); 
                }
                else { $("#sltdelivery").hide(); }
            }
        });
        offset = null;
    });
})

function inputPhoneRefresh(inval) {
    var strURL = document.URL.replace(/tel=\w*/i, 'tel=' + inval);
    strURL = strURL.replace('&Type=new', '');
    strURL = strURL.replace(/cardid=\d+/i, 'cardid=0');
    location = strURL;
}

//显示会员信息
function InputCardFilling(cardno) {
    $.ajax({ type: "get", global: false, async: true, cache: false, dataType: "json",
        url: encodeURI("save_do.aspx?action=getcardpoint&cardno=" + cardno),
        success: function(data) {
            if (data != null) {
                $("#div_cards_total").text(data["total"]);
                $("#div_cards_available").text(data["available"]);
                $("#div_cards_availableonline").text(data["availableonline"]);
                $("#div_cards_availableunderline").text(data["availableunderline"]);
            }
        }
    });
}

//刷新购物车
function RefreshCart() {
    $.ajax({ global: false, async: true, cache: false, type: "get",
        url: encodeURI("save_do.aspx?action=refresh_cart"),
        dataType: "html",
        success: function(data) {
            $("#cart_product").html(data);
        }
    });
}

function RefreshDelivery() {
    var state = $("#Orders_Address_State").val();
	var city = $("#Orders_Address_City").val();
	var county = $("#Orders_Address_County").val();
	
    $("#div_delivery").load(encodeURI("save_do.aspx?action=refresh_delivery&state="+ state +"&city="+ city +"&county="+ county +"&timer="+ Math.random()));
}

function RefreshDeliveryFee(id, cod, name) {
    $.getJSON(encodeURI("save_do.aspx?action=deliveryfee&delivery_id=" + id + "&timer=" + Math.random()), function(data) {
        RefreshCart();
        RefreshPay(cod)
    })
    
    if (name == "连锁门店自提" || name == "连锁门店配送")
        $("#selectstore").show();
    else
        $("#selectstore").hide();
}

function RefreshPay(cod) {
    $("#div_pay").load(encodeURI("save_do.aspx?action=refresh_pay&cod="+ cod +"&timer="+ Math.random()));
}

function SelectedPay(id) {
    $.getJSON(encodeURI("save_do.aspx?action=selectedpay&pay_id=" + id + "&timer=" + Math.random()), function(data) {
        RefreshCart();
    });
}

//顶部选择地址显示
function addressChoose(state, city, county, street, postcode, name, phone, mobile, drugdelivery, cardno, note) {

    note = note.replace(/<br \/>/g, "\r\n");
    note = note.replace(/&gt;/, ">");
    note = note.replace(/&lt;/, "<");
    
    $("#Orders_Address_Name").val(name);
    $("#Orders_Address_Phone_Number").val(phone);
    $("#Orders_Address_Mobile").val(mobile);
    $("#Orders_Address_Zip").val(postcode);
    $("#Orders_Address_StreetAddress").val(street);
    $("#U_Orders_DrugDelivery").val(drugdelivery);
    $("#U_Orders_CardNo").val(cardno);
    $("#address_note").val(note);
    
    
    if (state.length > 0) {
        RefillAddress('div_area', 'Orders_Address_State', 'Orders_Address_City', 'Orders_Address_County', state, city, county);
    }
    
    InputCardFilling(cardno);
    
	setAddressee();
}

//记录当前收货人信息
function setAddressee() {
	var buyerid = $("#Orders_BuyerID").val();
	var strname = $("#Orders_Address_Name").val();
	var phone = $("#Orders_Address_Phone_Number").val();
	var mobile = $("#Orders_Address_Mobile").val();
	var zip = $("#Orders_Address_Zip").val();
	var streetaddress = $("#Orders_Address_StreetAddress").val();
	var state = $("#Orders_Address_State").val();
	var city = $("#Orders_Address_City").val();
	var county = $("#Orders_Address_County").val();
	var cardno = $("#U_Orders_CardNo").val();
	var drugdelivery = $("#U_Orders_DrugDelivery").val();
	var note = $("#address_note").val();
	var adminnote = $("#Orders_Admin_Note").val();
	var ticket_type = $("input:radio[name='ticket_type'][checked]").val();
	var ticket_title = $("#ticket_title").val();
	
	var qurl = "save_do.aspx?action=setaddressee&buyerid="+ buyerid +"&name="+ strname +"&phone="+ phone;
	qurl += "&mobile=" + mobile + "&zip=" + zip + "&state=" + state + "&city=" + city + "&county=" + county;
	qurl += "&streetaddress=" + streetaddress + "&cardno=" + cardno + "&drugdelivery="+ drugdelivery +"&note="+ note +"&adminnote="+ adminnote;
	qurl += "&ticket_type=" + ticket_type + "&ticket_title=" + ticket_title + "&t=" + Math.random();
	
	$.get(encodeURI(qurl));
}

//检查邮箱
function check_member_email() {
    $("#email_tip").load(encodeURI("/callcenter/save_do.aspx?action=ajaxmemberemail&email="+ $("#Member_Email").val()));
}

function RefillAddress(targetdiv, statename, cityname, countyname, statecode, citycode, countycode) {
    $.ajax({
        type: "get", global: false, async: false, dataType: "html",
        url: encodeURI("/public/ajax_address.aspx?action=fill&targetdiv="+ targetdiv +"&statename="+ statename +"&cityname="+ cityname +"&countyname="+ countyname +"&statecode="+ statecode +"&citycode="+ citycode +"&countycode="+ countycode +"&timer="+Math.random()),
        success:function(data){
	        $("#"+ targetdiv).html(data);
            $("#"+ statename).val(statecode);
            $("#"+ cityname).val(citycode);
            $("#"+ countyname).val(countycode);
            if (countycode > 0)
                RefreshDelivery();
                setAddressee();
        }
    });
}