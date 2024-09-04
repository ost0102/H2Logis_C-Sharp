////////////////////전역 변수//////////////////////////

////////////////////jquery event///////////////////////
//$(function () {
//
//});

////number 클릭 시 이벤트
//$(document).on("click", "input[type='number']", function () {
//	fnSetCBM();
//});

////CBM 라디오 버튼 단위 
//$(document).on("change", "input[name='cbm_unit_value']", function () {
//	fnSetCBM();
//});

//$(document).on("keyup", "#cbm_length", function () {
//	$(this).val(fnSetOnlyNum($(this).val()));

//	fnSetCBM();
//});

//$(document).on("keyup", "#cbm_width", function () {
//	$(this).val(fnSetOnlyNum($(this).val()));

//	fnSetCBM();
//});

//$(document).on("keyup", "#cbm_height", function () {
//	$(this).val(fnSetOnlyNum($(this).val()));

//	fnSetCBM();
//});

//$(document).on("keyup", "#cbm_quantity", function () {
//	fnSetCBM();
//});

////버튼 클릭 이벤트
//$(document).on("click", "#cbm_search", function () {
//	fnSetCBM();
//});

//////////////////////////function///////////////////////
////숫자를 제외한 나머지 replace
//function fnSetOnlyNum(vValue) {
//	var vValue_Num = vValue.replace(/[^0-9]/g, "");
//	return vValue_Num;
//}

//function fnSetCBM() {

//	var vCheckUnit = $("input[name='cbm_unit_value']:checked").val();
//	var vUnit_Curr = 0;

//	//단위 체크
//	$("span[name='cbm_unit_text']").text(vCheckUnit);

//	if (vCheckUnit == "mm") {
//		vUnit_Curr = 1
//	}
//	else if (vCheckUnit == "cm") {
//		vUnit_Curr = 10
//	}
//	else if (vCheckUnit == "m") {
//		vUnit_Curr = 1000
//	}

//	var vCBM_length = $("#cbm_length").val();
//	var vCBM_width = $("#cbm_width").val();
//	var vCBM_height = $("#cbm_height").val();
//	var vCBM_quantity = $("#cbm_quantity").val();

//	//가로 , 세로 , 길이 변환
//	if (_fnToNull(vCBM_width) != "") {
//		vCBM_width = Number(vCBM_width) * vUnit_Curr;
//	} else {
//		vCBM_width = 0;
//	}

//	if (_fnToNull(vCBM_height) != "") {
//		vCBM_height = Number(vCBM_height) * vUnit_Curr;
//	} else {
//		vCBM_height = 0;
//	}

//	if (_fnToNull(vCBM_length) != "") {
//		vCBM_length = Number(vCBM_length) * vUnit_Curr;
//	} else {
//		vCBM_length = 0;
//	}

//	//폭 체크
//	if (vCBM_width > 2350) {
//		alert("폭이 2미터 35센티를 넘을 경우 계산이 제한됩니다.\n(※ HC, RF, FR, OT 등 스페셜 컨테이너는 별도 확인필요)");
//		$("#cbm_width").val("").focus();
//		return false;
//	}
//	//폭체크

//	//높이 체크
//	if (vCBM_height > 2280) {
//		alert("높이는 2미터 28센티를 넘을 경우 계산이 제한됩니다.\n(※ HC, RF, FR, OT 등 스페셜 컨테이너는 별도 확인필요)");
//		$("#cbm_height").val("").focus();
//		return false;
//	}
//	//높이체크

//	var vCBM = Number(vCBM_width) * Number(vCBM_height) * Number(vCBM_length) / 1000000000
//	var vCBM_SUM = vCBM * Number(vCBM_quantity);
//	var vKG_SUM = (vCBM / 0.006) * Number(vCBM_quantity);

//	if (vCBM_width != "" && vCBM_height != "" && vCBM_length != "") {
//		$("#cbm_result").val(vCBM_SUM.toFixed(3));
//		$("#cbm_air_result").val(vKG_SUM.toFixed(2));

//		//var v20FT = (33 / Number(vCBM.toFixed(3))).toFixed(0);

