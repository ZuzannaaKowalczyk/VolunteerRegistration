using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VolunteerRegistration.Models;

namespace VolunteerRegistration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Pobranie Connection String z appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Dodanie DbContext do Dependency Injection
            builder.Services.AddDbContext<VolunteerRegistrationContext>(options =>
                options.UseSqlServer(connectionString));


           

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            // Uruchomienie seedowania danych
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<VolunteerRegistrationContext>();
                SeedData.GenerateVolunteers(dbContext); // Wywo³anie generatora danych
            }

            app.Run();
        }
    }
}
