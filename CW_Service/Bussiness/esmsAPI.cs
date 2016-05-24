using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Xml;

namespace CW_Service
{
    class esmsAPI
    {
        public int SendSMS(string strPhone, string strMsg)
        {
            int _Result = 0;
            try
            {
                string url = "http://api.esms.vn/MainService.svc/xml/SendMultipleMessage_V2/";

                // declare ascii encoding
                UTF8Encoding encoding = new UTF8Encoding();
                string strResult = string.Empty;
                string customers = "";

                //strPhone += ",";
                //string[] lstPhone = strPhone.Split(',');
                //foreach (string item in lstPhone)
                //{
                //    if (item == "") continue;
                //    customers = customers + @"<CUSTOMER>"
                //                    + "<PHONE>" + item + "</PHONE>"
                //                    + "</CUSTOMER>";
                //}

                customers = @"<CUSTOMER>"
                                  + "<PHONE>" +
                                        strPhone
                                  + "</PHONE>"
                            + "</CUSTOMER>";

                //SMSTYPE 3: đầu số ngẫu nhiên tốc độ chậm, SMSTYPE=7: đầu số ngẫu nhiên tốc độ cao, SMSTYPE=4: Đầu số 19001534; SMSTYpe=6: đàu số 8755                               
                string SampleXml = @"<RQST>"
                                   + "<APIKEY>" + SMS_Config_Info.APIKey + "</APIKEY>"
                                   + "<SECRETKEY>" + SMS_Config_Info.SecretKey + "</SECRETKEY>"
                                   + "<ISFLASH>0</ISFLASH>"
                                   + "<SMSTYPE>" + SMS_Config_Info.SMS_TYPE + "</SMSTYPE>"
                                   + "<CONTENT>" + strMsg + "</CONTENT>"
                                   + "<CONTACTS>" + customers + "</CONTACTS>"


               + "</RQST>";
                //Tham khao them ve SMSTYPE tai day: http://esms.vn/SMSApi/ApiSendSMSNormal


                string postData = SampleXml.Trim().ToString();
                // convert xmlstring to byte using ascii encoding
                byte[] data = encoding.GetBytes(postData);
                // declare httpwebrequet wrt url defined above
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                // set method as post
                webrequest.Method = "POST";
                webrequest.Timeout = 500000;
                // set content type
                webrequest.ContentType = "application/x-www-form-urlencoded";
                // set content length
                webrequest.ContentLength = data.Length;
                // get stream data out of webrequest object
                Stream newStream = webrequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                // declare & read response from service
                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();

                // set utf8 encoding
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                // read response stream from response object
                StreamReader loResponseStream =
                    new StreamReader(webresponse.GetResponseStream(), enc);
                // read string from stream data
                strResult = loResponseStream.ReadToEnd();
                // close the stream object
                loResponseStream.Close();
                // close the response object
                webresponse.Close();
                // below steps remove unwanted data from response string
                strResult = strResult.Replace("</string>", "");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strResult);
                string strCodeResult = xmlDoc.SelectSingleNode("/SmsResultModel/CodeResult").InnerText;
                int.TryParse(strCodeResult, out _Result);
                return _Result;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public int SendBrandname(string phone, string message)
        {
            int _Result = 0;
            try
            {

                string url = "http://api.esms.vn/MainService.svc/xml/SendMultipleSMSBrandname/";
                // declare ascii encoding
                UTF8Encoding encoding = new UTF8Encoding();

                string strResult = string.Empty;
                // sample xml sent to Service & this data is sent in POST

                string customers = "";

                string[] lstPhone = phone.Split(',');

                for (int i = 0; i < lstPhone.Count(); i++)
                {
                    customers = customers + @"<CUSTOMER>"
                                    + "<PHONE>" + lstPhone[i] + "</PHONE>"
                                    + "</CUSTOMER>";
                }


                string SampleXml = @"<RQST>"
                                   + "<APIKEY>" + SMS_Config_Info.APIKey + "</APIKEY>"
                                   + "<SECRETKEY>" + SMS_Config_Info.SecretKey + "</SECRETKEY>"
                                   + "<SMSTYPE>2</SMSTYPE>"
                                   + "<BRANDNAME>" + SMS_Config_Info.BranhchName + "</BRANDNAME>"
                                   + "<CONTENT>" + message + "</CONTENT>"
                                   + "<CONTACTS>" + customers + "</CONTACTS>"
               + "</RQST>";
                string postData = SampleXml.Trim().ToString();
                // convert xmlstring to byte using ascii encoding
                byte[] data = encoding.GetBytes(postData);
                // declare httpwebrequet wrt url defined above
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                // set method as post
                webrequest.Method = "POST";
                webrequest.Timeout = 500000;
                // set content type
                webrequest.ContentType = "application/x-www-form-urlencoded";
                // set content length
                webrequest.ContentLength = data.Length;
                // get stream data out of webrequest object
                Stream newStream = webrequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                // declare & read response from service
                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();

                // set utf8 encoding
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                // read response stream from response object
                StreamReader loResponseStream =
                    new StreamReader(webresponse.GetResponseStream(), enc);
                // read string from stream data
                strResult = loResponseStream.ReadToEnd();
                // close the stream object
                loResponseStream.Close();
                // close the response object
                webresponse.Close();
                // below steps remove unwanted data from response string
                strResult = strResult.Replace("</string>", "");


                return _Result;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }
    }
}
