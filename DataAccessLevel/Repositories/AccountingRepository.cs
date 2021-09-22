using DataAccessLevel.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLevel.Repositories
{
    public class AccountingRepository : IRepository<Accounting>
    {
        private readonly string _connectionString;
        public AccountingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Accounting> GetAll()
        {
            List<Accounting> accountings = new List<Accounting>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, quantity, pizzaId, orderId FROM Accounting";
                SqlCommand command = new SqlCommand(quary, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Accounting accounting = new Accounting(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),
                        reader.GetInt32(3));
                    accountings.Add(accounting);
                }
            }
            return accountings;
        }
        public Accounting GetById(int id)
        {
            Accounting accounting = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, quantity, pizzaId, orderId FROM Accounting WHERE id = @id";
                SqlParameter parameter = new SqlParameter("@id", id);

                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    accounting = new Accounting(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),
                        reader.GetInt32(3));
                }
            }
            return accounting;
        }
        public int Create(Accounting item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@quantity", item.Quantity);
                parameters[1] = new SqlParameter("@pizzaId", item.PizzaId);
                parameters[2] = new SqlParameter("@orderId", item.OrderId);
                string quary = "INSERT INTO Accounting (quantity, pizzaId, orderId)" +
                    " VALUES (@quantity, @pizzaId, @orderId); SELECT scope_identity()";
                SqlCommand command = new SqlCommand(quary, connection);

                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                object obj = command.ExecuteScalar();
                return Convert.ToInt32(obj);
            }
        }
        public void Update(Accounting item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@id", item.Id);
                parameters[1] = new SqlParameter("@quantity", item.Quantity);
                parameters[2] = new SqlParameter("@pizzaId", item.PizzaId);
                parameters[3] = new SqlParameter("@orderId", item.OrderId);
                string quary = "UPDATE Accounting SET quantity = @quantity, pizzaId = @pizzaId, orderId = @orderId" +
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
                string quary = "DELETE FROM Accounting where id=@id";
                SqlParameter parameter = new SqlParameter("@id", id);
                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }

    }
}
