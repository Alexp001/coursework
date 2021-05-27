using DataAccessLevel.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLevel.Repositories
{
    public class AccountingRolesRepository : IRepository<AccountingRoles>
    {
        private readonly string _connectionString;
        public AccountingRolesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<AccountingRoles> GetAll()
        {
            List<AccountingRoles> accounings = new List<AccountingRoles>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, userId, roleId FROM AccountingRoles";
                MySqlCommand command = new MySqlCommand(quary, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AccountingRoles accounting = new AccountingRoles(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                    accounings.Add(accounting);
                }
            }
            return accounings;
        }
        public AccountingRoles GetById(int id)
        {
            AccountingRoles accounting = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, userId, roleId FROM AccountingRoles WHERE id = @id";
                MySqlParameter parameter = new MySqlParameter("@id", id);

                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    accounting = new AccountingRoles(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                }
            }
            return accounting;
        }
        public int Create(AccountingRoles item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@userId", item.UserId);
                parameters[1] = new MySqlParameter("@roleId", item.RoleId);
                string quary = "INSERT INTO AccountingRoles (userId, roleId)" +
                    " VALUES (@userId, @roleId); SELECT LAST_INSERT_ID();";
                MySqlCommand command = new MySqlCommand(quary, connection);

                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                object obj = command.ExecuteScalar();
                return Convert.ToInt32(obj);
            }
        }

        public void Update(AccountingRoles item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[3];
                parameters[0] = new MySqlParameter("@id", item.Id);
                parameters[1] = new MySqlParameter("@userId", item.UserId);
                parameters[2] = new MySqlParameter("@roleId", item.RoleId);
                string quary = "UPDATE AccountingRoles SET userId = @userId, roleId = @roleId" +
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
                string quary = "DELETE FROM AccountingRoles where id=@id";
                MySqlParameter parameter = new MySqlParameter("@id", id);
                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
