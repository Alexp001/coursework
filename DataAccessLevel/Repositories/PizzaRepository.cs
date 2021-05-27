using DataAccessLevel.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLevel.Repositories
{
    public class PizzaRepository : IRepository<Pizza>
    {
        private readonly string _connectionString;
        public PizzaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Pizza> GetAll()
        {
            List<Pizza> pizzas = new List<Pizza>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfPizza, price, image FROM Pizza";
                MySqlCommand command = new MySqlCommand(quary, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pizza pizza = new Pizza(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2),
                        reader["image"].ToString() == "" ? null : reader.GetString(3));
                    pizzas.Add(pizza);
                }
            }
            return pizzas;
        }
        public Pizza GetById(int id)
        {
            Pizza pizza = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfPizza, price, image FROM Pizza WHERE id = @id";
                MySqlParameter parameter = new MySqlParameter("@id", id);

                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pizza = new Pizza(reader.GetInt32(0), reader.GetString(1),
                        reader.GetDouble(2), reader["Image"].ToString() == "" ? null : (string)reader.GetValue(3));
                }
            }
            return pizza;
        }
        public int Create(Pizza item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[3];
                parameters[0] = new MySqlParameter("@nameOfPizza", item.Name);
                parameters[1] = new MySqlParameter("@price", item.Price);
                parameters[2] = new MySqlParameter("@image", item.Image);
                string quary = "INSERT INTO Pizza (nameOfPizza, price, image)" +
                    " VALUES (@nameOfPizza, @price, @image); SELECT LAST_INSERT_ID();";
                MySqlCommand command = new MySqlCommand(quary, connection);

                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                object obj = command.ExecuteScalar();
                return Convert.ToInt32(obj);
            }
        }

        public void Update(Pizza item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[4];
                parameters[0] = new MySqlParameter("@id", item.Id);
                parameters[1] = new MySqlParameter("@nameOfPizza", item.Name);
                parameters[2] = new MySqlParameter("@price", item.Price);
                parameters[3] = new MySqlParameter("@image", item.Image);
                string quary = "UPDATE Pizza SET nameOfPizza = @nameOfPizza, price = @price, image = @image" +
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
                string quary = "DELETE FROM Pizza where id=@id";
                MySqlParameter parameter = new MySqlParameter("@id", id);
                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
