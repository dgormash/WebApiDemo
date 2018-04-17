using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace WebApiDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        protected SQLiteWorker DBaseWorker = new SQLiteWorker();
        public IEnumerable<EmployeeModel> Get()
        {
            return DBaseWorker.GetEmploees().ToList();
        }

        public EmployeeModel Get(int id)
        {
            return DBaseWorker.GetEmployee(id);
        }
    }
}
