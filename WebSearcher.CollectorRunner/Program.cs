using System;
using System.Collections.Generic;
using System.Threading;
using WebSearcher.Collector;
using WebSearcher.Collector.WebPageSubpagesCollector;
using WebSearcher.Collector.WebPageUrlCollector;

namespace WebSearcher.CollectorRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var collectors = new List<ICollector>();
            collectors.Add(new WebPageUrlCollector());
            collectors.Add(new WebPageSubpagesCollector());

            foreach (var collector in collectors)
            {
                Thread t = new Thread(collector.Start);
                t.Start();
            }
        }
    }
}
