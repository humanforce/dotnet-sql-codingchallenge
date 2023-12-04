
using CarSales.Common.Database;
using CarSales.WebApi.Configuration;
using CarSales.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        builder.Services.Configure<IdentityProviderSettings>(builder.Configuration.GetSection("AppSettings:IdentityProvider"));
        var identityProviderSettings = builder.Configuration.GetSection("AppSettings:IdentityProvider").Get<IdentityProviderSettings>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = identityProviderSettings.Authority;
            options.Audience = identityProviderSettings.Audience;
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("read:cars", policy =>
            {
                policy.RequireAuthenticatedUser();
            });
            options.AddPolicy("write:cars", policy =>
            {
                policy.RequireAuthenticatedUser();
            });
            options.AddPolicy("write:sales", policy =>
            {
                policy.RequireAuthenticatedUser();
            });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        string? connectionString = builder.Configuration.GetConnectionString("Database");
        builder.Services.AddDbContext<CarSalesDbContext>(t => t.UseSqlServer(connectionString));

        builder.Services.AddTransient<ICarsService, CarsService>();
        builder.Services.AddTransient<ISalesService, SalesService>();
        builder.Services.AddTransient<IAuthService, AuthService>();

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
