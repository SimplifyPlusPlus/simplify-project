using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий клиентов
/// </summary>
public class MockClientRepository : IClientRepository
{
	private readonly List<Client> _clients;
    
	/// <summary>
	/// Контроллер класса <see cref="MockClientRepository"/>
	/// </summary>
	public MockClientRepository()
	{
		_clients = GenerateData();
	}
    
	/// <inheritdoc cref="IClientRepository.GetClients()"/>
	public IEnumerable<Client> GetClients()
	{
		return _clients;
	}
    
	/// <inheritdoc cref="IClientRepository.GetClient(Guid)"/>
	public Client? GetClient(Guid id)
	{
		var client = _clients.SingleOrDefault(client => client.Id == id);
		return client;
	}

	private static List<Client> GenerateData()
	{
		return new List<Client>
		{
			new()
			{
				Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6"), 
				Lastname = "Маркелов", 
				Firstname = "Павел", 
				Patronymic = "Николаевич", 
				Email = "pmarkelo77@gmail.com", 
				Phone = "",
				ApartmentsRelations = new List<ApartmentRelation>
				{
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
				},
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Backend и API разработчик"
			},
			new()
			{
				Id = Guid.Parse("00002f64-5717-4562-b3fc-2c963f66afa6"),
				Lastname = "Коколов", 
				Firstname = "Андрей", 
				Email = "", 
				Phone = "", 
				ApartmentsRelations = new List<ApartmentRelation>
				{
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
				},
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Frontend и DB разработчик"
			},
			new()
			{
				Id = Guid.Parse("00003f64-5717-4562-b3fc-2c963f66afa6"),
				Lastname = "Селимов", 
				Firstname = "Загидин", 
				Patronymic = "Мурадович", 
				Email = "", 
				Phone = "",
				ApartmentsRelations = new List<ApartmentRelation>
				{
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
				},
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Тех. Архитектор и Тимлид команды"
			},
			new()
			{
				Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6"),
				Lastname = "Ефимов", 
				Firstname = "Алексей", 
				Email = "", 
				Phone = "",
				ApartmentsRelations = new List<ApartmentRelation>
				{
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
				},
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Frontend и Design разработчик"
			}
		};
	}
}
