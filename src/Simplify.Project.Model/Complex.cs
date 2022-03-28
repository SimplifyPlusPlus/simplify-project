using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.Model;

/// <summary>
/// Жилой комплекс
/// </summary>
public class Complex
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
    /// Идентификаторы домов
    /// </summary>
    [Required]
    public ICollection<Guid> HousesIds { get; set; } = new List<Guid>();
}