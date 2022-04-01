using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Интерфейс репозитория отношений клиент-квартира
/// </summary>
public interface IApartmentRelationRepository
{
	/// <summary>
	/// Получить все отношения
	/// </summary>
	/// <returns>Список отношений</returns>
	public IEnumerable<ApartmentRelation> GetRelations();
       
	/// <summary>
	/// Получить отношения по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Отношение</returns>
	public ApartmentRelation? GetRelation(Guid id);

	/// <summary>
	/// Получить все связи с квартирой
	/// </summary>
	/// <param name="id">Идентификатор квартиры</param>
	/// <returns>Список связей с квартирой</returns>
	public IEnumerable<ApartmentRelation> GetApartmentRelations(Guid id);
}