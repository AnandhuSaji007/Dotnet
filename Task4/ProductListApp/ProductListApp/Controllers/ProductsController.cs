using Microsoft.AspNetCore.Mvc;

namespace ProductListApp.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Pen", Price = 10 },
                new Product { Id = 2, Name = "Notebook", Price = 50 },
                new Product { Id = 3, Name = "Bag", Price = 300 }
            };

            return View(products);
        }
    }
}
