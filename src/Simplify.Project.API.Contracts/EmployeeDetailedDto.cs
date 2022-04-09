using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts;

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
