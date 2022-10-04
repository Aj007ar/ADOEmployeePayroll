using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    public class EmployeePayroll
    {
        //entity
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public double BasicPay { get; set; }
        public double Deductions { get; set; }
        public double TaxPercent { get; set; }
        public string Zip { get; set; }
        public DateTime StartDate { get; set; }
        public string States { get; set; }
    }
}
