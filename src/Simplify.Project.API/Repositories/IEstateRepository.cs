using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Интерфейс репозитория комплексов
/// </summary>
public interface IEstateRepository
{
	/// <summary>
	/// Получить все комплексы
	/// </summary>
	/// <returns>Список комплексов</returns>
	public IQueryable<Estate> GetEstates();
       
	/// <summary>
	/// Получить комплекс по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Комплекс</returns>
	public Estate? GetEstate(Guid id);
}
