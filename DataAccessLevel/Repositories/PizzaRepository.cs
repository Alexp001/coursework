using DataAccessLevel.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfPizza, price, image FROM Pizza";
                SqlCommand command = new SqlCommand(quary, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pizza pizza = new Pizza(reader.GetInt32(0), reader.GetString(1), (double)reader.GetDecimal(2),
                        reader["image"].ToString() == "" ? null : reader.GetString(3));
                    pizzas.Add(pizza);
                }
            }
            return pizzas;
        }
        public Pizza GetById(int id)
        {
            Pizza pizza = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfPizza, price, image FROM Pizza WHERE id = @id";
                SqlParameter parameter = new SqlParameter("@id", id);

                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pizza = new Pizza(reader.GetInt32(0), reader.GetString(1),
                        (double)reader.GetDecimal(2), reader["Image"].ToString() == "" ? null : (string)reader.GetValue(3));
                }
            }
            return pizza;
        }
        public int Create(Pizza item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@nameOfPizza", item.Name);
                parameters[1] = new SqlParameter("@price", item.Price);
                parameters[2] = new SqlParameter("@image", item.Image);
                string quary = "INSERT INTO Pizza (nameOfPizza, price, image)" +
                    " VALUES (@nameOfPizza, @price, @image); SELECT scope_identity()";
                SqlCommand command = new SqlCommand(quary, connection);

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@id", item.Id);
                parameters[1] = new SqlParameter("@nameOfPizza", item.Name);
                parameters[2] = new SqlParameter("@price", item.Price);
                parameters[3] = new SqlParameter("@image", item.Image);
                string quary = "UPDATE Pizza SET nameOfPizza = @nameOfPizza, price = @price, image = @image" +
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
                string quary = "DELETE FROM Pizza where id=@id";
                SqlParameter parameter = new SqlParameter("@id", id);
                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
