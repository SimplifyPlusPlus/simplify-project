using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Handbooks;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Модель связи клиента с квартирой
/// </summary>
public class ApartmentRelation : Entity
{
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