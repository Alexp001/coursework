using DataAccessLevel.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, userId, roleId FROM AccountingRoles";
                SqlCommand command = new SqlCommand(quary, connection);
                SqlDataReader reader = command.ExecuteReader();
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, userId, roleId FROM AccountingRoles WHERE id = @id";
                SqlParameter parameter = new SqlParameter("@id", id);

                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    accounting = new AccountingRoles(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                }
            }
            return accounting;
        }
        public int Create(AccountingRoles item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@userId", item.UserId);
                parameters[1] = new SqlParameter("@roleId", item.RoleId);
                string quary = "INSERT INTO AccountingRoles (userId, roleId)" +
                    " VALUES (@userId, @roleId); SELECT scope_identity()";
                SqlCommand command = new SqlCommand(quary, connection);

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@id", item.Id);
                parameters[1] = new SqlParameter("@userId", item.UserId);
                parameters[2] = new SqlParameter("@roleId", item.RoleId);
                string quary = "UPDATE AccountingRoles SET userId = @userId, roleId = @roleId" +
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
                string quary = "DELETE FROM AccountingRoles where id=@id";
                SqlParameter parameter = new SqlParameter("@id", id);
                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
