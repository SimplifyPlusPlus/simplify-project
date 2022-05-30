using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simplify.Project.API.Contracts.Events;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;
using Simplify.Project.Shared;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с событиями
/// </summary>
[ApiController]
[Route("api/event")]
public class EventController : ControllerBase
{
	private readonly IEventRepository _repository;

	/// <summary>
	/// Конструктор класса <see cref="EventController"/>
	/// </summary>
	/// <param name="repository">Репозиторий событий</param>
	public EventController(IEventRepository repository)
	{
		_repository = repository;
	}
	
	/// <summary>
	/// Получить все события
	/// </summary>
	/// <returns>Список событий</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EventDto>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetEvents([FromServices] IEmployeeRepository employeeRepository, 
		[FromServices] IApartmentRepository apartmentRepository, [FromServices] IClientRepository clientRepository)
	{
		var events = _repository.GetEvents();
		var employeesById = employeeRepository.GetEmployees().ToDictionary(employee => employee.Id);
		var apartmentsById = apartmentRepository.GetApartments()
			.Include(apartment => apartment.Entrance)
				.ThenInclude(entrance => entrance.House)
			.Include(apartment => apartment.ApartmentRelations)
				.ThenInclude(relation => relation.Client)
			.ToDictionary(apartment => apartment.Id);
		var clientsById = clientRepository.GetClients().ToDictionary(client => client.Id);

		var eventDtos = new List<EventDto>();
		foreach (var ev in events)
		{
			var data = GetEventData(ev.Data, employeesById, apartmentsById, clientsById);
			var eventDto = new EventDto
			{
				Id = ev.Id,
				EventEntityType = ev.EventEntityType,
				EventType = ev.EventType,
				Created = ev.Created,
				Data = data,
			};

			eventDtos.Add(eventDto);
		}

		return Ok(eventDtos);
	}

	private static IDictionary<string, object> GetEventData(
		IReadOnlyDictionary<string, object> eventData, IDictionary<Guid, Employee> employeesById, 
		IDictionary<Guid, Apartment> apartmentsById, IDictionary<Guid, Client> clientsById)
	{
		var data = new Dictionary<string, object>();
		if (eventData.ContainsKey(EventDataKeys.CreatorId))
		{
			var creatorId = Guid.Parse(eventData[EventDataKeys.CreatorId].ToString()!);
			if (employeesById.ContainsKey(creatorId))
			{
				var creator = employeesById[creatorId];
				data.Add(EventDataKeys.Creator, $"{creator.Lastname} {creator.Firstname} {creator.Patronymic}".Trim());
			}
		}
		
		if (eventData.ContainsKey(EventDataKeys.EmployeeId))
		{
			var employeeId = Guid.Parse(eventData[EventDataKeys.EmployeeId].ToString()!);
			if (employeesById.ContainsKey(employeeId))
			{
				var employee = employeesById[employeeId];
				data.Add(EventDataKeys.Employee, $"{employee.Lastname} {employee.Firstname} {employee.Patronymic}".Trim());
			}
		}
			
		if (eventData.ContainsKey(EventDataKeys.ApartmentId))
		{
			var apartmentId = Guid.Parse(eventData[EventDataKeys.ApartmentId].ToString()!);
			if (apartmentsById.ContainsKey(apartmentId))
			{
				var apartment = apartmentsById[apartmentId];
				data.Add(EventDataKeys.Apartment, $"{apartment.Entrance.House.Street} {apartment.Entrance.House.Number}, {apartment.Entrance.House.Building}".Trim() + $" кв. {apartment.Number}");
			}
		}
			
		if (eventData.ContainsKey(EventDataKeys.ClientId))
		{
			var clientId = Guid.Parse(eventData[EventDataKeys.ClientId].ToString()!);
			if (clientsById.ContainsKey(clientId))
			{
				var client = clientsById[clientId];
				data.Add(EventDataKeys.Client, $"{client.Lastname} {client.Firstname} {client.Patronymic}".Trim());
			}
		}
		
		if (eventData.ContainsKey(EventDataKeys.RelationType))
		{
			data.Add(EventDataKeys.RelationType, eventData[EventDataKeys.RelationType].ToString()!);
		}

		return data;
	}
}
