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
     <script src="/Public/u/ueditor.config.js?v=1" type="text/javascript"></script>
    <script src="/Public/u/ueditor.all.min.js" type="text/javascript"></script>
    <script src="/Public/u/lang/zh-cn/zh-cn.js" type="text/javascript"></script>
    <style type="text/css">
         .mem-center-list2 .lsit2-tit ul li.active
        {
            color: black;
            border-bottom: 0px solid #338fff;
        }
        #edui1_iframeholder
        {
            min-height:250px!important;
        }
    </style>
    <script type="text/javascript">
        layui.use('layer', function () {
            var layer = layui.layer;
        });
      
        function editoradd(value, id) {
            UE.getEditor('Article_Content').execCommand('insertHtml', value)

        }
        $(function () {

            $("#summit_date").click(function () {

                $.ajax({
                    type: "GET",
                    url: "/member/member_do.aspx?action=addarticle",
                    data: $("#frm_memeber").serialize(),
                    dataType: "json",
                    success: function (data) {
                        if (data.err != "") {
                    
                            layer.alert(data.err, { icon: 2 });
                        } else {
                            layer.alert("操作成功，待管理员审核后，您的文章将会显示！", function () { location.href = data.url; });
                        }
                    }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                        layer.alert(textStatus);

                    }
                });
            });
        });
        function change(v)
        {
        
            if (v == 57)
            {
                
                $("#title_c").text("文章标题：");
                $("#a_content").text("内容：");
                $("#photo").css("display", "none");
                $("#video").css("display", "none");
            }
            if (v == 55 || v == 54)
            {
              
                $("#title_c").text("图片名称：");
                $("#a_content").text("图片介绍：");
                $("#photo").css("display", "");
                $("#video").css("display", "none");
                
            }
            if (v == 56)
            {
                $("#title_c").text("视频名称：");
                $("#a_content").text("视频介绍：");
                $("#photo").css("display", "none"); 
                $("#video").css("display", "");
            }
        }
    </script>


</head>
<body style="background: #f5f5f9;">

    <uctop1:top ID="top1" runat="server" />

    <div class="meber-content clearfix">

        <div class="mem-center clearfix">
            <!-- 左侧 -->
            <%MEM.Get_Member_Left_HTML(0, 4); %>
            <!-- 右侧 -->
            <!-- 右侧 -->
            <div class="mem-center-right">
                <!-- 添加 -->
                <div class="mem-center-list2">
                     <div class="lsit2-tit">
                        <span>我的投稿</span>
                        <ul>
                           
                          <li style="font-size:14px;cursor: initial;color:red"> 每次成功发表并审核通过，奖励200积分</li>
                        
                        </ul>

                    </div>
                    <form  method="post" name="frm_memeber" id="frm_memeber">
                    <div class="add-product-centre clearfix" style="border-top:1px solid #ddd;margin-top:10px;width:960px">
                        <ul style="margin-top:20px;">
                               <li>
                                <span>原创类型：</span>
                                <select name="article_cateid" style="width: 309px; height: 30px; padding-left: 5px;" onchange="change(this.options[this.options.selectedIndex].value)">
                                    <option value="57" >科普图文</option>
                                    <option value="55" >科普摄影</option>
                                    <option value="54" >科幻绘画</option>
                                    <option value="56">科普视频动漫</option>

                                </select>
                            </li>
                            <li>
                                <span id="title_c">文章标题：</span>
                                <input type="text" name="title"  value="" /><i style="color: red">*</i>
                            </li>
                               <li>
                                <span id="Span1">内容摘要：</span>
                                  <textarea name="Article_Intro" id="Article_Intro" style="width:307px;" rows="3" ></textarea><i style="color: red">*</i>
                            </li>
                            <li>
                                <span>作者：</span>
                               <input type="text" name="name"  value="" /><i style="color: red">*</i>
                            </li>
                                                      <li id="photo" style="display:none;" class="clearfix">
                                <span style="float:left;">上传图片：</span>
                                                         
                                                          <span style="width:300px;float:left;">
                                  <iframe id="formadd" src="<% =Application["Upload_Server_URL"]%>/public/FileUpload.aspx?App=content&formname=frm_memeber&frmelement=Article_Content&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>"
                                        width="100%" height="25" frameborder="0" scrolling="no"></iframe></span><i style="color: red">*</i>
                                                           <input type="hidden" name="img_Article_Content" id="img_Article_Content" />
                            </li>
                             <li id="video" style="display:none;" class="clearfix">
                                <span  style="float:left;">上传视频：</span>
                              <span style="width:300px;float:left;">
                                  <iframe id="Iframe1" src="<% =Application["Upload_Server_URL"]%>/public/FileUpload.aspx?App=video&formname=frm_memeber&frmelement=Article_Content&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>"
                                        width="100%" height="25" frameborder="0" scrolling="no"></iframe></span><i style="color: red">*</i>
                            </li>
                            <li class="clearfix" >
                                <span id="a_content" style="float:left;">内容：</span>
                                <span style="float:left;width:800px;">
                                <textarea name="Article_Content" id="Article_Content" cols="50" style="width:100%;"></textarea>
                      <script type="text/javascript">
                          var ue = UE.getEditor('Article_Content', {
                              allowDivTransToP: false
                          });
                                    </script>
                                    </span><i style="color: red">*</i>
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
