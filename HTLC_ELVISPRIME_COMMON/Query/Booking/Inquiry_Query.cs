using System.Data;

namespace HTLC_ELVISPRIME_COMMON.Query.Booking
{
    public class Inquiry_Query
    {
        string sqlstr;

        /// <summary>
        /// 부킹 조회 데이터 가져오기.
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string fnGetBkgData_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT * ";
            sqlstr += "   FROM (SELECT ROWNUM AS RNUM, ";
            sqlstr += "                FLOOR ( (ROWNUM - 1) / 10 + 1) AS PAGE, ";
            sqlstr += "                COUNT (*) OVER () AS TOTCNT, ";
            sqlstr += "                TOTAL.* ";
            sqlstr += "           FROM (SELECT A.BKG_NO, ";
            sqlstr += "                        A.SCH_NO, ";
            sqlstr += "                        A.HBL_NO, ";
            sqlstr += "                        (SELECT CHKBL_YN FROM FMS_HBL_OTH WHERE HBL_NO = A.HBL_NO) AS HBL_YN, "; //제출을 누르지 않았을 경우 리스트에 보여주지 않는다 
            sqlstr += "                        A.STATUS, ";
            sqlstr += "                        B.REQ_SVC, ";
            sqlstr += "                        CASE WHEN B.CNTR_TYPE = 'F' THEN 'FCL' WHEN B.CNTR_TYPE = 'L' THEN 'LCL' WHEN B.CNTR_TYPE = 'B' THEN 'BULK' END AS CNTR_TYPE, "; 
            sqlstr += "                        B.LINE_CD, ";
            sqlstr += "                        (SELECT CARR_NM ";
            sqlstr += "                           FROM MDM_CARR_MST ";
            sqlstr += "                          WHERE CARR_CD = B.LINE_CD) ";
            sqlstr += "                           AS LINE_NM, ";
            sqlstr += "                        VSL || ' ' || VOY AS VSL_VOY, ";
            sqlstr += "                        B.ETD, ";
            sqlstr += "                        B.ETA, ";
            sqlstr += "                        B.POL_CD, ";
            sqlstr += "                (SELECT LOC_NM ";
            sqlstr += "                   FROM MDM_PORT_MST ";
            sqlstr += "                  WHERE LOC_CD = B.POL_CD) ";
            sqlstr += "                   AS POL_TRMN, ";
            sqlstr += "                        B.POD_CD, ";
            sqlstr += "                (SELECT LOC_NM ";
            sqlstr += "                   FROM MDM_PORT_MST ";
            sqlstr += "                  WHERE LOC_CD = B.POD_CD) ";
            sqlstr += "                   AS POD_TRMN, ";
            sqlstr += "                        A.CURR_CD, ";
            sqlstr += "                        A.PRC, ";
            sqlstr += "                        B.POL_TML_NM, ";
            sqlstr += "                        B.SCH_PIC, ";
            sqlstr += "                        B.RMK ";
            sqlstr += "                   FROM PRM_BKG_MST A ";
            sqlstr += "                        INNER JOIN PRM_SCH_MST B ";
            sqlstr += "                           ON A.SCH_NO = B.SCH_NO ";
            sqlstr += "                        INNER JOIN MDM_CARR_MST CARR ";
            sqlstr += "                           ON B.LINE_CD = CARR.CARR_CD  ";

            sqlstr += "   WHERE 1=1 ";

            if (dr["AUTH_KEY"].ToString() == "")
            {
                if (dr["USER_TYPE"].ToString() != "M")
                {
                    sqlstr += "   AND A.CUST_CD = '" + dr["CUST_CD"].ToString() + "'";
                }
            }

