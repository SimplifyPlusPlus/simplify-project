using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts;

/// <summary>
/// Базовая информация по жилому комплексу
/// </summary>
public class EstateBaseDto
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }
	
	/// <summary>
	/// Наименование
	/// </summary>
	[Required]
	public string Name { get; set; } = string.Empty;
    
	/// <summary>
	/// Примечание
	/// </summary>
	public string? Note { get; set; }
    
	/// <summary>
	/// Коллекция домов комплекса
	/// </summary>
	[Required]
	public ICollection<HouseBaseDto> Houses { get; set; } = new List<HouseBaseDto>();
}
