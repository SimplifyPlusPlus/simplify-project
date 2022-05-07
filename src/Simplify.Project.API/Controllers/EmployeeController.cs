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
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDetailedDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
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
		return Ok(employee.Adapt<EmployeeDetailedDto>());
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
