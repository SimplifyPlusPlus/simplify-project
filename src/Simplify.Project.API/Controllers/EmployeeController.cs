using Mapster;
using Microsoft.AspNetCore.Mvc;
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
	/// <param name="id">Идентификатор сотрудника</param>
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
	/// Добавить нового сотрудника
	/// </summary>
	/// <param name="employeeCreateDto">Данные по сотруднику</param>
	[HttpPost]
	[Route("add")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult AddNewEmployee([FromBody] EmployeeCreateDto? employeeCreateDto)
	{
		if (employeeCreateDto == null)
			return BadRequest();

		var employee = employeeCreateDto.Adapt<Employee>();
		_repository.AddEmployee(employee);
		
		return NoContent();
	}

	/// <summary>
	/// Получить данные пользователя для изменения
	/// </summary>
	/// <param name="id">Идентификатор сотрудника</param>
	/// <returns>Данные сотрудника</returns>
	[HttpGet]
	[Route("{id:Guid}/for-edit")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeEditDto))]
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
	/// Частичное изменение данных сотрудника
	/// </summary>
	/// <param name="id">Идентификатор сотрудника</param>
	/// <param name="employeeEditDto">Измененные данные сотрудника</param>
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

		var employee = employeeEditDto.Adapt<Employee>();
		employee.Created = oldEmployee.Created;
		_repository.UpdateEmployee(id.Value, employee);
		
		return NoContent();
	}
    
	/// <summary>
	/// Получить базовую информацию по сотрудникам системы
	/// </summary>
	/// <returns>Базовая информация по всем сотрудникам</returns>
	[HttpGet]
	[Route("base-information")]
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
