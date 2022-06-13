using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Contracts.Search;
using Simplify.Project.API.Repositories;
using Simplify.Project.Shared;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер поиска
/// </summary>
[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
	private const double MinimalSearchScore = 0.6;
	private readonly IClientRepository _clientRepository;
	private readonly IApartmentRepository _apartmentRepository;

	/// <summary>
	/// Конструктор класса <see cref="SearchController"/>
	/// </summary>
	/// <param name="clientRepository">Репозиторий клиентов</param>
	/// <param name="apartmentRepository">Репозиторий квартир</param>
	public SearchController(IClientRepository clientRepository, IApartmentRepository apartmentRepository)
	{
		_clientRepository = clientRepository;
		_apartmentRepository = apartmentRepository;
	}

	/// <summary>
	/// Поиск
	/// </summary>
	/// <param name="searchString">Строка, по которой происходит поиск</param>
	/// <param name="target">Репозитории, по которым должен происходить поиск</param>
	/// <returns></returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SearchResultDto>))]
	public IActionResult GetResults([FromQuery] string searchString, [FromQuery] string? target = null)
	{
		IEnumerable<SearchResultDto> combinedRepositories = new List<SearchResultDto>();
		
		if (target?.Contains(HandbookSearchType.Clients.ToString()) ?? false)
			combinedRepositories = combinedRepositories.Concat(_clientRepository.GetClients().Adapt<SearchResultDto[]>());

		if (target?.Contains(HandbookSearchType.Apartments.ToString()) ?? false)
			combinedRepositories = combinedRepositories.Concat(_apartmentRepository.GetApartments()
				.Include(apartment => apartment.Entrance)
					.ThenInclude(house => house.House)
				.Adapt<SearchResultDto[]>());

		var result = combinedRepositories
			.OrderByDescending(searchItem => searchItem.Score(searchString))
			.Where(searchItem => searchItem.Score(searchString) > MinimalSearchScore);
		return Ok(result);
	}
	
	/// <summary>
	/// Поиск клиентов
	/// </summary>
	/// <param name="searchString">Строка, по которой происходит поиск</param>
	/// <returns></returns>
	[HttpGet]
	[Route("client")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SearchResultDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetClientResults([FromQuery] string? searchString)
	{
		if (string.IsNullOrEmpty(searchString))
			return BadRequest();

		var client = _clientRepository.GetClients().Adapt<SearchClientResultDto[]>();
		
		var result = client
			.OrderByDescending(searchItem => searchItem.Score(searchString))
			.Where(searchItem => searchItem.Score(searchString) > MinimalSearchScore);
		return Ok(result);
	}
}
