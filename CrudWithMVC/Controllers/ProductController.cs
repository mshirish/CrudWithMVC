using CrudWithMVC.DataAccessLayer;
using CrudWithMVC.Enums;
using CrudWithMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CrudWithMVC.Controllers
{
	public class ProductController : Controller
	{
		private readonly AppDbContext _context;

		public ProductController(AppDbContext context)
		{
			_context = context;
		}

		//This is initial controller. Will be hit as soon as the application starts.
		public async Task<IActionResult> Index()
		{
			var allProducts = await _context.Products.ToListAsync(); //getting all the products
			return View(allProducts);
		}

		public async Task<IActionResult> ViewProductDetails(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		public IActionResult CreateProduct()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateProductAsync([Bind("Id,Name,Category,Price,ExpiryDate")] Product productItem)
		{
			if (ModelState.IsValid)
			{
				_context.Products.Add(productItem);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(productItem);
		}

		public async Task<IActionResult> UpdateProduct(int? id)
		{
			var productToUpdate = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
			return View(productToUpdate);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateProductAsync(int id, [Bind("Id,Name,Category,Price,ExpiryDate")] Product productItem)
		{
			if (id != productItem.Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{

				_context.Products.Update(productItem);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(productItem);
		}

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var productToDelete = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
