using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence;

public static class CustomerContextSeed
{
    public static IHost SeedCustomerData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var customerContext = scope.ServiceProvider
            .GetRequiredService<CustomerContext>();
        customerContext.Database.MigrateAsync().GetAwaiter().GetResult();

        CreateCustomer(customerContext, "customer1", "customer1", "customer", "customer1@local.com").GetAwaiter().GetResult();
        CreateCustomer(customerContext, "customer2", "customer2", "customer", "customer2@local.com").GetAwaiter().GetResult();

        return host;
    }

    private static async Task CreateCustomer(CustomerContext customerContext, string username, string firstName, string lastName, string email)
    {
        var customer = await customerContext.Customers
            .SingleOrDefaultAsync(x => x.Username.Equals(username) ||
            x.EmailAddress.Equals(email));
        if (customer == null)
        {
            customer = new Entities.Customer
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = email
            };
            await customerContext.Customers.AddAsync(customer);
            await customerContext.SaveChangesAsync();
        }
    }
}