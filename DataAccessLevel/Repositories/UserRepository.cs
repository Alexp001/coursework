using DataAccessLevel.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, userLogin, userPassword FROM Users";
                MySqlCommand command = new MySqlCommand(quary, connection);
                MySqlDataReader reader = command.ExecuteReader();
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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, userLogin, userPassword FROM Users WHERE id = @id";
                MySqlParameter parameter = new MySqlParameter("@id", id);

                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }
            }
            return user;
        }
        public int Create(User item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@userLogin", item.Login);
                parameters[1] = new MySqlParameter("@userPassword", item.Password);
                string quary = "INSERT INTO Users (userLogin, userPassword)" +
                    " VALUES (@userLogin, @userPassword); SELECT LAST_INSERT_ID();";
                MySqlCommand command = new MySqlCommand(quary, connection);

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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[3];
                parameters[0] = new MySqlParameter("@id", item.Id);
                parameters[1] = new MySqlParameter("@userLogin", item.Login);
                parameters[2] = new MySqlParameter("@userPassword", item.Password);
                string quary = "UPDATE Users SET userLogin = @userLogin, userPassword = @userPassword" +
                    " WHERE id = @id";
                MySqlCommand command = new MySqlCommand(quary, connection);
                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "DELETE FROM Users where id=@id";
                MySqlParameter parameter = new MySqlParameter("@id", id);
                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
