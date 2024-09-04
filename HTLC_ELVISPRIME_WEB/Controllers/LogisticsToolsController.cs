using HTLC_ELVISPRIME_COMMON.YJIT_Utils;
using HTLC_ELVISPRIME_COMMON.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using System.Configuration;

namespace HTLC_ELVISPRIME_WEB.Controllers
{
    public class LogisticsToolsController : Controller
    {

        //
        // GET: /Default1/
        Encryption ec = new Encryption(); //DB_Data - Encryption     
        Con_LogisticsTools CL = new Con_LogisticsTools(); 
        String strResult = "";
        String strJson = "";

        DataTable dt = new DataTable();

        public ActionResult OB()
        {
            ViewBag.MAIN_MENU = "LogisticsMain";
            ViewBag.SUB_MENU2 = "LogisticsSub2";
            return View();
        }
        public ActionResult Cargo()
        {
            ViewBag.MAIN_MENU = "LogisticsMain";
            ViewBag.SUB_MENU1 = "LogisticsSub1";
            return View();
        }
        public ActionResult CBM()
        {
            ViewBag.MAIN_MENU = "LogisticsMain";
            ViewBag.SUB_MENU9 = "LogisticsSub9";
            return View();
        }
        public ActionResult Surcharge()
        {
            ViewBag.MAIN_MENU = "LogisticsMain";
            ViewBag.SUB_MENU4 = "LogisticsSub4";
            return View();
        }
        public ActionResult Incoterms()
        {
            ViewBag.MAIN_MENU = "LogisticsMain";
            ViewBag.SUB_MENU6 = "LogisticsSub6";
            return View();
        }
        public ActionResult PortLocation()
        {
            ViewBag.MAIN_MENU = "LogisticsMain";
            ViewBag.SUB_MENU8 = "LogisticsSub8";
            return View();
        }
        public ActionResult Tariff()
        {
            ViewBag.MAIN_MENU = "LogisticsMain";
            ViewBag.SUB_MENU3 = "LogisticsSub3";
            return View();
        }
        public ActionResult AEOInfo()
        {
            ViewBag.MAIN_MENU = "LogisticsMain";
            ViewBag.SUB_MENU5 = "LogisticsSub5";
            return View();
        }
        public ActionResult Vehicle()
        {
            ViewBag.MAIN_MENU = "LogisticsMain";
            ViewBag.SUB_MENU7 = "LogisticsSub7";
            return View();
        }

        public class JsonData
        {
            public string vJsonData { get; set; }
        }

        [HttpPost]
        public ActionResult GetCargoInfo(JsonData value)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            string vJsonData = value.vJsonData.ToString();
            dt = JsonConvert.DeserializeObject<DataTable>(vJsonData);

            string str_crkyCn = ConfigurationManager.AppSettings["CargoCrkyCn"].ToString();
            string str_cargMtNo = dt.Rows[0]["cargMtNo"].ToString();
            string str_mblNo = dt.Rows[0]["mblNo"].ToString();
            string str_hblNo = dt.Rows[0]["hblNo"].ToString();
            string str_blYy = dt.Rows[0]["blYy"].ToString();
            string sURL = "";
            string ReadingText = "";

            if (string.IsNullOrEmpty(str_cargMtNo) == false)
            {
                sURL = "https://unipass.customs.go.kr:38010/ext/rest/cargCsclPrgsInfoQry/retrieveCargCsclPrgsInfo?crkyCn=" + str_crkyCn + "&cargMtNo=" + str_cargMtNo;
            }
            else if (string.IsNullOrEmpty(str_blYy) == false)
            {
                sURL = "https://unipass.customs.go.kr:38010/ext/rest/cargCsclPrgsInfoQry/retrieveCargCsclPrgsInfo?crkyCn=" + str_crkyCn + "&mblNo=" + str_mblNo + "&hblNo=" + str_hblNo + "&blYy=" + str_blYy;
            }
            else
            {
                sURL = "https://unipass.customs.go.kr:38010/ext/rest/cargCsclPrgsInfoQry/retrieveCargCsclPrgsInfo?crkyCn=" + str_crkyCn;
            }

            if (sURL != "")
            {
                WebRequest wrGETURL;
               
                wrGETURL = WebRequest.Create(sURL);
                WebProxy myProxy = new WebProxy("myproxy", 80);

                myProxy.BypassProxyOnLocal = true;



                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);

                string sLine = "";
                int i = 0;
                while (sLine != null)
                {
                    i++;
                    sLine = objReader.ReadLine();
                    ReadingText += sLine;
                }
            }

