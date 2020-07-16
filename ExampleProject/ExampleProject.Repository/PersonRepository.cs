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
        public bool DeletePerson(int id)
        { 
            try
            {
                using (var cnn = new SqlConnection(PersonRepository.connectionString))
                {
                    String query = "DELETE person WHERE person_id = @Id";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@Id", id);
                    comm.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }

        public List<IPerson> GetPeople()
        {
            var People = new List<IPerson>();
            using (var cnn = new SqlConnection(PersonRepository.connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM person", cnn);
                cnn.Open();
                SqlDataReader rdr = comm.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        IPerson Person = new Person();
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

        public IPerson GetPerson(int id)
        {
            IPerson MyPerson = null;
            using (var cnn = new SqlConnection(PersonRepository.connectionString))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM person WHERE person_id = @Id", cnn);
                comm.Parameters.AddWithValue("@Id", id);
                cnn.Open();
                SqlDataReader rdr = comm.ExecuteReader();
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

        public bool PostPerson(string firstName, string lastName, string pAddress, string email)
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
                    comm.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }

        public bool UpdatePerson(int id, PersonInfo personInfo)
        {
            try
            {
                using (var cnn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE person SET first_name = @FirstName, last_name = @LastName, p_adress = @Adress, email = @Email WHERE person_id = @Id";
                    cnn.Open();
                    SqlCommand comm = new SqlCommand(query, cnn);
                    comm.Parameters.AddWithValue("@FirstName", personInfo.FirstName);
                    comm.Parameters.AddWithValue("@LastName", personInfo.LastName);
                    comm.Parameters.AddWithValue("@Adress", personInfo.PAddress);
                    comm.Parameters.AddWithValue("@Email", personInfo.Email);
                    comm.Parameters.AddWithValue("@Id", id);
                    comm.ExecuteNonQuery();
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
