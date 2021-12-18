using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AutoMapperProjectionsNullableValueObjectsRepro;

[Owned]
public record FileRef
{
    public FileRef(Guid fileId, string fileType)
    {
        FileId = fileId;
        FileType = fileType ?? throw new ArgumentNullException(nameof(fileType));
    }

    public Guid FileId { get; protected init; }
    
    [MaxLength(32)]
    public string FileType { get; protected init; }
}
