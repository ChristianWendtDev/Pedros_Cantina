using Pedros_Cantina.Model;

Console.WriteLine("Pedros Workers!");

Employee employee = new Employee();

EmployeeRepo Repo  = new EmployeeRepo();


Console.WriteLine("Listing all employees in the database");
foreach (Employee e in Repo.ReadAllEmp())
{
	Console.WriteLine(e);
}
Console.WriteLine();
Console.WriteLine("Getting a specific employee by ID from the database");
Console.WriteLine(Repo.GetEmp(1));


Console.WriteLine();
Console.WriteLine("Removing a specifc employee from the database");
Console.WriteLine(Repo.DeleteEmp(10)); 

Console.WriteLine();
Console.WriteLine("Updating an employee in the database");

Employee updatedEmp = new Employee
{ 
	EmployeeId = 1,
	FirstName = "John",
	LastName = "Doe",
	Phone = "555-555-5555",
	Email = "JE@Email",
	Role = "Admin"
};

Console.WriteLine(Repo.UpdateEmp(1, updatedEmp)); 