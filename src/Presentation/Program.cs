
using Application.Models.Announcements.Commands;
using Application.Models.Employee.Commands;
using Application.Services.Implementation.IEmployee;
using Application.Services.Implementation.MeetingService;
using Application.Services.Interface.IAuth;
using Application.Services.Interface.IEmployee;
using Application.Services.Interface.IMeeting;
using Domain.Entities.User;
using Infrastructure.DbConetxt;
using Infrastructure.Repositories.Implementation;
using Infrastructure.Repositories.Implementation.AnnouncementRepo;
using Infrastructure.Repositories.Implementation.EmployeeRepo;
using Infrastructure.Repositories.Implementation.MeetingRepo;
using Infrastructure.Repositories.Interfaces.IAnnouncementRepo;
using Infrastructure.Repositories.Interfaces.IDepartmentRepo;
using Infrastructure.Repositories.Interfaces.IEmployeeRepo;
using Infrastructure.Repositories.Interfaces.IMeetingRepo;
using Infrastructure.Services.Implementation.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add Identity with custom ApplicationUser and int as primary key
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register MediatR services
// Register MediatR for Employee-related commands and queries
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeCommand).Assembly));

// Register MediatR for Announcement-related commands and queries
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAnnouncementCommand).Assembly));




// Register application services for Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();

builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();



// JWT Settings from configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Add Authorization policies for role-based access
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireHRAdminRole", policy => policy.RequireRole("HRAdmin"));
    options.AddPolicy("RequireEmployeeRole", policy => policy.RequireRole("Employee"));
    options.AddPolicy("RequireITSupportRole", policy => policy.RequireRole("ITSupport"));
    options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
});

// Configure CORS (allowing all origins for development)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add controllers
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
        await SeedRoles(roleManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error occurred seeding roles: {ex.Message}");
    }
}
// Swagger setup for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware setup
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();  // Ensure Authentication middleware is invoked
app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

app.Run();


async Task SeedRoles(RoleManager<IdentityRole<int>> roleManager)
{
    var roles = new[] { "Admin", "HR", "Employee", "PendingApproval" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
        }
    }
}