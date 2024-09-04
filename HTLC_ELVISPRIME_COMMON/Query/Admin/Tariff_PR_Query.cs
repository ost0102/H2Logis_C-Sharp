using System;
using System.Data;

namespace HTLC_ELVISPRIME_COMMON.Query.Admin
{
    public class Tariff_PR_Query
    {   
        string sqlstr;

        /// <summary>
        /// 분기 / 년 데이터 가져오기
        /// </summary>
        /// <returns></returns>
        public string SetPRYearQuarter_Query()
        {
            sqlstr = "";

            sqlstr += " SELECT A.PERIOD_YEAR, ";
            sqlstr += "        A.PERIOD_QUARTER, ";
            sqlstr += "        A.PERIOD, ";
            sqlstr += "        (SELECT OPT_ITEM2 ";
            sqlstr += "           FROM MDM_COM_CODE ";
            sqlstr += "          WHERE GRP_CD = 'P05' AND OPT_ITEM1 = A.PERIOD) ";
            sqlstr += "           AS PERIOD_NAME ";
            sqlstr += "   FROM (  SELECT PERIOD_YEAR, ";
            sqlstr += "                  PERIOD_QUARTER, ";
            sqlstr += "                  PERIOD_YEAR || PERIOD_QUARTER AS PERIOD ";
            sqlstr += "             FROM PRM_SAFE_FRE_PR ";
            sqlstr += "         GROUP BY PERIOD_YEAR, PERIOD_QUARTER ";
            sqlstr += "         ORDER BY PERIOD_YEAR DESC, PERIOD_QUARTER DESC) A ";

            return sqlstr;
        }

        /// <summary>
        /// 안전운임제 할증 관리 검색
        /// </summary>
        /// <returns></returns>
        public string SearchTariffPR_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT * ";
            sqlstr += "   FROM (SELECT ROWNUM AS RNUM, ";
            sqlstr += "                FLOOR ( (ROWNUM - 1) / 10 + 1) AS PAGE, ";
            sqlstr += "                COUNT (*) OVER () AS TOTCNT, ";
            sqlstr += "                TOTAL.*, ";
            sqlstr += "                (SELECT OPT_ITEM2 ";
            sqlstr += "                   FROM MDM_COM_CODE ";
            sqlstr += "                  WHERE GRP_CD = 'P05' AND OPT_ITEM1 = TOTAL.PERIOD) ";
            sqlstr += "                   AS PERIOD_NAME ";
            sqlstr += "           FROM (SELECT MNGT_NO, ";
            sqlstr += "                        SEQ, ";
            sqlstr += "                        PERIOD_YEAR, ";
            sqlstr += "                        PERIOD_QUARTER, ";
            sqlstr += "                        PERIOD_YEAR || PERIOD_QUARTER AS PERIOD, ";
            sqlstr += "                        P_RATE_NAME, ";
            sqlstr += "                        P_RATE_PRICE, ";
            sqlstr += "                        P_RATE_WON, ";
            sqlstr += "                        EXCEPTION ";
            sqlstr += "                   FROM PRM_SAFE_FRE_PR ";
            sqlstr += "                   WHERE 1=1 ";
            sqlstr += "                   AND PERIOD_YEAR = '" + dr["YEAR"].ToString() + "'                   ";
            sqlstr += "                   AND PERIOD_QUARTER = '" + dr["QUARTER"].ToString() + "'                   ";
            sqlstr += "                   ) TOTAL) ";
            sqlstr += "  WHERE PAGE = "+ dr["PAGE"].ToString() + " ";

            return sqlstr;
        }

