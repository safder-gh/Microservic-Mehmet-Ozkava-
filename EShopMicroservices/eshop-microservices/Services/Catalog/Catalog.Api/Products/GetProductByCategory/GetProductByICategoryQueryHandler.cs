

namespace Catalog.Api.Products.GetProductById;

public record GetProductByCategoryCommand(string Category):IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryCommand, GetProductByCategoryResult>
    {
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryCommand query, CancellationToken cancellationToken)
        {
        var products = await session.Query<Product>().Where(p => p.Categories.Contains(query.Category)).ToListAsync();
        return new GetProductByCategoryResult(products);
        }
    }

