using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces;

public interface ICustomerRepository : IRepositoryBaseAsync<Entities.Customer, int, CustomerContext>
{
    Task<IEnumerable<Entities.Customer>> GetCustomersAsync();
    Task<Entities.Customer> GetCustomerByUsernameAsync(string username);
    Task CreateCustomerAsync(Entities.Customer customer);
    Task UpdateCustomerAsync(Entities.Customer customer);
    Task DeleteCustomerAsync(int id);
}