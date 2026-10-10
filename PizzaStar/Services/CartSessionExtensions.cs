using System;
using System.Text.Json;
using PizzaStar.Models.CartItem;

namespace PizzaStar.Services
{
    public static class CartSessionExtensions
    {
        private const string CartSessionKey = "Cart";

        public static List<CartItem> GetCart(this ISession session)
        {
            var cartJson = session.GetString(CartSessionKey);

            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }

            return JsonSerializer.Deserialize<List<CartItem>>(cartJson)
                   ?? new List<CartItem>();
        }

        public static void SaveCart(
            this ISession session,
            List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            session.SetString(CartSessionKey, cartJson);
        }
    }
}
