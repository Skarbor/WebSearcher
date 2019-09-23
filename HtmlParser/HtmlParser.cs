using HtmlParser.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HtmlParser
{
    public class HtmlParser
    {
        public HtmlDocument Parse(string htmlContent)
        {
            var document = new HtmlDocument();

            document.Links = GetLinks(htmlContent);

            return document;
        }

        private IList<Link> GetLinks(string htmlContent)
        {
            string hrefGroupName = "href";
            string getLinksHrefRegex = "<a href=['\"](?<href>h?t?t?p?s?:?/?/?[a-zA-Z-0-9.]*)['\"]";
            Regex rx = new Regex(getLinksHrefRegex);
            var x = rx.Matches(htmlContent);

            var links = new List<Link>();
            foreach (Match match in x)
            {
                foreach (Group group in match.Groups[hrefGroupName].Captures)
                {
                    links.Add(new Link() { Url = group.Value });
                }
            }

            return links;
        }
    }
}
