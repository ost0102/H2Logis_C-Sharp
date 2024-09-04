using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLC_ELVISPRIME_WEB.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/
        /*
                 *    <li><a href="/company/greeting.html"><span>인사말</span></a></li>
                                                <li><a href="/company/history.html"><span>회사개요 및 연혁</span></a></li>
                                                <li><a href="/company/organ.html"><span>조직도</span></a></li>
                                                <li><a href="/company/contact.html"><span>연락처</span></a></li>
                                                <li><a href="/company/philosophy.html"><span>경영철학</span></a></li>
                                                <li><a href="/company/map.html"><span>오시는길</span></a></li>
                */
        public ActionResult greeting()  //인사말
        {
            ViewBag.MAIN_MENU3 = "AboutUs";
            ViewBag.SUB_MENU12 = "History";
            ViewBag.SUB_MENU13 = "Business";
            ViewBag.SUB_MENU14 = "Location";
            return View();
        }

        //public ActionResult history()   //회사개요 및 연혁
        //{
        //    ViewBag.SUB_PAGE = "AboutUs";
        //    return View();
        //}

        //public ActionResult map()   //연락처
        //{
        //    ViewBag.SUB_PAGE = "AboutUs";
        //    return View();
        //}
        

    }
}
