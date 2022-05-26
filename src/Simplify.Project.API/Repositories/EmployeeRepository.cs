using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
	private readonly SimplifyContext _context;

	public EmployeeRepository(SimplifyContext context)
	{
		_context = context;
	}
	
	public IQueryable<Employee> GetEmployees()
	{
		return _context.Employees.AsQueryable();
	}

	public Employee? GetEmployee(Guid id)
	{
		return _context.Employees.SingleOrDefault(employee => employee.Id == id);
	}

	public async Task AddEmployee(Employee employee)
	{
		_context.Employees.Add(employee);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateEmployee(Guid id, Employee updatedEmployee)
	{
		_context.Employees.Update(updatedEmployee);
		await _context.SaveChangesAsync();
	}
}
