using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebSearcher.Collector;
using WebSearcher.DataAccess.Concrete;

namespace WebSearcher.App
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly WebPagesUrlFinder webUrlbuilder = new WebPagesUrlFinder();
        private static readonly WebPagesUrlFinder webPagesUrlFinder = new WebPagesUrlFinder();
        private static WebPageDataSynchronizer synchronizer = new WebPageDataSynchronizer();

        static void Main(string[] args)
        {
            WebPageUrlCollector collector = new WebPageUrlCollector();

            collector.Start();

            Console.ReadKey();
        }
    }
}
