using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;


namespace ImageProcessingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Chemin vers le dossier contenant les images
            string inputFolder = "../public/input";
            string[] inputFiles = Directory.GetFiles(inputFolder, "*.jpg");

            // Chemin vers le dossier de sortie
            string outputFolder = "../public/output";
            Directory.CreateDirectory(outputFolder); // Créer le dossier de sortie s'il n'existe pas

            // Mesurer le temps d'exécution sans parallélisme
            Stopwatch stopwatchSequential = Stopwatch.StartNew();
            ResizeImagesSequential(inputFiles, outputFolder, 100, 100);
            stopwatchSequential.Stop();

            Console.WriteLine($"Sequential resizing done in {stopwatchSequential.ElapsedMilliseconds} milliseconds.");

            // Mesurer le temps d'exécution avec parallélisme
            Stopwatch stopwatchParallel = Stopwatch.StartNew();
            ResizeImagesParallel(inputFiles, outputFolder, 100, 100);
            stopwatchParallel.Stop();

            Console.WriteLine($"Parallel resizing done in {stopwatchParallel.ElapsedMilliseconds} milliseconds.");

            // Comparer les temps d'exécution
            Console.WriteLine($"Speedup factor: {stopwatchSequential.ElapsedMilliseconds / (double)stopwatchParallel.ElapsedMilliseconds}");
        }

        static void ResizeImagesSequential(string[] inputFiles, string outputFolder, int newWidth, int newHeight)
        {
            foreach (var inputFile in inputFiles)
            {
                string outputFileName = Path.GetFileNameWithoutExtension(inputFile) + "_resized.jpg";
                string outputFilePath = Path.Combine(outputFolder, outputFileName);
                ResizeImage(inputFile, outputFilePath, newWidth, newHeight);
            }
        }

        static void ResizeImagesParallel(string[] inputFiles, string outputFolder, int newWidth, int newHeight)
        {
            Parallel.ForEach(inputFiles, inputFile =>
            {
                string outputFileName = Path.GetFileNameWithoutExtension(inputFile) + "_resized.jpg";
                string outputFilePath = Path.Combine(outputFolder, outputFileName);
                ResizeImage(inputFile, outputFilePath, newWidth, newHeight);
            });
        }

        static void ResizeImage(string inputPath, string outputPath, int newWidth, int newHeight)
        {
            // Charger l'image
            using (var image = Image.Load(inputPath))
            {
                // Redimensionner l'image
                image.Mutate(x => x.Resize(newWidth, newHeight));

                // Sauvegarder l'image redimensionnée
                image.Save(outputPath); // Le format est déduit de l'extension de fichier
            }
        }
    }
}



/* using Newtonsoft.Json;

namespace sample1;

class Program
{
    static void Main(string[] args)
    {
        Person personne = new Person { nom = "Jane", age = 20 };
        // Console.WriteLine(personne.Hello(true));
        Console.WriteLine(JsonConvert.SerializeObject(personne, Formatting.Indented));
    }
}

class Person
{
    public string nom { get; set; }
    public int age { get; set; }

    public string Hello(bool isLowercase)
    {
        string message = $"Hello {nom}, you are {age}";
        return isLowercase ? message.ToLower() : message.ToUpper();
    }
} */



