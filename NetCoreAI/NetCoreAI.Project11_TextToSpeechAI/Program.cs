using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    static async Task Main(string[] args)
    {
        Console.Write("Metni giriniz: ");
        string text = Console.ReadLine();

        if(!string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Ses dosyası oluşturuluyor...");
            await Generate(text);
            Console.WriteLine("Ses dosyası output.mp3 olarak kaydedildi.");
            System.Diagnostics.Process.Start("explorer.exe", "output.mp3");
        }
    }

    static async Task Generate(string text)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("xi-api-key", apiKey);

            var requestBody = new
            {
                text = text,
                model_id = "eleven_multilingual_v2",
                voice_settings = new { stability = 0.5, similarity_boost = 0.5 }
            };

            string jsonData = JsonSerializer.Serialize(requestBody);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.elevenlabs.io/v1/text-to-speech/onwK4e9ZLuTAKqWW03F9", content);

            if (response.IsSuccessStatusCode)
            {
                byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync("output.mp3", audioBytes);
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Bir hata oluştu: {error}");
            }
        }
    }
}
