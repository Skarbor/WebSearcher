using System.Collections.Generic;
using System.Threading;
using WebSearcher.Collector;
using WebSearcher.Collector.WebPageSubPagesCollector;

namespace WebSearcher.CollectorRunner
{
    class Program
    {
        static void Main()
        {
            var collectors = new List<ICollector> {new WebPageSubPagesCollector()};
            //collectors.Add(new WebPageUrlCollector());

            foreach (var collector in collectors)
            {
                var t = new Thread(collector.Start);
                t.Start();
            }
        }
    }
}
