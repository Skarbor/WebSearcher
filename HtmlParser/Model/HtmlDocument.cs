using System.Collections.Generic;

namespace HtmlParser.Model
{
    public class HtmlDocument
    {
        public IList<Link> Links { get; set; }

        public HtmlDocument()
        {
            Links = new List<Link>();
        }
    }
}
