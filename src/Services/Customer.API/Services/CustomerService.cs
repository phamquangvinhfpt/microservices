using AutoMapper;
using Customer.API.Repositories.Interfaces;
using Shared.DTOs.Customer;

namespace Customer.API.Services.Interfaces;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    
    public CustomerService(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IResult> GetCustomerByUsernameAsync(string username) => 
        Results.Ok(await _repository.GetCustomerByUsernameAsync(username));

    public async Task<IResult> GetCustomersAsync() =>
        Results.Ok(await _repository.GetCustomersAsync());

    public async Task<IResult> CreateCustomerAsync(CreateCustomerDto customer)
    {
        var entity = _mapper.Map<Entities.Customer>(customer);
        await _repository.CreateCustomerAsync(entity);
        await _repository.SaveChangesAsync();
        return Results.Ok(entity);
    }

    public async Task<IResult> UpdateCustomerAsync(int id, UpdateCustomerDto customer)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return Results.NotFound();
        _mapper.Map(customer, entity);
        await _repository.UpdateCustomerAsync(entity);
        await _repository.SaveChangesAsync();
        return Results.Ok(entity);
    }

    public async Task<IResult> DeleteCustomerAsync(int id)
    {
        await _repository.DeleteCustomerAsync(id);
        await _repository.SaveChangesAsync();
        return Results.Ok();
    }
}