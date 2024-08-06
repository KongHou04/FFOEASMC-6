using Admin.Components;
using Customer.Handlers;
using Customer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<ImageSVC>();
builder.Services.AddScoped<ProductSVC>();
builder.Services.AddScoped<CategorySVC>();
builder.Services.AddScoped<CartSVC>();
builder.Services.AddScoped<CouponSVC>();
builder.Services.AddScoped<OrderSVC>();
builder.Services.AddScoped<NotifySVC>();
builder.Services.AddScoped<AuthSVC>();
builder.Services.AddScoped<LocalStorageSVC>();
builder.Services.AddTransient<AuthHttpClientHandler>();

builder.Services.AddHttpClient("AuthorizedClient")
    .AddHttpMessageHandler<AuthHttpClientHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
