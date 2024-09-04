////////////////////전역 변수//////////////////////////
var _vREQ_SVC = "SEA";
////////////////////jquery event///////////////////////
$(function () {    

    fnMakeResult();

    $("#input_ETD").val(_fnPlusDate(0)); //ETD	

    if ($('.scrollbar_bkfile').length > 0) {
        $('.scrollbar_bkfile').slimScroll({
            height: '72px',
            width: '100%',
            alwaysVisible: false,
            railVisible: false,
        })
    }
});

//Departure 클릭 이벤트
$(document).on("click", "#input_Departure", function () {

    if ($(this).val().length == 0) {

        if (_vREQ_SVC == "SEA") {
            $("#select_SEA_pop01").hide();
            $("#select_SEA_pop02").hide();
            $("#select_FERRY_pop01").hide();
            $("#select_FERRY_pop02").hide();
            $("#select_AIR_pop01").hide();
            $("#select_AIR_pop02").hide();
            selectPopOpen("#select_SEA_pop01");
        }
        else if (_vREQ_SVC == "FERRY") {
            $("#select_SEA_pop01").hide();
            $("#select_SEA_pop02").hide();
            $("#select_FERRY_pop01").hide();
            $("#select_FERRY_pop02").hide();
            $("#select_AIR_pop01").hide();
            $("#select_AIR_pop02").hide();
            selectPopOpen("#select_FERRY_pop01");
        }
        else if (_vREQ_SVC == "AIR") {
            $("#select_SEA_pop01").hide();
            $("#select_SEA_pop02").hide();
            $("#select_FERRY_pop01").hide();
            $("#select_FERRY_pop02").hide();
            $("#select_AIR_pop01").hide();
            $("#select_AIR_pop02").hide();
            selectPopOpen("#select_AIR_pop01");
        }
    }
});

//Departure 클릭 이벤트
$(document).on("click", "#input_Arrival", function () {

    if ($(this).val().length == 0) {

        if (_vREQ_SVC == "SEA") {
            $("#select_SEA_pop01").hide();
            $("#select_SEA_pop02").hide();
            $("#select_FERRY_pop01").hide();
            $("#select_FERRY_pop02").hide();
            $("#select_AIR_pop01").hide();
            $("#select_AIR_pop02").hide();
            selectPopOpen("#select_SEA_pop02");
        }
        else if (_vREQ_SVC == "FERRY") {
            $("#select_SEA_pop01").hide();
            $("#select_SEA_pop02").hide();
            $("#select_FERRY_pop01").hide();
            $("#select_FERRY_pop02").hide();
            $("#select_AIR_pop01").hide();
            $("#select_AIR_pop02").hide();
            selectPopOpen("#select_FERRY_pop02");
        }
        else if (_vREQ_SVC == "AIR") {
            $("#select_SEA_pop01").hide();
            $("#select_SEA_pop02").hide();
            $("#select_FERRY_pop01").hide();
            $("#select_FERRY_pop02").hide();
            $("#select_AIR_pop01").hide();
            $("#select_AIR_pop02").hide();
            selectPopOpen("#select_AIR_pop02");
        }

    }
});

//퀵 Code 데이터 - SEA POL
$(document).on("click", "#quick_SEA_POLCD button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Departure").val(vSplit[0]);
    $("#input_POL").val(vSplit[1]);
    $("#select_SEA_pop01").hide();
    if (_fnCheckSamePort(vSplit[1], "", "POL", "Q", "select_SEA_pop01")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }

    selectPopOpen("#select_SEA_pop02");
});

//퀵 Code 데이터 - SEA POL
$(document).on("click", "#quick_SEA_POLCD2 button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Departure").val(vSplit[0]);
    $("#input_POL").val(vSplit[1]);
    $("#select_SEA_pop01").hide();
    if (_fnCheckSamePort(vSplit[1], "", "POL", "Q", "select_SEA_pop01")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }

    selectPopOpen("#select_SEA_pop02");
});

