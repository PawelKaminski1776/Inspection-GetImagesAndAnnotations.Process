using System.Text.Json;
using InspectionGetImagesAndAnnotations.Channel;
using InspectionGetImagesAndAnnotations.Messages.Dtos;

namespace InspectionGetImagesAndAnnotations.Handlers
{
    public class InspectionHandler : IHandleMessages<InspectionRequest>
    {
        private readonly PythonAPI _pythonAPI;
        private string StoragePath = $"{Directory.GetCurrentDirectory()}temp/databus";
        public InspectionHandler(PythonAPI pythonAPI)
        {
            this._pythonAPI = pythonAPI;
        }

        public async Task Handle(InspectionRequest message, IMessageHandlerContext context)
        {
            try
            {
                // Call external Python API
                var request = await _pythonAPI.SendToImageTrainingAPI("/SemiAutomaticTraining", message);

                // Deserialize response
                InspectionResponse response = null;
                try
                {
                    response = JsonSerializer.Deserialize<InspectionResponse>(request);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error while processing message: {ex.Message}");
                    return;
                }

                if (Directory.Exists(StoragePath))
                {
                    Directory.Delete(StoragePath, true);
                }

                if (!Directory.Exists(StoragePath))
                {
                    Directory.CreateDirectory(StoragePath);
                }

                // Process each image annotation
                foreach (var imageAnnotation in response.data)
                {
                    if (!string.IsNullOrEmpty(imageAnnotation.image)) // Image contains the base64 data
                    {
                        try
                        {
                            // Generate a unique file name while keeping original extension
                            string fileExtension = GetFileExtensionFromBase64(imageAnnotation.image);
                            string fileName = $"{imageAnnotation.image_name}";
                            string filePath = Path.Combine(StoragePath, fileName);

                            // Convert Base64 to bytes and save it
                            byte[] imageBytes = Convert.FromBase64String(imageAnnotation.image);
                            await File.WriteAllBytesAsync(filePath, imageBytes);

                            // Replace image string with just the filename
                            imageAnnotation.image = fileName;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error saving image: {e.Message}");
                        }
                    }
                }

                Console.WriteLine(JsonSerializer.Serialize<InspectionResponse>(response));


                // Send updated response (now contains file names instead of large images)
                await context.Reply(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while processing the message: {ex.Message}");
                throw;
            }
        }

        private string GetFileExtensionFromBase64(string base64String)
        {
            if (base64String.StartsWith("/9j")) return ".jpg";  // JPEG
            if (base64String.StartsWith("iVBORw0KGgo")) return ".png";  // PNG
            if (base64String.StartsWith("R0lGOD")) return ".gif";  // GIF
            if (base64String.StartsWith("UklGR")) return ".webp";  // WebP
            return ".jpg"; // Default to JPG if unknown
        }
    }
}
