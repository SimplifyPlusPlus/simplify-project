using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Интерфейс репозитория квартир
/// </summary>
public interface IApartmentRepository
{
	/// <summary>
	/// Получить квартиры
	/// </summary>
	/// <returns>Список квартир</returns>
	public IEnumerable<Apartment> GetApartments();
       
	/// <summary>
	/// Получить квартиру по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Квартира</returns>
	public Apartment? GetApartment(Guid id);
}