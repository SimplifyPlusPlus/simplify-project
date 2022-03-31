using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

public class MockEntranceRepository : IEntranceRepository
{
	private readonly List<Entrance> _entrances;

	public MockEntranceRepository()
	{
		_entrances = new List<Entrance>();
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
			for (var j = leftBorder; j < rightBorder; ++j)
			{
				var apartmentId = j switch
				{
					>= 100 => string.Format(guidTemplate, $"00{j}"),
					>= 10 => string.Format(guidTemplate, $"000{j}"),
					_ => string.Format(guidTemplate, $"0000{j}")
				};
				
				item.ApartmentsIds.Add(Guid.Parse(apartmentId));
			}
		}
	}
	
	public IEnumerable<Entrance> GetEntrances()
	{
		return _entrances;
	}

	public Entrance? GetEntrance(Guid id)
	{
		var entrance = _entrances.SingleOrDefault(entrance => entrance.Id == id);
		return entrance;
	}
}