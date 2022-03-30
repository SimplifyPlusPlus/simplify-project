using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Подъезд
/// </summary>
public class Entrance : IEntity
{
    /// <inheritdoc cref="IEntity.Id"/>
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
    public ICollection<Guid> ApartmentsIds { get; set; } = new List<Guid>();
}