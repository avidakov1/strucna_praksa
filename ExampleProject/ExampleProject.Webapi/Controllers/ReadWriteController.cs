using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


namespace ExampleProject.Webapi.Controllers
{    
    public class ReadWriteController : ApiController
    {
        private static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=example;Integrated Security=True";
        [HttpGet]
        [Route("api/getallPerson")]
        public HttpResponseMessage Get()
        {
            var persons = new List<personDTO>();
            using (var cnn = new SqlConnection(ReadWriteController.connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM person",cnn);
                cnn.Open();
                SqlDataReader rdr = comm.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        persons.Add(new personDTO(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4)));
                    }
                }
                rdr.Close();
            }
            if (persons.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, persons);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found!");
        }
        [Route("api/getAllPersonInfo")]
        [HttpGet]
        public HttpResponseMessage GetInfo()
        {
            var persons = new List<personInfoDTO>();
            using (var cnn = new SqlConnection(ReadWriteController.connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT first_name,last_name,position,pay,c_name FROM person RIGHT JOIN employee ON person.person_id = employee.person_id RIGHT JOIN company ON employee.company_id = company.company_id ", cnn);
                cnn.Open();
                SqlDataReader rdr = comm.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        persons.Add(new personInfoDTO(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2),rdr.GetDouble(3), rdr.GetString(4)));
                    }
                }
                rdr.Close();
            }
            if (persons.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, persons);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found!");
        }
        [HttpGet]
        [Route("api/getonePerson/{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            personDTO searchedPerson = null;
            using (var cnn = new SqlConnection(ReadWriteController.connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM person", cnn);
                cnn.Open();
                SqlDataReader rdr = comm.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (rdr.GetInt32(0) == id)
                        {
                            searchedPerson = new personDTO(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4));
                        }
                    }
                }
                rdr.Close();
            }
            if (searchedPerson != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, searchedPerson);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found!");

        }

        [Route("api/addPerson")]
        public HttpResponseMessage Post([FromBody]personDTO value)
        {
            try
            {
                using (var cnn = new SqlConnection(ReadWriteController.connectionString))
                {
                    String query = "INSERT INTO person VALUES (@FirstName,@LastName, @Address,@Email)";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@FirstName", value.first_name);
                    comm.Parameters.AddWithValue("@LastName", value.last_name);
                    comm.Parameters.AddWithValue("@Address", value.p_address);
                    comm.Parameters.AddWithValue("@Email", value.email);
                    comm.ExecuteNonQuery();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Person added");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e);
            }
        }

        [Route("api/updatePerson/{id:int}")]
        public HttpResponseMessage Put([FromUri] int id, [FromBody]personDTO value)
        {
            try
            {
                using (var cnn = new SqlConnection(ReadWriteController.connectionString))
                {
                    String query = "UPDATE person SET first_name = @FirstName, last_name = @LastName, p_adress = @Adress, email = @Email WHERE person_id = @Id";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@FirstName", value.first_name);
                    comm.Parameters.AddWithValue("@LastName", value.last_name);
                    comm.Parameters.AddWithValue("@Adress", value.p_address);
                    comm.Parameters.AddWithValue("@Email", value.email);
                    comm.Parameters.AddWithValue("@Id", id);
                    comm.ExecuteNonQuery();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Person updated");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e);
            }
        }

        [Route("api/remove/{id:int}")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                using (var cnn = new SqlConnection(ReadWriteController.connectionString))
                {
                    String query = "DELETE person WHERE person_id = @Id";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.ExecuteNonQuery();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Person deleted");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e);
            }
        }
    }
    public class personDTO
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string p_address { get; set; }
        public string email { get; set; }

        public personDTO (string pFirstName, string pLastName, string pAdress, string pEmail)
        {
            this.first_name = pFirstName; this.last_name = pLastName; 
            this.p_address = pAdress; this.email = pEmail;
        }
    }
    public class personInfoDTO
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string position { get; set; }
        public double pay { get; set; }
        public string c_name { get; set; }

        public personInfoDTO(string pFirstName, string pLastName, string position, double pay, string companyName)
        {
            this.first_name = pFirstName; this.last_name = pLastName;
            this.position = position; this.pay = pay; this.c_name = companyName;
        }
    }
}
