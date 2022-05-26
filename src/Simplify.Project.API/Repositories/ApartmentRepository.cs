using Microsoft.EntityFrameworkCore;
using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

public class ApartmentRepository : IApartmentRepository
{
	private readonly SimplifyContext _context;

	public ApartmentRepository(SimplifyContext context)
	{
		_context = context;
	}

	public IQueryable<Apartment> GetApartments()
	{
		return _context.Apartments.AsQueryable();
	}

	public Apartment? GetApartment(Guid id)
	{
		return _context.Apartments.SingleOrDefault(apartment => apartment.Id == id);
	}

	public async Task UpdateApartment(Guid id, Apartment updatedApartment)
	{
		_context.Apartments.Update(updatedApartment);
		await _context.SaveChangesAsync();
	}
}
