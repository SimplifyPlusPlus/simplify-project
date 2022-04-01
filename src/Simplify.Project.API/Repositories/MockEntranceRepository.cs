using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий подъездов
/// </summary>
public class MockEntranceRepository : IEntranceRepository
{
	private readonly List<Entrance> _entrances;

	/// <summary>
	/// Конструктор класса <see cref="MockEntranceRepository"/>
	/// </summary>
	public MockEntranceRepository()
	{
		_entrances = GenerateData();
	}

	/// <inheritdoc cref="IEntranceRepository.GetEntrances()"/>
	public IEnumerable<Entrance> GetEntrances()
	{
		return _entrances;
	}

	/// <inheritdoc cref="IEntranceRepository.GetEntrance(Guid)"/>
	public Entrance? GetEntrance(Guid id)
	{
		var entrance = _entrances.SingleOrDefault(entrance => entrance.Id == id);
		return entrance;
	}

	private static List<Entrance> GenerateData()
	{
		var entrances = new List<Entrance>();
		const string guidTemplate = "{0}f64-5717-4562-b3fc-2c963f66afa6";

		// Генерируем подъезд и добавляем в него квартиры
		for (var i = 1; i <= 3; i++)
		{
			var id = string.Format(guidTemplate, $"0000{i}");
			var item = new Entrance
			{
				Id = Guid.Parse(id),
				Number = i,
			};

			var rightBorder = i * 100;
			var leftBorder = rightBorder - 100;
			for (var j = leftBorder + 1; j < rightBorder + 1; ++j)
			{
				var apartmentId = j switch
				{
					>= 100 => string.Format(guidTemplate, $"00{j}"),
					>= 10 => string.Format(guidTemplate, $"000{j}"),
					_ => string.Format(guidTemplate, $"0000{j}")
				};

				item.ApartmentsIds.Add(Guid.Parse(apartmentId));
			}

			entrances.Add(item);
		}

		return entrances;
	}
}