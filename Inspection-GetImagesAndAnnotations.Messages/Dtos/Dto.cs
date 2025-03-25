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
        public List<ImageAnnotationDto> data { get; set; } = new();
    }

    public class ImageAnnotationDto
    {
        public Dictionary<string, List<AnnotationDto>> annotations { get; set; } = new();
        public string image { get; set; }

        public string image_name { get; set; }
    }

    public class AnnotationDto
    {
        public List<double> bounding_box { get; set; } = new();
        public string class_name { get; set; }
        public double score { get; set; }
    }

}
