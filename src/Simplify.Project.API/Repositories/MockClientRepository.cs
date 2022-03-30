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
        _clients = new List<Client>
        {
            new()
            {
                Id = Guid.NewGuid(), 
                Lastname = "Маркелов", 
                Firstname = "Павел", 
                Patronymic = "Николаевич", 
                Email = "pmarkelo77@gmail.com", 
                Phone = "", 
                ApartmentsRelationsIds = new List<Guid>
                {
                    Guid.NewGuid(),
                },
                Created = DateTime.Now, 
                IsBlocked = false, 
                Note = "Backend и API разработчик"
            },
            new()
            {
                Id = Guid.NewGuid(), 
                Lastname = "Коколов", 
                Firstname = "Андрей", 
                Email = "", 
                Phone = "", 
                ApartmentsRelationsIds = new List<Guid>
                {
                    Guid.NewGuid(),
                },
                Created = DateTime.Now, 
                IsBlocked = false, 
                Note = "Frontend и DB разработчик"
            },
            new()
            {
                Id = Guid.NewGuid(), 
                Lastname = "Селимов", 
                Firstname = "Загидин", 
                Patronymic = "Мурадович", 
                Email = "", 
                Phone = "", 
                ApartmentsRelationsIds = new List<Guid>
                {
                    Guid.NewGuid(),
                },
                Created = DateTime.Now, 
                IsBlocked = false, 
                Note = "Тех. Архитектор и Тимлид команды"
            },
            new()
            {
                Id = Guid.NewGuid(), 
                Lastname = "Ефимов", 
                Firstname = "Алексей", 
                Email = "", 
                Phone = "", 
                ApartmentsRelationsIds = new List<Guid>
                {
                    Guid.NewGuid(),
                },
                Created = DateTime.Now, 
                IsBlocked = false, 
                Note = "Frontend и Design разработчик"
            }
        };
    }
    
    /// <inheritdoc cref="IClientRepository.GetClient(Guid)"/>
    public Client? GetClient(Guid id)
    {
        var client = _clients.SingleOrDefault(x => x.Id == id);
        return client;
    }

    /// <inheritdoc cref="IClientRepository.GetClients()"/>
    public IEnumerable<Client> GetClients()
    {
        return _clients;
    }
}
