using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Api.Data;
using EmployeeManagement.Api.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Swashbuckle

var app = builder.Build();

if(app.Environment.IsDevelopment()) {
  app.UseSwagger();      // serves swagger/v1/swagger.json
  app.UseSwaggerUI();    // interactive UI
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();