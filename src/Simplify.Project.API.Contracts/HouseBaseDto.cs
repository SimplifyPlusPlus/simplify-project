using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts;

/// <summary>
/// Базовая информация по дому
/// </summary>
public class HouseBaseDto
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }
	
	/// <summary>
	/// Номер
	/// </summary>
	[Required]
	public int Number { get; set; }

	/// <summary>
	/// Улица
	/// </summary>
	[Required]
	public string Street { get; set; } = string.Empty;
    
	/// <summary>
	/// Корпус
	/// </summary>
	public string? Building { get; set; }

	/// <summary>
	/// Коллекция подъездов дома
	/// </summary>
	[Required]
	public ICollection<EntranceBaseDto> Entrances { get; set; } = new List<EntranceBaseDto>();
}
