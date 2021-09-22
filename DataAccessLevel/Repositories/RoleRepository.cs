using DataAccessLevel.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameRole FROM Roles";
                SqlCommand command = new SqlCommand(quary, connection);
                SqlDataReader reader = command.ExecuteReader();
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameRole FROM Roles WHERE id = @id";
                SqlParameter parameter = new SqlParameter("@id", id);

                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    role = new Role(reader.GetInt32(0), reader.GetString(1));
                }
            }
            return role;
        }
        public int Create(Role item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@nameRole", item.Name);
                string quary = "INSERT INTO Roles (roleName)" +
                    " VALUES (@roleName); SELECT scope_identity()";
                SqlCommand command = new SqlCommand(quary, connection);

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@id", item.Id);
                parameters[1] = new SqlParameter("@nameRole", item.Name);
                string quary = "UPDATE Roles SET nameRole = @nameRole" +
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
                string quary = "DELETE FROM Roles where id=@id";
                SqlParameter parameter = new SqlParameter("@id", id);
                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
