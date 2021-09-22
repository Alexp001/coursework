using DataAccessLevel.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, textOfComment, kindOfComment, mark, clientId FROM CommentOfClient";
                SqlCommand command = new SqlCommand(quary, connection);
                SqlDataReader reader = command.ExecuteReader();
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, textOfComment, kindOfComment, mark, clientId FROM CommentOfClient WHERE id = @id";
                SqlParameter parameter = new SqlParameter("@id", id);

                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                SqlDataReader reader = command.ExecuteReader();
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@textOfComment", item.Text);
                parameters[1] = new SqlParameter("@kindOfComment", item.KindOfComment);
                parameters[2] = new SqlParameter("@mark", item.Mark);
                parameters[3] = new SqlParameter("@clientId", item.ClientId);
                string quary = "INSERT INTO CommentOfClient (textOfComment, kindOfComment, mark, clientId)" +
                    " VALUES (@textOfComment, @kindOfComment, @mark, @clientId); SELECT scope_identity()";
                SqlCommand command = new SqlCommand(quary, connection);

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@id", item.Id);
                parameters[1] = new SqlParameter("@textOfComment", item.Text);
                parameters[2] = new SqlParameter("@kindOfComment", item.KindOfComment);
                parameters[3] = new SqlParameter("@mark", item.Mark);
                parameters[4] = new SqlParameter("@clientId", item.ClientId);
                string quary = "UPDATE CommentOfClient" +
                    " SET textOfComment = @textOfComment, kindOfComment = @kindOfComment, mark = @mark, clientId = @clientId" +
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
                string quary = "DELETE FROM CommentOfClient where id=@id";
                SqlParameter parameter = new SqlParameter("@id", id);
                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
