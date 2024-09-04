using System;
using System.Data;
using Newtonsoft.Json;

namespace HTLC_ELVISPRIME_COMMON.YJIT_Utils
{
    public class Common
    {
        Encryption String_Encrypt = new Encryption();        

        /// <summary>
        /// Json 형식으로 데이터 만들기
        /// </summary>
        /// <param name="status"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
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
                json = String_Encrypt.encryptAES256(JsonConvert.SerializeObject(ds, Formatting.Indented));
                return json;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Json 형식으로 데이터 만들기
        /// </summary>
        /// <param name="status"></param>
        /// <param name="Msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
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

                //암호화 로직 추가 
                json = String_Encrypt.encryptAES256(strValue);
                return json;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Json 형식으로 데이터 만들기
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="DT1"></param>
        /// <returns></returns>
        public string MakeJson(DataTable Result, DataTable DT1)
        {

            string strJson = "";

            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(Result);
                ds.Tables.Add(DT1);

                strJson = JsonConvert.SerializeObject(ds, Formatting.Indented);

                strJson = String_Encrypt.encryptAES256(strJson);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return strJson;
        }

        /// <summary>
        /// Json 형식으로 데이터 만들기
        /// </summary>
        /// <param name="status"></param>
        /// <param name="Msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string MakeNonJson(string status, string Msg, DataTable args)
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
        
        /// <summary>
        /// 결과값 DT로 만들어 주는 함수
        /// </summary>
        /// <param name="status"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public DataTable MakeResultDT(string status, string Msg)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("trxCode");
            dt.Columns.Add("trxMsg");
            DataRow row1 = dt.NewRow();
            row1["trxCode"] = status;
            row1["trxMsg"] = Msg;
            dt.Rows.Add(row1);
            dt.TableName = "Result";

            return dt;
        }

        

        /// <summary>
        /// DataSet Json 형식으로 만들기
        /// </summary>
        /// <param name="status"></param>
        /// <param name="Msg"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public string DS_MakeJson(string status, string Msg, DataSet ds)
        {

            string strJson = "";

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("trxCode");
                dt.Columns.Add("trxMsg");
                DataRow row1 = dt.NewRow();
                row1["trxCode"] = status;
                row1["trxMsg"] = Msg;
                dt.Rows.Add(row1);
                dt.TableName = "Result";

                ds.Tables.Add(dt);               

                strJson = JsonConvert.SerializeObject(ds, Formatting.Indented);

                strJson = String_Encrypt.encryptAES256(strJson);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return strJson;
        }
    }
}
