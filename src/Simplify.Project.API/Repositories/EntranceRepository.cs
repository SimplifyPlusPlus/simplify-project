using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий подъездов
/// </summary>
public class EntranceRepository : IEntranceRepository
{
	private readonly SimplifyContext _context;

	/// <summary>
	/// Конструктор класса <see cref="EntranceRepository"/>
	/// </summary>
	/// <param name="context">Контекст базы данных</param>
	public EntranceRepository(SimplifyContext context)
	{
		_context = context;
	}

	/// <inheritdoc cref="IEntranceRepository.GetEntrances()"/>
	public IQueryable<Entrance> GetEntrances()
	{
		return _context.Entrances.AsQueryable();
	}

	/// <inheritdoc cref="IEntranceRepository.GetEntrance(Guid)"/>
	public Entrance? GetEntrance(Guid id)
	{
		return _context.Entrances.SingleOrDefault(entrance => entrance.Id == id);
	}
}
