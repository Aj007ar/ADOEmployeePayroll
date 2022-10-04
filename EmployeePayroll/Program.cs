namespace EmployeePayroll
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to employee Payroll");
            EmployeeRepo repo = new EmployeeRepo();
            repo.GetAllEmployee();
            
        }
    }
}