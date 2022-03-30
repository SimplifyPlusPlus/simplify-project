using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Подъезд
/// </summary>
public class Entrance : Entity
{
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