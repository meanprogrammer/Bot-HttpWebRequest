using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestFootlocker
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://www.footlocker.com/product/model:190074/sku:55088011/jordan-retro-1-high-og-mens/black/white/?cm=";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            request.Headers.Add("DNT", "1");
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            request.Headers.Add("Accept-Language","en-US,en;q=0.8,da;q=0.6");
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            WebResponse response = request.GetResponse();
            var referrer = request.Referer;
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.

            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.
            reader.Close();
            response.Close();

            Console.Read();
        }
    }
}
