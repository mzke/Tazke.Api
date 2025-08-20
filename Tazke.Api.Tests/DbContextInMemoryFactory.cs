using Microsoft.EntityFrameworkCore;
using Tazke.Api.Data;

public static class DbContextTestUtils
{
    public static AppDbContext CreateInMemoryContext(string? dbName = null)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName ?? Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