//퀵 Code 데이터 - SEA POD
$(document).on("click", "#quick_SEA_PODCD button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Arrival").val(vSplit[0]);
    $("#input_POD").val(vSplit[1]);
    $("#select_SEA_pop02").hide();
    if (_fnCheckSamePort(vSplit[1], "", "POD", "Q", "select_SEA_pop02")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }
});

//퀵 Code 데이터 - SEA POD
$(document).on("click", "#quick_SEA_PODCD2 button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Arrival").val(vSplit[0]);
    $("#input_POD").val(vSplit[1]);
    $("#select_SEA_pop02").hide();

    if (_fnCheckSamePort(vSplit[1], "", "POD", "Q", "select_SEA_pop02")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }
});

//FERRY
//퀵 Code 데이터 - FERRY POL
$(document).on("click", "#quick_FERRY_POLCD button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Departure").val(vSplit[0]);
    $("#input_POL").val(vSplit[1]);
    $("#select_FERRY_pop01").hide();

    if (_fnCheckSamePort(vSplit[1], "", "POL", "Q", "select_FERRY_pop01")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }


    selectPopOpen("#select_FERRY_pop02");
});

//퀵 Code 데이터 - FERRY POL
$(document).on("click", "#quick_FERRY_POLCD2 button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Departure").val(vSplit[0]);
    $("#input_POL").val(vSplit[1]);
    $("#select_FERRY_pop01").hide();

    if (_fnCheckSamePort(vSplit[1], "", "POL", "Q", "select_FERRY_pop01")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }

    selectPopOpen("#select_FERRY_pop02");
});

//퀵 Code 데이터 - FERRY POD
$(document).on("click", "#quick_FERRY_PODCD button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Arrival").val(vSplit[0]);
    $("#input_POD").val(vSplit[1]);
    $("#select_FERRY_pop02").hide();

    if (_fnCheckSamePort(vSplit[1], "", "POD", "Q", "select_FERRY_pop02")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }
});

//퀵 Code 데이터 - FERRY POD
$(document).on("click", "#quick_FERRY_PODCD2 button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Arrival").val(vSplit[0]);
    $("#input_POD").val(vSplit[1]);
    $("#select_FERRY_pop02").hide();

    if (_fnCheckSamePort(vSplit[1], "", "POD", "Q", "select_FERRY_pop02")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }
});
//FERRY

//퀵 Code 데이터 - AIR POL
$(document).on("click", "#quick_AIR_POLCD button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Departure").val(vSplit[0]);
    $("#input_POL").val(vSplit[1]);
    $("#select_AIR_pop01").hide();

    if (_fnCheckSamePort(vSplit[1], "", "POL", "Q", "select_AIR_pop01")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }

    selectPopOpen("#select_AIR_pop02");
});

//퀵 Code 데이터 - AIR POL
$(document).on("click", "#quick_AIR_POLCD2 button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Departure").val(vSplit[0]);
    $("#input_POL").val(vSplit[1]);
    $("#select_AIR_pop01").hide();

    if (_fnCheckSamePort(vSplit[1], "", "POL", "Q", "select_AIR_pop01")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }

    selectPopOpen("#select_AIR_pop02");
});

//퀵 Code 데이터 - AIR POD
$(document).on("click", "#quick_AIR_PODCD button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Arrival").val(vSplit[0]);
    $("#input_POD").val(vSplit[1]);
    $("#select_AIR_pop02").hide();

    if (_fnCheckSamePort(vSplit[1], "", "POD", "Q", "select_AIR_pop02")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }
});

//퀵 Code 데이터 - AIR POD
$(document).on("click", "#quick_AIR_PODCD2 button", function () {

    //split 해서 네이밍 , POL_CD 넣기
    var vValue = $(this).val();
    var vSplit = vValue.split(";");

    $("#input_Arrival").val(vSplit[0]);
    $("#input_POD").val(vSplit[1]);
    $("#select_AIR_pop02").hide();

    if (_fnCheckSamePort(vSplit[1], "", "POD", "Q", "select_AIR_pop02")) {
        //X박스 만들기
        $(this).closest(".int_box").addClass("has_del");
        $(this).closest(".int_box").find(".delete").toggle(Boolean($(this).val()));
    }
});
////////////////////////function///////////////////////

