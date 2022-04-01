using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий домов
/// </summary>
public class MockHouseRepository : IHouseRepository
{
	private readonly List<House> _houses;

	/// <summary>
	/// Конструктор класса <see cref="MockHouseRepository"/>
	/// </summary>
	public MockHouseRepository()
	{
		_houses = GenerateData();
	}
	
	/// <inheritdoc cref="IHouseRepository.GetHouses()"/>
	public IEnumerable<House> GetHouses()
	{
		return _houses;
	}

	/// <inheritdoc cref="IHouseRepository.GetHouse(Guid)"/>
	public House? GetHouse(Guid id)
	{
		var house = _houses.SingleOrDefault(house => house.Id == id);
		return house;
	}

	private static List<House> GenerateData()
	{
		return new List<House>
		{
			new()
			{
				Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6"),
				Number = 1,
				Street = "Богдана Хмельницкого",
				EntrancesIds = new List<Guid>
				{
					Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6"),
					Guid.Parse("00002f64-5717-4562-b3fc-2c963f66afa6"),
					Guid.Parse("00003f64-5717-4562-b3fc-2c963f66afa6"),
				},
			},
		};
	}
}