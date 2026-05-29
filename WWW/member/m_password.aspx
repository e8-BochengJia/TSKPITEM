<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/topm.ascx" TagPrefix="uctop1" TagName="top" %>
<%@ Register Src="~/Public/Bottom.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Session["Cur_Position"] = "password";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();

    Member MEM = new Member();

    MEM.Member_Login_Check("/member/index.aspx");
    MemberInfo memberinfo = MEM.GetMemberByID();
    if (memberinfo == null)
    {
        Response.Redirect("/member/login.aspx?action=logout");
    }
    string gradename = "网站会员";
    MemberGradeInfo entity = MEM.GetMemberGradeByMemberID();
    if (entity == null)
    {
        entity = new MemberGradeInfo();
        gradename = entity.Member_Grade_Name;
    }    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="会员中心 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
   <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>
    <link href="/css/member_center.css" rel="stylesheet" type="text/css" />
    <link href="/layjs/css/layui.css" rel="stylesheet" />
    <script type="text/javascript" src="/layjs/layui.js"></script>
    <style type="text/css">
        .add-product-centre ul li > span
        {
            width:100px!important;
        }

    </style>
        <script type="text/javascript">
            layui.use('layer', function () {
                var layer = layui.layer;
            });
            $(function () {

                $("#summit_date").click(function () {

                    $.ajax({
                        type: "GET",
                        url: "/member/member_do.aspx?action=updatepassword",
                        data: $("#frm_memeber").serialize(),
                        dataType: "json",
                        success: function (data) {
                            if (data.err != "") {

                                layer.alert(data.err, { icon: 2 });
                            } else {
                                layer.alert("操作成功", function () { location.href = data.url; });
                            }
                        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                            layer.alert(textStatus);

                        }
                    });
                });
            });

    </script>

</head>
<body style="background: #f5f5f9;">

    <uctop1:top ID="top1" runat="server" />

    <div class="meber-content clearfix">

        <div class="mem-center clearfix">
            <!-- 左侧 -->
            <%MEM.Get_Member_Left_HTML(0, 2); %>
            <!-- 右侧 -->
            <!-- 右侧 -->
             <div class="mem-center-right" >
                <!-- 添加 -->
                      <div class="mem-center-list2">
                     <div class="lsit2-tit">
                        <span>会员信息</span>
                        <ul>
                            <li onclick="window.location.href='m_info.aspx'">个人资料 </li>
                            <li  class="active"  >修改密码</li>
                        
                        </ul>

                    </div>

            
                    <form  method="post" name="frm_memeber" id="frm_memeber">
                    <div class="add-product-centre clearfix" style="border-top:1px solid #ddd;margin-top:10px;width:960px">
                        <ul style="margin-top:20px">
                            <li>
                                <span>旧密码：</span>
                                <input type="password" name="member_oldpassword" placeholder="旧密码"  /><i style="color: red">*</i>
                            </li>
                           
                            <li>
                                <span>新密码：</span>
                                <input type="password" name="member_password" placeholder="新密码" /><i style="color: red">*</i>
                            </li>
                            <li>
                                <span>重复新密码：</span>
                                <input type="password" name="member_password_confirm" placeholder="重复密码"  /><i style="color: red">*</i>
                            </li>
                            
                        </ul>
                    </div>
                    <div class="add-product-btn" style="padding-left:20px">
                        <a href="javascript:void(0);" id="summit_date" class="add-product-btn-left">保存</a>
                        <%--  <a href="#" class="add-product-btn-right">下一步</a>--%>
                    </div>
                        </form>
                </div>
            </div>
        </div>
    </div>
    <ucbottom:bottom ID="bottom1" runat="server" />

</body>
</html>
