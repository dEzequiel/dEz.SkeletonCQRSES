using dEz.SkeletonCQRSES.Command.Api.Commands;
using dEz.SkeletonCQRSES.Command.Domain.Aggregates;
using dEz.SkeletonCQRSES.Command.Infrastructure;
using dEz.SkeletonCQRSES.Command.Infrastructure.Dispatchers;
using dEz.SkeletonCQRSES.Command.Infrastructure.Handlers;
using dEz.SkeletonCQRSES.Command.Infrastructure.Producers;
using dEz.SkeletonCQRSES.Command.Infrastructure.Repositories;
using dEz.SkeletonCQRSES.Command.Infrastructure.Services;
using dEz.SkeletonCQRSES.ES.Core;
using dEz.SkeletonCQRSES.ES.Core.Domain;
using dEz.SkeletonCQRSES.ES.Core.Handlers;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using dEz.SkeletonCQRSES.ES.Core.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/// Path for configuration.
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.Development.json", true, true);

// Mongo database.
builder.Services.Configure<MongoSettings>(options => builder.Configuration.GetSection("MongoSettings").Bind(options));

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<CompanyAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<IEventProducer, EventProducer>();

builder.Services.AddScoped<ICommandHandler,CommandHandler>();
var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();
dispatcher.RegisterHandler<AddCompanyCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<DeleteCompanyCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<UpdateCompanyCommand>(commandHandler.HandleAsync);
builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

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
