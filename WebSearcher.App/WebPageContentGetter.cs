using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WebSearcher.App
{
    public class WebPageContentGetter
    {
        private WebPagesUrlFinder _webUrlBuilder;

        public WebPageContentGetter() : this(new WebPagesUrlFinder())
        {}

        public WebPageContentGetter(WebPagesUrlFinder webUrlBuilder)
        {
            _webUrlBuilder = webUrlBuilder;
        }

        public string GetContentOfWebpage()
        {
            WebRequest webRequest = WebRequest.Create(_webUrlBuilder.GetRandomUrl());

            WebResponse response = webRequest.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);

                return reader.ReadToEnd();
            }
        }
    }
}
