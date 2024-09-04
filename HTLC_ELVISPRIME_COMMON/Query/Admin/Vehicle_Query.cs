using System.Data;

namespace HTLC_ELVISPRIME_COMMON.Query.Admin
{
    public class Vehicle_Query
    {   
        string sqlstr;

        /// <summary>
        /// 차량 구분 select
        /// </summary>
        /// <returns></returns>
        public string SetVehicleDiv_Query()
        {
            sqlstr = "";

            sqlstr += " SELECT DISTINCT ";
            sqlstr += "          (CASE WHEN CAR_DIV_CODE = '50UP' THEN '6톤이상' ELSE CAR_DIV END) ";
            sqlstr += "             AS CAR_DIV, ";
            sqlstr += "          CAR_DIV_CODE ";
            sqlstr += "     FROM PRM_VEHICLE_MST ";
            sqlstr += " GROUP BY CAR_DIV, CAR_DIV_CODE ";
            sqlstr += " ORDER BY CAR_DIV_CODE ASC, CAR_DIV ASC ";

            return sqlstr;
        }

        /// <summary>
        /// 차량 제원 검색 SQL
        /// </summary>
        /// <returns></returns>
        public string SearchVehicle_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT * ";
            sqlstr += "   FROM (SELECT ROWNUM AS RNUM, ";
            sqlstr += "                FLOOR ( (ROWNUM - 1) / 10 + 1) AS PAGE, ";
            sqlstr += "                COUNT (*) OVER () AS TOTCNT, ";
            sqlstr += "                A.* ";
            sqlstr += "           FROM (  SELECT MNGT_NO, ";
            sqlstr += "                          SEQ, ";
            sqlstr += "                          CAR_DIV_CODE, ";
            sqlstr += "                          CAR_DIV, ";
            sqlstr += "                          CAR_NAME, ";
            sqlstr += "                          SHORTHAND, ";
            sqlstr += "                          CAR_WIDTH, ";
            sqlstr += "                          IMG_PATH ";
            sqlstr += "                     FROM PRM_VEHICLE_MST ";
            sqlstr += "     WHERE 1=1 ";
            if (dr["CAR_DIV_CODE"].ToString() != "")
            {
                sqlstr += "    AND CAR_DIV_CODE = '" + dr["CAR_DIV_CODE"].ToString() + "' ";
            }
            sqlstr += "                 ORDER BY CAR_DIV_CODE ASC, CAR_DIV ASC, SEQ ASC) A) ";
            sqlstr += "  WHERE PAGE = " + dr["PAGE"].ToString() + " ";            

            return sqlstr;
        }

