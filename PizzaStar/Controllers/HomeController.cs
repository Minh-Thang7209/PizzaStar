using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaStar.Interfaces;
using PizzaStar.Models;
using PizzaStar.Models.Pages;

namespace PizzaStar.Controllers;

public class HomeController : Controller
{
    private readonly IProduct _products;
    private readonly ICategory _categories;

    public HomeController(IProduct products, ICategory categories)
    {
        _products = products;
        _categories = categories;
    }

    [Route("/")]
    [HttpGet]
    public IActionResult Index(QueryOptions options)
    {
        return View(_products.GetAllProducts(options));
    }

    [Route("/product")]
    [HttpGet]
    public async Task<IActionResult> GetProduct(int productId, string? returnUrl)
    {
        if (productId > 0)
        {
            var currentProduct = await _products.GetProductWithCategoryAsync(productId);
            if (currentProduct != null)
            {
                //Що б не було встановлено активної головної сторінки.
                ViewBag.CategoryId = double.NaN;
                return View(new CurrentProductViewModel
                {
                    Product = currentProduct,
                    ReturnUrl = returnUrl
                });
            }
        }
        return NotFound();
    }
}