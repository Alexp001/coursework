using DataAccessLevel.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, orderDate, clientId, employeeId, accountingImplementation FROM Orders";
                MySqlCommand command = new MySqlCommand(quary, connection);
                MySqlDataReader reader = command.ExecuteReader();
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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, orderDate, clientId, employeeId, accountingImplementation FROM Orders WHERE id = @id";
                MySqlParameter parameter = new MySqlParameter("@id", id);

                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                MySqlDataReader reader = command.ExecuteReader();
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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[4];
                parameters[0] = new MySqlParameter("@orderDate", item.Date);
                parameters[1] = new MySqlParameter("@clientId", item.ClientId);
                parameters[2] = new MySqlParameter("@employeeId", item.EmployeeId);
                parameters[3] = new MySqlParameter("@accountingImplementation", item.AccountingImplementation);
                string quary = "INSERT INTO Orders (orderDate, clientId, employeeId, accountingImplementation)" +
                    " VALUES (@orderDate, @clientId, @employeeId, @accountingImplementation); SELECT LAST_INSERT_ID();";
                MySqlCommand command = new MySqlCommand(quary, connection);

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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[5];
                parameters[0] = new MySqlParameter("@id", item.Id);
                parameters[1] = new MySqlParameter("@orderDate", item.Date);
                parameters[2] = new MySqlParameter("@clientId", item.ClientId);
                parameters[3] = new MySqlParameter("@employeeId", item.EmployeeId);
                parameters[4] = new MySqlParameter("@accountingImplementation", item.AccountingImplementation);
                string quary = "UPDATE Orders SET orderDate = @orderDate, clientId = @clientId, employeeId = @employeeId," +
                    "  accountingImplementation = @accountingImplementation WHERE id = @id";
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
                string quary = "DELETE FROM Orders where id=@id";
                MySqlParameter parameter = new MySqlParameter("@id", id);
                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
