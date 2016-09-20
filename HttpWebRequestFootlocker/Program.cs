using HtmlAgilityPack;
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
            string url = "http://www.footlocker.com/product/model:190074/sku:55088011/jordan-retro-1-high-og-mens/black/white/?cm=";
            string size = "9";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            request.Headers.Add("DNT", "1");
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            request.Headers.Add("Accept-Language","en-US,en;q=0.8,da;q=0.6");
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";



            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

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

                        string postUrl = "http://www.footlocker.com/catalog/miniAddToCart.cfm?secure=0&";

                        HttpWebRequest postRequest = (HttpWebRequest)WebRequest.Create(postUrl);
                        postRequest.Method = "POST";
                        postRequest.Credentials = CredentialCache.DefaultCredentials;
                        postRequest.ContentType = "application/x-www-form-urlencoded";

                        Dictionary<string, string> payload = CreatePayload(realRequestKeyValue, realSkuValue, realModelValue);

                        postRequest.Accept = "*/*";
                        postRequest.Headers.Add("Origin", "http://www.footlocker.com");
                        postRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
                        postRequest.Referer = url;
                        postRequest.Headers.Add("Accept-Encoding", "gzip, deflate");

                        string postData = "";

                        foreach (string key in payload.Keys)
                        {
                            postData += HttpUtility.UrlEncode(key) + "="
                                  + HttpUtility.UrlEncode(payload[key]) + "&";
                        }

                        byte[] data = Encoding.ASCII.GetBytes(postData);


                        Stream requestStream2 = postRequest.GetRequestStream();
                        requestStream2.Write(data, 0, data.Length);
                        requestStream2.Close();

                        HttpWebResponse myHttpWebResponse = (HttpWebResponse)postRequest.GetResponse();

                        Stream responseStream = myHttpWebResponse.GetResponseStream();

                        StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                        string pageContent = myStreamReader.ReadToEnd();

                        myStreamReader.Close();
                        responseStream.Close();

                        myHttpWebResponse.Close();

                    }
                }
            }
            Console.Read();
        }


        static Dictionary<string, string> CreatePayload(string realRequestKeyValue, string realSkuValue, string realModelValue)
        {
            Dictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("BV_TrackingTag_QA_Display_Sort", string.Empty);
            payload.Add("BV_TrackingTag_Review_Display_Sort", string.Format("http://footlocker.ugc.bazaarvoice.com/8001/{0}/reviews.djs?format=embeddedhtml", realSkuValue));
            payload.Add("coreMetricsCategory", "blank");
            payload.Add("fulfillmentType", "SHIP_TO_HOME");
            payload.Add("hasXYPromo", "false");
            payload.Add("inlineAddToCart", "0,1");
            payload.Add("qty", "1");
            payload.Add("rdo_deliveryMethod", "shiptohome");
            payload.Add("requestKey", realRequestKeyValue);
            payload.Add("size", "10.0");
            payload.Add("sku", realSkuValue);
            payload.Add("storeCostOfGoods", "0.00");
            payload.Add("storeNumber", "00000");
            payload.Add("the_model_nbr", realModelValue);

            return payload;
        }
    }
}
