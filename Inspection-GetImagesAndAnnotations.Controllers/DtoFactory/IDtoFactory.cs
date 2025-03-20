namespace InspectionGetImagesAndAnnotations.Controllers.DtoFactory;
public interface IDtoFactory
{
    object CreateDto(string dtoType, params object[] args);

    object UseDto(string dtoType, object dto);
}
