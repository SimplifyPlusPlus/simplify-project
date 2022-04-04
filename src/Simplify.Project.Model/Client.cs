using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Клиент
/// </summary>
public class Client : UserBase
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
	/// Идентификаторы связей квартир клиента
	/// </summary>
	[Required]
	public virtual ICollection<ApartmentRelation> ApartmentsRelations { get; set; } = new List<ApartmentRelation>();
}
