using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий сотрудников
/// </summary>
public class EmployeeRepository : IEmployeeRepository
{
	private readonly SimplifyContext _context;

	/// <summary>
	/// Конструктор класса <see cref="EmployeeRepository"/>
	/// </summary>
	/// <param name="context">Контекст базы данных</param>
	public EmployeeRepository(SimplifyContext context)
	{
		_context = context;
	}
	
	/// <inheritdoc cref="IEmployeeRepository.GetEmployees()"/>
	public IQueryable<Employee> GetEmployees()
	{
		return _context.Employees.AsQueryable();
	}

	/// <inheritdoc cref="IEmployeeRepository.GetEmployee(Guid)"/>
	public Employee? GetEmployee(Guid id)
	{
		return _context.Employees.SingleOrDefault(employee => employee.Id == id);
	}

	/// <inheritdoc cref="IEmployeeRepository.AddEmployee(Employee)"/>
	public async Task AddEmployee(Employee employee)
	{
		_context.Employees.Add(employee);
		await _context.SaveChangesAsync();
	}

	/// <inheritdoc cref="IEmployeeRepository.UpdateEmployee(Guid,Employee)"/>
	public async Task UpdateEmployee(Guid id, Employee updatedEmployee)
	{
		_context.Employees.Update(updatedEmployee);
		await _context.SaveChangesAsync();
	}
}
