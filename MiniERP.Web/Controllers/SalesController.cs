using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniERP.Application.Services;
using MiniERP.Web.Models;

namespace MiniERP.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly SaleService _saleService;

        public SalesController(SaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _saleService.GetAllAsync();
            return View(sales);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _saleService.GetByIdAsync(id.Value);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        public async Task<IActionResult> Create()
        {
            var model = new SaleCreateViewModel
            {
                Details = new List<SaleDetailInputViewModel>
                {
                    new SaleDetailInputViewModel { Quantity = 1 }
                }
            };

            await LoadLookupsAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleCreateViewModel model)
        {
            model.Details = model.Details
                .Where(x => x.ProductId > 0)
                .ToList();

            if (ModelState.IsValid)
            {
                var result = await _saleService.CreateAsync(
                    model.SaleDate,
                    model.CustomerId,
                    model.Details.Select(x => (x.ProductId, x.Quantity, x.UnitPrice)).ToList());

                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, result.Message);
            }

            if (model.Details.Count == 0)
            {
                model.Details.Add(new SaleDetailInputViewModel { Quantity = 1 });
            }

            await LoadLookupsAsync();
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _saleService.GetByIdAsync(id.Value);
            if (sale == null)
            {
                return NotFound();
            }

            var model = new SaleEditViewModel
            {
                Id = sale.Id,
                SaleDate = sale.SaleDate,
                CustomerId = sale.CustomerId,
                Details = sale.Details
                    .Select(x => new SaleDetailInputViewModel
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice
                    })
                    .ToList()
            };

            await LoadLookupsAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaleEditViewModel model)
        {
            model.Details = model.Details
                .Where(x => x.ProductId > 0)
                .ToList();

            if (ModelState.IsValid)
            {
                var result = await _saleService.UpdateAsync(
                    model.Id,
                    model.SaleDate,
                    model.CustomerId,
                    model.Details.Select(x => (x.ProductId, x.Quantity, x.UnitPrice)).ToList());

                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, result.Message);
            }

            if (model.Details.Count == 0)
            {
                model.Details.Add(new SaleDetailInputViewModel { Quantity = 1 });
            }

            await LoadLookupsAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _saleService.GetByIdAsync(id.Value);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _saleService.DeleteAsync(id);

            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadLookupsAsync()
        {
            var customers = await _saleService.GetAllCustomersAsync();
            var products = await _saleService.GetAllProductsAsync();

            ViewBag.Customers = new SelectList(customers, "Id", "Name");
            ViewBag.Products = products;
        }
    }
}
