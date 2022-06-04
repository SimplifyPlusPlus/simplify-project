using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.API.Contracts;

/// <summary>
///     Базовая информация по дому
/// </summary>
public class HouseBaseDto
{
	/// <summary>
	///     Идентификатор
	/// </summary>
	[Required]
	public Guid Id { get; set; }

	/// <summary>
	///     Местонахождение (улица, номер, корпус)
	/// </summary>
	[Required]
	public string Location { get; set; } = string.Empty;

	/// <summary>
	///     Коллекция подъездов дома
	/// </summary>
	[Required]
	public ICollection<EntranceBaseDto> Entrances { get; set; } = new List<EntranceBaseDto>();
}
