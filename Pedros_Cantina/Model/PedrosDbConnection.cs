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


	}
}
