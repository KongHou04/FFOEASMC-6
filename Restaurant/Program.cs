using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Contexts;
using Restaurant.Helpers;
using Restaurant.Mappings;
using Restaurant.Midlewares;
using Restaurant.Models.Db;
using Restaurant.Repositories.Implements;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Implements;
using Restaurant.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string? mailUsername = builder.Configuration["MailInfo:Username"];
string? mailPassword = builder.Configuration["MailInfo:Password"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
