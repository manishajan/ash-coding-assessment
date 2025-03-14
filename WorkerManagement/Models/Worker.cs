namespace WorkerManagement.Models
{
    // Represents a base worker in the system.
    // Every worker has common properties like Name and Address.
    // Employees, Supervisors, and Managers inherit from this.
    public class Worker
    {
        public int WorkerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public int WorkerID { get; set; }  // Foreign key to Worker
        public decimal PayPerHour { get; set; }

        public Worker Worker { get; set; }
    }

    public class Supervisor
    {
        public int SupervisorID { get; set; }
        public int WorkerID { get; set; }  // Foreign key to Worker
        public decimal AnnualSalary { get; set; }

        public Worker Worker { get; set; }
    }

    public class Manager
    {
        public int ManagerID { get; set; }
        public int WorkerID { get; set; }  // Foreign key to Worker
        public decimal AnnualSalary { get; set; }
        public decimal MaxExpenseAmount { get; set; }

        public Worker Worker { get; set; }
    }

}
