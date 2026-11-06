using System;
using System.Collections.Generic;
using System.Text;

namespace Pedros_Cantina.Model
{
	/// <summary>
	/// Defines the Employee class, which represents an employee object in the system.
	/// </summary>
	
	public class Employee
	{
		/// <summary>
		/// Defines the properties of the Employee class, including EmployeeId, FirstName, LastName, Phone, Email, and Role.
		/// </summary>
		/// <param name="employeeId">The unique identifier for the employee.</param>
		/// <param name="firstName">The first name of the employee.</param>
		/// <param name="lastName">The last name of the employee.</param>
		/// <param name="phone">The phone number of the employee.</param>
		/// <param name="email">The email address of the employee.</param>
		/// <param name="role">The role of the employee.</param>
		/// returns>A string representation of the Employee object.</returns>
		public int EmployeeId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Phone{ get; set; }
	public string Email { get; set; }
	public string Role { get; set; }

		/// <summary>
		/// Defines the default constructor for the Employee class, which initializes the properties with default values.
		/// </summary>
		public Employee()
		{
			EmployeeId = 0;
			FirstName = "DummyName";
			LastName = "DummyLastName";
			Phone = "000-000-0000";
			Email = "Dummy@Email.com";
			Role = "Staff";
		}

		/// <summary>
		/// Defines a constructor for the Employee class that takes in parameters for each property and initializes them accordingly.
		/// </summary>
		/// <param name="employeeId">The unique identifier for the employee.</param>
		/// <param name="firstName">The first name of the employee.</param>
		/// <param name="lastName">The last name of the employee.</param>
		/// <param name="phone">The phone number of the employee.</param>
		/// <param name="email">The email address of the employee.</param>
		/// <param name="role">The role of the employee.</param>
		public Employee(int employeeId, string firstName, string lastName, string phone, string email, string role)
		{
			EmployeeId = employeeId;
			FirstName = firstName;
			LastName = lastName;
			Phone = phone;
			Email = email;
			Role = role;
		}

		/// <summary>
		/// Defines a method that returns a string representation of the Employee object, including all of its properties.
		/// </summary>
		/// <returns>A string representation of the Employee object.</returns>
		public override string ToString()
		{
			return $"EmployeeId: {EmployeeId}, FirstName: {FirstName}, LastName: {LastName}, Phone: {Phone}, Email: {Email}, Role: {Role}"; 
		}

	}
}
