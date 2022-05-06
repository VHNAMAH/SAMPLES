using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace GAB.Cognitive.Services.Extensions
{
    public static class Extensions
    {
        public static async Task<ImageAnalysis> Analyze(this ComputerVisionClient client, Stream fileStream)
        {
            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Tags
            };

            ImageAnalysis results = await client.AnalyzeImageInStreamAsync(fileStream, visualFeatures: features);
            return results;
        }

        public static object ParseImageAnalysis(this ImageAnalysis imageAnalysis)
        {
            List<KeyValuePair<string, double>> output = new List<KeyValuePair<string, double>>();
            foreach (var item in imageAnalysis.Tags)
            {
                output.Add(new KeyValuePair<string, double>(item.Name, item.Confidence));
            }

            return output;
        }
    }
}