        /// <summary>
        /// 차량 제원 - 이미지 다운로드 데이터
        /// </summary>
        /// <returns></returns>
        public string VehicleImgDown_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT ";
            sqlstr += "     IMG_NAME,  ";
            sqlstr += "     IMG_PATH, ";
            sqlstr += "     REPLACE_IMG_NAME ";
            sqlstr += "   FROM PRM_VEHICLE_MST ";
            sqlstr += "  WHERE 1 = 1  ";
            sqlstr += "     AND MNGT_NO = '" + dr["MNGT_NO"].ToString() + "'  ";
            sqlstr += "     AND SEQ = '" + dr["SEQ"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 차량 제원 - 파일 삭제
        /// </summary>
        /// <returns></returns>
        public string DeleteVehicle_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " DELETE PRM_VEHICLE_MST ";
            sqlstr += "  WHERE ";
            sqlstr += "     MNGT_NO = '" + dr["MNGT_NO"].ToString() + "'  ";
            sqlstr += "     AND SEQ = '" + dr["SEQ"].ToString() + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 차량 제원 - 수정 데이터 검색
        /// </summary>
        /// <returns></returns>
        public string SearchVehicleModify_Query(string MNGT_NO, string SEQ)
        {
            sqlstr = "";

            sqlstr += " SELECT ";
            sqlstr += "        MNGT_NO, ";
            sqlstr += "        SEQ, ";
            sqlstr += "        CAR_DIV_CODE, ";
            sqlstr += "        CAR_NAME, ";
            sqlstr += "        SHORTHAND, ";
            sqlstr += "        CAR_WIDTH, ";
            sqlstr += "        TOP_HEIGHT, ";
            sqlstr += "        BOTTOM_HEIGHT, ";
            sqlstr += "        CAR_AREA, ";
            sqlstr += "        CAR_WEIGHT, ";
            sqlstr += "        CAR_CBM, ";
            sqlstr += "        TOTAL_HEIGHT, ";
            sqlstr += "        RMK, ";
            sqlstr += "        IMG_NAME, ";
            sqlstr += "        IMG_PATH, ";
            sqlstr += "        REPLACE_IMG_NAME ";
            sqlstr += "   FROM PRM_VEHICLE_MST ";
            sqlstr += "  WHERE 1 = 1  ";
            sqlstr += "     AND MNGT_NO = '" + MNGT_NO + "'  ";
            sqlstr += "     AND SEQ = '" + SEQ + "' ";

            return sqlstr;
        }        

        /// <summary>
        /// 차량 제원 - 기존 데이터 수정
        /// </summary>
        /// <returns></returns>
        public string VehicleModify_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT ";
            sqlstr += "        MNGT_NO, ";
            sqlstr += "        SEQ, ";
            sqlstr += "        CAR_DIV_CODE, ";
            sqlstr += "        CAR_NAME, ";
            sqlstr += "        SHORTHAND, ";
            sqlstr += "        CAR_WIDTH, ";
            sqlstr += "        TOP_HEIGHT, ";
            sqlstr += "        BOTTOM_HEIGHT, ";
            sqlstr += "        CAR_AREA, ";
            sqlstr += "        CAR_WEIGHT, ";
            sqlstr += "        CAR_CBM, ";
            sqlstr += "        TOTAL_HEIGHT, ";
            sqlstr += "        RMK, ";
            sqlstr += "        IMG_NAME, ";
            sqlstr += "        IMG_PATH, ";
            sqlstr += "        REPLACE_IMG_NAME ";
            sqlstr += "   FROM PRM_VEHICLE_MST ";
            sqlstr += "  WHERE 1 = 1  ";
            //sqlstr += "     AND MNGT_NO = '" + MNGT_NO + "'  ";
            //sqlstr += "     AND SEQ = '" + SEQ + "' ";

            return sqlstr;
        }

        /// <summary>
        /// 차량 제원 - 데이터 신규 저장
        /// </summary>
        /// <returns></returns>
        public string InsertVehicle_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr = "INSERT INTO PRM_VEHICLE_MST";
            sqlstr += "   (MNGT_NO ,SEQ ,CAR_DIV , CAR_DIV_CODE , CAR_NAME , SHORTHAND , CAR_WIDTH , TOP_HEIGHT , BOTTOM_HEIGHT , CAR_AREA , CAR_WEIGHT , CAR_CBM , TOTAL_HEIGHT ,RMK , IMG_NAME , IMG_PATH , REPLACE_IMG_NAME , INS_USR , INS_YMD , INS_HM , UPD_USR , UPD_YMD , UPD_HM)";
            sqlstr += " Values";
            sqlstr += "   ('" + dr["MNGT_NO"] + "'";
            sqlstr += "  ,(SELECT NVL(MAX(SEQ),0)+1 AS SEQ FROM PRM_VEHICLE_MST WHERE MNGT_NO = '" + dr["MNGT_NO"] + "')";
            sqlstr += "  ,'" + dr["CAR_DIV"] + "'";
            sqlstr += "  ,'" + dr["CAR_DIV_CODE"] + "'";
            sqlstr += "  ,'" + dr["CAR_NAME"] + "'";
            sqlstr += "   ,'" + dr["SHORTHAND"] + "' ";
            sqlstr += "  ,'" + dr["CAR_WIDTH"] + "'";
            sqlstr += "  ,'" + dr["TOP_HEIGHT"] + "'";
            sqlstr += "   ,'" + dr["BOTTOM_HEIGHT"] + "'";
            sqlstr += "   ,'" + dr["CAR_AREA"] + "'";
            sqlstr += "   ,'" + dr["CAR_WEIGHT"] + "'";
            sqlstr += "   ,'" + dr["CAR_CBM"] + "'";
            sqlstr += "   ,'" + dr["TOTAL_HEIGHT"] + "'";
            sqlstr += "   ,'" + dr["RMK"] + "'";

