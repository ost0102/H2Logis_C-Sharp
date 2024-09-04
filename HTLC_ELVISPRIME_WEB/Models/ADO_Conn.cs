using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using System.Text;

using System.Configuration;

namespace HTLC_ELVISPRIME_COM.Models
{
    public class ADO_Conn 
    {
        public string ConnectionStr = ConfigurationManager.ConnectionStrings["HTLC"].ConnectionString;
        private string memberKey = ConfigurationManager.AppSettings["memberKey"].ToString();

        public void ThrowMsg(bool ErrorOccur, string Msg)
        {
            ErrorOccur = true;
            throw new Exception(Msg);
        }

        public string Search_Notice(string Opt , string Type, string SearchText, int pageIndex)
        {
            string sSql = "";
            #region //이전 소스                        
            //sSql += "  SELECT * FROM ";
            //sSql += "  ( ";
            //sSql += "      SELECT ROW_NUMBER() OVER(ORDER BY A.REGDT) NUM ";
            //sSql += "              , A.* ";
            //sSql += "      FROM NOTICE A ";
            //sSql += "      WHERE A.USE_YN = 'y' ";
            //sSql += "         AND A.NOTICE_YN = 'y' ";
            //sSql += "      AND A.TITLE LIKE '%" + SearchText + "%' ";
            //sSql += "      ORDER BY A.REGDT DESC "; 
            //sSql += "  ) ";
            //sSql += "  UNION ALL ";
            //sSql += "  SELECT * FROM ";
            //sSql += "  ( ";
            //sSql += "      SELECT ROW_NUMBER() OVER(ORDER BY A.REGDT) NUM ";
            //sSql += "              , A.* ";
            //sSql += "      FROM NOTICE A ";
            //sSql += "      WHERE A.USE_YN = 'y' ";
            //sSql += "         AND A.NOTICE_YN = 'n' ";
            //sSql += "      AND A.TITLE LIKE '%" + SearchText + "%'";
            //sSql += "      ORDER BY A.REGDT DESC ";
            //sSql += " ) ";
            #endregion

            if (pageIndex == 0) pageIndex = 1;
            
            sSql += " SELECT * ";
            sSql += "  FROM ( ";
            sSql += "              SELECT ROWNUM AS RNUM ";
            sSql += "                     , FLOOR((ROWNUM-1) /10 + 1) AS PAGE ";
            sSql += "                     , COUNT(*) OVER () AS TOTCNT ";
            sSql += "                     , X.* ";
            sSql += "             FROM ( ";
            sSql += "                         SELECT * FROM             ";
            sSql += "                         ( SELECT ROW_NUMBER() OVER(ORDER BY A.REGDT) NUM, A.*  ";
            sSql += "                             FROM NOTICE A  ";
            sSql += "                            WHERE A.USE_YN = 'y'  ";
            sSql += "                               AND A.NOTICE_YN = 'y' ";
            if (!string.IsNullOrEmpty(SearchText)) sSql += "                               AND A." + Opt + " LIKE '%" + SearchText + "%' ";
            if (!string.IsNullOrEmpty(Type)) sSql += "                               AND A.TYPE = '" + Type + "' ";
            sSql += "                           ORDER BY A.REGDT DESC )  ";
            sSql += "                           UNION ALL ";
            sSql += "                         SELECT * FROM     ";
            sSql += "                           ( SELECT ROW_NUMBER() OVER(ORDER BY A.REGDT) NUM, A.*  ";
            sSql += "                              FROM NOTICE A  ";
            sSql += "                             WHERE A.USE_YN = 'y'  ";
            sSql += "                                AND A.NOTICE_YN = 'n'  ";
            if (!string.IsNullOrEmpty(SearchText)) sSql += "                               AND A." + Opt + " LIKE '%" + SearchText + "%' ";
            if (!string.IsNullOrEmpty(Type)) sSql += "                               AND A.TYPE = '" + Type + "' ";
            sSql += "                             ORDER BY A.REGDT DESC )   ";                   
            sSql += "                      ) X  ";
            sSql += "           ) XX ";
            sSql += "  WHERE XX.PAGE = "+ pageIndex +" ";
            
            return sSql;
        }

