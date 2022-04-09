using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts;
public class ClientDetailedDto : ClientBaseDto
{
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
	/// Заметка
	/// </summary>
	public string? Note { get; set; }
}
