﻿////////////////////전역 변수//////////////////////////
var _SEAObjCheck = new Object();
var _SEA_OrderBy = "";
var _SEA_Sort = "";

////////////////////jquery event///////////////////////
//$(function () {
//	_fnAlertMsg("되냐");
//});
window.onpageshow = function(event) {
	if (event.persisted || (window.performance && window.performance.navigation.type == 2) || event.originalEvent && event.originalEvent.persisted) {
		if (_fnToNull(sessionStorage.getItem("ETD")) != "") {
			if (_fnToNull(sessionStorage.getItem("LINE_TYPE")) == "SEA" && _fnToNull(sessionStorage.getItem("BEFORE_VIEW_NAME")) == "MAIN_SEA") {
				$(".panel").hide();
				$(".tab li[value=SEA]").addClass('on');
				$("#select_SEA_CntrType").val(_fnToNull(sessionStorage.getItem("CNTR_TYPE"))).prop('checked', true);
				$("#input_SEA_ETD").val(_fnToNull(sessionStorage.getItem("ETD"))); //ETD
				$("#input_SEA_Departure").val(_fnToNull(sessionStorage.getItem("POL_NM"))); //POL_NM
				$("#input_SEA_POL").val(_fnToNull(sessionStorage.getItem("POL_CD"))); //POL_CD
				$("#input_SEA_Arrival").val(_fnToNull(sessionStorage.getItem("POD_NM"))); //POD_NM
				$("#input_SEA_POD").val(_fnToNull(sessionStorage.getItem("POD_CD"))); //POD_CD
				sessionStorage.clear();
				$("#seaSchedule").show();
				fnGetSEAScheduleData();
			} else if (_fnToNull(sessionStorage.getItem("LINE_TYPE")) == "AIR" && _fnToNull(sessionStorage.getItem("BEFORE_VIEW_NAME")) == "MAIN_AIR") {
				$(".panel").hide();
				$(".tab li").removeClass('on');
				$(".tab li[value=AIR]").addClass('on');
				$("#select_AIR_CntrType").val(_fnToNull(sessionStorage.getItem("CNTR_TYPE"))).prop('checked', true);
				$("#input_AIR_ETD").val(_fnToNull(sessionStorage.getItem("ETD"))); //ETD
				$("#input_AIR_Departure").val(_fnToNull(sessionStorage.getItem("POL_NM"))); //
				$("#input_AIR_POL").val(_fnToNull(sessionStorage.getItem("POL_CD"))); //POL_CD
				$("#input_AIR_Arrival").val(_fnToNull(sessionStorage.getItem("POD_NM"))); //POD_NM
				$("#input_AIR_POD").val(_fnToNull(sessionStorage.getItem("POD_CD"))); //POD_CD
				sessionStorage.clear();
				$("#airSchedule").show();
				fnGetAIRScheduleData();
			} else if (_fnToNull(sessionStorage.getItem("LINE_TYPE")) == "FERRY" && _fnToNull(sessionStorage.getItem("BEFORE_VIEW_NAME")) == "MAIN_FERRY") {
				$(".panel").hide();
				$(".tab li").removeClass('on');
				$(".tab li[value=FERRY]").addClass('on');
				$("#select_FERRY_CntrType").val(_fnToNull(sessionStorage.getItem("CNTR_TYPE"))).prop('checked', true);
				$("#input_FERRY_ETD").val(_fnToNull(sessionStorage.getItem("ETD"))); //ETD
				$("#input_FERRY_Departure").val(_fnToNull(sessionStorage.getItem("POL_NM"))); //
				$("#input_FERRY_POL").val(_fnToNull(sessionStorage.getItem("POL_CD"))); //POL_CD
				$("#input_FERRY_Arrival").val(_fnToNull(sessionStorage.getItem("POD_NM"))); //POD_NM
				$("#input_FERRY_POD").val(_fnToNull(sessionStorage.getItem("POD_CD"))); //POD_CD
				sessionStorage.clear();
				$("#ferrySchedule").show();
				fnGetFERRYScheduleData();
			}
        }
	}
}


//sort 기능
$(document).on("click", "table[name='Main_SEATable_List'] th", function () {

	if ($(this).find("button").length > 0) {

		var vValue = "";
	
		if ($(this).find("button").hasClass("asc")) {
			vValue = "desc";
		}
		else if ($(this).find("button").hasClass("desc")) {
			vValue = "asc";
		} else {
			vValue = "desc";
		}

		//초기화
		$("table[name='Main_SEATable_List'] th button").removeClass();
		$(this).find("button").addClass(vValue);

		_SEA_OrderBy = $(this).find("button").val();
		_SEA_Sort = vValue.toUpperCase();
		_vPage = 0;
		fnGetSEAScheduleData();
	}
});

//날짜 validation 체크
$(document).on("focusout", "#input_SEA_ETD", function () {

	if (!_fnisDate($(this).val())) {
		$(this).val("");
		$(this).focus();
	} else {
		var vValue = $(this).val();
		var vValue_Num = vValue.replace(/[^0-9]/g, "");
		if (vValue != "") {
			vValue = vValue_Num.substring("0", "4") + "-" + vValue_Num.substring("4", "6") + "-" + vValue_Num.substring("6", "8");
			$(this).val(vValue);
		}
	}
});

//퀵 Code - POL
$(document).on("click", "#input_SEA_Departure", function () {
	if ($("#input_SEA_Departure").val().length == 0) {
		$("#select_SEA_pop01").hide();
		$("#select_SEA_pop02").hide();
		selectPopOpen("#select_SEA_pop01");
	}
});

//퀵 Code - POD
$(document).on("click", "#input_SEA_Arrival", function () {
	if ($("#input_SEA_Arrival").val().length == 0) {
		$("#select_SEA_pop01").hide();
		$("#select_SEA_pop02").hide();
		selectPopOpen("#select_SEA_pop02");
	}
});