        public string Search_NoticeView(string noticeID)
        {
            string sSql = "";
            sSql += " SELECT 'VIEW' AS FLAG, V.* FROM NOTICE V ";
            sSql += " WHERE V.NOTICE_ID = " + noticeID + " ";
            sSql += " UNION ALL ";
            sSql += " SELECT 'PREV' AS FLAG, P.* FROM NOTICE P  ";
            sSql += " WHERE P.NOTICE_ID  = (SELECT MAX(NOTICE_ID) FROM NOTICE WHERE NOTICE_ID < " + noticeID + ") ";
            sSql += " UNION ALL ";
            sSql += " SELECT 'NEXT' AS FLAG, N.* FROM NOTICE N  ";
            sSql += " WHERE N.NOTICE_ID  = (SELECT MIN(NOTICE_ID) FROM NOTICE WHERE NOTICE_ID > " + noticeID + ") ";
            return sSql;
        }

        public string Update_ViewCnt(string noticeID)
        {
            string sSql = "";
            sSql += " UPDATE NOTICE SET CNT = CNT + 1 WHERE NOTICE_ID = '" + noticeID + "'";
            return sSql;
        }

        public string Update_RecruitmentViewCnt(string noticeID)
        {
            string sSql = "";
            sSql += " UPDATE RECRUITMENT SET CNT = CNT + 1 WHERE RECRUITMENT_ID = '" + noticeID + "'";
            return sSql;
        }

        public string Admin_Notice(string Opt, string Type, string SearchText, int pageIndex)
        {
            string sSql = "";
            if (pageIndex == 0) pageIndex = 1;

            sSql += " SELECT * ";
            sSql += "  FROM ( ";
            sSql += "              SELECT ROWNUM AS RNUM ";
            sSql += "                     , FLOOR((ROWNUM-1) /10 + 1) AS PAGE ";
            sSql += "                     , COUNT(*) OVER () AS TOTCNT ";
            sSql += "                     , X.* ";
            sSql += "             FROM ( ";
            sSql += "                         SELECT * FROM             ";
            sSql += "                         ( SELECT ROW_NUMBER() OVER(ORDER BY A.REGDT) NUM, A.*  ";
            sSql += "                             FROM NOTICE A  ";
            sSql += "                           WHERE A.NOTICE_YN = 'y' ";
            if (Opt == "ALL")
            {
                sSql += "                               AND ( A.TITLE LIKE '%" + SearchText + "%' OR A.CONTENT LIKE '%" + SearchText + "%') ";
            }
            else
            {
                if (!string.IsNullOrEmpty(SearchText)) sSql += "                               AND A." + Opt + " LIKE '%" + SearchText + "%' ";
            }
            if (!string.IsNullOrEmpty(Type)) sSql += "                               AND A.TYPE = '" + Type + "' ";
            sSql += "                           ORDER BY A.REGDT DESC )  ";
            sSql += "                           UNION ALL ";
            sSql += "                         SELECT * FROM     ";
            sSql += "                           ( SELECT ROW_NUMBER() OVER(ORDER BY A.REGDT) NUM, A.*  ";
            sSql += "                              FROM NOTICE A  ";
            sSql += "                                WHERE A.NOTICE_YN = 'n'  ";
            if (Opt == "ALL")
            {
                sSql += "                               AND ( A.TITLE LIKE '%" + SearchText + "%' OR A.CONTENT LIKE '%" + SearchText + "%') ";
            }
            else
            {
                if (!string.IsNullOrEmpty(SearchText)) sSql += "                               AND A." + Opt + " LIKE '%" + SearchText + "%' ";
            }
            if (!string.IsNullOrEmpty(Type)) sSql += "                               AND A.TYPE = '" + Type + "' ";
            sSql += "                             ORDER BY A.REGDT DESC )   ";
            sSql += "                      ) X  ";
            sSql += "           ) XX ";
            sSql += "  WHERE XX.PAGE = " + pageIndex + " ";

            return sSql;
        }

