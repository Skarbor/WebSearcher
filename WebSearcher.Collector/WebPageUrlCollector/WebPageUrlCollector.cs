using System.Threading;
using WebSearcher.Common;
using WebSearcher.Common.Logger;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Concrete;
using WebSearcher.Entities;

namespace WebSearcher.Collector.WebPageUrlCollector
{
    public class WebPageUrlCollector : ICollector
    {
        private readonly IWebPageUrlGenerator _urlGenerator;
        private readonly IWebPageUrlChecker _urlChecker;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly ILogger _logger = new Logger();

        public WebPageUrlCollector() : this(new WebPageUrlGenerator(), new WebPageUrlChecker(), new UnitOfWorkFactory())
        {}

        public WebPageUrlCollector(IWebPageUrlGenerator webPageUrlGenerator, IWebPageUrlChecker webPageUrlChecker, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _urlGenerator = webPageUrlGenerator;
            _urlChecker = webPageUrlChecker;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        private async void TryRandomWebpage()
        {
            var randomWebPage = _urlGenerator.GenerateRandomUrl();

            var isWebPageWorking = await _urlChecker.CheckIfPageIsWorkingAsync(randomWebPage);

            if (isWebPageWorking)
            {
                _logger.Debug($"Found working webpage with url: {randomWebPage}");

                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    if (unitOfWork.WebPages.Find(webPage => webPage.Url == randomWebPage) == null)
                    {
                        unitOfWork.WebPages.Add(new WebPage() { Url = randomWebPage });
                        unitOfWork.Save();
                    }
                }
            }
            else
            {
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
