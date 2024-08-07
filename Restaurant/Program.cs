using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Restaurant.Contexts;
using Restaurant.Helpers;
using Restaurant.Mappings;
using Restaurant.Midlewares;
using Restaurant.Models.Db;
using Restaurant.Repositories.Implements;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Implements;
using Restaurant.Services.Interfaces;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string? mailUsername = builder.Configuration["MailInfo:Username"];
string? mailPassword = builder.Configuration["MailInfo:Password"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API document",
        Version = "v1",
        Description = "This is a document API. Provides API endpoints for the our project",
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("http://localhost:5272/")
        },
        Contact = new OpenApiContact
        {
            Name = "API Support",
            Email = "nguyenphuc14112003@gmail.com",
            Url = new Uri("http://localhost:5272/")
        }
    });
    // ??c các chú thích XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<FFOEContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]);
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<FFOEContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddScoped<ICategoryRES, CategoryRES>();
builder.Services.AddScoped<ICategorySVC, CategorySVC>();

builder.Services.AddScoped<IProductRES, ProductRES>();
builder.Services.AddScoped<IProductSVC, ProductSVC>();

builder.Services.AddScoped<IOrderRES, OrderRES>();
builder.Services.AddScoped<IOrderSVC, OrderSVC>();

builder.Services.AddScoped<IOrderDetailRES, OrderDetailRES>();
builder.Services.AddScoped<IOrderDetailSVC, OrderDetailSVC>();

builder.Services.AddScoped<ICustomerRES, CustomerRES>();
builder.Services.AddScoped<ICustomerSVC, CustomerSVC>();

builder.Services.AddScoped<ICouponTypeRES, CouponTypeRES>();
builder.Services.AddScoped<ICouponTypeSVC, CouponTypeSVC>();

builder.Services.AddScoped<ICouponRES, CouponRES>();
builder.Services.AddScoped<ICouponSVC, CouponSVC>();

builder.Services.AddScoped<IStatisticSVC, StatisticSVC>();

builder.Services.AddScoped<EmailSender>(options => new EmailSender(mailUsername!, mailPassword!));

builder.Services.AddScoped<ImgHelper>();

builder.Services.AddCors(options =>
{
    var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins(allowedOrigins!)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Our API V1");
    });
}

app.UseStaticFiles();

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
