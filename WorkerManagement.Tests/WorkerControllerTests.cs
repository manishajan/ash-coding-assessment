using WorkerManagement.Controllers;
using WorkerManagement.Models;
using Microsoft.AspNetCore.Mvc;
using WorkerManagement.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Dynamic;

namespace WorkerManagement.Tests
{
    //Test class for controller
    public class WorkerControllerTests
    {
        private ApplicationDbContext GetMockDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB" + Guid.NewGuid())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Workers.Add(new Worker { WorkerID = 1, FirstName = "Test", LastName = "Employee", Address1 = "123 Test St" });
            context.Employees.Add(new Employee { EmployeeID = 1, WorkerID = 1, PayPerHour = 30 });

            context.SaveChanges();

            return context;
        }

        //Test case to add worker 
        [Fact]
        public void AddWorker_ShouldReturn_Created()
        {
            var context = GetMockDbContext();
            var controller = new WorkerController(context);

            var workerData = new WorkerDTO
            {
                Type = "Employee",
                FirstName = "John",
                LastName = "Doe",
                Address1 = "123 Main St",
                PayPerHour = 40
            };

            var result = controller.AddWorker(workerData);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(actionResult.Value);
        }

        //Test case to get the workers as a list
        [Fact]
        public async Task GetAllWorkers_ShouldReturn_Workers()
        {
            var context = GetMockDbContext();
            var controller = new WorkerController(context);

            var result = await controller.GetAllWorkers();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var workers = Assert.IsType<List<object>>(actionResult.Value);
            Assert.Single(workers);
        }


    }

}
