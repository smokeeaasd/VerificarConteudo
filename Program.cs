using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Linq;

using static VerificarConteudo.Verificar;
using static System.Console;

namespace VerificarConteudo
{
    class Program
    {
        static string subscriptionKey = "subscription key";
        static string endpoint = "endpoint"

        static void Main(string[] args)
        {
            ComputerVisionClient client = Autenticar(endpoint, subscriptionKey);

            WriteLine("verificar se a imagem é conteudo adulto");

            Write("insira o link da imagem: ");
            string IMAGE_URL = ReadLine();
            

            Task<bool> res = AnalisarImagem(client, IMAGE_URL);

            WriteLine(res.Result);

            ReadKey();
        }
    }
}
