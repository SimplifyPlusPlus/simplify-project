using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

public class EntranceRepository : IEntranceRepository
{
	private readonly SimplifyContext _context;

	public EntranceRepository(SimplifyContext context)
	{
		_context = context;
	}

	public IQueryable<Entrance> GetEntrances()
	{
		return _context.Entrances.AsQueryable();
	}

	public Entrance? GetEntrance(Guid id)
	{
		return _context.Entrances.SingleOrDefault(entrance => entrance.Id == id);
	}
}