/////////////////function MakeList/////////////////////
function fnMakeResult() {
	try {
        var vHTML = "";

        vHTML += "   <tr> ";
        vHTML += "   	<td><img src=\"/Images/logo/SNKO.png\" alt=\"\"></td> ";
        vHTML += "   	<td>SNKO HEUNG-A XIAMEN<br />3 Days, Direct</td> ";
        vHTML += "   	<td> " + _fnPlusDate(0, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(0, "-").replace(/-/gi, ""))+")<br> KRPUS   	</td> ";
        vHTML += "   	<td> " + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) +")<br> CNSHA   	</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-7, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-7, "-").replace(/-/gi, "")) + ")<br> 18:00   	</td> ";
        vHTML += "   	<td class=\"mobile_layout\" colspan=\"6\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s2 block\">SNKO HEUNG-A XIAMEN</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Carrier</th> ";
        vHTML += "   							<td><img src=\"/Images/logo/SNKO.png\" alt=\"\"></td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td>" + _fnPlusDate(0, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(0, "-").replace(/-/gi, "")) +") KRPUS</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td>" + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) +") CNSHA</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Doc Closing</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-7, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-7, "-").replace(/-/gi, "")) +") 18:00</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/time</th> ";
        vHTML += "   							<td>3 Days</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/S</th> ";
        vHTML += "   							<td>T/S</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr> ";
        vHTML += "   	<td><img src=\"/Images/logo/HASL.png\" alt=\"\"></td> ";
        vHTML += "   	<td> HASL HONOR VOYAGER<br />2 Days, Direct</td> ";
        vHTML += "   	<td> " + _fnPlusDate(1, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(1, "-").replace(/-/gi, "")) +")<br> KRPUS   	</td> ";
        vHTML += "   	<td> " + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) +")<br> JPOSA   	</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-6, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-6, "-").replace(/-/gi, "")) + ")<br> 12:00   	</td> ";
        vHTML += "   	<td class=\"mobile_layout\" colspan=\"6\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s2 block\">HASL HONOR VOYAGER</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Carrier</th> ";
        vHTML += "   							<td><img src=\"/Images/logo/HASL.png\" alt=\"\"></td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td>" + _fnPlusDate(1, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(1, "-").replace(/-/gi, "")) + ") KRPUS</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td>" + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) + ") JPOSA</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Doc Closing</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-6, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-6, "-").replace(/-/gi, "")) + ") 12:00</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/time</th> ";
        vHTML += "   							<td>2 Days</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/S</th> ";
        vHTML += "   							<td>T/S</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr> ";
        vHTML += "   	<td><img src=\"/Images/logo/CKCO.png\" alt=\"\"></td> ";
        vHTML += "   	<td> CKCO SKY TIARA<br />3 Days, Direct</td> ";
        vHTML += "   	<td> " + _fnPlusDate(2, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(2, "-").replace(/-/gi, "")) + ")<br> KRPUS</td> ";
        vHTML += "   	<td> " + _fnPlusDate(5, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(5, "-").replace(/-/gi, "")) + ")<br> CNSHK</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-5, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-5, "-").replace(/-/gi, "")) + ")<br> 09:00</td> ";
        vHTML += "   	<td class=\"mobile_layout\" colspan=\"6\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s2 block\">CKCO SKY TIARA</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Carrier</th> ";
        vHTML += "   							<td><img src=\"/Images/logo/CKCO.png\" alt=\"\"></td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td> " + _fnPlusDate(2, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(2, "-").replace(/-/gi, "")) + ") KRPUS</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td> " + _fnPlusDate(5, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(5, "-").replace(/-/gi, "")) + ") CNSHK</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Doc Closing</th> ";
        vHTML += "   							<td> " + _fnPlusDate(-5, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-5, "-").replace(/-/gi, "")) + ") 09:00</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/time</th> ";
        vHTML += "   							<td>3 Days</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/S</th> ";
        vHTML += "   							<td>T/S</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr> ";
        vHTML += "   	<td><img src=\"/Images/logo/HDMU.png\" alt=\"\"></td> ";
        vHTML += "   	<td> HDMU SM JAKARTA<br />12 Days, Direct</td> ";
        vHTML += "   	<td> " + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) + ")<br> KRPUS</td> ";
        vHTML += "   	<td> " + _fnPlusDate(15, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(15, "-").replace(/-/gi, "")) + ")<br> BDCGP</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-4, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-4, "-").replace(/-/gi, "")) + ") 12:00";
        vHTML += "   	<td class=\"mobile_layout\" colspan=\"6\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s2 block\">HDMU SM JAKARTA</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Carrier</th> ";
        vHTML += "   							<td><img src=\"/Images/logo/HDMU.png\" alt=\"\"></td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td> " + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) + ") KRPUS</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td> " + _fnPlusDate(15, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(15, "-").replace(/-/gi, "")) + ") BDCGP</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Doc Closing</th> ";
        vHTML += "   							<td> " + _fnPlusDate(-4, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-4, "-").replace(/-/gi, "")) + ") 12:00</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/time</th> ";
        vHTML += "   							<td>12 Days</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/S</th> ";
        vHTML += "   							<td>T/S</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr> ";
        vHTML += "   	<td><img src=\"/Images/logo/HASL.png\" alt=\"\"></td> ";
        vHTML += "   	<td> HASL TO BE NOMINATED<br />11 Days, Direct</td> ";
        vHTML += "   	<td> " + _fnPlusDate(4, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(4, "-").replace(/-/gi, "")) + ")<br> KRPUS</td> ";
        vHTML += "   	<td> " + _fnPlusDate(15, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(15, "-").replace(/-/gi, "")) + ")<br> IDJKT</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-3, "-").replace(/-/gi, "")) + ")<br> 21:00</td> ";
        vHTML += "   	<td class=\"mobile_layout\" colspan=\"6\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s2 block\">HASL TO BE NOMINATED</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Carrier</th> ";
        vHTML += "   							<td><img src=\"/Images/logo/HASL.png\" alt=\"\"></td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td> " + _fnPlusDate(4, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(4, "-").replace(/-/gi, "")) + ") KRPUS</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td>" + _fnPlusDate(15, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(15, "-").replace(/-/gi, "")) + ") IDJKT</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Doc Closing</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-3, "-").replace(/-/gi, "")) + ") 21:00</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/time</th> ";
        vHTML += "   							<td>11 Days</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/S</th> ";
        vHTML += "   							<td>T/S</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr> ";
        vHTML += "   	<td><img src=\"/Images/logo/ONEY.png\" alt=\"\"></td> ";
        vHTML += "   	<td> ONEY VANTAGE<br />3 Days, Direct</td> ";
        vHTML += "   	<td> " + _fnPlusDate(5, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(5, "-").replace(/-/gi, "")) + ")<br> KRPUS</td> ";
        vHTML += "   	<td> " + _fnPlusDate(8, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(8, "-").replace(/-/gi, "")) + ")<br> JPYOK</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-2, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-2, "-").replace(/-/gi, "")) + ")<br> 12:00</td> ";
        vHTML += "   	<td class=\"mobile_layout\" colspan=\"6\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s2 block\">ONEY VANTAGE</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Carrier</th> ";
        vHTML += "   							<td><img src=\"/Images/logo/ONEY.png\" alt=\"\"></td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td> " + _fnPlusDate(5, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(5, "-").replace(/-/gi, "")) + ") KRPUS</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td> " + _fnPlusDate(8, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(8, "-").replace(/-/gi, "")) + ") JPYOK</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Doc Closing</th> ";
        vHTML += "   							<td> " + _fnPlusDate(-2, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-2, "-").replace(/-/gi, "")) + ") 12:00</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/time</th> ";
        vHTML += "   							<td>3 Days</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/S</th> ";
        vHTML += "   							<td>T/S</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr> ";
        vHTML += "   	<td><img src=\"/Images/logo/KMTC.png\" alt=\"\"></td> ";
        vHTML += "   	<td> KMTC SKY PRIDE<br />3 Days, Direct</td> ";
        vHTML += "   	<td> " + _fnPlusDate(6, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(6, "-").replace(/-/gi, "")) + ")<br> KRPUS</td> ";
        vHTML += "   	<td> " + _fnPlusDate(9, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(9, "-").replace(/-/gi, "")) + ")<br> CNNGB</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-1, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-1, "-").replace(/-/gi, "")) + ")<br> 22:00</td> ";
        vHTML += "   	<td class=\"mobile_layout\" colspan=\"6\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s2 block\">KMTC SKY PRIDE</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Carrier</th> ";
        vHTML += "   							<td><img src=\"/Images/logo/ONEY.png\" alt=\"\"></td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td> " + _fnPlusDate(6, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(6, "-").replace(/-/gi, "")) + ") KRPUS</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td> " + _fnPlusDate(9, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(9, "-").replace(/-/gi, "")) + ") CNNGB</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Doc Closing</th> ";
        vHTML += "   							<td> " + _fnPlusDate(-1, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-1, "-").replace(/-/gi, "")) + ") 22:00</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/time</th> ";
        vHTML += "   							<td>3 Days</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/S</th> ";
        vHTML += "   							<td>T/S</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr> ";
        vHTML += "   	<td><img src=\"/Images/logo/ONEY.png\" alt=\"\"></td> ";
        vHTML += "   	<td> ONEY CONTI CONTESSA<br />6 Days, Direct</td> ";
        vHTML += "   	<td> " + _fnPlusDate(7, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(7, "-").replace(/-/gi, "")) + ")<br> KRPUS</td> ";
        vHTML += "   	<td> " + _fnPlusDate(13, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(13, "-").replace(/-/gi, "")) + ")<br> CNSHK</td> ";
        vHTML += "   	<td> " + _fnPlusDate(0, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(0, "-").replace(/-/gi, "")) + ")<br> 12:00</td> ";
        vHTML += "   	<td class=\"mobile_layout\" colspan=\"6\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s2 block\">ONEY CONTI CONTESSA</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Carrier</th> ";
        vHTML += "   							<td><img src=\"/Images/logo/ONEY.png\" alt=\"\"></td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td> " + _fnPlusDate(7, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(7, "-").replace(/-/gi, "")) + ") KRPUS</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td> " + _fnPlusDate(13, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(13, "-").replace(/-/gi, "")) + ") CNSHK</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Doc Closing</th> ";
        vHTML += "   							<td> " + _fnPlusDate(0, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(0, "-").replace(/-/gi, "")) + ") 12:00</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/time</th> ";
        vHTML += "   							<td>6 Days</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>T/S</th> ";
        vHTML += "   							<td>T/S</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";

        $("#BK_Result_AREA")[0].innerHTML = vHTML;
	}
	catch (err) {
		console.log("[Error - fnMakeResult]" + err.message);
	}
}

////////////////////////API////////////////////////////

$(document).on("click", "#btn_BK_Search", function () {

    $('#BK_Result_AREA').show();
    $('#NoData_BK').hide();
});

$(document).on("click", ".booking_write .list_type1 tr th", function () {
    if (_fnToNull($(this).find("button").attr("id")) != "") {
        if ($(this).find("button").hasClass("desc")) {
            $(this).find("button").removeClass("desc");
            _Sort_Order = "ASC";
            _Sort_ID = _fnToNull($(this).find("button").attr("id"));
        } else {
            $(this).find("button").addClass("desc");
            _Sort_Order = "DESC";
            _Sort_ID = _fnToNull($(this).find("button").attr("id"));
        }
    }
});


$(document).on('click', '#BK_Result_AREA > tr', function () {
    $(this).addClass('hold');
})