using EmployeeManagement.Api.Core.Interfaces;
using EmployeeManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure JSON options to avoid object cycle errors when serializing navigation properties
builder.Services.AddControllers()
  .AddJsonOptions(options => {
    // Ignore cycles rather than throwing an exception.
    // Alternative: ReferenceHandler.Preserve will emit $id/$ref metadata if you need reference preservation.
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.MaxDepth = 64;
  });
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