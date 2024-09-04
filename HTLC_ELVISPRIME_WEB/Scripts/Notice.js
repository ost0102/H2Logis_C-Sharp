////////////////////////전역 변수//////////////////////////
var _vPage = 0;
var _vPage_Notice = 0;
var _vREQ_SVC = "";
var _ObjCheck = new Object();

////////////////////jquery event///////////////////////

$(function () {

	fnGetNoticeList();

});
//공지사항 리스트 가져오기
$(document).on("click", "#btn_search", function () {
	_vPage = 1;
	fnNoticeSearch();
});

//엔터 키 이벤트
$(document).on("keyup", "#Input_Notice", function (e) {
	if (e.keyCode == 13) {
		_vPage = 1;
		fnNoticeSearch();
	}
});


//공지사항 리스트 가져오기
function fnGetNoticeList() {
	var objJsonData = new Object();

	//선사인지 훼리인지 체크
	if (_vPage_Notice == 0) {
		objJsonData.PAGE = "1";
	} else {
		objJsonData.PAGE = _vPage_Notice;
	}

	$.ajax({
		type: "POST",
		url: "/Home/fnGetNoticeList",
		async: true,
		dataType: "json",
		data: { "vJsonData": _fnMakeJson(objJsonData) },
		success: function (result) {
			fnMakeNoticeList(result);
			if (JSON.parse(result).Result[0]["trxCode"] == "Y") {
				fnNoticePaging(JSON.parse(result).NoticeList[0]["TOTCNT"], 10, 5, objJsonData.PAGE);
			}
		}, error: function (xhr, status, error) {
			$("#notice_list").hide();
			$("#notice_paging_list").hide();
			$("#Notice_NoData").show();
			console.log("[Error - fnGetNoticeList()] 공지사항 데이터를 가져올 수 없습니다.");
			console.log("[Error - fnGetNoticeList()]" + error);
		}
	});
}

//totalData = 총 데이터 count
//dataPerPage = 한페이지에 나타낼 데이터 수
//pageCount = 한화면에 나타낼 페이지 수
//currentPage = 선택한 페이지 
//공지사항 페이징
function fnNoticePaging(totalData, dataPerPage, pageCount, currentPage) {
	var totalPage = Math.ceil(totalData / dataPerPage);    // 총 페이지 수
	var pageGroup = Math.ceil(currentPage / pageCount);    // 페이지 그룹
	if (pageCount > totalPage) pageCount = totalPage;
	var last = pageGroup * pageCount;    // 화면에 보여질 마지막 페이지 번호
	if (last > totalPage) last = totalPage;
	var first = last - (pageCount - 1);    // 화면에 보여질 첫번째 페이지 번호
	var next = last + 1;
	var prev = first - 1;

	$("#notice_paging_list").empty();

	var prevPage;
	var nextPage;
	if (currentPage - 1 < 1) { prevPage = 1; } else { prevPage = currentPage - 1; }
	if (last < totalPage) { nextPage = currentPage + 1; } else { nextPage = last; }

	var vHTML = "";

	vHTML += " <a href=\"javascript:void(0)\" onclick=\"fnNoticeGoPage(1)\" class=\"page first\"> ";
	vHTML += " 	<span class=\"blind\">처음페이지로 가기</span> ";
	vHTML += " </a> ";
	vHTML += " <a href=\"javascript:void(0)\" onclick=\"fnNoticeGoPage(" + prevPage + ")\" class=\"page prev\"> ";
	vHTML += " 	<span class=\"blind\">이전페이지로 가기</span> ";
	vHTML += " </a> ";

	for (var i = first; i <= last; i++) {
		if (i == currentPage) {
			vHTML += " <span class=\"number\"><span class=\"on\">" + i + "</span></span> ";
		} else {
			vHTML += " <span class=\"number\"><span onclick=\"fnNoticeGoPage(" + i + ")\">" + i + "</span></span> ";
		}
	}

	vHTML += " <a href=\"javascript:void(0)\" onclick=\"fnNoticeGoPage(" + nextPage + ")\" class=\"page next\"> ";
	vHTML += " 	<span class=\"blind\">다음페이지로 가기</span> ";
	vHTML += " </a> ";
	vHTML += " <a href=\"javascript:void(0)\" onclick=\"fnNoticeGoPage(" + totalPage + ")\" class=\"page last\"> ";
	vHTML += " 	<span class=\"blind\">마지막페이지로 가기</span> ";
	vHTML += " </a> ";

	$("#notice_paging_list").append(vHTML);    // 페이지 목록 생성		
}

