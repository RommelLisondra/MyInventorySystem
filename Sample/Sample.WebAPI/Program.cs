
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sample.ApplicationService;
using Sample.Infrastructure;
using Sample.Infrastructure.EntityFramework;
using Sample.WebAPI;
using Sample.WebAPI.Auth;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------
// 1. Connection String
// ------------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<INVENTORYDbContext>(options =>
    options.UseSqlServer(connectionString!));

// ------------------------------------------------------------
// 2. Register Application Services & UnitOfWork
// ------------------------------------------------------------
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ------------------------------------------------------------
// 3. JWT SETTINGS
// ------------------------------------------------------------
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

// Add JWT Authentication (should be BEFORE builder.Build)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// ------------------------------------------------------------
// 4. Controllers + Swagger
// ------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------------------------------------------
// 5. CORS
// ------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ------------------------------------------------------------
// MIDDLEWARE PIPELINE
// ------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS
app.UseCors("AllowReactApp");

// Authentication MUST come BEFORE Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

