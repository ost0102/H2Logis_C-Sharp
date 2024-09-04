using HTLC_ELVISPRIME_COMMON.Query.Admin;
using HTLC_ELVISPRIME_COMMON.YJIT_Utils;
using HTLC_ELVISPRIME_DATA;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace HTLC_ELVISPRIME_COMMON.Controllers
{
    public class Con_Admin
    {
        Encryption String_Encrypt = new Encryption(); //암호화
        Common comm = new Common(); //일반 함수 
        Admin_Query AQ = new Admin_Query();             //어드민 객체   
        Notice_Query NQ = new Notice_Query();           //공지사항 객체     
        Surcharge_Query SQ = new Surcharge_Query();     //부대비용 객체
        Vehicle_Query VQ = new Vehicle_Query();         //차량 제원 함수 객체
        Member_Query MQ = new Member_Query();           //관리자 관리 객체
        Tariff_Query TQ = new Tariff_Query();           //안전운임제 관리 객체
        Tariff_PR_Query TPQ = new Tariff_PR_Query();    //안전운임제 할증 관리 객체

        //전역변수 선언
        string rtnJson = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable Resultdt = new DataTable();

        /*************************관리자 페이지*******************************/
        public string Con_adminLogin(string id, string pwd, string memberkey)
        {
            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(AQ.fnAdminLogin_Query(id, pwd, memberkey), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 파일 로그 (공통)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string Con_InsertFileLog(DataTable dt)
        {
            int nResult = 0;

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(AQ.InsertFileLog_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }
                else
                {
                    rtnJson = comm.MakeJson("Y", "Success");
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

        public DataTable Con_NoticeView(string id)
        {
            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(NQ.AdminNoticeView_Query(id), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    //rtnJson = comm.MakeJson("N", "");
                    Resultdt = null;
                    return Resultdt;
                }
                else
                {
                    //rtnJson = comm.MakeJson("Y", "", Resultdt);
                    return Resultdt;
                }
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                //rtnJson = comm.MakeJson("E", e.Message);
                Resultdt = null;
                return Resultdt;
            }
        }

        public DataTable Con_NoticeList(object value)
        {
            Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(value).ToString());

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(NQ.fnAdminNotice_Query(dictionary), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    //rtnJson = comm.MakeJson("N", "");
                    Resultdt = null;
                    return Resultdt;
                }
                else
                {
                    //rtnJson = comm.MakeJson("Y", "", Resultdt);
                    return Resultdt;
                }
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                //rtnJson = comm.MakeJson("E", e.Message);
                Resultdt = null;
                return Resultdt;
            }
        }

        public string Con_AdminNoticeDel(string strNoticeID)
        {
            DataHelper.ConnectionString_Select = "PRIME";
            int nResult = 0;

            try
            {
                //DataHelper.ExecuteDataTable(AQ.fnAdminNoticeView_Query(strNoticeID), CommandType.Text);
                nResult = DataHelper.ExecuteNonQuery(NQ.fnAdminNoticeDel_Query(strNoticeID), CommandType.Text);

                if (nResult == 1)
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }
                else
                {
                    rtnJson = comm.MakeJson("Y", "Success");
                }

                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }
        public string Con_NoticeInsert(Hashtable htParam)
        {
            string rtnJson = "";
            int nResult = 0;

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(NQ.NoticeInsert_Query(htParam), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "insert가 실패 하였습니다.");
                    return rtnJson;
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        public string Con_NoticeUpdate(Hashtable htParam)
        {
            string rtnJson = "";
            int nResult = 0;

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(NQ.NoticeUpdate_Query(htParam), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "Update가 실패 하였습니다.");
                    return rtnJson;
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /*********************************************************************/
        /// <summary>
        /// 안전운임 데이터 엑셀 업로드
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string Con_SaveExcel_SaveFre(DataTable dt)
        {
            int nResult = 0;
            string strLog = "";

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                //TWKIM - 벨리데이션 체크하고 데이터 넣는 로직
                //bool EmptyFlag;                

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    EmptyFlag = true;
                //
                //    //데이터 누락 체크
                //    for (int j=0; j< dt.Columns.Count; j++)
                //    {
                //        //데이터가 하나라도 비어 있다면?
                //        if(dt.Rows[i][j].ToString() == "")
                //        {
                //            strLog += "- "+(i+3) + "번째 줄에 "+(j-2)+"번째 데이터가 누락되어 저장 되지 않았습니다.</br>";
                //            EmptyFlag = false;
                //            break;
                //        }
                //    }
                //
                //    if(EmptyFlag)
                //    {
                //        Resultdt = DataHelper.ExecuteDataTable(TQ.fnCheckExcelTariff_Query(dt.Rows[i]), CommandType.Text);
                //        
                //        if (Resultdt.Rows.Count == 0)
                //        {
                //            nResult = DataHelper.ExecuteNonQuery(TQ.fnInsertExcelTariff_Query(dt.Rows[i]), CommandType.Text);
                //        
                //            if (nResult == 0)
                //            {
                //                strLog += "- " + (i+3) + "번째 줄에 데이터가 저장 되지 않았습니다.</br>";
                //            }
                //        }
                //        else
                //        {
                //            strLog += "- " + (i+3) + "번째 줄에 데이터가 중복 되어 저장 되지 않았습니다.</br>";
                //        }
                //    }
                //}
                //
                ////로그 데이터 확인
                //if(strLog == "")
                //{
                //    rtnJson = comm.MakeJson("Y", "Success");
                //}
                //else
                //{
                //    rtnJson = comm.MakeJson("N", "아래의 데이터 외 나머지 저장이 완료 되었습니다. </br> 아래의 데이터는 저장이 되지 않았습니다. </br></br>" + strLog);
                //}

                //기존에 쓰는 for문
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    nResult = DataHelper.ExecuteNonQuery(TQ.fnInsertExcelTariff_Query(dt.Rows[i]), CommandType.Text);
                //
                //    if (nResult == 0)
                //    {
                //        strLog += "- " + (i + 3) + "번째 줄에 데이터가 저장 되지 않았습니다.</br>";
                //    }
                //}

                //100개씩 끊어서 insert 시키기
                for (int i = 0; i <= dt.Rows.Count;)
                {
                    //어떻게 100개씩 만들건지 체크

                    nResult = DataHelper.ExecuteNonQuery(TQ.fnInsertExcelTariffMulti_Query(dt, i), CommandType.Text);

                    i += 1000;
                }

                if (strLog == "")
                {
                    rtnJson = comm.MakeJson("Y", "Success");
                }
                else
                {
                    rtnJson = comm.MakeJson("N", "아래의 데이터 외 나머지 저장이 완료 되었습니다. </br> 아래의 데이터는 저장이 되지 않았습니다. </br></br>" + strLog);
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
        //부대비용
        #region

        /// <summary>
        /// 부대비용 엑셀 업로드
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string Con_SaveExcel_SaveSur(DataTable dt)
        {
            int nResult = 0;
            string strLog = "";

            try
            {
                //TWKIM - 벨리데이션 체크하고 데이터 넣는 로직
                //bool EmptyFlag;                

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    EmptyFlag = true;
                //
                //    //데이터 누락 체크
                //    for (int j=0; j< dt.Columns.Count; j++)
                //    {
                //        //데이터가 하나라도 비어 있다면?
                //        if(dt.Rows[i][j].ToString() == "")
                //        {
                //            strLog += "- "+(i+2) + "번째 줄에 "+(j-2)+"번째 데이터가 누락되어 저장 되지 않았습니다.</br>";
                //            EmptyFlag = false;
                //            break;
                //        }
                //    }
                //
                //    if(EmptyFlag)
                //    {
                //        Resultdt = DataHelper.ExecuteDataTable(SQ.fnCheckExcelduplicate_Query(dt.Rows[i]), CommandType.Text);
                //        
                //        if (Resultdt.Rows.Count == 0)
                //        {
                //            nResult = DataHelper.ExecuteNonQuery(SQ.fnInsertExcelSurcharge_Query(dt.Rows[i]), CommandType.Text);
                //        
                //            if (nResult == 0)
                //            {
                //                strLog += "- " + (i+2) + "번째 줄에 데이터가 저장 되지 않았습니다.</br>";
                //            }
                //        }
                //        else
                //        {
                //            strLog += "- " + (i+2) + "번째 줄에 데이터가 중복 되어 저장 되지 않았습니다.</br>";
                //        }
                //    }
                //}
                //
                ////로그 데이터 확인
                //if(strLog == "")
                //{
                //    rtnJson = comm.MakeJson("Y", "Success");
                //}
                //else
                //{
                //    rtnJson = comm.MakeJson("N", "아래의 데이터 외 나머지 저장이 완료 되었습니다. </br> 아래의 데이터는 저장이 되지 않았습니다. </br></br>" + strLog);
                //}

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    nResult = DataHelper.ExecuteNonQuery(SQ.fnInsertExcelSurcharge_Query(dt.Rows[i]), CommandType.Text);

                    if (nResult == 0)
                    {
                        strLog += "- " + (i + 2) + "번째 줄에 데이터가 저장 되지 않았습니다.</br>";
                    }
                }

                if (strLog == "")
                {
                    rtnJson = comm.MakeJson("Y", "Success");
                }
                else
                {
                    rtnJson = comm.MakeJson("N", "아래의 데이터 외 나머지 저장이 완료 되었습니다. </br> 아래의 데이터는 저장이 되지 않았습니다. </br></br>" + strLog);
                }
                return rtnJson;

                //if (Resultdt.Rows.Count == 0)
                //{
                //    rtnJson = comm.MakeJson("N", "", Resultdt);
                //}
                //else
                //{
                //    rtnJson = comm.MakeJson("Y", "", Resultdt);
                //}

                //return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }


        /// <summary>
        /// 부대비용 포트 검색
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SetSurchargePort(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(SQ.SetSurchargePort_Query(dt.Rows[0]), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 수입 / 수출 국가옵션 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SetSurchargeCountry(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(SQ.SetSurchargeCountry_Query(dt.Rows[0]), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 부대비용 검색 리스트 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SearchSurcharge(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                DataTable CntrListDt = new DataTable();

                Resultdt = DataHelper.ExecuteDataTable(SQ.SearchSurcharge_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "SurchargeData";

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
                return rtnJson;
            }
        }

        /// <summary>
        /// 부대비용 검색 리스트 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_InsertSurcharge(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    nResult = DataHelper.ExecuteNonQuery(SQ.fnInsertSur_Query(dt.Rows[i]), CommandType.Text);

                    if (nResult == 0)
                    {
                        rtnJson = comm.MakeJson("N", "");
                        return rtnJson;
                    }
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 부대비용 - 수정 및 삭제 검색
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SetModifySearch(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                DataTable CntrListDt = new DataTable();

                Resultdt = DataHelper.ExecuteDataTable(SQ.SetModifySearch_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "SurchargeData";

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
                return rtnJson;
            }
        }

        /// <summary>
        /// 부대비용 - 데이터 수정
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SurchargeModify(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    nResult = DataHelper.ExecuteNonQuery(SQ.SurchargeModify_Query(dt.Rows[i]), CommandType.Text);

                    if (nResult == 0)
                    {
                        rtnJson = comm.MakeJson("N", "");
                        return rtnJson;
                    }
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 부대비용 - 데이터 수정
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SurchargeDelete(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    nResult = DataHelper.ExecuteNonQuery(SQ.SurchargeDelete_Query(dt.Rows[i]), CommandType.Text);

                    if (nResult == 0)
                    {
                        rtnJson = comm.MakeJson("N", "");
                        return rtnJson;
                    }
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 부대비용 - 엑셀 데이터 체크 (기존에 데이터가 있는지 체크) 
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_CheckSurchargeExcel(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(SQ.CheckSurchargeExcel_Query(dt.Rows[0]), CommandType.Text);

                if (Resultdt.Rows[0]["COUNT"].ToString() == "0")
                {
                    rtnJson = comm.MakeJson("Y", "Success", Resultdt);
                }
                else
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }

                return rtnJson;

            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 기존에 데이터가 있다면 부대비용 삭제하는 로직
        /// </summary>
        /// <returns></returns>
        public string Con_DeleteExcelSurcharge(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(SQ.DeleteExcelSurcharge_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "Delete가 실패 하였습니다.");
                    return rtnJson;
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }
        #endregion

        //차량 제원
        #region
        /// <summary>
        /// 차량 제원 구분 Select 박스 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SetVehicleDiv()
        {
            string rtnJson = "";

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(VQ.SetVehicleDiv_Query(), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 차량 제원 -  검색
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SearchVehicle(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(VQ.SearchVehicle_Query(dt.Rows[0]), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 차량 제원 - 이미지 다운로드 데이터
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_VehicleImgDown(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(VQ.VehicleImgDown_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Download";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 차량 제원 - 이미지 다운로드 데이터
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_DeleteVehicle(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(VQ.DeleteVehicle_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 1)
                {
                    rtnJson = comm.MakeJson("Y", "Success");
                }
                else
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }

                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 차량 제원 - 수정 데이터 검색
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SearchVehicleModify(string strMngtNo, string strSEQ)
        {
            string rtnJson = "";

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(VQ.SearchVehicleModify_Query(strMngtNo, strSEQ), CommandType.Text);
                Resultdt.TableName = "Vehicle";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 차량 제원 - 이미지 다운로드 데이터
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_InsertVehicle(DataTable dt)
        {
            string rtnJson = "";
            int nResult = 0;

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(VQ.InsertVehicle_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }
                else
                {
                    rtnJson = comm.MakeJson("Y", "Success");
                }

                return rtnJson;

            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 차량 제원 - 이미지 다운로드 데이터
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_UpdateVehicle(DataTable dt)
        {
            string rtnJson = "";
            int nResult = 0;

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(VQ.UpdateVehicle_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }
                else
                {
                    rtnJson = comm.MakeJson("Y", "Success");
                }

                return rtnJson;

            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }
        #endregion

        //관리자 관리
        #region
        /// <summary>
        /// 관리자 관리 - 검색
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SearchMember(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(MQ.SearchMember_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Member";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 관리자 관리 - 관리자 관리 수정 검색
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SearchMemberModify(string strUserID)
        {
            string rtnJson = "";

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(MQ.SearchMemberModify_Query(strUserID), CommandType.Text);
                Resultdt.TableName = "Member";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 관리자 관리 - 아이디 중복 체크
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_CheckIDMember(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(MQ.CheckIDMember_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Member";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("Y", "Success");                    
                }
                else
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }

                return rtnJson;

            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }        

        /// <summary>
        /// 관리자 관리 - 아이디 등록
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_InsertMember(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(MQ.InsertMember_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 1)
                {
                    rtnJson = comm.MakeJson("Y", "Success");
                }
                else
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }

                return rtnJson;

            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 관리자 관리 - 아이디 삭제
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_DeleteMember(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(MQ.DeleteMember_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 1)
                {
                    rtnJson = comm.MakeJson("Y", "Success");
                }
                else
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }

                return rtnJson;

            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 관리자 관리 - 아이디 삭제
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_ModifyMember(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(MQ.ModifyMember_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 1)
                {
                    rtnJson = comm.MakeJson("Y", "Success");
                }
                else
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }

                return rtnJson;

            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }
        #endregion

        //안전운임제 관리
        #region
        /// <summary>
        /// 분기 / 년 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SetYearQuarter(string strValue)
        {
            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TQ.SetYearQuarter_Query(), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 전체(시.도) 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SetAddrState(string strValue)
        {
            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TQ.SetAddrState_Query(), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }        

        /// <summary>
        /// 전체(시.군.구) 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SetAddrCity(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TQ.SetAddrCity_Query(dt.Rows[0]), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 전체(읍,면,동) 행정동 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SetAddrTownship(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TQ.SetAddrTownship_Query(dt.Rows[0]), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 전체(읍,면,동) 법정동 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SetAddrTownship2(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TQ.SetAddrTownship2_Query(dt.Rows[0]), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 항구 기점 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_fnSetSection(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TQ.fnSetSection_Query(dt.Rows[0]), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 구분 변경 시 PRICE 변경 로직
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_GetCntrPrice(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TQ.GetCntrPrice_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Tariff";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 안전운임 데이터 검색
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_TariffSerach(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TQ.TariffSerach_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "Tariff";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 수정 검색
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SearchTariffPRModify(string strMngtNo, string strSEQ , string strType)
        {
            string rtnJson = "";

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TQ.SearchTariffPRModify_Query(strMngtNo,strSEQ,strType), CommandType.Text);
                Resultdt.TableName = "Tariff";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 Update
        /// </summary>
        /// <returns></returns>
        public string Con_UpdateTariff(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(TQ.UpdateTariff_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "Update가 실패 하였습니다.");
                    return rtnJson;
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 Delete
        /// </summary>
        /// <returns></returns>
        public string Con_DeleteTariff(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(TQ.DeleteTariff_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "Delete가 실패 하였습니다.");
                    return rtnJson;
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 엑셀 데이터 체크 (기존에 데이터가 있는지 체크) 
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_CheckTariffExcel(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TQ.CheckTariffExcel_Query(dt.Rows[0]), CommandType.Text);

                if (Resultdt.Rows[0]["COUNT"].ToString() == "0")
                {
                    rtnJson = comm.MakeJson("Y", "Success", Resultdt);
                }
                else
                {
                    rtnJson = comm.MakeJson("N", "Fail");
                }

                return rtnJson;

            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 엑셀 업로드 전 기존 데이터 삭제 로직
        /// </summary>
        /// <returns></returns>
        public string Con_DeleteExcelTariff(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(TQ.fnDeleteExcelTariff_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "Delete가 실패 하였습니다.");
                    return rtnJson;
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        #endregion

        //안전운임제 할증 관리 
        #region
        /// <summary>
        /// 분기 / 년 데이터 가져오기
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SetPRYearQuarter(string strValue)
        {
            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TPQ.SetPRYearQuarter_Query(), CommandType.Text);

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 검색
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SearchTariffPR(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TPQ.SearchTariffPR_Query(dt.Rows[0]), CommandType.Text);
                Resultdt.TableName = "TariffPR";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 INSERT
        /// </summary>
        /// <returns></returns>
        public string Con_InsertTariffPR(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(TPQ.InsertTariffPR_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "insert가 실패 하였습니다.");
                    return rtnJson;
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 DELETE
        /// </summary>
        /// <returns></returns>
        public string Con_DeleteTariffPR(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(TPQ.DeleteTariffPR_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "Delete가 실패 하였습니다.");
                    return rtnJson;
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 수정 검색
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Con_SearchTariffPRModify(string strMngtNo, string strSEQ)
        {
            string rtnJson = "";

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                Resultdt = DataHelper.ExecuteDataTable(TPQ.SearchTariffPRModify_Query(strMngtNo, strSEQ), CommandType.Text);
                Resultdt.TableName = "TariffPR";

                if (Resultdt.Rows.Count == 0)
                {
                    rtnJson = comm.MakeJson("N", "");
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
                return rtnJson;
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 Update
        /// </summary>
        /// <returns></returns>
        public string Con_UpdateTariffPR(string strValue)
        {
            string rtnJson = "";
            string strResult = String_Encrypt.decryptAES256(strValue);
            int nResult = 0;

            //데이터
            dt = JsonConvert.DeserializeObject<DataTable>(strResult);

            DataHelper.ConnectionString_Select = "PRIME";

            try
            {
                nResult = DataHelper.ExecuteNonQuery(TPQ.UpdateTariffPR_Query(dt.Rows[0]), CommandType.Text);

                if (nResult == 0)
                {
                    rtnJson = comm.MakeJson("N", "Update가 실패 하였습니다.");
                    return rtnJson;
                }

                rtnJson = comm.MakeJson("Y", "Success");
                return rtnJson;
            }
            catch (Exception e)
            {
                //만약 오류가 발생 하였을 경우
                rtnJson = comm.MakeJson("E", e.Message);
                return rtnJson;
            }
        }

        #endregion
    }
}
