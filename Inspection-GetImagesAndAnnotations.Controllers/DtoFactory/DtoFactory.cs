using System;
using InspectionGetImagesAndAnnotations.Messages;
using InspectionGetImagesAndAnnotations.Messages.Dtos;

namespace InspectionGetImagesAndAnnotations.Controllers.DtoFactory;
public class DtoFactory : IDtoFactory
{
    public object CreateDto(string dtoType, params object[] args)
    {
        if (args == null || args.Length == 0)
            throw new ArgumentException("Arguments cannot be null or empty.");

        switch (dtoType.ToLower())
        {
        
            case "inspectiondto":
                if (args.Length < 2 || !(args[0] is int) || !(args[1] is string) || !(args[2] is string))
                    throw new ArgumentException("Invalid arguments for messageRequest.");

                return new InspectionRequest
                {
                    numOfImages = (int)args[0],
                    county = (string)args[1],
                    website = (string)args[2]
                };

            default:
                throw new ArgumentException($"Invalid DTO type: {dtoType}");
        }
    }

    public object UseDto(string dtoType, object dto)
    {
        if (dto == null)
            throw new ArgumentException("DTO object cannot be null.");

        switch (dtoType.ToLower())
        {
            case "inspectiondto":
                return dto;
            default:
                throw new ArgumentException($"Invalid DTO type: {dtoType}");
        }
    }
}
