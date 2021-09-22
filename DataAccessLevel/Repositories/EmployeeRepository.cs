using DataAccessLevel.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLevel.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly string _connectionString;
        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfEmployee, email, phone, salary, employeePosition, address, userId FROM Employee";
                SqlCommand command = new SqlCommand(quary, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), (double)reader.GetDecimal(4), reader.GetString(5), reader.GetString(6), reader.GetInt32(7));
                    employees.Add(employee);
                }
            }
            return employees;
        }
        public Employee GetById(int id)
        {
            Employee employee = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfEmployee, email, phone, salary, employeePosition," +
                    " address, userId FROM Employee WHERE id = @id";
                SqlParameter parameter = new SqlParameter("@id", id);

                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employee = new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), (double)reader.GetDecimal(4), reader.GetString(5), reader.GetString(6),
                        reader.GetInt32(7));
                }
            }
            return employee;

        }
        public int Create(Employee item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@nameOfEmployee", item.Name);
                parameters[1] = new SqlParameter("@email", item.Email);
                parameters[2] = new SqlParameter("@phone", item.Phone);
                parameters[3] = new SqlParameter("@salary", item.Salary);
                parameters[4] = new SqlParameter("@empoyeePosition", item.Position);
                parameters[5] = new SqlParameter("@address", item.Address);
                parameters[6] = new SqlParameter("@userId", item.UserId);

                string quary = "INSERT INTO Employee (nameOfEmployee, email, phone, salary, employeePosition, address, userId)" +
                    " VALUES (@nameOfEmployee, @email, @phone, @salary, @empoyeePosition, @address, @userId); scope_identity()";
                SqlCommand command = new SqlCommand(quary, connection);

                foreach (var item1 in parameters)
                {
                    command.Parameters.Add(item1);
                }
                object obj = command.ExecuteScalar();
                return Convert.ToInt32(obj);
            }
        }
        public void Update(Employee item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@id", item.Id);
                parameters[1] = new SqlParameter("@nameOfEmployee", item.Name);
                parameters[2] = new SqlParameter("@email", item.Email);
                parameters[3] = new SqlParameter("@phone", item.Phone);
                parameters[4] = new SqlParameter("@salary", item.Salary);
                parameters[5] = new SqlParameter("@employeePosition", item.Position);
                parameters[6] = new SqlParameter("@address", item.Address);
                string quary = "UPDATE Employee SET nameOfEmployee = @nameOfEmployee, email = @email, phone = @phone, " +
                    "salary = @salary, employeePosition = @employeePosition, address = @address WHERE id = @id";
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
                string quary = "DELETE FROM Employee where id=@id";
                SqlParameter parameter = new SqlParameter("@id", id);
                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
