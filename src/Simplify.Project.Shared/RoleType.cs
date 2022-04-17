namespace Simplify.Project.Shared;

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
	/// Редактор
	/// </summary>
	public const string Editor = "Редактор";
	
	/// <summary>
	/// Менеджер
	/// </summary>
	public const string Manager = "Менеджер";
	
	/// <summary>
	/// Диспетчер
	/// </summary>
	public const string Dispatcher = "Диспетчер";

	/// <summary>
	/// Получить роли в виде списка
	/// </summary>
	public static ICollection<string> ToCollection() => new List<string>()
	{
		Administrator, Editor, Manager, Dispatcher
	};
}
