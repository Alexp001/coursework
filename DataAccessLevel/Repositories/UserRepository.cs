using DataAccessLevel.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLevel.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, userLogin, userPassword FROM Users";
                SqlCommand command = new SqlCommand(quary, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    users.Add(user);
                }
            }
            return users;
        }
        public User GetById(int id)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, userLogin, userPassword FROM Users WHERE id = @id";
                SqlParameter parameter = new SqlParameter("@id", id);

                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }
            }
            return user;
        }
        public int Create(User item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@userLogin", item.Login);
                parameters[1] = new SqlParameter("@userPassword", item.Password);
                string quary = "INSERT INTO Users (userLogin, userPassword)" +
                    " VALUES (@userLogin, @userPassword); select scope_identity()";
                SqlCommand command = new SqlCommand(quary, connection);

                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                object obj = command.ExecuteScalar();
                return Convert.ToInt32(obj);
            }
        }

        public void Update(User item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@id", item.Id);
                parameters[1] = new SqlParameter("@userLogin", item.Login);
                parameters[2] = new SqlParameter("@userPassword", item.Password);
                string quary = "UPDATE Users SET userLogin = @userLogin, userPassword = @userPassword" +
                    " WHERE id = @id";
                SqlCommand command = new SqlCommand(quary, connection);
                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "DELETE FROM Users where id=@id";
                SqlParameter parameter = new SqlParameter("@id", id);
                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
