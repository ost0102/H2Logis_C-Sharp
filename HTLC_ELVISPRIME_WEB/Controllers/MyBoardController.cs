
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace HTLC_ELVISPRIME_WEB.Controllers
{
    public class MyBoardController : Controller
    {
        //
        //Encryption ec = new Encryption(); //DB_Data - Encryption  
        //Con_Board Con_Board = new Con_Board();
        //string strJson = "";
        //string strResult = "";
        // GET: /Default1/

        public ActionResult MyBoard()
        {
            return View();
        }

        public class JsonData
        {
            public string vJsonData { get; set; }
        }

        /// <summary>
        /// MyBoard - Myboard 검색
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult fnGetBoradData(JsonData value)
        //{
        //    try
        //    {
        //        string vJsonData = value.vJsonData.ToString();
        //        string vEncodeData = "";
        //
        //        //암호화 걸기
        //        vEncodeData = ec.encryptAES256(vJsonData);
        //
        //        strResult = Con_Board.Con_fnGetBoardData(vEncodeData);
        //
        //        strJson = ec.decryptAES256(strResult);
        //
        //        return Json(strJson);
        //    }
        //    catch (Exception e)
        //    {
        //        strJson = e.Message;
        //        return Json(strJson);
        //    }
        //}
    }
}
