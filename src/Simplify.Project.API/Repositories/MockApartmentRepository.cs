using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий квартир
/// </summary>
public class MockApartmentRepository : IApartmentRepository
{
	private readonly List<Apartment> _apartments;

	public MockApartmentRepository()
	{
		_apartments = new List<Apartment>();
		const string guidTemplate = "{0}f64-5717-4562-b3fc-2c963f66afa6";

		// Генерируем 300 квартир
		for (var i = 1; i <= 300; i++)
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
			
			_apartments.Add(item);
		}
	}
	
	public IEnumerable<Apartment> GetApartments()
	{
		return _apartments;
	}

	public Apartment? GetApartment(Guid id)
	{
		var apartment = _apartments.SingleOrDefault(apartment => apartment.Id == id);
		return apartment;
	}
}