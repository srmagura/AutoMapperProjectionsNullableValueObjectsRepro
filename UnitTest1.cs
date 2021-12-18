using AutoMapper;
using AutoMapperProjectionsNullableValueObjectsRepro.Domain;
using AutoMapperProjectionsNullableValueObjectsRepro.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoMapperProjectionsNullableValueObjectsRepro;

[TestClass]
public class UnitTest1
{
    [AssemblyInitialize]
    public static void AssemblyInitialze(TestContext _)
    {
        using var db = new AppDataContext();
        db.Migrate();
    }

    private static void SetUpDb(bool hasAttachment)
    {
        using (var db = new AppDataContext())
        {

            db.Messages.RemoveRange(db.Messages);
            db.SaveChanges();
        }

        using (var db = new AppDataContext())
        {
            var attachment = hasAttachment ? new FileRef(Guid.NewGuid(), "image/png") : null;

            db.Messages.Add(new Message(body: "myBody", metadata: new MessageMetadata(attachment)));
            db.SaveChanges();
        }
    }

    private static IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FileRef, FileRefDto>();
            cfg.CreateMap<MessageMetadata, MessageMetadataDto>();
            cfg.CreateMap<Message, MessageDto>();
        });

        config.AssertConfigurationIsValid();

        return config.CreateMapper();
    }

    private static void ValidateMessageDto(MessageDto messageDto, bool hasAttachment)
    {
        Assert.IsTrue(messageDto.Id > 0);
        Assert.AreEqual("myBody", messageDto.Body);
        Assert.IsNotNull(messageDto.Metadata);

        if (hasAttachment)
        {
            Assert.IsNotNull(messageDto.Metadata.Attachment);
        }
        else
        {
            Assert.IsNull(messageDto.Metadata.Attachment);
        }
    }

    [TestMethod]
    public void NullAttachment_NoProjection()
    {
        SetUpDb(hasAttachment: false);
        var mapper = GetMapper();

        using var db = new AppDataContext();
        var message = db.Messages.Single();
        var messageDto = mapper.Map<MessageDto>(message);

        ValidateMessageDto(messageDto, hasAttachment: false);
    }

    [TestMethod]
    public void NullAttachment_Projection()
    {
        SetUpDb(hasAttachment: false);
        var mapper = GetMapper();

        using var db = new AppDataContext();
        var messageDto = mapper.ProjectTo<MessageDto>(db.Messages.AsNoTracking()).Single(); // ERROR

        ValidateMessageDto(messageDto, hasAttachment: false);
    }

    [TestMethod]
    public void NonNullAttachment_NoProjection()
    {
        SetUpDb(hasAttachment: true);
        var mapper = GetMapper();

        using var db = new AppDataContext();
        var message = db.Messages.Single();
        var messageDto = mapper.Map<MessageDto>(message);

        ValidateMessageDto(messageDto, hasAttachment: true);
    }

    [TestMethod]
    public void NonNullAttachment_Projection()
    {
        SetUpDb(hasAttachment: true);
        var mapper = GetMapper();

        using var db = new AppDataContext();
        var messageDto = mapper.ProjectTo<MessageDto>(db.Messages.AsNoTracking()).Single();

        ValidateMessageDto(messageDto, hasAttachment: true);
    }
}
