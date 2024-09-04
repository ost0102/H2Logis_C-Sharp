using System;
using System.Data;

namespace HTLC_ELVISPRIME_COMMON.Query.Admin
{
    public class Tariff_Query
    {   
        string sqlstr;

        /// <summary>
        /// 분기 / 년 데이터 가져오기
        /// </summary>
        /// <returns></returns>
        public string SetYearQuarter_Query()
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
            sqlstr += "             FROM PRM_SAFE_FRE ";
            sqlstr += "         GROUP BY PERIOD_YEAR, PERIOD_QUARTER ";
            sqlstr += "         ORDER BY PERIOD_YEAR DESC, PERIOD_QUARTER DESC) A ";

            return sqlstr;
        }

        /// <summary>
        /// 주소 (시,도) 가져오기
        /// </summary>
        /// <returns></returns>
        public string SetAddrState_Query()
        {
            sqlstr = "";

            sqlstr += " SELECT DISTINCT OPT_ITEM1 ";
            sqlstr += "     FROM MDM_COM_CODE ";
            sqlstr += "    WHERE GRP_CD = 'P01' ";
            sqlstr += " ORDER BY OPT_ITEM1 ASC ";

            return sqlstr;
        }        

        /// <summary>
        /// 주소 전체(시.군.구)
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string SetAddrCity_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT DISTINCT OPT_ITEM2 ";
            sqlstr += "     FROM MDM_COM_CODE ";
            sqlstr += "    WHERE GRP_CD = 'P01' ";
            sqlstr += "    AND OPT_ITEM1 = '" + dr["OPT_ITEM1"].ToString() + "' ";
            sqlstr += " ORDER BY OPT_ITEM2 ASC ";

