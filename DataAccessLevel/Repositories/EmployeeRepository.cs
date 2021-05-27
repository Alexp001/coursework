using DataAccessLevel.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfEmployee, email, phone, salary, employeePosition, address, userId FROM Employee";
                MySqlCommand command = new MySqlCommand(quary, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetDouble(4), reader.GetString(5), reader.GetString(6), reader.GetInt32(7));
                    employees.Add(employee);
                }
            }
            return employees;
        }
        public Employee GetById(int id)
        {
            Employee employee = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string quary = "SELECT id, nameOfEmployee, email, phone, salary, employeePosition," +
                    " address, userId FROM Employee WHERE id = @id";
                MySqlParameter parameter = new MySqlParameter("@id", id);

                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employee = new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetDouble(4), reader.GetString(5), reader.GetString(6),
                        reader.GetInt32(7));
                }
            }
            return employee;

        }
        public int Create(Employee item)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[7];
                parameters[0] = new MySqlParameter("@nameOfEmployee", item.Name);
                parameters[1] = new MySqlParameter("@email", item.Email);
                parameters[2] = new MySqlParameter("@phone", item.Phone);
                parameters[3] = new MySqlParameter("@salary", item.Salary);
                parameters[4] = new MySqlParameter("@empoyeePosition", item.Position);
                parameters[5] = new MySqlParameter("@address", item.Address);
                parameters[6] = new MySqlParameter("@userId", item.UserId);

                string quary = "INSERT INTO Employee (nameOfEmployee, email, phone, salary, employeePosition, address, userId)" +
                    " VALUES (@nameOfEmployee, @email, @phone, @salary, @empoyeePosition, @address, @userId); SELECT LAST_INSERT_ID();";
                MySqlCommand command = new MySqlCommand(quary, connection);

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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlParameter[] parameters = new MySqlParameter[7];
                parameters[0] = new MySqlParameter("@id", item.Id);
                parameters[1] = new MySqlParameter("@nameOfEmployee", item.Name);
                parameters[2] = new MySqlParameter("@email", item.Email);
                parameters[3] = new MySqlParameter("@phone", item.Phone);
                parameters[4] = new MySqlParameter("@salary", item.Salary);
                parameters[5] = new MySqlParameter("@employeePosition", item.Position);
                parameters[6] = new MySqlParameter("@address", item.Address);
                string quary = "UPDATE Employee SET nameOfEmployee = @nameOfEmployee, email = @email, phone = @phone, " +
                    "salary = @salary, employeePosition = @employeePosition, address = @address WHERE id = @id";
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
                string quary = "DELETE FROM Employee where id=@id";
                MySqlParameter parameter = new MySqlParameter("@id", id);
                MySqlCommand command = new MySqlCommand(quary, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}
