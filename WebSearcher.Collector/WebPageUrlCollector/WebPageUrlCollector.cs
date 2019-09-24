using System.Threading;
using WebSearcher.Collector.Synchronizer;
using WebSearcher.Common;
using WebSearcher.Common.Logger;

namespace WebSearcher.Collector.WebPageUrlCollector
{
    public class WebPageUrlCollector : ICollector
    {
        private readonly IWebPageUrlGenerator _urlGenerator;
        private readonly IWebPageUrlChecker _urlChecker;
        private readonly ILogger _logger = new Logger();
        private readonly WebPageDataSynchronizer _webPageDataSynchronizer;

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
                _logger.Debug($"Found working webpage with url: {randomWebPage}");
                _webPageDataSynchronizer.AddIfUnique(new Entities.WebPage() { Url = randomWebPage });
            }
            else {
                _logger.Debug($"Url: {randomWebPage} not wokring");
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
