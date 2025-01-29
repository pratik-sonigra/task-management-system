using Microsoft.EntityFrameworkCore;
using TaskManagement.Contracts.DTOs;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Shared.Authentication;
using TaskManagement.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register the DbContext
builder.Services.AddDbContext<TaskManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.Configure<JWTConfigurations>(builder.Configuration.GetSection("Jwt"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services and repositories
builder.Services.AddServiceLayer();
builder.Services.AddRepositoryLayer();
builder.Services.AddJwtAuthentication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthentication();

// Use the custom JWT authentication middleware
app.UseMiddleware<JwtAuthenticationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
