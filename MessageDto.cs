namespace AutoMapperProjectionsNullableValueObjectsRepro;

public class MessageDto
{
    public MessageDto(string body, FileRef? attachment)
    {
        Body = body ?? throw new ArgumentNullException(nameof(body));
        Attachment = attachment;
    }

    public int Id { get; set; }

    public string Body { get; set; }
    public FileRef? Attachment { get; set; }
}
