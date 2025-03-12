using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MVCDirectAPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var con = builder.Configuration.GetConnectionString("con");
            builder.Services.AddDbContext<DesAppDbContext>(options => options.UseSqlServer(con));
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IUnitOfWork, UnifOfWork>();
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
                pattern: "{controller=Patient}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
