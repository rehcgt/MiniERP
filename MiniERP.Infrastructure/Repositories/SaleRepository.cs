using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Interfaces;
using MiniERP.Domain.Entities;
using MiniERP.Infrastructure.Data;

namespace MiniERP.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(x => x.Customer)
                .Include(x => x.Details)
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.SaleDate)
                .ToListAsync();
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _context.Sales
                .Include(x => x.Customer)
                .Include(x => x.Details)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
        }

        public Task UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var sale = await _context.Sales
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            if (sale == null)
            {
                return;
            }

            sale.IsActive = false;

            foreach (var detail in sale.Details)
            {
                detail.IsActive = false;
            }
        }

        public async Task<List<Customer>> GetActiveCustomersAsync()
        {
            return await _context.Customers
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<List<Product>> GetActiveProductsAsync()
        {
            return await _context.Products
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(x => x.Id == productId && x.IsActive);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
