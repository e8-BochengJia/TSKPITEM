<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/topm.ascx" TagPrefix="uctop1" TagName="top" %>
<%@ Register Src="~/Public/Bottom.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Session["Cur_Position"] = "info";
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
            width:105px;
        }
        .add-product-btn-left
        {
        margin:0 30px 0 110px;
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
                    url: "/member/member_do.aspx?action=updatemember",
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
            <%MEM.Get_Member_Left_HTML(0, 1); %>
            <!-- 右侧 -->
            <!-- 右侧 -->
            <div class="mem-center-right" >
                <!-- 添加 -->
      
                 <div class="mem-center-list2">
                     <div class="lsit2-tit">
                        <span>会员信息</span>
                        <ul>
                            <li class="active">个人资料 </li>
                            <li onclick="window.location.href='m_password.aspx'" >修改密码</li>
                        
                        </ul>

                    </div>


         
                    <form  method="post" name="frm_memeber" id="frm_memeber">
                    <div class="add-product-centre clearfix" style="border-top:1px solid #ddd;margin-top:10px;width:960px">
                        <ul style="margin-top:20px;">
                            <li>
                                <span>用户名：</span>
                                <input type="text" name="Member_NickName" placeholder="请填写用户名" value="<%=memberinfo.Member_NickName %>" /><i style="color: red">*</i>
                            </li>
                            <li>
                                <span>手机号：</span>
                                <%=memberinfo.Member_LoginMobile==""?"暂无": memberinfo.Member_LoginMobile%>
                            </li>
                            <li>
                                <span>E-mail：</span>
                                <input type="text" name="Member_Email" placeholder="请填写E-mail" value="<%=memberinfo.Member_Email%>" /><i style="color: red">*</i>
                            </li>
                            <li>
                                <span>QQ：</span>
                                <input type="text" name="U_Member_QQ" placeholder="请填写QQ" value="<%=memberinfo.U_Member_QQ%>" />
                            </li>
                             <li>
                                <span>找回密码问题：</span>
                           

                                  <input type="text" name="U_Member_Question" placeholder="请填写问题" value="<%=memberinfo.U_Member_Question%>" /><i style="color: red">*</i>
                            </li>
                             <li>
                                <span>找回密码答案：</span>
                                <input type="text" name="U_Member_Answer" placeholder="请填写答案" value="<%=memberinfo.U_Member_Answer%>" /><i style="color: red">*</i>
                            </li>
                            <li>
                                <span>性别：</span>
                                <input name="U_Member_Male" type="radio" id="U_Member_Male" style="width: 30px; height: auto" value="1"
                                    <%=pub.CheckRadio(memberinfo.U_Member_Male.ToString(),"1") %> class="input02" />男&nbsp;&nbsp;&nbsp;&nbsp;
                               <input style="width: 30px; height: auto" name="U_Member_Male" id="U_Member_Male1" type="radio" value="0" <%=pub.CheckRadio(memberinfo.U_Member_Male.ToString(),"0") %> class="input02" />女
                            </li>
                            <li>
                                <span>生日：</span>
                                <input type="text" id="U_MeMber_Birth" name="U_MeMber_Birth" placeholder="请填写生日" value="<%=(memberinfo.U_MeMber_Birth.ToString("yyyy-MM-dd"))%>" />
                                <script>
                                    layui.use('laydate', function () {
                                        var laydate = layui.laydate;

                                        //执行一个laydate实例
                                        laydate.render({
                                            elem: '#U_MeMber_Birth' //指定元素
                                            , type: 'date'

                                        });
                                    });
                                </script>
                            </li>
                            <li>
                                <span>血型：</span>
                                <select name="U_Member_Bloodtype" style="width: 309px; height: 30px; padding-left: 5px;">
                                    <option value="A" <%=pub.CheckSelect(memberinfo.U_Member_Bloodtype.ToString(),"A") %>>A</option>
                                    <option value="B" <%=pub.CheckSelect(memberinfo.U_Member_Bloodtype.ToString(),"B") %>>B</option>
                                    <option value="O" <%=pub.CheckSelect(memberinfo.U_Member_Bloodtype.ToString(),"O") %>>O</option>
                                    <option value="AB" <%=pub.CheckSelect(memberinfo.U_Member_Bloodtype.ToString(),"AB") %>>AB</option>

                                </select>
                            </li>
                            <li>
                                <span>姓名：</span>
                                <input type="text" name="U_Member_Realname" placeholder="请填写姓名" value="<%=memberinfo.U_Member_Realname%>" /><i style="color: red">*</i>
                            </li>
                            <li>
                                <span>身份证号：</span>
                                <input type="text" name="U_Member_IDCard" placeholder="请填写身份证号" value="<%=memberinfo.U_Member_IDCard%>" /><i style="color: red">*</i>
                            </li>
                            <li>
                                <span>职业：</span>
                                <select name="U_Member_Job" style="width: 309px; height: 30px; padding-left: 5px;">

                                    <option value="财会/金融">财会/金融</option>
                                    <option value="工程师" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"工程师") %>>工程师</option>
                                    <option value="顾问" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"顾问") %>>顾问</option>
                                    <option value="计算机相关行业" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"计算机相关行业") %>>计算机相关行业</option>
                                    <option value="家庭主妇" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"家庭主妇") %>>家庭主妇</option>
                                    <option value="教育/培训" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"教育/培训") %>>教育/培训</option>
                                    <option value="客户服务/支持" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"客户服务/支持") %>>客户服务/支持</option>
                                    <option value="零售商/手工工人" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"零售商/手工工人") %>>零售商/手工工人</option>
                                    <option value="退休" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"退休") %>>退休</option>
                                    <option value="无职业" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"无职业") %>>无职业</option>
                                    <option value="销售/市场/广告" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"销售/市场/广告") %>>销售/市场/广告</option>
                                    <option value="学生" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"学生") %>>学生</option>
                                    <option value="研究和开发" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"研究和开发") %>>研究和开发</option>
                                    <option value="一般管理/监督" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"一般管理/监督") %>>一般管理/监督</option>
                                    <option value="政府/军队" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"政府/军队") %>>政府/军队</option>
                                    <option value="执行官/高级管理" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"执行官/高级管理") %>>执行官/高级管理</option>
                                    <option value="制造/生产/操作" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"制造/生产/操作") %>>制造/生产/操作</option>
                                    <option value="专业人员" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"专业人员") %>>专业人员</option>
                                    <option value="自雇/业主" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"自雇/业主") %>>自雇/业主</option>
                                    <option value="其他" <%=pub.CheckSelect(memberinfo.U_Member_Job.ToString(),"其他") %>>其他</option>
                                </select>
                            </li>
                            <li>
                                <span>学历：</span>
                                <select name="U_Member_Edu" style="width: 309px; height: 30px; padding-left: 5px;">
                                    <option value="小学" <%=pub.CheckSelect(memberinfo.U_Member_Edu.ToString(),"小学") %>>小学</option>
                                    <option value="初中" <%=pub.CheckSelect(memberinfo.U_Member_Edu.ToString(),"初中") %>>初中</option>
                                    <option value="高中" <%=pub.CheckSelect(memberinfo.U_Member_Edu.ToString(),"高中") %>>高中</option>
                                    <option value="大学" <%=pub.CheckSelect(memberinfo.U_Member_Edu.ToString(),"大学") %>>大学</option>
                                    <option value="硕士" <%=pub.CheckSelect(memberinfo.U_Member_Edu.ToString(),"硕士") %>>硕士</option>
                                    <option value="博士" <%=pub.CheckSelect(memberinfo.U_Member_Edu.ToString(),"博士") %>>博士</option>
                                </select>
                            </li>





                        </ul>
                        
                    </div>
                    <div class="add-product-btn">
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
