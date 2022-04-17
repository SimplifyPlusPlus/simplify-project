using Mapster;
using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Repositories;

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
	[HttpGet("{id:Guid}/detailed")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDetailedDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetEmployeeDetailed(Guid id)
	{
		var employee = _repository.GetEmployee(id);
		return employee == null ? NotFound() : Ok(employee.Adapt<EmployeeDetailedDto>());
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
