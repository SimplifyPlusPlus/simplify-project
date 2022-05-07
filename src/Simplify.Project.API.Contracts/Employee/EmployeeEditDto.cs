using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts.Employee;

/// <summary>
/// Модель данных для изменения данных сотрудника
/// </summary>
public class EmployeeEditDto : EmployeeCreateDto
{
	/// <summary>
	/// Флаг блокировки
	/// </summary>
	[Required]
	public bool IsBlocked { get; set; }
}
