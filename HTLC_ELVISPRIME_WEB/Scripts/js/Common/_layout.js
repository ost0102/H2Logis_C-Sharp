
$(document).on("click", "input[name='cargo']", function () {

	if ($(this).attr("id") == "cargo01") {
		$("div[name='Cargo_Express_Input_Box']").eq(0).show();
		$("div[name='Cargo_Express_Input_Box']").eq(1).hide();
	} else if ($(this).attr("id") == "cargo02") {
		$("div[name='Cargo_Express_Input_Box']").eq(0).hide();
		$("div[name='Cargo_Express_Input_Box']").eq(1).show();
	}

});

//수출이행내역 수출신고번호, B/L 인풋박스 보여주기
$(document).on("click", "input[name='export']", function () {

	if ($(this).attr("id") == "export01") {
		$("div[name='UniPass_Export_Box']").eq(0).show();
		$("div[name='UniPass_Export_Box']").eq(1).hide();
	} else if ($(this).attr("id") == "export02") {
		$("div[name='UniPass_Export_Box']").eq(0).hide();
		$("div[name='UniPass_Export_Box']").eq(1).show();
	}
});

//로그인 영역

var Login_Count = 0;
//로그인 버튼 클릭 이벤트
$(document).on("click", "#Login_btn", function () {
	if (Login_Count == -3) {
		_fnLayerAlertMsg("보안문자를 그려주세요");
	} else {
		_fnLogin();
	}
});

//로그인 엔터 이벤트
$(document).on("keyup", "#Login_ID", function (e) {
	if (e.keyCode == 13) {
		if (Login_Count == -3) {
			_fnLayerAlertMsg("보안문자를 그려주세요");
		} else {
			if (_fnToNull($("#Login_Password").val()) != "") {
				_fnLogin();
			} else {
				$("#Login_Password").focus();
			}
		}
	}
});

$(document).on("focus", "#Login_ID", function (e) {
	$("#Password_Warning").hide();
});

$(document).on("focus", "#Login_Password", function (e) {
	$("#Email_Warning").hide();
});



//로그인 엔터 이벤트
$(document).on("keyup", "#Login_Password", function (e) {
	if (e.keyCode == 13) {
		if (Login_Count == -3) {
			_fnLayerAlertMsg("보안문자를 그려주세요");
		} else {
			_fnLogin();
		}
	}
});


//로그인 모션캡처 '새로고침' 버튼 이벤트 
$(document).on("click", ".refresh", function (e) {
	var vHTML = "";
	vHTML += "<div id=\"mc\">";
	vHTML += "<canvas id=\"mc-canvas\"  width=\"220\" height=\"200\"  class=\"mc-valid\"></canvas>";
	vHTML += "</div> ";

	$("#Captcha_Area").empty();
	$("#Captcha_Area").append(vHTML);

	//mc-form => Captcha_Area
	$('#Captcha_Area').motionCaptcha({
		action: '#fairly-unique-id',
		shapes: ['triangle', 'x', 'rectangle', 'circle', 'check', 'caret', 'zigzag', 'arrow', 'leftbracket', 'rightbracket', 'v', 'delete', 'star', 'pigtail']
	});
});


