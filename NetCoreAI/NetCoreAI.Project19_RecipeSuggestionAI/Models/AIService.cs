using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NetCoreAI.Project19_RecipeSuggestionAI.Models
{
    public class AIService
    {
        private readonly HttpClient _httpClient;
        private const string AIUrl = "https://openrouter.ai/api/v1/chat/completions";
        private const string apiKey = "";

        public AIService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> GetRecipeAsync(string ingredients)
        {
            var requestBody = new
            {
                model = "openrouter/free",
                messages = new[]
                {
                    new {
                        role = "system",
                       content = """
Sen deneyimli bir profesyonel şefsin. Kullanıcının verdiği malzemelere göre
uygulanabilir, lezzetli tarifler öneriyorsun.

ÇIKTI FORMATI (bunu eksiksiz uygula):

YEMEK ADI: [ad]
AÇIKLAMA: [1-2 cümle]

SÜRE:
Hazırlık: [x dk]
Pişirme: [x dk]
Toplam: [x dk]

ZORLUK: [Kolay / Orta / Zor]
KİŞİ SAYISI: [x kişilik]

MALZEMELER:
- [miktar] [malzeme]
- [miktar] [malzeme]

YAPILIŞI:
1. [adım]
2. [adım]
3. [adım]

SERVİS ÖNERİSİ:
[öneri]

- Markdown, tablo, yıldız (*), diyez (#) KULLANMA
- Sadece yukarıdaki format şablonunu kullan, fazladan metin ekleme
- Yalnızca kullanıcının verdiği malzemeleri kullan
- Belirtilmemişse 2 kişilik tarif ver
"""
                    },
                    new { role = "user", content = $"Elimde şu malzemeler var: {ingredients}. Ne yapabilirim?" }
                },
                temperature = 0.7
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(AIUrl, content);
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
