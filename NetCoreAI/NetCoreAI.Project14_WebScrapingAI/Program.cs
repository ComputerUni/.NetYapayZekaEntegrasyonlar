using HtmlAgilityPack;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

class Program
{
    private static readonly string apiKey = "";

    static async Task Main(string[] args)
    {
        Console.Write("Lütfen analiz yapmak istediğiniz web sayfa URL'ini giriniz: ");
        string input = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine("Web sayfası içeriği: ");
        string webContent = ExtractTextFromWeb(input);
        string result = await AnalyzeWithAI(webContent, "Web Sayfası İçeriği");
        WriteWrapped(result);
    }

    static string ExtractTextFromWeb(string url)
    {
        var web = new HtmlWeb();
        var doc = web.Load(url);

        var bodyText = doc.DocumentNode.SelectSingleNode("//body")?.InnerText;
        return bodyText ?? "Sayfa içeriği bulunamadı.";
    }

    static void WriteWrapped(string text, int maxWidth = 100)
    {
        string[] words = text.Split(' ');
        int currentLength = 0;

        foreach(string word in words)
        {
            if(currentLength + word.Length + 1 > maxWidth)
            {
                Console.WriteLine();
                currentLength = 0;
;            }

            Console.Write(word + " ");
            currentLength += word.Length + 1;
        }
        Console.WriteLine();
    }

    static async Task<string> AnalyzeWithAI(string text, string sourceType)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var requestBody = new
            {
                model = "openrouter/free",
                messages = new[]
                {
                    new {role="system", content="Sen bir yapay zeka asistanısın. Kullanıcının gönderdiği metni analiz eder ve Türkçe olarak özetlersin. Yanıtlarını sadece Türkçe ver."},
                    new {role="user", content=$"Analyze and summarize the following {sourceType}: \n\n {text}"}
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
}