//로그인 함수
function _fnLogin() {
	try {
		//로그인 체크
		if ($("#Login_ID").val() == "") {
			$("#Password_Warning").hide();
			$("#Email_Warning").show();
			$("#Login_ID").focus();
			return false;
		}
		else {
			$("#Email_Warning").hide();
		}
		if ($("#Login_Password").val() == "") {
			$("#Email_Warning").hide();
			$("#Password_Warning").show();
			$("#Login_Password").focus();
			return false;
		}
		else {
			$("#Password_Warning").hide();
		}

		var objJsonData = new Object();
		objJsonData.USR_ID = $("#Login_ID").val();
		objJsonData.PSWD = $("#Login_Password").val();

		$.ajax({
			type: "POST",
			url: "/Home/fnLogin",
			async: true,
			dataType: "json",
			data: { "vJsonData": _fnMakeJson(objJsonData) },
			success: function (result) {

				if (JSON.parse(result).Result[0]["trxCode"] == "Y") {

					if (JSON.parse(result).Table[0].APV_YN == "Y") {
						//아이디 저장 체크 일 경우 쿠키에 저장
						if ($('input[name=login_keep]')[0].checked) {
							_fnSetCookie("Prime_CK_USR_ID_REMEMBER_ISLS", JSON.parse(result).Table[0].USR_ID, "168");
						} else {
							_fnDelCookie("Prime_CK_USR_ID_REMEMBER_ISLS");
						}

						var vUserType = JSON.parse(result).Table;

						$.ajax({
							type: "POST",
							url: "/Home/SaveLogin",
							async: true,
							data: { "vJsonData": _fnMakeJson(JSON.parse(result)) },
							success: function (result, status, xhr) {

								if (_fnToNull(result) == "Y") {
									window.location = window.location.origin;
								}
								else if (_fnToNull(result) == "N") {
									console.log("[Fail : fnLogin()]");
									_fnLayerAlertMsg("관리자에게 문의 하세요");
								}
								else {
									console.log("[Error : fnLogin()]" + _fnToNull(result));
									_fnLayerAlertMsg("관리자에게 문의 하세요");
								}

								//if (_fnToNull(JSON.parse(result).Table[0].APP_KEY) != "") {
								//    //window.location = urlpath + "/Schedule/SchList";
								//    window.location = window.location.origin + _initPage;
								//} else {
								//    if (_fnToNull(sessionStorage.getItem('finalUrl')) == '') {
								//        //window.location = vLocation_Login;
								//        window.location = window.location.origin + _initPage;
								//    } else {
								//        location.replace(sessionStorage.getItem('finalUrl'));
								//    }
								//}
							}
						});
					}
					else if (JSON.parse(result).Table[0].APV_YN == "N") {
						_fnLayerAlertMsg("승인이 되지 않았습니다. 담당자에게 문의 하세요.");
					}
					else if (JSON.parse(result).Table[0].APV_YN == "D") {
						_fnLayerAlertMsg("가입 승인이 거절 되었습니다. 메일에서 거절 사유를 확인 해 주세요.");
					}
					else if (JSON.parse(result).Table[0].APV_YN == "S") {
						_fnLayerAlertMsg("아이디가 정지 되었습니다. 담당자에게 문의 하세요.");
					}
				}
				else if (JSON.parse(result).Result[0]["trxCode"] == "N") {
					_fnLayerAlertMsg("아이디 혹은 비밀번호가 틀렸습니다");
					//_fnAlertMsg("아이디 혹은 비밀번호가 틀렸습니다");
					if (Login_Count >= 0) {
						Login_Count++;  //로그인 횟수 체크
					}
				}

				//5번 로그인 틀렸을 시 보안문자 생성
				if (Login_Count > 5 || Login_Count == -1) {
					Login_Count = -3;
					$(".security_char").show(); //로그인 버튼 비활성화
					var vHTML = "";
					vHTML += "<div id=\"mc\">";
					vHTML += "<canvas id=\"mc-canvas\"  width=\"220\" height=\"200\"  class=\"mc-valid\"></canvas>";
					vHTML += "</div> ";

					$("#Captcha_Area").empty();
					$("#Captcha_Area").append(vHTML);

					//mc-form => Captcha_Area
					$('#Captcha_Area').motionCaptcha({
						action: '#fairly-unique-id',
						shapes: ['triangle', 'x', 'rectangle', 'circle', 'check', 'caret', 'zigzag', 'arrow', 'leftbracket', 'rightbracket', 'v', 'delete', 'star', 'pigtail']
					});
				}
			}, error: function (xhr) {
				console.log("시스템 사정으로 요청하신 작업을 처리할 수 없습니다.");
				console.log(xhr);
				return;
			}, beforeSend: function () {
				$("#ProgressBar_Loading").show(); //프로그래스 바 

			},
			complete: function () {
				$("#ProgressBar_Loading").hide(); //프로그래스 바 
			}
		});
	} catch (err) {
		console.log(err.message);
	}
}

////로그아웃 (세션 , 쿠키 삭제)
function _fnLogout() {
	////로그아웃 (세션 , 쿠키 삭제)
	$.ajax({
		type: "POST",
		url: "/Home/LogOut",
		async: false,
		success: function (result, status, xhr) {

			$("#Session_USR_ID ").val("");
			$("#Session_LOC_NM ").val("");
			$("#Session_EMAIL").val("");
			$("#Session_CUST_CD").val("");
			$("#Session_HP_NO").val("");
			$("#Session_DOMAIN").val("");
			$("#Session_OFFICE_CD").val("");
			$("#Session_AUTH_KEY").val("");
			$("#Session_USR_TYPE").val("");

			location.href = window.location.origin;
		}
	});
}


//로그인 영역 끝

var mymap;

