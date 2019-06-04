using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebSearcher.App
{
    public class WebPagesUrlFinder
    {
        public string GetRandomUrl()
        {
            return "http://www.interia.pl";
        }

        public string GenerateRandomUrl()
        {
            var randomizer = new Random();

            int randomLenght = randomizer.Next(3, 20);

            var randomUrl = new StringBuilder();

            var minValueOfUrlCharacter = (int)'a';
            var maxValueOfUrlCharacter = (int)'z';

            for (int i = 0; i < randomLenght; i++)
            {
                var character = (char) randomizer.Next(minValueOfUrlCharacter, maxValueOfUrlCharacter);

                randomUrl.Append(character);
            }

            return $"http://www.{randomUrl}.pl";
        }

        public async Task<bool> CheckUrl(string url)
        {
            try
            {
                Console.WriteLine($"Checking url: {url}");
                var req = WebRequest.Create(url);

                WebResponse res = await req.GetResponseAsync();
                Console.WriteLine($"correct correct correct url: {url}");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"wrong url: {url}");
                return false;
            }
        }

        public async Task ttt()
        {

            var url = GenerateRandomUrl();
            
            var isUrlCorrect = await CheckUrl(url);
        }
    }
}
