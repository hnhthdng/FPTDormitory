using DormDataAccess.DBContext;
using DormModel.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using DormUtility.Email;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

using DormUtility.AutoMapper;
using DormDataAccess.Service.IServices;
using DormDataAccess.Services;
using DormDataAccess.Services.IService;
using DormDataAccess.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Congfig DbContext
builder.Services.AddDbContext<DormitoryDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Congfig Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false; // Don't require non-alphanumeric characters
    options.Password.RequireUppercase = false; // Don't require uppercase letters
    options.Password.RequiredLength = 3;

    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<DormitoryDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
   } )
        .AddCookie(options =>
        {
            options.Cookie.Name = "MyCookie";
            options.LoginPath = "/account/login";
            options.AccessDeniedPath = "/account/accessdenied";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        });

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

//Add Email Config
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<DormDAO>();
builder.Services.AddScoped<IDormService, DormService>();
builder.Services.AddScoped<FloorDAO>();
builder.Services.AddScoped<IFloorService, FloorService>();
builder.Services.AddScoped<RoomDAO>();
builder.Services.AddScoped<IRoomService, RoomService>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();
