using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GAB.Cognitive.Services.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using GAB.Cognitive.Services.Extensions;

namespace GAB.Cognitive.Services.Controllers;

public class CameraController : Controller
{
    private readonly ILogger<CameraController> _logger;
    
    // Add your Computer Vision subscription key and endpoint
    private static string key = "5db0c5452773476da477c5749caaa6dd";
    private static string endpoint = "https://cognitive-bootcamp.cognitiveservices.azure.com/";


    public CameraController(ILogger<CameraController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Capture(string name)
    {
        var files = HttpContext.Request.Form.Files;
        var result = new ImageAnalysis();

        if (files != null)
        {
            // Create a client
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
            

            var file = files[0];

                using (Stream fileStream = file.OpenReadStream())
                {
                    // Analyze an image to get features and other properties.
                    result = await client.Analyze(fileStream);
                }

                // if (file.Length > 0)
                // {
                //     // Getting Filename  
                //     var fileName = file.FileName;
                //     // Unique filename "Guid"  
                //     var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                //     // Getting Extension  
                //     var fileExtension = Path.GetExtension(fileName);
                //     // Concating filename + fileExtension (unique filename)  
                //     var newFileName = string.Concat(myUniqueFileName, fileExtension);
                //     //  Generating Path to store photo   
                //     var filepath = Path.Combine(_environment.WebRootPath, "CameraPhotos") + $@"\{newFileName}";

                //     if (!string.IsNullOrEmpty(filepath))
                //     {
                //         // Storing Image in Folder  
                //         StoreInFolder(file, filepath);
                //     }

                //     var imageBytes = System.IO.File.ReadAllBytes(filepath);
                //     if (imageBytes != null)
                //     {
                //         // Storing Image in Folder  
                //         StoreInDatabase(imageBytes);
                //     }

                // }

            return Json(result.ParseImageAnalysis());
        }
        else
        {
            return Json("Failed to analyse image");
        }
    }
}
