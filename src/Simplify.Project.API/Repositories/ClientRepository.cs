using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий клиентов
/// </summary>
public class ClientRepository : IClientRepository
{
	private readonly SimplifyContext _context;

	/// <summary>
	/// Конструктор класса <see cref="ClientRepository"/>
	/// </summary>
	/// <param name="context">Контекст базы данных</param>
	public ClientRepository(SimplifyContext context)
	{
		_context = context;
	}

	/// <inheritdoc cref="IClientRepository.GetClients()"/>
	public IQueryable<Client> GetClients()
	{
		return _context.Clients.AsQueryable();
	}

	/// <inheritdoc cref="IClientRepository.GetClient(Guid)"/>
	public Client? GetClient(Guid id)
	{
		return _context.Clients.SingleOrDefault(client => client.Id == id);
	}

	/// <inheritdoc cref="IClientRepository.AddClient(Client)"/>
	public async Task AddClient(Client client)
	{
		_context.Clients.Add(client);
		await _context.SaveChangesAsync();
	}

	/// <inheritdoc cref="IClientRepository.UpdateClient(Client)"/>
	public async Task UpdateClient(Client updatedClient)
	{
		_context.Clients.Update(updatedClient);
		await _context.SaveChangesAsync();
	}
}
