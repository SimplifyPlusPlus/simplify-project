using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts;

/// <summary>
/// Объект передачи данных для отношения квартиры и пользователя
/// </summary>
public class ApartmentRelationDto
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }

	/// <summary>
	/// Квартира
	/// </summary>
	[Required]
	public ApartmentBaseDto Apartment { get; set; } = new();

	/// <summary>
	/// Тип связи
	/// </summary>
	[Required] 
	public string RelationType { get; set; } = string.Empty;
	
	/// <summary>
	/// Дата добавления связи
	/// </summary>
	[Required]
	public DateTime Created { get; set; }

	/// <summary>
	/// Клиент
	/// </summary>
	[Required]
	public ClientBaseDto Client { get; set; } = new();
}
