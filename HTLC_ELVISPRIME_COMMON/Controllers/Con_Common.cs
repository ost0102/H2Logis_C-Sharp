using HTLC_ELVISPRIME_COMMON.Query.Common;
using HTLC_ELVISPRIME_COMMON.YJIT_Utils;
using HTLC_ELVISPRIME_DATA;
using Newtonsoft.Json;
using System;
using System.Data;

namespace HTLC_ELVISPRIME_COMMON.Controllers
{
    public class Con_Common
    {
        Encryption String_Encrypt = new Encryption(); //암호화
        Common comm = new Common(); //일반 함수 
        Comm_Query CQ = new Comm_Query();

        //전역변수 선언
        string rtnJson = "";
        DataTable dt = new DataTable();
        DataTable Resultdt = new DataTable();

        public string Con_GetPortData(string strValue)
        {
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(CQ.Query_GetPortData(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Table";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "", Resultdt);
                }
                else
                {
                    rtnJson = comm.MakeJson("Y", "", Resultdt);
                }

                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                rtnJson = String_Encrypt.decryptAES256(rtnJson);
                return rtnJson;
            }
        }

        /// <summary>
        /// Service 타입 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_fnGetServiceType(string strValue)
        {
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(CQ.Query_fnGetServiceType(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Service";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "", Resultdt);
                }
                else
                {
                    rtnJson = comm.MakeJson("Y", "", Resultdt);
                }

                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                rtnJson = String_Encrypt.decryptAES256(rtnJson);
                return rtnJson;
            }
        }

        /// <summary>
        /// 씨벤티지 Token 재발급
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_fnSetSvtgAuthToken(string strValue)
        {
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                DataHelper.ExecuteNonQuery(CQ.Query_SetSvtgAuthToken(dt.Rows[0]), CommandType.Text);
                rtnJson = comm.MakeJson("Y", "Success");

                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                rtnJson = String_Encrypt.decryptAES256(rtnJson);
                return rtnJson;
            }
        }

    }
}
