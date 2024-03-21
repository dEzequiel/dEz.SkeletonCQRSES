using Confluent.Kafka;
using dEz.SkeletonCQRSES.Command.Api.Commands;
using dEz.SkeletonCQRSES.Command.Domain.Aggregates;
using dEz.SkeletonCQRSES.Command.Infrastructure.Configuration;
using dEz.SkeletonCQRSES.Command.Infrastructure.Dispatchers;
using dEz.SkeletonCQRSES.Command.Infrastructure.Handlers;
using dEz.SkeletonCQRSES.Command.Infrastructure.Producers;
using dEz.SkeletonCQRSES.Command.Infrastructure.Repositories;
using dEz.SkeletonCQRSES.Command.Infrastructure.Services;
using dEz.SkeletonCQRSES.ES.Core;
using dEz.SkeletonCQRSES.ES.Core.Domain;
using dEz.SkeletonCQRSES.ES.Core.Events;
using dEz.SkeletonCQRSES.ES.Core.Handlers;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using dEz.SkeletonCQRSES.ES.Core.Producers;
using dEz.SkeletonCQRSES.Query.Domain.Repositories;
using dEz.SkeletonCQRSES.Query.Domain.Services;
using dEz.SkeletonCQRSES.Query.Infrastructure.Repositories;
using dEz.SkeletonCQRSES.Query.Infrastructure.Services;
using dEz.SkeletonCQRSES.Shared.Events;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

/// Path for configuration.
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.Development.json", true, true);

// Mongo database.
builder.Services.Configure<MongoSettings>(options => builder.Configuration.GetSection("MongoSettings").Bind(options));

BsonClassMap.RegisterClassMap<BaseEvent>();
BsonClassMap.RegisterClassMap<CompanyCreatedEvent>();
BsonClassMap.RegisterClassMap<CompanyRemovedEvent>();
BsonClassMap.RegisterClassMap<CompanyUpdatedEvent>();

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventSourcingHandler<CompanyAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler,CommandHandler>();

var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();

dispatcher.RegisterHandler<AddCompanyCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<DeleteCompanyCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<UpdateCompanyCommand>(commandHandler.HandleAsync);

builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    // Space
    cfg.SwaggerDoc("CommandApi",
        new OpenApiInfo { Title = "Command Api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.ShowCommonExtensions();
        c.SwaggerEndpoint("CommandApi/swagger.json", "CommandApi");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
