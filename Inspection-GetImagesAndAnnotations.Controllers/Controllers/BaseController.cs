using InspectionGetImagesAndAnnotations.Controllers.DtoFactory;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using System.Threading.Tasks;

namespace InspectionGetImagesAndAnnotations.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMessageSession _messageSession;
        protected readonly IDtoFactory _dtoFactory;

        public BaseController(IMessageSession messageSession, IDtoFactory dtoFactory)
        {
            _messageSession = messageSession;
            _dtoFactory = dtoFactory;
        }

        protected async Task<IActionResult> HandleMessage<TMessage>(TMessage message)
        {
            try
            {
                await _messageSession.Send("NServiceBusHandlers", message);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while processing the message.");
            }
        }
    }

}
