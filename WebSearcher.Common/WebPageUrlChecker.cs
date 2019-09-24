using System;
using System.Net;
using System.Threading.Tasks;

namespace WebSearcher.Common
{
    public class WebPageUrlChecker : IWebPageUrlChecker
    {
        public bool CheckIfPageIsWorking(string pageUrl)
        {
            try
            {
                var req = WebRequest.Create(pageUrl);
                req.GetResponse();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> CheckIfPageIsWorkingAsync(string pageUrl)
        {
            try
            {
                var req = WebRequest.Create(pageUrl);
                await req.GetResponseAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
