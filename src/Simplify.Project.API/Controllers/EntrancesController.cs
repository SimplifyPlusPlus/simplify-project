using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с подъездами
/// </summary>
[ApiController]
[Route("entrances")]
public class EntrancesController : ControllerBase
{
	private readonly IEntranceRepository _repository;

	/// <summary>
	/// Конструктор класса <see cref="EntrancesController"/>
	/// </summary>
	/// <param name="repository">Репозиторий подъездов</param>
	public EntrancesController(IEntranceRepository repository)
	{
		_repository = repository;
	}
	
	/// <summary>
	/// Получить все подъезды
	/// </summary>
	/// <returns>Список подъездов</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Entrance>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetEntrances()
	{
		var entrances = _repository.GetEntrances();
		return Ok(entrances);
	}
}
