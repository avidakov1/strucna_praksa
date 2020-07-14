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
        public string Name { get; set; }
        public string WorkPosition { get; set; }
        public float MonthlyPay { get; set; }
    }
    public class Employee
    {
        public string Id;
        public string Name = "John Doe";
        public string WorkPosition = "Salesman";
        public float MonthlyPay = 3000;
        public Employee(string Id) { this.Id = Id; }
        public Employee(string Id, string Name, string WorkPosition, float MonthlyPay) { this.Id = Id; this.Name = Name; this.WorkPosition = WorkPosition; this.MonthlyPay = MonthlyPay; }

        public bool MyId(string Id) => this.Id == Id;
        public string Next() => (Int32.Parse(this.Id) + 1).ToString();
        public void ReplaceInfo(EmployeeInfo EI)
        {
            this.Name = EI.Name;
            this.WorkPosition = EI.WorkPosition;
            this.MonthlyPay = EI.MonthlyPay;
        }
    }

    public static class Employees
    {
        public static List<Employee> Emps = new List<Employee>();
        static Employees() { }
        public static void AddEmp(Employee Emp) => Emps.Add(Emp);

        public static List<Employee> getEmps()
        {
            return Emps;
        }
        public static void setEmps(List<Employee> newEmps)
        {
            Emps = newEmps;
        }
        public static int changeEmp(string Id, EmployeeInfo values)
        {
            int T = Emps.FindIndex(r => r.MyId(Id));
            Emps[T].ReplaceInfo(values);
            return T;
        }

        public static bool deleteId(string Id)
        {
            List<Employee> OldEmps = new List<Employee>(Emps);
            Emps.RemoveAll(r => r.MyId(Id));
            return OldEmps != Emps;
        }
    }
    
    public class ReadWriteController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            return Employees.getEmps();
        }

        public Employee Get(string Id)
        {
            foreach (Employee employee in Employees.getEmps())
            {
                if (employee.MyId(Id))
                {
                    return employee;
                }
            }
            Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
            return null;
        }

        // POST: api/ReadWrite
        [HttpPost]
        public HttpResponseMessage Post([FromBody]EmployeeInfo value)
        {
            if (value != null)
            {
                int Id = Employees.getEmps().Count;
                Employee newEmp = new Employee(Id.ToString(), value.Name, value.WorkPosition, value.MonthlyPay);
                Employees.AddEmp(newEmp);
                return Request.CreateErrorResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }

        public HttpResponseMessage Put(string id, [FromBody]EmployeeInfo value)
        {
            int t = Employees.changeEmp(id, value);
            if(t!= -1)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "NotFound");
        }

        public HttpResponseMessage Delete(string id)
        {
            if (Employees.deleteId(id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "NotFound");
        }
    }

}
