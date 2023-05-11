using System.Text;
using ExpenseTracker.Config;
using ExpenseTracker.Data;
using ExpenseTracker.Helpers;
using ExpenseTracker.Helpers.Inteface;
using ExpenseTracker.Manager;
using ExpenseTracker.Manager.Interface;
using ExpenseTracker.Models;
using ExpenseTracker.Provider;
using ExpenseTracker.Provider.Interface;
using ExpenseTracker.Repository;
using ExpenseTracker.Repository.Interface;
using ExpenseTracker.Services;
using ExpenseTracker.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value!);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true,
    };
});
//unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//provider
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

//helpers
builder.Services.AddScoped<IJwtService, JwtService>();

//Managers
builder.Services.AddScoped<IAuthManager, AuthManager>();

//services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

//repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
