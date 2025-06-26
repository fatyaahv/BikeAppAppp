using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BikeAppApp.Models;
using BikeAppApp.Api.Mapping;   // ?? add this


var builder = WebApplication.CreateBuilder(args);

// Register DbContext from the main project (BikeAppApp)
builder.Services.AddDbContext<MotoDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Datacon")));
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add controllers
builder.Services.AddControllers()
    .PartManager.ApplicationParts.Clear(); // Remove all discovered parts

builder.Services.AddControllers()
    .AddApplicationPart(typeof(BikeAppApp.Api.Controllers.MotosikletApiController).Assembly) // Only API project assembly
    .AddControllersAsServices();




// Add Swagger generator
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BikeApp API",
        Version = "v1",
        Description = "API documentation for BikeApp project"
    });
});

//builder.Services.AddAutoMapper(typeof(BikeAppApp.Api.Mapping.MappingProfile));
var app = builder.Build();

//builder.Services.AddAutoMapper(typeof(MappingProfile));

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BikeApp API v1");
        c.RoutePrefix = "swagger"; // Use /swagger to access
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
