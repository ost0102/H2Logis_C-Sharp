using System.Data;

namespace HTLC_ELVISPRIME_COMMON.Query.Main
{
    class Main_Query
    {
        string sqlstr;

        /// <summary>
        /// 스케줄 데이터 가지고 오기
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string GetSEAScheduleData_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr = "  SELECT * FROM (";
            sqlstr += "  SELECT  ROWNUM AS RNUM";
            sqlstr += " , FLOOR ( (ROWNUM - 1) / 10 + 1) AS PAGE ";
            sqlstr += " , COUNT (*) OVER () AS TOTCNT	";
            sqlstr += " , SCH_NO	";
            sqlstr += " , LINE_CD	";
            sqlstr += " , LINE_NM	";
            sqlstr += " , IMG_PATH	";
            sqlstr += " , REQ_SVC	";
            sqlstr += " , OFFICE_CD	";
            sqlstr += " , VSL_VOY	";
            sqlstr += " , VSL";
            sqlstr += " , VOY";
            sqlstr += " , SCH_PIC	";
            sqlstr += " , POL_CD	";
            sqlstr += " , POL_NM	";
            sqlstr += " , POD_CD	";
            sqlstr += " , POD_NM	";
            sqlstr += " , POL_TML_NM	";
            sqlstr += " , ETD	";
            sqlstr += " , ETD_HM	";
            sqlstr += " , ETA	";
            sqlstr += " , ETA_HM	";
            sqlstr += " , DOC_CLOSE_YMD ";
            sqlstr += " , DOC_CLOSE_HM";
            sqlstr += " , TS_CNT	";
            sqlstr += " , RMK	";
            sqlstr += " , TRANSIT_TIME	";
            sqlstr += " , TRANSIT_TIME_NM	";
            sqlstr += " , TS_DTL	";
            sqlstr += " , TS_COUNT	";
            sqlstr += " , CARGO_CLOSE_YMD ";
            sqlstr += " , CARGO_CLOSE_HM ";
            sqlstr += " , CNTR_TYPE ";
            sqlstr += " , PREV_CLOSE ";
            sqlstr += "         FROM(";
            sqlstr += "  SELECT SCH_NO";
            sqlstr += "           , OFFICE_CD";
            sqlstr += "           , REQ_SVC";
            sqlstr += "           , A.LINE_CD";
            sqlstr += "           , IMG_PATH";
            sqlstr += "           , (SELECT CARR_NM";
            sqlstr += "               FROM MDM_CARR_MST";
            sqlstr += "              WHERE CARR_CD = A.LINE_CD)";
            sqlstr += "              AS LINE_NM";
            sqlstr += "           , VSL || ' ' ||VOY AS VSL_VOY";
            sqlstr += "           , VSL";
            sqlstr += "           , VOY";
            sqlstr += "           , POL_CD";
            sqlstr += "           , (SELECT LOC_NM";
            sqlstr += "               FROM MDM_PORT_MST";
            sqlstr += "             WHERE LOC_CD = POL_CD)";
            sqlstr += "              AS POL_NM";
            sqlstr += "            , POL_TML_NM";
            sqlstr += "            , POD_CD";
            sqlstr += "            , (SELECT LOC_NM";
            sqlstr += "                FROM MDM_PORT_MST";
            sqlstr += "              WHERE LOC_CD = POD_CD)";
            sqlstr += "               AS POD_NM";
            sqlstr += "            , ETD";
            sqlstr += "            , ETD_HM ";
            sqlstr += "            , ETA";
            sqlstr += "            , ETA_HM";
            sqlstr += "            , A.DOC_CLOSE_YMD ";
            sqlstr += "            , SUBSTR(A.DOC_CLOSE_HM,0,4) AS DOC_CLOSE_HM ";
            sqlstr += "            , A.CARGO_CLOSE_YMD ";
            sqlstr += "            , SUBSTR(A.CARGO_CLOSE_HM,0,4) AS CARGO_CLOSE_HM ";
            sqlstr += "            , TO_CHAR (TO_DATE (DOC_CLOSE_YMD) - 1, 'YYYYMMDD') AS PREV_CLOSE";
            sqlstr += "            , TS_CNT ";
            sqlstr += "            , TRANSIT_TIME";
            sqlstr += "            , TRANSIT_TIME_NM";
            sqlstr += "            , SCH_PIC";
            sqlstr += "            , A.RMK";
            sqlstr += "            , A.LINE_TYPE";
            sqlstr += "            , ( SELECT CNTN_GRP_CD FROM MDM_PORT_MST  WHERE LOC_CD= A.POD_CD) AS CNTN_GRP_CD ";
            sqlstr += "            , CASE WHEN TS_CNT =  0 THEN (SELECT MAX(SEQ) - 1 FROM PRM_SCH_TS WHERE SCH_NO = A.SCH_NO) ELSE 0 END AS TS_COUNT ";
            sqlstr += "            , CASE WHEN TS_CNT = '0'THEN(SELECT LISTAGG(POL_CD, '-') WITHIN GROUP (ORDER BY SEQ) AS POL_CD FROM PRM_SCH_TS WHERE SCH_NO = A.SCH_NO)|| '-' || A.POD_CD END AS TS_DTL ";
            sqlstr += "            , CASE WHEN CNTR_TYPE = 'F' THEN 'FCL' WHEN CNTR_TYPE = 'L' THEN 'LCL' WHEN A.CNTR_TYPE = 'B' THEN 'BULK' END AS CNTR_TYPE  ";
            sqlstr += "                    FROM PRM_SCH_MST A ";
            sqlstr += "                         INNER JOIN MDM_CARR_MST CARR ";
            sqlstr += "                            ON A.LINE_CD = CARR.CARR_CD ";
            sqlstr += "                         LEFT OUTER JOIN MDM_LINE_IMG B ";
            sqlstr += "                            ON CARR.SCAC_CD = B.LINE_CD ";
            sqlstr += "                               OR CARR.EDI_CD = B.LINE_CD ";
            sqlstr += "   WHERE 1 = 1 ";