//		//$("#cbm_20FT_result").val(fnSetComma((33 / Number(vCBM.toFixed(3))).toFixed(0))); //FT20
//		//$("#cbm_40FT_result").val(fnSetComma((66 / Number(vCBM.toFixed(3))).toFixed(0))); //FT40
//		//$("#cbm_40HC_result").val(fnSetComma((77 / Number(vCBM.toFixed(3))).toFixed(0))); //HQ40

//		//무한대 값 나올 경우 빈값
//		if ($("#cbm_result").val() == "Infinity") {
//			$("#cbm_result").val("");
//		}

//		if ($("#cbm_air_result").val() == "Infinity") {
//			$("#cbm_air_result").val("");
//		}

//		//if ($("#cbm_20FT_result").val() == "Infinity") {
//		//	$("#cbm_20FT_result").val("");
//		//}
//		//
//		//if ($("#cbm_40FT_result").val() == "Infinity") {
//		//	$("#cbm_40FT_result").val("");
//		//}
//		//
//		//if ($("#cbm_40HC_result").val() == "Infinity") {
//		//	$("#cbm_40HC_result").val("");
//		//}

//	}
//	else {
//		$("#cbm_result").val("");
//		$("#cbm_air_result").val("");
//		//$("#cbm_20FT_result").val("");
//		//$("#cbm_40FT_result").val("");
//		//$("#cbm_40HC_result").val("");
//	}
//}

//$('input').keyup(function (e) {

//	if (e.keyCode == 13) {//키가 13이면 실행 (엔터는 13)
//		//로지스틱스 툴 - CBM
//		if ($(e.target).attr('data-index').indexOf("LogisticsCBM") > -1) {
//			//indexOf 데이터를 지우고 +1
//			var vIndex = $(e.target).attr('data-index').replace("LogisticsCBM", "");
//			$('[data-index="LogisticsCBM' + (parseFloat(vIndex) + 1).toString() + '"]').focus();
//		}

//		//스케줄 관리 - 선사 스케줄 엔터 키 이벤트
//		if ($(e.target).attr('data-index').indexOf("Schedule_SEA") > -1) {
//			//indexOf 데이터를 지우고 +1
//			var vIndex = $(e.target).attr('data-index').replace("Schedule_SEA", "");

//			if (vIndex == 1) {
//				$('[data-index="Schedule_SEA' + (parseFloat(vIndex) + 1).toString() + '"]').focus();
//			}
//			else if (vIndex == 2) {
//				$("#btn_SEASchdule_Search").click();
//			}
//		}
//	}
//});

/////////////////function MakeList/////////////////////

////////////////////////API////////////////////////////

var cntCal = 0;
$("#btn_calc_cbm").click(function () {
    fnSetCBM();
});

