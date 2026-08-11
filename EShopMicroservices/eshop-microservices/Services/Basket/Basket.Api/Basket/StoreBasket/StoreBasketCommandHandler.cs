using Basket.Api.Data;
using Discount.Grpc;
using JasperFx.Events.Daemon;

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
public class StoreBasketCommandHandler(IBasketRepository repository,DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommad, StoreBasketResult>
    {
    public async Task<StoreBasketResult> Handle(StoreBasketCommad command, CancellationToken cancellationToken)
        {
        await ApplyDiscount(command, cancellationToken);
        var result = await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(result.UserName);
        }

    private async Task ApplyDiscount(StoreBasketCommad command, CancellationToken cancellationToken)
        {
        foreach (var product in command.Cart.Items)
            {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = product.ProductName }, cancellationToken: cancellationToken);
            product.Price -= coupon.Amount;
            }
        }
    }