//퀵 Code 데이터 - POL
$(document).on("click", "#quick_SEA_POLCD button", function () {

	//split 해서 네이밍 , POL_CD 넣기
	var vValue = $(this).val();
	var vSplit = vValue.split(";");

	$("#input_SEA_Departure").val(vSplit[0]);
	$("#input_SEA_POL").val(vSplit[1]);
	$("#select_SEA_pop01").hide();

	if (_fnCheckSamePort(vSplit[1], "SEA", "POL", "Q", "select_SEA_pop01")) {
		//X박스 만들기
		$(this).closest(".int_box").addClass("has_del");
		$(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
	}
	selectPopOpen("#select_SEA_pop02");
});

$(document).on("click", "#quick_SEA_POLCD2 button", function () {

	//split 해서 네이밍 , POL_CD 넣기
	var vValue = $(this).val();
	var vSplit = vValue.split(";");

	$("#input_SEA_Departure").val(vSplit[0]);
	$("#input_SEA_POL").val(vSplit[1]);
	$("#select_SEA_pop01").hide();

	if (_fnCheckSamePort(vSplit[1], "SEA", "POL", "Q", "select_SEA_pop01")) {
		//X박스 만들기
		$(this).closest(".int_box").addClass("has_del");
		$(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
	}
	selectPopOpen("#select_SEA_pop02");
});

//퀵 Code 데이터 - POL
$(document).on("click", "#quick_SEA_PODCD button", function () {

	//split 해서 네이밍 , POL_CD 넣기
	var vValue = $(this).val();
	var vSplit = vValue.split(";");

	$("#input_SEA_Arrival").val(vSplit[0]);
	$("#input_SEA_POD").val(vSplit[1]);
	$("#select_SEA_pop02").hide();

	if (_fnCheckSamePort(vSplit[1], "SEA", "POD", "Q", "select_SEA_pop02")) {
		//X박스 만들기
		$(this).closest(".int_box").addClass("has_del");
		$(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
	}
});

//퀵 Code 데이터 - POD
$(document).on("click", "#quick_SEA_PODCD2 button", function () {

	//split 해서 네이밍 , POL_CD 넣기
	var vValue = $(this).val();
	var vSplit = vValue.split(";");

	$("#input_SEA_Arrival").val(vSplit[0]);
	$("#input_SEA_POD").val(vSplit[1]);
	$("#select_SEA_pop02").hide();

	if (_fnCheckSamePort(vSplit[1], "SEA", "POD", "Q", "select_SEA_pop02")) {
		//X박스 만들기
		$(this).closest(".int_box").addClass("has_del");
		$(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
	}
});

//자동완성 기능 - POL
$(document).on("keyup", "#input_SEA_Departure", function () {

	//input_POL 초기화
	if (_fnToNull($(this).val()) == "") {
		$("#input_SEA_POL").val("");
	}

	//출발 도시 바로 선택 화면 가리기
	if ($(this).val().length > 0) {
		$("#select_SEA_pop01").hide();
	}
	else if ($(this).val().length == 0) {
		$("#select_SEA_pop01").hide();
	}

	//autocomplete
	$(this).autocomplete({
		minLength: 3,
		open: function (event, ui) {
			$(this).autocomplete("widget").css({
				"width": $("#AC_SeaDeparture_Width").width()
			});
		},
		source: function (request, response) {
			var result = fnGetSEAPortData($("#input_SEA_Departure").val().toUpperCase());
			if (result != undefined) {
				result = JSON.parse(result).Table
				response(
					$.map(result, function (item) {
						return {
							label: item.NAME,
							value: item.NAME,
							code: item.CODE
						}
					})
				);
			}
		},
		select: function (event, ui) {
			if (ui.item.value.indexOf('데이터') == -1) {
				$("#input_SEA_Departure").val(ui.item.value);
				$("#input_SEA_POL").val(ui.item.code);
			} else {
				ui.item.value = "";
			}
		},
	}).autocomplete("instance")._renderItem = function (ul, item) {
		return $("<li>")
			.append("<div>" + item.value + "<br>" + item.code + "</div>")
			.appendTo(ul);
	};
	
});

//자동완성 기능 - POD
$(document).on("keyup", "#input_SEA_Arrival", function (e) {

	if (e.keyCode == 13) {
		
	} else {
		//input_POD 초기화
		if (_fnToNull($(this).val()) == "") {
			$("#input_SEA_POD").val("");
		}

		//출발 도시 바로 선택 화면 가리기
		if ($(this).val().length > 0) {
			$("#select_SEA_pop02").hide();
		}
		else if ($(this).val().length == 0) {
			$("#select_SEA_pop02").hide();
		}

		//autocomplete
		$(this).autocomplete({
			minLength: 3,
			open: function (event, ui) {
				$(this).autocomplete("widget").css({
					"width": $("#AC_SeaArrival_Width").width()
				});
			},
			source: function (request, response) {
				var result = fnGetSEAPortData($("#input_SEA_Arrival").val().toUpperCase());
				if (result != undefined) {
					result = JSON.parse(result).Table
					response(
						$.map(result, function (item) {
							return {
								label: item.NAME,
								value: item.NAME,
								code: item.CODE
							}
						})
					);
				}
			},
			select: function (event, ui) {
				if (ui.item.value.indexOf('데이터') == -1) {
					$("#input_SEA_Arrival").val(ui.item.value);
					$("#input_SEA_POD").val(ui.item.code);
				} else {
					ui.item.value = "";
				}
			},
		}).autocomplete("instance")._renderItem = function (ul, item) {
			return $("<li>")
				.append("<div>" + item.value + "<br>" + item.code + "</div>")
				.appendTo(ul);
		};
    }
});

//스케줄 검색
$(document).on("click", "#btn_SEASchedule_Search", function () {		

	$("table[name='Main_SEATable_List'] th button").removeClass();
	_SEA_OrderBy = "";
	_SEA_Sort = "";
	_vPage = 0;
	fnGetSEAScheduleData();
});

//더보기 버튼 이벤트
$(document).on("click", "#Btn_SEAScheduleMore button", function () {

	_SEA_OrderBy = "";
	_SEA_Sort = "";

	if (fnSEACheckCarr()) {
		fnGetSEAScheduleData();
	} else {
		_vPage = 0;
		fnGetSEAScheduleData();
	}
});

//부킹 버튼 - 이벤트
$(document).on("click", "a[name='btn_SEA_Booking']", function () {
	//alert($(this).siblings("input[type='hidden']").val());

	if (_fnToNull($("#Session_USR_ID").val()) != "") {
		fnSEASetBooking($(this).siblings("input[type='hidden']").val());
	} else {
		//layerPopup2('#login_pop');
		fnShowLoginLayer("goSEABooking;" + $(this).siblings("input[type='hidden']").val());
    }
});

//부킹 마감
$(document).on("click", "a[name='btn_SEA_Booking_Close']", function () {
	_fnAlertMsg("서류마감 된 스케줄입니다.");
});
////////////////////////function///////////////////////
//port 정보 가져오는 함수
function fnGetSEAPortData(vValue) {
	try {
		var rtnJson;
		var objJsonData = new Object();

		objJsonData.LOC_TYPE = "S";
		objJsonData.LOC_CD = vValue;

		$.ajax({
			type: "POST",
			url: "/Common/fnGetPort",
			async: false,
			dataType: "json",
			//data: callObj,
			data: { "vJsonData": _fnMakeJson(objJsonData) },
			success: function (result) {
				rtnJson = result;
			}, error: function (xhr, status, error) {
				$("#ProgressBar_Loading").hide(); //프로그래스 바
				alert("담당자에게 문의 하세요.");
				console.log(error);
			},
			beforeSend: function () {
				$("#ProgressBar_Loading").show(); //프로그래스 바
			},
			complete: function () {
				$("#ProgressBar_Loading").hide(); //프로그래스 바
			}
		});

		return rtnJson;
	} catch (e) {
		console.log(e.message);
	}
}

//스케줄 벨리데이션
function fnVali_SEASchedule() {

	//ETD를 입력 해 주세요.
	if (_fnToNull($("#input_SEA_ETD").val().replace(/-/gi, "")) == "") {
		_fnAlertMsg("ETD를 입력 해 주세요.","input_SEA_ETD");		
		return false;
	}

	//POL을 입력 해 주세요.
	if (_fnToNull($("#input_SEA_Departure").val()) == "") {
		_fnAlertMsg("출발 · 도착지를 선택해주세요.", "input_SEA_Departure");		
		return false;
	}

	if (_fnToNull($("#input_SEA_Arrival").val()) == "") {
		_fnAlertMsg("출발 · 도착지를 선택해주세요.", "input_SEA_Arrival");
		return false;
	}

	return true;
}

//스케줄 데이터 가져오는 함수
function fnGetSEAScheduleData() {

	try {

		//로그인 하지 않았을 경우 검색 되지 않게.
		//if (_fnToNull($("#Session_USR_ID").val() == "")) {
		//	layerPopup2('#login_pop');
		//	return false;
        //}

		if (fnVali_SEASchedule()) {

			var rtnJson;
			var objJsonData = new Object();

			//실제 데이터 전송
			objJsonData.REQ_SVC = "SEA";
			objJsonData.LINE_TYPE = "L"; //Line(L) : 선사 , Ferry(F) : 훼리
			objJsonData.CNTR_TYPE = "F";			

			objJsonData.POL = $("#input_SEA_Departure").val();
			objJsonData.POL_CD = $("#input_SEA_POL").val();
			objJsonData.POD = $("#input_SEA_Arrival").val();
			objJsonData.POD_CD = $("#input_SEA_POD").val();
			objJsonData.ETD_START = $("#input_SEA_ETD").val().replace(/-/gi, "");
			objJsonData.ETD_END = "";
			objJsonData.ID = "";
			objJsonData.ORDER = "";

			if (_vPage == 0) {
				_SEAObjCheck.POL = $("#input_SEA_Departure").val();
				_SEAObjCheck.POL_CD = $("#input_SEA_POL").val();
				_SEAObjCheck.POD = $("#input_SEA_Arrival").val();
				_SEAObjCheck.POD_CD = $("#input_SEA_POD").val();
				_SEAObjCheck.ETD_START = $("#input_SEA_ETD").val().replace(/-/gi, "");
				_SEAObjCheck.CNTR_TYPE = $("#select_SEA_CntrType").find("option:selected").val();
								
				objJsonData.PAGE = 1;
				_vPage = 1;
			} else {
				_vPage++;
				objJsonData.PAGE = _vPage;
			}

			//Sort
			if (_fnToNull(_SEA_OrderBy) != "" || _fnToNull(_SEA_Sort) != "") {
				objJsonData.ID = _SEA_OrderBy;
				objJsonData.ORDER = _SEA_Sort;
			} else {
				objJsonData.ID = "";
				objJsonData.ORDER = "";
			}

			$.ajax({
				type: "POST",
				url: "/Home/fnGetSEASchedule",
				async: true,
				dataType: "json",
				data: { "vJsonData": _fnMakeJson(objJsonData) },
				success: function (result) {
					$("#NoData_SEA").hide();
					fnMakeSEASchedule(result);
				}, error: function (xhr, status, error) {
					$("#ProgressBar_Loading").hide(); //프로그래스 바
					alert("담당자에게 문의 하세요.");
					console.log(error);
				},
				beforeSend: function () {
					$("#ProgressBar_Loading").show(); //프로그래스 바
				},
				complete: function () {
					$("#ProgressBar_Loading").hide(); //프로그래스 바
				}
			});

		} else {
			$("table[name='Main_SEATable_List'] th button").removeClass();
        }
		
	} catch (e) {
		console.log(e.message);
	}
}

//선사 스케줄 Booking 클릭 시
function fnSEASetBooking(vSCH_NO) {
	try {

		var objJsonData = new Object();
		objJsonData.SCH_NO = vSCH_NO;

		sessionStorage.setItem("BEFORE_VIEW_NAME", "MAIN_SEA");
		sessionStorage.setItem("VIEW_NAME", "REGIST");
		sessionStorage.setItem("POL_CD", $("#input_SEA_POL").val());
		sessionStorage.setItem("POD_CD", $("#input_SEA_POD").val());
		sessionStorage.setItem("POL_NM", $("#input_SEA_Departure").val());
		sessionStorage.setItem("POD_NM", $("#input_SEA_Arrival").val());
		sessionStorage.setItem("ETD", $("#input_SEA_ETD").val());
		sessionStorage.setItem("CNTR_TYPE", $("#select_SEA_CntrType").find("option:selected").val());
		sessionStorage.setItem("LINE_TYPE", 'SEA');

		controllerToLink("Regist", "Booking", objJsonData);

	}
	catch (err) {
		console.log("[Error - fnSEASetBooking]" + err.message);
    }
}

//더보기 눌렀을 시 처음 검색조건과 상이할 경우 재 검색
function fnSEACheckCarr()
{
	var vCheck = true;

	if (_SEAObjCheck.POL_CD != $("#input_SEA_POL").val()) {
		vCheck = false;
	}
	else if (_SEAObjCheck.POD_CD != $("#input_SEA_POD").val()) {
		vCheck = false;
	}
	else if (_SEAObjCheck.ETD_START != $("#input_SEA_ETD").val().replace(/-/gi, "")) {
		vCheck = false;
	}	
	else if (_SEAObjCheck.CNTR_TYPE != $("#select_SEA_CntrType").find("option:selected").val()) {
		vCheck = false;
	}

	return vCheck;
}
/////////////////function MakeList/////////////////////
//SEA 스케줄 만들기
function fnMakeSEASchedule(vJsonData) {
	var vHTML = "";

	try {
		//스케줄 데이터 만들기
		var vResult = JSON.parse(vJsonData).Schedule;
		var vMorePage = true;

		//초기화
		if (_vPage == 1) {
			$("#SEA_Schedule_AREA").eq(0).empty();
		}
		if (vResult == undefined) {
			$("#Btn_SEAScheduleMore").hide();
			vHTML += " <span>데이터가 없습니다.</span> ";
			$("#NoData_SEA")[0].innerHTML = vHTML;
			vHTML = "";
			$("#NoData_SEA").show();
			//vHTML += "   <tr class=\"row\" data-row=\"row_5\"> ";
			//vHTML += "   	<td colspan=\"10\"> ";
			//vHTML += "   		<ul class=\"etc_info\"> ";
			//vHTML += "   			<li class=\"no_data\"> ";
			//vHTML += "   				<em>데이터가 없습니다.</em> ";
			//vHTML += "   			</li> ";
			//vHTML += "   		</ul> ";
			//vHTML += "   	</td> ";
			//vHTML += "   	<!-- mobile area --> ";
			//vHTML += "   	<td class=\"mobile_layout\" colspan=\"9\"> ";
			//vHTML += "   		<div class=\"layout_type3\"> ";
			//vHTML += "   			<ul class=\"etc_info\"> ";
			//vHTML += "   				<li class=\"no_data\"> ";
			//vHTML += "   					<em>데이터가 없습니다.</em> ";
			//vHTML += "   				</li> ";
			//vHTML += "   			</ul> ";
			//vHTML += "   		</div>  ";
			//vHTML += "		</td> ";
			//vHTML += "   	<!-- //mobile area --> ";
			//vHTML += "   </tr> ";
		} else {
			if (vResult.length > 0) {
				$.each(vResult, function (i) {

					vHtml += "		<tr class='related_info' id='row_1' style='display: none;'>	";
					vHtml += "            <td colspan='10' class='sea_info'>	";
					vHtml += "                <ul class='etc_info'>	";
					vHtml += "                    <li class='item'>	";
					vHtml += "                        <em>반입지 : <span>한국</span></em>	";
					vHtml += "                    </li>	";
					vHtml += "                    <li class='item'>	";
					vHtml += "                        <em>담당자 : <span>홍길동</span></em>	";
					vHtml += "                    </li>	";
					vHtml += "                    <li class='item'>	";
					vHtml += "                        <em>비고 : <span>내용이 들어갑니다.</span></em>	";
					vHtml += "                    </li>	";
					vHtml += "                </ul>	";
					vHtml += "            </td>	";
					vHtml += "        </tr>	";
					vHTML += "   <tr class=\"row Schedule_" + _fnToNull(vResult[i]["LINE_CD"]) + "\" data-row=\"row_1\"> ";
					vHTML += "   	<td><img src=\"" + _fnToNull(vResult[i]["IMG_PATH"]) + "\" alt=\"\"></td> ";
					vHTML += "   	<td> ";
					vHTML += _fnToNull(vResult[i]["LINE_CD"]) + "<br />";
					vHTML += _fnToNull(vResult[i]["VSL_VOY"]);
					vHTML += "   	</td> ";
					vHTML += "   	<td> ";
					vHTML += String(_fnToNull(vResult[i]["ETD"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + " (" + (_fnGetWhatDay(_fnToNull(vResult[i]["ETD"]).replace(/\./gi, ""))) + ")<br /> ";
					vHTML += _fnToNull(vResult[i]["POL_CD"]) + "";
					vHTML += "   	</td> ";
					vHTML += "   	<td> ";
					vHTML += String(_fnToNull(vResult[i]["ETA"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + " (" + (_fnGetWhatDay(_fnToNull(vResult[i]["ETA"]).replace(/\./gi, ""))) + ")<br /> ";
					vHTML += _fnToNull(vResult[i]["POD_CD"]) + "";
					vHTML += "   	</td> ";
					if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) == "" || _fnToNull(vResult[i]["DOC_CLOSE_YMD"]) == "0") {
						vHTML += "   	<td> ";
						vHTML += "";
					} else {

						if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) + _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"])).replace(/:/gi, "") < _fnGetDateStamp()) {
							vHTML += "   	<td style='color:red'> ";
						}
						else if (_fnToNull(vResult[i]["PREV_CLOSE"]) <= _fnGetTodayStamp()) {
							vHTML += "   	<td style='color:orange'> ";
						} else {
							vHTML += "   	<td> ";
						}
						vHTML += String(_fnToNull(vResult[i]["DOC_CLOSE_YMD"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + " (" + (_fnGetWhatDay(_fnToNull(vResult[i]["DOC_CLOSE_YMD"]).replace(/\./gi, ""))) + ")<br /> ";
						vHTML += _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"]));
					}

					vHTML += "   	</td> ";
					vHtml += "            <td>" + _fnToNull(vResult[i]["CNTR_TYPE"]) + "</td>	";
					if (Number(_fnToNull(vResult[i]["TRANSIT_TIME"])) > 0) {
						if (Number(_fnToNull(vResult[i]["TRANSIT_TIME"]) > 1)) {
							vHTML += "   	<td>" + _fnToNull(vResult[i]["TRANSIT_TIME"]) + " Days</td> ";
						} else {
							vHTML += "   	<td>" + _fnToNull(vResult[i]["TRANSIT_TIME"]) + " Day</td> ";
						}
					} else {
						vHTML += "   	<td></td> ";
					}

					if (_fnToNull(vResult[i]["TS_CNT"]) == "0") {
						vHTML += "   	<td>T/S</td> ";
					} else if (_fnToNull(vResult[i]["TS_CNT"]) == "1") {
						vHTML += "   	<td>Direct</td> ";
					} else {
						vHTML += "   	<td></td> ";
					}
					vHTML += "   	<td class=\"btns_w1\"> ";
					if (_fnToNull($("#Session_USR_TYPE").val()) == "P") {
						vHTML += "	 <a href=\"javascript:void(0)\" class=\"btn_type1 booking-btn\" id=\"ShowBookingPopup\">Booking</a>";
					} else {
						if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) + _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"])).replace(/:/gi, "") < _fnGetDateStamp()) {
							vHTML += "			<a href=\"javascript:void(0)\" name=\"btn_SEA_Booking_Close\" class=\"btn_type1 btnClose\">BOOKING</a>";
						} else {
							vHTML += "	 <a href=\"javascript:void(0)\" class=\"btn_type1 booking-btn\" id=\"ShowBookingPopup\">Booking</a>";
						}
					}
					vHTML += "			<input type=\"hidden\" value=\"" + _fnToNull(vResult[i]["SCH_NO"]) + "\" /> 		";
					vHTML += "		</td>";
					vHtml += "            <td class='btns_w2'>	";
					vHtml += "                <a class='plus' id='plus' href='javascript:void(0)'><span class='btn_minus'></span><span class='btn_plus'></span></a>	";
					vHtml += "            </td>	";
					vHtml += "            <td class='mobile_layout' colspan='9'>	";
					vHtml += "                <div class='layout_type2'>	";
					vHtml += "                    <div class='row s3'>	";
					vHtml += "                        <table>	";
					vHtml += "                            <tbody>	";
					vHtml += "                                <tr>	";
					vHtml += "                                    <th>Carrier</th>	";
					vHTML += "   						<td><img src=\"" + _fnToNull(vResult[i]["IMG_PATH"]) + "\" alt=\"\"></td> ";
					vHtml += "                                </tr>	";
					vHtml += "                                <tr>	";
					vHtml += "                                    <th>Vessel</th>	";
					vHTML += "   						<td>" + _fnToNull(vResult[i]["LINE_CD"]) + " " + _fnToNull(vResult[i]["VSL_VOY"]) + "</td> ";
					vHtml += "                                </tr>	";
					vHtml += "                                <tr>	";
					vHtml += "                                    <th>Departure</th>	";
					vHTML += "   						<td>" + String(_fnToNull(vResult[i]["ETD"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + "(" + (_fnGetWhatDay(_fnToNull(vResult[i]["ETD"]).replace(/\./gi, ""))) + ") " + _fnToNull(vResult[i]["POL_TRMN"]) + "</td> ";
					vHtml += "                                </tr>	";
					vHtml += "                                <tr>	";
					vHtml += "                                    <th>Arrival</th>	";
					vHTML += "   						<td>" + String(_fnToNull(vResult[i]["ETA"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + "(" + (_fnGetWhatDay(_fnToNull(vResult[i]["ETA"]).replace(/\./gi, ""))) + ") " + _fnToNull(vResult[i]["POD_TRMN"]) + "</td> ";
					vHtml += "                                </tr>	";
					vHtml += "                                <tr>	";
					vHtml += "                                    <th>Doc Closing</th>	";
					if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) == "" || _fnToNull(vResult[i]["DOC_CLOSE_YMD"]) == "0") {
						vHTML += "   						<td> ";
						vHTML += "   						</td> ";
					} else {

						if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) + _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"])).replace(/:/gi, "") < _fnGetDateStamp()) {
							vHTML += "   	<td style='color:red'> ";
						}
						else if (_fnToNull(vResult[i]["PREV_CLOSE"]) <= _fnGetTodayStamp()) {
							vHTML += "   	<td style='color:orange'> ";
						} else {
							vHTML += "   	<td> ";
						}

						vHTML += String(_fnToNull(vResult[i]["DOC_CLOSE_YMD"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + " (" + (_fnGetWhatDay(_fnToNull(vResult[i]["DOC_CLOSE_YMD"]).replace(/\./gi, ""))) + ") " + _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"]));
						vHTML += "   						</td> ";
					}
					vHtml += "                                </tr>	";
					vHtml += "                                <tr>	";
					vHtml += "                                    <th>Service</th>	";

					vHTML += " <td> ";
					vHTML += _fnToNull(vResult[i]["CNTR_TYPE"]);
					vHTML += " </td> ";
					vHtml += "                                </tr>	";
					vHtml += "                                <tr>	";
					vHtml += "                                    <th>T/time</th>	";
					if (Number(_fnToNull(vResult[i]["TRANSIT_TIME"])) > 0) {
						if (Number(_fnToNull(vResult[i]["TRANSIT_TIME"]) > 1)) {
							vHTML += "   	<td>" + _fnToNull(vResult[i]["TRANSIT_TIME"]) + " Days</td> ";
						} else {
							vHTML += "   	<td>" + _fnToNull(vResult[i]["TRANSIT_TIME"]) + " Day</td> ";
						}
					} else {
						vHTML += "   	<td></td> ";
					}
					vHtml += "                                </tr>	";
					vHtml += "                                <tr>	";
					vHtml += "                                    <th>T/S</th>	";
					if (_fnToNull(vResult[i]["TS_CNT"]) == "0") {
						vHTML += "   	<td>T/S</td> ";
					} else if (_fnToNull(vResult[i]["TS_CNT"]) == "1") {
						vHTML += "   	<td>Direct</td> ";
					} else {
						vHTML += "   	<td></td> ";
					}

					vHtml += "                                </tr>	";
					vHtml += "                                <tr class='sch_comment'>	";
					vHtml += "                                    <td colspan='2' class='sea_info'>	";
					vHtml += "                                        <ul class='etc_info'>	";
					vHtml += "                                            <li class='item'>	";
					vHTML += "   				<em>반입지 : " + _fnToNull(vResult[i]["POL_TML_NM"]) + "</em> ";
					vHtml += "                                            </li>	";
					vHtml += "                                            <li class='item'>	";
					vHTML += "   				<em>담당자 : " + _fnToNull(vResult[i]["SCH_PIC"]) + "</em> ";
					vHtml += "                                            </li>	";
					vHtml += "                                            <li class='item'>	";
					vHTML += "   				<em>비고 : " + _fnToNull(vResult[i]["RMK"]) + "</em> ";
					vHtml += "                                            </li>	";
					vHtml += "                                        </ul>	";
					vHtml += "                                    </td>	";
					vHtml += "                                </tr>	";
					vHtml += "                                <tr>	";
					vHtml += "                                    <td colspan='2'>	";
					if (_fnToNull($("#Session_USR_TYPE").val()) == "P") {
						vHTML += "			<a href=\"javascript:void(0)\"  class=\"btn_type1 gray\" style='cursor:default'>BOOKING</a>";
					} else {
						if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) + _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"])).replace(/:/gi, "") < _fnGetDateStamp()
						) {
							vHTML += "			<a href=\"javascript:void(0)\" name=\"btn_SEA_Booking_Close\" class=\"btn_type1 btnClose\">BOOKING</a>";
						} else {
							vHTML += "			<a href=\"javascript:void(0)\" name=\"btn_SEA_Booking\" class=\"btn_type1\">BOOKING</a>";
						}
					}
					vHTML += "			<input type=\"hidden\" value=\"" + _fnToNull(vResult[i]["SCH_NO"]) + "\" /> 		";
					vHtml += "                                    </td>	";
					vHtml += "                                </tr>	";
					vHtml += "                            </tbody>	";
					vHtml += "                        </table>	";
					vHtml += "                    </div>	";
					vHtml += "                </div>	";
					vHtml += "            </td>	";
					vHtml += "        </tr>	";

				//	vHTML += "   <tr class=\"row Schedule_" + _fnToNull(vResult[i]["LINE_CD"]) + "\" data-row=\"row_1\"> ";
				//	vHTML += "   	<td><img src=\"" + _fnToNull(vResult[i]["IMG_PATH"]) + "\" alt=\"\"></td> ";
				//	vHTML += "   	<td> ";
				//	vHTML += _fnToNull(vResult[i]["LINE_CD"]) + "<br />";
				//	vHTML += _fnToNull(vResult[i]["VSL_VOY"]);
				//	vHTML += "   	</td> ";
				//	vHTML += "   	<td> ";
				//	vHTML += String(_fnToNull(vResult[i]["ETD"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + " (" + (_fnGetWhatDay(_fnToNull(vResult[i]["ETD"]).replace(/\./gi, ""))) + ")<br /> ";
				//	vHTML += _fnToNull(vResult[i]["POL_CD"]) + "";
				//	vHTML += "   	</td> ";
				//	vHTML += "   	<td> ";
				//	vHTML += String(_fnToNull(vResult[i]["ETA"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + " (" + (_fnGetWhatDay(_fnToNull(vResult[i]["ETA"]).replace(/\./gi, ""))) + ")<br /> ";
				//	vHTML += _fnToNull(vResult[i]["POD_CD"]) + "";
				//	vHTML += "   	</td> ";

				//	if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) == "" || _fnToNull(vResult[i]["DOC_CLOSE_YMD"]) == "0") {
				//		vHTML += "   	<td> ";
				//		vHTML += "";
				//	} else {

				//		if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) + _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"])).replace(/:/gi, "") < _fnGetDateStamp()) {
				//			vHTML += "   	<td style='color:red'> ";
				//		}
				//		else if (_fnToNull(vResult[i]["PREV_CLOSE"]) <= _fnGetTodayStamp()) {
				//			vHTML += "   	<td style='color:orange'> ";
				//		} else {
				//			vHTML += "   	<td> ";
				//		}
				//		vHTML += String(_fnToNull(vResult[i]["DOC_CLOSE_YMD"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + " (" + (_fnGetWhatDay(_fnToNull(vResult[i]["DOC_CLOSE_YMD"]).replace(/\./gi, ""))) + ")<br /> ";
				//		vHTML += _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"]));
				//	}

				//	vHTML += "   	</td> ";
				//	vHTML += "   	<td> ";

				//	//Service
				//	vHTML += _fnToNull(vResult[i]["CNTR_TYPE"]);

				//	//if (_fnToNull(vResult[i]["CARGO_CLOSE_YMD"]) == "" || _fnToNull(vResult[i]["CARGO_CLOSE_YMD"]) == "0") {
				//	//	vHTML += "";
				//	//} else {
				//	//	vHTML += String(_fnToNull(vResult[i]["CARGO_CLOSE_YMD"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + " (" + (_fnGetWhatDay(_fnToNull(vResult[i]["CARGO_CLOSE_YMD"]).replace(/\./gi, ""))) + ")<br /> ";
				//	//	vHTML += _fnFormatTime(_fnToNull(vResult[i]["CARGO_CLOSE_HM"]));
				//	//}

				//	vHTML += "   	</td> ";

				//	if (Number(_fnToNull(vResult[i]["TRANSIT_TIME"])) > 0) {
				//		if (Number(_fnToNull(vResult[i]["TRANSIT_TIME"]) > 1)) {
				//			vHTML += "   	<td>" + _fnToNull(vResult[i]["TRANSIT_TIME"]) + " Days</td> ";
				//		} else {
				//			vHTML += "   	<td>" + _fnToNull(vResult[i]["TRANSIT_TIME"]) + " Day</td> ";
				//		}
				//	} else {
				//		vHTML += "   	<td></td> ";
				//	}

				//	if (_fnToNull(vResult[i]["TS_CNT"]) == "0") {
				//		vHTML += "   	<td>T/S</td> ";
				//	} else if (_fnToNull(vResult[i]["TS_CNT"]) == "1") {
				//		vHTML += "   	<td>Direct</td> ";
				//	} else {
				//		vHTML += "   	<td></td> ";
				//	}

				//	vHTML += "   	<td class=\"btns_w1\"> ";
				//	if (_fnToNull($("#Session_USR_TYPE").val()) == "P") {
				//		vHTML += "			<a href=\"javascript:void(0)\"  class=\"btn_type1 gray\" style='cursor:default'>BOOKING</a>";
				//	} else {
				//		if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) + _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"])).replace(/:/gi, "") < _fnGetDateStamp()) {
				//			vHTML += "			<a href=\"javascript:void(0)\" name=\"btn_SEA_Booking_Close\" class=\"btn_type1 btnClose\">BOOKING</a>";
				//		}  else {
				//			vHTML += "			<a href=\"javascript:void(0)\" name=\"btn_SEA_Booking\" class=\"btn_type1\">BOOKING</a>";
				//		}
				//	}
				//	vHTML += "			<input type=\"hidden\" value=\"" + _fnToNull(vResult[i]["SCH_NO"]) + "\" /> 		";
				//	vHTML += "		</td>";

				//	vHTML += "		<td class=\"btns_w2\"> ";
				//	vHTML += "			<a class=\"plus\" id=\"plus\" href=\"javascript:void(0)\"><span class=\"btn_minus\"></span><span class=\"btn_plus\"></span></a> ";
				//	vHTML += "		</td> ";

				//	/* mobile_layout  */
				//	vHTML += "   <td class=\"mobile_layout\" colspan=\"9\"> ";
				//	vHTML += "   	<div class=\"layout_type2\"> ";
				//	//vHTML += "   		<div class=\"row s1\"> ";
				//	//vHTML += "   			<img src=\"" + _fnToNull(vResult[i]["IMG_PATH"]) + "\" alt=\"\"> ";
				//	//vHTML += "   		</div> ";
				//	//vHTML += "   		<div class=\"row s2\"> ";
				//	//vHTML += "   			" + _fnToNull(vResult[i]["LINE_CD"]) + " " + _fnToNull(vResult[i]["VSL_VOY"]);
				//	//vHTML += "   		</div> ";
				//	vHTML += "   		<div class=\"row s3\"> ";
				//	vHTML += "   			<table> ";
				//	vHTML += "   				<tbody> ";

				//	vHTML += "   					<tr> ";
				//	vHTML += "   						<th>Carrier</th> ";
				//	vHTML += "   						<td><img src=\"" + _fnToNull(vResult[i]["IMG_PATH"]) + "\" alt=\"\"></td> ";
				//	vHTML += "   					</tr> ";

				//	vHTML += "   					<tr> ";
				//	vHTML += "   						<th>Vessel</th> ";
				//	vHTML += "   						<td>" + _fnToNull(vResult[i]["LINE_CD"]) + " " + _fnToNull(vResult[i]["VSL_VOY"])+ "</td> ";
				//	vHTML += "   					</tr> ";

				//	vHTML += "   					<tr> ";
				//	vHTML += "   						<th>Departure</th> ";
				//	vHTML += "   						<td>" + String(_fnToNull(vResult[i]["ETD"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + "(" + (_fnGetWhatDay(_fnToNull(vResult[i]["ETD"]).replace(/\./gi, ""))) + ") " + _fnToNull(vResult[i]["POL_TRMN"]) + "</td> ";
				//	vHTML += "   					</tr> ";
				//	vHTML += "   					<tr> ";
				//	vHTML += "   						<th>Arrival</th> ";
				//	vHTML += "   						<td>" + String(_fnToNull(vResult[i]["ETA"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + "(" + (_fnGetWhatDay(_fnToNull(vResult[i]["ETA"]).replace(/\./gi, ""))) + ") " + _fnToNull(vResult[i]["POD_TRMN"]) + "</td> ";
				//	vHTML += "   					</tr> ";
				//	vHTML += "   					<tr> ";
				//	vHTML += "   						<th>Doc Closing</th> ";

				//	if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) == "" || _fnToNull(vResult[i]["DOC_CLOSE_YMD"]) == "0") {
				//		vHTML += "   						<td> "; 
				//		vHTML += "   						</td> ";  
				//	} else {

				//		if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) + _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"])).replace(/:/gi, "") < _fnGetDateStamp()) {
				//			vHTML += "   	<td style='color:red'> ";
				//		}
				//		else if (_fnToNull(vResult[i]["PREV_CLOSE"]) <= _fnGetTodayStamp()) {
				//			vHTML += "   	<td style='color:orange'> ";
				//		} else {
				//			vHTML += "   	<td> ";
				//		}

				//		vHTML += String(_fnToNull(vResult[i]["DOC_CLOSE_YMD"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + " (" + (_fnGetWhatDay(_fnToNull(vResult[i]["DOC_CLOSE_YMD"]).replace(/\./gi, ""))) + ") " + _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"]));
				//		vHTML += "   						</td> ";
				//	}

				//	vHTML += "   					</tr> ";
				//	vHTML += "   					<tr> ";
				//	vHTML += "   						<th>Service</th> ";

				//	vHTML += " <td> ";
				//	vHTML += _fnToNull(vResult[i]["CNTR_TYPE"]);
				//	vHTML += " </td> ";

				//	//if (_fnToNull(vResult[i]["CARGO_CLOSE_YMD"]) == "" || _fnToNull(vResult[i]["CARGO_CLOSE_YMD"]) == "0") {
				//	//	vHTML += "   						<td></td> ";
				//	//} else {
				//	//	vHTML += "   						<td>" + String(_fnToNull(vResult[i]["CARGO_CLOSE_YMD"])).replace(/(\d{4})(\d{2})(\d{2})/, '$1.$2.$3') + " (" + (_fnGetWhatDay(_fnToNull(vResult[i]["CARGO_CLOSE_YMD"]).replace(/\./gi, ""))) + ") " + _fnFormatTime(_fnToNull(vResult[i]["CARGO_CLOSE_HM"])) + "</td> ";
				//	//}

				//	vHTML += "   					</tr> ";
				//	vHTML += "   					<tr> ";
				//	vHTML += "   						<th>T/time</th> ";

				//	if (Number(_fnToNull(vResult[i]["TRANSIT_TIME"])) > 0) {
				//		if (Number(_fnToNull(vResult[i]["TRANSIT_TIME"]) > 1)) {
				//			vHTML += "   	<td>" + _fnToNull(vResult[i]["TRANSIT_TIME"]) + " Days</td> ";
				//		} else {
				//			vHTML += "   	<td>" + _fnToNull(vResult[i]["TRANSIT_TIME"]) + " Day</td> ";
				//		}
				//	} else {
				//		vHTML += "   	<td></td> ";
				//	}

				//	vHTML += "   					</tr> ";
				//	vHTML += "   					<tr> ";
				//	vHTML += "   						<th>T/S</th> ";

				//	if (_fnToNull(vResult[i]["TS_CNT"]) == "0") {
				//		vHTML += "   	<td>T/S</td> ";
				//	} else if (_fnToNull(vResult[i]["TS_CNT"]) == "1") {
				//		vHTML += "   	<td>Direct</td> ";
				//	} else {
				//		vHTML += "   	<td></td> ";
				//	}

				//	vHTML += "   					</tr> ";

				//	vHTML += "   <tr class=\"sch_comment\"> ";
				//	vHTML += "   	<td colspan=\"2\"> ";
				//	vHTML += "   		<ul class=\"etc_info\"> ";
				//	vHTML += "   			<li class=\"item\"> ";
				//	vHTML += "   				<em>반입지 : " + _fnToNull(vResult[i]["POL_TML_NM"]) + "</em> ";
				//	vHTML += "   			</li> ";
				//	vHTML += "   			<li class=\"item\"> ";
				//	vHTML += "   				<em>담당자 : " + _fnToNull(vResult[i]["SCH_PIC"]) + "</em> ";
				//	vHTML += "   			</li> ";
				//	vHTML += "   			<li class=\"item\"> ";
				//	vHTML += "   				<em>비고 : " + _fnToNull(vResult[i]["RMK"]) + "</em> ";
				//	vHTML += "   			</li> ";
				//	vHTML += "   		</ul> ";
				//	vHTML += "   	</td> ";
				//	vHTML += "   </tr> ";
				//	vHTML += "   <tr> ";
				//	vHTML += "   	<td colspan=\"2\"> ";
				//	if (_fnToNull($("#Session_USR_TYPE").val()) == "P") {
				//		vHTML += "			<a href=\"javascript:void(0)\"  class=\"btn_type1 gray\" style='cursor:default'>BOOKING</a>";
				//	} else {
				//		if (_fnToNull(vResult[i]["DOC_CLOSE_YMD"]) + _fnFormatTime(_fnToNull(vResult[i]["DOC_CLOSE_HM"])).replace(/:/gi, "") < _fnGetDateStamp()
				//			) {
				//			vHTML += "			<a href=\"javascript:void(0)\" name=\"btn_SEA_Booking_Close\" class=\"btn_type1 btnClose\">BOOKING</a>";
				//		} else {
				//			vHTML += "			<a href=\"javascript:void(0)\" name=\"btn_SEA_Booking\" class=\"btn_type1\">BOOKING</a>";
				//		}
				//	}
				//	vHTML += "			<input type=\"hidden\" value=\"" + _fnToNull(vResult[i]["SCH_NO"]) + "\" /> 		";
				//	vHTML += "   	</td> ";
				//	vHTML += "   </tr> ";
				//	vHTML += "   				</tbody> ";
				//	vHTML += "   			</table> ";
				//	vHTML += "   		</div> ";
				//	vHTML += "   	</div> ";
				//	vHTML += "   </td> ";
				///* mobile_layout  */

				//	vHTML += "   </tr> ";
				//	vHTML += "   <tr class=\"related_info\" id=\"row_1\"> ";
				//	vHTML += "   	<td colspan=\"10\"> ";
				//	vHTML += "   		<ul class=\"etc_info\"> ";
				//	vHTML += "   			<li class=\"item\"> ";
				//	vHTML += "   				<em>반입지 : " + _fnToNull(vResult[i]["POL_TML_NM"]) + "</em> ";
				//	vHTML += "   			</li> ";
				//	vHTML += "   			<li class=\"item\"> ";
				//	vHTML += "   				<em>담당자 : " + _fnToNull(vResult[i]["SCH_PIC"]) + "</em> ";
				//	vHTML += "   			</li> ";
				//	vHTML += "   			<li class=\"item\"> ";
				//	vHTML += "   				<em>비고 : " + _fnToNull(vResult[i]["RMK"])+"</em> ";					
				//	vHTML += "   			</li> ";
				//	vHTML += "   		</ul> ";
				//	vHTML += "   	</td> ";
				//	vHTML += "   </tr> ";

					//더보기 체크 RNUM == TOTCNT
					if (_fnToNull(vResult[i]["RNUM"]) == _fnToNull(vResult[i]["TOTCNT"])) {
						vMorePage = false;
					} else {
						vMorePage = true;
					}
				});

				//더보기 영역
				if (vMorePage) {
					$("#Btn_SEAScheduleMore").show();
				} else {
					$("#Btn_SEAScheduleMore").hide();
				}
			}
			else {
				$("#Btn_SEAScheduleMore").hide();
				vHTML += " <span>데이터가 없습니다.</span> ";
				$("#NoData_SEA")[0].innerHTML = vHTML;
				$("#NoData_SEA").show();
				vHTML = "";
				//vHTML += "   <tr class=\"row\" data-row=\"row_5\"> ";
				//vHTML += "   	<td colspan=\"10\"> ";
				//vHTML += "   		<ul class=\"etc_info\"> ";
				//vHTML += "   			<li class=\"no_data\"> ";
				//vHTML += "   				<em>데이터가 없습니다.</em> ";
				//vHTML += "   			</li> ";
				//vHTML += "   		</ul> ";
				//vHTML += "   	</td> ";
				//vHTML += "   	<!-- mobile area --> ";
				//vHTML += "   	<td class=\"mobile_layout\" colspan=\"9\"> ";
				//vHTML += "   		<div class=\"layout_type3\"> ";
				//vHTML += "   			<ul class=\"etc_info\"> ";
				//vHTML += "   				<li class=\"no_data\"> ";
				//vHTML += "   					<em>데이터가 없습니다.</em> ";
				//vHTML += "   				</li> ";
				//vHTML += "   			</ul> ";
				//vHTML += "   		</div>  ";
				//vHTML += "		</td> ";
				//vHTML += "   	<!-- //mobile area --> ";
				//vHTML += "   </tr> ";
			}
		}

		$("#SEA_Schedule_AREA").eq(0).append(vHTML);
		$("#SEA_Schedule_AREA").show();
	} catch (e) {
		console.log(e.message);
	}
}
////////////////////////API////////////////////////////

