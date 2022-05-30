using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Интерфейс репозитория событий
/// </summary>
public interface IEventRepository
{
	/// <summary>
	/// Получить все события
	/// </summary>
	/// <returns>Список событий</returns>
	public IQueryable<Event> GetEvents();
}
