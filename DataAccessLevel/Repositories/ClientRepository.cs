using DataAccessLevel.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfClient, email, phone, address, userId FROM Clients";
                SqlCommand command = new SqlCommand(quary, connection);
                SqlDataReader reader = command.ExecuteReader();
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfClient, email, phone, address, userId FROM Clients WHERE id = @id";
                SqlParameter parameter = new SqlParameter("@id", id);

                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                SqlDataReader reader = command.ExecuteReader();
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@nameOfClient", item.Name);
                parameters[1] = new SqlParameter("@email", item.Email);
                parameters[2] = new SqlParameter("@phone", item.Phone);
                parameters[3] = new SqlParameter("@address", item.Address);
                parameters[4] = new SqlParameter("@userId", item.UserId);
                string quary = "INSERT INTO Clients (nameOfClient, email, phone, address, userId)" +
                    " VALUES (@nameOfClient, @email, @phone, @address, @userId); SELECT scope_identity()";
                SqlCommand command = new SqlCommand(quary, connection);
                
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@id", item.Id);
                parameters[1] = new SqlParameter("@nameOfClient", item.Name);
                parameters[2] = new SqlParameter("@email", item.Email);
                parameters[3] = new SqlParameter("@phone", item.Phone);
                parameters[4] = new SqlParameter("@address", item.Address);
                parameters[5] = new SqlParameter("@userId", item.UserId);
                string quary = "UPDATE Clients SET nameOfClient = @nameOfClient, email = @email, phone = @phone," + 
                    " address = @address, userId = @userId WHERE id = @id";
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
                string quary = "DELETE FROM Clients where id=@id";
                SqlParameter parameter = new SqlParameter("@id", id);
                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