        public string Admin_NoticeView(string noticeID)
        {
            string sSql = "";
            sSql += " SELECT 'VIEW' AS FLAG, V.* FROM NOTICE V ";
            sSql += " WHERE V.NOTICE_ID = " + noticeID + " ";            
            return sSql;
        }

        public string Admin_NoticeDel(string noticeID)
        {
            string sSql = "";
            sSql += " DELETE FROM NOTICE ";
            sSql += " WHERE NOTICE_ID = " + noticeID + " ";
            return sSql;
        }

        public string Admin_NoticeAdd(Hashtable ht)
        {
            string sSql = "";
            sSql += " INSERT INTO NOTICE VALUES ( ";
            sSql += "  (SELECT NVL(MAX(NOTICE_ID), 0) + 1 FROM NOTICE) ";   // NOTICE_ID
            sSql += " , '"+ ht["TITLE"].ToString() +"'";                            //TITLE
            sSql += " , 0";                                                             //CNT
            sSql += " , '관리자'";                                                     //WRITER
            sSql += " , '" + ht["USE_YN"].ToString() + "'";                       //USE_YN
            sSql += " , '" + ht["NOTICE_YN"].ToString() + "'";                  //NOTICE_YN
            sSql += " , TO_CHAR(SYSDATE, 'YY-MM-DD')";   //REGDT
            sSql += " , '' ";                                                           //EDITDT
            sSql += " , '" + ht["FILE"].ToString() + "'";                          //FILE
            sSql += " , '" + ht["FILE_NAME"].ToString() + "'";                 //FILE_NAME
            sSql += " , '" + ht["FILE1"].ToString() + "'";                          //FILE1
            sSql += " , '" + ht["FILE1_NAME"].ToString() + "'";                 //FILE1_NAME
            sSql += " , '" + ht["FILE2"].ToString() + "'";                          //FILE2
            sSql += " , '" + ht["FILE2_NAME"].ToString() + "'";                 //FILE2_NAME
            //sSql += " , '" + ht["CONTENT"].ToString() + "'";                 //CONTENT

            //TWKIM 20211124 - CLOB 데이터 타입은 한번에 4000byte 이상 데이터가 들어가지 않아서 아래와 같이 쪼개서 넣으면 데이터가 삽입 가능하기 때문에 로직 추가 하였습니다.
            int nConentLength = ht["CONTENT"].ToString().Trim().Length;

            if (nConentLength < 1000)
            {
                sSql += " , '" + ht["CONTENT"].ToString() + "'";                  //CONTENT
            }
            else
            {
                int nFor = (nConentLength / 1000) + 1;
                string strContent = "";

                for (int i = 1; i <= nFor; i++)
                {
                    if (i == 1)
                    {
                        strContent += " TO_CLOB('" + ht["CONTENT"].ToString().Substring(0, (1000 * i)) + "') ";
                    }
                    else
                    {
                        //마지막
                        if (i == nFor)
                        {
                            strContent += " || TO_CLOB('" + ht["CONTENT"].ToString().Substring((1000 * (i - 1)), (ht["CONTENT"].ToString().Length - 1) - (1000 * (i - 1))) + "') ";
                        }
                        else
                        {
                            strContent += " || TO_CLOB('" + ht["CONTENT"].ToString().Substring((1000 * (i - 1)), 1000) + "') ";
                        }
                    }
                }

                sSql += " , " + strContent + " ";                  //CONTENT
            }

            sSql += " , '0'";                 //TYPE
            sSql += " ) ";
            return sSql;
        }

