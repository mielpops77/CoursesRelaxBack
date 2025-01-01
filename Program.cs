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
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();


string connectionString = configuration.GetConnectionString("DefaultConnection");

// Configure DbContext with Azure SQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors(corsPolicyName);

app.UseHttpsRedirection();

app.UseAuthentication(); // Add this line for authentication
app.UseAuthorization();

app.MapControllers();
app.Run();
