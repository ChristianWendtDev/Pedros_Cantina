using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Pedros_Cantina.Model
{
	/// <summary>
	/// Defines EmployeeRepo class, which provides methods for interacting with the Employee table in the database.
	/// </summary>
	public class EmployeeRepo
	{

		/// <summary>
		/// Defines the properties of the EmployeeRepo class, including a list of Employee objects and a PedrosDbConnection object for connecting to the database.
		/// </summary>
		private List<Employee> _employees;
		public PedrosDbConnection db { get; set; }


		/// <summary>
		/// Defines the default constructor for the EmployeeRepo class, which initializes the list of Employee objects and creates a new PedrosDbConnection object for connecting to the database.
		/// </summary>
		public EmployeeRepo()
		{
			_employees = new List<Employee>();
			db = new PedrosDbConnection();
		}

		/// <summary>
		/// Defines a constructor for the EmployeeRepo class that takes in a list of Employee objects and a PedrosDbConnection object for connecting to the database, and initializes the corresponding properties accordingly.
		/// </summary>
		/// <param name="employees">The list of Employee objects to initialize.</param>
		/// <param name="dB">The PedrosDbConnection object to initialize.</param>
		public EmployeeRepo(List<Employee> employees, PedrosDbConnection dB)
		{
			_employees = employees;
			db = dB;
			
		}

		/// <summary>
		/// defines the Employees property, which gets or sets the list of Employee objects in the EmployeeRepo class.
		/// </summary>
		public List<Employee> Employees
		{
			get { return _employees; }
			set
			{
				_employees = value;
			}
		}

		/// <summary>
		/// Defines a method that creates a new Employee object in the database by executing an SQL INSERT statement, and returns the created Employee object.
		/// </summary>
		/// <param name="employee">The Employee object to create.</param>
		/// <returns>The created Employee object.</returns>
		/// <exception cref="KeyNotFoundException"> Thrown when the employee with {employee.EmployeeId} is not found.</exception>
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

		/// <summary>
		/// Define a method that reads all Employee objects from the database by executing an SQL SELECT statement, and returns a list of Employee objects.
		/// </summary>
		/// <returns>A list of Employee objects.</returns>
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


		/// <summary>
		/// Defines a method that retrieves a specific Employee object from the database by executing an SQL SELECT statement with a WHERE clause, and returns the retrieved Employee object.
		/// </summary>
		/// <param name="id">The ID of the employee to retrieve.</param>
		/// <returns>The retrieved Employee object.</returns>
		/// <exception cref="KeyNotFoundException">Thrown when the employee with the specified ID is not found.</exception>
		public Employee GetEmp(int id)
		{
			db.ConnectToDB();
			string sql = $"SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";

			SqlCommand cmd = new SqlCommand(sql, db.Connection);
			cmd.Parameters.AddWithValue("@EmployeeId", id);

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


		/// <summary>
		/// Defines a method that updates a specific Employee object in the database by executing an SQL UPDATE statement with a WHERE clause, and returns the updated Employee object.
		/// </summary>
		/// <param name="id">The ID of the employee to update.</param>
		/// <param name="Updatedemployee">The updated Employee object.</param>
		/// <returns>The updated Employee object.</returns>
		/// <exception cref="KeyNotFoundException">Thrown when the employee with the specified ID is not found.</exception>
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


		/// <summary>
		/// Defines a method that deletes a specific Employee object from the database by executing an SQL DELETE statement with a WHERE clause, and returns the deleted Employee object.
		/// </summary>
		/// <param name="idToRemove">The ID of the employee to delete.</param>
		/// <returns>The deleted Employee object.</returns>
		/// <exception cref="KeyNotFoundException">Thrown when the employee with the specified ID is not found.</exception>
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
