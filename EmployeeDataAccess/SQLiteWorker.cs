using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Dapper;

namespace EmployeeDataAccess
{
    public class SQLiteWorker
    {
        protected SQLiteCommand Command;
        static string _connectionString;

        public SQLiteWorker()
        {
             _connectionString = ConfigurationManager.ConnectionStrings["EmployeeDataAccess.SQLiteConnection"].ToString();
        }

        public IEnumerable<EmployeeModel> GetEmploees()
        {
            IEnumerable<EmployeeModel> employees;
            
            using (IDbConnection connection = new SQLiteConnection(_connectionString) )
            {
                employees = connection.Query<EmployeeModel>($"select * from employees").ToList();
            }

            return employees;
        }

        public EmployeeModel GetEmployee(int id)
        {
            EmployeeModel employee;
            using (IDbConnection connection = new SQLiteConnection(_connectionString))
            {
                employee = connection.Query<EmployeeModel>($"select * from employees where id = {id}").FirstOrDefault();
            }

            return employee;
        }

        public EmployeeModel AddNewEmployee(EmployeeModel employee)
        {

            using (IDbConnection connection = new SQLiteConnection(_connectionString))
            {
                  employee.Id =  connection.Query<int>(
                        "Insert into Employees (FirstName, LastName, Gender) " +
                        $"values ('{employee.FirstName}', '{employee.LastName}', '{employee.Gender}'); " +
                        "select last_insert_rowid()").FirstOrDefault();
            }

            return employee;
        }

        public void DeleteEmpolyee(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute($"delete from Employees where id={id}");
            }
        }

        public void UpdateEmployee(int id, EmployeeModel employee)
        {
            using (IDbConnection connection =  new SQLiteConnection(_connectionString))
            {
                connection.Execute($"update Employees set FirstName = '{employee.FirstName}', " +
                                   $"LastName = '{employee.LastName}'," +
                                   $"Gender = '{employee.Gender}' where id = {id}");
            }
        }

        public bool FindeUser(string login, string password)
        {

            using (IDbConnection connection = new SQLiteConnection(_connectionString))
            {
              var  user = connection.Query<UserModel>(
                        $"select * from users where username='{login}' and userpassword = '{password}'").FirstOrDefault();
                return user != null;
                

            }
        }
    }
}
