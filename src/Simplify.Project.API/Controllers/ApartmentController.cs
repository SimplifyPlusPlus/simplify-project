using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simplify.Project.API.Contracts.Apartment;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с квартирами
/// </summary>
[ApiController]
[Route("api/apartment")]
public class ApartmentController : ControllerBase
{
	private readonly IApartmentRepository _apartmentRepository;
	private readonly IApartmentRelationRepository _apartmentRelationRepository;

	/// <summary>
	/// Конструктор класса <see cref="ApartmentController"/>
	/// </summary>
	/// <param name="apartmentRepository">Репозиторий квартир</param>
	/// <param name="apartmentRelationRepository">Репозиторий отношений клиент-квартира</param>
	public ApartmentController(IApartmentRepository apartmentRepository, IApartmentRelationRepository apartmentRelationRepository)
	{
		_apartmentRepository = apartmentRepository;
		_apartmentRelationRepository = apartmentRelationRepository;
	}

	/// <summary>
	/// Получить данные квартиры для изменения
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Данные квартиры</returns>
	[HttpGet]
	[Route("{id:Guid}/for-edit")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentEditDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetApartmentForEdit([FromRoute] Guid? id)
	{
		if (id == null || id == Guid.Empty)
			return BadRequest();

		var apartment = _apartmentRepository.GetApartments()
			.Include(apartment => apartment.Entrance)
				.ThenInclude(entrance => entrance.House)
			.Include(apartment => apartment.ApartmentRelations)
				.ThenInclude(relation => relation.Client)
			.SingleOrDefault(apartment => apartment.Id == id.Value);
		return apartment == null ? NotFound(nameof(apartment)) : Ok(apartment.Adapt<ApartmentEditDto>());
	}

	/// <summary>
	/// Частичное изменение данных квартиры
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="apartmentEditDto">Измененные данные квартиры</param>
	/// <param name="clientRepository"></param>
	[HttpPatch]
	[Route("{id:Guid}/edit")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetClientEdit([FromRoute] Guid? id, [FromBody] ApartmentEditDto? apartmentEditDto, 
		[FromServices] IClientRepository clientRepository)
	{
		if (id == null || id == Guid.Empty || apartmentEditDto == null)
			return BadRequest();

		var apartment = _apartmentRepository.GetApartment(id.Value);
		if (apartment == null)
			return NotFound(nameof(apartment));
		
		// Саму квартиру можно не изменять, ибо с веб-интерфейса можно изменить только связи с квартирой
		var relations = apartmentEditDto.ApartmentRelations.Select(relation => relation.Adapt<ApartmentRelation>()).ToList();
		foreach (var relation in relations)
		{
			var client = clientRepository.GetClient(relation.Client.Id);
			if (client == null)
				return NotFound(nameof(client));
			relation.Client = client;
			relation.Apartment = apartment;
		}

		await _apartmentRelationRepository.RemoveApartmentRelations(id.Value);
		await _apartmentRelationRepository.AddApartmentRelationsRange(relations);

		return NoContent();
	}
}
