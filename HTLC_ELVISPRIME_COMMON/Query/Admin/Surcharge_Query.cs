using System;
using System.Data;

namespace HTLC_ELVISPRIME_COMMON.Query.Admin
{
    public class Surcharge_Query
    {   
        string sqlstr;

        /// <summary>
        /// 부대비용 - PORT 가져오기 (선사 , 훼리)
        /// </summary>
        /// <returns></returns>
        public string SetSurchargePort_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += "   SELECT PORT ";
            sqlstr += "     FROM PRM_CHARGE_MST ";
            sqlstr += "    WHERE SHIPPING = '" + dr["PORT_TYPE"].ToString() + "' ";
            sqlstr += " GROUP BY PORT ";
            sqlstr += " ORDER BY PORT ASC ";

            return sqlstr;
        }

        /// <summary>
        /// 수입 / 수출 국가옵션 가져오기
        /// </summary>
        /// <returns></returns>
        public string SetSurchargeCountry_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += "   SELECT COUNTRY_OPTION ";
            sqlstr += "     FROM PRM_CHARGE_MST ";
            sqlstr += "    WHERE BOUND = '" + dr["BOUND"].ToString() + "' ";
            sqlstr += " GROUP BY COUNTRY_OPTION ";
            sqlstr += " ORDER BY COUNTRY_OPTION ASC ";

