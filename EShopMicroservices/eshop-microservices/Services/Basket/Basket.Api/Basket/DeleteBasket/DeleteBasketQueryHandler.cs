using Basket.Api.Data;

namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBsketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);
public class DeleteBasketCommandValidator : AbstractValidator<DeleteBsketCommand>
    {
    public DeleteBasketCommandValidator()
        {
        RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBsketCommand, DeleteBasketResult>
    {
    public async Task<DeleteBasketResult> Handle(DeleteBsketCommand query, CancellationToken cancellationToken)
        {
        var result = await repository.DeleteBasket(query.UserName, cancellationToken);
        return new DeleteBasketResult(result);
        }
    }

