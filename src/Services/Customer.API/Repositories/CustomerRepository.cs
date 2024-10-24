using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Customer;

namespace Customer.API.Repositories;

public class CustomerRepository : RepositoryBaseAsync<Entities.Customer, int, CustomerContext>, ICustomerRepository
{
    public CustomerRepository(CustomerContext dbcontext,
        IUnitOfWork<CustomerContext> unitOfWork) : base(dbcontext, unitOfWork)
    {
    }

    public async Task<Entities.Customer> GetCustomerByUsernameAsync(string username) => 
        await FindByCondition(x => x.Username.Equals(username)).SingleOrDefaultAsync();

    public async Task<IEnumerable<Entities.Customer>> GetCustomersAsync() => 
        await FindAll().ToListAsync();

    public Task CreateCustomerAsync(Entities.Customer customer) =>
        CreateAsync(customer);

    public Task UpdateCustomerAsync(Entities.Customer customer) =>
        UpdateAsync(customer);

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await GetByIdAsync(id);
        if (customer != null) DeleteAsync(customer);
    }
}