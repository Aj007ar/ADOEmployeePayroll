namespace EmployeePayroll
{
    internal class Program
    {
        private static EmployeePayroll employee;

        public static void display()
        {
            Console.WriteLine("Welcome to employee Payroll");
            Console.WriteLine("1.Get all details\n2.Add employee details\n3.update employee details\n4.Get employee details by date");
            Console.WriteLine("enter your choice");
            int choice = Convert.ToInt32(Console.ReadLine());
            EmployeeRepo repo = new EmployeeRepo();
            switch (choice)
            {
                
                case 1:repo.GetAllEmployee();
                    display();
                    break;
                case 2:
                    repo.AddEmployee(employee);
                    display();
                    break;
                case 3:
                    repo.UpdateEmployee();
                    display();
                    break;
                case 4:
                    repo.GetEmployeeDetailsByDate();
                    display();
                    break;
            }
        }
        static void Main(string[] args)
        {
            display();
            
        }
    }
}