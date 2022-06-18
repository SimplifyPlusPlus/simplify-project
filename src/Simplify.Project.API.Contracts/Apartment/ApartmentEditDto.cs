using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts.Apartment;

/// <summary>
/// Модель данных для редактирования квартиры
/// </summary>
public class ApartmentEditDto
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
	/// Полное наименование
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Связи квартиры с клиентами
	/// </summary>
	public ICollection<ApartmentRelationDto> ApartmentRelations { get; set; } = new List<ApartmentRelationDto>();
}
