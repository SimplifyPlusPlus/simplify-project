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
	public IQueryable<ApartmentRelation> GetRelations();
       
	/// <summary>
	/// Получить отношение по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Отношение</returns>
	public ApartmentRelation? GetRelation(Guid id);

	/// <summary>
	/// Получить все отношения с квартирой
	/// </summary>
	/// <param name="id">Идентификатор квартиры</param>
	/// <returns>Список отношений с квартирой</returns>
	public IQueryable<ApartmentRelation> GetApartmentRelations(Guid id);
	
	/// <summary>
	/// Получить все отношения с квартирами у клиента
	/// </summary>
	/// <param name="id">Идентификатор клиента</param>
	/// <returns>Список отношений клиента</returns>
	public IQueryable<ApartmentRelation> GetClientApartmentsRelations(Guid id);
}
