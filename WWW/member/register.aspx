<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
  
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    CMS cms = new CMS();
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <%= pub.SEO_TITLE()%></title>
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="keywords" content="<%=Application["site_keyword"].ToString()%>" />
    <meta name="description" content="<%=Application["site_description"].ToString()%>" />

    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index.css"))%>" rel="stylesheet">

    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>
    <script src="/layjs/layui.js" type="text/javascript"></script>
     <%
    if(DateTime.Today==tools.NullDate("2020-07-28"))
    {
        Response.Write("<style>");
        Response.Write("html{filter: grayscale(100%); -webkit-filter: grayscale(100%); -moz-filter: grayscale(100%); -ms-filter: grayscale(100%); -o-filter: grayscale(100%); filter:progid:DXImageTransform.Microsoft.BasicImage(grayscale=1);}");
        Response.Write("</style>");
    }
%>
    <style type="text/css">
        .but
        {
            display: inline-block;
            width: 400px;
            height: 46px;
            border-radius: 2px;
            background: #0062dd;
            text-align: center;
            line-height: 46px;
            color: #fff;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            border: 0px;
        }
        .cb
        {
                background-color: #1E9FFF;
        }
        .login-box ul li > input
        {
            width:360px;
        }
        .login-box ul li
        {
            margin-bottom:10px;
        }
        .login-content2-box .login-box
        {
            height:495px;
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
                    url: "/member/login_do.aspx?action=register",
                    data: $("#frm_memeber").serialize(),
                    dataType: "json",
                    success: function (data) {
                        if (data.err != "") {


                            layer.alert(data.err, { icon: 2 });


                        } else {
                            location.href = data.url;
                        }
                    }
                });
            });

        });
    </script>


</head>

