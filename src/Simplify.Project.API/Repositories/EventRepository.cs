using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий событий
/// </summary>
public class EventRepository : IEventRepository
{
	private readonly SimplifyContext _context;

	/// <summary>
	/// Конструктор класса <see cref="EventRepository"/>
	/// </summary>
	/// <param name="context">Контекст базы данных</param>
	public EventRepository(SimplifyContext context)
	{
		_context = context;
	}
	
	/// <inheritdoc cref="IEventRepository.GetEvents()"/>
	public IQueryable<Event> GetEvents()
	{
		return _context.Events.AsQueryable();
	}
}
