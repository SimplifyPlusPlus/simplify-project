using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий домов
/// </summary>
public class HouseRepository : IHouseRepository
{
	private readonly SimplifyContext _context;

	/// <summary>
	/// Конструктор класса <see cref="HouseRepository"/>
	/// </summary>
	/// <param name="context">Контекст базы данных</param>
	public HouseRepository(SimplifyContext context)
	{
		_context = context;
	}

	/// <inheritdoc cref="IHouseRepository.GetHouses()"/>
	public IQueryable<House> GetHouses()
	{
		return _context.Houses.AsQueryable();
	}

	/// <inheritdoc cref="IHouseRepository.GetHouse(Guid)"/>
	public House? GetHouse(Guid id)
	{
		return _context.Houses.SingleOrDefault(house => house.Id == id);
	}
}
