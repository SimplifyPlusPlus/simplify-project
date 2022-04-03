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
}
