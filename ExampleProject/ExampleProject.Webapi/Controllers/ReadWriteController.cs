using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExampleProject.Webapi.Controllers
{
    
    public class EmployeeInfo
    {
        public string name { get; set; }
        public string workPosition { get; set; }
        public float monthlyPay { get; set; }
    }
    public class Employee
    {
        public string id { get; set; }
        public string name { get; set; }
        public string workPosition { get; set; }
        public float monthlyPay { get; set; }
        public Employee(string id) { this.id = id; }
        public Employee(string id, string name, string workPosition, float monthlyPay) { this.id = id; this.name = name; this.workPosition = workPosition; this.monthlyPay = monthlyPay; }

        public void ReplaceInfo(EmployeeInfo EI)
        {
            this.name = EI.name;
            this.workPosition = EI.workPosition;
            this.monthlyPay = EI.monthlyPay;
        }
    }
    
    public class ReadWriteController : ApiController
    {
        static public List<Employee> empleyees = new List<Employee> { new Employee("0", "Tim", "Banker", 10000) };
        [HttpGet]
        [Route("api/getall")]
        public HttpResponseMessage Get()
        {
            if (empleyees.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, empleyees);
            }
            else 
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "List is empty");
            }
        }
        [HttpGet]
        [Route("api/getone/{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            foreach (Employee employee in empleyees)
            {
                if (employee.id == id.ToString())
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, employee.name+" \n "+employee.workPosition+" \n "+employee.monthlyPay);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
        }

        // POST: api/ReadWrite
        [Route("api/add")]
        public HttpResponseMessage Post([FromBody]EmployeeInfo value)
        {
            if (value != null)
            {
                int Id = empleyees.Count;
                Employee newEmp = new Employee(Id.ToString(), value.name, value.workPosition, value.monthlyPay);
                empleyees.Add(newEmp);
                return Request.CreateErrorResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }

        [Route("api/update/{id:int}")]
        public HttpResponseMessage Put([FromUri] int id, [FromBody]EmployeeInfo value)
        {
            int iterator = 0;
            foreach (Employee employee in empleyees)
            {
                if (employee.id == id.ToString())
                {
                    empleyees[iterator].ReplaceInfo(value);
                }
                iterator = iterator + 1; 
                return Request.CreateResponse(HttpStatusCode.OK, "Change made successfully.");

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
        [Route("api/remove/{id:int}")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            var OldEmps = new List<Employee>(empleyees);
            empleyees.RemoveAll(r => r.id==id.ToString());
            if (OldEmps != empleyees)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "NotFound");
        }
    }

}
