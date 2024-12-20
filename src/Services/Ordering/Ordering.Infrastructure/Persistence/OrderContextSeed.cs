using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Serilog;

namespace Ordering.Infrastructure.Persistence;

public class OrderContextSeed
{
    private readonly ILogger _logger;
    private readonly OrderContext _context;

    public OrderContextSeed(ILogger logger, OrderContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception e)
        {
            _logger.Error($"An error occurred while seeding the database: {e.Message}");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Error($"An error occurred while seeding data: {e.Message}");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Orders.Any())
        {
            await _context.Orders.AddRangeAsync(
                new Order
                {
                    UserName = "customer1",
                    FirstName = "customer1",
                    LastName = "customer",
                    EmailAddress = "customer1@local.com",
                    ShippingAddress = "Buôn Mê",
                    InvoiceAddress = "Buôn Mê",
                    TotalPrice = 250
                });
        }
    }
}