using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlParser.Tests
{
    [TestFixture]
    public class HtmlParserTests
    {
        [Test]
        public void GetLinks_ShouldReturnSingleLink_ForHtmlContainingOneLinkInSingleQuote()
        {
            const string htmlContent = "<div><a href='www.google.com'>google</a></div>";
            var sut = new HtmlParser();

            var result = sut.Parse(htmlContent);

            Assert.AreEqual(result.Links[0].Url, "www.google.com");
        }

        [Test]
        public void GetLinks_ShouldReturnSingleLink_ForHtmlContainingOneLinkInDoubleQuote()
        {
            const string htmlContent = "<div><a href=\"www.google.com\">google</a></div>";
            var sut = new HtmlParser();

            var result = sut.Parse(htmlContent);

            Assert.AreEqual(result.Links[0].Url, "www.google.com");
        }

        [Test]
        public void GetLinks_ShouldReturnTwoLink_ForHtmlContainingOneLinkInDoubleQuoteAndOneLinkInSingleQuote()
        {
            const string htmlContent = "<div><a href=\"www.google.com\">google</a><a href='www.google.com'>google</a></div>";
            var sut = new HtmlParser();

            var result = sut.Parse(htmlContent);

            Assert.AreEqual(result.Links.Count,2);
        }
    }
}
