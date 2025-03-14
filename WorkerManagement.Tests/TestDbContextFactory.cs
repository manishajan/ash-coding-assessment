using WorkerManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace WorkerManagement.Tests
{
    //Inmemorydb for tests
    public static class TestDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}