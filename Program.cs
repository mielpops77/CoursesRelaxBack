using Microsoft.EntityFrameworkCore;
using CoursesRelaxBack.Data;

var builder = WebApplication.CreateBuilder(args);

// Activer les paramètres régionaux complets

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(); // Ajoute le support pour les contrôleurs

// Configure CORS
var corsPolicyName = "CoursesRelaxCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
     policy
            .AllowAnyOrigin() // Autorise toutes les origines
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Configure DbContext with Azure SQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyName);

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
