

using Catalog.Api.Products.UpdateProduct;

namespace Catalog.Api.Products.GetProductById;

public record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByResponse>;
public record DeleteProductByResponse(bool IsSuccess);
public class DeleteProductByIdCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductByIdCommand, DeleteProductByResponse>
    {
    public async Task<DeleteProductByResponse> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
        //var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        //if (product is null) throw new ProductNotFoundException();
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync();
        return new DeleteProductByResponse(true);
        }
    }

