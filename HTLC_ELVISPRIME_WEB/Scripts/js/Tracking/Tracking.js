var url = "Tracking";
var mymap;
var obj;
$(function () {
});

function validateCheck(bool) {
    if (_fnToNull($("#S_HBL_NO").val()) == "" && _fnToNull($("#S_CNTR_NO").val()) == "") {
        _fnAlertMsg("검색할 번호를 입력해주세요" , "S_HBL_NO");
        return false;
    }
    return true;
}

$('#btnSearch').click(function () {
    $('.tracking_area.tracking_page').css('display', 'block');

    $('.tracking-table').animate({
        scrollTop: parseInt($(".last-sec").offset().top)
    }, 1500);
    //if (validateCheck(false)) {
    //    var callObj = new Object();
    //    callObj.OFFICE_CD = _fnToNull($("#Session_OFFICE_CD").val());
    //    callObj.reqVal1 = _fnToNull($("#S_HBL_NO").val().toUpperCase().trim());
    //    callObj.reqVal2 = _fnToNull($("#S_CNTR_NO").val().toUpperCase().trim());

    //    var callObject = new Object();
    //    callObject.paramObj = _fnMakeJson(callObj);

    //    $.ajax({
    //        type: "POST",
    //        url: "/" + url + "/GetTrackingList",
    //        async: false,
    //        dataType: "json",
    //        data: callObject,
    //        success: function (rtnVal) {
    //            var apdVal = "";
    //        if (rtnVal.Result[0].trxCode == "Y") {
    //            var rtnTbl = rtnVal.Table1;
    //            if (_fnToNull(rtnTbl) != "") {
    //                $("#TrackList").empty();
    //                $("#MBL_NO").empty();
    //                $("#HBL_NO").empty();
    //                if (_fnToNull(rtnTbl[0].COLD_TYPE) != "N") {
    //                    $("#MBL_NO").append(_fnToNull(rtnTbl[0].LINE_BKG_NO));
    //                } else {
    //                    $("#MBL_NO").append(_fnToNull(rtnTbl[0].MBL_NO));
    //                }
    //                $("#HBL_NO").append(_fnToNull(rtnTbl[0].HBL_NO));
    //                $(rtnTbl).each(function (i) {
    //                    if (_fnToNull(rtnTbl[i].ACT_EVT_CD) == "Y") {
    //                        apdVal += "	<li class='now'>	";
    //                        apdVal += "        <div class='inner'>	";
    //                        apdVal += "            <h3 class='tit " + _fnDateCal(_fnDateFormat(_fnToNull(rtnTbl[i].EST)), _fnDateFormat(_fnToNull(rtnTbl[i].ACT))) + "'>" + _fnToNull(rtnTbl[i].EVENT_NM) + "</h3>	";
    //                        apdVal += "            <div class='cont'>	";
    //                        apdVal += "                <div class='info_box'>	";
    //                        apdVal += "                    <div class='col'>	";
    //                        if (_fnToNull(rtnTbl[i].ACT_LOC_CD) != "") {
    //                            apdVal += _fnToNull(rtnTbl[i].ACT_LOC_CD);
    //                        } else {
    //                            apdVal += _fnToNull(rtnTbl[i].EST_LOC_CD);
    //                        }
    //                        apdVal += "                    </div>	";
    //                        apdVal += "                    <div class='col right'>	";
    //                        apdVal += "                        <p>" + _fnToNull(_fnDateFormat(rtnTbl[i].EST_YMD)) + ' ' + _fnToNull(_fnDateFormat(rtnTbl[i].EST_HM)) + "</p>	";
    //                        apdVal += "                        <p style='color:#019e96'>" + _fnToNull(_fnDateFormat(rtnTbl[i].ACT_YMD)) + ' ' + _fnToNull(_fnDateFormat(rtnTbl[i].ACT_HM)) + "</p>	";
    //                        apdVal += "                    </div>	";
    //                        apdVal += "                </div>	";
    //                        apdVal += "            </div>	";
    //                        apdVal += "        </div>	";
    //                        apdVal += "    </li>	";
    //                    } else if (_fnToNull(rtnTbl[i].ACT_EVT_CD) == "E") {
    //                        apdVal += "	<li class='complete'>	";
    //                        apdVal += "        <div class='inner'>	";
    //                        apdVal += "            <h3 class='tit " + _fnDateCal(_fnDateFormat(_fnToNull(rtnTbl[i].EST)), _fnDateFormat(_fnToNull(rtnTbl[i].ACT))) + "'>" + _fnToNull(rtnTbl[i].EVENT_NM) + "</h3>	";
    //                        apdVal += "            <div class='cont'>	";
    //                        apdVal += "                <div class='info_box'>	";
    //                        apdVal += "                    <div class='col'>	";
    //                        if (_fnToNull(rtnTbl[i].ACT_LOC_CD) != "") {
    //                            apdVal += _fnToNull(rtnTbl[i].ACT_LOC_CD);
    //                        } else {
    //                            apdVal += _fnToNull(rtnTbl[i].EST_LOC_CD);
    //                        }
    //                        apdVal += "                    </div>	";
    //                        apdVal += "                    <div class='col right'>	";
    //                        apdVal += "                        <p>" + _fnToNull(_fnDateFormat(rtnTbl[i].EST_YMD)) + ' ' + _fnToNull(_fnDateFormat(rtnTbl[i].EST_HM)) + "</p>	";
    //                        apdVal += "                        <p style='color:#019e96'>" + _fnToNull(_fnDateFormat(rtnTbl[i].ACT_YMD)) + ' ' + _fnToNull(_fnDateFormat(rtnTbl[i].ACT_HM)) + "</p>	";
    //                        apdVal += "                    </div>	";
    //                        apdVal += "                </div>	";
    //                        apdVal += "            </div>	";
    //                        apdVal += "        </div>	";
    //                        apdVal += "    </li>	";
    //                    } else if (_fnToNull(rtnTbl[i].ACT_EVT_CD) == "N") {
    //                        apdVal += "	<li>	";
    //                        apdVal += "        <div class='inner'>	";
    //                        apdVal += "            <h3 class='tit " + _fnDateCal(_fnDateFormat(_fnToNull(rtnTbl[i].EST)), _fnDateFormat(_fnToNull(rtnTbl[i].ACT))) + "'>" + _fnToNull(rtnTbl[i].EVENT_NM) + "</h3>	";
    //                        apdVal += "            <div class='cont'>	";
    //                        apdVal += "                <div class='info_box'>	";
    //                        apdVal += "                    <div class='col'>	";
    //                        if (_fnToNull(rtnTbl[i].ACT_LOC_CD) != "") {
    //                            apdVal += _fnToNull(rtnTbl[i].ACT_LOC_CD);
    //                        } else {
    //                            apdVal += _fnToNull(rtnTbl[i].EST_LOC_CD);
    //                        }
    //                        apdVal += "                    </div>	";
    //                        apdVal += "                    <div class='col right'>	";
    //                        apdVal += "                        <p>" + _fnToNull(_fnDateFormat(rtnTbl[i].EST_YMD)) + ' ' + _fnToNull(_fnDateFormat(rtnTbl[i].EST_HM)) + "</p>	";
    //                        apdVal += "                        <p style='color:#019e96'>" + _fnToNull(_fnDateFormat(rtnTbl[i].ACT_YMD)) + ' ' + _fnToNull(_fnDateFormat(rtnTbl[i].ACT_HM)) + "</p>	";
    //                        apdVal += "                    </div>	";
    //                        apdVal += "                </div>	";
    //                        apdVal += "            </div>	";
    //                        apdVal += "        </div>	";
    //                        apdVal += "    </li>	";
    //                    }
    //                });
    //                $("#TrackList").append(apdVal);
    //                $(".delivery_status").show();
    //            } else {
    //                $(".delivery_status").hide();
    //            }
    //        }
    //        }, error: function (xhr) {
    //            console.log("시스템 사정으로 요청하신 작업을 처리할 수 없습니다.");
    //            console.log(xhr);
    //            return;
    //        }, beforeSend: function () {
    //            $("#ProgressBar_Loading").show(); //프로그래스 바 

    //        },
    //        complete: function () {
    //            $("#ProgressBar_Loading").hide(); //프로그래스 바 
    //        }
    //    });
    //    reqVal = _fnMakeJson(callObj);


    //    var objRPAJsonData = new Object();

    //    objRPAJsonData.reqVal1 = _fnToNull($("#S_HBL_NO").val().toUpperCase().trim());
    //    objRPAJsonData.reqVal2 = _fnToNull($("#S_CNTR_NO").val().toUpperCase().trim());

    //    var strurl = "http://api2.elvisprime.com/api/Trk/GetTrackingAIS";
    //    var AppKey = "20210322151646087070" + "^" + "uBW7FGEIHScRz84cCp4jEVyAqjMCEgeS+qpK5Prxq/mgc6owaTyAv89b3O5KlXlq";
    //    $.ajax({
    //        url: strurl,
    //        beforeSend: function (xhr) {
    //            xhr.setRequestHeader('Authorization-Token', AppKey);
    //        },
    //        type: "POST",
    //        async: true,
    //        dataType: "json",
    //        data: { "": _fnMakeJson(objRPAJsonData) },
    //        success: function (result) {
    //            if (_fnToNull(result) != "") {
    //                var rtnData = JSON.parse(result);
    //                if (_fnToNull(rtnData.Result) != "") {
    //                    if (rtnData.Result[0].trxCode == "N" || rtnData.Result[0].trxCode == "E") {
    //                        drawingmapNodata();
    //                        //_fnAlertMsg("추적이 되지않는 데이터입니다.");
    //                        return false;
    //                    }
    //                } else {
    //                    drawingMap(rtnData);
    //                }

    //            } else {
    //                _fnAlertMsg("추적이 되지않는 데이터입니다.");
    //            }
    //        }, error: function (xhr) {
    //            alert("시스템 사정으로 요청하신 작업을 처리할 수 없습니다.");
    //            console.log(xhr);
    //            return;
    //        }
    //    });
    //}
});

