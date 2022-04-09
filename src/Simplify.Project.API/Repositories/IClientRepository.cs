using Simplify.Project.Model;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Интерфейс репозитория клиентов
/// </summary>
public interface IClientRepository
{
	/// <summary>
	/// Получить клиентов
	/// </summary>
	/// <returns>Список клиентов</returns>
	public IEnumerable<Client> GetClients(); // заменить тут и в всех остальных на IQueryable?
       
	/// <summary>
	/// Получить клиента по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Клиент</returns>
	public Client? GetClient(Guid id);
}
