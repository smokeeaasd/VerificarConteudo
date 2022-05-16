using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Linq;

namespace VerificarConteudo
{
    class Program
    {
        // Add your Computer Vision subscription key and endpoint
        static string subscriptionKey = "your subscription key";
        static string endpoint = "endpoint";

        // URL image used for analyzing an image (image of puppy)
        private const string ANALYZE_URL_IMAGE = "analyse url image";

        static void Main(string[] args)
        {
            Console.WriteLine("Azure Cognitive Services Computer Vision - .NET");
            Console.WriteLine();

            // Criando um cliente
            ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);

            // Analyze an image to get features and other properties.
            AnalyzeImageUrl(client, ANALYZE_URL_IMAGE).Wait();

            Console.ReadKey();
        }
        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }

        public static async Task AnalyzeImageUrl(ComputerVisionClient client, string imageUrl)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Analisando imagem - URL");
            Console.WriteLine();

            // Creating a list that defines the features to be extracted from the image. 

            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Tags,
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Adult
            };

            Console.WriteLine($"Analisando a imagem: {Path.GetFileName(imageUrl)}...");
            Console.WriteLine();
            // Analisando a imagem da URL
            ImageAnalysis res = await client.AnalyzeImageAsync(imageUrl, visualFeatures: features);

            if (res.Adult.IsAdultContent)
                Console.WriteLine("Conteúdo adulto.");
            else
                Console.WriteLine("Não é conteúdo adulto.");
        }
    }
}