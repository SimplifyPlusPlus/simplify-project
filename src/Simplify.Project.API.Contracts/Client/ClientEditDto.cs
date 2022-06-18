using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts.Client;

/// <summary>
/// Модель данных для редактирования клиента
/// </summary>
public class ClientEditDto
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }
	
	/// <summary>
	/// Фамилия
	/// </summary>
	[Required(ErrorMessage = "Необходимо указать фамилию клиента")]
	public string Lastname { get; set; } = string.Empty;
    
	/// <summary>
	/// Имя
	/// </summary>
	[Required(ErrorMessage = "Необходимо указать имя клиента")]
	public string Firstname { get; set; } = string.Empty;

	/// <summary>
	/// Отчество
	/// </summary>
	public string? Patronymic { get; set; }
	
	/// <summary>
	/// Электронная почта
	/// </summary>
	[Required(ErrorMessage = "Необходимо указать адрес электронной почты")]
	public string Email { get; set; } = string.Empty;

	/// <summary>
	/// Номер телефона
	/// </summary>
	[Required(ErrorMessage = "Необходимо указать номер телефона")]
	public string Phone { get; set; } = string.Empty;

	/// <summary>
	/// Заметка
	/// </summary>
	public string? Note { get; set; }
	
	/// <summary>
	/// Флаг блокировки
	/// </summary>
	[Required]
	public bool IsBlocked { get; set; }
}
