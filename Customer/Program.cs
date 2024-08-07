using Customer;
using Customer.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Web;
using Customer.Handlers;

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
builder.Services.AddScoped<VnPayService>();
builder.Services.AddHttpClient<VnPayService>();
builder.Services.AddHttpClient("AuthorizedClient")
    .AddHttpMessageHandler<AuthHttpClientHandler>();

await builder.Build().RunAsync();