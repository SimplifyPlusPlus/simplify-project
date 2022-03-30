using System.ComponentModel.DataAnnotations;
using Simplify.Project.Model.Handbooks;
using Simplify.Project.Model.Utils;

namespace Simplify.Project.Model;

/// <summary>
/// Сотрудник предприятия
/// </summary>
public class Employee : UserBase
{
    /// <summary>
    /// Роль в системе
    /// </summary>
    [Required]
    public string Role { get; set; } = RoleType.Empty;
    
    /// <summary>
    /// Логин
    /// </summary>
    [Required]
    public string Login { get; set; } = string.Empty;
    
    /// <summary>
    /// Пароль 
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}