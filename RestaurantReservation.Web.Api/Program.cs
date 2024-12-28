using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using RestaurantReservation.Application.CQRS.Commands;
using RestaurantReservation.Application.Services;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Domain.Services;
using RestaurantReservation.Infrastructure.Authorization;
using RestaurantReservation.Infrastructure.Middleware;
using RestaurantReservation.Infrastructure.Persistence;
using RestaurantReservation.Infrastructure.Repositories;
using RestaurantReservation.Infrastructure.Services;
using RestaurantReservation.Web.Api.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

builder.Services.AddDbContext<RestaurantReservationDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("RestaurantReservation.Web.Api")
    );
});

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddScoped<ExceptionHandlingMiddleware>();
builder.Services.AddSingleton<IUserContextService, UserContextService>();
builder.Services.AddScoped<IAuthorizationHandler, TenantAuthorizationHandler>();

builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssembly(typeof(AddTableToRestaurantCommand).Assembly));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TenantPolicy", policy =>
        policy.Requirements.Add(new TenantAuthorizationRequirement()));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

//app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();