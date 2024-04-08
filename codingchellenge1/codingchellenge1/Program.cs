using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main()
    {
        string Url = "https://coderbyte.com/api/challenges/json/age-counting";
        using (HttpClient httpClient = new HttpClient())
        {
         
            HttpResponseMessage response = await httpClient.GetAsync(Url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                int count = CountItems(jsonResponse);

                Console.WriteLine($"Number of items with age >= 50: {count}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

    }

    static int CountItems(string jsonResponse)
    {
    
        int count = 0;

        JObject json = JObject.Parse(jsonResponse);
        string dataValue = json["data"].ToString();

   
        string[] items = dataValue.Split(',');

        foreach (var item in items)
        {
            int age;
            if (int.TryParse(item.Split('=')[1], out age))
            {
                if (age >= 50)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
