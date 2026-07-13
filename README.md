# .NET Yapay Zeka Entegrasyonları - Proje Koleksiyonu

Bu repo, .NET Core/ASP.NET Core teknolojileri kullanarak çeşitli yapay zeka servislerinin entegre edildiği 19 adet pratik proje içermektedir. Her proje, farklı AI API'leri ve kütüphaneleri kullanarak gerçek dünya problemlerine çözüm sunmaktadır.

---

## 📋 Proje Özeti

| # | Proje Adı | Tür | AI Servisi | Açıklama |
|---|-----------|-----|-----------|----------|
| 1 | **ApiDemo** | ASP.NET Core Web API | - | Temel REST API yapısı ve Entity Framework Core ile veri yönetimi |
| 2 | **ApiConsumeUI** | ASP.NET Core MVC | - | Project 1'in API'sini tüketen kullanıcı arayüzü |
| 3 | **RapidApi** | Console App | RapidAPI | RapidAPI platformu üzerinden birden fazla API entegrasyonu |
| 4 | **OpenAIChat** | Console App | OpenRouter AI | Sohbet temelli AI asistanı (multi-turn conversation) |
| 5 | **OpenWhisperAudioTranskript** | Console App | OpenAI Whisper | Ses dosyalarının metin transkriptine çevrilmesi |
| 6 | **DallEImageGeneration** | Console App | DALL-E API | Metin açıklamalarından görüntü üretimi |
| 7 | **TesseractOCR** | Console App | Tesseract OCR | Görüntülerden metni tanıma ve çıkarma |
| 8 | **GoogleCloudVision** | Console App | Google Cloud Vision | Görüntü analizi ve özellik çıkarımı |
| 9 | **OpenAITranslate** | Console App | OpenAI | Metin tercümesi (6 farklı dile) |
| 10 | **TextToSpeech** | Console App | TTS API | Metni sese dönüştürme |
| 11 | **TextToSpeechAI** | Console App | Yapay Zeka TTS | AI destekli metin-konuşma dönüştürü |
| 12 | **SentimentAnalyzer** | Console App | AI Duygu Analizi | Metinlerin duygusal tonunu analiz etme |
| 13 | **ArticleSummarizeAI** | Console App | OpenAI | Makale ve metinleri özetleme (3 seviye) |
| 14 | **WebScrapingAI** | Console App | OpenAI + HtmlAgilityPack | Web sayfalarını tarayıp AI ile analiz etme |
| 15 | **PdfAnalyzeAI** | Console App | OpenAI + PdfPig | PDF dosyalarını okuyup AI ile analiz etme |
| 16 | **GoogleCloudVisionImageDetection** | Console App | Google Cloud Vision | Nesne algılama ve sınıflandırma |
| 17 | **NewsSummarizeAI** | Console App | OpenAI | Haber kaynaklarından içerik özetleme |
| 18 | **CreateStoryAI** | Console App | OpenAI | Yapay zeka tarafından hikaye/öykü oluşturma |
| 19 | **RecipeSuggestionAI** | ASP.NET Core MVC | OpenRouter AI | Verilen malzemelere göre tarif önerme sistemi |

---

## 🚀 Proje Detayları

### Project 1: ApiDemo
**Tür:** ASP.NET Core Web API  
**Teknolojiler:** Entity Framework Core, SQL Server

Temel CRUD işlemleri için basit bir REST API. Müşteri (Customer) yönetimi için endpoint'ler içerir.

**Yapı:**
```
Controllers/
  └─ CustomersController.cs
Context/
  └─ ApiContext.cs (EF Core DbContext)
Entities/
  └─ Customer.cs
Migrations/
  └─ (EF Migrations)
```

---

### Project 2: ApiConsumeUI
**Tür:** ASP.NET Core MVC  
**Teknolojiler:** HttpClient, MVC, Bootstrap

Project 1'in API'sini tüketen bir web uygulaması. Müşteri ekleme, düzenleme, silme ve listeleme işlemleri yapılabilir.

**Özellikler:**
- Müşteri listesi görüntüleme
- Yeni müşteri ekleme
- Müşteri bilgilerini güncelleme
- Müşteri silme

---

### Project 3: RapidApi
**Tür:** Console Application  
**Teknolojiler:** RapidAPI

RapidAPI platformu üzerinden birden fazla API'ye erişim. Örnek olarak dış kaynaklardan veri çekme uygulanmıştır.

