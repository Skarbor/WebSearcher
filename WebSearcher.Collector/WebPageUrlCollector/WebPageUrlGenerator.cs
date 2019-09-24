using System;
using System.Text;

namespace WebSearcher.Collector.WebPageUrlCollector
{
    public class WebPageUrlGenerator : IWebPageUrlGenerator
    {
        public string GenerateRandomUrl()
        {
            var randomizer = new Random();

            int randomLength = randomizer.Next(3, 20);

            var randomUrl = new StringBuilder();

            var minValueOfUrlCharacter = (int)'a';
            var maxValueOfUrlCharacter = (int)'z';

            for (int i = 0; i < randomLength; i++)
            {
                var character = (char)randomizer.Next(minValueOfUrlCharacter, maxValueOfUrlCharacter);

                randomUrl.Append(character);
            }

            return $"http://www.{randomUrl}.pl";
        }
    }
}
