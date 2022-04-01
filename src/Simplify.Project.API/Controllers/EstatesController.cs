using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;
using Simplify.Project.API.Contracts;
using AutoMapper;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с комплексами
/// </summary>
[ApiController]
[Route("estates")]
public class EstatesController : ControllerBase
{
	private readonly IEstateRepository _repository;
	private readonly IMapper _mapper;

	/// <summary>
	/// Конструктор класса <see cref="EstatesController"/>
	/// </summary>
	/// <param name="repository">Репозиторий комплексов</param>
	/// <param name="mapper">Маппер</param>
	public EstatesController(IEstateRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	
	/// <summary>
	/// Получить все комплексы
	/// </summary>
	/// <returns>Список комплексов</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EstateBaseDto>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetEstates()
	{
		var estates = _repository.GetEstates();
		return Ok(_mapper.Map<EstateBaseDto[]>(estates));
	}
}
