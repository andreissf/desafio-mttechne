using MediatR;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using Relatorio.Infrastructure.Repository;
using Saldo.Application;
using Saldo.Application.UseCases.Get;
using Saldo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SaldoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddHostedService<SaldoAtualizadoIntegrationHandler>();

builder.Services.AddHealthChecks();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMessageBus(builder.Configuration["RabbitMQConnectionString"]);

builder.Services.AddScoped<ISaldoRepository, SaldoRepository>();

builder.Services.AddScoped<IRequestHandler<GetConsolidadoDiarioCommand, Saldo.Domain.Saldo>, GetConsolidadoDiarioHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.UseHealthChecks("/health");

await app.RunAsync();