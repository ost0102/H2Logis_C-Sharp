﻿////////////////////전역 변수//////////////////////////

////////////////////jquery event///////////////////////
$(function () {
	fnSetVehicleDiv();
	fnSearchVehicle();
});

//차량 제원 구분 값 변경 시 이벤트
$(document).on("change", "#select_vehicle_div", function () {
	fnSearchVehicle();
});

////////////////////////function///////////////////////
//전체(시.도) 데이터 가져오기
function fnSetVehicleDiv() {	

	$.ajax({
		type: "POST",
		url: "/LogisticsTools/fnSetVehicleDiv",
		async: true,
		cache: false,
		dataType: "Json",		
		success: function (result) {
			fnMakeVehicleDiv(result);
		}, error: function (xhr) {
			alert("시스템 사정으로 요청하신 작업을 처리할 수 없습니다.");
			console.log(xhr);
		}
	});
}

//차량제원 검색
function fnSearchVehicle()
{
	var objJsonData = new Object();
	objJsonData.CAR_DIV_CODE = $("#select_vehicle_div").find('option:selected').val();

	$.ajax({
		type: "POST",
		url: "/LogisticsTools/fnSearchVehicle",
		async: true,
		cache: false,
		dataType: "Json",
		data: { "vJsonData": _fnMakeJson(objJsonData) },
		success: function (result) {
			fnMakeVehicle(result);
		}, error: function (xhr) {
			alert("시스템 사정으로 요청하신 작업을 처리할 수 없습니다.");
			console.log(xhr);
		}
	});
}
/////////////////function MakeList/////////////////////
//차량 제원 - Select 구분 만들기
function fnMakeVehicleDiv(vJsonData) {

	var vHTML = "";
	vResult = JSON.parse(vJsonData).Table1;

	if (JSON.parse(vJsonData).Result[0]["trxCode"] == "Y") {
		vHTML += "<option value=\"\" selected>전체</option>";

		var vCAR_DIV = "";

		$.each(vResult, function (i) {
			vHTML += "<option value=\"" + _fnToNull(vResult[i]["CAR_DIV_CODE"]) + "\">" + _fnToNull(vResult[i]["CAR_DIV"]) + "</option>";
			vCAR_DIV = "";
		});
	}
	else if (JSON.parse(vJsonData).Result[0]["trxCode"] == "N") {
		//데이터가 없습니다.
		vHTML += "<option value=\"\">전체</option>";
	}
	else if (JSON.parse(vJsonData).Result[0]["trxCode"] == "E") {
		console.log("[fnMakeVehicleDiv]Error : " + JSON.parse(vJsonData).Result[0]["trxMsg"]);
	}

	$("#select_vehicle_div")[0].innerHTML = vHTML;
}

//차량 제원 - 결과 만들기
function fnMakeVehicle(vJsonData) {
	var vHTML = "";
	vResult = JSON.parse(vJsonData).Table1;

	if (JSON.parse(vJsonData).Result[0]["trxCode"] == "Y") {

		$.each(vResult, function (i) {

			vHTML += "   <div class=\"card\"> ";
			vHTML += "   	<div class=\"vehicle_box\"> ";
			//이미지가 있는지 없는지 확인
			if (_fnToNull(vResult[i]["IMG_PATH"]) != "") {
				vHTML += "   	<div class=\"vehicle_img\"> ";
				vHTML += "   		<img src=\"" + _fnToNull(vResult[i]["IMG_PATH"]) + "/" + _fnToNull(vResult[i]["REPLACE_IMG_NAME"]) + "\"> "; //이미지
				vHTML += "   	</div> ";
			} else {
				vHTML += "   	<div class=\"card_img no_image\"> ";
				vHTML += "   		<span>No Image</span> "; //이미지
				vHTML += "   	</div> ";
			}
			vHTML += "			<div class='vehicle_info'>";
			vHTML += "   			<h3 class='vehicle_tit'>" + _fnToNull(vResult[i]["CAR_NAME"]) + "</h3> ";			
			vHTML += "				<div class='vehicle_detail_box'>";
			vHTML += "					<div class='vehicle_detail'>";
			vHTML += "						<p class='vdetail_tit'>약칭</p>";
			vHTML += "						<p class='vdetail_con'>" + _fnToNull(vResult[i]["SHORTHAND"]) + "</p>";
			vHTML += "					</div>";
			vHTML += "					<div class='vehicle_detail'>";
			vHTML += "						<p class='vdetail_tit'>적재함 길이</p>";
			vHTML += "						<p class='vdetail_con'>" + _fnToNull(vResult[i]["CAR_WIDTH"]) + "</p>";
			vHTML += "					</div>";
			vHTML += "					<div class='vehicle_detail'>";
			vHTML += "						<p class='vdetail_tit'>탑높이</p>";
			vHTML += "						<p class='vdetail_con'>" + _fnToNull(vResult[i]["TOP_HEIGHT"]) + "</p>";
			vHTML += "					</div>";
			vHTML += "					<div class='vehicle_detail'>";
			vHTML += "						<p class='vdetail_tit'>바닥높이</p>";
			vHTML += "						<p class='vdetail_con'>" + _fnToNull(vResult[i]["BOTTOM_HEIGHT"]) + "</p>";
			vHTML += "					</div>";
			vHTML += "					<div class='vehicle_detail'>";
			vHTML += "						<p class='vdetail_tit'>적재함 넓이</p>";
			vHTML += "						<p class='vdetail_con'>" + _fnToNull(vResult[i]["CAR_AREA"]) + "</p>";
			vHTML += "					</div>";
			vHTML += "					<div class='vehicle_detail'>";
			vHTML += "						<p class='vdetail_tit'>적재중량</p>";
			vHTML += "						<p class='vdetail_con'>" + _fnToNull(vResult[i]["CAR_WEIGHT"]) + "</p>";
			vHTML += "					</div>";
			vHTML += "					<div class='vehicle_detail'>";
			vHTML += "						<p class='vdetail_tit'>적재부피</p>";
			vHTML += "						<p class='vdetail_con'>" + _fnToNull(vResult[i]["CAR_CBM"]) + "</p>";
			vHTML += "					</div>";
			vHTML += "					<div class='vehicle_detail'>";
			vHTML += "						<p class='vdetail_tit'>차량 총 높이</p>";
			vHTML += "						<p class='vdetail_con'>" + _fnToNull(vResult[i]["TOTAL_HEIGHT"]) + "</p>";
			vHTML += "					</div>";
			vHTML += "					<div class='vehicle_detail'>";
			vHTML += "						<p class='vdetail_tit'>특이사항</p>";
			vHTML += "						<p class='vdetail_con'>" + _fnToNull(vResult[i]["RMK"]) + "</p>";
			vHTML += "					</div>";
			vHTML += "				</div>";
			vHTML += "			</div>";
			vHTML += "		</div>";
			vHTML += "		</div>";

		});
	}
	else if (JSON.parse(vJsonData).Result[0]["trxCode"] == "N") {
		//alert("데이터가 없습니다.");
	}
	else if (JSON.parse(vJsonData).Result[0]["trxCode"] == "E") {
		console.log("[fnMakeVehicle]Error : " + JSON.parse(vJsonData).Result[0]["trxMsg"]);
	}

	$("#Vehicle_Result")[0].innerHTML = vHTML;
} 
////////////////////////API////////////////////////////

