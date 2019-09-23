using System;

namespace WebSearcher.Common.Logger
{
    public class Logger : ILogger
    {
        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }
    }
}
