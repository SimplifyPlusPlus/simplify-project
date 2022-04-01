using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с домами
/// </summary>
[ApiController]
[Route("houses")]
public class HousesController : ControllerBase
{
	private readonly IHouseRepository _repository;

	/// <summary>
	/// Конструктор класса <see cref="EstatesController"/>
	/// </summary>
	/// <param name="repository">Репозиторий домов</param>
	public HousesController(IHouseRepository repository)
	{
		_repository = repository;
	}
	
	/// <summary>
	/// Получить все дома
	/// </summary>
	/// <returns>Список домов</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<House>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetHouses()
	{
		var houses = _repository.GetHouses();
		return Ok(houses);
	}
}