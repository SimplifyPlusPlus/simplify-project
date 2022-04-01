using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;
using AutoMapper;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с клиентами
/// </summary>
[ApiController]
[Route("clients")]
public class ClientsController : ControllerBase
{
    private IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    /// <summary>
    /// Конструктор класса <see cref="ClientsController"/>
    /// </summary>
    /// <param name="repository">Репозиторий клиентов</param>
    /// <param name="mapper">Маппер</param>
    public ClientsController(IClientRepository repository, IMapper mapper)
    {
        _clientRepository = repository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Получить всех клиентов системы
    /// </summary>
    /// <returns>Список клиентов</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientBaseDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetClients()
    {
        // TODO: Client преобразовать в ClientDetailedDto
        var clients = _clientRepository.GetClients();
        return Ok(_mapper.Map<ClientBaseDto[]>(clients));
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
        return client == null ? NotFound() : Ok(_mapper.Map<ClientBaseDto>(client));
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
            .GetClients();
        return Ok(_mapper.Map<ClientBaseDto[]>(clients));
    }
}
