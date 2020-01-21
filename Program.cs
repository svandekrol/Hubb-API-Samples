using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIPagingSample
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        { 
            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var pageSize = 100;
            var currentPage = 0;
            var eventId = "528f6ea8-edf9-4fbd-b5d7-f028a25bf3f7";
            var subscriptionKey = config["SubscriptionKey"];
            
            int elementCount = pageSize;
            while(elementCount == pageSize)
            {
                var requestUri = new Uri($"https://eventapi.hubb.me/v1/Users?HubbEventId={eventId}&$expand=SpeakingAt&$top={pageSize}&$skip={currentPage}");
                using(var request = new HttpRequestMessage{RequestUri = requestUri})
                {
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                    using(var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();

                        var json = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());
                        elementCount = json.EnumerateArray().Count();
                        Console.WriteLine($"{elementCount} users were found");

                        currentPage = currentPage + pageSize;
                    }
                }
            }
        }
    }
}