            return sqlstr;
        }

        /// <summary>
        /// 주소 전체(읍,면,동) - 행정동
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string SetAddrTownship_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT DISTINCT OPT_ITEM3 ";
            sqlstr += "     FROM MDM_COM_CODE ";
            sqlstr += "    WHERE GRP_CD = 'P01' ";
            sqlstr += "    AND OPT_ITEM1 = '" + dr["OPT_ITEM1"].ToString() + "' ";
            sqlstr += "    AND OPT_ITEM2 = '" + dr["OPT_ITEM2"].ToString() + "' ";
            sqlstr += " ORDER BY OPT_ITEM3 ASC ";

            return sqlstr;
        }

        /// <summary>
        /// 주소 전체(읍,면,동) - 법정동
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string SetAddrTownship2_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT DISTINCT OPT_ITEM3 , OPT_ITEM4 ";
            sqlstr += "     FROM MDM_COM_CODE ";
            sqlstr += "    WHERE GRP_CD = 'P01' ";
            sqlstr += "    AND OPT_ITEM1 = '" + dr["OPT_ITEM1"].ToString() + "' ";
            sqlstr += "    AND OPT_ITEM2 = '" + dr["OPT_ITEM2"].ToString() + "' ";
            sqlstr += " ORDER BY OPT_ITEM3 ASC ";

            return sqlstr;
        }

        /// <summary>
        /// 항구 기점 데이터 가져오기
        /// </summary>
        /// <returns></returns>
        public string fnSetSection_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += "   SELECT A.PORT, ";
            sqlstr += "          A.SECTION, ";
            sqlstr += "          TRIM (B.OPT_ITEM2) || '기점(' || A.SECTION || ')' AS PORT_NM ";
            sqlstr += "     FROM PRM_SAFE_FRE A INNER JOIN MDM_COM_CODE B ON A.PORT = B.OPT_ITEM1 ";            
            sqlstr += " WHERE 1=1  ";
            sqlstr += " AND GRP_CD = 'P03' ";
            sqlstr += " AND A.PERIOD_YEAR = '" + dr["PERIOD_YEAR"].ToString() + "' ";
            sqlstr += " AND A.PERIOD_QUARTER = '" + dr["PERIOD_QUARTER"].ToString() + "' ";
            sqlstr += " GROUP BY PORT, SECTION, B.OPT_ITEM2 ";
            sqlstr += " ORDER BY SECTION ASC ";

            return sqlstr;
        }

        /// <summary>
        /// 구분 변경 시 PRICE 변경 로직
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string GetCntrPrice_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT MNGT_NO, ";
            sqlstr += "        SEQ, ";
            if (dr["TYPE"].ToString() == "TF")
            {
                sqlstr += "        TF_20FT AS CNTR_20FT, ";
                sqlstr += "        TF_40FT AS CNTR_40FT ";
            }
            else if (dr["TYPE"].ToString() == "CF")
            {
                sqlstr += "        CF_20FT AS CNTR_20FT, ";
                sqlstr += "        CF_40FT AS CNTR_40FT ";
            }
            else if (dr["TYPE"].ToString() == "IF")
            {
                sqlstr += "        IF_20FT AS CNTR_20FT, ";
                sqlstr += "        IF_40FT AS CNTR_40FT ";
            }

            sqlstr += "   FROM PRM_SAFE_FRE ";
            sqlstr += " WHERE ";
            sqlstr += "   1=1 ";
            sqlstr += "   AND MNGT_NO = '" + dr["MNGT_NO"].ToString() + "' ";
            sqlstr += "   AND SEQ = '" + dr["SEQ"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 안전 운임 데이터 검색
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string TariffSerach_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT * ";
            sqlstr += "   FROM (SELECT ROWNUM AS RNUM, ";
            sqlstr += "                FLOOR ( (ROWNUM - 1) / 10 + 1) AS PAGE, ";
            sqlstr += "                COUNT (*) OVER () AS TOTCNT, ";
            sqlstr += "                A.*, ";
            sqlstr += "                (SELECT OPT_ITEM2 ";
            sqlstr += "                   FROM MDM_COM_CODE ";
            sqlstr += "                  WHERE GRP_CD = 'P05' AND OPT_ITEM1 = A.PERIOD) ";
            sqlstr += "                AS PERIOD_NAME ";
            sqlstr += "           FROM (   ";
            sqlstr += " SELECT MNGT_NO, ";
            sqlstr += "        SEQ, ";
            sqlstr += "        PERIOD_QUARTER, ";
            sqlstr += "        PERIOD_YEAR, ";
            sqlstr += "        PERIOD_YEAR || PERIOD_QUARTER AS PERIOD, ";
            sqlstr += "        (SELECT OPT_ITEM2 FROM MDM_COM_CODE  WHERE GRP_CD = 'P03' AND OPT_ITEM1 = PORT) AS PORT, ";
            sqlstr += "        ADDR_STATE, ";
            sqlstr += "        ADDR_CITY, ";
            sqlstr += "        ADDR_TOWNSHIP, ";

            if (dr["TYPE"].ToString() == "TF")
            {
                sqlstr += "        TF_20FT, ";
                sqlstr += "        TF_40FT, ";
            }
            else if (dr["TYPE"].ToString() == "CF")
            {
                sqlstr += "        CF_20FT, ";
                sqlstr += "        CF_40FT, ";
            }
            else if (dr["TYPE"].ToString() == "IF")
            {
                sqlstr += "        IF_20FT, ";
                sqlstr += "        IF_40FT, ";
            }

            sqlstr += "        DISTANCE ";
            sqlstr += "   FROM PRM_SAFE_FRE ";

            sqlstr += " WHERE ";
            sqlstr += "   1=1 ";
            sqlstr += "   AND PERIOD_YEAR = '" + dr["YEAR"].ToString() + "' ";
            sqlstr += "   AND PERIOD_QUARTER = '" + dr["QUARTER"].ToString() + "' ";

            //if (dr["SECTION_TYPE"].ToString() == "왕복")
            //{
            //    sqlstr += "   AND SECTION_RETURN = '" + dr["SECTION"].ToString() + "' ";
            //}
            //else if (dr["SECTION_TYPE"].ToString() == "편도")
            //{
            //    sqlstr += "   AND SECTION_ONEWAY = '" + dr["SECTION"].ToString() + "' ";
            //}

            if (dr["PORT"].ToString() != "") { 
                sqlstr += "   AND PORT = '" + dr["PORT"].ToString() + "' ";
            }

            if (dr["SECTION"].ToString() != "")
            {
                sqlstr += "   AND SECTION = '" + dr["SECTION"].ToString() + "' ";
            }

            if (dr["ADDRSTATE"].ToString() != "")
            {
                sqlstr += " AND ADDR_STATE = '" + dr["ADDRSTATE"].ToString() + "' ";
            }

            if (dr["ADDRCITY"].ToString() != "")
            {
                sqlstr += " AND ADDR_CITY = '" + dr["ADDRCITY"].ToString() + "' ";
            }

            if (dr["ADDRTOWNSHIP"].ToString() != "")
            {
                sqlstr += " AND ADDR_TOWNSHIP = '" + dr["ADDRTOWNSHIP"].ToString() + "' ";
            }

            sqlstr += " ORDER BY ADDR_STATE ASC , ADDR_CITY ASC , ADDR_TOWNSHIP ASC) A ";

            sqlstr += "                 ) ";
            sqlstr += "  WHERE PAGE = '" + dr["PAGE"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 안전 운임 데이터 검색
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string SearchTariffPRModify_Query(string MNGT_NO, string SEQ , string TYPE)
        {
            sqlstr = "";

            sqlstr += " SELECT MNGT_NO, ";
            sqlstr += "        SEQ, ";
            sqlstr += "        PERIOD_YEAR, ";
            sqlstr += "        PERIOD_QUARTER, ";
            //sqlstr += "        SECTION_ONEWAY, ";
            //sqlstr += "        SECTION_RETURN, ";
            sqlstr += "        PORT || '_' || SECTION AS SECTION, ";
            sqlstr += "        ADDR_STATE, ";
            sqlstr += "        ADDR_CITY, ";
            sqlstr += "        ADDR_TOWNSHIP, ";
            sqlstr += "        (SELECT OPT_ITEM2 FROM MDM_COM_CODE  WHERE GRP_CD = 'P03' AND OPT_ITEM1 = PORT) AS PORT, ";
            if (TYPE == "TF")
            {
                sqlstr += "        TF_20FT AS CNTR_20FT, ";
                sqlstr += "        TF_40FT AS CNTR_40FT, ";
            }
            else if (TYPE == "CF")
            {
                sqlstr += "        CF_20FT AS CNTR_20FT, ";
                sqlstr += "        CF_40FT AS CNTR_40FT, ";
            }
            else if (TYPE == "IF")
            {
                sqlstr += "        IF_20FT AS CNTR_20FT, ";
                sqlstr += "        IF_40FT AS CNTR_40FT, ";
            }
            sqlstr += "        DISTANCE ";
            sqlstr += "   FROM PRM_SAFE_FRE ";
            sqlstr += " WHERE ";
            sqlstr += "   1=1 ";
            sqlstr += "   AND MNGT_NO = '"+ MNGT_NO + "' ";
            sqlstr += "   AND SEQ = '"+ SEQ +"' ";

            return sqlstr;
        }

        /// <summary>
        /// 안전운임제 할증 관리 UPDATE
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string UpdateTariff_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " UPDATE PRM_SAFE_FRE ";
            sqlstr += "    SET  ";
            //sqlstr += "        PORT = '" + dr["PORT"].ToString() + "', ";
            sqlstr += "        ADDR_STATE = '" + dr["ADDR_STATE"].ToString() + "', ";
            sqlstr += "        ADDR_CITY = '" + dr["ADDR_CITY"].ToString() + "', ";
            sqlstr += "        ADDR_TOWNSHIP = '" + dr["ADDR_TOWNSHIP"].ToString() + "', ";

            if (dr["TYPE"].ToString() == "TF")
            {
                sqlstr += "        TF_20FT = '" + dr["CNTR_20FT"].ToString() + "', ";
                sqlstr += "        TF_40FT = '" + dr["CNTR_40FT"].ToString() + "',";
            }
            else if (dr["TYPE"].ToString() == "CF")
            {
                sqlstr += "        CF_20FT = '" + dr["CNTR_20FT"].ToString() + "', ";
                sqlstr += "        CF_40FT = '" + dr["CNTR_40FT"].ToString() + "', ";
            }
            else if (dr["TYPE"].ToString() == "IF")
            {
                sqlstr += "        IF_20FT = '" + dr["CNTR_20FT"].ToString() + "', ";
                sqlstr += "        IF_40FT = '" + dr["CNTR_40FT"].ToString() + "', ";
            }

            sqlstr += "        DISTANCE = '" + dr["DISTANCE"].ToString() + "', ";
            sqlstr += "        UPD_USR = '" + dr["USR_ID"].ToString() + "', ";
            sqlstr += "        UPD_YMD = UFN_DATE_FORMAT('DATE'), ";
            sqlstr += "        UPD_HM = UFN_DATE_FORMAT('TIME') ";
            sqlstr += "  WHERE  ";
            sqlstr += "     MNGT_NO = '" + dr["MNGT_NO"].ToString() + "'  ";
            sqlstr += "     AND SEQ = '" + dr["SEQ"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 안전운임제 할증 관리 DELETE
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string DeleteTariff_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " DELETE FROM PRM_SAFE_FRE ";
            sqlstr += "  WHERE  ";
            sqlstr += "     MNGT_NO = '" + dr["MNGT_NO"].ToString() + "'  ";
            sqlstr += "     AND SEQ = '" + dr["SEQ"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 엑셀 데이터 체크 (기존에 데이터가 있는지 체크)
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string CheckTariffExcel_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT COUNT(*) AS COUNT ";
            sqlstr += "   FROM PRM_SAFE_FRE ";
            sqlstr += "  WHERE 1=1  ";
            sqlstr += "  AND PERIOD_YEAR = '" + dr["PERIOD_YEAR"].ToString() + "' ";
            sqlstr += "  AND PERIOD_QUARTER = '" + dr["PERIOD_QUARTER"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 엑셀 안전운임 업로드
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string fnInsertExcelTariff_Query(DataRow dr)
        {
            sqlstr = "";           

            sqlstr += " INSERT INTO PRM_SAFE_FRE  ";
            sqlstr += " ( ";
            sqlstr += " MNGT_NO  ";
            sqlstr += " ,SEQ  ";
            sqlstr += " ,PERIOD_YEAR  ";
            sqlstr += " ,PERIOD_QUARTER  ";
            sqlstr += " ,PORT  ";
            sqlstr += " ,SECTION  ";
            sqlstr += " ,ADDR_STATE  ";
            sqlstr += " ,ADDR_CITY  ";
            sqlstr += " ,ADDR_TOWNSHIP  ";
            sqlstr += " ,ADDR_TOWNSHIP2  "; //twkim 추가
            sqlstr += " ,DISTANCE  ";
            sqlstr += " ,TF_40FT  ";
            sqlstr += " ,IF_40FT  ";
            sqlstr += " ,CF_40FT  ";
            sqlstr += " ,TF_20FT  ";
            sqlstr += " ,IF_20FT  ";
            sqlstr += " ,CF_20FT  ";
            sqlstr += " ,INS_USR  ";
            sqlstr += " ,INS_YMD  ";
            sqlstr += " ,INS_HM  ";
            sqlstr += " ) ";
            sqlstr += " VALUES ";
            sqlstr += " ( ";
            sqlstr += " '" + dr["MNGT_NO"].ToString() + "' ";
            sqlstr += " ,(SELECT NVL(MAX(SEQ),0)+1 AS SEQ FROM PRM_SAFE_FRE WHERE MNGT_NO = '" + dr["MNGT_NO"] + "') ";
            sqlstr += " ,'" + dr["PERIOD_YEAR"].ToString() + "' ";
            sqlstr += " ,'" + dr["PERIOD_QUARTER"].ToString() + "' ";            
            sqlstr += " ,'" + dr["PORT"].ToString() + "' ";
            sqlstr += " ,'" + dr["SECTION"].ToString() + "' ";
            sqlstr += " ,'" + dr["ADDR_STATE"].ToString() + "' ";
            sqlstr += " ,'" + dr["ADDR_CITY"].ToString() + "' ";
            sqlstr += " ,'" + dr["ADDR_TOWNSHIP"].ToString() + "' ";
            sqlstr += " ,'" + dr["ADDR_TOWNSHIP2"].ToString() + "' "; //twkim 추가
            sqlstr += " ,'" + dr["DISTANCE"].ToString() + "' ";
            sqlstr += " ,'" + dr["TF_40FT"].ToString().Replace(",","") + "' ";
            sqlstr += " ,'" + dr["IF_40FT"].ToString().Replace(",","") + "' ";
            sqlstr += " ,'" + dr["CF_40FT"].ToString().Replace(",","") + "' ";
            sqlstr += " ,'" + dr["TF_20FT"].ToString().Replace(",","") + "' ";
            sqlstr += " ,'" + dr["IF_20FT"].ToString().Replace(",","") + "' ";
            sqlstr += " ,'" + dr["CF_20FT"].ToString().Replace(",","") + "' ";
            sqlstr += " ,'" + dr["USR_ID"].ToString() + "' ";
            sqlstr += " ,UFN_DATE_FORMAT('DATE') "; 
            sqlstr += " ,UFN_DATE_FORMAT('TIME') "; 
            sqlstr += " ) ";

            //sqlstr += "        UPD_USR = '" + dr["USR_ID"].ToString() + "', ";
            //sqlstr += "        UPD_YMD = UFN_DATE_FORMAT('DATE'), ";
            //sqlstr += "        UPD_HM = UFN_DATE_FORMAT('TIME') ";

            return sqlstr;
        }

        /// <summary>
        /// 엑셀 안전운임 업로드
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string fnInsertExcelTariffMulti_Query(DataTable dt,int nNumber)
        {
            sqlstr = "";

            try
            {
                sqlstr += " BEGIN ";

                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    sqlstr += " INSERT INTO PRM_SAFE_FRE  ";
                    sqlstr += " ( ";
                    sqlstr += " MNGT_NO  ";
                    sqlstr += " ,SEQ  ";
                    sqlstr += " ,PERIOD_YEAR  ";
                    sqlstr += " ,PERIOD_QUARTER  ";
                    sqlstr += " ,PORT  ";
                    sqlstr += " ,SECTION  ";
                    sqlstr += " ,ADDR_STATE  ";
                    sqlstr += " ,ADDR_CITY  ";
                    sqlstr += " ,ADDR_TOWNSHIP  ";
                    sqlstr += " ,ADDR_TOWNSHIP2  "; //twkim 추가
                    sqlstr += " ,DISTANCE  ";
                    sqlstr += " ,TF_40FT  ";
                    sqlstr += " ,IF_40FT  ";
                    sqlstr += " ,CF_40FT  ";
                    sqlstr += " ,TF_20FT  ";
                    sqlstr += " ,IF_20FT  ";
                    sqlstr += " ,CF_20FT  ";
                    sqlstr += " ,INS_USR  ";
                    sqlstr += " ,INS_YMD  ";
                    sqlstr += " ,INS_HM  ";
                    sqlstr += " ) ";
                    sqlstr += " VALUES ";
                    sqlstr += " ( ";
                    sqlstr += " '" + dt.Rows[nNumber]["MNGT_NO"].ToString() + "' ";
                    sqlstr += " ,(SELECT NVL(MAX(SEQ),0)+1 AS SEQ FROM PRM_SAFE_FRE WHERE MNGT_NO = '" + dt.Rows[nNumber]["MNGT_NO"] + "') ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["PERIOD_YEAR"].ToString() + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["PERIOD_QUARTER"].ToString() + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["PORT"].ToString() + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["SECTION"].ToString() + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["ADDR_STATE"].ToString() + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["ADDR_CITY"].ToString() + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["ADDR_TOWNSHIP"].ToString() + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["ADDR_TOWNSHIP2"].ToString() + "' "; //twkim 추가
                    sqlstr += " ,'" + dt.Rows[nNumber]["DISTANCE"].ToString() + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["TF_40FT"].ToString().Replace(",", "") + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["IF_40FT"].ToString().Replace(",", "") + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["CF_40FT"].ToString().Replace(",", "") + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["TF_20FT"].ToString().Replace(",", "") + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["IF_20FT"].ToString().Replace(",", "") + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["CF_20FT"].ToString().Replace(",", "") + "' ";
                    sqlstr += " ,'" + dt.Rows[nNumber]["USR_ID"].ToString() + "' ";
                    sqlstr += " ,UFN_DATE_FORMAT('DATE') ";
                    sqlstr += " ,UFN_DATE_FORMAT('TIME') ";
                    sqlstr += " ); ";

                    //마지막인지 체크함.
                    if (dt.Rows.Count == (nNumber + 1))
                    {
                        sqlstr += " END;";
                        return sqlstr;
                    }

                    if (i == 1000)
                    {
                        sqlstr += " END;";
                        return sqlstr;
                    }
                    else
                    {
                        nNumber++;
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return sqlstr;
        }

        /// <summary>
        /// 엑셀 안전운임 업로드 전 데이터 있는지 확인
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string fnCheckExcelTariff_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT * ";
            sqlstr += "   FROM PRM_SAFE_FRE ";
            sqlstr += "  WHERE     1 = 1 ";
            sqlstr += "        AND PERIOD_YEAR = '" + dr["PERIOD_YEAR"].ToString() + "' ";
            sqlstr += "        AND PERIOD_QUARTER = '" + dr["PERIOD_QUARTER"].ToString() + "' ";
            sqlstr += "        AND PORT = '" + dr["PORT"].ToString() + "' ";
            sqlstr += "        AND SECTION = '" + dr["SECTION"].ToString() + "' ";
            sqlstr += "        AND ADDR_STATE = '" + dr["ADDR_STATE"].ToString() + "' ";
            sqlstr += "        AND ADDR_CITY = '" + dr["ADDR_CITY"].ToString() + "' ";
            sqlstr += "        AND ADDR_TOWNSHIP = '" + dr["ADDR_TOWNSHIP"].ToString() + "' ";
            sqlstr += "        AND DISTANCE = '" + dr["DISTANCE"].ToString() + "' ";
            sqlstr += "        AND TF_40FT = '" + dr["TF_40FT"].ToString().Replace(",", "") + "' ";
            sqlstr += "        AND IF_40FT = '" + dr["IF_40FT"].ToString().Replace(",", "") + "' ";
            sqlstr += "        AND CF_40FT = '" + dr["CF_40FT"].ToString().Replace(",", "") + "' ";
            sqlstr += "        AND TF_20FT = '" + dr["TF_20FT"].ToString().Replace(",", "") + "' ";
            sqlstr += "        AND IF_20FT = '" + dr["IF_20FT"].ToString().Replace(",", "") + "' ";
            sqlstr += "        AND CF_20FT = '" + dr["CF_20FT"].ToString().Replace(",", "") + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 기존에 데이터가 있다면 타리프 삭제하는 로직
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string fnDeleteExcelTariff_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " DELETE FROM PRM_SAFE_FRE ";
            sqlstr += "  WHERE  ";
            sqlstr += "     PERIOD_YEAR = '" + dr["PERIOD_YEAR"].ToString() + "'  ";
            sqlstr += "     AND PERIOD_QUARTER = '" + dr["PERIOD_QUARTER"].ToString() + "' ";

            return sqlstr;
        }

    }
}