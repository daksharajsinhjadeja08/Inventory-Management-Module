
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Models;
using InventoryManagement.Services;

namespace InventoryManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // READ: Display all products
        public IActionResult Index()
        {
            return View(_productService.GetAll());
        }

        // CREATE: Display the form
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: Save the product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // UPDATE: Display the edit form
        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // UPDATE: Save changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // DELETE: Remove a product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
