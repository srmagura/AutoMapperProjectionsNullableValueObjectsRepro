namespace AutoMapperProjectionsNullableValueObjectsRepro.DTOs;

public class MessageDto
{
    public MessageDto(string body, MessageMetadataDto metadata)
    {
        Body = body ?? throw new ArgumentNullException(nameof(body));
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
    }

    public int Id { get; set; }

    public string Body { get; set; }
    public MessageMetadataDto Metadata { get; set; }
}
