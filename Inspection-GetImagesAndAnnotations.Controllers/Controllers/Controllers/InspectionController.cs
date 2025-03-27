using Microsoft.AspNetCore.Mvc;
using InspectionGetImagesAndAnnotations.Messages.Dtos;
using InspectionGetImagesAndAnnotations.Controllers.DtoFactory;

namespace InspectionGetImagesAndAnnotations.Controllers
{
    [ApiController]
    [Route("Api/GetImagesAndAnnotations")]
    public class InspectionController : BaseController
    {
        public InspectionController(IMessageSession messageSession, IDtoFactory dtoFactory)
            : base(messageSession, dtoFactory) { }

        [HttpPost("GetAnnotations")]
        public async Task<IActionResult> GetAnnotations([FromBody] InspectionRequest dto)
        {
            var Dto = (InspectionRequest)_dtoFactory.UseDto("inspectiondto", dto);

            try
            {
                var response = await _messageSession.Request<InspectionResponse>(Dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error while processing the request: {ex.Message}");
            }
        }

        private string StoragePath = $"{Directory.GetCurrentDirectory()}temp/databus"; // Change to your configured CheckClaims path

        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        
        {
            var filePath = Path.Combine(StoragePath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found.");
            }
            
            string contentType = GetContentType(filePath);
            var imageBytes = System.IO.File.ReadAllBytes(filePath);
            return File(imageBytes, contentType);
        }

       private string GetContentType(string filePath)
       {
        var extension = Path.GetExtension(filePath).ToLower();

            return extension switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".gif" => "image/gif",
                    ".bmp" => "image/bmp",
                    ".webp" => "image/webp",
                    ".svg" => "image/svg+xml",
                    ".tiff" or ".tif" => "image/tiff",
                    ".ico" => "image/x-icon",
                    _ => "application/octet-stream"
                };
        }
    }
    

}
