using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => {
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.WithOrigins("https://localhost:4200");
});

app.MapControllers();

app.Run();