<body style="background: #fff;">
    <div class="login_top">
        <div class="login_top_content">
            <div class="login_top_left">
                <img src="/images/logo1.png">
                <img src="/images/logo2.png">
                <span>欢迎注册</span>
            </div>
            <div class="login_top_right">
                <p>免费咨询电话 </p>
                <span><%=Application["Site_Tel"] %> </span>
            </div>
        </div>
    </div>
    <div class="login-content2" >
        <div class="login-content2-box">
     
            <div class="login-box">
                 
             <form  method="post" name="frm_memeber" id="frm_memeber">
                <h3>会员注册 <span>我已是会员，<a href="/member/login.aspx">立即登录</a></span></h3>
                        <ul>
                            <li>
                              
                                <input type="text" name="member_nickname" placeholder="请填写昵称" value="" /><i style="color: red">*</i>
                            </li>
                           <li>
                              
                                <input type="password" name="member_password" placeholder="新密码" /><i style="color: red">*</i>
                            </li>
                            <li>
                              
                                <input type="password" name="member_password_confirm" placeholder="重复新密码"  /><i style="color: red">*</i>
                            </li>
                            <li>
                              
                               
                                <input type="text" name="member_mobile" placeholder="请填写手机号" value="" />
                            </li>
                            <li>
                               
                                <input type="text" name="member_email" placeholder="请填写E-mail" value="" /><i style="color: red">*</i>
                            </li>
                             <%-- <li>
                              
                                <input type="text" name="U_Member_QQ" placeholder="请填写QQ" value="" />
                            </li>
                          <li>
                               
                                <input name="U_Member_Male" type="radio" id="U_Member_Male" style="width: 30px; height: auto" value="1"
                                     class="input02" />男&nbsp;&nbsp;&nbsp;&nbsp;
                               <input style="width: 30px; height: auto" name="U_Member_Male" id="U_Member_Male1" type="radio" value="0" class="input02" />女
                            </li>--%>
                          <%--  <li>
                               
                                <input type="text" id="U_MeMber_Birth" name="U_MeMber_Birth" placeholder="请填写生日" value="" />
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
                            </li>--%>
                          <%--  <li>
                             
                                <select name="U_Member_Bloodtype" style="width: 309px; height: 30px; padding-left: 5px;">
                                    <option value="A" >A</option>
                                    <option value="B" >B</option>
                                    <option value="O" >O</option>
                                    <option value="AB">AB</option>

                                </select>
                            </li>--%>
                            <li>
                             
                                <input type="text" name="U_Member_Realname" placeholder="请填写姓名" value="" /><i style="color: red">*</i>
                            </li>
                            <li>
                                
                                <input type="text" name="U_Member_IDCard" placeholder="请填写身份证号" value="" /><i style="color: red">*</i>
                            </li>
                         <%--   <li>
                             
                                <select name="U_Member_Job" style="width: 309px; height: 30px; padding-left: 5px;">

                                    <option value="财会/金融">财会/金融</option>
                                    <option value="工程师" >工程师</option>
                                    <option value="顾问" >顾问</option>
                                    <option value="计算机相关行业" >计算机相关行业</option>
                                    <option value="家庭主妇" >家庭主妇</option>
                                    <option value="教育/培训" >教育/培训</option>
                                    <option value="客户服务/支持" >客户服务/支持</option>
                                    <option value="零售商/手工工人">零售商/手工工人</option>
                                    <option value="退休">退休</option>
                                    <option value="无职业" >无职业</option>
                                    <option value="销售/市场/广告" >销售/市场/广告</option>
                                    <option value="学生" >学生</option>
                                    <option value="研究和开发" >研究和开发</option>
                                    <option value="一般管理/监督">一般管理/监督</option>
                                    <option value="政府/军队" >政府/军队</option>
                                    <option value="执行官/高级管理">执行官/高级管理</option>
                                    <option value="制造/生产/操作" >制造/生产/操作</option>
                                    <option value="专业人员" >专业人员</option>
                                    <option value="自雇/业主" >自雇/业主</option>
                                    <option value="其他" >其他</option>
                                </select>
                            </li>
                            <li>
                              
                                <select name="U_Member_Edu" style="width: 309px; height: 30px; padding-left: 5px;">
                                    <option value="小学" >小学</option>
                                    <option value="初中">初中</option>
                                    <option value="高中">高中</option>
                                    <option value="大学">大学</option>
                                    <option value="硕士">硕士</option>
                                    <option value="博士">博士</option>
                                </select>
                            </li>--%>





                        </ul>
                        
                 
                

                   <div class="forget-password clearfix">

                      
                    </div>
                  <span>
                    <div class="login-btn" style="margin: 10px 0 0 0;">

                        <input id="summit_date" style="width:270px" type="button" class="but" value="立即注册" />

                    </div>
                <input type="checkbox" name="checkbox_agreement" value="1" /><a href="/about/index.aspx?sign=register" target="_blank" style="color: #034494;">《用户注册协议》</a></span>
                        </form>
                    </div>
            
           
        </div>
    </div>


    <!-- slogen -->

    <!-- 底部 -->
    <div class="bottom-box2">
        <div class="bottom-center2 clearfix">
            <div class="bottom-left">
                <p><a href="/">首页</a> <%=cms.Bottom_About() %></p>
                <p>唐山市科学技术协会主办 Copyright © 2008-2020 唐山科普在线 版权所有 </p>
                <p>未经授权禁止复制或建立镜像 | <a href="http://beian.miit.gov.cn" target="_blank" style="font-weight: 100">冀ICP备05016301号-1 </a>| 技术支持：Glaer</p>
            <p><a href="http://www.12321.cn" target="_blank">
                <img alt="网络不良与垃圾信息举报中心" src="/images/12321.jpg"></a> | <a href="http://www.cyberpolice.cn" target="_blank" style="font-weight: 100">
                    <img alt="公安机关备案号" src="/images/cyberPoliceGif_02.gif">公安机关备案号: 13020002152308</a> | <a href="http://bszs.conac.cn/sitename?method=show&id=54A5B4CF50F423EBE053022819ACB7AB" target="_blank">
                        <img alt="" src="/images/red.png"></a></p>
            </div>
            <div class="bottom-link">
                <p>友情链接</p>
                <%=cms.Home_FriendlyLink(1) %>
            </div>

        </div>
    </div>
</body>
</html>
