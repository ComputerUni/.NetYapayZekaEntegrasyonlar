using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


class Program
{
    private static readonly string apiKey = "";

    public static async Task Main(string[] args)
    {
        Console.Write("Uzun metninizi veya makalenizi giriniz: ");
        string text = Console.ReadLine();

        if (!string.IsNullOrEmpty(text))
        {
            Console.WriteLine();
            Console.WriteLine("Girmiş olduğunuz metin AI tarafından özetleniyor.");
            Console.WriteLine();

            string shortSummary = await SummarizeText(text, "short");
            string mediumSummary = await SummarizeText(text, "medium");
            string longSummary = await SummarizeText(text, "detaied");

            Console.WriteLine("Özetler");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($" ** Kısa Özet: ** {shortSummary}");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($" ** Orta Uzunlukta Özet: ** {mediumSummary}");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($" ** Detaylı Özet: ** {longSummary}");

        }
    }

    private static async Task<string> SummarizeText(string text, string level)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            string instruction = level switch
            {
                "short" => "Summarize this text in 1-2 sentences",
                "medium" => "Summarize this text in 3-5 sentences",
                "detailed" => "Summarize this text in a detailed but concise manner.",
                _ => "Summarize this text"
            };

            var requestBody = new
            {
                model = "openrouter/free",
                messages = new[]
                {
                    new {role="system", content="You are an AI that summarize text info different levels: short, medium and detailed."},
                    new {role="user", content=$"{instruction}\n\n{text}"}
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