using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

public class EventRepository : IEventRepository
{
	private readonly SimplifyContext _context;

	public EventRepository(SimplifyContext context)
	{
		_context = context;
	}
	
	
	public IQueryable<Event> GetEvents()
	{
		return _context.Events.AsQueryable();
	}
}
