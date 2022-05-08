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

	/// <summary>
	/// Добавить сотрудника
	/// </summary>
	/// <param name="employee">Данные сотрудника</param>
	public void AddEmployee(Employee employee);

	/// <summary>
	/// Обновить данные сотрудника по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор сотрудника</param>
	/// <param name="updatedEmployee">Измененные данные сотрудника</param>
	public void UpdateEmployee(Guid id, Employee updatedEmployee);
}
