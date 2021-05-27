using DataAccessLevel.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLevel.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly string _connectionString;
        public CommentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Comment> GetAll()
        {
            List<Comment> comments = new List<Comment>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, textOfComment, kindOfComment, mark, clientId FROM CommentOfClient";
                MySqlCommand command = new MySqlCommand(quary, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Comment comment = new Comment(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetInt32(3), reader["ClientId"].ToString() == "" ? null : (int?)reader.GetValue(3));
                    comments.Add(comment);
                }
            }
            return comments;
        }
        public Comment GetById(int id)
        {
            Comment comment = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, textOfComment, kindOfComment, mark, clientId FROM CommentOfClient WHERE id = @id";
                MySqlParameter parameter = new MySqlParameter("@id", id);

                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comment = new Comment(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetInt32(3), reader.GetInt32(4));
                }
            }
            return comment;
        }
        public int Create(Comment item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[4];
                parameters[0] = new MySqlParameter("@textOfComment", item.Text);
                parameters[1] = new MySqlParameter("@kindOfComment", item.KindOfComment);
                parameters[2] = new MySqlParameter("@mark", item.Mark);
                parameters[3] = new MySqlParameter("@clientId", item.ClientId);
                string quary = "INSERT INTO CommentOfClient (textOfComment, kindOfComment, mark, clientId)" +
                    " VALUES (@textOfComment, @kindOfComment, @mark, @clientId); SELECT LAST_INSERT_ID();";
                MySqlCommand command = new MySqlCommand(quary, connection);

                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                object obj = command.ExecuteScalar();
                return Convert.ToInt32(obj);
            }
        }
        public void Update(Comment item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[5];
                parameters[0] = new MySqlParameter("@id", item.Id);
                parameters[1] = new MySqlParameter("@textOfComment", item.Text);
                parameters[2] = new MySqlParameter("@kindOfComment", item.KindOfComment);
                parameters[3] = new MySqlParameter("@mark", item.Mark);
                parameters[4] = new MySqlParameter("@clientId", item.ClientId);
                string quary = "UPDATE CommentOfClient" +
                    " SET textOfComment = @textOfComment, kindOfComment = @kindOfComment, mark = @mark, clientId = @clientId" +
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
                string quary = "DELETE FROM CommentOfClient where id=@id";
                MySqlParameter parameter = new MySqlParameter("@id", id);
                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
