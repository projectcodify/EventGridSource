using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid;
using Newtonsoft.Json;

namespace EventGridSource
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var sasKey = "DPaoQOpbq3UZdhSZUSLKNPKvxkZ1W6Ec1vUKXuXMmec=";
            var topicEndpoint = "https://my-test-events.westus2-1.eventgrid.azure.net/api/events";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("aeg-sas-key", sasKey);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("democlient");

            List<GridEvent<object>> eventList = new List<GridEvent<object>>();

            for (int i = 0; i < 5; i++)
            {
                GridEvent<object> testEvent = new GridEvent<object>
                {
                    Subject = $"Event {i}",
                    EventType = (i % 2 == 0) ? "allEvents" : "filteredEvent",
                    EventTime = DateTime.UtcNow,
                    Id = Guid.NewGuid().ToString()
                };
                eventList.Add(testEvent);
            }

            string json = JsonConvert.SerializeObject(eventList);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, topicEndpoint)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await client.SendAsync(request);
        }
    }
}
