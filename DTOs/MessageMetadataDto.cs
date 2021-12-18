namespace AutoMapperProjectionsNullableValueObjectsRepro.DTOs;

public class MessageMetadataDto
{
    public MessageMetadataDto(FileRefDto? attachment)
    {
        Attachment = attachment;
    }

    public FileRefDto? Attachment { get; set; }
}