function drawingLayerNodata() {

	if (_fnToNull(mymap) != "") {
		$("#map").empty();
		mymap.remove();
	}
	//mymap = L.map('map', {
	//    //center: [lat, lng],
	//    center: [32.896531, 124.402956],
	//    zoom: 5,
	//    zoomControl: false
	//});

	//L.tileLayer('http://mt0.google.com/vt/lyrs=y&hl=kr&x={x}&y={y}&z={z}', {
	//    attribution: 'Map data &copy; Copyright Google Maps<a target="_blank" href="https://maps.google.com/maps?ll=24.53279,56.62833&amp;z=13&amp;t=m&amp;hl=ko-KR&amp;gl=US&amp;mapclient=apiv3"></a>' //화면 오른쪽 하단 attributors
	//}).addTo(mymap);

	mymap = L.map('map', {
		center: [28.0, 120.0],
		zoom: 5,
		layers: [
			L.tileLayer('https://{s}.basemaps.cartocdn.com/rastertiles/voyager_labels_under/{z}/{x}/{y}.png', {
				attribution: 'Map data &copy; Copyright Google Maps<a target="_blank" href="https://maps.google.com/maps?ll=24.53279,56.62833&amp;z=13&amp;t=m&amp;hl=ko-KR&amp;gl=US&amp;mapclient=apiv3"></a>' //화면 오른쪽 하단 attributors
			})
		]
	});
	var multiCoords1 = [
		[[37.3371, 126.4887], [37.3233, 126.4966], [37.3231, 126.4933], [37.3226, 126.4899], [37.3222, 126.4864], [37.3220, 126.4831], [37.3215, 126.4797], [37.3212, 126.4763],
		[37.3195, 126.4699], [37.3178, 126.4674], [37.3156, 126.4649], [37.3140, 126.4624], [37.3121, 126.4599], [37.3103, 126.4574], [37.3085, 126.4548], [37.3067, 126.4522], [37.3047, 126.4497], [37.3029, 126.4471], [37.3011, 126.4446], [37.2992, 126.4420], [37.2974, 126.4396], [37.2956, 126.4370], [37.2938, 126.4345], [37.2929, 126.4330],
		[37.2915, 126.4326], [37.2889, 126.4316], [37.2852, 126.4301], [37.2836, 126.4294], [37.2811, 126.4283], [37.2785, 126.4273], [37.2760, 126.4263], [37.2733, 126.4251], [37.2708, 126.4239], [37.2681, 126.4230], [37.2656, 126.4218], [37.2629, 126.4208], [37.2603, 126.4196], [37.2577, 126.4187], [37.2552, 126.4175],
		[37.2252, 126.4134], [37.2206, 126.4093], [37.2159, 126.4058], [37.2114, 126.4019], [37.2073, 126.3981], [37.2025, 126.3943], [37.1980, 126.3903], [37.1934, 126.3868], [37.1888, 126.3827],
		[37.1848, 126.3779], [37.1809, 126.3732], [37.1770, 126.3686], [37.1731, 126.3636], [37.1691, 126.3587], [37.1639, 126.3539], [37.1644, 126.3530], [37.1607, 126.3503], [37.1561, 126.3462], [37.1515, 126.3429], [37.1472, 126.3390], [37.1424, 126.3353], [37.1377, 126.3318],
		[37.1231, 126.3196], [37.0883, 126.2870], [37.0708, 126.2684], [37.0568, 126.2499], [37.0399, 126.2310], [37.0259, 126.2114], [37.0094, 126.1915], [36.9798, 126.1510], [36.9623, 126.1284], [36.9464, 126.1160], [36.9085, 126.0865], [36.8695, 126.0597], [36.8321, 126.0281], [36.7953, 126.0041], [36.7529, 125.9794], [36.7122, 125.9636], [36.6682, 125.9512], [36.6252, 125.9409], [36.5839, 125.9196],
		[36.5436, 125.8983], [36.5066, 125.8757], [36.4680, 125.8510], [36.4266, 125.8228], [36.3851, 125.7995], [36.3470, 125.7741], [36.3093, 125.7473], [36.2723, 125.7226], [36.2313, 125.6937], [36.1936, 125.6662], [36.1548, 125.6381],
		[36.1165, 125.6127], [36.0760, 125.5887], [36.0377, 125.5639], [35.9961, 125.5385], [35.9555, 125.5111], [35.9161, 125.4857], [35.8793, 125.4616], [35.8387, 125.4362], [35.8003, 125.4108], [35.7212, 125.3614], [35.2852, 125.0771], [35.0162, 124.8807],
		[34.9892, 124.8594], [34.7971, 124.8546], [34.7582, 124.8560], [34.5277, 124.8114], [34.4988, 124.8045], [34.3493, 124.7633], [34.2852, 124.7427], [34.0449, 124.5340], [34.0147, 124.5024], [33.2973, 124.3060], [33.2738, 124.2991], [33.1103, 124.2710], [33.0379, 124.2737], [32.9618, 124.2614], [32.8050, 124.2504], [32.7392, 124.2119], [32.5971, 124.1398], [32.2917, 124.0616], [32.0610, 123.9531], [31.9777, 123.9311], [31.7546, 123.8999], [31.5236, 123.8383],
		[31.5007, 123.8349], [31.1441, 123.8108], [30.7832, 123.7106], [30.6864, 123.6982], [30.4901, 123.6488], [30.3966, 123.6426], [29.3377, 123.2671], [29.3168, 123.2596], [29.2503, 123.2541], [29.1922, 123.2520], [29.1478, 123.2507], [29.1208, 123.2465], [28.8575, 123.1446], [28.6832, 122.9712], [28.6507, 122.9163], [28.6212, 122.8091], [28.5175, 122.7257], [28.4940, 122.7161], [28.1506, 122.2733],
		[27.9388, 122.0743], [27.8538, 122.0263], [27.8059, 122.0070], [27.6147, 121.8366], [27.4552, 121.5928], [26.6848, 121.0620], [26.4471, 120.9576], [25.4709, 120.3367], [25.4244, 120.3058], [25.2018, 120.0972], [24.6261, 119.4142], [23.8952, 118.6411], [23.4140, 118.0048], [23.3964, 117.9897],
		[23.3341, 117.8796], [23.2420, 117.7889], [22.8001, 117.1562], [22.7064, 116.9322], [22.7077, 116.8917], [22.5160, 116.3997], [22.4645, 116.1983], [21.9844, 115.1705], [21.9588, 115.0199],
		[21.9811, 114.8037], [21.9817, 114.6437], [22.0900, 114.4277], [22.1203, 114.2430], [22.2182, 114.1524], [22.2445, 114.1209], [22.3328, 114.0771], [22.3544, 114.0476], [22.3470, 113.9778], [22.3759, 113.9037], [22.4072, 113.8881], [22.4409, 113.8830], [22.4685, 113.8699], [22.4075, 113.8892], [22.3625, 113.9122], [22.3450, 113.9798], [22.3475, 114.0337], [22.3390, 114.0773], [22.2958, 114.0893], [22.3059, 114.0914]
		]
	];
	var plArray = [];
	for (var i = 0; i < multiCoords1.length; i++) {
		plArray.push(L.polyline(multiCoords1[i]).addTo(mymap));
	}

	var makerIconBig = L.icon({
		iconUrl: '../Images/btn_icn.png',
		iconSize: [50, 50] // size of the icon
	});
	var portIconBig = L.icon({
		iconUrl: '../Images/btn_hai.png',
		iconSize: [50, 50] // size of the icon
	});

	var maker_POD = L.marker([37.4289, 126.4706], { icon: makerIconBig }).addTo(mymap);
	var maker_POL = L.marker([22.3507, 114.0728], { icon: portIconBig }).addTo(mymap);

}
$(document).on('keyup', '#Pc_Input_Tracking', function (e) {
	if (e.keyCode == 13) {
		if (_fnToNull($("#Pc_Input_Tracking").val()) != "") {
			$("#Mo_Input_Tracking2").val($("#Pc_Input_Tracking").val().toUpperCase().trim());
			fngetLayerTracking($("#Pc_Input_Tracking").val().toUpperCase().trim(), "");
		} else {
			_fnAlertMsg("검색할 번호를 입력해주세요");
		}
	}
});

