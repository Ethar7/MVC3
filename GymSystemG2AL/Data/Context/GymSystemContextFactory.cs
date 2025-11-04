using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class GymSystemContextFactory : IDesignTimeDbContextFactory<GymSystemDBContext>
{
    public GymSystemDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GymSystemDBContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=GymSystemDB;User Id=sa;Password=Ethar2025@#;TrustServerCertificate=True;");

        return new GymSystemDBContext(optionsBuilder.Options);
    }
}
