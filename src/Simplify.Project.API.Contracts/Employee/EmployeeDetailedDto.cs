using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts.Employee;

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
	/// Примечание
	/// </summary>
	public string? Note { get; set; }
}
