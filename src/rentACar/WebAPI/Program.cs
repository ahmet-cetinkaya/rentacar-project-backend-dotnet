using Application;
using Core.Application.Pipelines.Caching;
using Core.CrossCuttingConcerns.Exceptions;
using Infrastructure;
using Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddDistributedMemoryCache(); // InMemory
builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");

builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();