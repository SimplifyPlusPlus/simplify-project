using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Repositories;
using Simplify.Project.API.Contracts;
using AutoMapper;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с подъездами
/// </summary>
[ApiController]
[Route("entrances")]
public class EntrancesController : ControllerBase
{
	private readonly IEntranceRepository _repository;
	private readonly IMapper _mapper;

	/// <summary>
	/// Конструктор класса <see cref="EntrancesController"/>
	/// </summary>
	/// <param name="repository">Репозиторий подъездов</param>
	/// <param name="mapper">Маппер</param>
	public EntrancesController(IEntranceRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	
	/// <summary>
	/// Получить все подъезды
	/// </summary>
	/// <returns>Список подъездов</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntranceBaseDto>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetEntrances()
	{
		var entrances = _repository.GetEntrances();
		return Ok(_mapper.Map<EntranceBaseDto[]>(entrances));
	}
}
