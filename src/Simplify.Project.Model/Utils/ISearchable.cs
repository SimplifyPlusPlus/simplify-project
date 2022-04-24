namespace Simplify.Project.Model.Utils;

/// <summary>
/// Интерфейс, который должны реализовать все сущности, для которых существует поиск
/// </summary>
public interface ISearchable
{
	/// <summary>
	/// Значение от 0 до 1, показывающее точность совпадения, 1 - полное, 0 - нет совпадения
	/// </summary>
	double Score(string searchValue);
}
