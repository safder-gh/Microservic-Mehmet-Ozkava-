using Marten;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> products);
public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
        var products = await session.Query<Product>().ToListAsync();
        return new GetProductsResult(products);
        }
    }

