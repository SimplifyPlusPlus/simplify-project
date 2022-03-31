using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

public class MockEstateRepository : IEstateRepository
{
	private readonly List<Estate> _estates;

	public MockEstateRepository()
	{
		_estates = new List<Estate>
		{
			new()
			{
				Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6"),
				Name = "Звезда",
				HousesIds = new List<Guid>
				{
					Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6"),	
				},
			},
		};
	}
	
	public IEnumerable<Estate> GetEstates()
	{
		return _estates;
	}

	public Estate? GetEstate(Guid id)
	{
		var estate = _estates.SingleOrDefault(estate => estate.Id == id);
		return estate;
	}
}