using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using HTLC_ELVISPRIME_COM.COMMON.Controllers;
//using HTLC_ELVISPRIME_COM.COMMON.YJIT_Utils;

namespace HTLC_ELVISPRIME_WEB.Controllers
{
    public class TrackingController : Controller
    {
        // GET: /Tracking/
        public ActionResult Sea()
        {
            ViewBag.MAIN_MENU2 = "E-Main";
            ViewBag.SUB_MENU10 = "E-Sub10";
            return View();
        }

        public ActionResult CargoTracking()
        {
            ViewBag.MAIN_MENU2 = "E-Main";
            ViewBag.SUB_MENU11 = "E-Sub11";
            return View();
        }
    }
}