        public string Admin_NoticeModify(Hashtable ht)
        {
            string sSql = "";
            sSql += " UPDATE NOTICE SET " + "\r\n";
            sSql += "     TITLE = '" + ht["TITLE"].ToString() + "' " + "\r\n";
            sSql += "    , USE_YN = '" + ht["USE_YN"].ToString() + "' " + "\r\n";
            sSql += "    , NOTICE_YN = '" + ht["NOTICE_YN"].ToString() + "' " + "\r\n";
            sSql += "    , EDITDT = TO_CHAR(SYSDATE, 'YY-MM-DD') " + "\r\n";
            if (ht.ContainsKey("FILE"))
            {
                sSql += "    , \"FILE\" = '" + ht["FILE"].ToString() + "'" + "\r\n";                          //FILE
                sSql += "    , FILE_NAME = '" + ht["FILE_NAME"].ToString() + "'" + "\r\n";                          //FILE
            }
            if (ht.ContainsKey("FILE1"))
            {
                sSql += "    , FILE1 = '" + ht["FILE1"].ToString() + "'";                          //FILE
                sSql += "    , FILE1_NAME = '" + ht["FILE1_NAME"].ToString() + "'" + "\r\n";                          //FILE
            }
            if (ht.ContainsKey("FILE2"))
            {
                sSql += "    , FILE2 = '" + ht["FILE2"].ToString() + "'" + "\r\n";                          //FILE
                sSql += "    , FILE2_NAME = '" + ht["FILE2_NAME"].ToString() + "'" + "\r\n";                          //FILE
            }
            //sSql += "    , CONTENT = '" + ht["CONTENT"].ToString() + "'" + "\r\n";                 //FILE2_NAME
            //sSql += "    , TYPE = '" + ht["S_TYPE"].ToString() + "'" + "\r\n";                 //TYPE

            //TWKIM 20211124 - CLOB 데이터 타입은 한번에 4000byte 이상 데이터가 들어가지 않아서 아래와 같이 쪼개서 넣으면 데이터가 삽입 가능하기 때문에 로직 추가 하였습니다.
            int nConentLength = ht["CONTENT"].ToString().Trim().Length;

            if (nConentLength < 1000)
            {
                sSql += "    , CONTENT = '" + ht["CONTENT"].ToString() + "'" + "\r\n";                 //FILE2_NAME
            }
            else
            {
                int nFor = (nConentLength / 1000) + 1;
                string strContent = "";

                for (int i = 1; i <= nFor; i++)
                {
                    if (i == 1)
                    {
                        strContent += " TO_CLOB('" + ht["CONTENT"].ToString().Substring(0, (1000 * i)) + "') ";
                    }
                    else
                    {
                        //마지막
                        if (i == nFor)
                        {
                            strContent += " || TO_CLOB('" + ht["CONTENT"].ToString().Substring((1000 * (i - 1)), (ht["CONTENT"].ToString().Length - 1) - (1000 * (i - 1))) + "') ";
                        }
                        else
                        {
                            strContent += " || TO_CLOB('" + ht["CONTENT"].ToString().Substring((1000 * (i - 1)), 1000) + "') ";
                        }
                    }
                }

                sSql += "    , CONTENT = " + strContent + "" + "\r\n";                 //FILE2_NAME                
            }

            sSql += " WHERE NOTICE_ID = " + ht["NOTICE_ID"].ToString() + " " + "\r\n";
            return sSql;
        }

        public string Admin_MemberSearch()
        {
            string sSql = "";
            sSql += " SELECT * FROM ( ";
            sSql += " SELECT ROWNUM AS RNUM ";
            sSql += "         , MEMB_NO ";
            sSql += "         , M_ID ";
            sSql += "         , \"LEVEL\" AS LVL ";
            sSql += "         , AUTH_LEVEL ";
            sSql += "         , STATUS ";
            sSql += "         ,  CryptString.decrypt(PASSWORD, '" + memberKey + "') AS PWD ";
            sSql += "         , \"NAME\" AS M_NAME ";
            sSql += "         , MOBILE ";
            sSql += "         , SUBSTR(NVL(REGDT, ''), 0, 10) AS REGDT ";
            sSql += "         , LAST_LOGIN ";
            sSql += "  FROM MEMBER ";
            sSql += " WHERE NVL(DEL_FLAG, 'n') = 'n' ";
            sSql += "  )  ORDER BY RNUM DESC ";
            return sSql;
        }

