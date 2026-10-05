using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Pedros_Cantina.Model
{
	public class EmployeeRepo
	{
		private List<Employee> _employees;
		public PedrosDbConnection db { get; set; }

		public EmployeeRepo()
		{
			_employees = new List<Employee>();
			db = new PedrosDbConnection();
		}

		public EmployeeRepo(List<Employee> employees, PedrosDbConnection dB)
		{
			_employees = employees;
			db = dB;
			
		}


		public List<Employee> Employees
		{
			get { return _employees; }
			set
			{
				_employees = value;
			}
		}

		public Employee CreateEmployee(Employee employee)
		{
			db.ConnectToDB();

			string sql = $"INSERT INTO Employees (FirstName, LastName, Phone, Email, Role) VALUES (@FirstName, @LastName, @Phone, @Email, @Role)";

			SqlCommand cmd = new SqlCommand(sql, db.Connection);
			cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
			cmd.Parameters.AddWithValue("@LastName", employee.LastName);
			cmd.Parameters.AddWithValue("@Phone", employee.Phone);
			cmd.Parameters.AddWithValue("@Email", employee.Email);
			cmd.Parameters.AddWithValue("@Role", employee.Role);

			int row = cmd.ExecuteNonQuery();

			if (row == 0)
			{
				throw new KeyNotFoundException($"Employee with ID {employee.EmployeeId} not found.");
			}
			db.DisconnectFromDB();

			return employee;
		}

		public List<Employee> ReadAllEmp()
		{
			List<Employee> employeeList = new List<Employee>();
			db.ConnectToDB();

			string sql = "SELECT * FROM Employees";

			SqlCommand cmd = new SqlCommand(sql, db.Connection);

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
			db.DisconnectFromDB();
			return employeeList;
		}

		public Employee GetEmp(int id)
		{
			db.ConnectToDB();
			string sql = $"SELECT * FROM Employees WHERE EmployeeId = {id}";

			SqlCommand cmd = new SqlCommand(sql, db.Connection);

			SqlDataReader reader = cmd.ExecuteReader();

			Employee? employee = null;

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
				db.DisconnectFromDB();
				throw new KeyNotFoundException($"Employee with ID {id} not found.");
			}
			db.DisconnectFromDB();
			return employee;
		}

		public Employee UpdateEmp(int id, Employee Updatedemployee)
		{
			Employee employee = GetEmp(id);

			db.ConnectToDB();

			string sql = $"UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Email = @Email, Role = @Role WHERE EmployeeId = {id}";
			SqlCommand cmd = new SqlCommand(sql, db.Connection);

			cmd.Parameters.AddWithValue("@FirstName", Updatedemployee.FirstName);
			cmd.Parameters.AddWithValue("@LastName", Updatedemployee.LastName);
			cmd.Parameters.AddWithValue("@Phone", Updatedemployee.Phone);
			cmd.Parameters.AddWithValue("@Email", Updatedemployee.Email);
			cmd.Parameters.AddWithValue("@Role", Updatedemployee.Role);

			int row = cmd.ExecuteNonQuery();

			db.DisconnectFromDB();

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
			db.ConnectToDB();
			string sql = $"DELETE FROM Employees WHERE EmployeeId = {idToRemove}";

			SqlCommand cmd = new SqlCommand(sql, db.Connection);

			cmd.Parameters.AddWithValue("@EmployeeId", idToRemove);

			int row = cmd.ExecuteNonQuery();

			db.DisconnectFromDB();

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
