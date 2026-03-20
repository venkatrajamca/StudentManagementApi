using StudentManagementApi.Models;
using System.Data;
using Microsoft.Data.SqlClient;
namespace StudentManagementApi
{
    public class StudentRepository
    {
        private readonly string _connectionString;
        public StudentRepository(IConfiguration oConfiguration)
        {
            _connectionString = oConfiguration.GetConnectionString("Conn");
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetAllUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                UserID = (int)reader["UserID"],
                                Name = (string)reader["Name"],
                                Email = (string)reader["Email"],
                                UserType = (int)reader["UserType"],
                                UserName = (string)reader["UserName"],
                                Password = (string)reader["Password"],
                                Status = (string)reader["Status"]
                            };
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
        public void InserUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_InsertUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@UserType", user.UserType);
                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Status", user.Status);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_UpdateUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", user.UserID);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@UserType", user.UserType);
                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Status", user.Status);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public User GetUserById(int id)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_GetUserById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserID = (int)reader["UserID"],
                                Name = (string)reader["Name"],
                                Email = (string)reader["Email"],
                                UserType = (int)reader["UserType"],
                                UserName = (string)reader["UserName"],
                                Password = (string)reader["Password"],
                                Status = (string)reader["Status"]
                            };
                        }
                    }
                }
            }
            return user;
        }
    }
}
