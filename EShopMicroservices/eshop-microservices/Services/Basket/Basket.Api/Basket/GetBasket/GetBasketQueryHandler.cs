using Basket.Api.Data;

namespace Basket.Api.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);
public class GetBasketCommandValidator : AbstractValidator<GetBasketQuery>
    {
    public GetBasketCommandValidator()
        {
        RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName is required. ");
        }
    }
public class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
        var result = await repository.GetBasket(query.UserName, cancellationToken);
        return new GetBasketResult(result);
        }
    }

