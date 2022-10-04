using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    public class EmployeeRepo
    {
        public static string connectionString = @"Data Source=LAPTOP-3C6DDPAG;Initial Catalog=PayrollService;Integrated Security=True";
        SqlConnection connection = null;

        public void GetAllEmployee()
        {

            try
            {
                EmployeePayroll employeePayroll = new EmployeePayroll();
                using (connection = new SqlConnection(connectionString))
                {
                    string query = @"select * from EmployeePayroll;";

                    //define SqlCommand Object
                    SqlCommand cmd = new SqlCommand(query, connection);
                    //establish connection
                    connection.Open();
                    Console.WriteLine("connected");
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            employeePayroll.EmployeeId = dataReader.GetInt32(0);
                            employeePayroll.EmployeeName = dataReader.GetString(1);
                            employeePayroll.BasicPay = dataReader.GetDouble(2);
                            employeePayroll.StartDate = dataReader.GetDateTime(3);
                            employeePayroll.Gender = dataReader.GetString(4);

                            //Display retrieved record
                            Console.WriteLine("{0},{1},{2},{3},{4}",employeePayroll.EmployeeId, employeePayroll.EmployeeName, employeePayroll.BasicPay, employeePayroll.StartDate, employeePayroll.Gender);
                            Console.WriteLine("\n");
                        };
                    }
                    else
                    {
                        Console.WriteLine("No data found!");
                    }
                    //close connection
                    dataReader.Close();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool AddEmployee(EmployeePayroll employee)
        {
            try
            {
                using (this.connection)
                {

                    SqlCommand storedProcedure = new SqlCommand("Addmplopyee", this.connection);
                    storedProcedure.CommandType = CommandType.StoredProcedure;


                    storedProcedure.Parameters.AddWithValue("@name", employee.EmployeeName);
                    storedProcedure.Parameters.AddWithValue("@basicpay", employee.BasicPay);
                    storedProcedure.Parameters.AddWithValue("@startdate", employee.StartDate);
                    storedProcedure.Parameters.AddWithValue("@gender", employee.Gender);
                    storedProcedure.Parameters.AddWithValue("@contact", employee.Contact);
                    storedProcedure.Parameters.AddWithValue("@city", employee.City);
                    storedProcedure.Parameters.AddWithValue("@state", employee.States);
                    storedProcedure.Parameters.AddWithValue("@zip", employee.Zip);
                    storedProcedure.Parameters.AddWithValue("@deductions", employee.Deductions);
                    storedProcedure.Parameters.AddWithValue("@taxpercent", employee.TaxPercent);

                    connection.Open();
                    var result = storedProcedure.ExecuteNonQuery();
                    connection.Close();

                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
