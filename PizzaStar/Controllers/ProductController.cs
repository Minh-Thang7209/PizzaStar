using System;
using Microsoft.AspNetCore.Mvc;
using PizzaStar.Interfaces;
using PizzaStar.Models;

namespace PizzaStar.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _productRepository;

        public ProductController(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            product.DateOfPublication = DateTime.Now;

            await _productRepository.AddProductAsync(product);

            return RedirectToAction("Index", "Panel");
        }
    }
}