        public string Admin_MemberSelect(string id)
        {
            string sSql = "";            
            sSql += " SELECT MEMB_NO ";
            sSql += "         , M_ID ";
            sSql += "         , \"LEVEL\" AS LVL ";
            sSql += "         , AUTH_LEVEL ";
            sSql += "         , STATUS ";
            sSql += "         ,  CryptString.decrypt(PASSWORD, '" + memberKey + "') AS PWD ";
            sSql += "         , \"NAME\" AS M_NAME ";
            sSql += "         , MOBILE ";
            sSql += "         , SUBSTR(NVL(REGDT, ''), 0, 10) AS REGDT ";
            sSql += "         , LAST_LOGIN ";
            sSql += "  FROM MEMBER ";
            sSql += " WHERE MEMB_NO = "+ id +" ";            
            return sSql;
        }

        public string Admin_MemberUpdate(Hashtable ht)
        {
            string sSql = "";
            sSql += " UPDATE MEMBER ";
            sSql += " SET ";
            if (ht.ContainsKey("DEL_FLAG"))
            {
                if (ht["DEL_FLAG"].ToString() == "y")
                {
                    sSql += "       DEL_FLAG = 'y' ";
                    sSql += "     , DELDATE = TO_CHAR(SYSDATE, 'YYYY-MM-DD hh24:mi:ss') ";
                }
                else
                {
                    
                     sSql += "         \"NAME\" = '" + ht["NAME"].ToString() + "' ";
                     if (!string.IsNullOrEmpty(ht["PASSWORD"].ToString())) sSql += "         , PASSWORD = CryptString.encrypt('" + ht["PASSWORD"].ToString() + "', '" + memberKey + "') ";                    
                    sSql += "         , MOBILE = '" + ht["MOBILE1"].ToString() + "-" + ht["MOBILE2"].ToString() + "-" + ht["MOBILE3"].ToString() + "' ";
                    sSql += "         , REGDT = TO_CHAR(SYSDATE, 'YYYY-MM-DD hh24:mi:ss') ";                
                }
            }
            else
            {                
                sSql += "         \"NAME\" = '" + ht["NAME"].ToString() + "' ";
                if (!string.IsNullOrEmpty(ht["PASSWORD"].ToString())) sSql += "         , PASSWORD = CryptString.encrypt('" + ht["PASSWORD"].ToString() + "', '" + memberKey + "') ";
                sSql += "         , MOBILE = '" + ht["MOBILE1"].ToString() + "-" + ht["MOBILE2"].ToString() + "-" + ht["MOBILE3"].ToString() + "' ";
                sSql += "         , REGDT = TO_CHAR(SYSDATE, 'YYYY-MM-DD hh24:mi:ss') ";                
            }
                        
            sSql += " WHERE MEMB_NO = '"+ ht["MEMB_NO"].ToString() +"' ";
            return sSql;
        }

        public string Admin_MemberAdd(Hashtable ht)
        {
            string sSql = "";
            sSql += " INSERT INTO MEMBER VALUES ( ";
            sSql += " (SELECT NVL(MAX(MEMB_NO), 0) + 1 FROM MEMBER) ";
            sSql += " , '" + ht["M_ID"].ToString() + "' ";
            sSql += " , 50 ";   //level
            sSql += " , '' ";   // auth_level
            sSql += " , '' ";   // status
            sSql += " , CryptString.encrypt('" + ht["PASSWORD"].ToString() + "', '" + memberKey + "') ";
            sSql += " , '" + ht["NAME"].ToString() + "' ";
            sSql += " , '" + ht["MOBILE1"].ToString() + "-" + ht["MOBILE2"].ToString() + "-" + ht["MOBILE3"].ToString() + "' ";
            sSql += " , TO_CHAR(SYSDATE, 'YYYY-MM-DD hh24:mi:ss') ";
            sSql += " , '' ";   // last_login
            sSql += " , '' ";   // last_loginIP
            sSql += " , 0 ";   // cnt_login
            sSql += " , '' ";   // deldate
            sSql += " , '' ";   // drop_comment
            sSql += " , '' ";   // del_flag
            sSql += " ) ";
            return sSql;
        }

        public string Admin_Login(string id, string pwd)
        {
            string sSql = "";
            sSql += " SELECT MEMB_NO FROM MEMBER ";
            sSql += " WHERE M_ID = '" + id + "' ";
            sSql += " AND CryptString.decrypt(PASSWORD, '" + memberKey + "') = '" + pwd + "' ";
            return sSql;
        }

