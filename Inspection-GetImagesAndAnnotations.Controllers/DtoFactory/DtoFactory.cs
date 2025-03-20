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
        
            case "messagedto":
                if (args.Length < 2 || !(args[0] is string))
                    throw new ArgumentException("Invalid arguments for messageRequest.");

                return new MessageRequest
                {
                    Message = (string)args[0]
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
            case "messagedto":
                return dto;
            default:
                throw new ArgumentException($"Invalid DTO type: {dtoType}");
        }
    }
}