$(document).on('keyup', '#Pc_Input_Main_Tracking', function (e) {
	if (e.keyCode == 13) {
		if (_fnToNull($("#Pc_Input_Main_Tracking").val()) != "") {
			$("#Mo_Input_Tracking2").val($("#Pc_Input_Main_Tracking").val().toUpperCase().trim());
			fngetLayerTracking($("#Pc_Input_Main_Tracking").val().toUpperCase().trim(), "");
			$("body").addClass("layer_on");
		} else {
			_fnAlertMsg("검색할 번호를 입력해주세요");
		}
	}
});

$(document).on('click', '#Pc_btn_Tracking', function () {
	if (_fnToNull($("#Pc_Input_Tracking").val()) != "") {
		$("#Mo_Input_Tracking2").val($("#Pc_Input_Tracking").val().toUpperCase().trim());
		fngetLayerTracking($("#Pc_Input_Tracking").val().toUpperCase().trim(), "");
		$("body").addClass("layer_on");
	} else {
		_fnAlertMsg("검색할 번호를 입력해주세요");
	}
});

$(document).on('click', '#Pc_Main_Tracking', function () {
	if (_fnToNull($("#Pc_Input_Main_Tracking").val()) != "") {
		$("#Mo_Input_Tracking2").val($("#Pc_Input_Main_Tracking").val().toUpperCase().trim());
		fngetLayerTracking($("#Pc_Input_Main_Tracking").val().toUpperCase().trim(), "");
		$("body").addClass("layer_on");
	} else {
		_fnAlertMsg("검색할 번호를 입력해주세요");
	}
});
$(document).on('click', '#map_btn_Tracking', function () {
	if (_fnToNull($("#Mo_Input_Tracking2").val()) != "") {
		fngetLayerTracking($("#Mo_Input_Tracking2").val().toUpperCase().trim(), "");
	} else {
		_fnAlertMsg("검색할 번호를 입력해주세요");
	}
});

