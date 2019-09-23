using System;
using System.Collections.Generic;
using System.Text;

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
