namespace Simplify.Project.API.Contracts;

/// <summary>
/// Результат поиска
/// </summary>
public class SearchResultDto
{
	/// <summary>
	/// GUID результата
	/// </summary>
	public Guid Id;

	/// <summary>
	/// Название результата
	/// </summary>
	public string Name = string.Empty;

	/// <summary>
	/// Тип результата
	/// </summary>
	public string Type = string.Empty;
}
