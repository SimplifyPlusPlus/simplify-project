using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий отношений с квартирами
/// </summary>
public class ApartmentRelationRepository : IApartmentRelationRepository
{
	private readonly SimplifyContext _context;

	/// <summary>
	/// Конструктор класса <see cref="ApartmentRelationRepository"/>
	/// </summary>
	/// <param name="context">Контекст базы данных</param>
	public ApartmentRelationRepository(SimplifyContext context)
	{
		_context = context;
	}

	/// <inheritdoc cref="IApartmentRelationRepository.GetRelations()"/>
	public IQueryable<ApartmentRelation> GetRelations()
	{
		return _context.ApartmentRelations.AsQueryable();
	}

	/// <inheritdoc cref="IApartmentRelationRepository.GetRelation(Guid)"/>
	public ApartmentRelation? GetRelation(Guid id)
	{
		return _context.ApartmentRelations.SingleOrDefault(relation => relation.Id == id);
	}

	/// <inheritdoc cref="IApartmentRelationRepository.GetApartmentRelations(Guid)"/>
	public IQueryable<ApartmentRelation> GetApartmentRelations(Guid id)
	{
		return _context.ApartmentRelations.Where(relation => relation.Apartment.Id == id).AsQueryable();
	}

	/// <inheritdoc cref="IApartmentRelationRepository.GetClientApartmentsRelations(Guid)"/>
	public IQueryable<ApartmentRelation> GetClientApartmentsRelations(Guid id)
	{
		return _context.ApartmentRelations.Where(relation => relation.Client.Id == id).AsQueryable();
	}

	/// <inheritdoc cref="IApartmentRelationRepository.AddApartmentRelation(ApartmentRelation)"/>
	public async Task AddApartmentRelation(ApartmentRelation relation)
	{
		_context.ApartmentRelations.Add(relation);
		await _context.SaveChangesAsync();
	}

	/// <inheritdoc cref="IApartmentRelationRepository.AddApartmentRelationsRange(IEnumerable{ApartmentRelation})"/>
	public async Task AddApartmentRelationsRange(IEnumerable<ApartmentRelation> relations)
	{
		_context.ApartmentRelations.AddRange(relations);
		await _context.SaveChangesAsync();
	}
	
	/// <inheritdoc cref="IApartmentRelationRepository.RemoveApartmentRelation(ApartmentRelation)"/>
	public async Task RemoveApartmentRelation(ApartmentRelation relation)
	{
		_context.ApartmentRelations.Remove(relation);
		await _context.SaveChangesAsync();
	}

	/// <inheritdoc cref="IApartmentRelationRepository.RemoveApartmentRelations(Guid)"/>
	public async Task RemoveApartmentRelations(Guid id)
	{
		var relations = GetApartmentRelations(id);
		_context.ApartmentRelations.RemoveRange(relations);
		await _context.SaveChangesAsync();
	}
}
