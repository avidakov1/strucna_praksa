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
        public async Task<bool> DeletePerson(Guid id)
        { 
            try
            {
                using (var cnn = new SqlConnection(PersonRepository.connectionString))
                {
                    String query = "DELETE person WHERE id = @Id";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@Id", id);
                    await comm.ExecuteNonQueryAsync();
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
                SqlCommand comm = new SqlCommand("select * from person;", cnn);
                cnn.Open();
                SqlDataReader rdr = await comm.ExecuteReaderAsync();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Person Person = new Person();
                        Person.Id = rdr.GetGuid(0);
                        Person.FirstName = rdr.GetString(1);
                        Person.LastName = rdr.GetString(2);
                        Person.DOB = rdr.GetDateTime(3);
                        People.Add(Person);
                    }
                }
                rdr.Close();
            }
            return People;
        }

        public async Task<Person> GetPerson(Guid id)
        {
            Person MyPerson = null;
            using (var cnn = new SqlConnection(PersonRepository.connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM person WHERE id = @Id", cnn);
                comm.Parameters.AddWithValue("@Id", id);
                cnn.Open();
                SqlDataReader rdr = await comm.ExecuteReaderAsync();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (id == rdr.GetGuid(0))
                        {
                            MyPerson = new Person();
                            MyPerson.Id = id;
                            MyPerson.FirstName = rdr.GetString(1);
                            MyPerson.LastName = rdr.GetString(2);
                            MyPerson.DOB = rdr.GetDateTime(3);
                        }
                    }
                }
                rdr.Close();
            }
            return MyPerson;
        }

        public async Task<bool> PostPerson(PersonInfo person)
        {
            try
            {
                using (var cnn = new SqlConnection(PersonRepository.connectionString))
                {
                    String query = "INSERT INTO person VALUES (default,@FirstName,@LastName, @Dob);";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@FirstName", person.FirstName);
                    comm.Parameters.AddWithValue("@LastName", person.LastName);
                    comm.Parameters.AddWithValue("@Dob", person.DOB);
                    await comm.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Here");
                return false;
            }
        }

        public async Task<bool> UpdatePerson(Guid id, PersonInfo personInfo)
        {
            try
            {
                using (var cnn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE person SET first_name = @FirstName, last_name = @LastName, date_of_birth = @DOB WHERE id = @Id";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@FirstName", personInfo.FirstName);
                    comm.Parameters.AddWithValue("@LastName", personInfo.LastName);
                    comm.Parameters.AddWithValue("@DOB", personInfo.DOB);
                    comm.Parameters.AddWithValue("@Id", id);
                    await comm.ExecuteNonQueryAsync();
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
