using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Api.Data;
using EmployeeManagement.Api.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
  ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

if(builder.Environment.IsDevelopment()) {
  builder.Services.AddSwaggerGen();
  builder.Services.AddOpenApi();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();