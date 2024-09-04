using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using YJIT.Data;
using HTLC_ELVISPRIME_COM.Models;

using PagedList;
using Newtonsoft.Json;

namespace HTLC_ELVISPRIME_COM.Controllers
{
    public class RecruitmentController : Controller
    {
        private List<RecruitmentModel> RecruitmentList = new List<RecruitmentModel>();

        public class pageParam
        {
            public string Option { get; set; }
            public string Type { get; set; }
            public string SearchText { get; set; }
            public int Page { get; set; }
        }
        //
        // GET: /Recruitment/

        public ActionResult Welfare_benefits()
        {
            return View();
        }

        public ActionResult Recruitment_procedure()
        {
            return View();
        }

        public ActionResult Job_vacancy()
        {
            return View();
        }

        //데이터 조회
        public string CallAjax(pageParam rtnVal)
        {
            string rtnJson = "";
            try
            {
                if (rtnVal != null)
                {
                    string strOpt = rtnVal.Option;
                    string strType = rtnVal.Type;
                    string strText = rtnVal.SearchText;
                    int pageIndex = rtnVal.Page;

                    ADO_Conn con = new ADO_Conn();
                    DBA dbConn = new DBA(con.ConnectionStr, DbConfiguration.Current.DatabaseType);
                    DataTable dt = dbConn.SqlGet(con.Search_Recruitment(strOpt, strType, strText, pageIndex));
                    if (dt.Rows.Count > 0) rtnJson = JsonConvert.SerializeObject(dt);
                }
                return rtnJson;
            }
            catch
            {
                return "";
            }
        }

        public ActionResult Job_vacancyView(string id)   //공지사항 상세보기
        {
            if (!string.IsNullOrEmpty(id))
            {
                ADO_Conn con = new ADO_Conn();
                DBA dbConn = new DBA(con.ConnectionStr, DbConfiguration.Current.DatabaseType);
                dbConn.SqlSet(con.Update_RecruitmentViewCnt(id));
                dbConn.Commit();
                DataTable dt = dbConn.SqlGet(con.Search_RecruitmentView(id));


                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RecruitmentList.Add(new RecruitmentModel
                        {
                            FLAG = dr["FLAG"].ToString(),
                            RECRUITMENT_ID = int.Parse(dr["RECRUITMENT_ID"].ToString()),
                            TITLE = dr["TITLE"].ToString(),
                            CNT = int.Parse(dr["CNT"].ToString()),
                            WRITER = dr["WRITER"].ToString(),
                            USE_YN = dr["USE_YN"].ToString(),
                            RECRUITMENT_YN = dr["RECRUITMENT_YN"].ToString(),
                            REGDT = dr["REGDT"].ToString(),
                            EDITDT = dr["EDITDT"].ToString(),
                            FILE = dr["FILE"].ToString(),
                            FILE_NAME = dr["FILE_NAME"].ToString(),
                            FILE1 = dr["FILE1"].ToString(),
                            FILE_NAME1 = dr["FILE1_NAME"].ToString(),
                            FILE2 = dr["FILE2"].ToString(),
                            FILE_NAME2 = dr["FILE2_NAME"].ToString(),
                            CONTENT = dr["CONTENT"].ToString()
                        });
                    }
                }
            }
            return View(RecruitmentList);
        }

    }
}
