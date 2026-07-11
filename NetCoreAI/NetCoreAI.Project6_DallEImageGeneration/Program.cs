using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

class Program
{
    public static async Task Main(string[] args)
    {
        string apiKey = "your_api_key";
        Console.Write("Example prompts: ");
        string prompt = Console.ReadLine();
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var requestBody = new
            {
                model = "openai/dall-e-3",
                prompt = prompt,
                size = "1024x1024"
            };
            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://openrouter.ai/api/v1/images/generations", content);
            string responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }
    }
}
