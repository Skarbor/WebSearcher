using System;
using System.Collections.Generic;
using System.Text;

namespace WebSearcher.Collector.WebPageUrlCollector
{
    public class WebPageUrlGenerator : IWebPageUrlGenerator
    {
        public string GenerateRandomUrl()
        {
            var randomizer = new Random();

            int randomLenght = randomizer.Next(3, 20);

            var randomUrl = new StringBuilder();

            var minValueOfUrlCharacter = (int)'a';
            var maxValueOfUrlCharacter = (int)'z';

            for (int i = 0; i < randomLenght; i++)
            {
                var character = (char)randomizer.Next(minValueOfUrlCharacter, maxValueOfUrlCharacter);

                randomUrl.Append(character);
            }

            return $"http://www.{randomUrl}.pl";
        }
    }
}
