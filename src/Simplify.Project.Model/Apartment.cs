using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.Model;

/// <summary>
/// Квартира
/// </summary>
public class Apartment
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
}