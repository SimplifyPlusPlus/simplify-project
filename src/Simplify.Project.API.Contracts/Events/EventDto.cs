using System.ComponentModel.DataAnnotations;
using Simplify.Project.Shared;

namespace Simplify.Project.API.Contracts.Events;

public class EventDto
{
	/// <summary>
	///     Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }

	/// <summary>
	///     Тип сущности, с которым произошло событие
	/// </summary>
	[Required]
	public EventEntityType EventEntityType { get; set; }

	/// <summary>
	///     Тип события
	/// </summary>
	[Required]
	public EventType EventType { get; set; }

	/// <summary>
	///     Дата добавления события
	/// </summary>
	[Required]
	public DateTime Created { get; set; }

	/// <summary>
	///     Данные по событию
	/// </summary>
	[Required]
	public IDictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
}
