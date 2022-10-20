using FinalProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


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
                Console.WriteLine("What is the ID of the disc that you would like to look at? If you want to see the catalog, type 0.");
                var ID = int.Parse(Console.ReadLine());

                if (ID > 1 && ID <= 970)
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

                        var parsedBody = JObject.Parse(body);
                        var disc = parsedBody.GetValue("disc");

                        var discClass = JsonConvert.DeserializeObject<Disc>(disc.ToString());

                        //Console.WriteLine(body); old way of priniting information. Lots of unnecessary info. 
                        Console.WriteLine($"Disc ID: {discClass.id}");
                        Console.WriteLine($"Disc Name: {discClass.name}");
                        Console.WriteLine($"Disc Speed: {discClass.speed}");
                        Console.WriteLine($"Disc Glide: {discClass.glide}");
                        Console.WriteLine($"Disc Turn: {discClass.turn}");
                        Console.WriteLine($"Disc Fade: {discClass.fade}");
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
                if (ID < 0 || ID >= 971 || ID == 1)
                {
                    Console.WriteLine("Invlaid Input");
                    Console.WriteLine("");
                }

                if (ID == 0)
                {
                    for (int i = 0; i <= 971; i++)
                    {
                        int Id = 0 + i;
                        Console.WriteLine("");
                        var client = new HttpClient();
                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri($"https://disc-golf-discs.p.rapidapi.com/discs/{Id}"),
                            Headers =
                            {
                                { "X-RapidAPI-Key", "a16be5af85mshe17ecebc6f2ce6fp12ad7ajsnab739c86fb33" },
                                { "X-RapidAPI-Host", "disc-golf-discs.p.rapidapi.com" },
                            },
                        };
                        
                        using (var response = await client.SendAsync(request))
                        {
                            if (response == null || !response.IsSuccessStatusCode)
                            {
                                continue;
                            }

                            var body = await response.Content.ReadAsStringAsync();

                            var parsedBody = JObject.Parse(body);
                            var disc = parsedBody.GetValue("disc");


                            var discCatalog = JsonConvert.DeserializeObject<Catalog>(disc.ToString());
                            List<Catalog> discList = new List<Catalog>();
                            discList.Add(new Catalog() { id = discCatalog.id, name = discCatalog.name });

                            foreach (var item in discList)
                            {
                                Console.WriteLine($"ID: {discCatalog.id}");
                                Console.WriteLine($"Name: {discCatalog.name}");
                            }
                        }
                    }
                }
            }
        }
    }
}