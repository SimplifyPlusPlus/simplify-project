using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с квартирами
/// </summary>
[ApiController]
[Route("apartments")]
public class ApartmentsController : ControllerBase
{
	private readonly IApartmentRepository _repository;

	/// <summary>
	/// Конструктор класса <see cref="ApartmentsController"/>
	/// </summary>
	/// <param name="repository">Репозиторий квартир</param>
	public ApartmentsController(IApartmentRepository repository)
	{
		_repository = repository;
	}

	/// <summary>
	/// Получить все квартиры
	/// </summary>
	/// <returns>Список квартир</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Apartment>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetApartments()
	{
		var apartments = _repository.GetApartments();
		return Ok(apartments);
	}
}