using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Domain.Enums;
using Soat.Eleven.FastFood.Infra.Data;
using Soat.Eleven.FastFood.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString")));

builder.Services.AddCors();

builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(option =>
    {
        option.RequireHttpsMetadata = false;
        option.SaveToken = true;
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["SecretKeyPassword"]!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("Cliente", policy => policy.RequireRole(RolesAuthorization.Cliente));
    option.AddPolicy("Administrador", policy => policy.RequireRole(RolesAuthorization.Administrador));
    option.AddPolicy("ClienteTotem", policy => policy.RequireRole([RolesAuthorization.Cliente, RolesAuthorization.IdentificacaoTotem]));
    option.AddPolicy("Commom", policy => policy.RequireRole([RolesAuthorization.Cliente, RolesAuthorization.Administrador]));
});

builder.Services.RegisterValidation();
builder.Services.RegisterServices();
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryPgSql<>));

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseMiddleware<ErrorExceptionHandlingMiddleware>(app.Logger);

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();