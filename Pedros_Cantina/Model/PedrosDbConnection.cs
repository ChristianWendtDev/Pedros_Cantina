using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pedros_Cantina.Model
{
	public class PedrosDbConnection
	{
		SqlConnection _conn;

		public PedrosDbConnection() 
		{ }

		public void ConnectToDB()
		{
			string ConnString = @"Data Source (localdb)\MSSQLLocalDB; Initial Catalog = Pedros_Cantina; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False; Command Timeout = 30";
			_conn = new SqlConnection(ConnString);
			_conn.Open();
		}

		public void DisconnectFromDB()
		{
			_conn.Close();
		}

		public Employee CreateEmployee(Employee employee)
		{
			ConnectToDB();

			string sql = $"INSERT INTO Employees (FirstName, LastName, Phone, Email, Role) VALUES (@FirstName, @LastName, @Phone, @Email, @Role)";

			SqlCommand cmd = new SqlCommand(sql, _conn);
			cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
			cmd.Parameters.AddWithValue("@LastName", employee.LastName);
			cmd.Parameters.AddWithValue("@Phone", employee.Phone);
			cmd.Parameters.AddWithValue("@Email", employee.Email);
			cmd.Parameters.AddWithValue("@Role", employee.Role);

			int row = cmd.ExecuteNonQuery();

			if ( row == 0 )
			{
				throw new KeyNotFoundException($"Employee with ID {employee.EmployeeId} not found.");
			}
			DisconnectFromDB();

			return employee;
		}

		public List<Employee> ReadAllEmp()
		{
			List<Employee> employeeList = new List<Employee>();
			ConnectToDB();

			string sql = "SELECT * FROM Employees";

			SqlCommand cmd = new SqlCommand(sql, _conn);

			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				int EmployeeId = (int)reader["EmployeeId"];
				string FirstName = (string)reader["FirstName"];
				string LastName = (string)reader["LastName"];
				string Phone = (string)reader["Phone"];
				string Email = (string)reader["Email"];
				string Role = (string)reader["Role"];

				Employee employee = new Employee(EmployeeId, FirstName, LastName, Phone, Email, Role);
				employeeList.Add(employee);
			}
			DisconnectFromDB();
			return employeeList;
		}

		public Employee GetEmp(int id)
		{
			ConnectToDB();
			string sql = $"SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";

			SqlCommand cmd =new SqlCommand(sql, _conn);

			SqlDataReader reader = cmd.ExecuteReader();

			Employee employee = null;
			
			if (reader.Read())
			{
			int EmployeeId = (int)reader["EmployeeId"];
			string FirstName = (string)reader["FirstName"];
			string LastName = (string)reader["LastName"];
			string phone = (string)reader["Phone"];
			string email = (string)reader["Email"];
			string role = (string)reader["Role"];

			employee = new Employee(EmployeeId, FirstName, LastName, phone, email, role);
			}
			else
			{
				DisconnectFromDB();
				throw new KeyNotFoundException($"Employee with ID {id} not found.");
			}
			DisconnectFromDB();
			return employee;
		}

		public Employee UpdateEmp(int id, Employee Updatedemployee)
		{
		Employee employee = GetEmp(id);

			ConnectToDB();

		string sql = $"UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Email = @Email, Role = @Role WHERE EmployeeId = @EmployeeId";
			SqlCommand cmd = new SqlCommand(sql, _conn);

			cmd.Parameters.AddWithValue("@FirstName", Updatedemployee.FirstName);
			cmd.Parameters.AddWithValue("@LastName", Updatedemployee.LastName);
			cmd.Parameters.AddWithValue("@Phone", Updatedemployee.Phone);
			cmd.Parameters.AddWithValue("@Email", Updatedemployee.Email);
			cmd.Parameters.AddWithValue("@Role", Updatedemployee.Role);

			int row = cmd.ExecuteNonQuery();

			DisconnectFromDB();

			if (row == 1)
			{
				return Updatedemployee;
			}
			else
			{
				throw new KeyNotFoundException($"Employee with ID {id} not found.");
			}

		}

		public Employee DeleteEmp(int idToRemove)
		{
		Employee employee = GetEmp(idToRemove);
			ConnectToDB();
			string sql = $"DELETE FROM Employees WHERE EmployeeId = @EmployeeId";

			SqlCommand cmd = new SqlCommand(sql, _conn);

			cmd.Parameters.AddWithValue("@EmployeeId", idToRemove);

			int row = cmd.ExecuteNonQuery();

			DisconnectFromDB();

			if (row == 1)
			{
				return employee;
			}
			else
			{
				throw new KeyNotFoundException($"Employee with ID {idToRemove} not found.");
			}
		}

	}
}
