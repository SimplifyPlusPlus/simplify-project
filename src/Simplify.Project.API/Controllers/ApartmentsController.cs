using Mapster;
using Microsoft.AspNetCore.Mvc;
using Simplify.Project.API.Contracts.Apartment;
using Simplify.Project.API.Repositories;
using Simplify.Project.Model;

namespace Simplify.Project.API.Controllers;

/// <summary>
/// Контроллер для работы с квартирами
/// </summary>
[ApiController]
[Route("api/apartment")]
public class ApartmentsController : ControllerBase
{
	private readonly IApartmentRepository _apartmentRepository;
	private readonly IApartmentRelationRepository _apartmentRelationRepository;

	/// <summary>
	/// Конструктор класса <see cref="ApartmentsController"/>
	/// </summary>
	/// <param name="apartmentRepository">Репозиторий квартир</param>
	/// <param name="apartmentRelationRepository">Репозиторий отношений клиент-квартира</param>
	public ApartmentsController(IApartmentRepository apartmentRepository, IApartmentRelationRepository apartmentRelationRepository)
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

		var apartment = _apartmentRepository.GetApartment(id.Value);
		if (apartment == null)
			return NotFound(nameof(apartment));

		var apartmentEditDto = apartment.Adapt<ApartmentEditDto>();
		var apartmentRelations = _apartmentRelationRepository
			.GetApartmentRelations(id.Value)
			.Select(relation => relation.Adapt<ApartmentRelationDto>())
			.ToList();

		apartmentEditDto.ApartmentRelations = apartmentRelations;
		return Ok(apartmentEditDto);
	}

	/// <summary>
	/// Частичное изменение данных квартиры
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="apartmentEditDto">Измененные данные квартиры</param>
	[HttpPatch]
	[Route("{id:Guid}/edit")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult GetClientEdit([FromRoute] Guid? id, [FromBody] ApartmentEditDto? apartmentEditDto)
	{
		if (id == null || id == Guid.Empty || apartmentEditDto == null)
			return BadRequest();

		var oldApartment = _apartmentRepository.GetApartment(apartmentEditDto.Id);
		if (oldApartment == null)
			return NotFound(nameof(oldApartment));

		// Саму квартиру можно не изменять, ибо с веб-интерфейса можно изменить только связи с квартирой
		var relations = apartmentEditDto.ApartmentRelations.Select(relation => relation.Adapt<ApartmentRelation>());
		_apartmentRelationRepository.RemoveApartmentRelations(id.Value);
		_apartmentRelationRepository.AddApartmentRelationsRange(relations);

		return NoContent();
	}
}
