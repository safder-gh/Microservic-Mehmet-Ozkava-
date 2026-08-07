using System.Text.Json;

namespace Basket.Api.Data;

public class CachedBasketRepository(IBasketRepository repository,IDistributedCache cache ) : IBasketRepository
    {
    public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
        {
        await repository.DeleteBasket(UserName, cancellationToken);
        await cache.RemoveAsync(UserName);
        return true;
        }

    public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
        {
        var cachedBasket = await cache.GetStringAsync(UserName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket)) return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        var basket = await repository.GetBasket(UserName, cancellationToken);
        await  cache.SetStringAsync(UserName, JsonSerializer.Serialize(basket),cancellationToken);
        return basket;
        }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
        await repository.StoreBasket(cart, cancellationToken);
        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);
        return cart;
        }
    }