function drawingmapNodata() {

    if (_fnToNull(mymap) != "") {
        mymap.remove();
    }
    mymap = L.map('map', {
        //center: [lat, lng],
        center: [32.896531, 124.402956],
        zoom: 5
    });

    L.tileLayer('http://mt0.google.com/vt/lyrs=y&hl=kr&x={x}&y={y}&z={z}', {
        attribution: 'Map data &copy; Copyright Google Maps<a target="_blank" href="https://maps.google.com/maps?ll=24.53279,56.62833&amp;z=13&amp;t=m&amp;hl=ko-KR&amp;gl=US&amp;mapclient=apiv3"></a>' //화면 오른쪽 하단 attributors
    }).addTo(mymap);
}

function connectTheDots(data) {
    var c = [];
    for (i in data._layers) {
        var x = data._layers[i]._latlng.lat;
        var y = data._layers[i]._latlng.lng;
        c.push([x, y]);
    }
    return c;
}
function drawingMap(json_data) {
    var spiral = {
        type: "FeatureCollection",
        features: []
    };
    var master = json_data.Master[0];//헤더 테이블 조회
    var result = [];
    for (var i in master)
        result.push([master[i]]);

    var POD = result[4].concat(result[5]);
    var POL = result[8].concat(result[9]);
    var lastRoute;
    var rotate;
    var detail = json_data.Detail;//디테일 테이블 조회

    var result2 = [];
    if (detail.length == 0) {
        var arrayDt = [];
        arrayDt.push(result[4]);
        arrayDt.push(result[5]);
        lastRoute = arrayDt;    // 배 아이콘 위치
        rotate = "0"; // 배 방향
        result2.push(arrayDt);

        var g = {
            "color": "red",
            "type": "Point",
            "coordinates": [result[9], result[8]]
        };
        var p = {
            "id": 0,
            "speed": 0,
            "course": 0
        };
        spiral.features.push({
            "geometry": g,
            "type": "Feature",
            "properties": p
        });
    } else {
        for (var i = 0; i < detail.length; i++) {
            var arrayDt = [];
            arrayDt.push(detail[i]["MAP_LAT"]);
            arrayDt.push(detail[i]["MAP_LNG"]);
            lastRoute = arrayDt;    // 배 아이콘 위치
            rotate = detail[i]["MAP_COURSE"]; // 배 방향
            result2.push(arrayDt);
            var g = {
                "type": "Point",
                "coordinates": [detail[i]["MAP_LNG"], detail[i]["MAP_LAT"]]
            };
            var p = {
                "id": i,
                "speed": detail[i]["MAP_SPEED"],
                "course": detail[i]["MAP_COURSE"]
            };
            spiral.features.push({
                "geometry": g,
                "type": "Feature",
                "properties": p
            });
        }
    }
    var zoom = 5; //줌 레벨
    if (_fnToNull(mymap) != "") {
        mymap.remove();
    }
    mymap = L.map('map', {
        //center: [lat, lng],
        center: [32.896531, 124.402956],
        zoom: zoom,
        zoomControl: false
    });
    L.control.zoom({
        position: 'bottomright'
    }).addTo(mymap);

    /*
    lyrs=m : Standard Road Map
    lyrs=p : Terrain
    lyrs=r : Somehow Altered Road Map
    lyrs=s : Satellite Only
    lyrs=t : Terrain Only
    lyrs=y : Hybrid
    lyrs=h : Roads Only
    */
    L.tileLayer('http://mt0.google.com/vt/lyrs=y&hl=kr&x={x}&y={y}&z={z}', {
        attribution: 'Map data &copy; Copyright Google Maps<a target="_blank" href="https://maps.google.com/maps?ll=24.53279,56.62833&amp;z=13&amp;t=m&amp;hl=ko-KR&amp;gl=US&amp;mapclient=apiv3"></a>' //화면 오른쪽 하단 attributors
    }).addTo(mymap);
    // Creating a poly line

    var circleIcon = L.icon({
        iconUrl: "../Images/circle_red.png",
        iconSize: [4, 4]  // size of the icon
    });


    var polyline = L.geoJson(spiral, {
        pointToLayer: function (feature, latlng) {
            return L.marker(latlng, {
                icon: circleIcon
            });
        }
        //onEachFeature: function (feature, layer) {
        //    layer.bindPopup('<table style="width:80px"><tbody><tr><td><div><b>speed:</b></div></td><td><div>' + feature.properties.speed + '</div></td></tr><tr><td><div><b>course:</b></div></td><td><div>' + feature.properties.course + '</div></td></tr></tbody></table>');
        //    layer.on('mouseover', function () { layer.openPopup(); });
        //    layer.on('mouseout', function () { layer.closePopup(); });
        //}
    });


    //spiralBounds = polyline.getBounds();
    //mymap.fitBounds(spiralBounds);
    polyline.addTo(mymap);

    spiralCoords = connectTheDots(polyline);
    var spiralLine = L.polyline(spiralCoords, { color: '#ff2600' }).addTo(mymap); //color 변경 

    var shipIconBig = L.icon({
        iconUrl: "../Images/map_ship@73x87.png",
        iconSize: [24, 30]  // size of the icon
    });

    var shipIcon = L.icon({
        iconUrl: "../Images/map_ship@73x87.png",
        iconSize: [30, 42]
    });

    var makerIcon = L.icon({
        iconUrl: '../Images/map_marker@64x79.png',
        iconSize: [30, 42]
    });
    var portIcon = L.icon({
        iconUrl: '../Images/map_port@69x79.png',
        iconSize: [30, 42]
    });

    var makerIconBig = L.icon({
        iconUrl: '../Images/map_marker@64x79.png',
        iconSize: [24, 30]
    });
    var portIconBig = L.icon({
        iconUrl: '../Images/map_port@69x79.png',
        iconSize: [24, 30] // size of the icon
    });


    var maker_POD = L.marker(POD, { icon: makerIconBig }).addTo(mymap);
    var maker_POL = L.marker(POL, { icon: portIconBig }).addTo(mymap);
    var LastMarker = L.marker(lastRoute, { icon: shipIcon, rotationOrigin: 'center center', rotationAngle: rotate }).addTo(mymap);


    mymap.on('zoomend', function () {
        var currentZoom = mymap.getZoom();
        if (currentZoom <= 5) {
            maker_POD.setIcon(makerIconBig);
            maker_POL.setIcon(portIconBig);
            LastMarker.setIcon(shipIconBig);
        }
        else {
            maker_POD.setIcon(makerIcon);
            maker_POL.setIcon(portIcon);
            LastMarker.setIcon(shipIcon);
        }
    });
}
