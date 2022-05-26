using Microsoft.AspNetCore.Mvc;
using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

public class HouseRepository : IHouseRepository
{
	private readonly SimplifyContext _context;

	public HouseRepository(SimplifyContext context)
	{
		_context = context;
	}

	public IQueryable<House> GetHouses()
	{
		return _context.Houses.AsQueryable();
	}

	public House? GetHouse(Guid id)
	{
		return _context.Houses.SingleOrDefault(house => house.Id == id);
	}
}