        public string Admin_Login_Update(string id, string userIP)
        {
            string sSql = "";
            sSql += " UPDATE MEMBER ";
            sSql += " SET LAST_LOGIN = TO_CHAR(SYSDATE, 'YYYY-MM-DD hh24:mi:ss') ";
            sSql += " AND LAST_LOGIN_IP = '"+ userIP +"' ";
            sSql += " WHERE MEMB_NO = '" + id + "' ";
            return sSql;
        }

        public string Admin_Member_Check(string id)
        {
            string sSql = "";
            sSql += " SELECT M_ID FROM MEMBER ";
            sSql += " WHERE M_ID = '" + id + "' ";            
            return sSql;
        }

        /* 채용공고 sql */
        public string Search_Recruitment(string Opt, string Type, string SearchText, int pageIndex)
        {
            string sSql = "";
            if (pageIndex == 0) pageIndex = 1;

            sSql += " SELECT * ";
            sSql += "  FROM ( ";
            sSql += "              SELECT ROWNUM AS RNUM ";
            sSql += "                     , FLOOR((ROWNUM-1) /10 + 1) AS PAGE ";
            sSql += "                     , COUNT(*) OVER () AS TOTCNT ";
            sSql += "                     , X.* ";
            sSql += "             FROM ( ";
            sSql += "                         SELECT * FROM             ";
            sSql += "                         ( SELECT ROW_NUMBER() OVER(ORDER BY A.REGDT) NUM, A.*  ";
            sSql += "                             FROM RECRUITMENT A  ";
            sSql += "                            WHERE A.USE_YN = 'y'  ";
            sSql += "                               AND A.RECRUITMENT_YN = 'y' ";
            if (!string.IsNullOrEmpty(SearchText)) sSql += "                               AND A." + Opt + " LIKE '%" + SearchText + "%' ";
            if (!string.IsNullOrEmpty(Type)) sSql += "                               AND A.TYPE = '" + Type + "' ";
            sSql += "                           ORDER BY A.REGDT DESC )  ";
            sSql += "                           UNION ALL ";
            sSql += "                         SELECT * FROM     ";
            sSql += "                           ( SELECT ROW_NUMBER() OVER(ORDER BY A.REGDT) NUM, A.*  ";
            sSql += "                              FROM RECRUITMENT A  ";
            sSql += "                             WHERE A.USE_YN = 'y'  ";
            sSql += "                                AND A.RECRUITMENT_YN = 'n'  ";
            if (!string.IsNullOrEmpty(SearchText)) sSql += "                               AND A." + Opt + " LIKE '%" + SearchText + "%' ";
            if (!string.IsNullOrEmpty(Type)) sSql += "                               AND A.TYPE = '" + Type + "' ";
            sSql += "                             ORDER BY A.REGDT DESC )   ";
            sSql += "                      ) X  ";
            sSql += "           ) XX ";
            sSql += "  WHERE XX.PAGE = " + pageIndex + " ";

            return sSql;
        }

        public string Search_RecruitmentView(string noticeID)
        {
            string sSql = "";
            sSql += " SELECT 'VIEW' AS FLAG, V.* FROM RECRUITMENT V ";
            sSql += " WHERE V.RECRUITMENT_ID = " + noticeID + " ";
            sSql += " UNION ALL ";
            sSql += " SELECT 'PREV' AS FLAG, P.* FROM RECRUITMENT P  ";
            sSql += " WHERE P.RECRUITMENT_ID  = (SELECT MAX(RECRUITMENT_ID) FROM RECRUITMENT WHERE RECRUITMENT_ID < " + noticeID + ") ";
            sSql += " UNION ALL ";
            sSql += " SELECT 'NEXT' AS FLAG, N.* FROM RECRUITMENT N  ";
            sSql += " WHERE N.RECRUITMENT_ID  = (SELECT MIN(RECRUITMENT_ID) FROM RECRUITMENT WHERE RECRUITMENT_ID > " + noticeID + ") ";
            return sSql;
        }

