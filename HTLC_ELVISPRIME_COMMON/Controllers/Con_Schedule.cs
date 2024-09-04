using HTLC_ELVISPRIME_COMMON.Query.Main;
using HTLC_ELVISPRIME_COMMON.YJIT_Utils;
using HTLC_ELVISPRIME_DATA;
using Newtonsoft.Json;
using System;
using System.Data;

namespace HTLC_ELVISPRIME_COMMON.Controllers
{
    public class Con_Schedule
    {
        Encryption String_Encrypt = new Encryption(); //암호화
        Common comm = new Common(); //일반 함수 
        Schedule_Query SQ = new Schedule_Query();

        //전역 변수
        DataTable dt = new DataTable();
        DataTable Resultdt = new DataTable();
        string strJson = "";

        /// <summary>
        /// 해운 - 해운 체크박스 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetSEALinerData(string strValue)
        {
            strJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(SQ.GetSEALinerData_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Liner";

                if (Resultdt.Rows.Count == 0)
                {
                    strJson = comm.MakeJson("N", "", Resultdt);
                }
                else
                {
                    strJson = comm.MakeJson("Y", "", Resultdt);
                }

                return strJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                strJson = comm.MakeJson("E", e.Message);
                strJson = String_Encrypt.decryptAES256(strJson);
                return strJson;
            }

        }

        /// <summary>
        /// 해운 - 스케줄 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetSEAScheduleData(string strValue)
        {
            strJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(SQ.GetSEAScheduleData_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Schedule";

                if (Resultdt.Rows.Count == 0)
                {
                    strJson = comm.MakeJson("N", "", Resultdt);
                }
                else
                {
                    strJson = comm.MakeJson("Y", "", Resultdt);
                }

                return strJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                strJson = comm.MakeJson("E", e.Message);
                strJson = String_Encrypt.decryptAES256(strJson);
                return strJson;
            }
        }

        /// <summary>
        /// 해운 - 체크된 해운 스케줄 오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetSEAChkScheduleData(string strValue)
        {
            strJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(SQ.GetSEAChkSchedule_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Schedule";

                if (Resultdt.Rows.Count == 0)
                {
                    strJson = comm.MakeJson("N", "", Resultdt);
                }
                else
                {
                    strJson = comm.MakeJson("Y", "", Resultdt);
                }

                return strJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                strJson = comm.MakeJson("E", e.Message);
                strJson = String_Encrypt.decryptAES256(strJson);
                return strJson;
            }
        }

        /// <summary>
        /// 항공 - 항공사 체크박스 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetAIRLinerData(string strValue)
        {
            strJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(SQ.GetAIRLinerData_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Liner";

                if (Resultdt.Rows.Count == 0)
                {
                    strJson = comm.MakeJson("N", "", Resultdt);
                }
                else
                {
                    strJson = comm.MakeJson("Y", "", Resultdt);
                }

                return strJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                strJson = comm.MakeJson("E", e.Message);
                strJson = String_Encrypt.decryptAES256(strJson);
                return strJson;
            }

        }

        /// <summary>
        /// 항공 - 스케줄 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetAIRScheduleData(string strValue)
        {
            strJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(SQ.GetAIRScheduleData_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Schedule";

                if (Resultdt.Rows.Count == 0)
                {
                    strJson = comm.MakeJson("N", "", Resultdt);
                }
                else
                {
                    strJson = comm.MakeJson("Y", "", Resultdt);
                }

                return strJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                strJson = comm.MakeJson("E", e.Message);
                strJson = String_Encrypt.decryptAES256(strJson);
                return strJson;
            }
        }

        /// <summary>
        /// 항공 - 체크된 항공사의 데이터만 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetAIRChkScheduleData(string strValue)
        {
            strJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(SQ.GetAIRChkSchedule_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Schedule";

                if (Resultdt.Rows.Count == 0)
                {
                    strJson = comm.MakeJson("N", "", Resultdt);
                }
                else
                {
                    strJson = comm.MakeJson("Y", "", Resultdt);
                }

                return strJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                strJson = comm.MakeJson("E", e.Message);
                strJson = String_Encrypt.decryptAES256(strJson);
                return strJson;
            }
        }
    }
}
