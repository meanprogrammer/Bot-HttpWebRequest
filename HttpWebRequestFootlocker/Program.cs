﻿using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpWebRequestFootlocker
{
    class Program
    {
        static void Main(string[] args)
        {
            string base_url = "http://www.footlocker.com";

            HttpWebRequest initialRequest = (HttpWebRequest)WebRequest.Create(base_url);

            string rawCookies = "TID=5555%2D29121607402129070918580%2D0; TRACK_USER_P=29031291216074029070967883; USER_PROFILE=enw6maPKkWi63kKVcW5m4tPeCZotA3X22AN6W962AW4oQrIC6Vy187h3hXGez9upTenaqyHuQzCHCWYFlV0aJoSdXXK%2BSNxu8QOFVRKutxv2ZcojHT8fZE9MkUZYB4m%2B094bp%2BMAQ9DgcoZyMaGbDOzF4p7BDMSzuWioDuw4oXoX%2FLWeo%2BoYjzirNlEibjHQ; optimizelyEndUserId=oeu1473684041957r0.6113794356256115; mbdc=AE91C60A.4C0E.5526.01C6.42BA96F7283E; cnx_sa=1; optimizelySegments=%7B%22656580285%22%3A%22none%22%2C%22656580286%22%3A%22gc%22%2C%22658230792%22%3A%22false%22%2C%22659760039%22%3A%22search%22%7D; optimizelyBuckets=%7B%7D; LOCALEID=en%5FUS; visits=2; _ga=GA1.2.2090161550.1473684072; mbcc=22975E8D-053D-58C3-044E-A0B74BA03F60; _ceg.s=ode8ot; _ceg.u=ode8ot; cnx_sid=720232357240974480; cnx_start=1473687679796; cnx_rid=1473687680320407147; cnx_views=1; cnx_pg=1473687679796; rCookie=q5lz390jw7izs6vpni1u81akqskav2; lastVisitURL=http://www.footlocker.com/";

           CookieCollection initCookie = CovertToCookies(rawCookies);

            initialRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            initialRequest.CookieContainer = new CookieContainer();
            initialRequest.CookieContainer.Add(new Uri(base_url),initCookie);
            initialRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            initialRequest.Headers.Add("DNT", "1");
            initialRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            initialRequest.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            initialRequest.Headers.Add("Accept-Language", "en-US,en;q=0.8,da;q=0.6");
            initialRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            initialRequest.Method = "GET";
            initialRequest.Credentials = CredentialCache.DefaultCredentials;
            initialRequest.CookieContainer = new CookieContainer();
            System.Net.HttpWebResponse initialResponse = (HttpWebResponse)initialRequest.GetResponse();

            var initialCookies = initialResponse.Cookies;

            initialResponse.Dispose();


            string url = "http://www.footlocker.com/product/model:190074/sku:55088011/jordan-retro-1-high-og-mens/black/white/?cm=";
            //string url = "http://www.footlocker.com/product/model:201111/sku:54861001/nike-sb-stefan-janoski-boys-grade-school/black/white/?cm=";
            string size = "7.0";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            request.Headers.Add("DNT", "1");
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            request.Headers.Add("Accept-Language","en-US,en;q=0.8,da;q=0.6");
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";



            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                var setcookie = response.Headers.Get("Set-Cookie");
                var cookies = request.Headers.AllKeys;
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.

                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        // Read the content.
                        string responseFromServer = reader.ReadToEnd();
                        // Display the content.
                        //Console.WriteLine(responseFromServer);

                        var doc = new HtmlDocument();
                        doc.LoadHtml(responseFromServer);

                        HtmlNode pdp_selectedSKU = doc.GetElementbyId("pdp_selectedSKU");
                        var realSkuValue = pdp_selectedSKU.GetAttributeValue("value", string.Empty);
                        HtmlNode pdp_model = doc.GetElementbyId("pdp_model");
                        var realModelValue = pdp_model.GetAttributeValue("value", string.Empty);
                        HtmlNode requestKey = doc.GetElementbyId("requestKey");
                        var realRequestKeyValue = requestKey.GetAttributeValue("value", string.Empty);

                        string postUrl = "http://www.footlocker.com/catalog/miniAddToCart.cfm?secure=0";

                        request.Headers.Clear();

                        request = (HttpWebRequest)WebRequest.Create(postUrl);
                        request.Method = "POST";
                        request.Credentials = CredentialCache.DefaultNetworkCredentials;
                        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                        //request.ContentType = "application/x-www-form-urlencoded";
                        //request.Headers.Add("Cookie", setcookie);

                        //string setcookie.Split(new char[]{ ';' });

                        Dictionary<string, string> payload = CreatePayload(realRequestKeyValue, realSkuValue, realModelValue);
                        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
                        request.Accept = "*/*";
                        request.Headers.Add("Origin", "http://www.footlocker.com");
                        request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                        request.Referer = url;
                        request.Headers.Add("Accept-Encoding", "gzip, deflate");
                        request.Headers.Add("Accept-Language", "en-US,en;q=0.8");
                        request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                        request.Host = "www.footlocker.com";
                        request.KeepAlive = true;


                        request.CookieContainer = new CookieContainer();
                        request.CookieContainer.Add(new Uri("http://www.footlocker.com"), initialCookies);

                        string postData = "";

                        foreach (string key in payload.Keys)
                        {
                            postData += HttpUtility.UrlEncode(key) + "="
                                  + HttpUtility.UrlEncode(payload[key]) + "&";
                        }

                        postData += "inlineAddToCart=1";
                        byte[] data = Encoding.ASCII.GetBytes(postData);


                        Stream requestStream2 = request.GetRequestStream();
                        requestStream2.Write(data, 0, data.Length);
                        requestStream2.Close();

                        HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();

                        Stream responseStream = myHttpWebResponse.GetResponseStream();

                        StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                        string pageContent = myStreamReader.ReadToEnd();

                        myStreamReader.Close();
                       // responseStream.Close();

                        myHttpWebResponse.Close();

                    }
                }
            }
            Console.Read();
        }

        static CookieCollection CovertToCookies(string text)
        {
            CookieCollection list = new CookieCollection();

            var cookieLongString = text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in cookieLongString)
            {
                string[] keyValue = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length == 2) {
                    Cookie c = new Cookie(keyValue.First().Trim(), HttpUtility.UrlEncode(keyValue.Last().Trim()));
                    list.Add(c);
                }
            }
            return list;
        }

        static Dictionary<string, string> CreatePayload(string realRequestKeyValue, string realSkuValue, string realModelValue)
        {
            Dictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("requestKey", realRequestKeyValue);
            payload.Add("qty", "1");
            payload.Add("size", "10.0");
            payload.Add("the_model_nbr", realModelValue);
            payload.Add("sku", realSkuValue);
            payload.Add("storeNumber", "00000");
            payload.Add("fulfillmentType", "SHIP_FROM_STORE");
            payload.Add("storeCostOfGoods", "0.00");
            payload.Add("inlineAddToCart", "0");
            payload.Add("coreMetricsCategory", "blank");
            payload.Add("hasXYPromo", "false");
            payload.Add("BV_TrackingTag_Review_Display_Sort", string.Format("http://footlocker.ugc.bazaarvoice.com/8001/{0}/reviews.djs?format=embeddedhtml", realSkuValue));
            payload.Add("BV_TrackingTag_QA_Display_Sort", string.Empty);
            payload.Add("rdo_deliveryMethod", "shiptohome");
            //payload.Add("inlineAddToCart", "1");
            
            
            
            
            

            return payload;
        }
    }
}
