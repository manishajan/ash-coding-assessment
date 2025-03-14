using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkerManagement.Data;
using WorkerManagement.Models;

namespace WorkerManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkerController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: api/worker
        //This will return list of workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllWorkers()
        {
            var employees = await _context.Employees
                .Include(e => e.Worker)
                .Select(e => new { Type = "Employee", e.EmployeeID, e.Worker.FirstName, e.Worker.LastName, e.Worker.Address1, e.PayPerHour })
                .ToListAsync();

            var supervisors = await _context.Supervisors
                .Include(s => s.Worker)
                .Select(s => new { Type = "Supervisor", s.SupervisorID, s.Worker.FirstName, s.Worker.LastName, s.Worker.Address1, s.AnnualSalary })
                .ToListAsync();

            var managers = await _context.Managers
                .Include(m => m.Worker)
                .Select(m => new { Type = "Manager", m.ManagerID, m.Worker.FirstName, m.Worker.LastName, m.Worker.Address1, m.AnnualSalary, m.MaxExpenseAmount })
                .ToListAsync();

            var allWorkers = employees.Cast<object>()
                .Concat(supervisors.Cast<object>())
                .Concat(managers.Cast<object>())
                .ToList();

            return Ok(allWorkers);
        }

        //POST: api/worker
        //Add a worker to db
        [HttpPost]
        public ActionResult<object> AddWorker([FromBody] WorkerDTO workerData)
        {
            if (workerData == null)
                return BadRequest("Invalid worker data.");

            var newWorker = new Worker
            {
                FirstName = workerData.FirstName,
                LastName = workerData.LastName,
                Address1 = workerData.Address1
            };

            _context.Workers.Add(newWorker);
            _context.SaveChanges();

            object newRecord = null;

            switch (workerData.Type)
            {
                case "Employee":
                    newRecord = new Employee { WorkerID = newWorker.WorkerID, PayPerHour = workerData.PayPerHour ?? 0 }; //Setting 0 if null in request
                    _context.Employees.Add((Employee)newRecord);
                    break;

                case "Supervisor":
                    newRecord = new Supervisor { WorkerID = newWorker.WorkerID, AnnualSalary = workerData.AnnualSalary ?? 0 }; //Setting 0 if null in request
                    _context.Supervisors.Add((Supervisor)newRecord);
                    break;

                case "Manager":
                    newRecord = new Manager
                    {
                        WorkerID = newWorker.WorkerID,
                        AnnualSalary = workerData.AnnualSalary ?? 0, //Setting 0 if null in request
                        MaxExpenseAmount = workerData.MaxExpenseAmount ?? 0 //Setting 0 if null in request
                    };
                    _context.Managers.Add((Manager)newRecord);
                    break;

                default:
                    return BadRequest("Invalid worker type.");
            }

            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAllWorkers), new { id = newWorker.WorkerID }, newRecord);
        }
    }
}
