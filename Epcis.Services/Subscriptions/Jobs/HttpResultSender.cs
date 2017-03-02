using System.Diagnostics;
using System.IO;
using System.Net;
using Epcis.Infrastructure.Aop.Log;

namespace Epcis.Services.Subscriptions.Jobs
{
    public class HttpResultSender : IResultSender
    {
        [LogMethodCall]
        public virtual void SendResults<T>(string destination, T results)
        {
            var webRequest = WebRequest.Create(destination);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml";

            using (var sw = new StreamWriter(webRequest.GetRequestStream()))
            {
                sw.Write(results);
            }

            var response = webRequest.GetResponse();
            EnsureIsSuccess((HttpWebResponse)response);
        }

        // TODO: define what to do when response is not successful
        private static void EnsureIsSuccess(HttpWebResponse response)
        {
            if (response.StatusCode < HttpStatusCode.OK || response.StatusCode >= HttpStatusCode.Ambiguous)
            {
                Debug.Write("Response code does not indicate success.");
            }
        }
    }
}