using System.Threading;
using WebSearcher.Collector.Synchronizer;
using WebSearcher.Common.Logger;

namespace WebSearcher.Collector.WebPageUrlCollector
{
    public class WebPageUrlCollector : ICollector
    {
        private IWebPageUrlGenerator _urlGenerator { get; set; }
        private IWebPageUrlChecker _urlChecker { get; set; }
        protected readonly ILogger _logger = new Logger();
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
