using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var apiKey = "buraya apikey yazılacak";
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        var messages = new List<object> { new { role = "system", content = "You are a helpful assistant" } };
        Console.WriteLine("Lütfen sorunuzu buraya yazınız. (Çıkış için 'exit')");

        while (true)
        {
            Console.Write("\nSen: ");
            var prompt = Console.ReadLine();
            if (prompt?.ToLower() == "exit") break;

            messages.Add(new { role = "user", content = prompt });
            var requestBody = new { model = "openrouter/free", messages };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                    var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                    Console.Write("\nAI'nin Cevabı: ");
                    Console.WriteLine(answer);
                }
                else
                {
                    Console.WriteLine($"Bir hata oluştu: {response.StatusCode}");
                    Console.WriteLine(responseString);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}


