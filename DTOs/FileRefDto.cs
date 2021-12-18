namespace AutoMapperProjectionsNullableValueObjectsRepro.DTOs;

public class FileRefDto
{
    public FileRefDto(Guid fileId, string fileType)
    {
        FileId = fileId;
        FileType = fileType ?? throw new ArgumentNullException(nameof(fileType));
    }

    public Guid FileId { get; protected set; }
    public string FileType { get; protected set; }
}
