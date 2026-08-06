using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.GetProductById;

public record DeleteProductByIdRequest(Guid Id);
public record DeleteProductByIdResponse(bool IsSuccess);

public class DeleteProductByIdEndpoint() : ICarterModule
    {
    public void AddRoutes(IEndpointRouteBuilder app)
        {
        app.MapDelete("/products/{id}", async (Guid id,ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductByIdCommand(id));
            var response = result.Adapt<DeleteProductByIdResponse>();
            return Results.Ok(response);
        }).WithName("DeleteProductsById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product By Id")
            .WithDescription("Delete Product By Id");
        }
    }

