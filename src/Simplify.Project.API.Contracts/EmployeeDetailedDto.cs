using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts;

/// <summary>
/// Детальная информация по сотруднику
/// </summary>
public class EmployeeDetailedDto : EmployeeBaseDto
{
	/// <summary>
	/// Логин
	/// </summary>
	[Required]
	public string Login { get; set; } = string.Empty;

	/// <summary>
	/// Заметка
	/// </summary>
	public string? Note { get; set; }
}
