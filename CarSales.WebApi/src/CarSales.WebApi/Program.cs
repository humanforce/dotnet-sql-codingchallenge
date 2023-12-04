
using CarSales.Common.Database;
using CarSales.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace CarSales.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = new CarApiJsonContractResolver();
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        string? connectionString = builder.Configuration.GetConnectionString("Database");
        builder.Services.AddDbContext<CarSalesDbContext>(t => t.UseSqlServer(connectionString));

        builder.Services.AddTransient<ICarsService, CarsService>();
        builder.Services.AddTransient<ISalesService, SalesService>();

        var app = builder.Build();

        var allowedOrigins = app.Configuration.GetSection("AppSettings:AllowedOrigins").Get<string[]>();
        app.UseCors(t => t.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader());

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
