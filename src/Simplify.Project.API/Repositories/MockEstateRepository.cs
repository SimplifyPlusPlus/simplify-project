using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий комплексов
/// </summary>
public class MockEstateRepository : IEstateRepository
{
	private readonly List<Estate> _estates;
	
	/// <summary>
	/// Конструктор класса <see cref="MockEstateRepository"/>
	/// </summary>
	public MockEstateRepository()
	{
		_estates = GenerateData();
	}
	
	/// <inheritdoc cref="IEstateRepository.GetEstates()"/>
	public IEnumerable<Estate> GetEstates()
	{
		return _estates;
	}

	/// <inheritdoc cref="IEstateRepository.GetEstate(Guid)"/>
	public Estate? GetEstate(Guid id)
	{
		var estate = _estates.SingleOrDefault(estate => estate.Id == id);
		return estate;
	}

	private static List<Estate> GenerateData()
	{
		return new List<Estate>
		{
			new()
			{
				Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6"),
				Name = "Центральный",
				Houses = new List<House>
				{
					new House { Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6") },	
				},
			},
		};
	}
}
