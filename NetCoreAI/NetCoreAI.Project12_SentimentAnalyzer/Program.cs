using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";

    record AnalysisResult(string sent, int rate, string comment, Dictionary<string, int> otherSent);

    static async Task Main(string[] args)
    {
        Console.Write("Lütfen metni giriniz: ");
        string input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine();
            Console.WriteLine("Duygu Analizi Yapılıyor...");
            Console.WriteLine();
            string sentiment = await SentimentAnalyzer(input);
            AnalysisResult result = Analyze(sentiment);
            ConsoleColor renk = BelirleRenk(result.sent);
            Console.ForegroundColor = renk;
            Console.WriteLine($"Duygu  : {result.sent}");
            Console.WriteLine();
            Console.WriteLine($"Oran   : %{result.rate}  {ProgressBar(result.rate)}");
            Console.WriteLine();
            Console.WriteLine($"Analiz : {result.comment}");
            Console.WriteLine();
            Console.WriteLine("Diğer Duygular:");
            foreach (var o in result.otherSent)
                Console.WriteLine($"  {o.Key,-10}: %{o.Value}  {ProgressBar(o.Value)}");
            Console.ResetColor();
        }
    }

    static AnalysisResult Analyze(string sentiment)
    {
        try
        {
            var json = JsonSerializer.Deserialize<JsonElement>(sentiment.Trim());
            string sent = json.GetProperty("sentiment").GetString() ?? "nötr";
            int rate = json.GetProperty("rate").GetInt32();
            string comment = json.GetProperty("comment").GetString() ?? "";
            var other = new Dictionary<string, int>();
            foreach(var otherSent in json.GetProperty("diger").EnumerateObject())
            {
                other[otherSent.Name] = otherSent.Value.GetInt32();
            }
            return new AnalysisResult(sent, rate, comment, other);
        }
        catch
        {

            return new AnalysisResult("belirsiz", 0, sentiment, new Dictionary<string, int>());
        }
    }

    static string ProgressBar(int rate)
    {
        int full = rate / 10;
        return new string('█', full) + new string('░', 10 - full);
    }

    static ConsoleColor BelirleRenk(string sentiment)
    {
        string lower = sentiment.ToLower();

        if (lower.Contains("mutlu") || lower.Contains("sevinç") || lower.Contains("pozitif") || lower.Contains("olumlu"))
            return ConsoleColor.Green;

        if (lower.Contains("üzgün") || lower.Contains("hüzün") || lower.Contains("keder") || lower.Contains("negatif"))
            return ConsoleColor.Cyan;

        if (lower.Contains("sinir") || lower.Contains("öfke") || lower.Contains("kızgın"))
            return ConsoleColor.Red;

        if (lower.Contains("heyecan") || lower.Contains("coşku") || lower.Contains("şaşkın"))
            return ConsoleColor.Magenta;

        return ConsoleColor.White;
    }

    static async Task<string> SentimentAnalyzer(string text)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);


            var requestBody = new
            {
                model = "openrouter/free",
                messages = new[]
    {
      new { role = "system", content = "Sen profesyonel bir duygu analistisisin. Cümle içerisindeki kelimelere ve ifadelere dikkat ederek yorum yap ve sonuç çıkar. SADECE şu JSON formatında yanıt ver, başka hiçbir şey yazma: {\"sentiment\": \"mutlu/üzgün/sinirli/heyecanlı/nötr\", \"rate\": 0-100 arası yüzde, \"comment\": \"kısa ve detaylı analiz\", \"diger\": {\"mutlu\": 0-100, \"üzgün\": 0-100, \"sinirli\": 0-100, \"heyecanlı\": 0-100, \"nötr\": 0-100}} — diger alanındaki tüm değerlerin toplamı kesinlikle 100 olmalıdır." },
        new { role = "user", content = text }
    }
            };

            string jsonData = JsonSerializer.Serialize(requestBody);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                return answer ?? "";
            }
            else
            {
                Console.WriteLine($"Bir hata oluştu: {response.StatusCode}");
                Console.WriteLine(responseString);
                return "";
            }

        }
    }
}