        public string Admin_Recruitment(string Opt, string SearchText, int pageIndex)
        {
            string sSql = "";
            if (pageIndex == 0) pageIndex = 1;

            sSql += " SELECT * ";
            sSql += "  FROM ( ";
            sSql += "              SELECT ROWNUM AS RNUM ";
            sSql += "                     , FLOOR((ROWNUM-1) /10 + 1) AS PAGE ";
            sSql += "                     , COUNT(*) OVER () AS TOTCNT ";
            sSql += "                     , X.* ";
            sSql += "             FROM ( ";
            sSql += "                         SELECT * FROM             ";
            sSql += "                         ( SELECT ROW_NUMBER() OVER(ORDER BY A.REGDT) NUM, A.*  ";
            sSql += "                             FROM RECRUITMENT A  ";
            sSql += "                           WHERE A.RECRUITMENT_YN = 'y' ";
            if (Opt == "ALL")
            {
                sSql += "                               AND ( A.TITLE LIKE '%" + SearchText + "%' OR A.CONTENT LIKE '%" + SearchText + "%') ";
            }
            else
            {
                if (!string.IsNullOrEmpty(SearchText)) sSql += "                               AND A." + Opt + " LIKE '%" + SearchText + "%' ";
            }
            sSql += "                           ORDER BY A.REGDT DESC )  ";
            sSql += "                           UNION ALL ";
            sSql += "                         SELECT * FROM     ";
            sSql += "                           ( SELECT ROW_NUMBER() OVER(ORDER BY A.REGDT) NUM, A.*  ";
            sSql += "                              FROM RECRUITMENT A  ";
            sSql += "                                WHERE A.RECRUITMENT_YN = 'n'  ";
            if (Opt == "ALL")
            {
                sSql += "                               AND ( A.TITLE LIKE '%" + SearchText + "%' OR A.CONTENT LIKE '%" + SearchText + "%') ";
            }
            else
            {
                if (!string.IsNullOrEmpty(SearchText)) sSql += "                               AND A." + Opt + " LIKE '%" + SearchText + "%' ";
            }
            sSql += "                             ORDER BY A.REGDT DESC )   ";
            sSql += "                      ) X  ";
            sSql += "           ) XX ";
            sSql += "  WHERE XX.PAGE = " + pageIndex + " ";

            return sSql;
        }

        public string Admin_RecruitmentView(string RecruitmentID)
        {
            string sSql = "";
            sSql += " SELECT 'VIEW' AS FLAG, V.* FROM RECRUITMENT V ";
            sSql += " WHERE V.RECRUITMENT_ID = " + RecruitmentID + " ";
            return sSql;
        }

        public string Admin_RecruitmentDel(string RecruitmentID)
        {
            string sSql = "";
            sSql += " DELETE FROM RECRUITMENT ";
            sSql += " WHERE RECRUITMENT_ID = " + RecruitmentID + " ";
            return sSql;
        }

        public string Admin_RecruitmentAdd(Hashtable ht)
        {
            string sSql = "";
            sSql += " INSERT INTO RECRUITMENT VALUES ( ";
            sSql += "  (SELECT NVL(MAX(RECRUITMENT_ID), 0) + 1 FROM RECRUITMENT) ";   // RECRUITMENT_ID
            sSql += " , '" + ht["TITLE"].ToString() + "'";                            //TITLE
            sSql += " , 0";                                                             //CNT
            sSql += " , '관리자'";                                                     //WRITER
            sSql += " , '" + ht["USE_YN"].ToString() + "'";                       //USE_YN
            sSql += " , '" + ht["RECRUITMENT_YN"].ToString() + "'";                  //RECRUITMENT_YN
            sSql += " , TO_CHAR(SYSDATE, 'YYYY-MM-DD hh24:mi:ss')";   //REGDT
            sSql += " , '' ";                                                           //EDITDT
            sSql += " , '" + ht["FILE"].ToString() + "'";                          //FILE
            sSql += " , '" + ht["FILE_NAME"].ToString() + "'";                 //FILE_NAME
            sSql += " , '" + ht["FILE1"].ToString() + "'";                          //FILE1
            sSql += " , '" + ht["FILE1_NAME"].ToString() + "'";                 //FILE1_NAME
            sSql += " , '" + ht["FILE2"].ToString() + "'";                          //FILE2
            sSql += " , '" + ht["FILE2_NAME"].ToString() + "'";                 //FILE2_NAME
            sSql += " , '" + ht["CONTENT"].ToString() + "'";                 //CONTENT
            sSql += " ) ";
            return sSql;
        }

