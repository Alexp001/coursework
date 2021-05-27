using DataAccessLevel.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLevel.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly string _connectionString;
        public ClientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Client> GetAll()
        {
            List<Client> clients = new List<Client>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfClient, email, phone, address, userId FROM Clients";
                MySqlCommand command = new MySqlCommand(quary, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Client client = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetInt32(5));
                    clients.Add(client);
                }
            }
            return clients;
        }
        public Client GetById(int id)
        {
            Client client = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfClient, email, phone, address, userId FROM Clients WHERE id = @id";
                MySqlParameter parameter = new MySqlParameter("@id", id);

                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    client = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetInt32(5));
                }
            }
            return client;
            
        }
        public int Create(Client item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[5];
                parameters[0] = new MySqlParameter("@nameOfClient", item.Name);
                parameters[1] = new MySqlParameter("@email", item.Email);
                parameters[2] = new MySqlParameter("@phone", item.Phone);
                parameters[3] = new MySqlParameter("@address", item.Address);
                parameters[4] = new MySqlParameter("@userId", item.UserId);
                string quary = "INSERT INTO Clients (nameOfClient, email, phone, address, userId)" +
                    " VALUES (@nameOfClient, @email, @phone, @address, @userId); SELECT LAST_INSERT_ID();";
                MySqlCommand command = new MySqlCommand(quary, connection);
                
                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                object obj = command.ExecuteScalar();
                return Convert.ToInt32(obj);
            }
        }
        public void Update(Client item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[6];
                parameters[0] = new MySqlParameter("@id", item.Id);
                parameters[1] = new MySqlParameter("@nameOfClient", item.Name);
                parameters[2] = new MySqlParameter("@email", item.Email);
                parameters[3] = new MySqlParameter("@phone", item.Phone);
                parameters[4] = new MySqlParameter("@address", item.Address);
                parameters[5] = new MySqlParameter("@userId", item.UserId);
                string quary = "UPDATE Clients SET nameOfClient = @nameOfClient, email = @email, phone = @phone," + 
                    " address = @address, userId = @userId WHERE id = @id";
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
                string quary = "DELETE FROM Clients where id=@id";
                MySqlParameter parameter = new MySqlParameter("@id", id);
                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
