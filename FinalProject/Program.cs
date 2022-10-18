using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace finalProject
{
    public class DiscType
    {
        private static object ID;

        public static async Task Main(string[] args)
        {
            bool answer = true;
            while (answer)
            {
                Console.WriteLine("What is the Id of the disc that you would like to look at?");
                int ID = int.Parse(Console.ReadLine());

                if (ID <= 970)
                {
                    Console.WriteLine("");
                    var client = new HttpClient();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"https://disc-golf-discs.p.rapidapi.com/discs/{ID}"),
                        Headers =
                        {
                    { "X-RapidAPI-Key", "a16be5af85mshe17ecebc6f2ce6fp12ad7ajsnab739c86fb33" },
                    { "X-RapidAPI-Host", "disc-golf-discs.p.rapidapi.com" },
                        },
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(body);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Would you like to check another disc?");
                    var userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        answer = true;
                        Console.WriteLine("");
                    }
                    else { answer = false; }
                }
                else { Console.WriteLine("Invlaid Input"); }
            }
        }
    }
}