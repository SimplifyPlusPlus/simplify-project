using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;
using Simplify.Project.Shared;

namespace Simplify.Project.Model;

/// <summary>
/// Модель события
/// </summary>
public class Event : Entity
{
	/// <summary>
	/// Тип сущности, с которым произошло событие
	/// </summary>
	[Required]
	public virtual EventEntityType EventEntityType { get; set; }
	
	/// <summary>
	/// Тип события
	/// </summary>
	[Required]
	public virtual EventType EventType { get; set; }
	
	/// <summary>
	/// Дата добавления события
	/// </summary>
	[Required]
	public DateTime Created { get; set; }

	/// <summary>
	/// Данные по событию
	/// </summary>
	[Required]
	public IReadOnlyDictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
}
