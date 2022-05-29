using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

public class ApartmentRelationRepository : IApartmentRelationRepository
{
	private readonly SimplifyContext _context;

	public ApartmentRelationRepository(SimplifyContext context)
	{
		_context = context;
	}

	public IQueryable<ApartmentRelation> GetRelations()
	{
		return _context.ApartmentRelations.AsQueryable();
	}

	public ApartmentRelation? GetRelation(Guid id)
	{
		return _context.ApartmentRelations.SingleOrDefault(relation => relation.Id == id);
	}

	public IQueryable<ApartmentRelation> GetApartmentRelations(Guid id)
	{
		return _context.ApartmentRelations.Where(relation => relation.Apartment.Id == id).AsQueryable();
	}

	public IQueryable<ApartmentRelation> GetClientApartmentsRelations(Guid id)
	{
		return _context.ApartmentRelations.Where(relation => relation.Client.Id == id).AsQueryable();
	}

	public async Task AddApartmentRelation(ApartmentRelation relation)
	{
		_context.ApartmentRelations.Add(relation);
		await _context.SaveChangesAsync();
	}

	public async Task AddApartmentRelationsRange(IEnumerable<ApartmentRelation> relations)
	{
		_context.ApartmentRelations.AddRange(relations);
		await _context.SaveChangesAsync();
	}

	public async Task RemoveApartmentRelations(Guid id)
	{
		var relations = GetApartmentRelations(id);
		_context.ApartmentRelations.RemoveRange(relations);
		await _context.SaveChangesAsync();
	}
}
