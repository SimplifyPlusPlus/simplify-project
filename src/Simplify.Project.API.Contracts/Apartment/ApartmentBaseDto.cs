using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts.Apartment;

/// <summary>
/// Базовая информация по квартире
/// </summary>
public class ApartmentBaseDto
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
}
