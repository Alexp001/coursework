using DataAccessLevel.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLevel.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly string _connectionString;
        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, orderDate, clientId, employeeId, accountingImplementation FROM Orders";
                SqlCommand command = new SqlCommand(quary, connection);
                SqlDataReader reader = command.ExecuteReader();
                Order order;
                while (reader.Read())
                {
                    order = new Order(reader.GetInt32(0), reader.GetDateTime(1), reader["ClientId"].ToString() == "" ? null : (int?)reader.GetValue(2),
                        reader["EmployeeId"].ToString() == "" ? null : (int?)reader.GetValue(3), reader.GetBoolean(4));
                    orders.Add(order);
                }
            }
            return orders;
        }
        public Order GetById(int id)
        {
            Order order = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, orderDate, clientId, employeeId, accountingImplementation FROM Orders WHERE id = @id";
                SqlParameter parameter = new SqlParameter("@id", id);

                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    order = new Order(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2),
                         reader["EmployeeId"].ToString() == "" ? null : (int?)reader.GetValue(3), reader.GetBoolean(4));
                }
            }
            return order;
        }
        public int Create(Order item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@orderDate", item.Date);
                parameters[1] = new SqlParameter("@clientId", item.ClientId);
                parameters[2] = new SqlParameter("@employeeId", item.EmployeeId ?? (object)DBNull.Value);
                parameters[3] = new SqlParameter("@accountingImplementation", item.AccountingImplementation);
                string quary = "INSERT INTO Orders (orderDate, clientId, employeeId, accountingImplementation)" +
                    " VALUES (@orderDate, @clientId, @employeeId, @accountingImplementation); SELECT scope_identity()";
                SqlCommand command = new SqlCommand(quary, connection);

                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                object obj = command.ExecuteScalar();
                return Convert.ToInt32(obj);
            }
        }
        public void Update(Order item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@id", item.Id);
                parameters[1] = new SqlParameter("@orderDate", item.Date);
                parameters[2] = new SqlParameter("@clientId", item.ClientId);
                parameters[3] = new SqlParameter("@employeeId", item.EmployeeId ?? (object)DBNull.Value);
                parameters[4] = new SqlParameter("@accountingImplementation", item.AccountingImplementation);
                string quary = "UPDATE Orders SET orderDate = @orderDate, clientId = @clientId, employeeId = @employeeId," +
                    "  accountingImplementation = @accountingImplementation WHERE id = @id";
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
                string quary = "DELETE FROM Orders where id=@id";
                SqlParameter parameter = new SqlParameter("@id", id);
                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
