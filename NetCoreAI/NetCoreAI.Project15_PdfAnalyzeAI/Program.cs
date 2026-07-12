using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using UglyToad.PdfPig;

class Program
{
    private static readonly string apiKey = "";

    static async Task Main(string[] args)
    {
        Console.WriteLine("PDF Dosya Yolunu Giriniz: ");
        string pdfPath = Console.ReadLine();
        Console.WriteLine("PDF Analizi AI Tarafından Yapılıyor...");
        Console.WriteLine();
        string pdfText = ExtractTextFromPdf(pdfPath);
        await AnalyzeWithAI(pdfText, "PDF İçeriği");

        static string ExtractTextFromPdf(string filePath)
        {
            StringBuilder text = new StringBuilder();
            using (PdfDocument pdf = PdfDocument.Open(filePath))
            {
                foreach (var page in pdf.GetPages())
                {
                    text.AppendLine(page.Text);
                }
            }
            return text.ToString();
        }

        static async Task AnalyzeWithAI(string text, string sourceType)
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
                    Console.WriteLine($"\n AI Analizi ({sourceType}): \n {answer}");
                }
                else
                {
                    Console.WriteLine($"Bir hata oluştu: {response.StatusCode}");
                    Console.WriteLine(responseJson);
                }
            }
        }

    }
}
