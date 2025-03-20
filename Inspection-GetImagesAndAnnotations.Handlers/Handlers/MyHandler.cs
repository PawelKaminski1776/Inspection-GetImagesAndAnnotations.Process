using InspectionGetImagesAndAnnotations.Messages.Dtos;

namespace InspectionGetImagesAndAnnotations.Handlers
{
    public class MyHandler : IHandleMessages<MessageRequest>
    {
        public MyHandler()
        {
        }

        public async Task Handle(MessageRequest message, IMessageHandlerContext context)
        {
            try
            {
                
                await context.Reply(new MessageResponse { Message = "HELLO BACK FROM HANDLER"});

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while processing the message: {ex.Message}");

                throw;
            }
        }
    }
}
