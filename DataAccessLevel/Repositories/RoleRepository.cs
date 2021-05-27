using DataAccessLevel.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLevel.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly string _connectionString;
        public RoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Role> GetAll()
        {
            List<Role> roles = new List<Role>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameRole FROM Roles";
                MySqlCommand command = new MySqlCommand(quary, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Role role = new Role(reader.GetInt32(0), reader.GetString(1));
                    roles.Add(role);
                }
            }
            return roles;
        }
        public Role GetById(int id)
        {
            Role role = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameRole FROM Roles WHERE id = @id";
                MySqlParameter parameter = new MySqlParameter("@id", id);

                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    role = new Role(reader.GetInt32(0), reader.GetString(1));
                }
            }
            return role;
        }
        public int Create(Role item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@nameRole", item.Name);
                string quary = "INSERT INTO Roles (roleName)" +
                    " VALUES (@roleName); SELECT LAST_INSERT_ID();";
                MySqlCommand command = new MySqlCommand(quary, connection);

                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                object obj = command.ExecuteScalar();
                return Convert.ToInt32(obj);
            }
        }

        public void Update(Role item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@id", item.Id);
                parameters[1] = new MySqlParameter("@nameRole", item.Name);
                string quary = "UPDATE Roles SET nameRole = @nameRole" +
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
                string quary = "DELETE FROM Roles where id=@id";
                MySqlParameter parameter = new MySqlParameter("@id", id);
                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
