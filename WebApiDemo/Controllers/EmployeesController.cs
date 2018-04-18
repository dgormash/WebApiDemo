using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Batch;
using EmployeeDataAccess;

namespace WebApiDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        protected SQLiteWorker DBaseWorker = new SQLiteWorker();
        public HttpResponseMessage Get(string gender = "All")
        {
            switch (gender.ToLower())
            {
                case "all":
                    return Request.CreateResponse(HttpStatusCode.OK, DBaseWorker.GetEmploees().ToList());
                case "male":
                    return Request.CreateResponse(HttpStatusCode.OK, DBaseWorker.GetEmploees().Where(e => e.Gender.ToLower() == "м").ToList());
                case "female":
                    return Request.CreateResponse(HttpStatusCode.OK, DBaseWorker.GetEmploees().Where(e => e.Gender.ToLower() == "ж").ToList());
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        $"Value for gender must be All, Male or Female. {gender} is not valid parameter");
            }
        }

        public HttpResponseMessage Get(int id)
        {
            var employee = DBaseWorker.GetEmployee(id);
            return employee != null ? Request.CreateResponse(HttpStatusCode.OK, employee) : 
                Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with {id} wasn't found");
        }

        public HttpResponseMessage Post([FromBody]EmployeeModel employee)
        {

            try
            {
                DBaseWorker.AddNewEmployee(employee);
                var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                message.Headers.Location = new Uri($"{Request.RequestUri}\\{employee.Id}");
                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var employee = DBaseWorker.GetEmployee(id);

                if (employee == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There's no employee with id {id}");

                DBaseWorker.DeleteEmpolyee(employee.Id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPut]
        public HttpResponseMessage Update(int id, EmployeeModel employee)
        {
            try
            {
                var emp = DBaseWorker.GetEmployee(id);
                if (emp == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There's no such employee with id {id}");

                DBaseWorker.UpdateEmployee(id, employee);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
