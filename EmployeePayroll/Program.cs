using System.Net.Http.Headers;

namespace EmployeePayroll
{
    internal class Program
    {

        static void Main(string[] args)
        {
            EmployeeRepo repo = new EmployeeRepo();
            repo.GetAllEmployee(@"SELECT * FROM Payroll;");
            repo.UpdateEmployee();
            repo.GetEmployeeDetailsByDate();
            //repo.UsingDatabaseFunction();
            Payroll payroll = new Payroll();
            EmployeePayroll employeePayroll = new EmployeePayroll();
            Department department = new Department();
            repo.AddEmployeeToPayroll(payroll, employeePayroll, department);
        }
    }
}