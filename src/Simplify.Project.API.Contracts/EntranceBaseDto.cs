using System.ComponentModel.DataAnnotations;

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
	public ICollection<ApartmentBaseDto> Apartments { get; set; } = new List<ApartmentBaseDto>();
}
