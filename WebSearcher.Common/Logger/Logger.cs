using System;
using System.Collections.Generic;
using System.Text;

namespace WebSearcher.Common.Logger
{
    public class Logger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }
    }
}
