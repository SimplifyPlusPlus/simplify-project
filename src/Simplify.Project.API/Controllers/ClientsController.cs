using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Repository;
using Simplify.Project.Model;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с клиентами
/// </summary>
[ApiController]
[Route("clients")]
public class ClientsController : ControllerBase
{
    private IClientRepository _clientRepository;
    
    /// <summary>
    /// Конструктор класса <see cref="ClientsController"/>
    /// </summary>
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
        var clients = _clientRepository.GetClients();
        return Ok(clients);
    }
    
    /// <summary>
    /// Получить клиента по идентификатору 
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Клиента</returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetClient(Guid id)
    {
        var client = _clientRepository.GetClient(id);
        return client == null ? NotFound() : Ok(client);
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
            .Select(client => new ClientBaseDto
            {
                Id = client.Id,
                Lastname = client.Lastname,
                Firstname = client.Firstname,
                Email = client.Email,
                Phone = client.Phone,
                IsBlocked = client.IsBlocked,
            });
        return Ok(clients);
    }
}