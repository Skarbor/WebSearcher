﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebSearcher.Collector
{
    public interface IWebPageUrlChecker
    {
        bool CheckIfPageIsWorking(string pageUrl);
        Task<bool> CheckIfPageIsWorkingAsync(string pageUrl);
    }
}
