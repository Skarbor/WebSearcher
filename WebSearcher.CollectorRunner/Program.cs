using System.Collections.Generic;
using System.Threading;
using WebSearcher.Collector;
using WebSearcher.Collector.WebPageSubPagesCollector;
using WebSearcher.Collector.WebPageUrlCollector;

namespace WebSearcher.CollectorRunner
{
    class Program
    {
        static void Main()
        {
            var collectors = new List<ICollector> { new WebPageUrlCollector(), new WebPageSubPagesCollector()};
            
            foreach (var collector in collectors)
            {
                var t = new Thread(collector.Start);
                t.Start();
            }
        }
    }
}
