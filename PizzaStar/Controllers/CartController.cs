using System;
using Microsoft.AspNetCore.Mvc;
using PizzaStar.Interfaces;
using PizzaStar.Models.CartItem;
using PizzaStar.Services;
using PizzaStar.ViewModels;

namespace PizzaStar.Controllers
{
   public class CartController : Controller
    {
        private readonly IProduct _productRepository;

        public CartController(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cart = HttpContext.Session.GetCart();
            var model = new CartViewModel();

            foreach (var item in cart)
            {
                var product =
                    await _productRepository.GetProductAsync(item.ProductId);

                if (product != null && item.Quantity > 0)
                {
                    model.Items.Add(new CartLineViewModel
                    {
                        Product = product,
                        Quantity = item.Quantity
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId)
        {
            var product =
                await _productRepository.GetProductAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetCart();
            var item = cart.FirstOrDefault(
                x => x.ProductId == productId);

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }

            HttpContext.Session.SaveCart(cart);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            var cart = HttpContext.Session.GetCart();

            cart.RemoveAll(x => x.ProductId == productId);

            HttpContext.Session.SaveCart(cart);

            return RedirectToAction(nameof(Index));
        }
}
}
