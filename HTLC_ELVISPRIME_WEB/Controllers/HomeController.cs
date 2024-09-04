using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using HTLC_ELVISPRIME_COMMON.YJIT_Utils;
using HTLC_ELVISPRIME_COMMON.Controllers;

namespace HTLC_ELVISPRIME_WEB.Controllers
{
    public class HomeController : Controller
    {
        Encryption ec = new Encryption(); //DB_Data - Encryption      
        Con_Main Con_Main = new Con_Main();
        Con_Schedule Con_Schedule = new Con_Schedule();

        DataTable dt = new DataTable();
        string strJson = "";
        string strResult = "";
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public class JsonData
        {
            public string vJsonData { get; set; }
        }
        public class LoginCls
        {
            public string paramObj { get; set; }
        }

        [HttpPost]
        public ActionResult fnIsCheckBL(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = Con_Main.Con_fnIsCheckBL(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        public string SetSvtgAuthToken(LoginCls loginObj)
        {
            string returnVal = "";

            Con_Main Con = new Con_Main();

            returnVal = Con.SetSvtgAuthToken(loginObj.paramObj);

            return returnVal;
        }
        /// <summary>
        /// 공지사항 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnGetNoticeList(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = Con_Main.Con_GetNoticeList(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        [HttpPost]
        public ActionResult GetSeaTrackingList(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = Con_Main.Con_GetSeaTrackingList(vEncodeData);

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
        /// 선사 / 훼리 라이너 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnGetSEALiner(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = Con_Schedule.Con_GetSEALinerData(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                //ds = JsonConvert.DeserializeObject<DataSet>(strJson);
                //dt = ds.Tables["Result"];

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 선사 / 훼리 스케줄 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnGetSEASchedule(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = Con_Schedule.Con_GetSEAScheduleData(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                //ds = JsonConvert.DeserializeObject<DataSet>(strJson);
                //dt = ds.Tables["Result"];

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 선사 / 훼리 Carr 선택 된 선사만 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnGetSEAChkSchedule(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = Con_Schedule.Con_GetSEAChkScheduleData(vEncodeData);

                strJson = ec.decryptAES256(strResult);

                //ds = JsonConvert.DeserializeObject<DataSet>(strJson);
                //dt = ds.Tables["Result"];

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }


        /// <summary>
        /// 로그인 함수
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnLogin(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = ec.encryptAES256(vJsonData);

                strResult = Con_Main.Con_fnLogin(vEncodeData);

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
        /// 로그인 후 데이터 세션에 Save
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionResult SaveLogin(JsonData value)
        {
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(value.vJsonData);
            DataTable rst = ds.Tables["Result"];
            DataTable dt = ds.Tables["Table"];

            try
            {
                if (rst.Rows[0]["trxCode"].ToString() == "N") return Content("N");

                if (rst.Rows[0]["trxCode"].ToString() == "Y")
                {
                    Session["USR_ID"] = dt.Rows[0]["USR_ID"].ToString();
                    Session["USER_NM"] = dt.Rows[0]["USER_NM"].ToString();
                    Session["CUST_CD"] = dt.Rows[0]["CUST_CD"].ToString();
                    Session["EMAIL"] = dt.Rows[0]["EMAIL"].ToString();
                    Session["AUTH_KEY"] = dt.Rows[0]["AUTH_KEY"].ToString();
                    Session["AUTH_TYPE"] = dt.Rows[0]["AUTH_TYPE"].ToString();
                    Session["USR_TYPE"] = dt.Rows[0]["USR_TYPE"].ToString();
                    Session["HP_NO"] = dt.Rows[0]["HP_NO"].ToString();
                    Session["DOMAIN"] = System.Configuration.ConfigurationManager.AppSettings["Domain"];
                    Session["OFFICE_CD"] = dt.Rows[0]["OFFICE_CD"].ToString();
                    Session["CUST_NM"] = dt.Rows[0]["CUST_NM"].ToString();

                    return Content("Y");
                }

                return Content("N");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        /// <summary>
        /// 로그아웃 페이지
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string LogOut()
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            return "Y";
        }
    }
}
