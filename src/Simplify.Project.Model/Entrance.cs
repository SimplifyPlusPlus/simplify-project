using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Подъезд
/// </summary>
public class Entrance : Entity
{
	/// <summary>
	/// Номер
	/// </summary>
	[Required]
	public int Number { get; set; }

	/// <summary>
	/// Здание
	/// </summary>
	public virtual House House { get; set; } = new();

	/// <summary>
	/// Коллекция квартир
	/// </summary>
	[Required]
	public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

	/// <summary>
	/// Связи клиента с квартирами подъезда
	/// </summary>
	[Required]
	public virtual ICollection<ApartmentRelation> ApartmentRelations { get; set; } = new List<ApartmentRelation>();
}
