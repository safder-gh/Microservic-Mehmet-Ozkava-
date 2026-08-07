using Basket.Api.Basket.GetBasket;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);
public class StoreBasketEndpoint : ICarterModule
    {
    public void AddRoutes(IEndpointRouteBuilder app)
        {
        app.MapPost("/basket", async (StoreBasketRequest request,ISender sender ) =>
        {
            var command  = request.Adapt<StoreBasketCommad>();
            var result = await sender.Send(command);
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Ok(response);
        }).WithName("UpsertBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store Basket By User Name.")
            .WithDescription("Store Basket By User Name.");
        }
    }

