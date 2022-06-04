using System.ComponentModel.DataAnnotations;
using Simplify.Project.API.Contracts.Client;
using Simplify.Project.Shared;

namespace Simplify.Project.API.Contracts.Apartment;

/// <summary>
///     Объект передачи данных для отношения квартиры и пользователя
/// </summary>
public class ApartmentRelationDto
{
	/// <summary>
	///     Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }

	/// <summary>
	///     Квартира
	/// </summary>
	[Required]
	public ApartmentBaseDto Apartment { get; set; } = new();

	/// <summary>
	///     Тип связи
	/// </summary>
	[Required]
	public ApartmentRelationType RelationType { get; set; }

	/// <summary>
	///     Дата добавления связи
	/// </summary>
	[Required]
	public DateTime Created { get; set; }

	/// <summary>
	///     Клиент
	/// </summary>
	[Required]
	public ClientBaseDto Client { get; set; } = new();
}
