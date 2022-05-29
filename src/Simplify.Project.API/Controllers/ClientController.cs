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
public class ClientController : ControllerBase
{
	private readonly IClientRepository _clientRepository;

	/// <summary>
	/// Конструктор класса <see cref="ClientController"/>
	/// </summary>
	/// <param name="repository">Репозиторий клиентов</param>
	public ClientController(IClientRepository repository)
	{
		_clientRepository = repository;
	}

	/// <summary>
	/// Добавить нового клиента
	/// </summary>
	/// <param name="clientCreateDto">Данные нового клиента</param>
	[HttpPost]
	[Route("add")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> ClientCreate([FromBody] ClientCreateDto? clientCreateDto)
	{
		if (clientCreateDto == null || clientCreateDto.Id == Guid.Empty)
			return BadRequest();

		var client = clientCreateDto.Adapt<Client>();
		await _clientRepository.AddClient(client);
		return Ok();
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
