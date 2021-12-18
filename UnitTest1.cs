using AutoMapper;
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

    private static void SetUpDb()
    {
        using (var db = new AppDataContext())
        {
            
            db.Messages.RemoveRange(db.Messages);
            db.SaveChanges();
        }

        using (var db = new AppDataContext())
        {
            db.Messages.Add(new Message(body: "myBody"));
            db.SaveChanges();
        }
    }

    private static IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FileRef, FileRefDto>();
            cfg.CreateMap<Message, MessageDto>();
        });

        config.AssertConfigurationIsValid();

        return config.CreateMapper();
    }
  
    [TestMethod]
    public void GetMessage_NoProjection()
    {
        SetUpDb();
        var mapper = GetMapper();

        using var db = new AppDataContext();
        var message = db.Messages.Single();
        var messageDto = mapper.Map<MessageDto>(message);

        Assert.IsTrue(messageDto.Id > 0);
        Assert.AreEqual("myBody", messageDto.Body);
        Assert.IsNull(messageDto.Attachment);
    }

    [TestMethod]
    public void GetMessage_Projection()
    {
        SetUpDb();
        var mapper = GetMapper();

        using var db = new AppDataContext();
        var messageDto = mapper.ProjectTo<MessageDto>(db.Messages.AsNoTracking()).Single();

        Assert.IsTrue(messageDto.Id > 0);
        Assert.AreEqual("myBody", messageDto.Body);
        Assert.IsNull(messageDto.Attachment);
    }
}
