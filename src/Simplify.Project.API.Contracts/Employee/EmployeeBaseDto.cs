using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts.Employee;

/// <summary>
///     Базовая информация по сотруднику
/// </summary>
public class EmployeeBaseDto
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
	///     Роль в системе
	/// </summary>
	[Required]
	public string Role { get; set; } = string.Empty;

	/// <summary>
	///     Признак блокировки пользователя
	/// </summary>
	[Required]
	public bool IsBlocked { get; set; }
}