            if (dr.Table.Columns.Contains("MNGT_NO"))
            {
                if (dr["MNGT_NO"].ToString() != "")
                {
                    sqlstr += "    AND A.BKG_NO = '" + dr["MNGT_NO"].ToString() + "'";
                }
            }
            else
            {
                sqlstr += "  AND A.REQ_SVC = '" + dr["REQ_SVC"].ToString() + "'";

                if (dr["REQ_SVC"].ToString() != "AIR")
                {
                    sqlstr += "  AND B.LINE_TYPE = '" + dr["LINE_TYPE"].ToString() + "'";
                }

                if (dr["DATE_TYPE"].ToString() == "ALL")
                {
                    sqlstr += "  AND ((A.INS_YMD >= '" + dr["ETD"].ToString() + "'";
                    sqlstr += "  AND A.INS_YMD <= '" + dr["ETA"].ToString() + "')";
                    sqlstr += "  OR (B.ETD >= '" + dr["ETD"].ToString() + "'";
                    sqlstr += "  AND B.ETD <= '" + dr["ETA"].ToString() + "')";
                    sqlstr += " OR (B.ETA >= '" + dr["ETD"].ToString() + "'";
                    sqlstr += " AND B.ETA <= '" + dr["ETA"].ToString() + "'))";
                }
                else
                {
                    if (dr["DATE_TYPE"].ToString() == "BK")
                    {
                        sqlstr += "  AND A.INS_YMD >= '" + dr["ETD"].ToString() + "'";
                        sqlstr += "  AND A.INS_YMD <= '" + dr["ETA"].ToString() + "'";
                    }
                    else if (dr["DATE_TYPE"].ToString() == "ETD")
                    {
                        sqlstr += "  AND B.ETD >= '" + dr["ETD"].ToString() + "'";
                        sqlstr += "  AND B.ETD <= '" + dr["ETA"].ToString() + "'";
                    }
                    else if (dr["DATE_TYPE"].ToString() == "ETA")
                    {
                        sqlstr += " AND  B.ETA >= '" + dr["ETD"].ToString() + "'";
                        sqlstr += " AND  B.ETA <= '" + dr["ETA"].ToString() + "'";
                    }
                }                

                if (dr["DETAIL"].ToString() == "Y")
                {
                    if (dr["POL_CD"].ToString() == "")
                    {
                        sqlstr += "    AND ( (UPPER('" + dr["POL"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POL"] + "') IS NOT NULL and B.POL_CD LIKE UPPER('%" + dr["POL"] + "%')) or (UPPER('" + dr["POL"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = B.POL_CD) LIKE UPPER('%" + dr["POL"] + "%') ) )";
                    }
                    else if (dr["POL"].ToString() != "")
                    {
                        sqlstr += "    AND ( (UPPER('" + dr["POL"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POL"] + "') IS NOT NULL and B.POL_CD LIKE UPPER('%" + dr["POL_CD"] + "%') ) or (UPPER('" + dr["POL"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = B.POL_CD) LIKE UPPER('%" + dr["POL"] + "%') ) )";
                    }

                    if (dr["POD_CD"].ToString() == "")
                    {
                        sqlstr += "    AND ( (UPPER('" + dr["POD"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and B.POD_CD LIKE UPPER('%" + dr["POD"] + "%') ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = B.POD_CD) LIKE UPPER('%" + dr["POD"] + "%') ) )";
                    }
                    else if (dr["POD"].ToString() != "")
                    {
                        sqlstr += "    AND ( (UPPER('" + dr["POD"] + "') IS NULL and 1 = 1 ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and B.POD_CD LIKE UPPER('%" + dr["POD_CD"] + "%') ) or (UPPER('" + dr["POD"] + "') IS NOT NULL and (SELECT LOC_NM FROM MDM_PORT_MST WHERE LOC_CD = B.POD_CD) LIKE UPPER('%" + dr["POD"] + "%') ) )";
                    }

                    if (dr["STATUS"].ToString() != "ALL")
                    {
                        sqlstr += "  AND A.STATUS = '" + dr["STATUS"].ToString() + "'";
                    }

                    if (dr["BKG_NO"].ToString() != "")
                    {
                        sqlstr += "  AND A.BKG_NO = '" + dr["BKG_NO"].ToString() + "'";
                    }
                }
                //else
                //{
                //    sqlstr += "  AND A.STATUS IN ('Y','Q','F')";
                //}
            }

            if (dr["ID"].ToString() != "")
            {
                sqlstr += " ORDER BY " + dr["ID"].ToString() + " " + dr["ORDER"].ToString() + "";
            }
            else
                sqlstr += " ORDER BY B.ETA DESC";

            sqlstr += ") TOTAL) ";

            sqlstr += "  WHERE PAGE = '" + dr["PAGE"].ToString() + "' ";

            return sqlstr;
        }        

        /// <summary>
        /// 부킹 상태 취소로 변경
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string fnSetCancelStatus_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " UPDATE PRM_BKG_MST SET STATUS = 'C' ";
            sqlstr += " WHERE BKG_NO = '" + dr["BKG_NO"].ToString() + "' ";

            return sqlstr;
        }

        public string fnGetBookingStatus_Query()
        {
            sqlstr = "";

            sqlstr += " SELECT COMN_CD AS CODE , CD_NM AS NAME ";
            sqlstr += "   FROM MDM_COM_CODE ";
            sqlstr += "  WHERE 1 = 1  ";
            sqlstr += "  AND GRP_CD = 'R09'  ";
            sqlstr += "  AND OPT_ITEM1 IN ('A', 'W') ";

            return sqlstr;
        }





    }
}
