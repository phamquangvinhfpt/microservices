using Customer.API.Services.Interfaces;
using Shared.DTOs.Customer;

namespace Customer.API.Controllers;

public static class CustomersController
{
    public static void MapCustomersAPI(this WebApplication app)
    {
        app.MapGet("/", () => "Welcome to Customer Minimal API!");
        
        app.MapGet("/api/customers/{username}",
            async (ICustomerService customerService, string username) => 
            {
                var customer = await customerService.GetCustomerByUsernameAsync(username);
                return customer != null ? customer : Results.NotFound();
            });

        app.MapGet("/api/customers",
            async (ICustomerService customerService) => 
            {
                var customers = await customerService.GetCustomersAsync();
                return customers != null ? customers : Results.NotFound();
            });

        app.MapPost("/api/customers",
            async (ICustomerService customerService, CreateCustomerDto customer) =>
            {
                var result = await customerService.CreateCustomerAsync(customer);
                return result != null ? Results.Created($"/api/customers/{customer.Username}", result) : Results.NotFound();
            });

        app.MapPut("/api/customers/{id}",
            async (ICustomerService customerService, int id, UpdateCustomerDto customer) =>
            {
                var result = await customerService.UpdateCustomerAsync(id, customer);
                return result != null ? Results.Ok(result) : Results.NotFound();
            });

        app.MapDelete("/api/customers/{id}",
            async (ICustomerService customerService, int id) =>
            {
                var result = await customerService.DeleteCustomerAsync(id);
                return result != null ? Results.Ok(result) : Results.NotFound();
            });
    }
}