---

### Project 4: OpenAIChat
**Tür:** Console Application  
**Teknolojiler:** OpenRouter API, HTTP Client

OpenRouter üzerinden OpenAI modellerine erişen interaktif sohbet uygulaması. Kullanıcıyla çok turlu (multi-turn) konuşmalar yapılabilir.

**Özellikler:**
- Sistem mesajı ile AI davranışı belirleme
- Konuşma geçmişi tutma
- Hata yönetimi

---

### Project 5: OpenWhisperAudioTranskript
**Tür:** Console Application  
**Teknolojiler:** OpenAI Whisper API, HttpClient

MP3 veya diğer ses dosyalarını metin olarak transkripsiyon yapan uygulama.

**İşlev:**
- Yerel ses dosyasını OpenAI Whisper API'sine gönderme
- Gerçek zamanlı transkripsiyon sonucunu alma

---

### Project 6: DallEImageGeneration
**Tür:** Console Application  
**Teknolojiler:** DALL-E 3 API, Newtonsoft.Json

Metin açıklamalarından (prompts) görüntü oluşturan yapay zeka sistemi.

**Parametreler:**
- Prompt: Oluşturulacak görüntü açıklaması
- Boyut: 512x512 px
- Sayı: 1 görüntü

---

### Project 7: TesseractOCR
**Tür:** Console Application  
**Teknolojiler:** Tesseract OCR Engine (Leptonica)

Görüntülerden metni tanıma (Optical Character Recognition). İngilizce ve Türkçe metinleri destekler.

**Özellikler:**
- Yerel görüntü dosyasından metin çıkarma
- Güvenilirlik skorunu gösterme
- Hata yönetimi

**Sistem Gereksinimleri:**
```
tessdata klasörü: C:\tessdata (Windows)
```

---

### Project 8: GoogleCloudVision
**Tür:** Console Application  
**Teknolojiler:** Google Cloud Vision API

Google Cloud Vision API kullanarak görüntüleri analiz eder (etiketler, açıklama, metin algılama).

---

### Project 9: OpenAITranslate
**Tür:** Console Application  
**Teknolojiler:** OpenAI API, HttpClient

Metinleri 6 farklı dile tercüme eden uygulama:
- İngilizce (English)
- Fransızca (French)
- Almanca (German)
- Rusça (Russian)
- Arapça (Arabic)
- Japonca (Japanese)

**Özellikler:**
- Renkli çıktı
- İnteraktif dil seçimi

---

### Project 10: TextToSpeech
**Tür:** Console Application  
**Teknolojiler:** Text-to-Speech API

Metni sese dönüştüren basit bir uygulamadır. Harici bir TTS servisi kullanmaktadır.

---

### Project 11: TextToSpeechAI
**Tür:** Console Application  
**Teknolojiler:** AI-powered TTS

Yapay zeka destekli metin-konuşma dönüştürme. Daha doğal ve akıcı çıktı sağlamaktadır.

---

### Project 12: SentimentAnalyzer
**Tür:** Console Application  
**Teknolojiler:** OpenAI API, HttpClient

Metinlerin duygusal tonunu analiz eder. Duygu türünü, yoğunluk oranını ve analiz sonucunu gösterir.

**Çıktı Formatı:**
```
Duygu: [Olumlu/Olumsuz/Nötr/...]
Oran: %[0-100]
Analiz: [Detaylı açıklama]
Diğer Duygular: [Tüm duyguların yüzde oranları]
```

---

### Project 13: ArticleSummarizeAI
**Tür:** Console Application  
**Teknolojiler:** OpenAI API

Makale ve uzun metinleri üç seviye halinde özetler:
1. **Kısa Özet** - En temel bilgiler
2. **Orta Uzunlukta Özet** - Dengeli detay
3. **Detaylı Özet** - Kapsamlı bilgiler

---

### Project 14: WebScrapingAI
**Tür:** Console Application  
**Teknolojiler:** HtmlAgilityPack, OpenAI API

URL'den web sayfası içeriğini çeker ve AI ile analiz eder.

**Adımlar:**
1. Kullanıcıdan URL alıp
2. Web sayfasını HtmlAgilityPack ile tarar
3. Metin içeriğini çıkarır
4. OpenAI'ye analiz için gönderir

---

