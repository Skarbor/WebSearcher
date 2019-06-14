using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlParser.Model
{
    public class HtmlDocument
    {
        public IEnumerable<Link> Links { get; }

        public HtmlDocument()
        {
            Links = new List<Link>();
        }
    }
}
