using System.ComponentModel.DataAnnotations;

namespace AutoMapperProjectionsNullableValueObjectsRepro.Domain;

public class Message
{
#nullable disable
    [Obsolete("For Entity Framework only.")]
    protected Message() { }
#nullable enable

    public Message(string body, MessageMetadata metadata)
    {
        Body = body ?? throw new ArgumentNullException(nameof(body));
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
    }

    public int Id { get; protected set; }

    [MaxLength(1024)]
    public string Body { get; protected set; }

    public MessageMetadata Metadata { get; protected set; }
}
