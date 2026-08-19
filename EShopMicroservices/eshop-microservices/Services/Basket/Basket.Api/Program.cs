
using BuildingBlocks.Messaging.MassTransit;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBahavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("database")!);
    option.Schema.For<ShoppingCart>().Identity(sc=>sc.UserName);
}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddMessageBroker(builder.Configuration);
// Add services to the container.
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
        {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    return handler;
});

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options =>
{

});
app.UseHealthChecks("/health", new HealthCheckOptions
    {
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.Run();

