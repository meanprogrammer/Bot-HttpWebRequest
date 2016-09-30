using HtmlAgilityPack;
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

            string rawCookies = "TID=5555%2D29121607402129070918580%2D0; TRACK_USER_P=29031291216074029070967883; USER_PROFILE=enw6maPKkWi63kKVcW5m4tPeCZotA3X22AN6W962AW4oQrIC6Vy187h3hXGez9upTenaqyHuQzCHCWYFlV0aJoSdXXK%2BSNxu8QOFVRKutxv2ZcojHT8fZE9MkUZYB4m%2B094bp%2BMAQ9DgcoZyMaGbDOzF4p7BDMSzuWioDuw4oXoX%2FLWeo%2BoYjzirNlEibjHQ; optimizelyEndUserId=oeu1473684041957r0.6113794356256115; mbdc=AE91C60A.4C0E.5526.01C6.42BA96F7283E; cnx_sa=1; SSLC=web%2D15; DOTOMI_SESSION=1; CHOSEN_BANNER=1; CHOSEN_BANNER_ID=; BROWSER_SESSION=enw6maPKkWiWMYtmf8cr%2F1pXol9%2Fok9yV5uCb1sQOupqQVitDbTrpaI23u964b9pdMi5iT11Nd4RCH4SDQRxey5JN45YavNz89ZjvJHPuf0RCH4SDQRxe11Zet22S9EK%2BJH1b6exHvvUzfJnED0nn79HfpKRzULkSOXkPQvZJpRWQGRXEBn2%2FjlCOEnQ3nDcrAdpEXdo9VLZazaMo6zcz5letSoCzj2McsKo%2Bc2hSAr7oDdDt4XSjNEadH3rLvvZaLNkAXpLlyYHdBzbU55woMNMdGDWtaV4L1LDEgYBifmfKFLJe0o7zsIm3KiQklboHc%2FelScjhZ5oJl%2B0a1VVk8FrKh9Nn4B0Bbqi0Rubso0p6zWM5H5N3hbyRtWyZKGStb5g1vr3WsbTsPcaunE0%2B%2FatcYKuCniSZZtqxwfyj7FKXeknacNlOTbrTTRT2EghhxV9I88b7aV3HZTMGJ5UgzICjVbKdOO5tznrBmBfPxD%2B6jK8W7jx0g%3D%3D; SSLB=0; mdr_browser=desktop; cmTPSet=Y; QSI_S_ZN_3PMUUOL1V08evMF=v:100:0; cnx_sid=120231151042057186; cnx_start=1474770386066; cnx_rid=1474770386310403756; QSI_HistorySession=http%3A%2F%2Fwww.footlocker.com%2F~1474770428888%7Chttp%3A%2F%2Fwww.footlocker.com%2FShoes%2F_-_%2FN-rjZ1z141xe~1474770439880%7Chttp%3A%2F%2Fwww.footlocker.com%2F_-_%2Fkeyword-jordan%2Bretro~1474770540870%7Chttp%3A%2F%2Fwww.footlocker.com%2FMens%2F_-_%2FN-24%2Fkeyword-jordan%2Bretro%3Fcm_REF%3DMen%2527s%26Nr%3DAND%2528P%255FRecordType%253AProduct%2529~1474770638298; rCookie=q5lz390jw7izs6vpni1u81akqskav2; cnx_exit=true; lastVisitURL=http://www.footlocker.com/Mens/_-_/N-24/keyword-jordan+retro?cm_REF=Men%27s&Nr=AND%28P%5FRecordType%3AProduct%29; RECENTSKULIST=19176001%3A255653%3AMain; INLINECARTSUMMARY=0%2C0; optimizelySegments=%7B%22656580285%22%3A%22none%22%2C%22656580286%22%3A%22gc%22%2C%22658230792%22%3A%22false%22%2C%22659760039%22%3A%22search%22%7D; optimizelyBuckets=%7B%7D; _ga=GA1.2.2090161550.1473684072; _gat=1; RECENTNAMELIST=Jordan%20AJ%201%20Retro%20High%20Nouveau%20-%20Men's; visits=2; mbcs=6FE1FCF4-587E-5B2F-BB7B-F9183AF1B596; mbcc=22975E8D-053D-58C3-044E-A0B74BA03F60; xyz_cr_100238_et_111==undefined&cr=100238&et=111&ap=undefined; _ceg.s=oe1gl7; _ceg.u=oe1gl7; cnx_views=5; cnx_pg=1474771005551; optimizelyPendingLogEvents=%5B%5D; LOCALEID=en%5FUS";

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
                        request.CookieContainer.Add(new Uri("http://www.footlocker.com"), CovertToCookies(rawCookies));

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
                    Cookie c = new Cookie(keyValue.First().Trim(), keyValue.Last().Trim());
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
