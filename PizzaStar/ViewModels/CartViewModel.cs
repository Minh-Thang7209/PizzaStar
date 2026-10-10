using System;
using PizzaStar.Models;

namespace PizzaStar.ViewModels
{
    public class CartViewModel
    {
        public List<CartLineViewModel> Items { get; set; } = new();

        public decimal TotalPrice =>
            Items.Sum(item => item.Product.Price * item.Quantity);
    }

    public class CartLineViewModel
    {
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
