using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    private static readonly string apiKey = "";

    static async Task Main()
    {
        Console.Write("Hikaye Türünü Seçiniz (Macera, Korku, Bilim Kurgu, Fantastik, Komedi): ");
        string genre = Console.ReadLine();

        Console.Write("Ana karakteriniz kim: ");
        string character = Console.ReadLine();

        Console.Write("Hikaye nerede geçiyor ?: ");
        string place = Console.ReadLine();

        Console.Write("Hikayenizin uzunluğu (kısa/orta/uzun): ");
        string length = Console.ReadLine();

        string prompt = $"{genre} türünde bir hikaye yaz. Baş karakterin adı {character}. Hikaye {place} bölgesinde geçiyor. {length} bir hikaye olsun. Giriş, gelişme ve sonuç içermeli.";

        string story = await GenerateStory(prompt);
        Console.WriteLine();
        Console.WriteLine("------ AI Tarafında Oluşturulan Hikaye ------\n");
        Console.WriteLine();
        Console.WriteLine(story);
    }

    static async Task<string> GenerateStory(string prompt)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new
        {
            model = "openrouter/free",
            messages = new[]
        {
                new { role = "system", content = "Sen profesyonel bir hikaye yazarısın." },
                new { role = "user", content = prompt }
            }
        };

        var jsonContent = JsonSerializer.Serialize(requestBody);
        HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
        string responseJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<JsonElement>(responseJson);
            var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
            return answer ?? "";
        }
        else
        {
            Console.WriteLine($"Bir hata oluştu: {response.StatusCode}");
            Console.WriteLine(responseJson);
            return "";
        }
    }
}