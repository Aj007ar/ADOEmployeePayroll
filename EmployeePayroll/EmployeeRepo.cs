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
        public int UpdateEmployee()
        {
            EmployeePayroll emp = new EmployeePayroll();
            emp.EmployeeName = "john";
            emp.BasicPay = 300000;
            DateTime dateTime = Convert.ToDateTime(2022 - 05 - 01);
            emp.City = "bangalore";
            emp.Contact = "9876543210";
            emp.Deductions = 5000;
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("GetCompleteDetails", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Name", emp.EmployeeName);
                    sqlCommand.Parameters.AddWithValue("@BasicPay", emp.BasicPay);
                    sqlCommand.Parameters.AddWithValue("@startdate", emp.StartDate);
                    sqlCommand.Parameters.AddWithValue("@address", emp.City);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", emp.Contact);
                    sqlCommand.Parameters.AddWithValue("@Deduction", emp.Deductions);


                    int result = sqlCommand.ExecuteNonQuery();
                    if (result == 1)
                        Console.WriteLine("employee details are updated...");
                    else
                        Console.WriteLine("details are not updated!");
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        static void readDataRows(SqlDataReader dr, EmployeePayroll employeePayroll)
        {
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    employeePayroll.EmployeeName = dr.GetString(1);
                    employeePayroll.BasicPay = dr.GetDouble(2);
                    employeePayroll.StartDate = dr.GetDateTime(3);
                    employeePayroll.Gender = dr.GetString(4);
                    employeePayroll.Contact = dr.GetString(5);
                    employeePayroll.City = dr.GetString(6);
                    employeePayroll.Deductions = dr.GetDouble(8);
                    employeePayroll.TaxPercent = dr.GetDouble(9);

                    //Display retrieved record
                    Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", employeePayroll.EmployeeName, employeePayroll.BasicPay, employeePayroll.StartDate, employeePayroll.Gender,employeePayroll.Contact,employeePayroll.City,employeePayroll.Deductions,employeePayroll.TaxPercent);
                    Console.WriteLine("\n");
                }
            }
            else
            {
                Console.WriteLine("No data found!");
            }

        }

        public void GetEmployeeDetailsByDate()
        {
            EmployeePayroll employee = new EmployeePayroll();
            DateTime startDate = new DateTime(2015, 01, 02);
            DateTime endDate = new DateTime(2020, 04, 15);
            try
            {
                //establish connection
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("spGetDataByDateRange", connection);
                    //stored procedure
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    //pass parameters
                    sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
                    sqlCommand.Parameters.AddWithValue("@EndDate", endDate);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    //read all rows & display data
                    readDataRows(reader, employee);
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //close connection
                connection.Close();
            }
        }
    }
}
