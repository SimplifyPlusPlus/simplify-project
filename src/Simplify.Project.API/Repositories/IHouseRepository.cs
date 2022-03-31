using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Интерфейс репозитория домов
/// </summary>
public interface IHouseRepository
{
	/// <summary>
	/// Получить все дома
	/// </summary>
	/// <returns>Список домов</returns>
	public IEnumerable<House> GetHouses();
       
	/// <summary>
	/// Получить дом по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Дом</returns>
	public House? GetHouse(Guid id);
}