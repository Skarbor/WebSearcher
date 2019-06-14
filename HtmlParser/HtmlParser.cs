using HtmlParser.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlParser
{
    public static class HtmlParser
    {
        public static HtmlDocument Parse(string htmlContent)
        {
            var document = new HtmlDocument();

            GetLinks(htmlContent);

            return null;
        }

        private static IEnumerable<Link> GetLinks(string htmlContent)
        {
            Regex rx = new Regex(@"\ba href=\b");
            var x = rx.GetGroupNames();

            return null;
        }
    }
}