            if (dr["IS_FILE"].ToString() == "Y")
            {
                sqlstr += "   ,'" + dr["IMG_NAME"] + "'";
                sqlstr += "   ,'" + dr["IMG_PATH"] + "'";
                sqlstr += "   ,'" + dr["REPLACE_IMG_NAME"] + "'";
            }
            else if (dr["IS_FILE"].ToString() == "N")
            {
                sqlstr += "   ,''";
                sqlstr += "   ,''";
                sqlstr += "   ,''";
            }

            sqlstr += "  ,'" + dr["USR_ID"] + "'";
            sqlstr += "   , UFN_DATE_FORMAT('DATE') ";
            sqlstr += "   , UFN_DATE_FORMAT('TIME') ";
            sqlstr += "   ,'" + dr["USR_ID"] + "'";
            sqlstr += "   , UFN_DATE_FORMAT('DATE') ";
            sqlstr += "   , UFN_DATE_FORMAT('TIME') ";
            sqlstr += ")";

            return sqlstr;
        }

        /// <summary>
        /// 차량 제원 - 데이터 수정
        /// </summary>
        /// <returns></returns>
        public string UpdateVehicle_Query(DataRow dr)
        {
            sqlstr = "";

            sqlstr = "UPDATE PRM_VEHICLE_MST";
            sqlstr += "   SET";
            sqlstr += "     CAR_DIV = '" + dr["CAR_DIV"] + "'";
            sqlstr += "     , CAR_DIV_CODE = '" + dr["CAR_DIV_CODE"] + "'";
            sqlstr += "     , CAR_NAME = '" + dr["CAR_NAME"] + "'";
            sqlstr += "     , SHORTHAND = '" + dr["SHORTHAND"] + "'";
            sqlstr += "     , CAR_WIDTH = '" + dr["CAR_WIDTH"] + "'";
            sqlstr += "     , TOP_HEIGHT = '" + dr["TOP_HEIGHT"] + "'";
            sqlstr += "     , BOTTOM_HEIGHT = '" + dr["BOTTOM_HEIGHT"] + "'";
            sqlstr += "     , CAR_AREA = '" + dr["CAR_AREA"] + "'";
            sqlstr += "     , CAR_WEIGHT = '" + dr["CAR_WEIGHT"] + "'";
            sqlstr += "     , CAR_CBM = '" + dr["CAR_CBM"] + "'";
            sqlstr += "     , TOTAL_HEIGHT = '" + dr["TOTAL_HEIGHT"] + "'";
            sqlstr += "     , RMK = '" + dr["RMK"] + "'";

            //UPDATE 인지 아닌지
            if (dr["IS_FILE"].ToString() == "Y")
            {
                sqlstr += "     , IMG_NAME = '" + dr["IMG_NAME"] + "'";
                sqlstr += "     , IMG_PATH = '" + dr["IMG_PATH"] + "'";
                sqlstr += "     , REPLACE_IMG_NAME = '" + dr["REPLACE_IMG_NAME"] + "'";
            }

            sqlstr += "     , UPD_USR = '" + dr["USR_ID"] + "'";
            sqlstr += "     , UPD_YMD = UFN_DATE_FORMAT('DATE') ";
            sqlstr += "     , UPD_HM = UFN_DATE_FORMAT('TIME') ";
            sqlstr += " WHERE MNGT_NO = '" + dr["MNGT_NO"] + "'";
            sqlstr += " AND SEQ = '" + dr["SEQ"] + "'";

            return sqlstr;
        }

    }
}