using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Infra.Data;
using Soat.Eleven.FastFood.Infra.Repositories;
using Soat.Eleven.FastFood.Api.Adapters;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Application.Interfaces;

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

builder.Services.RegisterValidation();
builder.Services.RegisterServices();
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryPgSql<>));
builder.Services.AddScoped<IArmazenamentoArquivoService, ArmazenamentoArquivoAdapter>();

var app = builder.Build();

app.UseMiddleware<ErrorExceptionHandlingMiddleware>(app.Logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

//Exemplo de migra��o autom�tica do banco de dados
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    db.Database.Migrate();
//}
app.Run();
