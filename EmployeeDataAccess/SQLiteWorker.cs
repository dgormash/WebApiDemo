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
        protected SQLiteConnection Connection;
        protected SQLiteCommand Command;

        public SQLiteWorker()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["EmployeeDataAccess.SQLiteConnection"].ToString();
            Connection = new SQLiteConnection(connectionString);
        }

        public IEnumerable<EmployeeModel> GetEmploees()
        {
            IEnumerable<EmployeeModel> employees;
            
            using (IDbConnection connection = Connection )
            {
                employees = connection.Query<EmployeeModel>("select * from employees").ToList();
            }

            return employees;
        }

        public EmployeeModel GetEmployee(int id)
        {
            EmployeeModel employee;
            using (IDbConnection connection = Connection)
            {
                employee = connection.Query<EmployeeModel>($"select * from employees where id = {id}").FirstOrDefault();
            }

            return employee;
        }
    }
}
