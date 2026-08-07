using Basket.Api.Data;

namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketCommad(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);
public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommad>
    {
    public StoreBasketCommandValidator()
        {
        RuleFor(c => c.Cart).NotNull().WithMessage("Cart can't be null.");
        RuleFor(c => c.Cart.UserName).NotEmpty().WithMessage("UserName is required. ");
        }
    }
public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommad, StoreBasketResult>
    {
    public async Task<StoreBasketResult> Handle(StoreBasketCommad commad, CancellationToken cancellationToken)
        {
        var result = await repository.StoreBasket(commad.Cart, cancellationToken);
        return new StoreBasketResult(result.UserName);
        }
    }

