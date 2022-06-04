using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts.Client;

/// <summary>
///     Базовая информация по клиенту
/// </summary>
public class ClientBaseDto
{
	/// <summary>
	///     Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }

	/// <summary>
	///     Имя
	/// </summary>
	[Required]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	///     Флаг блокировки
	/// </summary>
	[Required]
	public bool IsBlocked { get; set; }
}
