<%@ OutputCache Duration="600" VaryByParam="none" %>

<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Register Src="Public/top.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="Public/bottom.ascx" TagName="bottom" TagPrefix="uc1" %>

<%
    Public_Class pub = new Public_Class();
    AD ad = new AD();
    Session["CurrentNav"] = "0";
    CMS cms = new CMS();
  
%>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <%= pub.SEO_TITLE()%></title>
    <meta name="author" content="<%=Application["site_name"].ToString()%>">
    <meta name="keywords" content="<%=Application["site_keyword"].ToString()%>" />
    <meta name="description" content="<%=Application["site_description"].ToString()%>" />

    <link href="/css/index.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/index.css"))%>" rel="stylesheet">
    <link href="/css/jquery.bxslider.css?v=<%=pub.GetFileMD5(Server.MapPath("/css/jquery.bxslider.css"))%>" rel="stylesheet" type="text/css">
    <script src="/js/jquery-1.9.1.js?v=<%=pub.GetFileMD5(Server.MapPath("/js/jquery-1.9.1.js"))%>"></script>
  <style type="text/css">
      .icon-list li
      {
          cursor:pointer;
      }

  </style>
</head>

<body>
    <uc1:top ID="top" runat="server" />
    <!-- 轮播图 -->
    <%=ad.AD_Show("home_switchover", "", "scroll2",0)%>
    <!-- 轮播图结束-->
    <!-- 头条开始 -->
    <div class="new-hotspots clearfix">
        <div class="new-hotspots-center">
            <div class="new-hotspots-title">
                <img src="images/icon-new.png">最新资讯
            </div>
            <%=cms.Home_TopOne(5) %>
            <span class="new-hotspots-right"><a href="/2/">更多></a></span>
        </div>

    </div>
    <div class="w-1200">
        <ul class="icon-list clearfix">
            <li onclick="location.href='/1'">
                <div class="icon-top">
                    <span></span>
                </div>
                <p>全景科协</p>
            </li>
            <li onclick="location.href='/2'">
                <div class="icon-top icon-top2">
                    <span></span>
                </div>
                <p>科协动态</p>
            </li>
            <li onclick="location.href='/3'">
                <div class="icon-top icon-top3">
                    <span></span>
                </div>
                <p>科普在线</p>
            </li>
            <li onclick="location.href='/4'">
                <div class="icon-top icon-top4">
                    <span></span>
                </div>
                <p>学会工作</p>
            </li>
            <li onclick="location.href='/5'">
                <div class="icon-top icon-top5">
                    <span></span>
                </div>
                <p>党建工作</p>
            </li>
            <li onclick="location.href='/33'">
                <div class="icon-top icon-top6">
                    <span></span>
                </div>
                <p>科技工作者之家</p>
            </li>
            <li>
                <div class="icon-top icon-top7">
                    <span></span>
                </div>
                <p>网上科技馆</p>
            </li>
            <li>
                <div class="icon-top icon-top8">
                    <span></span>
                </div>
                <p>视频会议</p>
            </li>
        </ul>
    </div>
    <!-- 头条结束 -->
    <uc1:bottom ID="bottom" runat="server" />

    <script type="text/javascript">
        //banner图
        //var _banner = $('.banner li');
        //var _num = $('.new-number span');
        //var timer = ' ';
        //var index = 0;

        //function changeBanner() {
        //    var _bannerIndex = _banner.eq(index);
        //    var _numIndex = _num.eq(index);
        //    _bannerIndex.fadeIn().siblings().fadeOut();
        //    _numIndex.addClass('cur').siblings().removeClass('cur');
        //    index = ++index % _num.size();
        //}

        //_num.on('mouseover', function () {
        //    clearInterval(timer);
        //    index = $(this).index();
        //    changeBanner();
        //    timer = setInterval(changeBanner, 8000);
        //});

        //timer = setInterval(changeBanner, 8000);
        //changeBanner();

    </script>
    <!-- 图片新闻 -->
    <script src="/js/slider.js"></script>
     <script type="text/javascript">
         $(function () {
             var bannerSlider = new Slider($('#banner_tabs'), {
                 time: 5000,
                 delay: 400,
                 event: 'hover',
                 auto: true,
                 mode: 'fade',
                 controller: $('#bannerCtrl'),
                 activeControllerCls: 'active'
             });
             $('#banner_tabs .flex-prev').click(function () {
                 bannerSlider.prev()
             });
             $('#banner_tabs .flex-next').click(function () {
                 bannerSlider.next()
             });
         })
    </script>
    <script type="text/javascript">
        //banner图
        //function slide(slideList, clickNum) {
        //    var _banner = $(slideList);
        //    var _num = $(clickNum);
        //    var timer = ' ';
        //    var index = 0;

        //    function changeBanner() {
        //        var _bannerIndex = _banner.eq(index);
        //        var _numIndex = _num.eq(index);
        //        _bannerIndex.fadeIn(1000).siblings().fadeOut();
        //        _numIndex.addClass('cur').siblings().removeClass('cur');
        //        _banner.closest('.message-banner').find('.banner-text a').eq(index).show().siblings().hide();
        //        index = ++index % _num.size();
        //    }

        //    _num.on('mouseover', function () {
        //        clearInterval(timer);
        //        index = $(this).index();
        //        changeBanner();
        //    });

        //    _num.on('mouseout', function () {
        //        index = $(this).index();
        //        changeBanner();
        //        timer = setInterval(changeBanner, 3000);
        //    });

        //    _banner.on('mouseover', function () {
        //        clearInterval(timer);
        //    });

        //    _banner.on('mouseout', function () {
        //        timer = setInterval(changeBanner, 3000);
        //    });

        //    timer = setInterval(changeBanner, 3000);
        //    changeBanner();
        //}

        function race(obj, table1, table2) {
            var table1 = document.getElementById(table1);
            var table2 = document.getElementById(table2);
            var tableBody = document.querySelector(obj);
            table2.innerHTML = table1.innerHTML;
            var speed = 50;
            var test = tableBody.scrollTop;
            tableBody.scrollTop = 0;
            function Marquee() {
                var test2 = tableBody.scrollTop;
                if (tableBody.scrollTop >= table1.offsetHeight) {
                    tableBody.scrollTop = 0;
                } else {
                    tableBody.scrollTop++;
                }
            }
            var timer = setInterval(Marquee, speed);

            tableBody.onmouseover = function () {
                window.clearInterval(timer)
            };
            tableBody.onmouseout = function () {
                timer = window.setInterval(Marquee, speed)
            };
        }

       

        $(function () {


            ////顶部轮播
            //slide('.main-banner-slide .banner li', '.main-banner-slide .number span');
            ////底部轮播
            //slide('.message-banner .banner li', '.message-banner .new-number span');


            $('.nav-list ul li').on('click', function () {
                $(this).addClass('active').siblings('li').removeClass('active');
            });
            //新闻滚动
            setInterval(function () {
                $('.new-news').stop().animate({ 'margin-top': '-46px' }, function () {
                    $('.new-news li:eq(0)').appendTo($('.new-news'));
                    $('.new-news').css({ 'margin-top': 0 });
                })
            }, 3000);

        });
    </script>
</body>
</html>
