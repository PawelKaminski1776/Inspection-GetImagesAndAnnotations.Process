using NServiceBus;

namespace InspectionGetImagesAndAnnotations.Messages.Dtos
{
    public class MessageRequest : IMessage
    {
        public string Message { get; set; }
    }

    public class MessageResponse : IMessage
    {
        public string Message { get; set; }
    }

}
