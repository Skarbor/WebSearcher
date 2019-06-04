using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebSearcher.Collector
{
    public class WebPageUrlChecker : IWebPageUrlChecker
    {
        public bool CheckIfPageIsWorking(string pageUrl)
        {
            try
            {
                var req = WebRequest.Create(pageUrl);
                WebResponse res = req.GetResponse();
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
                WebResponse res = await req.GetResponseAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
