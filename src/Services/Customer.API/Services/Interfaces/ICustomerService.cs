using Shared.DTOs.Customer;

namespace Customer.API.Services.Interfaces;

public interface ICustomerService
{
    Task<IResult> GetCustomerByUsernameAsync(string username);
    Task<IResult> GetCustomersAsync();
    Task<IResult> CreateCustomerAsync(CreateCustomerDto customer);
    Task<IResult> UpdateCustomerAsync(int id, UpdateCustomerDto customer);
    Task<IResult> DeleteCustomerAsync(int id);
}