using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ExampleProject.Model;
using ExampleProject.Model.Common;
using ExampleProject.Repository.Common;


namespace ExampleProject.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=example;Integrated Security=True";
        public async Task<bool> DeletePerson(int id)
        { 
            try
            {
                using (var cnn = new SqlConnection(PersonRepository.connectionString))
                {
                    String query = "DELETE person WHERE person_id = @Id";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@Id", id);
                    comm.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }

        public async Task<List<Person>> GetPeople()
        {
            var People = new List<Person>();
            using (var cnn = new SqlConnection(PersonRepository.connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM person", cnn);
                cnn.Open();
                SqlDataReader rdr = await comm.ExecuteReaderAsync();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Person Person = new Person();
                        Person.Id = rdr.GetInt32(0);
                        Person.FirstName = rdr.GetString(1);
                        Person.LastName = rdr.GetString(2);
                        Person.PAddress = rdr.GetString(3);
                        Person.Email = rdr.GetString(4);
                        People.Add(Person);
                    }
                }
                rdr.Close();
            }
            return People;
        }

        public async Task<Person> GetPerson(int id)
        {
            Person MyPerson = null;
            using (var cnn = new SqlConnection(PersonRepository.connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM person WHERE person_id = @Id", cnn);
                comm.Parameters.AddWithValue("@Id", id);
                cnn.Open();
                SqlDataReader rdr = await comm.ExecuteReaderAsync();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (id == rdr.GetInt32(0))
                        {
                            MyPerson = new Person();
                            MyPerson.Id = id;
                            MyPerson.FirstName = rdr.GetString(1);
                            MyPerson.LastName = rdr.GetString(2);
                            MyPerson.PAddress = rdr.GetString(3);
                            MyPerson.Email = rdr.GetString(4);
                        }
                    }
                }
                rdr.Close();
            }
            return MyPerson;
        }

        public async Task<bool> PostPerson(string firstName, string lastName, string pAddress, string email)
        {
            try
            {
                using (var cnn = new SqlConnection(PersonRepository.connectionString))
                {
                    String query = "INSERT INTO person VALUES (@FirstName,@LastName, @Address,@Email)";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@FirstName", firstName);
                    comm.Parameters.AddWithValue("@LastName", lastName);
                    comm.Parameters.AddWithValue("@Address", pAddress);
                    comm.Parameters.AddWithValue("@Email", email);
                    comm.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> UpdatePerson(int id, PersonInfo personInfo)
        {
            try
            {
                using (var cnn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE person SET first_name = @FirstName, last_name = @LastName, p_address = @Adress, email = @Email WHERE person_id = @Id";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@FirstName", personInfo.FirstName.ToString());
                    comm.Parameters.AddWithValue("@LastName", personInfo.LastName.ToString());
                    comm.Parameters.AddWithValue("@Adress", personInfo.PAddress.ToString());
                    comm.Parameters.AddWithValue("@Email", personInfo.Email.ToString());
                    comm.Parameters.AddWithValue("@Id", id);
                    comm.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }
    }
}
