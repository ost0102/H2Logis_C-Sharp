using HTLC_ELVISPRIME_COMMON.Query.Main;
using HTLC_ELVISPRIME_COMMON.YJIT_Utils;
using HTLC_ELVISPRIME_DATA;
using Newtonsoft.Json;
using System;
using System.Data;


namespace HTLC_ELVISPRIME_COMMON.Controllers
{
    public class Con_Main
    {
        Encryption String_Encrypt = new Encryption(); //암호화
        Common comm = new Common(); //일반 함수 
        Main_Query MQ = new Main_Query();

        //전역 변수
        DataTable dt = new DataTable();
        DataTable Resultdt = new DataTable();
        DataSet ds = new DataSet();

        /// <summary>
        /// 스케줄 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetSEAScheduleData(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(MQ.GetSEAScheduleData_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Schedule";

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
        /// 스케줄 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetAIRScheduleData(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(MQ.GetAIRScheduleData_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Schedule";

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
        /// [CRM]공지사항 리스트 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetNoticeList(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "PRIME";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(MQ.Query_GetNoticeList(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "NoticeList";

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
        /// Sea - Tracking 리스트 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetSeaTrackingList(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";
            DataSet ds = new DataSet();
            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                DataTable Checkdt = DataHelper.ExecuteDataTable(MQ.Query_GetSeaTrackingBLData(dt.Rows[0]), CommandType.Text);

                if (Checkdt != null)
                {
                    if (Checkdt.Rows.Count == 0)
                    {
                        rtnJson = comm.MakeJson("N", "데이터 없음");
                    }
                    else
                    {
                        ds.Tables.Add(Checkdt);
                        Resultdt = DataHelper.ExecuteDataTable(MQ.Query_GetSeaTrackingData(Checkdt.Rows[0]), CommandType.Text);
                        Resultdt.TableName = "TrackingList";

                        if (Resultdt.Rows.Count == 0)
                        {
                            rtnJson = comm.MakeJson("N", "데이터 없음");
                        }
                        else
                        {
                            ds.Tables.Add(Resultdt);
                            rtnJson = comm.DS_MakeJson("Y", "Success", ds);
                        }
                    }
                }
                else
                {
                    rtnJson = comm.MakeJson("N", "데이터가 없습니다.");
                }

                //Resultdt = DataHelper.ExecuteDataTable(MQ.Query_GetTrackMstList(dt.Rows[0]), CommandType.Text);
                //Resultdt.TableName = "Main";
                //ds.Tables.Add(Resultdt);
                //
                //if (Resultdt.Rows.Count == 0)
                //{
                //    rtnJson = comm.MakeJson("N", "No Data", Resultdt);
                //}
                //else
                //{
                //    Resultdt = DataHelper.ExecuteDataTable(MQ.Query_GetTrackDTL(Resultdt.Rows[0]), CommandType.Text);
                //    Resultdt.TableName = "DTL";
                //    ds.Tables.Add(Resultdt);
                //
                //    if (Resultdt.Rows.Count == 0)
                //    {
                //        rtnJson = comm.DS_MakeJson("N", "", ds);
                //    }
                //    else
                //    {
                //        rtnJson = comm.DS_MakeJson("Y", "", ds);
                //    }
                //}

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
        /// Air - Tracking 리스트 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetAirTrackingList(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(MQ.Query_GetAirTrackingBLData(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Main";
                ds.Tables.Add(Resultdt);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "No Data", Resultdt);
                }
                else
                {
                    Resultdt = DataHelper.ExecuteDataTable(MQ.Query_GetAirTrackingData(Resultdt.Rows[0]), CommandType.Text);
                    Resultdt.TableName = "DTL";
                    ds.Tables.Add(Resultdt);

                    if (Resultdt.Rows.Count == 0)
                    {
                        rtnJson = comm.DS_MakeJson("N", "No Tracking", ds);
                    }
                    else
                    {
                        rtnJson = comm.DS_MakeJson("Y", "Success", ds);
                    }
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
        /// Tracking 마일스톤 디테일 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetTrackDetail(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(MQ.Query_GetTrackDTL(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "DTL";

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
        /// HBL_NO CheckBL 확인
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_fnIsCheckBL(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(MQ.Query_fnIsCheckBL(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Check";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "Fail", Resultdt);
                }
                else
                {
                    rtnJson = comm.MakeJson("Y", "Success", Resultdt);
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

        public string SetSvtgAuthToken(string rtnVal)
        {
            string rtnJson = "";
            DataTable rtnDt = JsonConvert.DeserializeObject<DataTable>(rtnVal);
            try
            {
                if (rtnVal != null)
                {
                    DataTable dt = new DataTable();


                    DataHelper.ExecuteNonQuery(MQ.SetSvtgAuthToken(rtnDt.Rows[0]), CommandType.Text);
                    rtnJson = comm.MakeJson("Y", "Success");

                }
                return rtnJson;
            }
            catch (Exception e)

            {
                return "";
            }
        }

        /// <summary>
        /// 로그인
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_fnLogin(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            DataHelper.ConnectionString_Select = "ELVIS";

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(MQ.GetUserInfo(dt.Rows[0]), CommandType.Text);
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
        /// 메일 자동 로그인
        /// </summary>
        /// <param name="loginObj"></param>
        /// <returns></returns>
        public string GetMailLogin(string loginObj)
        {
            Boolean ErrorOccur = false;
            string rtnJson = "";

            DataHelper.ConnectionString_Select = "ELVIS";

            DataTable rtnDt = JsonConvert.DeserializeObject<DataTable>(loginObj);
            try
            {
                if (loginObj != null)
                {
                    DataTable resultUserInfo = DataHelper.ExecuteDataTable(MQ.MailLogin(rtnDt.Rows[0]), CommandType.Text);
                    if (resultUserInfo.Rows.Count == 1)
                    {
                        DataTable ResultDT = comm.MakeResultDT("Y", "로그인 성공");
                        rtnJson = comm.MakeJson(ResultDT, resultUserInfo);
                        return rtnJson;
                    }
                    else
                    {
                        rtnJson = comm.MakeJson("N", "로그인 실패 하였습니다. ");
                    }
                }
                else
                {
                    //Json Data is Null 
                    rtnJson = comm.MakeJson("N", "로그인 실패 하였습니다. ");
                }

            }
            catch (Exception e)
            {
                rtnJson = comm.MakeJson("N", "로그인 실패 하였습니다. ");

            }
            return rtnJson;
        }
    }
}
