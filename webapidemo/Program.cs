using Microsoft.EntityFrameworkCore;
using Mywebapidemo.Data;

namespace demowebapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<demowebapi.Services.IProductService, demowebapi.Services.ProductService>();
            builder.Services.AddSingleton<demowebapi.Services.ICategoryService, demowebapi.Services.CategoryService>();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(
                options=>options.UseSqlServer(builder.Configuration.GetConnectionString("constr"))
                );

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