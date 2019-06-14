using System.IO;
using System.Net;

namespace WebSearcher.Collector.WebPageUrlCollector
{
    public class WebPageContentGetter : IWebPageContentGetter
    {
        public string GetWebPageContent(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);

            WebResponse response = webRequest.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);

                return reader.ReadToEnd();
            }
        }
    }
}
