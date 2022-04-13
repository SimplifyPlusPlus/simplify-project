using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Дом
/// </summary>
public class House : Entity
{
	/// <summary>
	/// Номер
	/// </summary>
	[Required]
	public int Number { get; set; }

	/// <summary>
	/// Улица
	/// </summary>
	[Required]
	public string Street { get; set; } = string.Empty;
    
	/// <summary>
	/// Корпус
	/// </summary>
	public string? Building { get; set; }

	/// <summary>
	/// Жилой комплекс
	/// </summary>
	public virtual Estate Estate { get; set; } = new();

	/// <summary>
	/// Идентификаторы подъездов
	/// </summary>
	[Required]
	public virtual ICollection<Entrance> Entrances { get; set; } = new List<Entrance>();
}
