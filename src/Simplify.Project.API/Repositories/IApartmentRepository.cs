using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Интерфейс репозитория квартир
/// </summary>
public interface IApartmentRepository
{
	/// <summary>
	/// Получить все квартиры
	/// </summary>
	/// <returns>Список квартир</returns>
	public IQueryable<Apartment> GetApartments();
       
	/// <summary>
	/// Получить квартиру по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Квартира</returns>
	public Apartment? GetApartment(Guid id);

	/// <summary>
	/// Обновить данные квартиры по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор квартиры</param>
	/// <param name="updatedApartment">Измененные данные квартиры</param>
	public Task UpdateApartment(Guid id, Apartment updatedApartment);
}