////////////////////////function///////////////////////
//숫자를 제외한 나머지 replace
function fnSetOnlyNum(vValue) {
    var vValue_Num = vValue.replace(/[^0-9]/g, "");
    return vValue_Num;
}
function fnSetCBM() {


    var cbm_vwgt = 0;
    var cbm_cwgt = 0;
    var cbm_calcul = 0;
    //단위 체크
    vCheckUnit = $("#CBM_UNIT option:selected").val();


    var vCBM_length = $("#cbm_length").val().replace(/,/gi, '');
    var vCBM_width = $("#cbm_width").val().replace(/,/gi, '');
    var vCBM_height = $("#cbm_height").val().replace(/,/gi, '');
    var vCBM_grs_wgt = $("#cbm_grs_wgt").val().replace(/,/gi, '');
    if (_fnToNull(vCBM_length) == "") {
        _fnAlertMsg("가로를 입력해주세요");
        enable();
        return false;
    }
    if (_fnToNull(vCBM_width) == "") {
        _fnAlertMsg("세로를 입력해주세요");
        enable();
        return false;
    }
    if (_fnToNull(vCBM_height) == "") {
        _fnAlertMsg("높이를 입력해주세요");
        enable();
        return false;
    }
    if (_fnToNull(vCheckUnit) == "") {
        _fnAlertMsg("단위를 선택해주세요");
        enable();
        return false;
    }
    if (_fnToNull(vCBM_grs_wgt) == "") {
        _fnAlertMsg("무게를 입력해주세요");
        enable();
        return false;
    }

    if (vCheckUnit == "MM") {
        cbm_vwgt = (vCBM_length * vCBM_width * vCBM_height) / 6000000;
        cbm_calcul = (vCBM_length * vCBM_width * vCBM_height) / 1000000000;
    }
    else if (vCheckUnit == "CM") {
        cbm_vwgt = (vCBM_length * vCBM_width * vCBM_height) / 6000;
        cbm_calcul = (vCBM_length * vCBM_width * vCBM_height) / 1000000;
    }
    else if (vCheckUnit == "M") {
        cbm_vwgt = (vCBM_length * vCBM_width * vCBM_height) / 6 * 1000;
        cbm_calcul = (vCBM_length * vCBM_width * vCBM_height);

    } else if (vCheckUnit == "INCH") {
        cbm_vwgt = (vCBM_length / 39.37) * (vCBM_width / 39.37) * (vCBM_height / 39.37) / 6000 * 1000000;
        cbm_calcul = (vCBM_length / 39.37) * (vCBM_width / 39.37) * (vCBM_height / 39.37);
    }

    if (vCBM_grs_wgt >= cbm_vwgt) {
        cbm_cwgt = vCBM_grs_wgt;
    } else {
        cbm_cwgt = cbm_vwgt;
    }

    cbm_vwgt = cbm_vwgt * 100
    var vcbm_vwgt = Math.ceil(cbm_vwgt);
    var result1 = vcbm_vwgt / 100;
    result1 = result1.toFixed(3);


    cbm_cwgt = cbm_cwgt * 100
    var vcbm_cwgt = Math.floor(cbm_cwgt);
    var result2 = vcbm_cwgt / 100;
    result2 = result2.toFixed(3);


    cbm_calcul = cbm_calcul * 1000
    var vcbm_cacul = Math.floor(cbm_calcul);
    var result3 = vcbm_cacul / 1000;
    result3 = result3.toFixed(3);

    cntCal += 1;

    var apdVal = "";

    apdVal += " <div class='cbm_list'>	 ";
    apdVal += "               <div class='cbm_desc'> ";
    apdVal += "                 <div class='cbm_info_box'>";
    apdVal += "                   <div class='cbm_info'> ";
    apdVal += "                       <div class='cbm_unit'>가로 : <span>" + $("#cbm_length").val() + " </span></div> ";
    apdVal += "                       <div class='cbm_unit'>세로 : <span>" + $("#cbm_width").val() + " </span></div> ";
    apdVal += "                       <div class='cbm_unit'>높이 : <span>" + $("#cbm_height").val() + " </span></div> ";
    apdVal += "                       <div class='cbm_unit'>단위 : <span>" + $("#CBM_UNIT option:selected").text() + " </span></div> ";
    apdVal += "                       <div class='cbm_unit'>무게 : <span>" + $("#cbm_grs_wgt").val() + " </span></div> ";
    apdVal += "                   </div> ";
    apdVal += "                   <button type='button' class='btn_delete'>삭제<img src='/Images/icn_sm_delete.png'/></button> ";
    apdVal += "                 </div> ";
    apdVal += "                 <div class='cbm_detail'> ";
    apdVal += "                   <div class='cbm_cal'>";
    apdVal += "                       <span class='cbm_h'>V/WT(kg)</span>";
    apdVal += "                       <span class='rst_vol'>" + result1 + " <span class='cbm-unit'>kg</span></span>";
    apdVal += "                   </div>";
    apdVal += "                   <div class='cbm_cal'>";
    apdVal += "                       <span class='cbm_h'>C/WT(kg)</span >";
    apdVal += "                       <span class='rst_chr'>" + result2 + " <span class='cbm-unit'>kg</span></span>";
    apdVal += "                   </div>";
    apdVal += "                   <div class='cbm_cal'>";
    apdVal += "                       <span class='cbm_h'>CBM</span>";
    apdVal += "                       <span class='rst_cbm'>" + result3 + " <span class='cbm-unit'>CBM</span></span>";
    apdVal += "                   </div>";
    apdVal += "                   <div class='cbm_cal'>";
    apdVal += "                       <span class='cbm_h'>R/T(운임톤)</span>";
    apdVal += "                       <span class='rst_rton'>" + result3 + " <span class='cbm-unit'>CBM</span></span>";
    apdVal += "                   </div>";
    apdVal += "                 </div> ";
    apdVal += "               </div> ";
    apdVal += "           </div>";

    $(".scrollbar_cbm").append(apdVal);

    $(".one > input").val("");
    $(".one > .delete").hide();
    $('#CBM_UNIT').val('').prop("selected", true);

    var tot_vWgt = 0;
    var tot_cWgt = 0;
    var tot_cbm = 0;
    var tot_rton = 0;
    $(".rst_vol").each(function (i) {
        tot_vWgt += parseFloat($(this).text());

    });

    $("#tot_vWgt").text(tot_vWgt.toFixed(3));

    $(".rst_chr").each(function (i) {
        tot_cWgt += parseFloat($(this).text());

    });

    $(".rst_cbm").each(function (i) {
        tot_cbm += parseFloat($(this).text());

    });

    $(".rst_rton").each(function (i) {
        tot_rton += parseFloat($(this).text());

    });
    $("#tot_cWgt").text(tot_cWgt.toFixed(3));
    $("#tot_cbm").text(tot_cbm.toFixed(3));
    $("#tot_rton").text(tot_rton.toFixed(3));

    $("#result_cbm").show();
}