        public string Admin_RecruitmentModify(Hashtable ht)
        {
            string sSql = "";
            sSql += " UPDATE RECRUITMENT SET " + "\r\n";
            sSql += "     TITLE = '" + ht["TITLE"].ToString() + "' " + "\r\n";
            sSql += "    , USE_YN = '" + ht["USE_YN"].ToString() + "' " + "\r\n";
            sSql += "    , RECRUITMENT_YN = '" + ht["RECRUITMENT_YN"].ToString() + "' " + "\r\n";
            sSql += "    , EDITDT = TO_CHAR(SYSDATE, 'YYYY-MM-DD hh24:mi:ss') " + "\r\n";
            if (ht.ContainsKey("FILE"))
            {
                sSql += "    , \"FILE\" = '" + ht["FILE"].ToString() + "'" + "\r\n";                          //FILE
                sSql += "    , FILE_NAME = '" + ht["FILE_NAME"].ToString() + "'" + "\r\n";                          //FILE
            }
            if (ht.ContainsKey("FILE1"))
            {
                sSql += "    , FILE1 = '" + ht["FILE1"].ToString() + "'";                          //FILE
                sSql += "    , FILE1_NAME = '" + ht["FILE1_NAME"].ToString() + "'" + "\r\n";                          //FILE
            }
            if (ht.ContainsKey("FILE2"))
            {
                sSql += "    , FILE2 = '" + ht["FILE2"].ToString() + "'" + "\r\n";                          //FILE
                sSql += "    , FILE2_NAME = '" + ht["FILE2_NAME"].ToString() + "'" + "\r\n";                          //FILE
            }
            sSql += "    , CONTENT = '" + ht["CONTENT"].ToString() + "'" + "\r\n";                 //FILE2_NAME
            sSql += " WHERE RECRUITMENT_ID = " + ht["RECRUITMENT_ID"].ToString() + " " + "\r\n";
            return sSql;
        }

        /// <summary>
        /// 공지사항 가져오기
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string fnGetNoticeData_Query(DataRow dr)
        {
            string sqlstr = "";

            sqlstr += " SELECT * ";
            sqlstr += "   FROM (SELECT  ";
            sqlstr += "                ROWNUM AS RNUM, ";
            sqlstr += "                FLOOR ( (ROWNUM - 1) / 10 + 1) AS PAGE, ";
            sqlstr += "                COUNT (*) OVER () AS TOTCNT, ";
            sqlstr += "                TOTAL.* ";
            sqlstr += "           FROM (  SELECT  ";
            sqlstr += "                          A.NOTICE_ID, ";
            sqlstr += "                          A.TITLE, ";
            sqlstr += "                          A.REGDT, ";
            sqlstr += "                          A.\"FILE\", ";
            sqlstr += "                          A.FILE_NAME, ";
            sqlstr += "                          A.FILE1, ";
            sqlstr += "                          A.FILE1_NAME, ";
            sqlstr += "                          A.FILE2, ";
            sqlstr += "                          A.FILE2_NAME, ";
            sqlstr += "                          A.CONTENT ";
            sqlstr += "                     FROM NOTICE A ";
            sqlstr += "                     WHERE 1=1  ";
            sqlstr += "                     AND USE_YN = 'y' ";

            if (dr["SEARCH"].ToString() != "")
            {
                sqlstr += "                     AND (TITLE LIKE '%" + dr["SEARCH"].ToString() + "%' OR CONTENT LIKE '%" + dr["SEARCH"].ToString() + "%')";
            }

            sqlstr += "                 ORDER BY REGDT DESC ";
            sqlstr += "                 )  ";
            sqlstr += "             TOTAL) ";
            sqlstr += "  WHERE PAGE = '" + dr["PAGE"].ToString() + "' ";
            
            return sqlstr;
        }


    }
}