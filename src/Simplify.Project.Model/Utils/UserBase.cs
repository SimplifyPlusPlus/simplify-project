using System.ComponentModel.DataAnnotations;

namespace Simplify.Project.Model.Utils;

/// <summary>
/// Базовый класс пользователя системы
/// </summary>
public abstract class UserBase : Entity
{
    /// <summary>
    /// Фамилия
    /// </summary>
    [Required]
    public string Lastname { get; set; } = string.Empty;
    
    /// <summary>
    /// Имя
    /// </summary>
    [Required]
    public string Firstname { get; set; } = string.Empty;

    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }
    
    /// <summary>
    /// Дата добавления клиента
    /// </summary>
    [Required]
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Признак блокировки пользователя
    /// </summary>
    [Required]
    public bool IsBlocked { get; set; }
    
    /// <summary>
    /// Примечание
    /// </summary>
    public string? Note { get; set; }
}