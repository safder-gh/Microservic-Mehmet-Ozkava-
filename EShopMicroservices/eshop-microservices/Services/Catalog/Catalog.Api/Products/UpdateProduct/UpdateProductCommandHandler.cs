using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id,string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
    public UpdateProductCommandValidator()
        {
        RuleFor(p => p.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 150).WithMessage("Name must be 2 to 150 characters length.");
        RuleFor(p => p.Category).NotEmpty().WithMessage("Category is required.");
        RuleFor(p => p.ImageFile).NotEmpty().WithMessage("Image is required.");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null) throw new ProductNotFoundException(command.Id);
        product.Name = command.Name;
        product.Description = command.Description;
        product.Categories = command.Category;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        session.Update(product);
        await  session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
        }
    }

