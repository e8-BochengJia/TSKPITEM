<%@ Page Language="C#" %>

<%@ Register Src="~/Public/topcate.ascx" TagName="topcate" TagPrefix="uc1" %>
<%@ Register Src="~/Public/bottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();

    string SEO_Keywords = tools.NullStr(Application["Site_Keyword"]);
    string SEO_Description = tools.NullStr(Application["Site_Description"]);
    string SEO_Title = pub.SEO_TITLE();
    int Article_ID = 0;
    Article_ID = tools.CheckInt(Request["cate_id"]);
    int secondid = 0;
    string Englishname = "";
    int parentID = 0;
    ArticleCateInfo articlecate = cms.GetArticleCateByID(Article_ID);
    if (articlecate != null)
    {
        if (articlecate.Article_Cate_ParentID != 0)
        {
            parentID = cms.GetParentCate_ID(Article_ID);
            if (parentID != 0)
            {
                articlecate = cms.GetArticleCateByID(parentID);
            }
            else
            {
                Response.Redirect("/404.aspx");
            }
        }
        else {
            parentID = Article_ID;
        }
        secondid = cms.GetParentSecondCate_ID(Article_ID);
        SEO_Title = articlecate.Article_Cate_Name + " - " + SEO_Title;
        if (articlecate.Article_Cate_ID == 3)
        {
            Response.Redirect("/Voice/3/");
        }
        //if (articlecate.Article_Cate_ID == 53)
        //{
        //    Response.Redirect("/online/53/");
        //}
        if (articlecate.Article_Cate_Name == "全景科协")
        {
            Englishname = "About Us";
        }
        if (articlecate.Article_Cate_Name == "科技工作之家")
        {
            Englishname = "Workers'Home";
        }
        if (articlecate.Article_Cate_Name == "科协动态")
        {
            Englishname = "Dynamic";
        }
        if (articlecate.Article_Cate_Name == "科普你我共参与" || articlecate.Article_Cate_ID == 53)
        {
            Englishname = "Popular science participation";
       
        }


        //if (articlecate.Article_Cate_ID == 12)
        //{
        //    Response.Redirect("/personnel/12/");
        //}
    }
    else
    {
        Response.Redirect("/404.aspx");
    }
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
        <img src="/images/icon-nav.png">当前位置 > <a href="/">首页 </a><%=cms.GetArticleInfo_Cate_Nav(Article_ID, " &gt; ")%></p>
    <div class="w-1200 clearfix" style="margin-bottom: 30px;">
        <!-- 左侧 -->
        <div class="kx-state-left">
            <div class="ky-tit">
                <span><%=Englishname %></span>
                <p><%=articlecate.Article_Cate_Name %></p>

            </div>
            <%=cms.GetArticle_CateLeft(parentID,secondid) %>
        </div>
     
       
            <%cms.GetArticle_ListRight(Article_ID); %>
         
        

    </div>
  <script type="text/javascript">
      //banner图
      var _banner = $('.banner2 li');
      var _num = $('.new-number span');
      var timer = ' ';
      var index = 0;

      function changeBanner() {
          var _bannerIndex = _banner.eq(index);
          var _numIndex = _num.eq(index);
          _bannerIndex.fadeIn().siblings().fadeOut();
          _numIndex.addClass('cur').siblings().removeClass('cur');
          index = ++index % _num.size();
      }

      _num.on('mouseover', function () {
          clearInterval(timer);
          index = $(this).index();
          changeBanner();
          timer = setInterval(changeBanner, 3000);
      });

      timer = setInterval(changeBanner, 3000);
      changeBanner();

</script>
    <uc1:bottom ID="bottom" runat="server" />

</body>
</html>
