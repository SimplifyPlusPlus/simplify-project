using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts;

/// <summary>
/// Базовая информация по клиенту
/// </summary>
public class ClientBaseDto
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
	/// Электронная почта
	/// </summary>
	[Required]
	public string Email { get; set; } = string.Empty;
    
	/// <summary>
	/// Номер телефона
	/// </summary>
	[Required]
	public string Phone { get; set; } = string.Empty;
	
	/// <summary>
	/// Признак блокировки пользователя
	/// </summary>
	[Required]
	public bool IsBlocked { get; set; }
}