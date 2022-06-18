using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий жилых комплексов
/// </summary>
public class EstateRepository : IEstateRepository
{
	private readonly SimplifyContext _context;

	/// <summary>
	/// Конструктор класса <see cref="EstateRepository"/>
	/// </summary>
	/// <param name="context">Контекст базы данных</param>
	public EstateRepository(SimplifyContext context)
	{
		_context = context;
	}
	
	/// <inheritdoc cref="IEstateRepository.GetEstates()"/>
	public IQueryable<Estate> GetEstates()
	{
		return _context.Estates.AsQueryable();
	}

	/// <inheritdoc cref="IEstateRepository.GetEstate(Guid)"/>
	public Estate? GetEstate(Guid id)
	{
		return _context.Estates.SingleOrDefault(estate => estate.Id == id);
	}
}
