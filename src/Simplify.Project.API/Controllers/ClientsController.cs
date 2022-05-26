using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Repositories;
using Mapster;
using Simplify.Project.API.Contracts.Client;
using Simplify.Project.Model;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с клиентами
/// </summary>
[ApiController]
[Route("api/client")]
public class ClientsController : ControllerBase
{
	private readonly IClientRepository _clientRepository;
	private readonly IApartmentRelationRepository _apartmentRelationRepository;
    
	/// <summary>
	/// Конструктор класса <see cref="ClientsController"/>
	/// </summary>
	/// <param name="repository">Репозиторий клиентов</param>
	/// <param name="apartmentRelationRepository">Репозиторий отношений клиент-квартира</param>
	public ClientsController(IClientRepository repository, IApartmentRelationRepository apartmentRelationRepository)
	{
		_clientRepository = repository;
		_apartmentRelationRepository = apartmentRelationRepository;
	}

	/// <summary>
	/// Получить клиента по идентификатору 
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Детальная информация по клиенту</returns>
	[HttpGet]
	[Route("{id:Guid}/detailed")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDetailedDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetClientDetailed([FromRoute] Guid? id)
	{
		if (id == null || id == Guid.Empty)
			return BadRequest();

		var client = _clientRepository.GetClient(id.Value);
		return client == null ? NotFound(nameof(client)) : Ok(client.Adapt<ClientDetailedDto>());
	}
	
	/// <summary>
	/// Получить данные клиента для изменения
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Данные клиента</returns>
	[HttpGet]
	[Route("{id:Guid}/for-edit")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientEditDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetClientForEdit([FromRoute] Guid? id)
	{
		if (id == null || id == Guid.Empty)
			return BadRequest();
		
		var client = _clientRepository.GetClient(id.Value);
		return client == null ? NotFound(nameof(client)) : Ok(client.Adapt<ClientEditDto>());
	}

	/// <summary>
	/// Частичное изменение данных клиента
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="clientEditDto">Измененные данные клиента</param>
	[HttpPatch]
	[Route("{id:Guid}/edit")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetClientEdit([FromRoute] Guid? id, [FromBody] ClientEditDto? clientEditDto)
	{
		if (id == null || id == Guid.Empty || clientEditDto == null)
			return BadRequest();
		
		var oldClient = _clientRepository.GetClient(id.Value);
		if (oldClient == null)
			return NotFound(nameof(oldClient));

		var client = clientEditDto.Adapt<Client>();
		client.Created = oldClient.Created;
		await _clientRepository.UpdateClient(id.Value, client);
		return NoContent();
	}
}
