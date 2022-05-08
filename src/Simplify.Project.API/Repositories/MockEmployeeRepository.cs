using Simplify.Project.Model;
using Simplify.Project.Shared;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий сотрудников
/// </summary>
public class MockEmployeeRepository : IEmployeeRepository
{
	private readonly List<Employee> _employees;
    
	/// <summary>
	/// Контроллер класса <see cref="MockEmployeeRepository"/>
	/// </summary>
	public MockEmployeeRepository()
	{
		_employees = GenerateData();
	}
    
	/// <inheritdoc cref="IEmployeeRepository.GetEmployees()"/>
	public IEnumerable<Employee> GetEmployees()
	{
		return _employees;
	}
    
	/// <inheritdoc cref="IEmployeeRepository.GetEmployee(Guid)"/>
	public Employee? GetEmployee(Guid id)
	{
		var employee = _employees.SingleOrDefault(employee => employee.Id == id);
		return employee;
	}

	/// <inheritdoc cref="IEmployeeRepository.AddEmployee(Employee)"/>
	public void AddEmployee(Employee employee)
	{
		_employees.Add(employee);
	}

	/// <inheritdoc cref="IEmployeeRepository.UpdateEmployee(Guid, Employee)"/>
	public void UpdateEmployee(Guid id, Employee updatedEmployee)
	{
		var employee = _employees.Single(employee => employee.Id ==  id);
		_employees.Remove(employee);
		_employees.Add(updatedEmployee);
	}

	private static List<Employee> GenerateData()
	{
		return new List<Employee>
		{
			new()
			{
				Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6"), 
				Lastname = "Маркелов", 
				Firstname = "Павел", 
				Patronymic = "Николаевич",
				Role = RoleType.Administrator,
				Login = "pmarkelov",
				Password = "pmarkelov",
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Backend и API разработчик"
			},
			new()
			{
				Id = Guid.Parse("00002f64-5717-4562-b3fc-2c963f66afa6"),
				Lastname = "Коколов", 
				Firstname = "Андрей", 
				Role = RoleType.Administrator,
				Login = "akokolov",
				Password = "akokolov",
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Frontend и DB разработчик"
			},
			new()
			{
				Id = Guid.Parse("00003f64-5717-4562-b3fc-2c963f66afa6"),
				Lastname = "Селимов", 
				Firstname = "Загидин", 
				Patronymic = "Мурадович", 
				Role = RoleType.Manager,
				Login = "zselimov",
				Password = "zselimov",
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Тех. Архитектор и Тимлид команды"
			},
			new()
			{
				Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6"),
				Lastname = "Ефимов", 
				Firstname = "Алексей", 
				Role = RoleType.Editor,
				Login = "aefimov",
				Password = "aefimov",
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Frontend и Design разработчик"
			}
		};
	}
}
