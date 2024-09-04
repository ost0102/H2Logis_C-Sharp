using System.Data;

namespace HTLC_ELVISPRIME_COMMON.Query.Common
{
    public class Comm_Query
    {
        string sqlstr;

        /// <summary>
        /// Port 정보 가져오는 로직
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string Query_GetPortData(DataRow dr)
        {
            sqlstr = "";

            sqlstr += " SELECT PORT.LOC_CD AS CODE, PORT.LOC_NM AS NAME ";
            sqlstr += "   FROM MDM_PORT_MST PORT ";
            sqlstr += "  WHERE     1 = 1 ";
            sqlstr += "        AND NVL (PORT.USE_YN, 'Y') <> 'N' ";
            sqlstr += "        AND ( ('" + dr["LOC_TYPE"].ToString() + "' IS NULL AND 1 = 1) OR ('" + dr["LOC_TYPE"].ToString() + "' IS NOT NULL AND LOC_TYPE = '" + dr["LOC_TYPE"].ToString() + "')) ";

            if (dr["LOC_CD"].ToString() != "")
            {
                sqlstr += "        AND (REPLACE (LOC_CD, ' ', '') LIKE '%" + dr["LOC_CD"].ToString() + "%' ";
                sqlstr += "             OR REPLACE (LOC_NM, ' ', '') LIKE '%" + dr["LOC_CD"].ToString() + "%') ";
            }           

            return sqlstr;
        }

        /// <summary>
        /// Service 타입 가져오기
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string Query_fnGetServiceType(DataRow dr)
        {
            sqlstr = "";

            if(dr["REQ_SVC"].ToString() == "SEA")
            {
                sqlstr += " SELECT COMN_CD AS CODE , CD_NM AS NAME FROM MDM_COM_CODE ";
                sqlstr += " WHERE 1=1  ";
                sqlstr += " AND GRP_CD = 'B02' ";
                sqlstr += " ORDER BY SORT";
            }
            else if (dr["REQ_SVC"].ToString() == "AIR")
            {
                sqlstr += " SELECT COMN_CD AS CODE , CD_NM AS NAME FROM MDM_COM_CODE ";
                sqlstr += " WHERE 1=1  ";
                sqlstr += " AND GRP_CD = 'B02' ";
                sqlstr += " ORDER BY SORT";
            }

            return sqlstr;
        }

        public string Query_SetSvtgAuthToken(DataRow dr)
        {

            sqlstr += "  UPDATE MDM_OFFICE_CONFIG SET KEY_CD = '" + dr["TOKEN"] + "' WHERE OFFICE_CD = '" + dr["OFFICE_CD"] + "' AND ITEM_CD = '112'";


            return sqlstr;
        }

    }
}