$(document).on('keyup', '#Mo_Input_Tracking2', function (e) {
	if (e.keyCode == 13) {
		if (_fnToNull($("#Mo_Input_Tracking2").val()) != "") {
			fngetLayerTracking($("#Mo_Input_Tracking2").val().toUpperCase().trim(), "");
			$("body").addClass("layer_on");
		} else {
			_fnAlertMsg("검색할 번호를 입력해주세요");
		}
	}
});

$(document).on('click', '#Mo_Main_Tracking', function () {
	if (_fnToNull($("#Pc_Input_Main_Tracking").val()) != "") {
		$("#Mo_Input_Tracking2").val($("#Pc_Input_Main_Tracking").val().toUpperCase().trim());
		fngetLayerTracking($("#Pc_Input_Main_Tracking").val().toUpperCase().trim(), "");
		$("body").addClass("layer_on");
	} else {
		_fnAlertMsg("검색할 번호를 입력해주세요");
	}
});

$(document).on('click', '#Mo_btn_Tracking', function () {
	//$('.layer_zone2').css('display', 'block');
	$("body").addClass("layer_on");
	if (_fnToNull($("#Mo_Input_Tracking").val()) != "") {
		$("#Mo_Input_Tracking2").val($("#Mo_Input_Tracking").val().toUpperCase().trim());
		fngetLayerTracking($("#Mo_Input_Tracking").val().toUpperCase().trim(), "");
	} else {
		_fnAlertMsg("검색할 번호를 입력해주세요");
	}
})

$(document).on('click', '#tracking-close', function () {
	$('.layer_zone2').css('display', 'none');
	$("body").removeClass("layer_on");
})

$(document).on('click', '.greeting', function () {
	$(".greeting").removeClass('active');
	$(this).addClass('active');
});

$(document).on('click', '#terms', function () {
	$('#layer_Policy_Terms').css('display', 'block');
	$("body").addClass("layer_on");
});
$(document).on('click', '#Policy', function () {
	$('#layer_Policy_Policy').css('display', 'block');
	$("body").addClass("layer_on");
});
$(document).on('click', '#enterlogin', function () {
	$('#login_pop').css('display', 'block');
	$("body").addClass("layer_on");
});
$(document).on('click', '#myboard', function () {
	location.href = "/MyBoard/MyBoard";
});

$(document).on('click', '#Tariff_Search', function () {
	$('#tariff_tbl').css('display', 'table');
	$('#Tariff_Nodata').css('display', 'none');
});


/*함수*/

function SearchTracking() {
	var trackingPC = $("#Pc_Input_Tracking").val();

	$(".layer_zone2").css("display", "block");
	$("body").addClass("layer_on");
	$('.tracking-table').animate({
		scrollTop: parseInt($(".last-sec").offset().top)
	}, 1500);
}

var _objTrkLayer;

function fngetLayerTracking(vMngtNo, vCntrNo) {
	try {
		_objTrkLayer = new Object();

		_objTrkLayer.OFFICE_CD = _Office_CD;
		if (isLayerChkBL(vMngtNo, vCntrNo)) {

			fnTrkLayerInit(); //초기화

			if (fnLayerChkTokenExpire()) {
				_objTrkLayer.HBL_NO = vMngtNo;
				_objTrkLayer.CNTR_NO = vCntrNo;
				fnLayerTrkList();
			}
			else {
				layerClose('#SVT_tracking_layer');
			}
		}
		else {
			layerClose('#L_delivery_pop');
			layerClose('#SVT_tracking_layer');
		}
	}
	catch (err) {
		console.log("[Error - fnfngetLayerTracking()]" + err.message);
	}
}