function fnNoticeGoPage(vPage) {
	_vPage_Notice = vPage;
	fnGetNoticeList();
}
/////////////////function MakeList/////////////////////
function fnMakeNoticeList(vJsonData) {

	var vHTML = "";
	var vResult = "";

	try {
		if (JSON.parse(vJsonData).Result[0]["trxCode"] == "Y") {
			vResult = JSON.parse(vJsonData).NoticeList;

			$.each(vResult, function (i) {
				vHTML += " <li> ";
				vHTML += " 	<a href=\"javascript:void(0)\" class=\"notice_tit\"> ";
				vHTML += " 		<span class=\"icn q\">" + _fnToNull(vResult[i]["REGDT"]);
				if (_fnToNull(vResult[i]["FILE_NAME"]) != "") {
					vHTML += " <img src='/Images/icn_file.png' />"
				}
				vHTML += "		</span> ";
				vHTML += " 		" + _fnToNull(vResult[i]["TITLE"]) + "  ";
				vHTML += " 	</a> ";
				vHTML += " 	<div class=\"notice_cont\"> ";
				vHTML += " 		<div class=\"scrollbar_notice\"> ";
				vHTML += " 			<div class=\"inner\"> " + _fnToNull(vResult[i]["CONTENT"]) + " ";
				vHTML += " 			</div> ";
				vHTML += " 		</div> ";

				if (_fnToNull(vResult[i]["FILE_NAME"]) != "") {
					vHTML += " 		<div class=\"notice_file\"> ";
					vHTML += " 			<ul> ";


					//파일이 있는 경우 그리기
					if (_fnToNull(vResult[i]["FILE_NAME"]) != "") {
						vHTML += " 				<li> ";
						vHTML += "					<a href=\"/HP_File/NoticeDownload?filename=" + _fnToNull(vResult[i]["FILE_NAME"]) + "&rFilename=" + _fnToNull(vResult[i]["FILE"]) + "\"> ";
						vHTML += "						<span>" + _fnToNull(vResult[i]["FILE_NAME"]) + "</span> ";
						vHTML += "					</a> ";
						vHTML += "				</li> ";
					}

					if (_fnToNull(vResult[i]["FILE1_NAME"]) != "") {
						vHTML += " 				<li> ";
						vHTML += "					<a href=\"/HP_File/NoticeDownload?filename=" + _fnToNull(vResult[i]["FILE1_NAME"]) + "&rFilename=" + _fnToNull(vResult[i]["FILE1"]) + "\"> ";
						vHTML += "						<span>" + _fnToNull(vResult[i]["FILE1_NAME"]) + "</span> ";
						vHTML += "					</a> ";
						vHTML += "				</li> ";
					}

					if (_fnToNull(vResult[i]["FILE2_NAME"]) != "") {
						vHTML += " 				<li> ";
						vHTML += "					<a href=\"/HP_File/NoticeDownload?filename=" + _fnToNull(vResult[i]["FILE2_NAME"]) + "&rFilename=" + _fnToNull(vResult[i]["FILE2"]) + "\"> ";
						vHTML += "						<span>" + _fnToNull(vResult[i]["FILE2_NAME"]) + "</span> ";
						vHTML += "					</a> ";
						vHTML += "				</li> ";
					}

					vHTML += " 			</ul> ";
					vHTML += " 		</div> ";
				}

				vHTML += " 	</div> ";
				vHTML += " </li> ";
			});
			$("#notice_list").show();
			$("#notice_paging_list").show();
			$("#Notice_NoData").hide();
		}
		else if (JSON.parse(vJsonData).Result[0]["trxCode"] == "N") {
			$("#notice_list").hide();
			$("#notice_paging_list").hide();
			$("#Notice_NoData").show();
		}
		else if (JSON.parse(vJsonData).Result[0]["trxCode"] == "E") {
			$("#notice_list").hide();
			$("#notice_paging_list").hide();
			$("#Notice_NoData").show();
		}

		//데이터 넣기
		$("#notice_list")[0].innerHTML = vHTML;

		//데이터 넣고 슬림 스크롤 넣기
		//if ($('.scrollbar_notice').length > 0) {
		//	$('.scrollbar_notice').slimScroll({
		//		height: '400px',
		//		width: '100%',
		//		color: '#005bac',
		//		alwaysVisible: false,
		//		railVisible: true,
		//	})
		//}
	}
	catch (err) {
		console.log(err);
	}
}

//더보기 버튼 이벤트
$(document).on("click", "#Btn_NoticeMore", function () {
	fnNoticeSearch();
});

//공지사항 클릭 시 내용 on/off 이벤트
$(document).on("click", ".notice_list > li .notice_tit", function (e) {
	e.preventDefault();
	var $par = $(this).closest("li");
	var inx = $par.index();
	if ($par.hasClass("on") == true) {
		$('.notice_list > li:eq(' + inx + ')').find(".notice_cont").stop().slideUp();
		$par.removeClass("on");
	} else {
		if ($('.notice_list > li').hasClass("on")) {
			$('.notice_list > li').removeClass("on");
			$('.notice_list > li').find(".notice_cont").slideUp();
		}
		$('.notice_list > li:eq(' + inx + ')').addClass("on");
		$par.find(".notice_cont").slideDown();
	}
});

////////////////////////function///////////////////////

/////////////////function MakeList/////////////////////

////////////////////////API////////////////////////////
