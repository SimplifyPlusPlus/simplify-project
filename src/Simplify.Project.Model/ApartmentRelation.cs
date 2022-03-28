using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Handbooks;

namespace Simplify.Project.Model;

/// <summary>
/// Модель связи клиента с квартирой
/// </summary>
public class ApartmentRelation
{
	/// <summary>
	/// Идентификатор связи
	/// </summary>
	[Required]
	public Guid Id { get; set; }
	
	/// <summary>
	/// Идентификатор квартиры
	/// </summary>
	[Required]
	public Guid ApartmentId { get; set; }

	/// <summary>
	/// Тип связи
	/// </summary>
	[Required] 
	public string RelationType { get; set; } = ApartmentRelationType.Empty;
	
	/// <summary>
	/// Дата добавления связи
	/// </summary>
	[Required]
	public DateTime Created { get; set; }
}