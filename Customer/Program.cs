using Customer;
using Customer.Handlers;
using Customer.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


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

await builder.Build().RunAsync();
