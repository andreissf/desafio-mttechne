using System.Reflection;
using FluentValidation.Results;
using Lancamentos.Application;
using Lancamentos.Application.Service;
using Lancamentos.Application.UseCases.Create;
using Lancamentos.Application.UseCases.Delete;
using Lancamentos.Application.UseCases.Get;
using Lancamentos.Application.UseCases.Update;
using Lancamentos.Infrastructure;
using Lancamentos.Domain;
using Lancamentos.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MessageBus;
using Refit;
using Silverback.Messaging.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LancamentosDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMessageBus(builder.Configuration["RabbitMQConnectionString"]);

builder.Services.AddScoped<ILancamentoRepository, LancamentoRepository>();
builder.Services.AddScoped<ITipoLancamentoRepository, TipoLancamentoRepository>();

builder.Services.AddRefitClient<ISaldoService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["SaldoUrl"]));
    
builder.Services.AddScoped<IRequestHandler<CreateLancamentoCommand, ValidationResult>, CreateLancamentoHandler>();
builder.Services.AddScoped<IRequestHandler<GetLancamentoCommand, Lancamento>, GetLancamentoHandler>();
builder.Services.AddScoped<IRequestHandler<GetLancamentosCommand, IEnumerable<Lancamento>>, GetLancamentosHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateLancamentoCommand, Lancamento>, UpdateLancamentoHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteLancamentoCommand, ValidationResult>, DeleteLancamentoHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseHealthChecks("/health");

await app.RunAsync();