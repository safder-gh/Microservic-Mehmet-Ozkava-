using Marten;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
//builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("database")!);
}).UseLightweightSessions();
var app = builder.Build();
app.MapCarter();
app.Run();
