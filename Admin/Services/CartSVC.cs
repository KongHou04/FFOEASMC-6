using Customer.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Customer.Services
{
    public class CartSVC
    {
        private readonly IJSRuntime _jsRuntime;
        private const string CartKey = "cart";

        public int CartItemCount { get; private set; }
        public event Action? OnChange;

        public CartSVC(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            UpdateCartItemCountAsync().GetAwaiter();
        }

        public async Task SaveCartAsync(IEnumerable<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CartKey, cartJson);
            NotifyCartChanged();
        }

        public async Task<List<CartItem>?> GetCartAsync()
        {
            var cartJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CartKey);
            return cartJson == null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        public async Task AddToCartAsync(Guid productId)
        {
            var cart = await GetCartAsync() ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Add(new CartItem() { ProductId = productId });
            }
            await SaveCartAsync(cart);
            await UpdateCartItemCountAsync();
            await _jsRuntime.InvokeVoidAsync("showNotification", "Product hase been added successfully");
        }

        public async Task UpdateCartItem(Guid productId, int quantity)
        {
            var cart = await GetCartAsync();
            if (cart == null)
                return;
            var existingCartItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existingCartItem == null)
                return;
            var index = cart.IndexOf(existingCartItem);
            cart[index].Quantity = quantity;
            await SaveCartAsync(cart);
            await UpdateCartItemCountAsync();
        }

        public async Task RemoveItemFromCart(Guid productId)
        {
            var cart = await GetCartAsync();
            if (cart == null)
                return;
            var deleteItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (deleteItem != null)
                cart.Remove(deleteItem);
            await SaveCartAsync(cart);
            await UpdateCartItemCountAsync();
        }

        public async Task ClearCartAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", CartKey);
            await UpdateCartItemCountAsync();
        }

        private async Task UpdateCartItemCountAsync()
        {
            var cart = await GetCartAsync();
            CartItemCount = cart == null ? 0 : cart.Sum(od => od.Quantity);
            NotifyCartChanged();
        }

        private void NotifyCartChanged() => OnChange?.Invoke();
    }
}
