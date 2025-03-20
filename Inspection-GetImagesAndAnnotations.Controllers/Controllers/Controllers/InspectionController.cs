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
    }

}
