using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Интерфейс репозитория подъездов
/// </summary>
public interface IEntranceRepository
{
	/// <summary>
	/// Получить все подъезды
	/// </summary>
	/// <returns>Список подъездов</returns>
	public IEnumerable<Entrance> GetEntrances();
       
	/// <summary>
	/// Получить подъезд по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Подъезд</returns>
	public Entrance? GetEntrance(Guid id);
}