
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;

using System.Text;

using InventarioCCL.Data;

var builder = WebApplication.CreateBuilder(args);

// Base de datos

builder.Services.AddDbContext<AppDbContext>(options =>

    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT

var jwtKey = builder.Configuration["Jwt:Key"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    .AddJwtBearer(options =>

    {

        options.TokenValidationParameters = new TokenValidationParameters

        {

            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))

        };

    });

// CORS 

builder.Services.AddCors(options =>

{

    options.AddPolicy("AllowAngular", policy =>

        policy.WithOrigins("http://localhost:4200")

              .AllowAnyHeader()

              .AllowAnyMethod());

});

builder.Services.AddControllers();

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())

{

    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var admin = db.Usuarios.FirstOrDefault(u => u.Username == "admin");

    if (admin != null && !admin.Password.StartsWith("$2"))

    {

        admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);

        db.SaveChanges();

    }

}

app.UseCors("AllowAngular");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

