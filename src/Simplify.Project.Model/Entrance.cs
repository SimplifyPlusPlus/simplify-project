using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.Model;

/// <summary>
/// Подъезд
/// </summary>
public class Entrance
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
    /// Идентификаторы квартир
    /// </summary>
    [Required]
    public IEnumerable<Guid> ApartmentsIds { get; set; } = new List<Guid>();
}