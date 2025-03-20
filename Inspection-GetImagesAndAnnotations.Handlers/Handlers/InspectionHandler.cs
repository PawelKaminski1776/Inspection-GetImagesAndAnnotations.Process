using System.Text.Json;
using InspectionGetImagesAndAnnotations.Channel;
using InspectionGetImagesAndAnnotations.Messages.Dtos;

namespace InspectionGetImagesAndAnnotations.Handlers
{
    public class InspectionHandler : IHandleMessages<InspectionRequest>
    {
        private readonly PythonAPI _pythonAPI;
        public InspectionHandler(PythonAPI pythonAPI)
        {
            this._pythonAPI = pythonAPI;
        }

        public async Task Handle(InspectionRequest message, IMessageHandlerContext context)
        {
            try
            {
                InspectionResponse response = JsonSerializer.Deserialize<InspectionResponse>(await _pythonAPI.SendToImageTrainingAPI("/SemiAutomaticTraining", message));
                
                await context.Reply(response);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while processing the message: {ex.Message}");

                throw;
            }
        }
    }
}
