using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Manager API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(
                options =>
                {
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                }
            )
            .AddEntityFrameworkStores<ApplicationDbContext>();
//JWT Authentication
var jwtSection = builder.Configuration.GetSection("Jwt");
string securityKey = jwtSection["Key"]!;
byte[] keyBytes = ASCIIEncoding.ASCII.GetBytes(securityKey);
var key = new SymmetricSecurityKey(keyBytes);
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = "default";
        options.DefaultChallengeScheme = "default";
    }
    )
.AddJwtBearer("default", options =>
options.TokenValidationParameters = new TokenValidationParameters
{
    IssuerSigningKey = key,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidIssuer = jwtSection["Issuer"],
    ValidAudience = jwtSection["Audience"]
}
);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/error");
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();