using HTLC_ELVISPRIME_COMMON.YJIT_Utils;
using HTLC_ELVISPRIME_DATA;
using HTLC_ELVISPRIME_COMMON.Query.Myboard;
using System;
using System.Data;
using Newtonsoft.Json;

namespace HTLC_ELVISPRIME_COMMON.Controllers
{
    public class Con_Board
    {
        Encryption String_Encrypt = new Encryption(); //암호화
        Common comm = new Common(); //일반 함수 
        Myboard_Query BQ = new Myboard_Query();

        DataTable dt = new DataTable();
        DataTable Resultdt = new DataTable();
        
        /// <summary>
        /// HBL 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_fnGetBoardData(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(BQ.GetBoardList_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "BOARD";

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
    }
}
