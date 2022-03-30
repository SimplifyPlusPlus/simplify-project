using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.Model.Utils;

/// <summary>
/// Базовая модель сущности системы
/// </summary>
public class Entity
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }
}
