using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Жилой комплекс
/// </summary>
public class Estate : Entity
{
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
