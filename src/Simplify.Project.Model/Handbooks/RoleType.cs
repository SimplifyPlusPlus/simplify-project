namespace Simplify.Project.Model.Handbooks;

/// <summary>
/// Тип роли сотрудника в системе
/// </summary>
public static class RoleType
{
    /// <summary>
    /// Не назначено
    /// </summary>
    public const string Empty = "Не назначено";

    /// <summary>
    /// Администратор
    /// </summary>
    public const string Administrator = "Администратор";

    /// <summary>
    /// Получить роли в виде списка
    /// </summary>
    public static ICollection<string> ToCollection() => new List<string>()
    {
        Empty, Administrator
    };
}