using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;
using Simplify.Project.API.Contracts;
using AutoMapper;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с домами
/// </summary>
[ApiController]
[Route("houses")]
public class HousesController : ControllerBase
{
	private readonly IHouseRepository _repository;
	private readonly IMapper _mapper;

	/// <summary>
	/// Конструктор класса <see cref="EstatesController"/>
	/// </summary>
	/// <param name="repository">Репозиторий домов</param>
	/// <param name="mapper">Маппер</param>
	public HousesController(IHouseRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	
	/// <summary>
	/// Получить все дома
	/// </summary>
	/// <returns>Список домов</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HouseBaseDto>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetHouses()
	{
		var houses = _repository.GetHouses();
		return Ok(_mapper.Map<HouseBaseDto[]>(houses));
	}
}
