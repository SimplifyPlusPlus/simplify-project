using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;
using Mapster;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с клиентами
/// </summary>
[ApiController]
[Route("clients")]
public class ClientsController : ControllerBase
{
	private readonly IClientRepository _clientRepository;
    
	/// <summary>
	/// Конструктор класса <see cref="ClientsController"/>
	/// </summary>
	/// <param name="repository">Репозиторий клиентов</param>
	public ClientsController(IClientRepository repository)
	{
		_clientRepository = repository;
	}
    
	/// <summary>
	/// Получить всех клиентов системы
	/// </summary>
	/// <returns>Список клиентов</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Client>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetClients()
	{
		// TODO: Client преобразовать в ClientDetailedDto
		var clients = _clientRepository.GetClients();
		return Ok(clients);
	}
    
	/// <summary>
	/// Получить клиента по идентификатору 
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Клиента</returns>
	[HttpGet("{id:Guid}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientBaseDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetClient(Guid id)
	{
		var client = _clientRepository.GetClient(id);
		return client == null ? NotFound() : Ok(client.Adapt<ClientDetailedDto>());
	}
    
	/// <summary>
	/// Получить базовую информацию по клиентам системы
	/// </summary>
	/// <returns>Список клиентов</returns>
	[HttpGet("/clients-base-information")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientBaseDto>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetClientsBaseInformation()
	{
		var clients = _clientRepository
			.GetClients()
			.Adapt<ClientBaseDto[]>();
		return Ok(clients);
	}
}
