using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Company.Function
{
    public static class ResizeHttpTrigger
    {
        [FunctionName("ResizeHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                // Récupérer les paramètres de largeur et de hauteur de la requête
                int width = int.Parse(req.Query["w"]);
                int height = int.Parse(req.Query["h"]);

                // Charger l'image à partir du corps de la requête
                using (Stream imageStream = req.Body)
                using (var image = Image.Load(imageStream))
                {
                    // Redimensionner l'image
                    image.Mutate(x => x.Resize(width, height));

                    // Convertir l'image en octets JPEG
                    using (var outputStream = new MemoryStream())
                    {
                        image.Save(outputStream, new JpegEncoder());
                        byte[] targetImageBytes = outputStream.ToArray();

                        // Renvoyer les octets de la nouvelle image en tant que réponse à la requête
                        return new FileContentResult(targetImageBytes, "image/jpeg");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Gérer l'exception d'argument invalide
                log.LogError(ex, "Invalid argument exception occurred.");
                return new BadRequestResult();
            }
            catch (FormatException ex)
            {
                // Gérer l'exception de format invalide
                log.LogError(ex, "Format exception occurred.");
                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                // Gérer toute autre exception générique
                log.LogError(ex, "An error occurred while processing the request.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
