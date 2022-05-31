using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using Simplify.Project.Shared;

namespace Simplify.Project.Model;

/// <summary>
/// Модель связи клиента с квартирой
/// </summary>
public class ApartmentRelation : Entity
{
	/// <summary>
	/// Квартира
	/// </summary>
	[Required]
	public virtual Apartment Apartment { get; set; } = new();

	/// <summary>
	/// Тип связи
	/// </summary>
	[Required] 
	public ApartmentRelationType RelationType { get; set; }
	
	/// <summary>
	/// Дата добавления связи
	/// </summary>
	[Required]
	public DateTime Created { get; set; }

	/// <summary>
	/// Клиент
	/// </summary>
	[Required]
	public virtual Client Client { get; set; } = new();
}
