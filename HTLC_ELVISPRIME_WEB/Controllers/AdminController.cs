using HTLC_ELVISPRIME_COMMON.Controllers;
using HTLC_ELVISPRIME_COMMON.YJIT_Utils;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HTLC_ELVISPRIME_WEB.Controllers.Admin
{
    public class AdminController : Controller
    {
        //
        // GET: /Default1/
        Con_Admin CA = new Con_Admin();
        string _EditorFilePath = "/data/editor/";
        const string scriptTag = "<script type='text/javascript'>window.parent.CKEDITOR.tools.callFunction({0}, '{1}', '{2}')</script>";
        DataTable dt = new DataTable();
        Encryption String_Encrypt = new Encryption();
        HTLC_ELVISPRIME_COMMON.YJIT_Utils.Common Comm = new HTLC_ELVISPRIME_COMMON.YJIT_Utils.Common();
        string strResult = "";
        string _NoticeFilePath = "/data/notice/";
        string strJson = "";
        string memberKey = ConfigurationManager.AppSettings["memberKey"].ToString();

        public ActionResult AEOAdmin()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Member()
        {
            return View();
        }
        public ActionResult Notice()
        {
            return View();
        }
        public ActionResult NoticeWrite(string id)
        {

            if (id == null) return View();

            dt = CA.Con_NoticeView(id);

            if (dt.Rows.Count > 0)
            {

                if (dt.Columns.Contains("FLAG"))
                {
                    ViewBag.FLAG = dt.Rows[0]["FLAG"].ToString();
                }

                if (dt.Columns.Contains("NOTICE_ID"))
                {
                    ViewBag.NOTICE_ID = dt.Rows[0]["NOTICE_ID"].ToString();
                }

                if (dt.Columns.Contains("TITLE"))
                {
                    ViewBag.TITLE2 = dt.Rows[0]["TITLE"].ToString();
                }

                if (dt.Columns.Contains("TYPE"))
                {
                    ViewBag.TYPE = dt.Rows[0]["TYPE"].ToString();
                }

                if (dt.Columns.Contains("CNT"))
                {
                    ViewBag.CNT = dt.Rows[0]["CNT"].ToString();
                }

                if (dt.Columns.Contains("WRITER"))
                {
                    ViewBag.WRITER = dt.Rows[0]["WRITER"].ToString();
                }

                if (dt.Columns.Contains("USE_YN"))
                {
                    ViewBag.USE_YN = dt.Rows[0]["USE_YN"].ToString();
                }

                if (dt.Columns.Contains("NOTICE_YN"))
                {
                    ViewBag.NOTICE_YN = dt.Rows[0]["NOTICE_YN"].ToString();
                }

                if (dt.Columns.Contains("REGDT"))
                {
                    ViewBag.REGDT = dt.Rows[0]["REGDT"].ToString();
                }

                if (dt.Columns.Contains("EDITDT"))
                {
                    ViewBag.EDITDT = dt.Rows[0]["EDITDT"].ToString();
                }

                if (dt.Columns.Contains("FILE"))
                {
                    ViewBag.FILE = dt.Rows[0]["FILE"].ToString();
                }

                if (dt.Columns.Contains("FILE_NAME"))
                {
                    ViewBag.FILE_NAME = dt.Rows[0]["FILE_NAME"].ToString();
                }

                if (dt.Columns.Contains("FILE1"))
                {
                    ViewBag.FILE1 = dt.Rows[0]["FILE1"].ToString();
                }

                if (dt.Columns.Contains("FILE1_NAME"))
                {
                    ViewBag.FILE1_NAME = dt.Rows[0]["FILE1_NAME"].ToString();
                }

                if (dt.Columns.Contains("FILE2"))
                {
                    ViewBag.FILE2 = dt.Rows[0]["FILE2"].ToString();
                }

                if (dt.Columns.Contains("FILE2_NAME"))
                {
                    ViewBag.FILE2_NAME = dt.Rows[0]["FILE2_NAME"].ToString();
                }

                if (dt.Columns.Contains("CONTENT"))
                {
                    ViewBag.CONTENT = dt.Rows[0]["CONTENT"].ToString();
                }
            }

            return View();
        }


        public ActionResult MemberWrite(string UserID)
        {
            //신규, 수정
            if (UserID == null)
            {
                return View();
            }
            else
            {
                //Search 한번 하고 고고
                strResult = CA.Con_SearchMemberModify(UserID);
                strResult = String_Encrypt.decryptAES256(strResult);
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(strResult);

                DataTable dt_result = ds.Tables["Result"];

                if (dt_result.Rows[0]["trxCode"].ToString() == "Y")
                {
                    dt = ds.Tables["Member"];

                    if (dt.Rows[0]["MEMB_NO"] != null)
                    {
                        ViewBag.MEMB_NO = dt.Rows[0]["MEMB_NO"];
                    }

                    if (dt.Rows[0]["M_ID"] != null)
                    {
                        ViewBag.M_ID = dt.Rows[0]["M_ID"];
                    }

                    if (dt.Rows[0]["LVL"] != null)
                    {
                        ViewBag.LVL = dt.Rows[0]["LVL"];
                    }

                    if (dt.Rows[0]["AUTH_LEVEL"] != null)
                    {
                        ViewBag.AUTH_LEVEL = dt.Rows[0]["AUTH_LEVEL"];
                    }

                    if (dt.Rows[0]["STATUS"] != null)
                    {
                        ViewBag.STATUS = dt.Rows[0]["STATUS"];
                    }

                    if (dt.Rows[0]["M_NAME"] != null)
                    {
                        ViewBag.M_NAME = dt.Rows[0]["M_NAME"];
                    }

                    string[] mList = dt.Rows[0]["MOBILE"].ToString().Split(new char[] { '-' });

                    if (mList.Length > 0)
                    {
                        switch (mList.Length)
                        {
                            case 1:
                                ViewBag.MOBILE1 = mList[0];
                                break;
                            case 2:
                                ViewBag.MOBILE1 = mList[0];
                                ViewBag.MOBILE2 = mList[1];
                                break;
                            case 3:
                                ViewBag.MOBILE1 = mList[0];
                                ViewBag.MOBILE2 = mList[1];
                                ViewBag.MOBILE3 = mList[2];
                                break;
                        }
                    }

                    if (dt.Rows[0]["MOBILE"] != null)
                    {
                        ViewBag.MOBILE = dt.Rows[0]["MOBILE"];
                    }

                    if (dt.Rows[0]["REGDT"] != null)
                    {
                        ViewBag.REGDT = dt.Rows[0]["REGDT"];
                    }

                    if (dt.Rows[0]["LAST_LOGIN"] != null)
                    {
                        ViewBag.LAST_LOGIN = dt.Rows[0]["LAST_LOGIN"];
                    }
                }
                return View();
            }
        }

        public ActionResult SurchargeAdmin()
        {
            return View();
        }

        public ActionResult SurchargeWrite(string Ship, string Bound, string Port, string CntrType, string CntrSize, string Country)
        {
            //신규, 수정
            if (Ship == null)
            {
                return View();
            }
            else
            {
                //여기는 데이터가지고 받아서 그려줘야됨.
                ViewBag.SHIPPING = Ship;
                ViewBag.BOUND = Bound;
                ViewBag.PORT = Port;
                ViewBag.CNTRTYPE = CntrType;
                ViewBag.CNTRSIZE = CntrSize;
                ViewBag.COUNTRY = Country;

                return View();
            }
        }

        public ActionResult TariffAdmin()
        {
            return View();
        }
        public ActionResult TariffWrite(string MngtNo, string SEQ, string TYPE)
        {
            //신규, 수정
            if (MngtNo == null)
            {
                return View();
            }
            else
            {
                //Search 한번 하고 고고
                strResult = CA.Con_SearchTariffPRModify(MngtNo, SEQ, TYPE);
                strResult = String_Encrypt.decryptAES256(strResult);
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(strResult);

                DataTable dt_result = ds.Tables["Result"];

                if (dt_result.Rows[0]["trxCode"].ToString() == "Y")
                {
                    dt = ds.Tables["Tariff"];

                    //MNGT_NO - 관리번호
                    if (dt.Rows[0]["MNGT_NO"] != null)
                    {
                        ViewBag.MNGT_NO = dt.Rows[0]["MNGT_NO"];
                    }

                    //SEQ - 시퀀스 번호
                    if (dt.Rows[0]["SEQ"] != null)
                    {
                        ViewBag.SEQ = dt.Rows[0]["SEQ"];
                    }

                    //TYPE - 타입
                    if (TYPE != null)
                    {
                        ViewBag.TYPE = TYPE;
                    }

                    //PERIOD_YEAR - 기간
                    if (dt.Rows[0]["PERIOD_YEAR"] != null)
                    {
                        ViewBag.PERIOD_YEAR = dt.Rows[0]["PERIOD_YEAR"];
                    }

                    //PERIOD_QUARTER - 분기
                    if (dt.Rows[0]["PERIOD_QUARTER"] != null)
                    {
                        ViewBag.PERIOD_QUARTER = dt.Rows[0]["PERIOD_QUARTER"];
                    }

                    //SECTION - 왕복 , 편도
                    if (dt.Rows[0]["SECTION"] != null)
                    {
                        ViewBag.SECTION = dt.Rows[0]["SECTION"];
                    }

                    //ADDR_STATE - 시,도
                    if (dt.Rows[0]["ADDR_STATE"] != null)
                    {
                        ViewBag.ADDR_STATE = dt.Rows[0]["ADDR_STATE"];
                    }
                    //ADDR_CITY - 시,군,구
                    if (dt.Rows[0]["ADDR_CITY"] != null)
                    {
                        ViewBag.ADDR_CITY = dt.Rows[0]["ADDR_CITY"];
                    }

                    //ADDR_TOWNSHIP - 읍,면,동
                    if (dt.Rows[0]["ADDR_TOWNSHIP"] != null)
                    {
                        ViewBag.ADDR_TOWNSHIP = dt.Rows[0]["ADDR_TOWNSHIP"];
                    }

                    //PORT - 항구
                    if (dt.Rows[0]["PORT"] != null)
                    {
                        ViewBag.PORT = dt.Rows[0]["PORT"];
                    }

                    //DISTANCE - 거리
                    if (dt.Rows[0]["DISTANCE"] != null)
                    {
                        ViewBag.DISTANCE = dt.Rows[0]["DISTANCE"];
                    }

                    //20FT - 20FT
                    if (dt.Rows[0]["CNTR_20FT"] != null)
                    {
                        ViewBag.CNTR_20FT = dt.Rows[0]["CNTR_20FT"];
                    }

                    //40FT - 40FT
                    if (dt.Rows[0]["CNTR_40FT"] != null)
                    {
                        ViewBag.CNTR_40FT = dt.Rows[0]["CNTR_40FT"];
                    }
                }
                return View();
            }
        }
        public ActionResult TariffPremium()
        {
            return View();
        }
        public ActionResult TariffPremiumWrite(string MngtNo, string SEQ)
        {
            //신규, 수정
            if (MngtNo == null)
            {
                return View();
            }
            else
            {
                //Search 한번 하고 고고
                strResult = CA.Con_SearchTariffPRModify(MngtNo, SEQ);
                strResult = String_Encrypt.decryptAES256(strResult);
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(strResult);

                DataTable dt_result = ds.Tables["Result"];

                if (dt_result.Rows[0]["trxCode"].ToString() == "Y")
                {
                    dt = ds.Tables["TariffPR"];

                    //MNGT_NO - 관리번호
                    if (dt.Rows[0]["MNGT_NO"] != null)
                    {
                        ViewBag.MNGT_NO = dt.Rows[0]["MNGT_NO"];
                    }

                    //SEQ - 시퀀스 번호
                    if (dt.Rows[0]["SEQ"] != null)
                    {
                        ViewBag.SEQ = dt.Rows[0]["SEQ"];
                    }

                    //PERIOD_YEAR - 기간
                    if (dt.Rows[0]["PERIOD_YEAR"] != null)
                    {
                        ViewBag.PERIOD_YEAR = dt.Rows[0]["PERIOD_YEAR"];
                    }

                    //PERIOD_QUARTER - 분기
                    if (dt.Rows[0]["PERIOD_QUARTER"] != null)
                    {
                        ViewBag.PERIOD_QUARTER = dt.Rows[0]["PERIOD_QUARTER"];
                    }

                    //P_RATE_NAME - 할증명칭
                    if (dt.Rows[0]["P_RATE_NAME"] != null)
                    {
                        ViewBag.P_RATE_NAME = dt.Rows[0]["P_RATE_NAME"];
                    }

                    //P_RATE_PRICE - 계산식(%)
                    if (dt.Rows[0]["P_RATE_PRICE"] != null)
                    {
                        ViewBag.P_RATE_PRICE = dt.Rows[0]["P_RATE_PRICE"];
                    }

                    //P_RATE_WON - 계산식(원)
                    if (dt.Rows[0]["P_RATE_WON"] != null)
                    {
                        ViewBag.P_RATE_WON = dt.Rows[0]["P_RATE_WON"];
                    }

                    //EXCEPTION - 예외처리
                    if (dt.Rows[0]["EXCEPTION"] != null)
                    {
                        ViewBag.EXCEPTION = dt.Rows[0]["EXCEPTION"];
                    }
                }
                return View();
            }
        }
        public ActionResult VehicleAdmin()
        {
            return View();
        }
        public ActionResult VehicleWrite(string MngtNo, string SEQ)
        {
            //신규, 수정
            if (MngtNo == null)
            {
                return View();
            }
            else
            {
                //Search 한번 하고 고고
                strResult = CA.Con_SearchVehicleModify(MngtNo, SEQ);
                strResult = String_Encrypt.decryptAES256(strResult);
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(strResult);

                DataTable dt_result = ds.Tables["Result"];

                if (dt_result.Rows[0]["trxCode"].ToString() == "Y")
                {
                    dt = ds.Tables["Vehicle"];

                    //MNGT_NO - 관리번호
                    if (dt.Rows[0]["MNGT_NO"] != null)
                    {
                        ViewBag.MNGT_NO = dt.Rows[0]["MNGT_NO"];
                    }

                    //SEQ - 시퀀스 번호
                    if (dt.Rows[0]["SEQ"] != null)
                    {
                        ViewBag.SEQ = dt.Rows[0]["SEQ"];
                    }

                    //구분 - CAR_DIV_CODE
                    if (dt.Rows[0]["CAR_DIV_CODE"] != null)
                    {
                        ViewBag.CAR_DIV_CODE = dt.Rows[0]["CAR_DIV_CODE"];
                    }

                    //명칭 - CAR_NAME
                    if (dt.Rows[0]["CAR_NAME"] != null)
                    {
                        ViewBag.CAR_NAME = dt.Rows[0]["CAR_NAME"];
                    }

                    //약칭 - SHORTHAND
                    if (dt.Rows[0]["SHORTHAND"] != null)
                    {
                        ViewBag.SHORTHAND = dt.Rows[0]["SHORTHAND"];
                    }

                    //적재함 길이 - CAR_WIDTH
                    if (dt.Rows[0]["CAR_WIDTH"] != null)
                    {
                        ViewBag.CAR_WIDTH = dt.Rows[0]["CAR_WIDTH"];
                    }

                    //탑높이 - TOP_HEIGHT
                    if (dt.Rows[0]["TOP_HEIGHT"] != null)
                    {
                        ViewBag.TOP_HEIGHT = dt.Rows[0]["TOP_HEIGHT"];
                    }

                    //바닥높이 - BOTTOM_HEIGHT
                    if (dt.Rows[0]["BOTTOM_HEIGHT"] != null)
                    {
                        ViewBag.BOTTOM_HEIGHT = dt.Rows[0]["BOTTOM_HEIGHT"];
                    }

                    //적재함 넓이 - CAR_AREA
                    if (dt.Rows[0]["CAR_AREA"] != null)
                    {
                        ViewBag.CAR_AREA = dt.Rows[0]["CAR_AREA"];
                    }

                    //적재 중량 - CAR_WEIGHT
                    if (dt.Rows[0]["CAR_WEIGHT"] != null)
                    {
                        ViewBag.CAR_WEIGHT = dt.Rows[0]["CAR_WEIGHT"];
                    }

                    //적재부피 - CAR_CBM
                    if (dt.Rows[0]["CAR_CBM"] != null)
                    {
                        ViewBag.CAR_CBM = dt.Rows[0]["CAR_CBM"];
                    }

                    //차량 총 높이 - TOTAL_HEIGHT
                    if (dt.Rows[0]["TOTAL_HEIGHT"] != null)
                    {
                        ViewBag.TOTAL_HEIGHT = dt.Rows[0]["TOTAL_HEIGHT"];
                    }

                    //특이사항 - RMK
                    if (dt.Rows[0]["RMK"] != null)
                    {
                        ViewBag.RMK = dt.Rows[0]["RMK"];
                    }

                    //이미지 이름 IMG_NAME
                    if (dt.Rows[0]["IMG_NAME"] != null)
                    {
                        ViewBag.IMG_NAME = dt.Rows[0]["IMG_NAME"];
                    }

                    //이미지 경로 - IMG_PATH
                    if (dt.Rows[0]["IMG_PATH"] != null)
                    {
                        ViewBag.IMG_PATH = dt.Rows[0]["IMG_PATH"];
                    }

                    //실제 이미지 파일 이름 - REPLACE_IMG_NAME
                    if (dt.Rows[0]["REPLACE_IMG_NAME"] != null)
                    {
                        ViewBag.REPLACE_IMG_NAME = dt.Rows[0]["REPLACE_IMG_NAME"];
                    }
                }
                return View();
            }
        }




        public class noticeParam
        {
            public string Option { get; set; }
            public string Type { get; set; }
            public string SearchText { get; set; }
            public int Page { get; set; }
        }

        public string Notice_CallAjax(noticeParam rtnVal)
        {
            string rtnJson = "";
            try
            {
                if (rtnVal != null)
                {
                    string strOpt = rtnVal.Option;
                    //string strType = rtnVal.Type;
                    string strText = rtnVal.SearchText;
                    int pageIndex = rtnVal.Page;

                    dt = CA.Con_NoticeList(rtnVal);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0) rtnJson = JsonConvert.SerializeObject(dt);
                    }
                }
                return rtnJson;
            }
            catch (Exception e)
            {
                return rtnJson;
            }
        }

        public static string GetRandomChar(int _totLen)
        {
            Random rand = new Random();
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, _totLen).Select(x => input[rand.Next(0, input.Length)]);
            return new string(chars.ToArray());
        }

        //입력-수정 모두 처리        
        //[AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        [HttpPost, ValidateInput(false)]
        public ActionResult NoticeModify()
        {
            try
            {
                Hashtable htParam = new Hashtable();
                if (Request.Form.Count > 0)
                {
                    if (Request.Form.AllKeys.Contains("notice_id")) htParam.Add("NOTICE_ID", Request.Form["notice_id"]);
                    if (Request.Form.AllKeys.Contains("user_id")) htParam.Add("USR_ID", Request.Form["user_id"]);
                    if (Request.Form.AllKeys.Contains("title")) htParam.Add("TITLE", Request.Form["title"]);
                    //if (Request.Form.AllKeys.Contains("s_type")) htParam.Add("S_TYPE", Request.Form["s_type"]);
                    if (Request.Form.AllKeys.Contains("notice_yn")) htParam.Add("NOTICE_YN", Request.Form["notice_yn"]);
                    if (Request.Form.AllKeys.Contains("content")) htParam.Add("CONTENT", Request.Form["content"]);
                    if (Request.Form.AllKeys.Contains("use_yn")) htParam.Add("USE_YN", Request.Form["use_yn"]);
                    if (Request.Form.AllKeys.Contains("file_del")) htParam.Add("FILE_DEL", Request.Form["file_del"]);
                    if (Request.Form.AllKeys.Contains("file1_del")) htParam.Add("FILE1_DEL", Request.Form["file1_del"]);
                    if (Request.Form.AllKeys.Contains("file2_del")) htParam.Add("FILE2_DEL", Request.Form["file2_del"]);

                    htParam.Add("FILE", "");
                    htParam.Add("FILE_NAME", "");
                    htParam.Add("FILE1", "");
                    htParam.Add("FILE1_NAME", "");
                    htParam.Add("FILE2", "");
                    htParam.Add("FILE2_NAME", "");

                    if (htParam.ContainsKey("NOTICE_ID"))
                    {
                        if (!string.IsNullOrEmpty(htParam["NOTICE_ID"].ToString())) //notice id가 있다! => update
                        {
                            #region //파일 삭제 로직
                            if (htParam.ContainsKey("FILE_DEL")) //파일삭제가 체크 되어있고
                            {
                                if (htParam["FILE_DEL"].ToString() == "y")  //삭제값이 y 이면
                                {
                                    NoticeFileDel(htParam["NOTICE_ID"].ToString(), 1);
                                    htParam["FILE"] = "";
                                    htParam["FILE_NAME"] = "";
                                }
                            }
                            else
                            {
                                htParam.Remove("FILE");
                            }

                            if (htParam.ContainsKey("FILE1_DEL")) //파일삭제가 체크 되어있고
                            {
                                if (htParam["FILE1_DEL"].ToString() == "y")  //삭제값이 y 이면
                                {
                                    NoticeFileDel(htParam["NOTICE_ID"].ToString(), 2);
                                    htParam["FILE1"] = "";
                                    htParam["FILE1_NAME"] = "";
                                }
                            }
                            else
                            {
                                htParam.Remove("FILE1");
                            }

                            if (htParam.ContainsKey("FILE2_DEL")) //파일삭제가 체크 되어있고
                            {
                                if (htParam["FILE2_DEL"].ToString() == "y")  //삭제값이 y 이면
                                {
                                    NoticeFileDel(htParam["NOTICE_ID"].ToString(), 3);
                                    htParam["FILE2"] = "";
                                    htParam["FILE2_NAME"] = "";
                                }
                            }
                            else
                            {
                                htParam.Remove("FILE2");
                            }
                            #endregion
                        }
                    }

                    //파일객체가 있다면
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var filename = "";
                        var file1 = Request.Files[1];
                        var file1name = "";
                        var file2 = Request.Files[2];
                        var file2name = "";

                        if (file != null && file.ContentLength > 0)
                        {
                            filename = Path.GetFileName(file.FileName);
                            string file_name = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "_" + GetRandomChar(20) + Path.GetExtension(file.FileName);

                            NoticeFileDel(htParam["NOTICE_ID"].ToString(), 1);

                            var path = Path.Combine(Server.MapPath(_NoticeFilePath), file_name);
                            file.SaveAs(path);

                            htParam["FILE"] = file_name;
                            htParam["FILE_NAME"] = filename;
                        }

                        if (file1 != null && file1.ContentLength > 0)
                        {
                            file1name = Path.GetFileName(file1.FileName);
                            string file1_name = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "_" + GetRandomChar(20) + Path.GetExtension(file1.FileName);

                            NoticeFileDel(htParam["NOTICE_ID"].ToString(), 2);

                            var path = Path.Combine(Server.MapPath(_NoticeFilePath), file1_name);
                            file1.SaveAs(path);

                            htParam["FILE1"] = file1_name;
                            htParam["FILE1_NAME"] = file1name;
                        }

                        if (file2 != null && file2.ContentLength > 0)
                        {
                            file2name = Path.GetFileName(file2.FileName);
                            string file2_name = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "_" + GetRandomChar(20) + Path.GetExtension(file2.FileName);

                            NoticeFileDel(htParam["NOTICE_ID"].ToString(), 3);

                            var path = Path.Combine(Server.MapPath(_NoticeFilePath), file2_name);
                            file2.SaveAs(path);

                            htParam["FILE2"] = file2_name;
                            htParam["FILE2_NAME"] = file2name;
                        }
                    }

                    string strResult = "";

                    if (htParam.ContainsKey("NOTICE_ID"))
                    {
                        if (!string.IsNullOrEmpty(htParam["NOTICE_ID"].ToString())) //notice id가 있다! => update
                        {
                            strResult = CA.Con_NoticeUpdate(htParam);
                        }
                        else
                        {
                            //Insert
                            strResult = CA.Con_NoticeInsert(htParam);
                        }
                    }
                    else
                    {
                        //Insert
                        strResult = CA.Con_NoticeInsert(htParam);
                    }

                    strResult = String_Encrypt.decryptAES256(strResult);

                    //return Json(strResult);                    
                    return RedirectToAction("Notice");
                }
                //return Content("<script>저장 할 수 없습니다.</script>");
                return RedirectToAction("Notice");
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return RedirectToAction("Notice");
                //return Json(strJson);
            }
        }

        //데이터 삭제
        public void NoticeFileDel(string Notice_ID, int FileIndex)
        {
            try
            {
                if (!string.IsNullOrEmpty(Notice_ID))
                {
                    dt = CA.Con_NoticeView(Notice_ID);

                    string strFile1 = "";
                    string strFile2 = "";
                    string strFile3 = "";
                    if (dt.Rows.Count > 0)
                    {
                        #region //File Delete

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            strFile1 = dt.Rows[i]["FILE"].ToString();
                            strFile2 = dt.Rows[i]["FILE1"].ToString();
                            strFile3 = dt.Rows[i]["FILE2"].ToString();

                            string FullFilePath1 = Server.MapPath(_NoticeFilePath) + strFile1;
                            string FullFilePath2 = Server.MapPath(_NoticeFilePath) + strFile2;
                            string FullFilePath3 = Server.MapPath(_NoticeFilePath) + strFile3;

                            switch (FileIndex)
                            {
                                case 0:
                                    if (System.IO.File.Exists(FullFilePath1))    //파일이 존재한다면
                                    {
                                        System.IO.FileInfo file = new System.IO.FileInfo(FullFilePath1);
                                        file.Delete();
                                    }
                                    if (System.IO.File.Exists(FullFilePath2))    //파일이 존재한다면
                                    {
                                        System.IO.FileInfo file = new System.IO.FileInfo(FullFilePath1);
                                        file.Delete();
                                    }
                                    if (System.IO.File.Exists(FullFilePath3))    //파일이 존재한다면
                                    {
                                        System.IO.FileInfo file = new System.IO.FileInfo(FullFilePath1);
                                        file.Delete();
                                    }
                                    break;
                                case 1:
                                    if (System.IO.File.Exists(FullFilePath1))    //파일이 존재한다면
                                    {
                                        System.IO.FileInfo file = new System.IO.FileInfo(FullFilePath1);
                                        file.Delete();
                                    }
                                    break;
                                case 2:
                                    if (System.IO.File.Exists(FullFilePath2))    //파일이 존재한다면
                                    {
                                        System.IO.FileInfo file = new System.IO.FileInfo(FullFilePath1);
                                        file.Delete();
                                    }
                                    break;
                                case 3:
                                    if (System.IO.File.Exists(FullFilePath3))    //파일이 존재한다면
                                    {
                                        System.IO.FileInfo file = new System.IO.FileInfo(FullFilePath1);
                                        file.Delete();
                                    }
                                    break;
                            }
                        }
                        #endregion

                        //string strJson = "";
                        //strJson = CA.Con_AdminNoticeDel(Notice_ID);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }


        /// <summary>
        /// 데이터 삭제
        /// </summary>
        /// <param name="Notice_ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Notice_DelAjax(string Notice_ID)
        {
            string strJson = "";

            try
            {
                if (Notice_ID != null)
                {
                    strJson = CA.Con_AdminNoticeDel(Notice_ID);
                }
                var result = new { Success = "True", Message = "Complete" };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                var result = new { Success = "False", Message = e.ToString() };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult NoticeEditor()
        {
            //_EditorFilePath
            string ckEditor = System.Web.HttpContext.Current.Request["CKEditor"];
            string funcNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];
            string langCode = System.Web.HttpContext.Current.Request["langCode"];

            try
            {
                int total = System.Web.HttpContext.Current.Request.Files.Count;
                if (total == 0) return Content(string.Format(scriptTag, funcNum, "", "no File"), "text/html");

                HttpPostedFile theFile = System.Web.HttpContext.Current.Request.Files[0];
                string strFilename = theFile.FileName;
                string sFileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + GetRandomChar(20) + Path.GetExtension(theFile.FileName);//Path.GetFileName(strFilename);
                string name = Path.Combine(Server.MapPath(_EditorFilePath), sFileName);
                theFile.SaveAs(name);
                string url = _EditorFilePath + sFileName.Replace("'", "\'");

                return Content(
                    string.Format(scriptTag, funcNum, HttpUtility.JavaScriptStringEncode(url ?? ""), ""),
                    "text/html"
                    );
            }
            catch (Exception ex)
            {
                return Content(
                    string.Format(scriptTag, funcNum, "", ex.ToString()),
                    "text/html"
                    );
            }

        }

        public class JsonData
        {
            public string vJsonData { get; set; }
        }


        /************************************************관리자 페이지****************************************************/


        public class LoginCls
        {
            public string m_id { get; set; }
            public string pwd { get; set; }
        }

        public ActionResult adminLogin(LoginCls loginObj)
        {
            bool loginCheck = false;
            string strMessage = "";

            try
            {
                if (loginObj != null)
                {

                    strJson = CA.Con_adminLogin(loginObj.m_id, loginObj.pwd, memberKey);

                    strJson = String_Encrypt.decryptAES256(strJson);

                    DataSet resultDs = JsonConvert.DeserializeObject<DataSet>(strJson);

                    if (resultDs.Tables["Result"].Rows[0]["trxCode"].ToString() == "Y")
                    {
                        loginCheck = true;
                        strMessage = "로그인 성공";

                        #region // 로그인 성공시 정보를 Session에 저장하자
                        Session["admin_idx"] = loginObj.m_id;
                        #endregion
                    }
                    else if (resultDs.Tables["Result"].Rows[0]["trxCode"].ToString() == "N")
                    {
                        loginCheck = false;
                        strMessage = "로그인 정보가 없습니다.";
                        return Json(new { Success = loginCheck, Message = strMessage });
                    }
                    else if (resultDs.Tables["Result"].Rows[0]["trxCode"].ToString() == "E")
                    {
                        loginCheck = false;
                        strMessage = "에러가 발생하였습니다. 담당자에게 문의 하세요";
                        return Json(new { Success = loginCheck, Message = strMessage });
                    }
                    else
                    {
                        loginCheck = false;
                        strMessage = "없는 아이디거나 패스워드가 틀렸습니다. 다시 시도해 주세요";
                    }
                }
                else
                {
                    loginCheck = false;
                    strMessage = "로그인 정보가 없습니다.";
                }

                return Json(new { Success = loginCheck, Message = strMessage });

            }
            catch
            {
                loginCheck = false;
                strMessage = "로그인 실패했습니다. 관리자에게 문의 부탁드립니다.";
                return Json(new { Success = loginCheck, Message = strMessage });

            }
        }

        /// <summary>
        /// 로그아웃 버튼
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        /*****************************************************************************************************************/

        /// <summary>
        /// 안전운임 엑셀 데이터 저장 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSaveExcel_SaveFre()
        {

            try
            {
                HttpFileCollectionBase files = Request.Files;

                string ChkFileNM = Path.GetFileName(files[0].FileName);
                string strPERIOD_YEAR = Request.Form["PERIOD_YEAR"];
                string strPERIOD_QUARTER = Request.Form["PERIOD_QUARTER"];
                string strUSR_ID = Request.Form["USR_ID"];

                string strMNGT_NO = "";
                string strFILE_NM = "";
                string strREPLACE_FILE_NM = "";
                string strFILE_PATH = "";
                string strFILE_SIZE = "";
                string strFileExtension = "";

                string strFilePath = "";

                //엑셀 파일 데이터 저장
                if (ChkFileNM != "")
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        string InputFileName = "";
                        string strNowTime = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

                        HttpPostedFileBase file = files[i];

                        if (file.FileName != "" && file.ContentLength != 0)
                        {
                            if (file != null)
                            {
                                string strFileName = "";

                                //파일 데이터 저장
                                strMNGT_NO = "S" + "_" + strNowTime;
                                strFILE_NM = Path.GetFileName(file.FileName);
                                strREPLACE_FILE_NM = strUSR_ID + "_" + strNowTime;
                                strFILE_PATH = Path.Combine(Server.MapPath("~/Files/Tariff_Excel/"));
                                strFILE_SIZE = file.ContentLength.ToString();
                                strFileExtension = Path.GetExtension(file.FileName); //확장자 저장

                                InputFileName = Path.GetFileName(file.FileName); //파일 이름                            

                                string[] fileinfo = InputFileName.Split('.');
                                for (int j = 0; j < fileinfo.Length - 1; j++)
                                {
                                    //.이 파일명 사이에 있을 경우						
                                    if (j == fileinfo.Length - 2)
                                    {
                                        strFileName += fileinfo[j];
                                    }
                                    else
                                    {
                                        strFileName += fileinfo[j] + ".";
                                    }
                                }

                                InputFileName = strUSR_ID + "_" + strNowTime; //입력자 _ 시분초밀리초

                                //파일 사이즈						
                                strFilePath = Path.Combine(Server.MapPath("~/Files/Tariff_Excel/") + InputFileName); //날짜 붙혀서 보내기

                                //Save file to server folder  
                                file.SaveAs(strFilePath);
                            }
                        }

                    }
                }

                //데이터 테이블 만들어서 한번에 넣고 insert 시켜주자
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add("MNGT_NO");
                dt.Columns.Add("PERIOD_YEAR");
                dt.Columns.Add("PERIOD_QUARTER");
                dt.Columns.Add("PORT");
                dt.Columns.Add("SECTION");
                dt.Columns.Add("ADDR_STATE");
                dt.Columns.Add("ADDR_CITY");
                dt.Columns.Add("ADDR_TOWNSHIP");
                dt.Columns.Add("ADDR_TOWNSHIP2");
                dt.Columns.Add("DISTANCE");
                dt.Columns.Add("TF_40FT");
                dt.Columns.Add("IF_40FT");
                dt.Columns.Add("CF_40FT");
                dt.Columns.Add("TF_20FT");
                dt.Columns.Add("IF_20FT");
                dt.Columns.Add("CF_20FT");
                dt.Columns.Add("USR_ID");

                //엑셀 파일 읽기
                FileInfo fi = new FileInfo(strFilePath);

                using (var package = new ExcelPackage(fi))
                {
                    //var workbook = package.Workbook;
                    //var worksheet = workbook.Worksheets.First();                

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count

                    for (int row = 3; row <= rowCount; row++)
                    {
                        string[] strCellvalue = new string[13];

                        for (int col = 1; col <= colCount; col++)
                        {
                            System.Diagnostics.Debug.WriteLine(" Row:" + row + " column:" + col + " Value:" + worksheet.Cells[row, col].Value?.ToString().Trim());
                            strCellvalue[col - 1] = worksheet.Cells[row, col].Value?.ToString().Trim();
                        }

                        //dr에 넣은거 dt에 넣기
                        dr = dt.NewRow();

                        dr["MNGT_NO"] = strMNGT_NO;
                        dr["PERIOD_YEAR"] = strPERIOD_YEAR;
                        dr["PERIOD_QUARTER"] = strPERIOD_QUARTER;
                        dr["PORT"] = strCellvalue[0];
                        dr["SECTION"] = strCellvalue[1];
                        dr["ADDR_STATE"] = strCellvalue[2];
                        dr["ADDR_CITY"] = strCellvalue[3];
                        dr["ADDR_TOWNSHIP"] = strCellvalue[4];
                        dr["ADDR_TOWNSHIP2"] = strCellvalue[5];
                        dr["DISTANCE"] = strCellvalue[6];
                        dr["CF_40FT"] = strCellvalue[7];
                        dr["IF_40FT"] = strCellvalue[8];
                        dr["TF_40FT"] = strCellvalue[9];
                        dr["CF_20FT"] = strCellvalue[10];
                        dr["IF_20FT"] = strCellvalue[11];
                        dr["TF_20FT"] = strCellvalue[12];
                        dr["USR_ID"] = strUSR_ID;

                        dt.Rows.Add(dr);
                    }
                }

                //주석 풀어야 됨.
                strResult = CA.Con_SaveExcel_SaveFre(dt);
                strJson = String_Encrypt.decryptAES256(strResult);

                dt = new DataTable();

                dt.Columns.Add("MNGT_NO");
                dt.Columns.Add("DOC_TYPE");
                dt.Columns.Add("FILE_NM");
                dt.Columns.Add("REPLACE_FILE_NM");
                dt.Columns.Add("FILE_PATH");
                dt.Columns.Add("FILE_SIZE");
                dt.Columns.Add("INS_USR");
                dt.Columns.Add("EXTENSION");

                //dr에 넣은거 dt에 넣기
                dr = dt.NewRow();

                dr["MNGT_NO"] = strMNGT_NO;
                dr["DOC_TYPE"] = "S";
                dr["FILE_NM"] = strFILE_NM;
                dr["REPLACE_FILE_NM"] = strREPLACE_FILE_NM;
                dr["FILE_PATH"] = strFILE_PATH;
                dr["FILE_SIZE"] = strFILE_SIZE;
                dr["INS_USR"] = strUSR_ID;
                dr["EXTENSION"] = strFileExtension;

                dt.Rows.Add(dr);

                CA.Con_InsertFileLog(dt);

                return Json(strJson);
            }
            catch (Exception ex)
            {
                strJson = Comm.MakeJson("E", ex.Message);
                strJson = String_Encrypt.decryptAES256(strJson);
                return Json(strJson);
            }
        }

        /// <summary>
        /// 부대비용 엑셀 데이터 저장 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSaveExcel_SaveSur()
        {
            HttpFileCollectionBase files = Request.Files;

            string ChkFileNM = Path.GetFileName(files[0].FileName);
            string strUSR_ID = Request.Form["USR_ID"];

            string strMNGT_NO = "";
            string strFILE_NM = "";
            string strREPLACE_FILE_NM = "";
            string strFILE_PATH = "";
            string strFILE_SIZE = "";
            string strFileExtension = "";

            string strFilePath = "";

            //엑셀 파일 데이터 저장
            if (ChkFileNM != "")
            {
                for (int i = 0; i < files.Count; i++)
                {
                    string InputFileName = "";
                    string strNowTime = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

                    HttpPostedFileBase file = files[i];

                    if (file.FileName != "" && file.ContentLength != 0)
                    {
                        if (file != null)
                        {
                            string strFileName = "";

                            //파일 데이터 저장
                            strMNGT_NO = "C" + "_" + strNowTime;
                            strFILE_NM = Path.GetFileName(file.FileName);
                            strREPLACE_FILE_NM = strUSR_ID + "_" + strNowTime;
                            strFILE_PATH = Path.Combine(Server.MapPath("~/Files/Surcharge_Excel/"));
                            strFILE_SIZE = file.ContentLength.ToString();
                            strFileExtension = Path.GetExtension(file.FileName); //확장자 저장

                            InputFileName = Path.GetFileName(file.FileName); //파일 이름

                            string[] fileinfo = InputFileName.Split('.');
                            for (int j = 0; j < fileinfo.Length - 1; j++)
                            {
                                //.이 파일명 사이에 있을 경우						
                                if (j == fileinfo.Length - 2)
                                {
                                    strFileName += fileinfo[j];
                                }
                                else
                                {
                                    strFileName += fileinfo[j] + ".";
                                }
                            }

                            InputFileName = strUSR_ID + "_" + strNowTime; //입력자 _ 시분초밀리초

                            //파일 사이즈						
                            strFilePath = Path.Combine(Server.MapPath("~/Files/Surcharge_Excel/") + InputFileName); //날짜 붙혀서 보내기

                            //Save file to server folder  
                            file.SaveAs(strFilePath);
                        }
                    }

                }
            }

            //데이터 테이블 만들어서 한번에 넣고 insert 시켜주자
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("MNGT_NO");
            dt.Columns.Add("SHIPPING");
            dt.Columns.Add("BOUND");
            dt.Columns.Add("PORT");
            dt.Columns.Add("CNTR_TYPE");
            dt.Columns.Add("CNTR_SIZE");
            dt.Columns.Add("COUNTRY_OPTION");
            dt.Columns.Add("CODE");
            dt.Columns.Add("UNIT");
            dt.Columns.Add("CURRENCY");
            dt.Columns.Add("PRICE");
            dt.Columns.Add("INS_USR");

            //엑셀 파일 읽기
            FileInfo fi = new FileInfo(strFilePath);

            using (var package = new ExcelPackage(fi))
            {
                //var workbook = package.Workbook;
                //var worksheet = workbook.Worksheets.First();                

                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count

                for (int row = 2; row <= rowCount; row++)
                {
                    string[] strCellvalue = new string[11];

                    for (int col = 1; col <= colCount; col++)
                    {
                        System.Diagnostics.Debug.WriteLine(" Row:" + row + " column:" + col + " Value:" + worksheet.Cells[row, col].Value?.ToString().Trim());
                        strCellvalue[col - 1] = worksheet.Cells[row, col].Value?.ToString().Trim();
                    }

                    //dr에 넣은거 dt에 넣기
                    dr = dt.NewRow();

                    dr["MNGT_NO"] = strMNGT_NO;
                    dr["SHIPPING"] = strCellvalue[0];
                    dr["BOUND"] = strCellvalue[1];
                    dr["PORT"] = strCellvalue[2];
                    dr["CNTR_TYPE"] = strCellvalue[3];
                    dr["CNTR_SIZE"] = strCellvalue[4];
                    dr["COUNTRY_OPTION"] = strCellvalue[5];
                    dr["CODE"] = strCellvalue[6];
                    dr["UNIT"] = strCellvalue[7];
                    dr["CURRENCY"] = strCellvalue[8];
                    dr["PRICE"] = strCellvalue[9];
                    dr["INS_USR"] = strUSR_ID;

                    dt.Rows.Add(dr);
                }
            }

            //dt 테이블을 보내서 데이터 저장.
            strResult = CA.Con_SaveExcel_SaveSur(dt);
            strJson = String_Encrypt.decryptAES256(strResult);

            dt = new DataTable();

            dt.Columns.Add("MNGT_NO");
            dt.Columns.Add("DOC_TYPE");
            dt.Columns.Add("FILE_NM");
            dt.Columns.Add("REPLACE_FILE_NM");
            dt.Columns.Add("FILE_PATH");
            dt.Columns.Add("FILE_SIZE");
            dt.Columns.Add("INS_USR");
            dt.Columns.Add("EXTENSION");

            //dr에 넣은거 dt에 넣기
            dr = dt.NewRow();

            dr["MNGT_NO"] = strMNGT_NO;
            dr["DOC_TYPE"] = "C";
            dr["FILE_NM"] = strFILE_NM;
            dr["REPLACE_FILE_NM"] = strREPLACE_FILE_NM;
            dr["FILE_PATH"] = strFILE_PATH;
            dr["FILE_SIZE"] = strFILE_SIZE;
            dr["INS_USR"] = strUSR_ID;
            dr["EXTENSION"] = strFileExtension;

            dt.Rows.Add(dr);

            CA.Con_InsertFileLog(dt);

            return Json(strJson);
        }

        //부대비용 컨트롤러 숨김
        #region
        ///////////////////////////////////////////////////부대비용 관리자 페이지////////////////////////////////////////////////////
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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SetSurchargePort(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SetSurchargeCountry(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SearchSurcharge(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 부대비용 - 신규 Insert
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnInsertSurcharge(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_InsertSurcharge(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 부대비용 - 수정 및 삭제 검색
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetModifySearch(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SetModifySearch(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 부대비용 - 데이터 수정
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSurchargeModify(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SurchargeModify(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 부대비용 - 데이터 삭제
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSurchargeDelete(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SurchargeDelete(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 엑셀 데이터 체크 (기존에 데이터가 있는지 체크)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnCheckSurchargeExcel(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_CheckSurchargeExcel(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 기존에 데이터가 있다면 부대비용 삭제하는 로직
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnDeleteExcelSurcharge(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_DeleteExcelSurcharge(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion //부대비용 컨트롤러

        //차량 제원 컨트롤러 숨김
        #region
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
                strResult = CA.Con_SetVehicleDiv();

                strJson = String_Encrypt.decryptAES256(strResult);

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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SearchVehicle(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        ////// <summary>
        /// 차량 제원 - 이미지 다운로드 데이터
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnVehicleImgDown(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_VehicleImgDown(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        ////// <summary>
        /// 차량 제원 - 이미지 다운로드 데이터
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnDeleteVehicle(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_DeleteVehicle(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
		/// 파일 업로드
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		[HttpPost]
        public ActionResult fnInsertVehicle()
        {
            DataRow dr;

            try
            {
                string strMNGT_NO = Request.Form["MNGT_NO"];
                string strSEQ = Request.Form["SEQ"];
                string strUSR_ID = Request.Form["USR_ID"];
                string strCAR_DIV = Request.Form["CAR_DIV"];
                string strCAR_DIV_CODE = Request.Form["CAR_DIV_CODE"];
                string strCAR_NAME = Request.Form["CAR_NAME"];
                string strSHORTHAND = Request.Form["SHORTHAND"];
                string strCAR_WIDTH = Request.Form["CAR_WIDTH"];
                string strTOP_HEIGHT = Request.Form["TOP_HEIGHT"];
                string strBOTTOM_HEIGHT = Request.Form["BOTTOM_HEIGHT"];
                string strCAR_AREA = Request.Form["CAR_AREA"];
                string strCAR_WEIGHT = Request.Form["CAR_WEIGHT"];
                string strCAR_CBM = Request.Form["CAR_CBM"];
                string strTOTAL_HEIGHT = Request.Form["TOTAL_HEIGHT"];
                string strRMK = Request.Form["RMK"];
                string strCRUD = Request.Form["DB_CRUD"];
                string strIS_FILE = Request.Form["IS_FILE"];

                HttpFileCollectionBase files = Request.Files;

                string strIMG_NAME = "";
                string strIMG_PATH = "";
                string strDBIMG_PATH = "";
                string strREPLACE_IMG_NAME = "";

                if (strMNGT_NO == "")
                {
                    strMNGT_NO = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                }

                //파일 저장
                if (files.Count > 0)
                {
                    /////////////////////////////////////////////////////////////////////////////폴더 생성/////////////////////////////////////////////////////////////////////////////				
                    //strIMG_PATH = Server.MapPath("~/Files/Vehicle") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                    strIMG_PATH = Server.MapPath("~/Files/Vehicle") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                    strDBIMG_PATH = "/Files/Vehicle" + "\\" + DateTime.Now.ToString("yyyyMMdd");

                    //현재 날짜 파일 생성 - 날짜로 계산
                    DirectoryInfo di = new DirectoryInfo(strIMG_PATH); //폴더 관련 객체
                    if (di.Exists != true)
                    {
                        di.Create();
                    }

                    string strDateTimeDi = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //////////////////////////////////////////////////////////////////////////첨부파일 저장/////////////////////////////////////////////////////////////////////////////
                    System.IO.FileInfo fi;

                    for (int i = 0; i < files.Count; i++)
                    {
                        try
                        {
                            HttpPostedFileBase file = files[i];
                            //파일 채번 룰 , 관리번호_업로드아이디_파일명
                            strREPLACE_IMG_NAME = strMNGT_NO + "_" + strDateTimeDi + "_" + file.FileName;
                            strIMG_NAME = file.FileName;

                            fi = new System.IO.FileInfo(strIMG_PATH + "/" + strREPLACE_IMG_NAME);

                            //파일 이름이 벌써 있는 경우 채번을 다시 시도. (무한루프)
                            while (true)
                            {
                                if (fi.Exists)
                                {
                                    strMNGT_NO = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                                    strREPLACE_IMG_NAME = strMNGT_NO + "_" + strUSR_ID + "_" + file.FileName;
                                    fi = new System.IO.FileInfo(strIMG_PATH + "/" + strREPLACE_IMG_NAME);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            if (fi.Exists != true)
                            {
                                //파일만 저장
                                file.SaveAs(strIMG_PATH + "/" + strREPLACE_IMG_NAME);
                            }
                        }
                        catch (Exception ex)
                        {
                            fi = new FileInfo(strIMG_PATH + "/" + strREPLACE_IMG_NAME);

                            if (fi.Exists == true)
                            {
                                fi.Delete();
                            }

                            strJson = Comm.MakeJson("E", ex.Message);

                            return Json(strJson);
                        }
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }

                //파일 데이터 까지 해서 데이터 저장
                //데이터 테이블 만들기
                dt = new DataTable("VEHICLE");
                dt.Columns.Add("MNGT_NO");
                dt.Columns.Add("SEQ");
                dt.Columns.Add("CAR_DIV");
                dt.Columns.Add("CAR_DIV_CODE");
                dt.Columns.Add("CAR_NAME");
                dt.Columns.Add("SHORTHAND");
                dt.Columns.Add("CAR_WIDTH");
                dt.Columns.Add("TOP_HEIGHT");
                dt.Columns.Add("BOTTOM_HEIGHT");
                dt.Columns.Add("CAR_AREA");
                dt.Columns.Add("CAR_WEIGHT");
                dt.Columns.Add("CAR_CBM");
                dt.Columns.Add("TOTAL_HEIGHT");
                dt.Columns.Add("RMK");
                dt.Columns.Add("USR_ID");
                dt.Columns.Add("IMG_NAME");
                dt.Columns.Add("IMG_PATH");
                dt.Columns.Add("REPLACE_IMG_NAME");
                dt.Columns.Add("IS_FILE");

                dr = dt.NewRow();

                dr["MNGT_NO"] = strMNGT_NO;
                dr["SEQ"] = strSEQ;
                dr["CAR_DIV"] = strCAR_DIV;
                dr["CAR_DIV_CODE"] = strCAR_DIV_CODE;
                dr["CAR_NAME"] = strCAR_NAME;
                dr["SHORTHAND"] = strSHORTHAND;
                dr["CAR_WIDTH"] = strCAR_WIDTH;
                dr["TOP_HEIGHT"] = strTOP_HEIGHT;
                dr["BOTTOM_HEIGHT"] = strBOTTOM_HEIGHT;
                dr["CAR_AREA"] = strCAR_AREA;
                dr["CAR_WEIGHT"] = strCAR_WEIGHT;
                dr["CAR_CBM"] = strCAR_CBM;
                dr["TOTAL_HEIGHT"] = strTOTAL_HEIGHT;
                dr["RMK"] = strRMK;
                dr["USR_ID"] = strUSR_ID;

                if (files.Count > 0)
                {
                    dr["IMG_NAME"] = strIMG_NAME;
                    dr["IMG_PATH"] = strDBIMG_PATH;
                    dr["REPLACE_IMG_NAME"] = strREPLACE_IMG_NAME;
                }

                dr["IS_FILE"] = strIS_FILE;

                dt.Rows.Add(dr);


                //파일 데이터 넘기기~
                if (strCRUD == "INSERT")
                {
                    strResult = CA.Con_InsertVehicle(dt);
                }
                else if (strCRUD == "UPDATE")
                {
                    //파일을 어떻게 할까..

                    //실제 파일이 변경 된건지도 확인이 필요
                    strResult = CA.Con_UpdateVehicle(dt);
                }

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = Comm.MakeJson("E", e.Message);

                return Json(strJson);
            }
        }
        #endregion

        //관리자 관리 페이지 컨트롤러 숨김
        #region
        //// <summary>
        /// 관리자 관리 페이지 데이터 검색
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSearchMember(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SearchMember(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        //// <summary>
        /// 아이디 중복 체크
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnCheckIDMember(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_CheckIDMember(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        //// <summary>
        /// 관리자 관리 등록
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnInsertMember(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_InsertMember(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        //// <summary>
        /// 관리자 관리 삭제
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnDeleteMember(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_DeleteMember(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        //// <summary>
        /// 관리자 관리 삭제
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnModifyMember(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_ModifyMember(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }
        #endregion

        //안전운임제 관리
        #region
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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SetYearQuarter(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SetAddrState(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SetAddrCity(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 전체(읍,면,동) 행정동 데이터 가져오기
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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SetAddrTownship(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 전체(읍,면,동) 법정동 데이터 가져오기
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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SetAddrTownship2(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_fnSetSection(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 구분 변경 시 PRICE 변경 로직
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnGetCntrPrice(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_GetCntrPrice(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

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
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_TariffSerach(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 안전운임 데이터 업데이트
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnUpdateTariff(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_UpdateTariff(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 안전운임 데이터 삭제
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnDeleteTariff(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_DeleteTariff(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 엑셀 데이터 체크 (기존에 데이터가 있는지 체크)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnCheckTariffExcel(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_CheckTariffExcel(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 엑셀 업로드 전 기존 데이터 삭제 로직
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnDeleteExcelTariff(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_DeleteExcelTariff(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        #endregion

        //안전운임제 할증관리
        #region
        /// <summary>
        /// 분기 / 년 데이터 가져오기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSetPRYearQuarter(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SetPRYearQuarter(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 검색
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnSearchTariffPR(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_SearchTariffPR(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 INSERT
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnInsertTariffPR(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_InsertTariffPR(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 DELETE
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnDeleteTariffPR(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_DeleteTariffPR(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }

        /// <summary>
        /// 안전운임제 할증 관리 UPDATE
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnUpdateTariffPR(JsonData value)
        {
            try
            {
                string vJsonData = value.vJsonData.ToString();
                string vEncodeData = "";

                //암호화 걸기
                vEncodeData = String_Encrypt.encryptAES256(vJsonData);

                strResult = CA.Con_UpdateTariffPR(vEncodeData);

                strJson = String_Encrypt.decryptAES256(strResult);

                return Json(strJson);
            }
            catch (Exception e)
            {
                strJson = e.Message;
                return Json(strJson);
            }
        }


        #endregion
    }
}
