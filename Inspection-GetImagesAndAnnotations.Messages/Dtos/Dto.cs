using NServiceBus;

namespace InspectionGetImagesAndAnnotations.Messages.Dtos
{
    public class InspectionRequest : IMessage
    {
        public int numOfImages { get; set; }
        public string county { get; set; }
        public string model_dir { get; set; }
        public string website { get; set; }
        public string inspection { get; set; }

    }


    public class InspectionResponse : IMessage
    {
        public InspectionImageAndAnnotations[] data { get; set; }
    }

    public class InspectionImageAndAnnotations
    {
        public IFormFile? image { get; set; }
        public string annotations { get; set; }
    }

}
