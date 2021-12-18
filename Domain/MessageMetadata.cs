using Microsoft.EntityFrameworkCore;

namespace AutoMapperProjectionsNullableValueObjectsRepro.Domain;

// Value object
[Owned]
public record MessageMetadata
{
#nullable disable
    [Obsolete("For Entity Framework only.")]
    protected MessageMetadata() { }
#nullable enable

    public MessageMetadata(FileRef? attachment)
    {
        Attachment = attachment;
    }

    public FileRef? Attachment { get; protected init; }
}
