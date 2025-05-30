using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Infra.Data;
using Soat.Eleven.FastFood.Infra.Repositories;
using Swashbuckle.AspNetCore.Swagger;

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

builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
    option.AddPolicy("ClienteLogin", policy => policy.RequireClaim("AccessType", "ClienteLogin"));
    option.AddPolicy("ClienteIdentification", policy => policy.RequireClaim("AccessType", "ClienteIdentification"));
    option.AddPolicy("AdminLogin", policy => policy.RequireClaim("AccessType", "AdminLogin"));
});

builder.Services.RegisterValidation();
builder.Services.RegisterServices();
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryPgSql<>));

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "FastFood Api",
        Description = "Projeto acadêmico desenvolvido para a disciplina de Arquitetura de Software (FIAP - Pós-graduação)"
    });
});

var app = builder.Build();

app.UseMiddleware<ErrorExceptionHandlingMiddleware>(app.Logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(new SwaggerOptions() { OpenApiVersion = OpenApiSpecVersion.OpenApi2_0 });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Exemplo de migração automática do banco de dados
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    db.Database.Migrate();
//}
app.Run();

// -https://balta.io/blog/aspnet-core-autenticacao-autorizacao
// -https://learn.microsoft.com/en-us/aspnet/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api
// -https://medium.com/@codewithankitsahu/authentication-and-authorization-in-net-8-web-api-94dda49516ee