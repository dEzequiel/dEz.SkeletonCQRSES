using dEz.SkeletonCQRSES.Command.Domain.Aggregates;
using dEz.SkeletonCQRSES.Command.Infrastructure;
using dEz.SkeletonCQRSES.Command.Infrastructure.Dispatchers;
using dEz.SkeletonCQRSES.Command.Infrastructure.Handlers;
using dEz.SkeletonCQRSES.Command.Infrastructure.Repositories;
using dEz.SkeletonCQRSES.Command.Infrastructure.Services;
using dEz.SkeletonCQRSES.ES.Core;
using dEz.SkeletonCQRSES.ES.Core.Domain;
using dEz.SkeletonCQRSES.ES.Core.Handlers;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();

/// Path for configuration.
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.Development.json", true, true);

// Mongo database.
builder.Services.Configure<MongoSettings>(options => builder.Configuration.GetSection("MongoSettings").Bind(options));

// Repositories.
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();

// Services.
builder.Services.AddScoped<IEventStore, EventStore>();

// Handlers
builder.Services.AddScoped<IEventSourcingHandler<CompanyAggregate>, EventSourcingHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
