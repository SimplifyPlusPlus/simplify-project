using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с комплексами
/// </summary>
[ApiController]
[Route("estates")]
public class EstatesController : ControllerBase
{
	private IEstateRepository _repository;

	/// <summary>
	/// Конструктор класса <see cref="EstatesController"/>
	/// </summary>
	/// <param name="repository">Репозиторий комплексов</param>
	public EstatesController(IEstateRepository repository)
	{
		_repository = repository;
	}
	
	/// <summary>
	/// Получить все комплексы
	/// </summary>
	/// <returns>Список комплексов</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Estate>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetEstates()
	{
		var estates = _repository.GetEstates();
		return Ok(estates);
	}
}
