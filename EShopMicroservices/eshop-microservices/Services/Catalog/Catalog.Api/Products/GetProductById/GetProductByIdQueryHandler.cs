

namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdCommand(Guid Id):IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);
public class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdCommand, GetProductByIdResult>
    {
    public async Task<GetProductByIdResult> Handle(GetProductByIdCommand query, CancellationToken cancellationToken)
        {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        if (product is null) throw new ProductNotFoundException(query.Id);
        return new GetProductByIdResult(product);
        }
    }

