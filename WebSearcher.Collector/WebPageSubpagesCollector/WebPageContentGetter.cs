using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WebSearcher.Collector.WebPageSubpagesCollector
{
    public class WebPageContentGetter
    {
        public string GetWebpageContent(string webPageUrl)
        {
            WebRequest webRequest = WebRequest.Create(webPageUrl);

            WebResponse response = webRequest.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);

                return reader.ReadToEnd();
            }
        }
    }
}
