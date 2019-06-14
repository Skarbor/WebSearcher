using System;
using System.Collections.Generic;
using System.Text;

namespace WebSearcher.Collector.WebPageUrlCollector
{
    interface IWebPageContentGetter
    {
        string GetWebPageContent(string url);
    }
}
