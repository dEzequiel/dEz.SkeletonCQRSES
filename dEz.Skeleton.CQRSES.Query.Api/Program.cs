using Confluent.Kafka;
using dEz.Skeleton.CQRSES.Query.Api.Extensions;
using dEz.Skeleton.CQRSES.Query.Api.Queries;
using dEz.SkeletonCQRSES.ES.Core.Consumers;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using dEz.SkeletonCQRSES.Query.Abstraction;
using dEz.SkeletonCQRSES.Query.Domain.Entities;
using dEz.SkeletonCQRSES.Query.Domain.Repositories;
using dEz.SkeletonCQRSES.Query.Domain.Services;
using dEz.SkeletonCQRSES.Query.Infrastructure.Consumers;
using dEz.SkeletonCQRSES.Query.Infrastructure.DataAccess;
using dEz.SkeletonCQRSES.Query.Infrastructure.Dispatchers;
using dEz.SkeletonCQRSES.Query.Infrastructure.Handlers;
using dEz.SkeletonCQRSES.Query.Infrastructure.Repositories;
using dEz.SkeletonCQRSES.Query.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));


Action<DbContextOptionsBuilder> configureDbContext = (o => o.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnectionString")));
builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<IEventHandler, dEz.SkeletonCQRSES.Query.Infrastructure.Handlers.EventHandler>();
builder.Services.AddScoped<IQueryHandler, QueryHandler>();
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();

var queryHandler = builder.Services.BuildServiceProvider().GetRequiredService<IQueryHandler>();
var queryDispatcher = new QueryDispatcher();

queryDispatcher.RegisterHandler<FindCompanyByIdQuery>(queryHandler.HandleAsync);
queryDispatcher.RegisterHandler<FindAllCompaniesQuery>(queryHandler.HandleAsync);
builder.Services.AddSingleton<IQueryDispatcher<Company>>(_ =>  queryDispatcher);

builder.Services.AddHostedService<ConsumerHostedService>();

var app = builder.Build();
app.ConfigureExceptionHandler();

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
