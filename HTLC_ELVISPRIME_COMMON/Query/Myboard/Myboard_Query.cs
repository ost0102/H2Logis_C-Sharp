using System.Data;

namespace HTLC_ELVISPRIME_COMMON.Query.Myboard
{
    public class Myboard_Query
    {
        string sqlstr;

        /// <summary>
        /// 마이보드 데이터 가져오기
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string GetBoardList_Query(DataRow dr)
        {

            sqlstr += " SELECT * ";
            sqlstr += "   FROM (SELECT ROWNUM AS RNUM, ";
            sqlstr += "                FLOOR ( (ROWNUM - 1) / 10 + 1) AS PAGE, ";
            sqlstr += "                COUNT (*) OVER () AS TOTCNT, ";
            sqlstr += "                HBL_NO, ";
            sqlstr += "                MBL_NO, ";
            sqlstr += "                POL_CD, ";
            sqlstr += "                POD_CD, ";
            sqlstr += "                REQ_SVC, ";
            sqlstr += "                EX_IM_TYPE, ";
            sqlstr += "                ETD, ";
            sqlstr += "                ETA, ";
            sqlstr += "                POL_NM, ";
            sqlstr += "                POD_NM, ";
            sqlstr += "                FILE_CNT, ";
            sqlstr += "                INV_NO, ";
            sqlstr += "                BKG_NO,	";
            sqlstr += "                VSL, ";
            sqlstr += "                (SELECT INV_YN ";
            sqlstr += "                   FROM ACT_INV_MST ";
            sqlstr += "                  WHERE INV_NO = A.INV_NO) ";
            sqlstr += "                   AS INV_YN ";
            sqlstr += "           FROM (  SELECT A.HBL_NO, ";
            sqlstr += "                          A.MBL_NO, ";
            sqlstr += "                          A.POL_CD, ";
            sqlstr += "                          A.REQ_SVC, ";
            sqlstr += "                          A.POL_NM, ";
            sqlstr += "                          A.POD_NM, ";
            sqlstr += "                          D.EX_IM_TYPE, ";
            sqlstr += "                          A.POD_CD, ";
            sqlstr += "                          TO_CHAR (TO_DATE (A.ETD), 'YYYY.MM.DD') AS ETD, ";
            sqlstr += "                          TO_CHAR (TO_DATE (A.ETA), 'YYYY.MM.DD') AS ETA,                          ";
            sqlstr += "                           B.BKG_NO,	";
            sqlstr += "                          (SELECT COUNT (MAX (SEQ)) ";
            sqlstr += "                          		FROM COM_DOC_MST ";
            sqlstr += "                          	WHERE MNGT_NO = A.HBL_NO ";
            sqlstr += "                          			AND DOC_TYPE IN (" + dr["HBL_DOC_TYPE"].ToString() + ") ";
            sqlstr += "                          	GROUP BY DOC_TYPE) ";
            sqlstr += "                          	+ (  SELECT COUNT (MAX (SEQ)) ";
            sqlstr += "                          		FROM COM_DOC_MST ";
            sqlstr += "                          		WHERE MNGT_NO = (SELECT MBL_NO ";
            sqlstr += "                          							FROM FMS_HBL_MST ";
            sqlstr += "                          						WHERE HBL_NO = A.HBL_NO) ";
            sqlstr += "                          			AND DOC_TYPE IN (" + dr["MBL_DOC_TYPE"].ToString() + ") ";
            sqlstr += "                          	GROUP BY DOC_TYPE) + ";
            sqlstr += "                              (SELECT COUNT (FILE_NM)	";
            sqlstr += "                                 FROM COM_DOC_MST	";
            sqlstr += "                                WHERE MNGT_NO = B.BKG_NO) +	";
            sqlstr += "                          (SELECT COUNT (MAX (SEQ)) ";
            sqlstr += "                          		FROM ACT_INV_MST MST ";
            sqlstr += "                          		 INNER JOIN ACT_INV_DTL DTL ";
            sqlstr += "                          		  ON MST.INV_NO = DTL.INV_NO ";
            sqlstr += "                          		  INNER JOIN COM_DOC_MST DOC ";
            sqlstr += "                          		  ON DTL.MBL_HBL_NO = DOC.MNGT_NO ";
            sqlstr += "                          		  WHERE MST.INV_NO = (SELECT MAX (INV_NO) FROM ACT_INV_DTL WHERE MBL_HBL_NO = A.HBL_NO) ";
            sqlstr += "                          		  AND DOC.DOC_TYPE = 'INV' AND MST.INV_YN = 'Y' ";            
            sqlstr += "                          	GROUP BY DOC_TYPE) ";
            sqlstr += "                          	AS FILE_CNT, ";
            sqlstr += "                          (SELECT MAX (INV_NO) ";
            sqlstr += "                             FROM ACT_INV_DTL ";
            sqlstr += "                            WHERE MBL_HBL_NO = A.HBL_NO) ";
            sqlstr += "                             AS INV_NO, ";
            sqlstr += "                          A.VSL ";
            sqlstr += "                     FROM FMS_HBL_MST A ";
            sqlstr += "                         LEFT OUTER JOIN PRM_BKG_MST B	"; //20220705 twkim - WebBooking이 아니여도 B/L을 보여져야되는 업체 요청으로 INNER 가 아닌 LEFT로 변경
            sqlstr += "                             ON A.HBL_NO = B.HBL_NO	";
            sqlstr += "                          INNER JOIN FMS_HBL_OTH C ";
            sqlstr += "                             ON A.HBL_NO = C.HBL_NO ";
            sqlstr += "                          LEFT JOIN FMS_HBL_AUTH D ";
            sqlstr += "                             ON A.HBL_NO = D.HBL_NO AND D.OFFICE_CD = '" + dr["OFFICE_CD"].ToString() + "' ";
            sqlstr += "              WHERE C.CHKBL_YN = 'Y' ";

            if (dr["AUTH_KEY"].ToString() == "")
            {
                if (dr["USER_TYPE"].ToString() != "M")
                {
                    sqlstr += "   AND D.CUST_CD = '" + dr["CUST_CD"].ToString() + "'";
                }
            }

            if (dr["ETD_ETA"].ToString() == "ETD_ETA")
            {
                sqlstr += " AND ((C.EX_IM_TYPE = 'E' AND A.ETD >= '" + dr["STRT_YMD"].ToString() + "' AND A.ETD <= '" + dr["END_YMD"].ToString() + "')  OR (C.EX_IM_TYPE = 'I' AND A.ETA >= '" + dr["STRT_YMD"].ToString() + "'AND A.ETA <= '" + dr["END_YMD"].ToString() + "'))";
            }
            else
            {
                if (dr["STRT_YMD"].ToString() != "")
                {
                    sqlstr += "    AND A." + dr["ETD_ETA"].ToString() + " >= '" + dr["STRT_YMD"].ToString() + "'";
                }
                if (dr["END_YMD"].ToString() != "")
                {
                    sqlstr += "    AND A." + dr["ETD_ETA"].ToString() + " <= '" + dr["END_YMD"].ToString() + "'";
                }
            }

            sqlstr += "    AND D.EX_IM_TYPE = '" + dr["EX_IM_TYPE"].ToString() + "'";

            sqlstr += "    AND ( ('" + dr["REQ_SVC"].ToString() + "' IS NULL and 1 = 1 ) or ('" + dr["REQ_SVC"].ToString() + "' IS NOT NULL and A.REQ_SVC = '" + dr["REQ_SVC"].ToString() + "' ) )";
            sqlstr += "    AND ( ('" + dr["HBL_NO"].ToString() + "' IS NULL and 1 = 1 )or ('" + dr["HBL_NO"].ToString() + "' IS NOT NULL and A.HBL_NO LIKE '%" + dr["HBL_NO"].ToString() + "%' ) )";

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
                sqlstr += " ORDER BY " + dr["ID"] + " " + dr["ORDER"] + "";
            }
            else
                sqlstr += " ORDER BY ETA DESC";
            sqlstr += " )A)	";
            sqlstr += " WHERE PAGE = " + dr["PAGE"];

            return sqlstr;
        }

    }
}
