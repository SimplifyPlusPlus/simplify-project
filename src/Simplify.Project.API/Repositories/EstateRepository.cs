using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

public class EstateRepository : IEstateRepository
{
	private readonly SimplifyContext _context;

	public EstateRepository(SimplifyContext context)
	{
		_context = context;
	}
	
	public IQueryable<Estate> GetEstates()
	{
		return _context.Estates.AsQueryable();
	}

	public Estate? GetEstate(Guid id)
	{
		return _context.Estates.SingleOrDefault(estate => estate.Id == id);
	}
}
