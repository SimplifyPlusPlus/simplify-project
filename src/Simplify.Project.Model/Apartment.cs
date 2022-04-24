using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Квартира
/// </summary>
public class Apartment : Entity, ISearchable
{
	/// <summary>
	/// Номер
	/// </summary>
	[Required]
	public int Number { get; set; }

	/// <summary>
	/// Подъезд
	/// </summary>
	public virtual Entrance Entrance { get; set; } = new();

	/// <summary>
	/// Отношения
	/// </summary>
	public virtual ICollection<ApartmentRelation> ApartmentRelations { get; set; } = new List<ApartmentRelation>();

	///<inheritdoc/>
	public double Score(string searchValue) => JaroWinklerDistance.Proximity($"{Entrance.House.Street} {Entrance.House.Number} {Entrance.House.Building} {Number}", searchValue);
}
