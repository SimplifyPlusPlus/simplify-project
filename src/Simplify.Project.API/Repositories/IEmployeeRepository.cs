using Simplify.Project.API.Contracts;
using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Интерфейс репозитория сотрудников
/// </summary>
public interface IEmployeeRepository
{
	/// <summary>
	/// Получить список сотрудников
	/// </summary>
	/// <returns>Список сотрудников</returns>
	public IEnumerable<Employee> GetEmployees();
       
	/// <summary>
	/// Получить сотрудника по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Сотрудник</returns>
	public Employee? GetEmployee(Guid id);

	public void AddEmployee(Employee employee);
}
