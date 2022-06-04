using System.ComponentModel.DataAnnotations;
using Simplify.Project.API.Contracts.Apartment;

namespace Simplify.Project.API.Contracts.Client;

/// <summary>
///     Детальная информация по клиенту
/// </summary>
public class ClientDetailedDto : ClientBaseDto
{
	/// <summary>
	///     Электронная почта
	/// </summary>
	[Required]
	public string Email { get; set; } = string.Empty;

	/// <summary>
	///     Номер телефона
	/// </summary>
	[Required]
	public string Phone { get; set; } = string.Empty;

	/// <summary>
	///     Заметка
	/// </summary>
	public string? Note { get; set; }

	/// <summary>
	///     Отношения клиента с квартирами
	/// </summary>
	public ICollection<ApartmentRelationDto> ApartmentsRelations { get; set; } = new List<ApartmentRelationDto>();
}
