using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Configuration;
using YJIT.Data;
using HTLC_ELVISPRIME_COM.Models;

using PagedList;
using Newtonsoft.Json;
using System.Text;
//using EASendMail;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net;
using AegisImplicitMail;

namespace HTLC_ELVISPRIME_COM.Controllers
{
    public class communityController : Controller
    {
        private List<NoticeModel> NoticeList = new List<NoticeModel>();
        public List<DataRow> dtList { get; set; }

        string strJson = "";

        public class pageParam
        {
            public string Option { get; set; }
            public string Type { get; set; }
            public string SearchText { get; set; }
            public int Page { get; set; }
        }
        //public PagedList<DataRow> pList { get; set; }

        //
        // GET: /community/

        public class JsonData
        {
            public string vJsonData { get; set; }
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
                    DataTable dt = dbConn.SqlGet(con.Search_Notice(strOpt, strType, strText, pageIndex));
                    if (dt.Rows.Count > 0) rtnJson = JsonConvert.SerializeObject(dt);
                }
                return rtnJson;
            }
            catch
            {
                return "";
            }
        }


        public ActionResult notice()   //공지사항
        {
            return View();
        }

        public ActionResult map()   //공지사항
        {
            return View();
        }

        public ActionResult noticeView(string id)   //공지사항 상세보기
        {
            if (!string.IsNullOrEmpty(id))
            {
                ADO_Conn con = new ADO_Conn();
                DBA dbConn = new DBA(con.ConnectionStr, DbConfiguration.Current.DatabaseType);
                dbConn.SqlSet(con.Update_ViewCnt(id));
                dbConn.Commit();
                DataTable dt = dbConn.SqlGet(con.Search_NoticeView(id));


                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        NoticeList.Add(new NoticeModel
                        {
                            FLAG = dr["FLAG"].ToString(),
                            NOTICE_ID = int.Parse(dr["NOTICE_ID"].ToString()),
                            TITLE = dr["TITLE"].ToString(),
                            CNT = int.Parse(dr["CNT"].ToString()),
                            WRITER = dr["WRITER"].ToString(),
                            USE_YN = dr["USE_YN"].ToString(),
                            NOTICE_YN = dr["NOTICE_YN"].ToString(),
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
            return View(NoticeList);
        }

        public ActionResult customerVoice()   //공지사항
        {
            return View();
        }


        public class mailParam
        {
            public string fst_EndAddr { get; set; }
            public string mail_title { get; set; }
            public string mail_Content { get; set; }
            //public string mailCon { get; set; }
        }

        [HttpPost]
        public ActionResult SendEmail(mailParam rtnVal)
        {
            //DataTable dt = new DataTable();
            //dt = JsonConvert.DeserializeObject<DataTable>(value.mailCon);

            try
            {
                //SmtpSection smtpConfig = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                /* 465 stmps 일떄 메일 전송 테스트 */
                //AIM - 메일 제목 (한글 깨짐) , 메일 내용 (스타일 X)
                #region                                
                //UTF-8
                //byte[] bytes = Encoding.Default.GetBytes(testSubject);
                //testSubject = Encoding.UTF8.GetString(bytes);

                //EUC-KR
                //byte[] bytes = Encoding.Default.GetBytes(testSubject);
                //testSubject = Encoding.GetEncoding(51949).GetString(bytes);

                //UTF-32 얘는 깨지네
                //byte[] bytes = Encoding.Default.GetBytes(testSubject);
                //testSubject = Encoding.UTF32.GetString(bytes);

                //인코딩 체크
                //var encodingName = mailMessage.SubjectEncoding.BodyName.ToLower();
                //string.Format("=?{0}?B?{1}?=", encodingName, Convert.ToBase64String(mailMessage.SubjectEncoding.GetBytes(mailMessage.Subject)));

                //mailMessage.Subject = Encoding.GetEncoding(51949).GetString(bytes);
                //mailMessage.Subject = Encoding.UTF8.GetString(bytes);                 //string으로 받아서 보내면 안꺠지지만 바로 쓰면 꺠짐
                //mailMessage.Subject = Encoding.GetEncoding(51949).GetString(bytes);   //SUbject는 꺠지지 않지만 전송할떄 꺠짐
                //mailMessage.Subject = Encoding.UTF32.GetString(bytes)                 //한글 자체가 깨짐

                //subject 인코딩
                //mailMessage.SubjectEncoding = System.Text.Encoding.GetEncoding(51949); //ecu-kr
                //mailMessage.SubjectEncoding = System.Text.Encoding.UTF8; //UTF-8
                //mailMessage.SubjectEncoding = System.Text.Encoding.Default; //Default                

                //mailMessage.Subject = "asdasdasd";
                //mailMessage.Body = "testtest";//MakeEmailForm(rtnVal);

                //인코딩 체크
                //encodingName = mailMessage.BodyEncoding.BodyName.ToLower();
                //string.Format("=?{0}?B?{1}?=", encodingName, Convert.ToBase64String(mailMessage.BodyEncoding.GetBytes(mailMessage.Subject)));

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var mailMessage = new MimeMailMessage();

                DateTime dateT = DateTime.Now;
                string datePart = dateT.ToString("yyyy-MM-dd hh:mm:ss");

                mailMessage.Subject = "[Heung-A Logistics] Customer's voice has been received. (" + datePart + ")";
                mailMessage.Body = MakeEmailForm(rtnVal);
                mailMessage.IsBodyHtml = true;

                var encodingName = mailMessage.BodyEncoding.BodyName.ToLower();
                string.Format("=?{0}?B?{1}?=", encodingName, Convert.ToBase64String(mailMessage.BodyEncoding.GetBytes(mailMessage.Subject)));

                //mailMessage.Sender = new MimeMailAddress("gyjung@halogistics.co.kr");
                //mailMessage.From = new MimeMailAddress("gyjung@halogistics.co.kr");
                mailMessage.From = new MimeMailAddress(rtnVal.fst_EndAddr.ToString());
                //mailMessage.To.Add(new MimeMailAddress("yhoh@halogistics.co.kr"));
                mailMessage.To.Add(new MimeMailAddress("willy@halogistics.co.kr")); //sihong 20220214 - 대표 이메일 수정
                //mailMessage.To.Add(new MimeMailAddress("jhpark@yjit.co.kr"));
                //mailMessage.To.Add(new MimeMailAddress("twkim@yjit.co.kr"));

                var emailer = new SmtpSocketClient();
                //emailer.Host = "m.halogistics.co.kr";
                emailer.Host = "mail.yjit.co.kr";
                emailer.Port = 465;
                emailer.SslType = SslMode.Ssl;
                //emailer.User = "websmtps@halogistics.co.kr";
                //emailer.Password = "Gmddkfhwltmxlrtm!12";
                emailer.User = "mailmaster@yjit.co.kr";
                emailer.Password = "Yjit0921)#$%";
                emailer.AuthenticationMode = AuthenticationType.Base64;
                emailer.MailMessage = mailMessage;
                emailer.SendMailAsync();
                #endregion

                //전송 X 465포트는 불가능 - system@yjit.co.kr로는 보낼 수 있음.
                //EASendMail
                #region
                //SmtpMail oMail = new SmtpMail("TryIt");

                //DateTime dateT = DateTime.Now;
                //string datePart = dateT.ToString("yyyy-MM-dd hh:mm:ss");

                //oMail.Subject = "[흥아로지스틱스]"+ datePart+"에 전송된 고객의 소리 입니다.";
                ////oMail.TextBody = MakeEmailForm(rtnVal); //텍스트로 보내는 body
                //oMail.HtmlBody = MakeEmailForm(rtnVal);   //HTML 코드로 보내는 body

                //oMail.From = rtnVal.fst_EndAddr;
                ////oMail.To = "yhoh@halogistics.co.kr"; //yhoh@halogistics.co.kr 흥아로지스틱스 받는사람                             
                //oMail.To = "twkim@yjit.co.kr";

                //// Your SMTP server address
                //SmtpServer oServer = new SmtpServer(smtpConfig.Network.Host);
                //// User and password for ESMTP authentication
                //oServer.User = smtpConfig.Network.UserName;
                //oServer.Password = smtpConfig.Network.Password;

                //// Set SSL 465 port
                //oServer.Port = 587;
                //// Set direct SSL connection, you can also use ConnectSSLAuto
                //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                ////oServer.AuthType = SmtpAuthType.AuthAuto;

                //SmtpClient oSmtp = new SmtpClient();
                //oSmtp.SendMail(oServer, oMail);
                #endregion

                //전송 X
                //Chilkat 
                #region                
                //Chilkat.MailMan mailman = new Chilkat.MailMan();

                //// Set the SMTP server.
                //mailman.SmtpHost = "m.halogistics.co.kr";

                //// Set SMTP login and password (if necessary)
                //mailman.SmtpUsername = "websmtps@halogistics.co.kr";
                //mailman.SmtpPassword = "Gmddkfhwltmxlrtm!12";

                //mailman.SmtpSsl = true;
                //mailman.SmtpPort = 465;

                //// Create a new email object
                //Chilkat.Email email = new Chilkat.Email();

                //email.Subject = "This is a test";
                //email.Body = "This is a test";
                //email.From = "twkim@yjit.co.kr";
                //bool success = email.AddTo("twkim", "twkim@yjit.co.kr");

                //success = mailman.SendEmail(email);
                //success = mailman.CloseSmtpConnection();
                #endregion

                //System.Net.Mail의 465 Port Implicit Port는 사용 불가능
                #region
                //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("m.halogistics.co.kr", 587)
                //{
                //    UseDefaultCredentials = false, // 시스템에 설정된 인증 정보를 사용하지 않는다.
                //    EnableSsl = true,  // SSL을 사용한다.
                //    DeliveryMethod = SmtpDeliveryMethod.Network, // 이걸 하지 않으면 Gmail에 인증을 받지 못한다.
                //    Credentials = new System.Net.NetworkCredential("websmtps@halogistics.co.kr", "Gmddkfhwltmxlrtm!12"),
                //    Timeout = 10000
                //};

                //MailAddress from = new MailAddress("twkim@yjit.co.kr");
                //MailAddress to = new MailAddress("twkim@yjit.co.kr");

                //MailMessage message = new MailMessage(from, to);

                //message.Body = "This is a test e-mail message sent by an application. ";
                //string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
                //message.Body += Environment.NewLine + someArrows;
                //message.BodyEncoding = System.Text.Encoding.UTF8;
                //message.Subject = "test message 2" + someArrows;
                //message.SubjectEncoding = System.Text.Encoding.UTF8;

                ////서버 인증서의 유효성 검사하는 부분을 무조건 true
                ////System.Net.ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;

                //client.Send(message);

                //message.Dispose();
                #endregion


                #region
                //DateTime dateT = DateTime.Now;
                //string datePart = dateT.ToString("yyyy-MM-dd hh:mm:ss");

                //MailMessage mail = new MailMessage();

                //mail.From = new MailAddress("twkim@yjit.co.kr");
                ////mail.From = new MailAddress(rtnVal.fst_EndAddr); 
                //mail.To.Add("twkim@yjit.co.kr");
                //mail.Subject = "[흥아로지스틱스]" + datePart + "에 전송된 고객의 소리 입니다.";
                //mail.Body = MakeEmailForm(rtnVal); //텍스트로 보내는 body
                //mail.IsBodyHtml = true;

                //mail.SubjectEncoding = System.Text.Encoding.UTF8;
                //mail.BodyEncoding = System.Text.Encoding.UTF8;

                //SmtpClient smtp = new SmtpClient("mail.yjit.co.kr");
                //smtp.Port = 587;
                //smtp.Credentials = new System.Net.NetworkCredential("system@yjit.co.kr", "elvis2015");
                //smtp.EnableSsl = true;
                //////서버 인증서의 유효성 검사하는 부분을 무조건 true (이거 하면 메일 보내는건 문제 없음)
                //////우리 도메인의 smtp는 메일이 잘 보내짐.
                //System.Net.ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                //smtp.Send(mail);
                //mail.Dispose();                
                #endregion

                //System.Web.Mail 지원 X
                #region
                //System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();

                //msg.Body = "Test is good";
                //msg.Subject = "Test mail";
                //msg.To = "from@chol.net";
                //msg.From = "to@hanmail.net";

                //int cdoBasic = 1;

                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true);
                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 465);
                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "...[다음_사용자_계정]...");
                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "...[다음_사용자_암호]...");

                //SmtpMail.SmtpServer = "smtp.daum.net";
                //SmtpMail.Send(msg);
                #endregion

                return Json("Y");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public string MakeEmailForm(mailParam rtnVal)
        {
            string strHTML = "";

            strHTML += "보내시는 분 : ";
            strHTML += rtnVal.fst_EndAddr + "\n\n";
            strHTML += "제목 : ";
            strHTML += rtnVal.mail_title + "\n\n";
            strHTML += "내용 : ";
            strHTML += rtnVal.mail_Content + "\n\n";

            return strHTML;
        }

        /// <summary>
        /// 공지사항 데이터 가져오기~
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult fnGetNoticeList(JsonData value)
        {
            try
            {
                string rtnJson = "";
                string vJsonData = value.vJsonData.ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(vJsonData);

                ADO_Conn con = new ADO_Conn();
                DBA dbConn = new DBA(con.ConnectionStr, DbConfiguration.Current.DatabaseType);
                DataTable Resultdt = dbConn.SqlGet(con.fnGetNoticeData_Query(dt.Rows[0]));

                if (Resultdt != null)
                {
                    Resultdt.TableName = "NoticeList";
                    if (Resultdt.Rows.Count == 0)
                    {
                        rtnJson = MakeJson("N", "데이터가 없습니다.", Resultdt);
                    }
                    else
                    {   
                        rtnJson = MakeJson("Y", "Success", Resultdt);
                    }
                }
                else
                {
                    rtnJson = MakeJson("N", "데이터가 없습니다.");
                }
                

                return Json(rtnJson);
            }
            catch (Exception e)
            {
                strJson = MakeJson("E", e.Message);
                return Json(strJson);
            }
        }

        public string MakeJson(string status, string Msg)
        {
            try
            {
                string json = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("trxCode");
                dt.Columns.Add("trxMsg");
                DataRow row1 = dt.NewRow();
                row1["trxCode"] = status;
                row1["trxMsg"] = Msg;
                dt.Rows.Add(row1);
                dt.TableName = "Result";
                ds.Tables.Add(dt);
                string strValue = JsonConvert.SerializeObject(ds);
                json = strValue;
                return json;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string MakeJson(string status, string Msg, DataTable args)
        {
            try
            {
                string json = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("trxCode");
                dt.Columns.Add("trxMsg");
                DataRow row1 = dt.NewRow();
                row1["trxCode"] = status;
                row1["trxMsg"] = Msg;
                dt.Rows.Add(row1);
                dt.TableName = "Result";
                ds.Tables.Add(dt);
                if (status != "E" && args.Rows.Count > 0)
                {
                    ds.Tables.Add(args);
                }
                string strValue = JsonConvert.SerializeObject(ds);
                json = strValue;                

                return json;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        string _NoticeFilePath = "/data/notice/";

        public ActionResult NoticeDownload(string filename, string rFilename)
        {
            try
            {
                string FullFilePath = Server.MapPath(_NoticeFilePath) + rFilename;
                if (System.IO.File.Exists(FullFilePath))    //파일이 존재한다면
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(FullFilePath);
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
                }
                else
                {
                    return Content("<script>alert('파일이 존재하지 않습니다'); history.back(-1);</script>");
                    //return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Content("<script>alert('파일이 존재하지 않습니다');  history.back(-1);</script>");
                //return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }
        }

    }
}