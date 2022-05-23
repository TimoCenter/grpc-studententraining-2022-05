using Backend.GrpcServices;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddSingleton<ChatStateService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.MapGrpcService<ChatGrpcService>();
app.MapGrpcService<CarGrpcService>();

app.Run();
