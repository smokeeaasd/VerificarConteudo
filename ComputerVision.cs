using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerificarConteudo
{
    public static class Verificar
    {
        public static ComputerVisionClient Autenticar(string endpoint, string key)
        {
            ComputerVisionClient client =
                new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
                { Endpoint = endpoint };
            return client;
        }

        public static async Task<bool> AnalisarImagem(ComputerVisionClient client, string imageUrl)
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

            ImageAnalysis res = await client.AnalyzeImageAsync(imageUrl, visualFeatures: features);

            return (res.Adult.IsAdultContent || res.Adult.IsGoryContent || res.Adult.IsRacyContent);
        }
    }
}
