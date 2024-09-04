//메인 탭처리
$(function () {
    history.pushState(null, null, "/Business");
});

$(window).on("load", function () {

    var offset = "";
    var _vIndex = $("input[name=Business_index]").val();

    if (_fnToNull(_vIndex) != "") {
        if (_vIndex == "1") {
            offset = $("#index1").eq(0).offset();
        }
        else if (_vIndex == "2") {
            offset = $("#index2").eq(0).offset();
        }
        else if (_vIndex == "3") {
            offset = $("#index3").eq(0).offset();
        }

        else if (_vIndex == "4") {
            offset = $("#index4").eq(0).offset();
        }

        else if (_vIndex == "5") {
            offset = $("#index5").eq(0).offset();
        }
    }

    $('html, body').animate({ scrollTop: offset.top}, 500);
});