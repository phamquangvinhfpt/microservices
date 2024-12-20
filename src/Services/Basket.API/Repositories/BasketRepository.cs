using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCacheService;
    private readonly ISerializeService _serializeService;
    private readonly ILogger _logger;

    public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService, ILogger logger)
    {
        _redisCacheService = redisCacheService;
        _serializeService = serializeService;
        _logger = logger;
    }

    public async Task<Cart?> GetBasketByUserName(string userName)
    {
        _logger.Information($"BEGIN: Getting basket by username {userName}");
        var basket = await _redisCacheService.GetStringAsync(userName);
        _logger.Information($"END: Getting basket by username {userName}");
        return string.IsNullOrEmpty(basket) ? null : 
            _serializeService.Deserialize<Cart>(basket);
    }

    public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
    {
        _logger.Information($"BEGIN: Updating basket by username {cart.UserName}");
        if (options != null)
        {
            await _redisCacheService.SetStringAsync(cart.UserName,
                _serializeService.Serialize(cart), options);
        }
        else
        {
            await _redisCacheService.SetStringAsync(cart.UserName,
                _serializeService.Serialize(cart));
        }
        _logger.Information($"END: Updating basket by username {cart.UserName}");
        return await GetBasketByUserName(cart.UserName);
    }

    public async Task<bool> DeleteBasketFromUserName(string userName)
    {
        try
        {
            _logger.Information($"BEGIN: Deleting basket from username {userName}");
            await _redisCacheService.RemoveAsync(userName);
            _logger.Information($"END: Deleting basket from username {userName}");
            return true;
        }
        catch(Exception e)
        {
            _logger.Error(e, "Error deleting basket from username {userName}", userName);
            throw;
        }
    }
}