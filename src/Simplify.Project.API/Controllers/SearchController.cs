using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Repositories;

namespace Simplify.Project.API.Controllers;

[ApiController]
[Route("search")]
public class SearchController : ControllerBase
{
	private readonly IClientRepository _clientRepository;
	private readonly IApartmentRepository _apartmentRepository;
	private readonly IHouseRepository _houseRepository;

	/// <summary>
	/// Конструктор класса <see cref="SearchController"/>
	/// </summary>
	/// <param name="clientRepository">Репозиторий клиентов</param>
	/// <param name="apartmentRepository">Репозиторий квартир</param>
	/// <param name="houseRepository">Репозиторий домов</param>
	public SearchController(
		IClientRepository clientRepository, 
		IApartmentRepository apartmentRepository, 
		IHouseRepository houseRepository 
	)
	{
		_clientRepository = clientRepository;
		_apartmentRepository = apartmentRepository;
		_houseRepository = houseRepository;
	}

	/// <summary>
	/// Поиск
	/// </summary>
	/// <param name="searchString">Строка, по которой происходит поиск</param>
	/// <param name="ignored">Репозитории, по которым не должен происходить поиск</param>
	/// <returns></returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SearchResultDto>))]
	public IActionResult GetResults([FromQuery] string searchString, [FromQuery] string? ignored = "")
	{
		IEnumerable<SearchResultDto> combinedRepositories = new List<SearchResultDto>();

		if (!(ignored?.Contains("client") ?? false))
			combinedRepositories = combinedRepositories.Concat(_clientRepository.GetClients().Adapt<SearchResultDto[]>());

		if (!(ignored?.Contains("apartment") ?? false))
			combinedRepositories = combinedRepositories.Concat(_apartmentRepository.GetApartments().Include(x => x.Entrance).ThenInclude(x => x.House).Adapt<SearchResultDto[]>());

		if (!(ignored?.Contains("house") ?? false))
			combinedRepositories = combinedRepositories.Concat(_houseRepository.GetHouses().Adapt<SearchResultDto[]>());

		return Ok(combinedRepositories.OrderByDescending(x => x.Score(searchString)).Where(x => x.Score(searchString) > 0.6));
	}	
}
