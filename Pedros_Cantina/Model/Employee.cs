using System;
using System.Collections.Generic;
using System.Text;

namespace Pedros_Cantina.Model
{
	public class Employee
	{
	public int EmployeeId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Phone{ get; set; }
	public string Email { get; set; }
	public string Role { get; set; }

		public Employee()
		{
			EmployeeId = 0;
			FirstName = "DummyName";
			LastName = "DummyLastName";
			Phone = "000-000-0000";
			Email = "Dummy@Email.com";
			Role = "Staff";
		}

		public Employee(int employeeId, string firstName, string lastName, string phone, string email, string role)
		{
			EmployeeId = employeeId;
			FirstName = firstName;
			LastName = lastName;
			Phone = phone;
			Email = email;
			Role = role;
		}

		public override string ToString()
		{
			return $"EmployeeId: {EmployeeId}, FirstName: {FirstName}, LastName: {LastName}, Phone: {Phone}, Email: {Email}, Role: {Role}"; 
		}

	}
}