//화물추적 B/L 제출 되었는지 확인
function isLayerChkBL(vMngtNo, vCntrNo) {
	try {
		var isBoolean = true;
		var objJsonData = new Object();

		objJsonData.HBL_NO = vMngtNo;
		objJsonData.CNTR_NO = vCntrNo;
		objJsonData.OFFICE_CD = _Office_CD;

		$.ajax({
			type: "POST",
			url: "/Home/fnIsCheckBL",
			async: false,
			dataType: "json",
			data: { "vJsonData": _fnMakeJson(objJsonData) },
			success: function (result) {

				//받은 데이터 Y / N 체크
				if (JSON.parse(result).Result[0]["trxCode"] == "Y") {
					//엘비스 - 제출 여부 확인

					if (JSON.parse(result).Check[0]["CHKBL_YN"] == "Y") {
						_objTrkLayer.HBL_NO = JSON.parse(result).Check[0]["HBL_NO"];
						_objTrkLayer.MBL_NO = JSON.parse(result).Check[0]["MBL_NO"];
						_objTrkLayer.LINE_BKG_NO = JSON.parse(result).Check[0]["LINE_BKG_NO"];
						_objTrkLayer.REQ_SVC = JSON.parse(result).Check[0]["REQ_SVC"];
						_objTrkLayer.TOKEN = JSON.parse(result).Check[0]["TOKEN"];
						_objTrkLayer.EXPIREDDT = JSON.parse(result).Check[0]["EXPIREDDT"];
						_objTrkLayer.ID = JSON.parse(result).Check[0]["ID"];
						_objTrkLayer.PWD = JSON.parse(result).Check[0]["PWD"];

						isBoolean = true;
					}
					else if (JSON.parse(result).Check[0]["CHKBL_YN"] == "N") {
						_fnAlertMsg2("B/L 제출을 해주시기 바랍니다.");
						isBoolean = false;
					} else {
						_fnAlertMsg2("담당자에게 문의 하세요.");
						isBoolean = false;
					}
				}
				else if (JSON.parse(result).Result[0]["trxCode"] == "N") {
					_fnAlertMsg2("Tracking 정보가 없습니다");
					isBoolean = false;
				}
				else if (JSON.parse(result).Result[0]["trxCode"] == "E") {
					_fnAlertMsg2("B/L 정보가 없습니다");
					isBoolean = false;
				}
			}, error: function (xhr, status, error) {
				_fnAlertMsg2("담당자에게 문의 하세요.");
				console.log(error);
			}
		});

		return isBoolean;
	}
	catch (err) {
		console.log("[Error - isLayerChkBL()]" + err.message);
	}

}

function fnTrkLayerInit() {
	try {
		$("#layer_tracking_List").empty();
		//씨벤티지 누를때마다 초기화가 안되서 지웠다가 다시 그림
		$("#SVT_layer_Area").empty();
		var vHTML = "";
		vHTML += "<iframe src=\"\" id=\"SVT_layer_map\" class=\"map\" width=\"100%\" height=\"100%\"></iframe>";
		$("#SVT_layer_Area")[0].innerHTML = vHTML;
	}
	catch (err) {
		console.log("[Error - fnTrkInit]" + err.message);
	}
}


function fnLayerChkTokenExpire() {
	try {
		var vBoolean = true;
		var GetTokenTime;
		var nowTime;

		//토큰 만료 체크 확인
		$.ajax({
			url: "https://svmp.seavantage.com/api/v1/user/me",
			beforeSend: function (xhr) {
				xhr.setRequestHeader('Authorization', "Basic " + btoa(_objTrkLayer.ID + ":" + _objTrkLayer.PWD));
			},
			type: "GET",
			async: false,
			dataType: "json",
			success: function (result) {
				if (result.message == "OK") {
					GetTokenTime = new Date(_objTrkLayer.EXPIREDDT);
					nowTime = new Date();
					//현재 시간이 만료 시간을 지났을 경우 or 토큰 값이 null인 경우  갱신
					if (GetTokenTime.getTime() < nowTime.getTime() || _fnToNull(_objTrkLayer.TOKEN) == "") {
						vBoolean = false;
						vBoolean = fnSetSvtgAuthToken();
					}
					else {
						if (_objTrkLayer.TOKEN == result.response.authToken) {
							vBoolean = true;
						}
						// 만료는 안되었지만 토큰 값이 저장된 값과 다를 때 
						else {
							vBoolean = false;
							vBoolean = fnSetSvtgAuthToken();
						}
					}
				} else {
					vBoolean = false;
					_fnAlertMsg("담당자에게 문의하세요");
					console.log("[Error - fnChkLayerTokenExpire()]" + result.message);
				}
			}, error: function (xhr) {
				fnSetSvtgAuthToken();
				vBoolean = true;
			}
		});

		return vBoolean;
	}
	catch (err) {
		console.log("[Error - fnLayerChkTokenExpire()]" + err.message);
	}
}