            return sqlstr;
        }

        /// <summary>
        /// 부대비용 검색
        /// </summary>
        /// <returns></returns>
        public string SearchSurcharge_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT * ";
            sqlstr += "   FROM (SELECT ROWNUM AS RNUM, ";
            sqlstr += "                FLOOR ( (ROWNUM - 1) / 10 + 1) AS PAGE, ";
            sqlstr += "                COUNT (*) OVER () AS TOTCNT, ";
            sqlstr += "                A.* ";
            sqlstr += "           FROM (  SELECT  ";
            sqlstr += "						CASE ";
            sqlstr += "                         WHEN SHIPPING = 'S' THEN '선사' ";
            sqlstr += "                         WHEN SHIPPING = 'F' THEN '훼리' ";
            sqlstr += "                      END ";
            sqlstr += "                         AS SHIPPING, ";
            sqlstr += "						CASE ";
            sqlstr += "                         WHEN BOUND = 'O' THEN '수출' ";
            sqlstr += "                         WHEN BOUND = 'I' THEN '수입' ";
            sqlstr += "                      END ";
            sqlstr += "                         AS BOUND, ";
            sqlstr += "                      PORT, ";
            sqlstr += "                      CNTR_TYPE, ";
            sqlstr += "                      COUNTRY_OPTION, ";
            sqlstr += "                      (SELECT OPT_ITEM2 AS OPT_ITEM1 FROM MDM_COM_CODE WHERE GRP_CD = 'P04' AND OPT_ITEM1 = CNTR_TYPE) AS CNTR_TYPE_NM, ";
            sqlstr += "                      CNTR_SIZE ";
            sqlstr += "                  FROM PRM_CHARGE_MST ";
            sqlstr += "                 WHERE     1 = 1 ";

            if (dr["PORT_TYPE"].ToString() != "")
            {
                sqlstr += "        AND SHIPPING = '" + dr["PORT_TYPE"].ToString() + "' ";
            }

            if (dr["BOUND"].ToString() != "")
            {
                sqlstr += "        AND BOUND = '" + dr["BOUND"].ToString() + "' ";
            }

            if (dr["PORT"].ToString() != "")
            {
                sqlstr += "        AND PORT = '" + dr["PORT"].ToString() + "' ";
            }

            if (dr["CNTR_TYPE"].ToString() != "")
            {
                sqlstr += "        AND CNTR_TYPE = '" + dr["CNTR_TYPE"].ToString() + "' ";
            }

            if (dr["PORT_TYPE"].ToString() == "S")
            {
                if (dr["COUNTRY_OPTION"].ToString() == "")
                {
                    sqlstr += "        AND COUNTRY_OPTION = '기본' ";
                }
                else
                {
                    sqlstr += "        AND COUNTRY_OPTION = '" + dr["COUNTRY_OPTION"].ToString() + "' ";
                }
            }

            sqlstr += "                 GROUP BY SHIPPING, ";
            sqlstr += "                          BOUND, ";
            sqlstr += "                          PORT, ";
            sqlstr += "                          CNTR_TYPE, ";
            sqlstr += "                          COUNTRY_OPTION, ";
            sqlstr += "                          CNTR_SIZE ";

            sqlstr += "                 ORDER BY CNTR_SIZE ASC ";
            sqlstr += " ) A) TOTAL ";

            sqlstr += "  WHERE PAGE = '" + dr["PAGE"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string fnInsertSur_Query(DataRow dr)
        {
            sqlstr = "";

            string strMngt_NO = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            sqlstr += " INSERT INTO PRM_CHARGE_MST ( ";
            sqlstr += "                                 MNGT_NO, ";
            sqlstr += "                                  SEQ, ";
            sqlstr += "                                  SHIPPING, ";
            sqlstr += "                                  BOUND, ";
            sqlstr += "                                  PORT, ";
            sqlstr += "                                  CNTR_TYPE, ";
            sqlstr += "                                  CNTR_SIZE, ";
            sqlstr += "                                  COUNTRY_OPTION, ";
            sqlstr += "                                  CONTENTS, ";
            sqlstr += "                                  CODE, ";
            sqlstr += "                                  UNIT, ";
            sqlstr += "                                  CURRENCY, ";
            sqlstr += "                                  PRICE, ";
            sqlstr += "                                  INS_USR, ";
            sqlstr += "                                  INS_YMD, ";
            sqlstr += "                                  INS_HM) ";
            sqlstr += "      VALUES ( ";
            sqlstr += " 	 '" + strMngt_NO + "', ";
            sqlstr += "  (SELECT NVL (MAX (SEQ), 0) + 1 ";
            sqlstr += " 	FROM PRM_CHARGE_MST ";
            sqlstr += "     WHERE MNGT_NO = '" + strMngt_NO + "'), ";
            sqlstr += " 	 '" + dr["SHIPPING"].ToString() + "', ";
            sqlstr += " 	 '" + dr["BOUND"].ToString() + "', ";
            sqlstr += " 	 '" + dr["PORT"].ToString() + "', ";
            sqlstr += " 	 '" + dr["CNTR_TYPE"].ToString() + "', ";
            sqlstr += " 	 '" + dr["CNTR_SIZE"].ToString() + "', ";
            sqlstr += " 	 '" + dr["COUNTRY_OPTION"].ToString() + "', ";
            sqlstr += " 	 '" + dr["CONTENTS"].ToString() + "', ";
            sqlstr += " 	 '" + dr["CODE"].ToString() + "', ";
            sqlstr += " 	 '" + dr["UNIT"].ToString() + "', ";
            sqlstr += " 	 '" + dr["CURRENCY"].ToString() + "', ";
            sqlstr += " 	 '" + dr["PRICE"].ToString() + "', ";
            sqlstr += " 	 '" + dr["INS_USR"].ToString() + "', ";
            sqlstr += " 	 '" + DateTime.Now.ToString("yyyyMMdd") + "', ";
            sqlstr += " 	 '" + DateTime.Now.ToString("HHmm") + "' ";
            sqlstr += " ) ";

            return sqlstr;
        }

        /// <summary>
        /// 부대비용 검색
        /// </summary>
        /// <returns></returns>
        public string SetModifySearch_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += "   SELECT ";
            sqlstr += "          MNGT_NO, ";
            sqlstr += "          SEQ, ";
            sqlstr += "          CONTENTS, ";
            sqlstr += "          CODE,             ";
            sqlstr += "          UNIT, ";
            sqlstr += "          CURRENCY, ";
            sqlstr += "          PRICE, ";
            sqlstr += "          COUNTRY_OPTION          ";
            sqlstr += "     FROM PRM_CHARGE_MST ";
            sqlstr += "    WHERE     1 = 1 ";

            if (dr["PORT_TYPE"].ToString() != "")
            {
                sqlstr += "        AND SHIPPING = '" + dr["PORT_TYPE"].ToString() + "' ";
            }

            if (dr["BOUND"].ToString() != "")
            {
                sqlstr += "        AND BOUND = '" + dr["BOUND"].ToString() + "' ";
            }

            if (dr["PORT"].ToString() != "")
            {
                sqlstr += "        AND PORT = '" + dr["PORT"].ToString() + "' ";
            }

            if (dr["CNTR_TYPE"].ToString() != "")
            {
                sqlstr += "        AND CNTR_TYPE = '" + dr["CNTR_TYPE"].ToString() + "' ";
            }

            if (dr["CNTR_SIZE"].ToString() != "")
            {
                sqlstr += "        AND CNTR_SIZE = '" + dr["CNTR_SIZE"].ToString() + "' ";
            }
            
            if (dr["COUNTRY_OPTION"].ToString() != "")
            {
                sqlstr += "        AND COUNTRY_OPTION = '" + dr["COUNTRY_OPTION"].ToString() + "' ";
            }

            sqlstr += "                 ORDER BY CNTR_SIZE ASC ";            

            return sqlstr;
        }

        /// <summary>
        /// 부대비용 - 데이터 수정
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string SurchargeModify_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " UPDATE PRM_CHARGE_MST ";
            sqlstr += "    SET  ";
            sqlstr += "        CONTENTS = (SELECT OPT_ITEM2 AS OPT_ITEM1 FROM MDM_COM_CODE WHERE GRP_CD = 'P02' AND OPT_ITEM1 = '" + dr["CODE"].ToString() + "' ), ";
            sqlstr += "        CODE = '" + dr["CODE"].ToString() + "',  ";
            sqlstr += "        UNIT = '" + dr["UNIT"].ToString() + "',  ";
            sqlstr += "        CURRENCY = '" + dr["CURRENCY"].ToString() + "', ";
            sqlstr += "        PRICE = '" + dr["PRICE"].ToString() + "', ";
            sqlstr += "        COUNTRY_OPTION = '" + dr["COUNTRY_OPTION"].ToString() + "' ,  ";
            sqlstr += "        UPD_USR = '" + dr["UPD_USR"].ToString() + "' , ";
            sqlstr += "        UPD_YMD = '" + DateTime.Now.ToString("yyyyMMdd") + "' , ";
            sqlstr += "        UPD_HM = '" + DateTime.Now.ToString("HHmm") + "'   ";
            sqlstr += "  WHERE  ";
            sqlstr += "  MNGT_NO = '" + dr["MNGT_NO"].ToString() + "'  ";
            sqlstr += "  AND SEQ = '" + dr["SEQ"].ToString() + "'  ";            

            return sqlstr;
        }

        /// <summary>
        /// 부대비용 - 데이터 삭제
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string SurchargeDelete_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += "  DELETE PRM_CHARGE_MST  ";  
            sqlstr += "  WHERE  ";
            sqlstr += "  MNGT_NO = '" + dr["MNGT_NO"].ToString() + "'  ";
            sqlstr += "  AND SEQ = '" + dr["SEQ"].ToString() + "'  ";

            return sqlstr;
        }

        /// <summary>
        /// 부대비용 - 엑셀 데이터 있는지 여부 체크
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string CheckSurchargeExcel_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT COUNT(*) AS COUNT ";
            sqlstr += "   FROM PRM_CHARGE_MST ";

            return sqlstr;
        }

        /// <summary>
        /// 부대비용 - 엑셀 데이터 Insert
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string fnInsertExcelSurcharge_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " INSERT INTO PRM_CHARGE_MST ( ";
            sqlstr += "                                 MNGT_NO, ";
            sqlstr += "                                  SEQ, ";
            sqlstr += "                                  SHIPPING, ";
            sqlstr += "                                  BOUND, ";
            sqlstr += "                                  PORT, ";
            sqlstr += "                                  CNTR_TYPE, ";
            sqlstr += "                                  CNTR_SIZE, ";
            sqlstr += "                                  COUNTRY_OPTION, ";
            sqlstr += "                                  CONTENTS, ";
            sqlstr += "                                  CODE, ";
            sqlstr += "                                  UNIT, ";
            sqlstr += "                                  CURRENCY, ";
            sqlstr += "                                  PRICE, ";
            sqlstr += "                                  INS_USR, ";
            sqlstr += "                                  INS_YMD, ";
            sqlstr += "                                  INS_HM) ";
            sqlstr += "      VALUES ( ";
            sqlstr += " 	 '" + dr["MNGT_NO"].ToString() + "', ";
            sqlstr += "  (SELECT NVL (MAX (SEQ), 0) + 1 ";
            sqlstr += " 	FROM PRM_CHARGE_MST ";
            sqlstr += "     WHERE MNGT_NO = '" + dr["MNGT_NO"].ToString() + "'), ";
            sqlstr += " 	 '" + dr["SHIPPING"].ToString() + "', ";
            sqlstr += " 	 '" + dr["BOUND"].ToString() + "', ";
            sqlstr += " 	 '" + dr["PORT"].ToString() + "', ";
            sqlstr += " 	 '" + dr["CNTR_TYPE"].ToString() + "', ";
            sqlstr += " 	 '" + dr["CNTR_SIZE"].ToString() + "', ";
            sqlstr += " 	 '" + dr["COUNTRY_OPTION"].ToString() + "', ";
            sqlstr += " 	 (SELECT OPT_ITEM2 AS OPT_ITEM1 FROM MDM_COM_CODE WHERE GRP_CD = 'P02' AND OPT_ITEM1 = '" + dr["CODE"].ToString() + "' ), ";
            sqlstr += " 	 '" + dr["CODE"].ToString() + "', ";
            sqlstr += " 	 '" + dr["UNIT"].ToString() + "', ";
            sqlstr += " 	 '" + dr["CURRENCY"].ToString() + "', ";
            sqlstr += " 	 '" + dr["PRICE"].ToString().Replace(",", "") + "', ";
            sqlstr += " 	 '" + dr["INS_USR"].ToString() + "', ";
            sqlstr += " 	 UFN_DATE_FORMAT('DATE'), ";
            sqlstr += " 	 UFN_DATE_FORMAT('TIME') ";
            sqlstr += " ) ";

            return sqlstr;
        }

        /// <summary>
        /// 부대비용 - 엑셀 데이터 중복 체크
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string fnCheckExcelduplicate_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT * ";
            sqlstr += "   FROM PRM_CHARGE_MST ";
            sqlstr += "  WHERE     1 = 1 ";
            sqlstr += "        AND SHIPPING = '" + dr["SHIPPING"].ToString() + "' ";
            sqlstr += "        AND BOUND = '" + dr["BOUND"].ToString() + "' ";
            sqlstr += "        AND PORT = '" + dr["PORT"].ToString() + "' ";
            sqlstr += "        AND CNTR_TYPE = '" + dr["CNTR_TYPE"].ToString() + "' ";
            sqlstr += "        AND CNTR_SIZE = '" + dr["CNTR_SIZE"].ToString() + "' ";
            sqlstr += "        AND COUNTRY_OPTION = '" + dr["COUNTRY_OPTION"].ToString() + "' ";
            sqlstr += "        AND CODE = '" + dr["CODE"].ToString() + "' ";
            sqlstr += "        AND UNIT = '" + dr["UNIT"].ToString() + "' ";
            sqlstr += "        AND CURRENCY = '" + dr["CURRENCY"].ToString() + "' ";
            sqlstr += "        AND PRICE = '" + dr["PRICE"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 기존에 데이터가 있다면 부대비용 삭제하는 로직
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string DeleteExcelSurcharge_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " DELETE FROM PRM_CHARGE_MST ";

            return sqlstr;
        }
    }
}