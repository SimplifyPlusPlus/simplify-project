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
	public IQueryable<Client> GetClients()
	{
		return _clients.AsQueryable();
	}
    
	/// <inheritdoc cref="IClientRepository.GetClient(Guid)"/>
	public Client? GetClient(Guid id)
	{
		var client = _clients.SingleOrDefault(client => client.Id == id);
		return client;
	}

	/// <inheritdoc cref="IClientRepository.UpdateClient(Guid, Client)"/>
	public void UpdateClient(Guid id, Client updatedClient)
	{
		var client = _clients.Single(client => client.Id == id);
		_clients.Remove(client);
		_clients.Add(updatedClient);
	}

	private static List<Client> GenerateData()
	{
		return new List<Client>
		{
			new()
			{
				Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6"),
				Lastname = "Селимов", 
				Firstname = "Загидин", 
				Patronymic = "Мурадович", 
				Email = "", 
				Phone = "",
				ApartmentRelations = new List<ApartmentRelation>
				{
					new ApartmentRelation {Id = Guid.Parse("00007f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00008f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00009f64-5717-4562-b3fc-2c963f66afa6") },
				},
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Тех. Архитектор и Тимлид команды"
			},
			new()
			{
				Id = Guid.Parse("00002f64-5717-4562-b3fc-2c963f66afa6"), 
				Lastname = "Маркелов", 
				Firstname = "Павел", 
				Patronymic = "Николаевич", 
				Email = "pmarkelo77@gmail.com", 
				Phone = "",
				ApartmentRelations = new List<ApartmentRelation>
				{
					new ApartmentRelation {Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00002f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00003f64-5717-4562-b3fc-2c963f66afa6") },
				},
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Backend и API разработчик"
			},
			new()
			{
				Id = Guid.Parse("00003f64-5717-4562-b3fc-2c963f66afa6"),
				Lastname = "Коколов", 
				Firstname = "Андрей", 
				Email = "", 
				Phone = "", 
				ApartmentRelations = new List<ApartmentRelation>
				{
					new ApartmentRelation {Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00005f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00006f64-5717-4562-b3fc-2c963f66afa6") },
				},
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Frontend и DB разработчик"
			},
			new()
			{
				Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6"),
				Lastname = "Ефимов", 
				Firstname = "Алексей", 
				Email = "", 
				Phone = "",
				ApartmentRelations = new List<ApartmentRelation>
				{
					new ApartmentRelation {Id = Guid.Parse("00010f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00011f64-5717-4562-b3fc-2c963f66afa6") },
					new ApartmentRelation {Id = Guid.Parse("00012f64-5717-4562-b3fc-2c963f66afa6") },
				},
				Created = DateTime.Now, 
				IsBlocked = false, 
				Note = "Frontend и Design разработчик"
			}
		};
	}
}
