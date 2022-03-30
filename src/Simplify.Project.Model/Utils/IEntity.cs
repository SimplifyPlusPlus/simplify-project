using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.Model.Utils;

/// <summary>
/// Интерфейс сущности системы
/// </summary>
public interface IEntity
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }
}