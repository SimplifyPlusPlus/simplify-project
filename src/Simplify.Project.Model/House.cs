using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.Model;

/// <summary>
/// Дом
/// </summary>
public class House
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
    /// Идентификаторы подъездов
    /// </summary>
    [Required]
    public ICollection<Guid> EntrancesIds { get; set; } = new List<Guid>();
}