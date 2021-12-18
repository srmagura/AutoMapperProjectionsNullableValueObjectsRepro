using Microsoft.EntityFrameworkCore;

namespace AutoMapperProjectionsNullableValueObjectsRepro.Domain;

public class AppDataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=AutoMapperProjectionsNullableValueObjectsRepro;TrustServerCertificate=True;Trusted_Connection=True;Connection Timeout=180;MultipleActiveResultSets=True;");
    }

    public DbSet<Message> Messages => Set<Message>();

    public void Migrate()
    {
        Database.Migrate();
    }
}