            return this.Content(ReadingText, "text/xml");
        }

        [HttpPost]
        public ActionResult GetOBNumInfo(JsonData value)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            string vJsonData = value.vJsonData.ToString();
            dt = JsonConvert.DeserializeObject<DataTable>(vJsonData);

            string str_crkyCn = ConfigurationManager.AppSettings["OBcrkyCn"].ToString();
            string str_expDclrNo = dt.Rows[0]["expDclrNo"].ToString();
            string str_Url = "";
            string ReadingText = "";
            WebRequest wrGETURL;

            if (string.IsNullOrEmpty(str_expDclrNo) == false)
            {
                str_Url = "https://unipass.customs.go.kr:38010/ext/rest/expDclrNoPrExpFfmnBrkdQry/retrieveExpDclrNoPrExpFfmnBrkd?crkyCn=" + str_crkyCn + "&expDclrNo=" + str_expDclrNo;
            }
            else
            {
                str_Url = "https://unipass.customs.go.kr:38010/ext/rest/expDclrNoPrExpFfmnBrkdQry/retrieveExpDclrNoPrExpFfmnBrkd?crkyCn=" + str_crkyCn;
            }

            if (str_Url != "")
            {
                wrGETURL = WebRequest.Create(str_Url);
                WebProxy myProxy = new WebProxy("myproxy", 80);

                myProxy.BypassProxyOnLocal = true;

                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();

                StreamReader objReader = new StreamReader(objStream);

                string sLine = "";
                int i = 0;
                while (sLine != null)
                {
                    i++;
                    sLine = objReader.ReadLine();
                    ReadingText += sLine;
                }
            }

            return this.Content(ReadingText, "text/xml");
        }


        /// <summary>
		/// 수출이행내역 XML 가져오기 - B/L
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		[HttpPost]
        public ActionResult GetOBBLInfo(JsonData value)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            string vJsonData = value.vJsonData.ToString();
            dt = JsonConvert.DeserializeObject<DataTable>(vJsonData);

            string str_crkyCn = ConfigurationManager.AppSettings["OBcrkyCn"].ToString();
            string str_blNo = dt.Rows[0]["blNo"].ToString();
            string str_Url = "";
            string ReadingText = "";
            WebRequest wrGETURL;

            if (string.IsNullOrEmpty(str_blNo) == false)
            {
                str_Url = "https://unipass.customs.go.kr:38010/ext/rest/expDclrNoPrExpFfmnBrkdQry/retrieveExpDclrNoPrExpFfmnBrkd?crkyCn=" + str_crkyCn + "&blNo=" + str_blNo;
            }
            else
            {
                str_Url = "https://unipass.customs.go.kr:38010/ext/rest/expDclrNoPrExpFfmnBrkdQry/retrieveExpDclrNoPrExpFfmnBrkd?crkyCn=" + str_crkyCn;
            }


            if (str_Url != "")
            {
                wrGETURL = WebRequest.Create(str_Url);
                WebProxy myProxy = new WebProxy("myproxy", 80);

                myProxy.BypassProxyOnLocal = true;

                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();

                StreamReader objReader = new StreamReader(objStream);

                string sLine = "";
                int i = 0;
                while (sLine != null)
                {
                    i++;
                    sLine = objReader.ReadLine();
                    ReadingText += sLine;
                }
            }

            return this.Content(ReadingText, "text/xml");
        }
        /// <summary>
        /// 분기 / 년 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetYearQuarter(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_SetYearQuarter(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 전체(시.도) 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetAddrState(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_SetAddrState(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 전체(시.군.구) 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetAddrCity(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_SetAddrCity(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 전체(읍,면,동 - 행정동)데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetAddrTownship(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_SetAddrTownship(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 전체(읍,면,동 - 법정동) 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetAddrTownship2(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_SetAddrTownship2(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 항구 기점 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetSection(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_fnSetSection(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }


        /// <summary>
        /// 할증 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetPremiumRate(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_SetPremiumRate(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 안전운임 데이터 검색
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnTariffSerach(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_TariffSerach(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 안전운임 데이터 검색
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetSurchargePort(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_SetSurchargePort(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 수입 / 수출 국가옵션 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetSurchargeCountry(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_SetSurchargeCountry(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 부대비용 검색 리스트 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSearchSurcharge(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_SearchSurcharge(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        ///////////////////////////////////////차량 제원 컨트롤러/////////////////////////////////////////////////
        ////// <summary>
        /// 차량 제원 구분 Select 박스 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetVehicleDiv()
        {
            try
            {
                strResult = CL.Con_SetVehicleDiv();

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        ////// <summary>
        /// 차량 제원 구분 Select 박스 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSearchVehicle(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = CL.Con_SearchVehicle(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

    }
}
