using MiniERP.Domain.Entities;

namespace MiniERP.Application.Interfaces
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetAllAsync();
        Task<Sale?> GetByIdAsync(int id);
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(int id);

        Task<List<Customer>> GetActiveCustomersAsync();
        Task<List<Product>> GetActiveProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);

        Task SaveChangesAsync();
    }
}
