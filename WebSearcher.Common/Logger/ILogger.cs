﻿using System;

namespace WebSearcher.Common.Logger
{
    public interface ILogger
    {
        void Info(string message);
        void Debug(string message);
        void Error(string message);
        void Error(Exception exception);
        void Error(string message, Exception exception);
    }
}
