using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Application.Services.Interfaces;
using Soat.Eleven.FastFood.Infra.Data;
using Soat.Eleven.FastFood.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryPgSql<>));

builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<ITokenAtendimentoService, TokenAtendimentoService>();

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
