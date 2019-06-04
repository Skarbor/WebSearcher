using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WebSearcher.Collector
{
    public class WebPageUrlCollector
    {
        private IWebPageUrlGenerator _urlGenerator { get; set; }
        private IWebPageUrlChecker _urlChecker { get; set; }
        private WebPageDataSynchronizer _webPageDataSynchronizer { get; set; }
        public WebPageUrlCollector() : this(new WebPageUrlGenerator(), new WebPageUrlChecker())
        {}

        public WebPageUrlCollector(IWebPageUrlGenerator webPageUrlGenerator, IWebPageUrlChecker webPageUrlChecker)
        {
            _urlGenerator = webPageUrlGenerator;
            _urlChecker = webPageUrlChecker;
            _webPageDataSynchronizer = new WebPageDataSynchronizer();
        }

        private async void TryRandomWebpage()
        {
            var randomWebPage = _urlGenerator.GenerateRandomUrl();

            var isWebPageWorking = await _urlChecker.CheckIfPageIsWorkingAsync(randomWebPage);

            if (isWebPageWorking)
            {
                Console.WriteLine($"Found working webpage with url: {randomWebPage}");
                _webPageDataSynchronizer.AddIfUniqe(new Entities.WebPage() { Url = randomWebPage });
            }
            else {
                Console.WriteLine($"Url: {randomWebPage} not wokring");
            }
        }

        public void Start()
        {
            while (true)
            {
                TryRandomWebpage();
                Thread.Sleep(20);
            }    
        }
    }
}
