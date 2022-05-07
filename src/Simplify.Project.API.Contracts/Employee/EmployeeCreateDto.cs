using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts.Employee;

/// <summary>
/// Модель данных для создания сотрудника
/// </summary>
public class EmployeeCreateDto
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }

	/// <summary>
	/// Фамилия
	/// </summary>
	[Required]
	public string Lastname { get; set; } = string.Empty;
    
	/// <summary>
	/// Имя
	/// </summary>
	[Required]
	public string Firstname { get; set; } = string.Empty;

	/// <summary>
	/// Отчество
	/// </summary>
	public string? Patronymic { get; set; }

	/// <summary>
	/// Роль в системе
	/// </summary>
	[Required]
	public string Role { get; set; } = string.Empty;
	
	/// <summary>
	/// Логин
	/// </summary>
	[Required]
	public string Login { get; set; } = string.Empty;

	/// <summary>
	/// Пароль 
	/// </summary>
	[Required]
	public string Password { get; set; } = string.Empty;
	
	/// <summary>
	/// Заметка
	/// </summary>
	public string? Note { get; set; }
}
