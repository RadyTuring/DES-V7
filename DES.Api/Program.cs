
using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DES.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var con = builder.Configuration.GetConnectionString("con");
            builder.Services.AddDbContext<DesAppDbContext>(options => options.UseSqlServer(con));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddTransient(typeof(IRepository<>),typeof(Repository<>)) ;
            builder.Services.AddTransient<IUnitOfWork,UnifOfWork>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
