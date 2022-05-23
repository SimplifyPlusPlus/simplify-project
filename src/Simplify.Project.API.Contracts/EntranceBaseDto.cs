using System.ComponentModel.DataAnnotations;
using Simplify.Project.API.Contracts.Apartment;

namespace Simplify.Project.API.Contracts;

/// <summary>
/// Базовая информация по подъезду
/// </summary>
public class EntranceBaseDto
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }
	
	/// <summary>
	/// Номер
	/// </summary>
	[Required]
	public int Number { get; set; }
	
	/// <summary>
	/// Коллекция квартир подъезда
	/// </summary>
	[Required]
	public ICollection<ApartmentRelationDto> Apartments { get; set; } = new List<ApartmentRelationDto>();
}
