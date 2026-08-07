namespace Basket.Api.Data;

public interface IBasketRepository
    {
    public Task<ShoppingCart> GetBasket(string UserName,CancellationToken cancellationToken = default(CancellationToken));
    public Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default(CancellationToken));
    public Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default(CancellationToken));
    }