            sqlstr += "    AND A.WEB_FLAG = 'Y' "; 
            sqlstr += "    AND A.ETD >=  '" + dr["ETD_START"] + "'";

            sqlstr += "    AND ( ('" + dr["REQ_SVC"] + "' IS NULL and 1 = 1 ) or ('" + dr["REQ_SVC"] + "'  = 'ALL' and 1 = 1 ) or ('" + dr["REQ_SVC"] + "' IS NOT NULL and A.REQ_SVC = '" + dr["REQ_SVC"] + "' ) ) ";
            sqlstr += "    AND A.LINE_TYPE = '" + dr["LINE_TYPE"] + "'";


            if (dr["CNTR_TYPE"].ToString() != "")
            {
                sqlstr += "    AND A.CNTR_TYPE = '" + dr["CNTR_TYPE"] + "'";
            }

            if (dr["POL_CD"].ToString() == "")
            {
                sqlstr += "    AND ( (UPPER('" + dr["POL"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POL"] + "') IS NOT NULL and A.POL_CD LIKE UPPER('%" + dr["POL"] + "%')) or (UPPER('" + dr["POL"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = A.POL_CD) LIKE UPPER('%" + dr["POL"] + "%') ) )";
            }
            else
            {
                sqlstr += "    AND ( (UPPER('" + dr["POL"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POL"] + "') IS NOT NULL and A.POL_CD LIKE UPPER('%" + dr["POL_CD"] + "%') ) or (UPPER('" + dr["POL"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = A.POL_CD) LIKE UPPER('%" + dr["POL"] + "%') ) )";
            }

            if (dr["POD_CD"].ToString() == "")
            {
                sqlstr += "    AND ( (UPPER('" + dr["POD"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and A.POD_CD LIKE UPPER('%" + dr["POD"] + "%') ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = A.POD_CD) LIKE UPPER('%" + dr["POD"] + "%') ) )";
            }
            else
            {
                sqlstr += "    AND ( (UPPER('" + dr["POD"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and A.POD_CD LIKE UPPER('%" + dr["POD_CD"] + "%') ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = A.POD_CD) LIKE UPPER('%" + dr["POD"] + "%') ) )";
            }

            if (dr["ID"].ToString() != "")
            {
                sqlstr += "ORDER BY " + dr["ID"] + " " + dr["ORDER"] + "";
            }
            else
                sqlstr += "ORDER BY ETA";
            sqlstr += " )A ";
            sqlstr += ")WHERE PAGE = " + dr["PAGE"];

            return sqlstr;
        }

        /// <summary>
        /// 스케줄 데이터 가지고 오기
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string GetAIRScheduleData_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr = "  SELECT * FROM (";
            sqlstr += "  SELECT  ROWNUM AS RNUM";
            sqlstr += " , FLOOR ( (ROWNUM - 1) / 10 + 1) AS PAGE ";
            sqlstr += " , COUNT (*) OVER () AS TOTCNT	";
            sqlstr += " , SCH_NO	";
            sqlstr += " , LINE_CD	";
            sqlstr += " , LINE_NM	";
            sqlstr += " , IMG_PATH	";
            sqlstr += " , REQ_SVC	";
            sqlstr += " , OFFICE_CD	";
            sqlstr += " , VSL_VOY	";
            sqlstr += " , VSL";
            sqlstr += " , VOY";
            sqlstr += " , SCH_PIC	";
            sqlstr += " , POL_CD	";
            sqlstr += " , POL_NM	";
            sqlstr += " , POD_CD	";
            sqlstr += " , POD_NM	";
            sqlstr += " , POL_TML_NM	";
            sqlstr += " , ETD	";
            sqlstr += " , ETD_HM	";
            sqlstr += " , ETA	";
            sqlstr += " , ETA_HM	";
            sqlstr += " , DOC_CLOSE_YMD ";
            sqlstr += " , DOC_CLOSE_HM";
            sqlstr += " , PREV_CLOSE";
            sqlstr += " , TS_CNT	";
            sqlstr += " , RMK	";
            sqlstr += " , TRANSIT_TIME	";
            sqlstr += " , TRANSIT_TIME_NM	";
            sqlstr += " , TS_DTL	";
            sqlstr += " , TS_COUNT	";
            sqlstr += " , CARGO_CLOSE_YMD ";
            sqlstr += " , CARGO_CLOSE_HM ";
            sqlstr += " , CNTR_TYPE ";
            sqlstr += "         FROM(";
            sqlstr += "  SELECT SCH_NO";
            sqlstr += "           , OFFICE_CD";
            sqlstr += "           , REQ_SVC";
            sqlstr += "           , A.LINE_CD";
            sqlstr += "           , IMG_PATH";
            sqlstr += "           , (SELECT CARR_NM";
            sqlstr += "               FROM MDM_CARR_MST";
            sqlstr += "              WHERE CARR_CD = A.LINE_CD)";
            sqlstr += "              AS LINE_NM";
            sqlstr += "           , VSL || ' ' ||VOY AS VSL_VOY";
            sqlstr += "           , VSL";
            sqlstr += "           , VOY";
            sqlstr += "           , POL_CD";
            sqlstr += "           , (SELECT LOC_NM";
            sqlstr += "               FROM MDM_PORT_MST";
            sqlstr += "             WHERE LOC_CD = POL_CD)";
            sqlstr += "              AS POL_NM";
            sqlstr += "            , POL_TML_NM";
            sqlstr += "            , POD_CD";
            sqlstr += "            , (SELECT LOC_NM";
            sqlstr += "                FROM MDM_PORT_MST";
            sqlstr += "              WHERE LOC_CD = POD_CD)";
            sqlstr += "               AS POD_NM";
            sqlstr += "            , ETD ";
            sqlstr += "            , ETD_HM ";
            sqlstr += "            , ETA ";
            sqlstr += "            , ETA_HM";
            sqlstr += "            , A.DOC_CLOSE_YMD ";
            sqlstr += "            , SUBSTR(A.DOC_CLOSE_HM,0,4) AS DOC_CLOSE_HM ";
            sqlstr += "            , A.CARGO_CLOSE_YMD ";
            sqlstr += "            , SUBSTR(A.CARGO_CLOSE_HM,0,4) AS CARGO_CLOSE_HM ";
            sqlstr += "            , TO_CHAR (TO_DATE (DOC_CLOSE_YMD) - 1, 'YYYYMMDD') AS PREV_CLOSE";
            sqlstr += "            , TS_CNT ";
            sqlstr += "            , TRANSIT_TIME";
            sqlstr += "            , TRANSIT_TIME_NM";
            sqlstr += "            , SCH_PIC";
            sqlstr += "            , A.RMK";
            sqlstr += "            , A.LINE_TYPE";
            sqlstr += "            , ( SELECT CNTN_GRP_CD FROM MDM_PORT_MST  WHERE LOC_CD= A.POD_CD) AS CNTN_GRP_CD ";
            sqlstr += "            , CASE WHEN TS_CNT =  0 THEN (SELECT MAX(SEQ) - 1 FROM PRM_SCH_TS WHERE SCH_NO = A.SCH_NO) ELSE 0 END AS TS_COUNT ";
            sqlstr += "            , CASE WHEN TS_CNT = '0'THEN(SELECT LISTAGG(POL_CD, '-') WITHIN GROUP (ORDER BY SEQ) AS POL_CD FROM PRM_SCH_TS WHERE SCH_NO = A.SCH_NO)|| '-' || A.POD_CD END AS TS_DTL ";
            sqlstr += "            , CASE WHEN CNTR_TYPE = 'F' THEN 'FCL' WHEN CNTR_TYPE = 'L' THEN 'LCL' WHEN CNTR_TYPE = 'C' THEN 'CONSOL' END AS CNTR_TYPE  ";
            sqlstr += "    FROM PRM_SCH_MST  A";
            sqlstr += "    LEFT OUTER JOIN MDM_LINE_IMG B ON A.LINE_CD = B.LINE_CD";
            sqlstr += "                             INNER JOIN MDM_CARR_MST CARR ";
            sqlstr += "                         ON A.LINE_CD = CARR.CARR_CD ";
            sqlstr += "   WHERE 1 = 1 ";

            sqlstr += "    AND A.WEB_FLAG = 'Y' "; 
            sqlstr += "    AND A.ETD >=  '" + dr["ETD_START"] + "'";

            sqlstr += "    AND ( ('" + dr["REQ_SVC"] + "' IS NULL and 1 = 1 ) or ('" + dr["REQ_SVC"] + "'  = 'ALL' and 1 = 1 ) or ('" + dr["REQ_SVC"] + "' IS NOT NULL and A.REQ_SVC = '" + dr["REQ_SVC"] + "' ) ) ";            

            if (dr["POL_CD"].ToString() == "")
            {
                sqlstr += "    AND ( (UPPER('" + dr["POL"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POL"] + "') IS NOT NULL and A.POL_CD LIKE UPPER('%" + dr["POL"] + "%')) or (UPPER('" + dr["POL"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = A.POL_CD) LIKE UPPER('%" + dr["POL"] + "%') ) )";
            }
            else
            {
                sqlstr += "    AND ( (UPPER('" + dr["POL"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POL"] + "') IS NOT NULL and A.POL_CD LIKE UPPER('%" + dr["POL_CD"] + "%') ) or (UPPER('" + dr["POL"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = A.POL_CD) LIKE UPPER('%" + dr["POL"] + "%') ) )";
            }

            if (dr["POD_CD"].ToString() == "")
            {
                sqlstr += "    AND ( (UPPER('" + dr["POD"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and A.POD_CD LIKE UPPER('%" + dr["POD"] + "%') ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = A.POD_CD) LIKE UPPER('%" + dr["POD"] + "%') ) )";
            }
            else
            {
                sqlstr += "    AND ( (UPPER('" + dr["POD"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and A.POD_CD LIKE UPPER('%" + dr["POD_CD"] + "%') ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = A.POD_CD) LIKE UPPER('%" + dr["POD"] + "%') ) )";
            }

            if (dr["ID"].ToString() != "")
            {
                sqlstr += "ORDER BY " + dr["ID"] + " " + dr["ORDER"] + "";
            }
            else
                sqlstr += "ORDER BY ETA";
            sqlstr += " )A ";
            sqlstr += ")WHERE PAGE = " + dr["PAGE"];

            return sqlstr;
        }

        /// <summary>
        /// 화물추적 MST 쿼리
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string Query_GetTrackMstList(DataRow dr)
        {
            sqlstr = "";

            sqlstr += "	SELECT A.HBL_NO		";
            sqlstr += "          , A.CNTR_NO		";
            sqlstr += "          , A.MBL_NO		";
            sqlstr += "          , (SELECT CD_NM FROM MDM_COM_CODE WHERE COMN_CD = A.NOW_EVENT_CD AND GRP_CD = CASE WHEN A.REQ_SVC = 'SEA' THEN 'B30' WHEN A.REQ_SVC = 'AIR' THEN 'A30' END) AS NOW_EVENT_NM		";
            sqlstr += "          , ACT_LOC_NM	";
            sqlstr += "          , (SELECT CASE WHEN CTRY_CD LIKE 'KR' THEN 'E' ELSE 'I' END FROM MDM_PORT_MST WHERE LOC_CD = A.POL_CD) AS EX_IM_TYPE 		";
            sqlstr += "          , A.REQ_SVC 		";
            sqlstr += "    FROM(  		";
            sqlstr += "    SELECT A.HBL_NO		";
            sqlstr += "             , A.CNTR_NO		";
            sqlstr += "             , MAX(C.MBL_NO) AS MBL_NO		";
            sqlstr += "             , MAX(NOW_EVENT_CD) AS NOW_EVENT_CD		";
            sqlstr += "             , MAX(LAST_EVENT_CD) AS LAST_EVENT_CD   ";
            sqlstr += "             , MAX(B.ACT_LOC_CD) AS ACT_LOC_CD		";
            sqlstr += "             , MAX(B.ACT_LOC_NM) AS ACT_LOC_NM		";
            sqlstr += "             , MAX(C.REQ_SVC) AS REQ_SVC		";
            sqlstr += "             , MAX(C.POL_CD) AS POL_CD		";
            sqlstr += "       FROM FMS_TRK_MST A		";
            sqlstr += "                LEFT OUTER JOIN FMS_TRK_DTL B ON A.HBL_NO = B.HBL_NO		";
            sqlstr += "                LEFT OUTER JOIN FMS_HBL_MST C ON A.HBL_NO = C.HBL_NO		";
            sqlstr += "                WHERE 1 = 1		";
            sqlstr += "                AND (A.HBL_NO = '" + dr["HBL_NO"] + "' OR A.CNTR_NO = '" + dr["HBL_NO"] + "' OR C.MBL_NO = '" + dr["HBL_NO"] + "')		";

            sqlstr += "                GROUP BY A.HBL_NO,A.CNTR_NO		";
            sqlstr += "	 )A		";

            return sqlstr;
        }

        /// <summary>
        /// 화물추적 DTL 쿼리
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string Query_GetTrackDTL(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT EVENT_CD	";
            sqlstr += "          ,  EVENT_NM";
            sqlstr += "          ,  ACT_LOC_CD";
            sqlstr += "          ,  TO_CHAR(TO_DATE(ACT_YMD),'YYYY.MM.DD') AS ACT_YMD ";
            sqlstr += "          ,  TO_CHAR(TO_DATE(ACT_HM,'HH24MISS'),'HH24:MI') AS ACT_HM";
            sqlstr += "          ,  TO_CHAR(TO_DATE(EST_YMD),'YYYY.MM.DD') AS EST_YMD ";
            sqlstr += "          ,  TO_CHAR(TO_DATE(EST_HM,'HH24MISS'),'HH24:MI') AS EST_HM";
            sqlstr += "          ,  ACT_LOC_NM";
            sqlstr += "          , EVENT_STATUS";
            sqlstr += "          , EX_IM_TYPE";
            sqlstr += "          , REQ_SVC";
            sqlstr += "          FROM (";
            sqlstr += " SELECT EVENT_CD	";
            sqlstr += "          ,  B.CD_NM AS EVENT_NM";
            sqlstr += "          ,  ACT_LOC_CD";
            sqlstr += "          ,  ACT_YMD";
            sqlstr += "          ,  ACT_HM";
            sqlstr += "          ,  EST_YMD";
            sqlstr += "          ,  EST_HM";
            sqlstr += "          ,  ACT_LOC_NM";
            sqlstr += "          , CASE WHEN (SELECT NOW_EVENT_CD FROM FMS_TRK_MST WHERE CNTR_NO = A.CNTR_NO AND HBL_NO = A.HBL_NO)  = A.EVENT_CD THEN 'Y' ";
            sqlstr += "                 WHEN ACT_YMD IS NOT NULL OR ACT_HM IS NOT NULL THEN 'E'";
            sqlstr += "                 ELSE 'N' END AS EVENT_STATUS";
            sqlstr += "         , (SELECT CASE WHEN CTRY_CD LIKE 'KR' THEN 'E' ELSE 'I' END FROM MDM_PORT_MST WHERE LOC_CD = C.POL_CD) AS EX_IM_TYPE";
            sqlstr += "         , C.REQ_SVC";
            sqlstr += "     FROM FMS_TRK_DTL A";
            sqlstr += "               LEFT OUTER JOIN FMS_HBL_MST C ON C.HBL_NO = A.HBL_NO";
            sqlstr += "               INNER JOIN MDM_COM_CODE B ON A.EVENT_CD = B.COMN_CD ";
            sqlstr += "               AND B.GRP_CD  = (CASE WHEN C.REQ_SVC = 'SEA' THEN 'B30' WHEN C.REQ_SVC = 'AIR' THEN 'A30' END ) ";
            sqlstr += "  WHERE ";
            sqlstr += "  A.CNTR_NO = '" + dr["CNTR_NO"] + "'";
            sqlstr += "    AND A.HBL_NO = '" + dr["HBL_NO"] + "'";

            if (dr["REQ_SVC"].ToString() == "SEA")
            {
                if (dr["EX_IM_TYPE"].ToString() == "E")
                {
                    sqlstr += " AND A.EVENT_CD in ('EMT','CIG','LOD','POL','ARR')";
                }
                else if (dr["EX_IM_TYPE"].ToString() == "I")
                {
                    sqlstr += " AND A.EVENT_CD in ('POL','ARR','ICC','CYA','CYD','EMR')";
                }
                else
                {
                    sqlstr += " AND A.EVENT_CD in ('EMT','CNI','CIG','LOD','POL','ARR','ICC','CYA','CYD','EMR')";
                }
            }
            else if (dr["REQ_SVC"].ToString() == "AIR")
            {
                sqlstr += " AND A.EVENT_CD in ('POL','ARR')";
            }

            sqlstr += "  ORDER BY SEQ";
            sqlstr += "  )";

            return sqlstr;
        }

        /// <summary>
        /// HBL_NO CheckBL 확인
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string Query_fnIsCheckBL(DataRow dr)
        {
            sqlstr = "";


            sqlstr += " SELECT A.HBL_NO, ";
            sqlstr += "        A.MBL_NO, ";
            sqlstr += "        A.REQ_SVC, ";
            sqlstr += "        H.CHKBL_YN, ";
            sqlstr += "        (SELECT SUBSTR(KEY_CD,1,INSTR(KEY_CD,'|')-1) FROM MDM_OFFICE_CONFIG WHERE OFFICE_CD = '" + dr["OFFICE_CD"] + "' AND ITEM_CD = '112') AS TOKEN, ";
            sqlstr += "        (SELECT SUBSTR(KEY_CD,INSTR(KEY_CD,'|')+1) FROM MDM_OFFICE_CONFIG WHERE OFFICE_CD = '" + dr["OFFICE_CD"] + "' AND ITEM_CD = '112') AS EXPIREDDT, ";// 토큰 만료 시간 추가 
            sqlstr += "        (SELECT ID FROM MDM_OFFICE_EDI WHERE OFFICE_CD = '" + dr["OFFICE_CD"] + "' AND EDI_SYS_CD = 'SVTG') AS ID, ";
            sqlstr += "        (SELECT PWD FROM MDM_OFFICE_EDI WHERE OFFICE_CD = '" + dr["OFFICE_CD"] + "' AND EDI_SYS_CD = 'SVTG') AS PWD ";
            sqlstr += "            , NVL((SELECT MAX(LINE_BKG_NO) FROM FMS_MBL_OTH WHERE SR_NO = A.SR_NO), A.MBL_NO) AS LINE_BKG_NO	";
            sqlstr += "   FROM FMS_HBL_MST A ";
            sqlstr += "        INNER JOIN FMS_TRK_MST C ";
            sqlstr += "           ON A.HBL_NO = C.HBL_NO ";
            sqlstr += "        INNER JOIN FMS_HBL_OTH H ";
            sqlstr += "           ON A.HBL_NO = H.HBL_NO ";
            sqlstr += "  WHERE 1 = 1 ";

            if (dr["CNTR_NO"].ToString() != "")
            {
                sqlstr += "        AND (A.HBL_NO = '" + dr["HBL_NO"] + "' OR C.CNTR_NO = '" + dr["CNTR_NO"] + "' OR A.MBL_NO = '" + dr["HBL_NO"] + "') ";
            }
            else
            {
                sqlstr += "        AND (A.HBL_NO = '" + dr["HBL_NO"] + "' OR C.CNTR_NO = '" + dr["HBL_NO"] + "' OR A.MBL_NO = '" + dr["HBL_NO"] + "') ";
            }

            return sqlstr;
        }


        public string SetSvtgAuthToken(DataRow dr)
        {
            sqlstr += "  UPDATE MDM_OFFICE_CONFIG SET KEY_CD = '" + dr["TOKEN"] + "' WHERE OFFICE_CD = '" + dr["OFFICE_CD"] + "' AND ITEM_CD = '112'";

            return sqlstr;
        }

        /// <summary>
        /// 로그인 정보 데이터 가져오기
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string GetUserInfo(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT USR_ID, ";
            sqlstr += "        LOC_NM AS USER_NM, ";
            sqlstr += "        EMAIL, ";
            sqlstr += "       HP_NO, ";
            sqlstr += "        CUST_CD, ";
            sqlstr += "        CUST_NM, ";
            sqlstr += "        USR_TYPE, ";
            sqlstr += "        APV_YN, ";
            sqlstr += "        AUTH_KEY, ";
            sqlstr += "        CASE ";
            sqlstr += "           WHEN AUTH_TYPE = 'A' THEN 'A' ";
            sqlstr += "           WHEN AUTH_TYPE = 'C' THEN 'B' ";
            sqlstr += "           ELSE 'C' ";
            sqlstr += "        END ";
            sqlstr += "           AS AUTH_TYPE, ";
            sqlstr += " DEF_OFFICE_CD AS OFFICE_CD ";
            sqlstr += "   FROM MDM_EXT_USR_MST ";
            sqlstr += "   WHERE 1=1 ";
            sqlstr += " AND USE_YN = 'Y' ";
            sqlstr += " AND UPPER(USR_ID) = UPPER('" + dr["USR_ID"] + "') ";
            sqlstr += " AND PSWD = '" + YJIT.Utils.StringUtils.Md5Hash((string)dr["PSWD"]) + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 메일에서 자동로그인 시 로그인 데이터 가져오기
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MailLogin(DataRow dr)
        {
            sqlstr = "";
            sqlstr += " SELECT  B.LOC_NM ";
            sqlstr += "         , B.USR_ID ";
            sqlstr += "         , B.LOC_NM AS USER_NM ";
            sqlstr += "         , B.EMAIL ";
            sqlstr += "         , B.HP_NO ";
            sqlstr += "         , B.CUST_CD ";
            sqlstr += "         , B.CUST_NM ";
            sqlstr += "         , B.USR_TYPE ";
            sqlstr += "         , B.APV_YN ";
            sqlstr += "         , B.AUTH_KEY ";
            sqlstr += "         , AUTH_TYPE ";
            sqlstr += "         , DEF_OFFICE_CD AS OFFICE_CD";
            sqlstr += "         , A.JOB_TYPE AS JOB_TYPE";
            sqlstr += "         , A.DOMAIN AS DOMAIN";
            sqlstr += "         , A.REF1 ";
            sqlstr += "         , A.REF2 ";
            sqlstr += "         , A.REF3 ";
            sqlstr += "         , A.REF4 ";
            sqlstr += "         , A.REF5 ";
            sqlstr += " FROM PRM_EMAIL_LOG A";
            sqlstr += "     INNER JOIN MDM_EXT_USR_MST B ON UPPER(A.RECEIVE_MAIL) = UPPER(B.EMAIL) ";
            sqlstr += " WHERE 1=1 ";
            sqlstr += " AND A.AUTH_KEY = '" + dr["USR_ID"] + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 공지사항 리스트 가져오기
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string Query_GetNoticeList(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT * ";
            sqlstr += "   FROM (SELECT  ";
            sqlstr += "                ROWNUM AS RNUM, ";
            sqlstr += "                FLOOR ( (ROWNUM - 1) / 10 + 1) AS PAGE, ";
            sqlstr += "                COUNT (*) OVER () AS TOTCNT, ";
            sqlstr += "                TOTAL.* ";
            sqlstr += "           FROM (  SELECT  ";
            sqlstr += "                          A.NOTICE_ID, ";
            sqlstr += "                          A.TITLE, ";
            sqlstr += "                          A.REGDT, ";
            sqlstr += "                          A.\"FILE\", ";
            sqlstr += "                          A.FILE_NAME, ";
            sqlstr += "                          A.FILE1, ";
            sqlstr += "                          A.FILE1_NAME, ";
            sqlstr += "                          A.FILE2, ";
            sqlstr += "                          A.FILE2_NAME, ";
            sqlstr += "                          A.CONTENT ";
            sqlstr += "                     FROM NOTICE A ";
            sqlstr += "                     WHERE USE_YN = 'y' ";
            sqlstr += "                 ORDER BY REGDT DESC ";
            sqlstr += "                 )  ";
            sqlstr += "             TOTAL) ";
            sqlstr += "  WHERE PAGE = '" + dr["PAGE"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// Tracking B/L 데이터 가져오기
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string Query_GetSeaTrackingBLData(DataRow dr)
        {
            sqlstr = "";
                        
            sqlstr += "   SELECT TRK.HBL_NO, ";
            sqlstr += "        MAX (TRK.CNTR_NO) AS CNTR_NO, ";
            sqlstr += "        POL_CD, ";
            sqlstr += "        MAX(MST.REQ_SVC) AS REQ_SVC, ";
            sqlstr += "        MAX (AUTH.EX_IM_TYPE) AS EX_IM_TYPE, ";
            sqlstr += "        MAX (COMM.CD_NM) AS NOW_EVENT_NM ";
            sqlstr += "   FROM FMS_HBL_MST MST ";
            sqlstr += "        INNER JOIN FMS_HBL_OTH OTH ";
            sqlstr += "           ON MST.HBL_NO = OTH.HBL_NO ";
            sqlstr += "        INNER JOIN FMS_TRK_DTL TRK ";
            sqlstr += "           ON MST.HBL_NO = TRK.HBL_NO ";
            sqlstr += "        INNER JOIN FMS_TRK_MST TRK_M ";
            sqlstr += "           ON MST.HBL_NO = TRK_M.HBL_NO ";
            sqlstr += "        LEFT JOIN MDM_COM_CODE COMM ";
            sqlstr += "           ON TRK_M.NOW_EVENT_CD = COMM.COMN_CD ";
            sqlstr += "        INNER JOIN FMS_HBL_AUTH AUTH ";
            sqlstr += "           ON MST.HBL_NO = AUTH.HBL_NO AND AUTH.OFFICE_CD = '"+ dr["OFFICE_CD"].ToString() + "' ";
            sqlstr += "   WHERE 1 = 1 ";
            sqlstr += "           AND (TRK.HBL_NO = '" + dr["HBL_NO"].ToString() + "' ";
            if (dr["CNTR_NO"].ToString() != "")
            {
                sqlstr += "       AND TRK.CNTR_NO = '" + dr["CNTR_NO"].ToString() + "' ";
            }
            else
            {
                sqlstr += "       OR TRK.CNTR_NO = '" + dr["HBL_NO"].ToString() + "' ";
            }

            sqlstr += "                       ) ";
            sqlstr += "         GROUP BY TRK.HBL_NO, POL_CD";

            return sqlstr;
        }

        /// <summary>
        /// Tracking B/L 제출 체크 로직 이후 리스트 데이터
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string Query_GetSeaTrackingData(DataRow dr)
        {
            sqlstr = "";

            sqlstr += "  SELECT A.HBL_NO, ";
            sqlstr += "          A.CNTR_NO, ";
            sqlstr += "          MST.MBL_NO, ";
            sqlstr += "          EVENT_CD, ";
            sqlstr += "          B.CD_NM AS EVENT_NM, ";
            sqlstr += "          EST_LOC_CD, ";
            sqlstr += "          EST_LOC_NM, ";
            sqlstr += "          ACT_LOC_CD, ";
            sqlstr += "          ACT_LOC_NM, ";
            sqlstr += "          RPAD (EST_YMD || EST_HM, 14, 0) AS EST, ";
            sqlstr += "          RPAD (ACT_YMD || ACT_HM, 14, 0) AS ACT, ";
            sqlstr += "          ACT_YMD, ";
            sqlstr += "          ACT_HM, ";
            sqlstr += "          EST_YMD, ";
            sqlstr += "          EST_HM, ";
            sqlstr += "          CASE ";
            sqlstr += "          WHEN (SELECT COUNT(*) ";
            sqlstr += "          FROM FMS_TRK_MST MST ";
            sqlstr += "          INNER JOIN FMS_TRK_DTL DTL ON MST.HBL_NO = DTL.HBL_NO    ";
            sqlstr += "          WHERE A.HBL_NO = MST.HBL_NO ";
            sqlstr += "          AND DTL.SEQ = A.SEQ ";
            sqlstr += "          AND MST.NOW_EVENT_CD = DTL.EVENT_CD ";
            sqlstr += "           ) > 0 THEN 'Y' ";
            sqlstr += "             WHEN (SELECT COUNT (*) ";
            sqlstr += "                     FROM FMS_TRK_DTL DTL ";
            sqlstr += "                    WHERE     DTL.SEQ >= A.SEQ ";
            sqlstr += "                          AND A.HBL_NO = DTL.HBL_NO ";
            sqlstr += "                          AND A.CNTR_NO = DTL.CNTR_NO ";
            sqlstr += "                          AND NOT DTL.ACT_YMD IS NULL ";
            sqlstr += "                          AND DTL.ACT_YMD <= TO_CHAR (SYSDATE, 'yyyyMMdd')) > 0 ";
            sqlstr += "             THEN ";
            sqlstr += "                'E' ";
            sqlstr += "             WHEN NOT A.ACT_YMD IS NULL ";
            sqlstr += "                  AND (SELECT MAX (SEQ) ";
            sqlstr += "                         FROM FMS_TRK_DTL DTL ";
            sqlstr += "                        WHERE A.HBL_NO = DTL.HBL_NO AND A.CNTR_NO = DTL.CNTR_NO) = ";
            sqlstr += "                         A.SEQ ";
            sqlstr += "                  AND A.ACT_YMD <= TO_CHAR (SYSDATE, 'yyyyMMdd') ";
            sqlstr += "             THEN ";
            sqlstr += "                'E' ";
            sqlstr += "             WHEN     NOT A.ACT_YMD IS NULL ";
            sqlstr += "                  AND A.ACT_YMD <= TO_CHAR (SYSDATE, 'yyyyMMdd') ";
            sqlstr += "                  AND (SELECT COUNT (*) ";
            sqlstr += "                         FROM FMS_TRK_DTL DTL ";
            sqlstr += "                        WHERE     DTL.SEQ > A.SEQ ";
            sqlstr += "                              AND A.HBL_NO = DTL.HBL_NO ";
            sqlstr += "                              AND A.CNTR_NO = DTL.CNTR_NO ";
            sqlstr += "                              AND (DTL.ACT_YMD IS NULL ";
            sqlstr += "                                   OR DTL.ACT_YMD <= ";
            sqlstr += "                                         TO_CHAR (SYSDATE, 'yyyyMMdd'))) = 0 ";
            sqlstr += "             THEN ";
            sqlstr += "                'Y' ";
            sqlstr += "             WHEN (A.ACT_YMD IS NULL ";
            sqlstr += "                   OR A.ACT_YMD > TO_CHAR (SYSDATE, 'yyyyMMdd')) ";
            sqlstr += "             THEN ";
            sqlstr += "                'N' ";
            sqlstr += "             ELSE ";
            sqlstr += "                'N' ";
            sqlstr += "          END ";
            sqlstr += "             AS ACT_EVT_CD, ";
            sqlstr += "          MST.COLD_TYPE ";
            sqlstr += "            , NVL((SELECT MAX(LINE_BKG_NO) FROM FMS_MBL_OTH WHERE SR_NO = MST.SR_NO), MST.MBL_NO) AS LINE_BKG_NO	";
            sqlstr += "     FROM FMS_HBL_MST MST ";
            sqlstr += "          INNER JOIN FMS_TRK_DTL A ";
            sqlstr += "             ON MST.HBL_NO = A.HBL_NO ";
            sqlstr += "          INNER JOIN MDM_COM_CODE B ";
            sqlstr += "             ON A.EVENT_CD = B.COMN_CD AND B.GRP_CD = (CASE WHEN MST.REQ_SVC = 'SEA' THEN 'B30' WHEN MST.REQ_SVC = 'AIR' THEN 'A30' END ) ";
            sqlstr += "             WHERE 1=1 ";
            sqlstr += "                 AND MST.HBL_NO = '" + dr["HBL_NO"].ToString() + "'             ";
            sqlstr += "                 AND A.CNTR_NO = '" + dr["CNTR_NO"].ToString() + "'             ";

            if (dr["REQ_SVC"].ToString() == "AIR")
            {
                sqlstr += "             AND A.EVENT_CD in ('POL','ARR')";
            }
            else if (dr["REQ_SVC"].ToString() == "SEA")
            {
                if (dr["EX_IM_TYPE"].ToString() == "E")
                {
                    sqlstr += "             AND A.EVENT_CD in ('EMT','CIG','LOD','POL','ARR')";
                }
                else if (dr["EX_IM_TYPE"].ToString() == "I")
                {
                    sqlstr += "             AND A.EVENT_CD in ('POL','ARR','ICC','CYA','CYD','EMR')";
                }
                else
                {
                    sqlstr += "             AND A.EVENT_CD in ('EMT','CIG','LOD','POL','ARR','ICC','CYA','CYD','EMR')";
                }
            }

            sqlstr += " ORDER BY A.HBL_NO, A.SEQ ";

            return sqlstr;
        }

        /// <summary>
        /// Tracking B/L 데이터 가져오기
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string Query_GetAirTrackingBLData(DataRow dr)
        {
            sqlstr = "";

            sqlstr += "	SELECT A.HBL_NO		";
            sqlstr += "          , A.CNTR_NO		";
            sqlstr += "          , A.MBL_NO		";
            sqlstr += "          , (SELECT CD_NM FROM MDM_COM_CODE WHERE COMN_CD = A.NOW_EVENT_CD AND GRP_CD = CASE WHEN A.REQ_SVC = 'SEA' THEN 'B30' WHEN A.REQ_SVC = 'AIR' THEN 'A30' END) AS NOW_EVENT_NM		";
            sqlstr += "          , ACT_LOC_NM	";
            sqlstr += "          , A.EX_IM_TYPE 		";
            sqlstr += "          , A.REQ_SVC 		";
            sqlstr += "    FROM(  		";
            sqlstr += "    SELECT A.HBL_NO		";
            sqlstr += "             , A.CNTR_NO		";
            sqlstr += "             , MAX(C.MBL_NO) AS MBL_NO		";
            sqlstr += "             , MAX(NOW_EVENT_CD) AS NOW_EVENT_CD		";
            sqlstr += "             , MAX(LAST_EVENT_CD) AS LAST_EVENT_CD   ";
            sqlstr += "             , MAX(B.ACT_LOC_CD) AS ACT_LOC_CD		";
            sqlstr += "             , MAX(B.ACT_LOC_NM) AS ACT_LOC_NM		";
            sqlstr += "             , MAX(C.REQ_SVC) AS REQ_SVC		";
            sqlstr += "             , MAX(C.POL_CD) AS POL_CD		";
            sqlstr += "             , MAX(E.EX_IM_TYPE) AS EX_IM_TYPE		";
            sqlstr += "       FROM FMS_TRK_MST A		";
            sqlstr += "                LEFT OUTER JOIN FMS_TRK_DTL B ON A.HBL_NO = B.HBL_NO		";
            sqlstr += "                LEFT OUTER JOIN FMS_HBL_MST C ON A.HBL_NO = C.HBL_NO		";
            sqlstr += "                LEFT OUTER JOIN FMS_HBL_OTH D ON A.HBL_NO = D.HBL_NO		";
            sqlstr += "                LEFT OUTER JOIN FMS_HBL_AUTH E ON A.HBL_NO = E.HBL_NO AND E.OFFICE_CD = '"+ dr["OFFICE_CD"] + "'	";
            sqlstr += "                WHERE 1 = 1		";
            sqlstr += "                AND D.CHKBL_YN = 'Y'		";

            if (dr["HBL_NO"].ToString() != "" && dr["CNTR_NO"].ToString() != "")
            {
                sqlstr += "     AND ((A.HBL_NO = '" + dr["HBL_NO"] + "' OR C.MBL_NO = '" + dr["HBL_NO"] + "') AND A.CNTR_NO = '" + dr["CNTR_NO"] + "')		";
            }
            else if (dr["HBL_NO"].ToString() != "")
            {
                sqlstr += "     AND (A.HBL_NO = '" + dr["HBL_NO"] + "' OR C.MBL_NO = '" + dr["HBL_NO"] + "')		";
            }
            else if (dr["CNTR_NO"].ToString() != "")
            {
                sqlstr += "      AND (A.CNTR_NO = '" + dr["CNTR_NO"] + "')		";
            }
            else
            {
                sqlstr += "     AND (A.HBL_NO = '" + dr["HBL_NO"] + "' OR A.CNTR_NO = '" + dr["HBL_NO"] + "' OR C.MBL_NO = '" + dr["HBL_NO"] + "')		";
            }

            sqlstr += "                GROUP BY A.HBL_NO,A.CNTR_NO		";
            sqlstr += "	 )A		";

            return sqlstr;
        }

        /// <summary>
        /// Tracking B/L 제출 체크 로직 이후 리스트 데이터
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string Query_GetAirTrackingData(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT EVENT_CD	";
            sqlstr += "          ,  EVENT_NM";
            sqlstr += "          ,  ACT_LOC_CD";
            sqlstr += "          ,  TO_CHAR(TO_DATE(ACT_YMD),'YYYY.MM.DD') AS ACT_YMD ";
            sqlstr += "          ,  TO_CHAR(TO_DATE(ACT_HM,'HH24MISS'),'HH24:MI') AS ACT_HM";
            sqlstr += "          ,  TO_CHAR(TO_DATE(EST_YMD),'YYYY.MM.DD') AS EST_YMD ";
            sqlstr += "          ,  TO_CHAR(TO_DATE(EST_HM,'HH24MISS'),'HH24:MI') AS EST_HM";
            sqlstr += "          ,  ACT_LOC_NM";
            sqlstr += "          , EVENT_STATUS";
            sqlstr += "          , EX_IM_TYPE";
            sqlstr += "          , REQ_SVC";
            sqlstr += "          FROM (";
            sqlstr += " SELECT EVENT_CD	";
            sqlstr += "          ,  B.CD_NM AS EVENT_NM";
            sqlstr += "          ,  ACT_LOC_CD";
            sqlstr += "          ,  ACT_YMD";
            sqlstr += "          ,  ACT_HM";
            sqlstr += "          ,  EST_YMD";
            sqlstr += "          ,  EST_HM";
            sqlstr += "          ,  ACT_LOC_NM";
            sqlstr += "          , CASE WHEN (SELECT NOW_EVENT_CD FROM FMS_TRK_MST WHERE CNTR_NO = A.CNTR_NO AND HBL_NO = A.HBL_NO)  = A.EVENT_CD THEN 'Y' ";
            sqlstr += "                 WHEN ACT_YMD IS NOT NULL OR ACT_HM IS NOT NULL THEN 'E'";
            sqlstr += "                 ELSE 'N' END AS EVENT_STATUS";
            sqlstr += "         , '"+dr["EX_IM_TYPE"]+"' AS EX_IM_TYPE";
            sqlstr += "         , C.REQ_SVC";
            sqlstr += "     FROM FMS_TRK_DTL A";
            sqlstr += "               LEFT OUTER JOIN FMS_HBL_MST C ON C.HBL_NO = A.HBL_NO";            
            sqlstr += "               INNER JOIN MDM_COM_CODE B ON A.EVENT_CD = B.COMN_CD ";
            sqlstr += "               AND B.GRP_CD  = (CASE WHEN C.REQ_SVC = 'SEA' THEN 'B30' WHEN C.REQ_SVC = 'AIR' THEN 'A30' END ) ";
            sqlstr += "  WHERE ";
            sqlstr += "  A.CNTR_NO = '" + dr["CNTR_NO"] + "'";
            sqlstr += "    AND A.HBL_NO = '" + dr["HBL_NO"] + "'";
            sqlstr += "  ORDER BY SEQ";
            sqlstr += "  )";

            return sqlstr;
        }

    }
}
