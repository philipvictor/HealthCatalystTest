using HealthCatalystUserSearchAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthCatalystUserSearchAPI.UnitTests
{
    public class DbContextMocker
    {
        public static UserDbContext GetTestingDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new UserDbContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
