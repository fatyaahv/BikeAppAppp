using BikeAppApp.Models;
using Microsoft.EntityFrameworkCore;
using BikeAppApp.Middlewares;
//using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MotoDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Datacon")));

/*builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Customize password requirements if needed
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<MotoDBContext>()
.AddDefaultTokenProviders();*/

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles(); // Statik dosyaların servis edilmesi için bu satır önemlidir

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
