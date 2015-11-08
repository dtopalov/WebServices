namespace ConsumingWebServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    using Newtonsoft.Json;

    class JsonPlaceholderConsumer
    {
        static async void PrintTodos(HttpClient httpClient, int numberOfItems, string query)
        {
            var response = await httpClient.GetAsync("todos");

            var text = response.Content.ReadAsStringAsync().Result;
            var jsons = JsonConvert.DeserializeObject<List<Todo>>(text);
            var filtered = jsons.Where(x => x.Title.Contains(query)).Take(numberOfItems);

            Console.WriteLine("Items:\n" + string.Join(Environment.NewLine, filtered));
        }

        static void Main()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://jsonplaceholder.typicode.com");

            Console.Write("Enter number of items: (set to 5 by default) ");
            int numberOfItems;
            bool validNumber = int.TryParse(Console.ReadLine(), out numberOfItems);
            if (!validNumber)
            {
                numberOfItems = 5;
            }

            Console.Write("Enter query string: (set to 'harum' by default): ");
            string queryString = Console.ReadLine();
            if (string.IsNullOrEmpty(queryString))
            {
                queryString = "harum";
            }

            PrintTodos(httpClient, numberOfItems, queryString);
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }
    }
}