function fnSetSvtgAuthToken() {
	try {
		var vBoolean = false;
		var objJsonData = new Object();
		objJsonData.SVTG_ID = _objTrkLayer.ID;
		objJsonData.SVTG_PWD = _objTrkLayer.PWD;

		$.ajax({
			url: "https://svmp.seavantage.com/api/v1/user/authToken",
			beforeSend: function (xhr) {
				xhr.setRequestHeader('Authorization', "Basic " + btoa(objJsonData.SVTG_ID + ":" + objJsonData.SVTG_PWD));
			},
			type: "GET",
			async: false,
			contentType: "application/json",
			success: function (result) {
				if (result.message == "OK") {
					_objTrkLayer.TOKEN = result.response.tokenId + "|" + result.response.expiredDt;
				} else {
					vBoolean = false;
					_fnAlertMsg("담당자에게 문의하세요");
					console.log("[Error - fnChkLayerTokenExpire()]" + result.message);
				}
			}, error: function (xhr) {
				if (JSON.parse(xhr.responseText).message == "UNAUTHORIZED") {
					console.log("[Error - fnChkLayerTokenExpire()]" + xhr);
					vBoolean = true;
				}
			}
		});
		var callObject = new Object();
		callObject.paramObj = _fnMakeJson(_objTrkLayer);


		$.ajax({
			type: "POST",
			url: "/Home/SetSvtgAuthToken ",
			async: false,
			dataType: "json",
			data: callObject,
			success: function (rtnVal) {
				if (rtnVal.Result[0]["trxCode"] == "Y") {
					_objTrkLayer.TOKEN = _objTrkLayer.TOKEN.split("|")[0];
					vBoolean = true;
				} else if (rtnVal.Result[0]["trxCode"] == "N") {
					_fnAlertMsg("토큰생성에 실패했습니다. 담당자에게 문의해주세요");
				}
			}, error: function (xhr) {
				console.log("시스템 사정으로 요청하신 작업을 처리할 수 없습니다.");
				console.log(xhr);
				return;
			}
		});
		return vBoolean;
	}
	catch (err) {
		console.log("[Error - fnGetTrackingParam()]" + err.message);
	}
}


