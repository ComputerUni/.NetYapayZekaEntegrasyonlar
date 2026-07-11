using System.Speech.Synthesis;

class Program
{
    static void Main(string[] args)
    {
        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();

        speechSynthesizer.Volume = 100;
        speechSynthesizer.Rate = 0;

        Console.Write("Metni giriniz: ");
        string text = Console.ReadLine();
        if(!string.IsNullOrEmpty(text))
        {
            speechSynthesizer.Speak(text);
        }
        Console.ReadLine();
    } 
}