$(document).on("click", "#alert01_confirm, #alert_close", function () {
    disable();
})

$(document).on("click", ".btn_delete", function () {
    $(this).closest(".cbm_list").remove();
    var tot_vWgt = 0;
    var tot_cWgt = 0;
    var tot_cbm = 0;
    var tot_rton = 0;
    $(".rst_vol").each(function (i) {
        tot_vWgt += parseFloat($(this).text());

    });

    $("#tot_vWgt").text(tot_vWgt.toFixed(3));

    $(".rst_chr").each(function (i) {
        tot_cWgt += parseFloat($(this).text());

    });

    $(".rst_cbm").each(function (i) {
        tot_cbm += parseFloat($(this).text());

    });

    $(".rst_rton").each(function (i) {
        tot_rton += parseFloat($(this).text());

    });
    $("#tot_cWgt").text(tot_cWgt.toFixed(3));
    $("#tot_cbm").text(tot_cbm.toFixed(3));
    $("#tot_rton").text(tot_rton.toFixed(3));

    $("#result_cbm").show();
});
$(document).on("focusin", ".wgt", function () {
    if ($(this).val() != "") {
        _fnUncomma(this, "");
    }
});
//콤마 풀기
function _fnUncomma(str, val) {
    if (val == "val") {
        var num = str.val();
    } else {
        var num = str.value;
    }
    num = num.replace(/,/g, '');
    str.value = num;
}

$(document).on("keyup", ".wgt", function (key) {

    if (key.keyCode != 8 && key.keyCode != 46) {
        //
        var _pattern2 = /^\d*[.]\d{4}$/; // 현재 value값이 소수점 셋째짜리 숫자이면 더이상 입력 불가
        if (_pattern2.test($(this).val().replace(/,/gi, ''))) {
            $(this).val($(this).val().substr(0, $(this).val().length - 1));
        }
        _fnGetNumber(this, "");
    }
});
function isNumberKey(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    var _value = event.srcElement.value;
    // 소수점(.)이 두번 이상 나오지 못하게

    var _pattern0 = /^\d*[.]\d*$/; // 현재 value값에 소수점(.) 이 있으면 . 입력불가

    if (_pattern0.test(_value)) {
        if (charCode == 46) {
            return false;
        }
    }

    return true;
}

$(document).on("keyup", "input", function (e) {
    if (e.which == 13) {
        var tr = $(this).closest('div');
        if (tr.length > 0) {
            var $this = $(e.target);
            var td = tr[0].className;
            td = td.split(" ")[0];
            var index = parseFloat($this.attr('data-index'));
            $('.' + td + ' [data-index="' + (index + 1).toString() + '"]').focus();
        } else {
            var $this = $(e.target);
            var index = parseFloat($this.attr('data-index'));
            $('[data-index="' + (index + 1).toString() + '"]').focus();
        }
    }
});

