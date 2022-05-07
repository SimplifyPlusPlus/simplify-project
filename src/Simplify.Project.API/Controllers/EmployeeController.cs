using Mapster;
using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Contracts.Employee;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с сотрудниками
/// </summary>
[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
	private readonly IEmployeeRepository _repository;

	/// <summary>
	/// Конструктор класса <see cref="EmployeeController"/>
	/// </summary>
	/// <param name="repository">Репозиторий сотрудников</param>
	public EmployeeController(IEmployeeRepository repository)
	{
		_repository = repository;
	}
	
	/// <summary>
	/// Получить всех сотрудников системы
	/// </summary>
	/// <returns>Детальная информация по всем сотрудникам</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDetailedDto>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetEmployeesDetailed()
	{
		var employees = _repository
			.GetEmployees()
			.Adapt<EmployeeDetailedDto[]>();
		return Ok(employees);
	}
    
	/// <summary>
	/// Получить детальную информацию по сотруднику по идентификатору 
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Детальная информация по сотруднику</returns>
	[HttpGet]
	[Route("{id:Guid}/detailed")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDetailedDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetEmployeeDetailed(Guid id)
	{
		var employee = _repository.GetEmployee(id);
		return employee == null 
			? NotFound(nameof(EmployeeDetailedDto)) 
			: Ok(employee.Adapt<EmployeeDetailedDto>());
	}

	/// <summary>
	/// Добавить нового пользователя
	/// </summary>
	/// <param name="employeeCreateDto">Данные по сотруднику</param>
	/// <returns>Созданный сотрудник</returns>
	[HttpPost]
	[Route("add")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult AddNewEmployee([FromBody] EmployeeCreateDto? employeeCreateDto)
	{
		if (employeeCreateDto == null)
			return BadRequest();
		
		var employee = new Employee
		{
			Id = Guid.NewGuid(),
			Created = DateTime.Now,
			Lastname = employeeCreateDto.Lastname,
			Firstname = employeeCreateDto.Firstname,
			Patronymic = employeeCreateDto.Patronymic,
			IsBlocked = false,
			Role = employeeCreateDto.Role,
			Login = employeeCreateDto.Login,
			Password = employeeCreateDto.Password,
			Note = employeeCreateDto.Note,
		};
		
		_repository.AddEmployee(employee);
		return NoContent();
	}

	/// <summary>
	/// Получить данные пользователя для изменения
	/// </summary>
	/// <param name="id"></param>
	/// <returns>Созданный сотрудник</returns>
	[HttpGet]
	[Route("{id:Guid}/for-edit")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult ForEditEmployee([FromRoute] Guid? id)
	{
		if (id == null || id == Guid.Empty)
			return BadRequest();

		var employee = _repository.GetEmployee(id.Value);
		return employee == null 
			? NotFound(nameof(employee)) 
			: Ok(employee.Adapt<EmployeeEditDto>());
	}
	
	/// <summary>
	/// Частичное изменение данных пользователя
	/// </summary>
	/// <param name="id"></param>
	/// <param name="employeeEditDto"></param>
	/// <returns>Созданный сотрудник</returns>
	[HttpPatch]
	[Route("{id:Guid}/edit")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult EditEmployee([FromRoute] Guid? id, [FromBody] EmployeeEditDto? employeeEditDto)
	{
		if (id == null || id == Guid.Empty || employeeEditDto == null)
			return BadRequest();
		
		var oldEmployee = _repository.GetEmployee(id.Value);
		if (oldEmployee == null)
			return NotFound(nameof(oldEmployee));
		
		var employee = new Employee
		{
			Id = employeeEditDto.Id,
			Created = oldEmployee.Created,
			Lastname = employeeEditDto.Lastname,
			Firstname = employeeEditDto.Firstname,
			Patronymic = employeeEditDto.Patronymic,
			IsBlocked = employeeEditDto.IsBlocked,
			Role = employeeEditDto.Role,
			Login = employeeEditDto.Login,
			Password = employeeEditDto.Password,
			Note = employeeEditDto.Note,
		};
		
		_repository.UpdateEmployee(id.Value, employee);
		return NoContent();
	}
    
	/// <summary>
	/// Получить базовую информацию по сотрудникам системы
	/// </summary>
	/// <returns>Базовая информация по всем сотрудникам</returns>
	[HttpGet("base-information")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeBaseDto>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetEmployeesBaseInformation()
	{
		var employees = _repository
			.GetEmployees()
			.Adapt<EmployeeBaseDto[]>();
		return Ok(employees);
	}
}
