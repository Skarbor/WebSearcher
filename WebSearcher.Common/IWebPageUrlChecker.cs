using System.Threading.Tasks;

namespace WebSearcher.Common
{
    public interface IWebPageUrlChecker
    {
        bool CheckIfPageIsWorking(string pageUrl);
        Task<bool> CheckIfPageIsWorkingAsync(string pageUrl);
    }
}
