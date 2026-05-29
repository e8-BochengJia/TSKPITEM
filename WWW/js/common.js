var page = 1;
function Recommend(count) {
    page++;
    $("#RecommendList").html("<div style=\"text-align:center; margin-top:10px;\"><img src=\"/images/loading-1.gif\"  /></div>");

    setTimeout(function () {


        $.ajax({
            type: "get",
            global: false,
            async: false,
            dataType: "html",
            url: encodeURI("/article/articcle_do.aspx?action=RecommendList&page=" + page + "&PageSize=" + count + "&timer=" + Math.random()),
            success: function (data) {
                if (data == "Error") {
                    page = 0;
                    $("#RecommendList").html("<div style=\"text-align:center; margin-top:10px;\">已经到头了</div>");
                }
                else {
                    $("#RecommendList").html(data);
                }

            },
            error: function () {
                alert("请求错误，请稍后重试");
            }
        });

    },500)
    
}



