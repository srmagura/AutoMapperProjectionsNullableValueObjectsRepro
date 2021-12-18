using System.ComponentModel.DataAnnotations;

namespace AutoMapperProjectionsNullableValueObjectsRepro;

public class Message
{
    public Message(string body)
    {
        Body = body ?? throw new ArgumentNullException(nameof(body));
    }

    public int Id { get; protected set; }

    [MaxLength(1024)]
    public string Body { get; protected set; }

    public FileRef? Attachment { get; protected set; }
}
