using BuidingBlocks.CQRS;
using Catalog.Api.Model;
using Marten;
namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
    public CreateProductCommandValidator()
        {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 150).WithMessage("Name must be 2 to 150 characters length.");
        RuleFor(p => p.Category).NotEmpty().WithMessage("Category is required.");
        RuleFor(p => p.ImageFile).NotEmpty().WithMessage("Image is required.");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
        var product = new Product
            {
            Name = command.Name,
            Description = command.Description,
            Categories = command.Category,
            ImageFile = command.ImageFile,
            Price = command.Price,
            };
        session.Store(product);
        await  session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
        }
    }

