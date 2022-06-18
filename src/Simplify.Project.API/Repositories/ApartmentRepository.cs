using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий квартир
/// </summary>
public class ApartmentRepository : IApartmentRepository
{
	private readonly SimplifyContext _context;

	/// <summary>
	/// Конструктор класса <see cref="ApartmentRepository"/>
	/// </summary>
	/// <param name="context">Контекст базы данных</param>
	public ApartmentRepository(SimplifyContext context)
	{
		_context = context;
	}

	/// <inheritdoc cref="IApartmentRepository.GetApartments()"/>
	public IQueryable<Apartment> GetApartments()
	{
		return _context.Apartments.AsQueryable();
	}

	/// <inheritdoc cref="IApartmentRepository.GetApartment(Guid)"/>
	public Apartment? GetApartment(Guid id)
	{
		return _context.Apartments.SingleOrDefault(apartment => apartment.Id == id);
	}

	/// <inheritdoc cref="IApartmentRepository.UpdateApartment(Guid,Apartment)"/>
	public async Task UpdateApartment(Guid id, Apartment updatedApartment)
	{
		_context.Apartments.Update(updatedApartment);
		await _context.SaveChangesAsync();
	}
}
