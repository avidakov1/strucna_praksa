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
            var persons = new List<PersonDTO>();
            using (var cnn = new SqlConnection(ReadWriteController.connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM person",cnn);
                cnn.Open();
                SqlDataReader rdr = comm.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        persons.Add(new PersonDTO(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4)));
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
            var persons = new List<PersonInfoDTO>();
            using (var cnn = new SqlConnection(ReadWriteController.connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT first_name,last_name,position,pay,c_name FROM person RIGHT JOIN employee ON person.person_id = employee.person_id RIGHT JOIN company ON employee.company_id = company.company_id ", cnn);
                cnn.Open();
                SqlDataReader rdr = comm.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        persons.Add(new PersonInfoDTO(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2),rdr.GetDouble(3), rdr.GetString(4)));
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
            PersonDTO searchedPerson = null;
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
                            searchedPerson = new PersonDTO(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4));
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
        public HttpResponseMessage Post([FromBody]PersonDTO value)
        {
            try
            {
                using (var cnn = new SqlConnection(ReadWriteController.connectionString))
                {
                    String query = "INSERT INTO person VALUES (@FirstName,@LastName, @Address,@Email)";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@FirstName", value.FirstName);
                    comm.Parameters.AddWithValue("@LastName", value.LastName);
                    comm.Parameters.AddWithValue("@Address", value.PAddress);
                    comm.Parameters.AddWithValue("@Email", value.Email);
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
        public HttpResponseMessage Put([FromUri] int id, [FromBody]PersonDTO value)
        {
            try
            {
                using (var cnn = new SqlConnection(ReadWriteController.connectionString))
                {
                    String query = "UPDATE person SET first_name = @FirstName, last_name = @LastName, p_adress = @Adress, email = @Email WHERE person_id = @Id";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@FirstName", value.FirstName);
                    comm.Parameters.AddWithValue("@LastName", value.LastName);
                    comm.Parameters.AddWithValue("@Adress", value.PAddress);
                    comm.Parameters.AddWithValue("@Email", value.Email);
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
    public class PersonDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PAddress { get; set; }
        public string Email { get; set; }

        public PersonDTO (string pFirstName, string pLastName, string pAddress, string pEmail)
        {
            this.FirstName = pFirstName; this.LastName = pLastName; 
            this.PAddress = pAddress; this.Email = pEmail;
        }
    }
    public class PersonInfoDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public double Pay { get; set; }
        public string CompanyName { get; set; }

        public PersonInfoDTO(string pFirstName, string pLastName, string position, double pay, string companyName)
        {
            this.FirstName = pFirstName; this.LastName = pLastName;
            this.Position = position; this.Pay = pay; this.CompanyName = companyName;
        }
    }
}
