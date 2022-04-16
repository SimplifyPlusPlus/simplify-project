using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Handbooks;
using Simplify.Project.Model.Utils;
using System.ComponentModel.DataAnnotations.Schema;

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
	public Apartment Apartment { get; set; } = new();

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

	/// <summary>
	/// Клиент
	/// </summary>
	[Required]
	public Client Client { get; set; } = new();
}
