using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий квартир
/// </summary>
public class MockApartmentRepository : IApartmentRepository
{
	private readonly List<Apartment> _apartments;

	/// <summary>
	/// Конструктор класса <see cref="MockApartmentRepository"/>
	/// </summary>
	public MockApartmentRepository()
	{
		_apartments = GenerateData();
	}
	
	/// <inheritdoc cref="IApartmentRepository.GetApartments()"/>
	public IEnumerable<Apartment> GetApartments()
	{
		return _apartments;
	}

	/// <inheritdoc cref="IApartmentRepository.GetApartment(Guid)"/>
	public Apartment? GetApartment(Guid id)
	{
		var apartment = _apartments.SingleOrDefault(apartment => apartment.Id == id);
		return apartment;
	}

	private static List<Apartment> GenerateData()
	{
		var apartments = new List<Apartment>();
		const string guidTemplate = "{0}f64-5717-4562-b3fc-2c963f66afa6";

		// Генерируем 400 квартир
		for (var i = 1; i <= 400; i++)
		{
			var id = i switch
			{
				>= 100 => string.Format(guidTemplate, $"00{i}"),
				>= 10 => string.Format(guidTemplate, $"000{i}"),
				_ => string.Format(guidTemplate, $"0000{i}")
			};

			var item = new Apartment
			{
				Id = Guid.Parse(id),
				Number = i,
			};
			
			apartments.Add(item);
		}

		return apartments;
	}
}