function _fnGetNumber(obj, sum) {
    var num01;
    var num02;
    if (sum == "sum") {
        num02 = obj;
        num01 = fnSetComma(num02); //콤마 찍기
        return num01;
    }
    else {
        num01 = obj.value.slice(0, 13);
        num02 = num01.replace(/[^0-9.]/g, ""); //0으로 시작하거나 숫자가 아닌것을 제거,
        num01 = fnSetComma(num02); //콤마 찍기
        obj.value = num01;
    }

}

/////////////////function MakeList/////////////////////

////////////////////////API////////////////////////////

$(document).on("click", "#btn_clear_cbm", function () {
    $("#result_cbm").css("display", "none");
    $(".scrollbar_cbm").empty();
    $("#tot_vWgt").text("");
    $("#tot_cWgt").text("");
    $("#tot_cbm").text("");
    $("#tot_rton").text("");
    $("#cbm_length").val("");
    $("#cbm_width").val("");
    $("#cbm_height").val("");
    $("#cbm_grs_wgt").val("");
    $(".one > .delete").hide();
    $('#CBM_UNIT').val('').prop("selected", true);
    cntCal = 0;
});


$(document).on('click', '.cntr_status.air.off', function () {
    $('.cntr_ship').hide();
    $('.cntr_air').show();
    $('#show_air').hide();
    $('#hide_air').show();
    $('#show_air_mo').hide();
    $('#hide_air_mo').show();
    $('.cntr_status.sea').removeClass('on');
    $('.cntr_status.sea').addClass('off');
    $('.cntr_status.air').addClass('on');
    $('.cntr_status.air').removeClass('off');
    if ($('.cntr_ship').css("display") == 'none') {
        $('#hide_ship').hide();
        $('#show_ship').show();
        $('#hide_ship_mo').hide();
        $('#show_ship_mo').show();
    }
})
$(document).on('click', '.cntr_status.air.on', function () {
    $('.cntr_air').hide();
    $('#hide_air').hide();
    $('#show_air').show();
    $('#hide_air_mo').hide();
    $('#show_air_mo').show();
    if ($('.cntr_ship').css('display') == 'none') {
        $('.cntr_status.air').removeClass('on');
        $('.cntr_status.air').addClass('off');
    }
})

$(document).on('click', '.cntr_status.sea.off', function () {
    $('.cntr_air').hide();
    $('.cntr_ship').show();
    $('#show_ship').hide();
    $('#hide_ship').show();
    $('#show_ship_mo').hide();
    $('#hide_ship_mo').show();
    $('.cntr_status.air').removeClass('on');
    $('.cntr_status.air').addClass('off');
    $('.cntr_status.sea').addClass('on');
    $('.cntr_status.sea').removeClass('off');
    if ($('.cntr_air').css("display") == 'none') {
        $('#hide_air').hide();
        $('#show_air').show();
        $('#hide_air_mo').hide();
        $('#show_air_mo').show();
    }
})
$(document).on('click', '.cntr_status.sea.on', function () {
    $('.cntr_ship').hide();
    $('#hide_ship').hide();
    $('#show_ship').show();
    $('#hide_ship_mo').hide();
    $('#show_ship_mo').show();
    if ($('.cntr_air').css('display') == 'none') {
        $('.cntr_status.sea').removeClass('on');
        $('.cntr_status.sea').addClass('off');
    }
})

// 팝업 스크롤
const body = document.querySelector('body');
let scrollPosition = 0;

// 팝업 오픈
function enable() {
    scrollPosition = window.pageYOffset;
    body.style.overflow = 'scroll';
    body.style.position = 'fixed';
    body.style.top = `-${scrollPosition}px`;
    body.style.width = '100vw';
}
// 팝업 닫기
function disable() {
    body.style.removeProperty('overflow');
    body.style.removeProperty('position');
    body.style.removeProperty('top');
    body.style.removeProperty('width');
    window.scrollTo(0, scrollPosition);
}
//스크롤