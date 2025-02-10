using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Selu383.SP25.Api.Data;
using Selu383.SP25.Api.Entities;
using Selu383.SP25.Api.Seeding;

namespace Selu383.SP25.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<DataContext>();
            builder.Services.AddScoped<DbInitializer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
                app.UseItToSeedSqlServer();
                

            }

            

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }


    }

}