        /// <summary>
        /// 안전운임제 할증 관리 INSERT
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string InsertTariffPR_Query(DataRow dr)
        {
            sqlstr = "";

            string strMngt_NO = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            sqlstr += " INSERT INTO PRM_SAFE_FRE_PR (MNGT_NO, ";
            sqlstr += "                              SEQ, ";
            sqlstr += "                              PERIOD_YEAR, ";
            sqlstr += "                              PERIOD_QUARTER, ";
            sqlstr += "                              P_RATE_NAME, ";
            sqlstr += "                              P_RATE_PRICE, ";
            sqlstr += "                              P_RATE_WON, ";
            sqlstr += "                              EXCEPTION, ";
            sqlstr += "                              INS_USR, ";
            sqlstr += "                              INS_YMD, ";
            sqlstr += "                              INS_HM, ";
            sqlstr += "                              UPD_USR, ";
            sqlstr += "                              UPD_YMD, ";
            sqlstr += "                              UPD_HM) ";
            sqlstr += "      VALUES ('"+ strMngt_NO + "', ";
            sqlstr += "              (SELECT NVL (MAX (SEQ), 0) + 1 ";
            sqlstr += "                 FROM PRM_SAFE_FRE_PR ";
            sqlstr += "                WHERE MNGT_NO = '"+ strMngt_NO + "'), ";
            sqlstr += "              '"+ dr["YEAR"].ToString() + "', ";
            sqlstr += "              '"+ dr["QUARTER"].ToString() + "', ";
            sqlstr += "              '"+ dr["P_RATE_NAME"].ToString() + "', ";
            sqlstr += "              '"+ dr["P_RATE_PRICE"].ToString() + "', ";
            sqlstr += "              '"+ dr["P_RATE_WON"].ToString() + "', ";
            sqlstr += "              '"+ dr["EXCEPTION"].ToString() + "', ";
            sqlstr += "              '"+ dr["USR_ID"].ToString() + "', ";
            sqlstr += "               UFN_DATE_FORMAT('DATE'), ";
            sqlstr += "               UFN_DATE_FORMAT('TIME'), ";
            sqlstr += "              '"+ dr["USR_ID"].ToString() + "', ";
            sqlstr += "               UFN_DATE_FORMAT('DATE'), ";
            sqlstr += "               UFN_DATE_FORMAT('TIME') ";
            sqlstr += "              ) ";

            return sqlstr;
        }

        /// <summary>
        /// 안전운임제 할증 관리 DELETE
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string DeleteTariffPR_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " DELETE FROM PRM_SAFE_FRE_PR ";
            sqlstr += "       WHERE MNGT_NO = '" + dr["MNGT_NO"].ToString() + "'  ";
            sqlstr += "       AND SEQ = '" + dr["SEQ"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 차량 제원 - 수정 데이터 검색
        /// </summary>
        /// <returns></returns>
        public string SearchTariffPRModify_Query(string MNGT_NO, string SEQ)
        {
            sqlstr = "";

            sqlstr += " SELECT MNGT_NO, ";
            sqlstr += "        SEQ, ";
            sqlstr += "        PERIOD_YEAR, ";
            sqlstr += "        PERIOD_QUARTER, ";
            sqlstr += "        P_RATE_NAME, ";
            sqlstr += "        P_RATE_PRICE, ";
            sqlstr += "        P_RATE_WON, ";
            sqlstr += "        EXCEPTION ";
            sqlstr += "   FROM PRM_SAFE_FRE_PR ";
            sqlstr += "  WHERE 1 = 1  ";
            sqlstr += "     AND MNGT_NO = '" + MNGT_NO + "'  ";
            sqlstr += "     AND SEQ = '" + SEQ + "' ";

            return sqlstr;
        }


        /// <summary>
        /// 안전운임제 할증 관리 UPDATE
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string UpdateTariffPR_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " UPDATE PRM_SAFE_FRE_PR ";
            sqlstr += "    SET  ";
            sqlstr += "        PERIOD_YEAR = '" + dr["YEAR"].ToString() + "', ";
            sqlstr += "        PERIOD_QUARTER = '" + dr["QUARTER"].ToString() + "', ";
            sqlstr += "        P_RATE_NAME = '" + dr["P_RATE_NAME"].ToString() + "', ";
            sqlstr += "        P_RATE_PRICE = '" + dr["P_RATE_PRICE"].ToString() + "', ";
            sqlstr += "        P_RATE_WON = '" + dr["P_RATE_WON"].ToString() + "', ";
            sqlstr += "        EXCEPTION = '" + dr["EXCEPTION"].ToString() + "', ";
            sqlstr += "        UPD_USR = '" + dr["USR_ID"].ToString() + "', ";
            sqlstr += "        UPD_YMD = UFN_DATE_FORMAT('DATE'), ";
            sqlstr += "        UPD_HM = UFN_DATE_FORMAT('TIME') ";
            sqlstr += "  WHERE  ";
            sqlstr += "     MNGT_NO = '" + dr["MNGT_NO"].ToString() + "'  ";
            sqlstr += "     AND SEQ = '" + dr["SEQ"].ToString() + "' ";

            return sqlstr;
        }

    }
}