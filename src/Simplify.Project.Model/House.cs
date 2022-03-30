using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Дом
/// </summary>
public class House : Entity
{
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
    /// Идентификаторы подъездов
    /// </summary>
    [Required]
    public ICollection<Guid> EntrancesIds { get; set; } = new List<Guid>();
}
