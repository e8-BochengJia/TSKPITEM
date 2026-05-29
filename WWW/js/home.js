$(document).ready(function () {
    $('.interview-pic').bxSlider({
        slideWidth: 285,
        maxSlides: 5,
        moveSlides: 1,
        slideMargin: 20,
        auto: true
    });
});
// 二维码切换
$(function () {

    $('.code-list li').on('mouseover', function () {
        $(this).addClass('active').siblings('li').removeClass('active');
        $('.code-pic li').eq($(this).index()).show().siblings().hide();
    });
});

//banner图
var _banner = $('.banner li');
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
    timer = setInterval(changeBanner, 8000);
});

timer = setInterval(changeBanner, 8000);
changeBanner();