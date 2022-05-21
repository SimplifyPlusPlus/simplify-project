using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts.Client;

/// <summary>
/// Модель данных для редактирования клиента
/// </summary>
public class ClientEditDto : ClientCreateDto
{
	/// <summary>
	/// Флаг блокировки
	/// </summary>
	[Required]
	public bool IsBlocked { get; set; }
}
