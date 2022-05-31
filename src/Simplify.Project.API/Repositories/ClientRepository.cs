using Microsoft.EntityFrameworkCore;
using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

public class ClientRepository : IClientRepository
{
	private readonly SimplifyContext _context;

	public ClientRepository(SimplifyContext context)
	{
		_context = context;
	}

	public IQueryable<Client> GetClients()
	{
		return _context.Clients.AsQueryable();
	}

	public Client? GetClient(Guid id)
	{
		return _context.Clients.AsNoTracking().SingleOrDefault(client => client.Id == id);
	}

	public async Task AddClient(Client client)
	{
		_context.Clients.Add(client);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateClient(Client updatedClient)
	{
		_context.Clients.Update(updatedClient);
		await _context.SaveChangesAsync();
	}
}
