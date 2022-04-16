using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Квартира
/// </summary>
public class Apartment : Entity
{
	/// <summary>
	/// Номер
	/// </summary>
	[Required]
	public int Number { get; set; }

	/// <summary>
	/// Подъезд
	/// </summary>
	public Entrance Entrance { get; set; } = new();

	/// <summary>
	/// Отношения
	/// </summary>
	public virtual ICollection<ApartmentRelation> ApartmentRelations { get; set; } = new List<ApartmentRelation>();
}
