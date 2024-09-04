////////////////////전역 변수//////////////////////////
var _vREQ_SVC = "SEA";
////////////////////jquery event///////////////////////
$(function () {   

    $("#input_ETD").val(_fnPlusDate(-14)); //ETD	
    $("#input_ETA").val(_fnPlusDate(7)); //ETD	        
    
});

//Departure 클릭 이벤트
$(document).on("click", "#input_Departure", function () {

    if ($(this).val().length == 0) {

        if (_vREQ_SVC == "SEA") {
            $("#select_SEA_pop01").hide();
            $("#select_SEA_pop02").hide();
            $("#select_AIR_pop01").hide();
            $("#select_AIR_pop02").hide();
            selectPopOpen("#select_SEA_pop01");
        }
        else if (_vREQ_SVC == "AIR") {
            $("#select_SEA_pop01").hide();
            $("#select_SEA_pop02").hide();
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
            $("#select_AIR_pop01").hide();
            $("#select_AIR_pop02").hide();
            selectPopOpen("#select_SEA_pop02");
        }
        else if (_vREQ_SVC == "AIR") {
            $("#select_SEA_pop01").hide();
            $("#select_SEA_pop02").hide();
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
    $("#select_AIR_pop02").hide();

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

//invoice 검색 버튼 이벤트
$(document).on("click", "button[name='Search_invoice']", function () {
    fnMakeResult();
});

////////////////////////function///////////////////////
/////////////////function MakeList/////////////////////
function fnMakeResult() {
    try {
        var vHTML = "";

        vHTML += "   <tr class=\"row\" data-row=\"row_1\"> ";
        vHTML += "   	<td><span class=\"trade import ship\">Export</span></td> ";
        vHTML += "   	<td>SAMPLE20230328001</td> ";
        vHTML += "   	<td>KRW 320,000</td> ";
        vHTML += "   	<td>BUSAN, KOREA<br>" + _fnPlusDate(0, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(0, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>SHANGHAI, CHINA<br>" + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>" + _fnPlusDate(-7, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-7, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>" + _fnPlusDate(-7, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-7, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>SAMPLEINV001</td> ";
        vHTML += "   	<td class=\"btns_w1\"> ";
        vHTML += "   		<div class=\"btn_padding\"> ";
        vHTML += "   			<a href=\"javascript:void(0)\" class=\"btn_type1 skyblue\" onclick=\"layerPopup('#Invoice_pop')\">Invoice 출력</a> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   	<td class=\"mobile_layout\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s4\"> ";
        vHTML += "   				<div class=\"col\"><span class=\"trade import ship\">Export</span></div> ";
        vHTML += "   				<div class=\"col\">SAMPLE20230328001</div> ";
        vHTML += "   			</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Total Amount</th> ";
        vHTML += "   							<td>KRW 320,000</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td>BUSAN, KOREA<br>" + _fnPlusDate(0, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(0, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td>SHANGHAI, CHINA<br>" + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>On Board</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-7, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-7, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Invoice Date</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-7, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-7, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Invoice</th> ";
        vHTML += "   							<td>SAMPLEINV001</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<td colspan=\"2\"> ";
        vHTML += "   								<a href=\"javascript:void(0)\" class=\"btn_type1 skyblue\" onclick=\"layerPopup('#Invoice_pop')\">Invoice 출력</a> ";
        vHTML += "   							</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr class=\"row\" data-row=\"row_1\"> ";
        vHTML += "   	<td><span class=\"trade import ship\">Export</span></td> ";
        vHTML += "   	<td>SAMPLE20230328002</td> ";
        vHTML += "   	<td>KRW 410,000</td> ";
        vHTML += "   	<td>BUSAN, KOREA<br> " + _fnPlusDate(1, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(1, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>OSAKA, JAPAN<br> " + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-6, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-6, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-6, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-6, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>SAMPLEINV002</td> ";
        vHTML += "   	<td class=\"btns_w1\"> ";
        vHTML += "   		<div class=\"btn_padding\"> ";
        vHTML += "   			<a href=\"javascript:void(0)\" class=\"btn_type1 skyblue\" onclick=\"layerPopup('#Invoice_pop')\">Invoice 출력</a> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   	<td class=\"mobile_layout\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s4\"> ";
        vHTML += "   				<div class=\"col\"><span class=\"trade import ship\">Export</span></div> ";
        vHTML += "   				<div class=\"col\">SAMPLE20230328002</div> ";
        vHTML += "   			</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Total Amount</th> ";
        vHTML += "   							<td>KRW 410,000</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td>BUSAN, KOREA<br>" + _fnPlusDate(1, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(1, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td>OSAKA, JAPAN<br>" + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>On Board</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-6, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-6, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Invoice Date</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-6, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-6, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Invoice</th> ";
        vHTML += "   							<td>SAMPLEINV002</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<td colspan=\"2\"> ";
        vHTML += "   								<a href=\"javascript:void(0)\" class=\"btn_type1 skyblue\" onclick=\"layerPopup('#Invoice_pop')\">Invoice 출력</a> ";
        vHTML += "   							</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr class=\"row\" data-row=\"row_1\"> ";
        vHTML += "   	<td><span class=\"trade import ship\">Export</span></td> ";
        vHTML += "   	<td>SAMPLE20230328003</td> ";
        vHTML += "   	<td>KRW 751,550</td> ";
        vHTML += "   	<td>BUSAN, KOREA<br> " + _fnPlusDate(2, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(2, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>SHEKOU, CHINA<br> " + _fnPlusDate(5, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(5, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-5, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-5, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td> " + _fnPlusDate(-5, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-5, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>SAMPLEINV003</td> ";
        vHTML += "   	<td class=\"btns_w1\"> ";
        vHTML += "   		<div class=\"btn_padding\"> ";
        vHTML += "   			<a href=\"javascript:void(0)\" class=\"btn_type1 skyblue\" onclick=\"layerPopup('#Invoice_pop')\">Invoice 출력</a> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   	<td class=\"mobile_layout\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s4\"> ";
        vHTML += "   				<div class=\"col\"><span class=\"trade import ship\">Export</span></div> ";
        vHTML += "   				<div class=\"col\">SAMPLE20230328003</div> ";
        vHTML += "   			</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Total Amount</th> ";
        vHTML += "   							<td>KRW 751,550</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td>BUSAN, KOREA<br>" + _fnPlusDate(2, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(2, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td>SHEKOU, CHINA<br>" + _fnPlusDate(5, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(5, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>On Board</th> ";
        vHTML += "   							<td>2023.03.14</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Invoice Date</th> ";
        vHTML += "   							<td>2023.03.14</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Invoice</th> ";
        vHTML += "   							<td>SAMPLEINV003</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<td colspan=\"2\"> ";
        vHTML += "   								<a href=\"javascript:void(0)\" class=\"btn_type1 skyblue\" onclick=\"layerPopup('#Invoice_pop')\">Invoice 출력</a> ";
        vHTML += "   							</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr class=\"row\" data-row=\"row_1\"> ";
        vHTML += "   	<td><span class=\"trade import ship\">Export</span></td> ";
        vHTML += "   	<td>SAMPLE20230328004</td> ";
        vHTML += "   	<td>KRW 517,430</td> ";
        vHTML += "   	<td>BUSAN, KOREA<br>" + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>CHITTAGONG, BANGLADESH<br>" + _fnPlusDate(15, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(15, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>" + _fnPlusDate(-4, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-4, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>" + _fnPlusDate(-4, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-4, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>SAMPLEINV004</td> ";
        vHTML += "   	<td class=\"btns_w1\"> ";
        vHTML += "   		<div class=\"btn_padding\"> ";
        vHTML += "   			<a href=\"javascript:void(0)\" class=\"btn_type1 skyblue\" onclick=\"layerPopup('#Invoice_pop')\">Invoice 출력</a> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   	<td class=\"mobile_layout\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s4\"> ";
        vHTML += "   				<div class=\"col\"><span class=\"trade import ship\">Export</span></div> ";
        vHTML += "   				<div class=\"col\">SAMPLE20230328004</div> ";
        vHTML += "   			</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Total Amount</th> ";
        vHTML += "   							<td>KRW 517,430</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td>BUSAN, KOREA<br>" + _fnPlusDate(3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(3, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td>CHITTAGONG, BANGLADESH<br>" + _fnPlusDate(15, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(15, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>On Board</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-4, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-4, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Invoice Date</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-4, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-4, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Invoice</th> ";
        vHTML += "   							<td>SAMPLEINV004</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<td colspan=\"2\"> ";
        vHTML += "   								<a href=\"javascript:void(0)\" class=\"btn_type1 skyblue\" onclick=\"layerPopup('#Invoice_pop')\">Invoice 출력</a> ";
        vHTML += "   							</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";
        vHTML += "   <tr class=\"row\" data-row=\"row_1\"> ";
        vHTML += "   	<td><span class=\"trade import ship\">Export</span></td> ";
        vHTML += "   	<td>SAMPLE20230328005</td> ";
        vHTML += "   	<td>KRW 1,055,000</td> ";
        vHTML += "   	<td>BUSAN, KOREA<br>" + _fnPlusDate(4, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(4, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>JAKARTA, INDONESIA<br>" + _fnPlusDate(15, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(15, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>" + _fnPlusDate(-3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-3, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>" + _fnPlusDate(-3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-3, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   	<td>SAMPLEINV005</td> ";
        vHTML += "   	<td class=\"btns_w1\"> ";
        vHTML += "   		<div class=\"btn_padding\"> ";
        vHTML += "   			<a href=\"javascript:void(0)\" class=\"btn_type1 skyblue\" onclick=\"layerPopup('#Invoice_pop')\">Invoice 출력</a> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   	<td class=\"mobile_layout\"> ";
        vHTML += "   		<div class=\"layout_type2\"> ";
        vHTML += "   			<div class=\"row s4\"> ";
        vHTML += "   				<div class=\"col\"><span class=\"trade import ship\">Export</span></div> ";
        vHTML += "   				<div class=\"col\">SAMPLE20230328005</div> ";
        vHTML += "   			</div> ";
        vHTML += "   			<div class=\"row s3 block\"> ";
        vHTML += "   				<table> ";
        vHTML += "   					<tbody> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Total Amount</th> ";
        vHTML += "   							<td>KRW 1,055,000</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Departure</th> ";
        vHTML += "   							<td>BUSAN, KOREA<br>" + _fnPlusDate(4, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(4, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Arrival</th> ";
        vHTML += "   							<td>JAKARTA, INDONESIA<br>" + _fnPlusDate(15, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(15, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>On Board</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-3, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Invoice Date</th> ";
        vHTML += "   							<td>" + _fnPlusDate(-3, ".") + " (" + _fnGetWhatDay_Kor(_fnPlusDate(-3, "-").replace(/-/gi, "")) + ")</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<th>Invoice</th> ";
        vHTML += "   							<td>SAMPLEINV005</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   						<tr> ";
        vHTML += "   							<td colspan=\"2\"> ";
        vHTML += "   								<a href=\"javascript:void(0)\" class=\"btn_type1 skyblue\" onclick=\"layerPopup('#Invoice_pop')\">Invoice 출력</a> ";
        vHTML += "   							</td> ";
        vHTML += "   						</tr> ";
        vHTML += "   					</tbody> ";
        vHTML += "   				</table> ";
        vHTML += "   			</div> ";
        vHTML += "   		</div> ";
        vHTML += "   	</td> ";
        vHTML += "   </tr> ";

        $("#invoice_no_data").hide();
        $("#Invoice_Result_AREA")[0].innerHTML = vHTML;

    }
    catch (err) {
        console.log("[Error - fnMakeResult]" + err.message);
    }
}
////////////////////////API////////////////////////////

$(document).on("click", "input[name='transfer']", function () {

    $("#input_Departure").val('');
    $("#input_POL").val('');
    $("#input_Arrival").val('');
    $("#input_POD").val('');

    if ($(this).val() == "SEA") {
        $("#Table_th_BL").text("House B/L");
        _vREQ_SVC = "SEA";
    }
    else if ($(this).val() == "AIR") {
        $("#Table_th_BL").text("HAWB");
        _vREQ_SVC = "AIR";
    }

});

$(document).on('click', 'button[name=Search_invoice]', function () {
    $('#Invoice_Result_AREA').css('display', 'table-row-group');
    $('#invoice_no_data').css('display', 'none');
})

$(document).on("click", ".container.Document .list_type1 tr th", function () {
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