### Project 15: PdfAnalyzeAI
**Tür:** Console Application  
**Teknolojiler:** UglyToad.PdfPig, OpenAI API

PDF dosyalarını okuyup içeriğini AI ile analiz eden uygulama.

**Adımlar:**
1. PDF dosya yolu al
2. PdfPig kütüphanesi ile metni çıkar
3. Tüm sayfaları birleştir
4. AI'ye analiz için gönder

---

### Project 16: GoogleCloudVisionImageDetection
**Tür:** Console Application  
**Teknolojiler:** Google Cloud Vision API

Görüntülerdeki nesneleri tespit ve sınıflandırır. Özellikle nesne algılama (object detection) üzerine odaklanmıştır.

---

### Project 17: NewsSummarizeAI
**Tür:** Console Application  
**Teknolojiler:** News API, OpenAI API

Haber sitelerinden haberleri çeker ve yapay zeka ile özetler.

**İşlev:**
- Haber kaynağından veri çekme
- Her haberi AI ile özetleme
- Yoğun bilgi sunumu

---

### Project 18: CreateStoryAI
**Tür:** Console Application  
**Teknolojiler:** OpenAI API

Yapay zeka tarafından hikaye, öykü, roman parçaları gibi yaratıcı metin üretimi yapan uygulama.

**Giriş Parametreleri:**
- Tema/konu
- Ton (macera, gerilim, komedi, vb.)
- Uzunluk (kısa, orta, uzun)

---

### Project 19: RecipeSuggestionAI
**Tür:** ASP.NET Core MVC  
**Teknolojiler:** OpenRouter AI, HttpClient, MVC

Verilen malzemelere göre tarif önerisi yapan web uygulaması. Profesyonel şef rolüyle AI tarafından yemek tariferleri oluşturulur.

**Özellikler:**
- Malzeme listesi girdisi
- Hazırlık ve pişirme süreleri
- Zorluk derecesi
- Adım adım yapılış talimatları
- Web tabanlı kullanıcı arayüzü

**AI Prompt Özellikleri:**
```
Model: OpenRouter (free)
Role: Profesyonel Şef
Çıktı: Yapılandırılmış tarif formatı
- Yemek adı, açıklama
- Hazırlık/pişirme süreleri
- Malzeme listesi
- Adım adım talimatlar
- Zorluk derecesi
```

---

## 🛠️ Teknoloji Stack

### Temel Teknolojiler
- **.NET 8 / .NET 9**
- **ASP.NET Core MVC**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **Razor Pages/Views**

### Kullanılan AI Servisleri
- **OpenAI** (Chat, Whisper, DALL-E, API)
- **OpenRouter** (Ücretsiz AI modelleri)
- **Google Cloud Vision**
- **RapidAPI** (Çoklu API gateway)
- **Tesseract OCR** (Açık kaynak OCR)
- **HtmlAgilityPack** (Web scraping)
- **PdfPig** (PDF parsing)

### NuGet Paketleri
```
- Newtonsoft.Json
- HtmlAgilityPack
- UglyToad.PdfPig
- Tesseract (OCR)
- Google.Cloud.Vision.V1
- Entity Framework Core
- System.Net.Http
```

---

## 🔧 Kurulum ve Çalıştırma

### Proje Spesifik Kurulumlar

#### Console Uygulamaları Çalıştırma
```bash
cd NetCoreAI.Project4_OpenAIChat
dotnet run
```

#### Web Uygulamaları Çalıştırma
```bash
# Project 1: API
cd NetCoreAI.Project1_ApiDemo
dotnet run

# Project 2: UI (farklı terminal)
cd NetCoreAI.Project2_ApiConsumeUI
dotnet run
```

#### Tesseract OCR Setup (Project 7)
Windows için tessdata klasörü C:\tessdata konumuna konulmalıdır:
```
C:\tessdata\
  ├─ eng.traineddata
  └─ tur.traineddata (opsiyonel)
```



## 📖 API Entegrasyonları Rehberi

### Google Cloud Vision
1. Google Cloud Console'dan project oluştur
2. Vision API'yi etkinleştir
3. Service Account key indir
4. `GOOGLE_APPLICATION_CREDENTIALS` environment variable'ı ayarla

### RapidAPI
1. https://rapidapi.com'a üye ol
2. İstediğin API'yi seç
3. API key ile request gönder

---


