using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

class Program
{
    private static readonly string apiKey = "";

    static async Task Main(string[] args)
    {
        Console.Write("Haberleri Görüntülemek İçin Bir URL Giriniz: ");
        string rssUrl = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine("Haberler Yükleniyor...");
        Console.WriteLine();

        List<(string title, string description)> news = await FetchRSS(rssUrl);

        foreach(var item in news)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Başlık: {item.title}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            string summary = await SummarizeNews(item.description);
            Console.WriteLine($"Özet: {summary}");
            Console.WriteLine("------------------------------------------------------------------------------------------");
        }
    }


    static async Task<List<(string, string)>> FetchRSS(string url)
    {
        using var client = new HttpClient();
        string xml = await client.GetStringAsync(url);
        var doc = XDocument.Parse(xml);

        var items = doc.Descendants("item")
            .Take(10)
            .Select(x => (
            title: x.Element("title")?.Value ?? "",
            description: x.Element("description")?.Value ?? "")).ToList();

        return items;
    }

    static async Task<string> SummarizeNews(string text)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new
        {
            model = "openrouter/free",
            messages = new[]
            {
                new { role = "system", content = "Sen bir haber özetleyicisisin. Verilen haberi Türkçe olarak 2-3 cümleyle özetle." },
                new { role = "user", content = text }
            }
        };

        string json = JsonSerializer.Serialize(requestBody);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
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