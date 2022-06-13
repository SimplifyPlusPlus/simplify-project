namespace Simplify.Project.API.Contracts.Search;

/// <summary>
/// Результат поиска клиента
/// </summary>
public class SearchClientResultDto
{ 
	/// <summary>
	/// Идентификатор
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Имя
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Флаг блокировки
	/// </summary>
	public bool IsBlocked { get; set; }

	/// <summary>
	/// Значение от 0 до 1, показывающее точность совпадения, 1 - полное, 0 - нет совпадения
	/// </summary>
	/// <param name="searchValue">Исследуемая строка</param>
	public double Score(string searchValue) => JaroWinklerDistance.Proximity(Name, searchValue);
}
