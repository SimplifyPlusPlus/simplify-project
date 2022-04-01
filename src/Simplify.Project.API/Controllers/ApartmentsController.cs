using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;
using AutoMapper;
using Simplify.Project.API.Contracts;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с квартирами
/// </summary>
[ApiController]
[Route("apartments")]
public class ApartmentsController : ControllerBase
{
	private readonly IApartmentRepository _repository;
	private readonly IMapper _mapper;
	/// <summary>
	/// Конструктор класса <see cref="ApartmentsController"/>
	/// </summary>
	/// <param name="repository">Репозиторий квартир</param>
	/// <param name="mapper">Маппер</param>
	public ApartmentsController(IApartmentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	/// <summary>
	/// Получить все квартиры
	/// </summary>
	/// <returns>Список квартир</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApartmentBaseDto>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetApartments()
	{
		var apartments = _repository.GetApartments();
		return Ok(_mapper.Map<ApartmentBaseDto[]>(apartments));
	}
}
