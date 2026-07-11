using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write("Lütfen çevirmek istediğiniz cümleyi giriniz: ");
        string inputText = Console.ReadLine();

        string apiKey = "";

        string[] languages = { "English", "French", "German", "Russian", "Arabic", "Japanese" };
        string[] languagesTR = { "İngilizce", "Fransızca", "Almanca", "Rusça", "Arapça", "Japonca" };

        ConsoleColor[] colors =
        {
            ConsoleColor.Cyan,
            ConsoleColor.Green,
            ConsoleColor.Magenta,
            ConsoleColor.Yellow,
            ConsoleColor.Blue,
            ConsoleColor.Red
        };

        Console.WriteLine("Çevirmek istediğiniz dili ya da dilleri seçiniz: ");
        for (int i = 0; i < languagesTR.Length; i++)
        {
            Console.Write($"  {i + 1} - ");
            Console.BackgroundColor = colors[i];
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"  {languagesTR[i]} ");
            Console.ResetColor();
            Console.WriteLine();
        }


        Console.Write("\nSeçiminiz (örn: 1,3,4): ");
        string selection = Console.ReadLine();

        List<int> selectedIndexes = new();
        foreach (string key in selection.Split(','))
        {
            if (int.TryParse(key.Trim(), out int num) && num >= 1 && num <= languages.Length)
                selectedIndexes.Add(num - 1);
            else
                Console.WriteLine($"Geçersiz seçim: {key.Trim()}");
        }

        if (selectedIndexes.Count == 0)
        {
            Console.WriteLine("Hiçbir dil seçilmedi.");
            return;
        }

        Console.WriteLine();

        for (int i = 0; i < selectedIndexes.Count; i++)
        {
            int idx = selectedIndexes[i];
            string translatedText = await TranslateTextToLanguage(inputText, apiKey, languages[idx], languagesTR[idx], colors[idx]);
        }

        Console.WriteLine();
    }

    private static async Task<string> TranslateTextToLanguage(string text, string apiKey, string targetLanguage, string targetLanguageTR, ConsoleColor color)
    {
        var requestBody = new
        {
            model = "openrouter/free",
            messages = new object[]
    {
        new
        {
            role = "system",
            content = $"You are a professional translator. You only return translated text in {targetLanguage}. You never explain, never repeat the original."
        },
        new
        {
            role = "user",
            content = $"Translate this to {targetLanguage}:\n\n{text}"
        }
    }
        };

        var jsonData = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var response = await client.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<JsonElement>(responseString);
            var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($" {targetLanguageTR}: {answer} ");
            Console.ResetColor();
            Console.WriteLine();
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