using Tesseract;

class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Karakter Okuması Yapılacak Resim Yolu: ");
        string imagePath = Console.ReadLine();
        imagePath = imagePath.Trim().TrimStart('?');
        Console.WriteLine($"Temizlenmiş Yol: [{imagePath}]");
        string tessDataPath = @"C:\tessdata";

        try
        {
            using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.LstmOnly))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using(var page = engine.Process(img))
                    {
                        string text = page.GetText();
                        float confidence = page.GetMeanConfidence();
                        Console.WriteLine($"Güvenilirlik Skoru: {confidence}");
                        Console.WriteLine($"Resimden Okunan Metin: {text}");
                    }
                }
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        }
        Console.ReadLine();
    }
}