function fnLayerTrkList() {
	try {

		layerPopup2('#SVT_tracking_layer');
		//$("#SVT_layer_map").attr("src", "https://svmp.seavantage.com/#/cargo/tracking?authToken=" + _objTrkLayer.TOKEN + "&mblNo=" + _objTrkLayer.LINE_BKG_NO + "&hiddenPanel=all");
		$("#SVT_layer_map").attr("src", "https://svmp.seavantage.com/#/cargo/tracking?authToken=" + _objTrkLayer.TOKEN + "&mblNo=" + _objTrkLayer.LINE_BKG_NO);

		$.ajax({
			type: "POST",
			url: "/Home/GetSeaTrackingList",
			async: true,
			dataType: "json",
			data: { "vJsonData": _fnMakeJson(_objTrkLayer) },
			success: function (result) {
				fnMakeLayerSeaTrkList(result);
	
			}, error: function (xhr, status, error) {
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
	}
	catch (err) {
		console.log("[Error - fnLayerTrkList(" + vREQ_SVC + ")]" + err.message);
	}
}


//씨벤티지 - 레이어 화물 추적 데이터 리스트
function fnMakeLayerSeaTrkList(vJsonData) {
	var chkLastSec = false;
	try {

		var vHtml = "";
		var vResult = "";

		if (JSON.parse(vJsonData).Result[0]["trxCode"] == "Y") {
			var vMain = JSON.parse(vJsonData).Table1;
			vResult = JSON.parse(vJsonData).TrackingList;

			$("#SVT_P_HBL_NO").empty();
			$("#SVT_P_CNTR_NO").empty();
			$("#SVT_P_STATUS").empty();
			$("#SVT_P_HBL_NO").text(_fnToNull(vResult[0].LINE_BKG_NO));
			$("#SVT_P_CNTR_NO").text(_fnToNull(vResult[0].CNTR_NO));
			$("#SVT_P_STATUS").text(_fnToNull(vMain[0].NOW_EVENT_NM));

			if (_fnToNull(vResult) != "") {
				$("#layer_tracking_List").empty();

				$(vResult).each(function (i) {
					if (_fnToNull(vMain[0].EX_IM_TYPE) == "E") {
						$("#ex_im_result").removeClass('import-result');
						$("#ex_im_result").addClass('export-result');

					if (_fnToNull(vResult[i].ACT_EVT_CD) == "Y") {
						vHtml += "	<div class='table-1 last-sec'>	";
					} else {
						vHtml += "	<div class='table-1'>	";
					}
					if (i < 4) {
						if (_fnToNull(vResult[i].ACT_EVT_CD) == "N") {
							vHtml += "        <div class='vertical-line-ing'></div>	";
						}
						else if (_fnToNull(vResult[i].ACT_EVT_CD) == "E") {
							if (chkLastSec) {
								vHtml += "        <div class='vertical-line-ing'></div>	";
							} else {
								vHtml += "        <div class='vertical-line'></div>	";
							}
						} else {
							vHtml += "        <div class='vertical-line-ing'></div>	";
							chkLastSec = true;
						}
					}

					if (_fnToNull(vResult[i].ACT_EVT_CD) != "Y") {
						vHtml += "        <div class='table-title'></div>	";
					} else {
						vHtml += "        <div class='table-title tracking-ing'></div>	";
					}
						if (_fnToNull(vResult[i].ACT_EVT_CD) == "N") {
							vHtml += "        <div class='graph yet'>	";
						} else if (_fnToNull(vResult[i].ACT_EVT_CD) == "E") {
							if (chkLastSec) {
								vHtml += "        <div class='graph yet'>	";
							} else {
								vHtml += "        <div class='graph complete'>	";
							}

						} else {
							vHtml += "        <div class='graph graph-ing'>	";
						}

					if (_fnToNull(vResult[i].ACT_EVT_CD) != "Y") {
						vHtml += "            <div class='graph-title'>" + _fnToNull(vResult[i].EVENT_NM) + "</div>	";
					} else {
						vHtml += "            <div class='graph-title-ing'>" + _fnToNull(vResult[i].EVENT_NM) + "</div>	";
					}
					vHtml += "            <div class='graph-info'>	";
					vHtml += "                <div class='location-info'>	";
					vHtml += "                    <p class='g-location-title'>Location</p>	";
					vHtml += "                    <p class='g-location-info'>" + _fnToNull(vResult[i].ACT_LOC_NM) + "</p>	";
					vHtml += "                </div>	";
					vHtml += "                <div class='date-info'>	";
					vHtml += "                    <p class='g-date-title'>Date and Time</p>	";
					vHtml += "                    <p class='g-date-info'>" + _fnToNull(_fnDateFormat(vResult[i].ACT_YMD)) + " " + _fnToNull(_fnDateFormat(vResult[i].ACT_HM)) + "</p>	";
					vHtml += "                </div>	";
					vHtml += "            </div>	";
					vHtml += "        </div>	";
					vHtml += "    </div>	";
				} else {
					$("#ex_im_result").removeClass('export-result');
					$("#ex_im_result").addClass('import-result');
					if (_fnToNull(vResult[i].ACT_EVT_CD) == "Y") {
						vHtml += "	<div class='table-1 last-sec'>	";
					} else {
						vHtml += "	<div class='table-1'>	";
					}
					if (i < 5) {
						if (_fnToNull(vResult[i].ACT_EVT_CD) == "N") {
							vHtml += "        <div class='vertical-line-ing'></div>	";
						}
						else if (_fnToNull(vResult[i].ACT_EVT_CD) == "E") {
							if (chkLastSec) {
								vHtml += "        <div class='vertical-line-ing'></div>	";
							} else {
								vHtml += "        <div class='vertical-line'></div>	";
							}
						} else {
							vHtml += "        <div class='vertical-line-ing'></div>	";
							chkLastSec = true;
						}
					}

					if (_fnToNull(vResult[i].ACT_EVT_CD) != "Y") {
						vHtml += "        <div class='table-title'></div>	";
					} else {
						vHtml += "        <div class='table-title tracking-ing'></div>	";
					}
					if (_fnToNull(vResult[i].ACT_EVT_CD) == "N") {
						vHtml += "        <div class='graph yet'>	";
					} else if (_fnToNull(vResult[i].ACT_EVT_CD) == "E") {
						if (chkLastSec) {
							vHtml += "        <div class='graph yet'>	";
						} else {
							vHtml += "        <div class='graph complete'>	";
						}
							
					} else {
						vHtml += "        <div class='graph graph-ing'>	";
					}

					if (_fnToNull(vResult[i].ACT_EVT_CD) != "Y") {
						vHtml += "            <div class='graph-title'>" + _fnToNull(vResult[i].EVENT_NM) + "</div>	";
					} else {
						vHtml += "            <div class='graph-title-ing'>" + _fnToNull(vResult[i].EVENT_NM) + "</div>	";
					}
					vHtml += "            <div class='graph-info'>	";
					vHtml += "                <div class='location-info'>	";
					vHtml += "                    <p class='g-location-title'>Location</p>	";
					vHtml += "                    <p class='g-location-info'>" + _fnToNull(vResult[i].ACT_LOC_NM) + "</p>	";
					vHtml += "                </div>	";
					vHtml += "                <div class='date-info'>	";
					vHtml += "                    <p class='g-date-title'>Date and Time</p>	";
					vHtml += "                    <p class='g-date-info'>" + _fnToNull(_fnDateFormat(vResult[i].ACT_YMD)) + " " + _fnToNull(_fnDateFormat(vResult[i].ACT_HM)) + "</p>	";
					vHtml += "                </div>	";
					vHtml += "            </div>	";
					vHtml += "        </div>	";
					vHtml += "    </div>	";
				}

				});
				//alert($(".last-sec").offset().top);
				$("#layer_tracking_List").append(vHtml);
				console.log(parseInt($(".last-sec").offset().top));
				$('#layer_tracking_List').animate({
					scrollTop: parseInt($(".last-sec").offset().top - 380)
				}, 1500);

			} else {
				$("#layer_tracking_List").hide();
			}
		} else {
			$("#layer_tracking_List").hide();
		}
	}
	catch (err) {
		console.log("[Error - fnMakeLayerSeaTrkList]" + err.message);
	}
}
